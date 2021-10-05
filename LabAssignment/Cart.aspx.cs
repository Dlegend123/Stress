using LabAssignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class Cart : System.Web.UI.Page
    {
        SqlCommand sqlCommand;
        SqlConnection conn;
        SqlDataReader reader;
        ShoppingCart shoppingCart;
        List<float> subTotals;
        List<float> prices;
        List<Product> Quantities;
        int x;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Cart.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Admin"))
                    if (!Page.Master.FindControl("AdminFunc").Visible)
                        Page.Master.FindControl("AdminFunc").Visible = true;
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Cust"))
                {
                    if (!Page.Master.FindControl("CartLink").Visible)
                        Page.Master.FindControl("CartLink").Visible = true;
                    if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                        (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                }
            }
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            CartTable.Style.Add("max-width", "fit-content");
            stressTable.Style.Add("max-width", "fit-content");
            try
            {
                sqlCommand = new SqlCommand("Select * from proorder where c_name = '" + (Session["Account"] as ApplicationUser).UserName + "' and o_id IS NULL", conn);
                conn.Open();
                reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    Quantities = new List<Product>();
                    sqlCommand = new SqlCommand("select product.quantity,product.p_id from product inner join proorder on proorder.p_id=product.p_id where proorder.c_name='" + (Session["Account"] as ApplicationUser).UserName + "' and o_id IS NULL", conn);
                    reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            p_id = reader["p_id"].ToString(),
                            quantity = int.Parse(reader["quantity"].ToString())
                        };
                        Quantities.Add(product);
                    }
                    sqlCommand = new SqlCommand("select * from proorder inner join proimage on proorder.p_id=proimage.p_id where proorder.c_name='" + (Session["Account"] as ApplicationUser).UserName + "' and o_id IS NULL", conn); reader = sqlCommand.ExecuteReader();
                    string id = "";
                    prices = new List<float>();
                    shoppingCart = new ShoppingCart();
                    subTotals = new List<float>();
                    x = 0;
                    while (reader.Read())
                    {
                        if (id != reader["p_id"].ToString())
                        {
                            id = reader["p_id"].ToString();
                            QuickFunction(DataCollect());
                        }
                    }
                    
                    Session["subTotals"] = subTotals;
                    GrandTotal.Text = "$" + subTotals.Sum().ToString();
                    if (subTotals.Count == 0)
                    {
                        ShowEmpty();
                    }
                }
                else
                {
                    ShowEmpty();
                }
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
            Session["shoppingCart"] = shoppingCart;
        }

        protected void ProceedCheck_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Checkout.aspx", false);
        }
        protected void RemoveProduct_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            sqlCommand = new SqlCommand("Delete  from proorder where c_name= '" + (Session["Account"] as ApplicationUser).UserName + "' and p_id='" + button.ID.Split('*').First() + "' and o_id IS NULL", conn);
            try
            {
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
            shoppingCart.products.RemoveAll(x => x.p_id == button.ID.Split('*').First());
            Session["shoppingCart"] = shoppingCart;
            Response.Redirect("~/Cart.aspx");
        }

        protected void Quantity_Selected(object sender, EventArgs e)
        {
            DropDownList DropDown = sender as DropDownList;
            TextBox temp = stressTable.FindControl("Subtotal" + DropDown.ID.Split('*')[1]) as TextBox;
            Session["subTotals"] = subTotals[Convert.ToInt32(DropDown.ID.Split('*')[1])] = Convert.ToInt32(DropDown.SelectedItem.Text) * prices[(Convert.ToInt32(DropDown.ID.Split('*')[1]))];
            shoppingCart.products.Find(x => x.p_id == DropDown.ID.Split('*').First()).quantity = int.Parse(DropDown.SelectedItem.Text);
            GrandTotal.Text = "$" + subTotals.Sum().ToString();
            temp.Text = "Subtotal: $" + subTotals[(Convert.ToInt32(DropDown.ID.Split('*')[1]))].ToString();
            Session["shoppingCart"] = shoppingCart;
            sqlCommand = new SqlCommand("Update proorder set quantity = " + DropDown.SelectedItem.Text + ", subtotal = " + subTotals[Convert.ToInt32(DropDown.ID.Split('*')[1])].ToString() + " where c_name = '" + (Session["Account"] as ApplicationUser).UserName + "' and p_id='" + DropDown.ID.Split('*').First() + "' and o_id IS NULL", conn);
            try
            {
                conn.Open();
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
        void ShowEmpty()
        {
            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            tableRow.HorizontalAlign = HorizontalAlign.Justify;
            tableRow.BorderStyle = BorderStyle.Solid;
            tableRow.BorderWidth = Unit.Pixel(3);
            tableCell.Controls.Add(new LiteralControl("The cart is empty"));
            tableRow.Cells.Add(tableCell);
            stressTable.Rows.Add(tableRow);
            ProceedCheck.Parent.Parent.Visible = false;
        }
        Product DataCollect()
        {
            Product product = new Product
            {
                p_id = reader["p_id"].ToString(),
                p_url = reader["p_url"].ToString(),
                p_name = reader["p_name"].ToString(),
                u_price = float.Parse(reader["u_price"].ToString()),
                quantity = int.Parse(reader["quantity"].ToString()),
                p_image = (byte[])(reader["p_image"]),
            };
            prices.Add(float.Parse(reader["u_price"].ToString()));
            shoppingCart.products.Add(product);

            return product;
        }
        DropDownList DropDownItems(string pid, int chosen)
        {
            DropDownList dropDown = new DropDownList
            {
                CssClass = "btn btn-warning dropdown-toggle",
                AutoPostBack = true,
                ID = pid + "*" + (x++).ToString()
            };
            dropDown.SelectedIndexChanged += new EventHandler(Quantity_Selected);
            for (int i = 0; i < Quantities.Find(t => t.p_id == pid).quantity; i++)
            {
                dropDown.Items.Add(new ListItem((i + 1).ToString()));
            }
            dropDown.Items.FindByText(chosen.ToString()).Selected = true;
            return dropDown;
        }
        void QuickFunction(Product p)
        {
            TableCell tableCell = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Left
            };
            Image image = new Image
            {
                ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(p.p_image),
                CssClass = "img-fluid",
            };
            image.Style.Add("max-height", "40vh");
            image.Style.Add("max-width", "30vw");
            TableRow tableRow = new TableRow
            {
                BorderStyle = BorderStyle.Solid,
                CssClass = "border-bottom-0",
                BorderWidth = Unit.Pixel(3),
                HorizontalAlign = HorizontalAlign.Justify
            };
            image.Visible = true;
            DynamicHyperLink hyperLink = new DynamicHyperLink
            {
                NavigateUrl = p.p_url,
                Visible = true,
                CssClass = "nav-link active",
                ForeColor = System.Drawing.Color.WhiteSmoke
            };
            hyperLink.Controls.Add(image);
            tableCell.Controls.Add(hyperLink);
            tableRow.Visible = true;
            tableRow.Cells.Add(tableCell);
            stressTable.Rows.Add(tableRow);
            DynamicHyperLink hyperLink2 = new DynamicHyperLink();

            tableCell.Visible = true;
            hyperLink2.Visible = true;
            hyperLink2.NavigateUrl = p.p_url;
            hyperLink2.ForeColor = System.Drawing.Color.WhiteSmoke;
            hyperLink2.CssClass = "nav-link active";
            hyperLink2.Controls.Add(new LiteralControl("Name: " + p.p_name + "<br />"));
            hyperLink2.Controls.Add(new LiteralControl("Unit Price: $" + p.u_price + "<br />"));

            TextBox textBox1 = new TextBox
            {
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = System.Drawing.Color.WhiteSmoke,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                ID = "Subtotal" + x.ToString(),
            };

            subTotals.Add(float.Parse(p.quantity.ToString()) * p.u_price);
            textBox1.Text = "Subtotal: $" + (float.Parse(p.quantity.ToString()) * p.u_price).ToString();

            hyperLink2.Controls.Add(textBox1);
            TableCell tableCell2 = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Left,
                Visible = true
            };
            tableCell2.Controls.Add(hyperLink2);
            stressTable.Rows[stressTable.Rows.Count - 1].Cells.Add(tableCell2);

            Button button1 = new Button();

            Button button = new Button
            {
                Text = "Delete",
                CssClass = "btn btn-outline-warning",
                Visible = true,
                ID = p.p_id + "*Delete"
            };
            button.Click += new EventHandler(RemoveProduct_Click);
            TableCell tableCell1 = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Center,
                ColumnSpan = 2,
            };
            DropDownList dropDownList = new DropDownList();

            tableCell1.Controls.Add(DropDownItems(p.p_id, p.quantity));
            tableCell1.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;"));
            tableCell1.Controls.Add(button);
            tableCell1.Visible = true;
            TableRow tableRow1 = new TableRow
            {
                BorderStyle = BorderStyle.Solid,
                CssClass = "border-top-0",
                BorderWidth = Unit.Pixel(3),
                Visible = true
            };
            tableRow1.Cells.Add(tableCell1);
            stressTable.Rows.Add(tableRow1);
        }
    }
}