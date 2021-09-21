using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
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
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                    Page.Master.FindControl("AdminFunc").Visible = true;
            }
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("select * from proorder inner join proimage on proorder.p_id=proimage.p_id where proorder.c_name='" + (Session["Account"] as IdentityUser).UserName+"'", conn);
            conn.Open();
            shoppingCart = new ShoppingCart();
            subTotals= new List<float>();
            x = 0;
            try
            {
                reader = sqlCommand.ExecuteReader();
                int count = 0;
                string id = "";
                while (reader.Read())
                {
                    if (id != reader["p_id"].ToString())
                    {
                        id = reader["p_id"].ToString();
                        QuickFunction(DataCollect(), count++);
                    }
                }
                GrandTotal.Text = "$" + subTotals.Sum().ToString();
                prices = new List<float>();
                if (subTotals.Count == 0)
                {
                    TableRow tableRow = new TableRow();
                    TableCell tableCell = new TableCell();
                    tableRow.HorizontalAlign = HorizontalAlign.Justify;
                    tableRow.BorderStyle = BorderStyle.Solid;
                    tableRow.BorderWidth = Unit.Pixel(3);
                    tableCell.Controls.Add(new LiteralControl("The cart is empty"));
                    tableRow.Cells.Add(tableCell);
                    stressTable.Rows.Add(tableRow);
                }
            }
            catch (SqlException)
            {

            }
            conn.Close();
        }

        protected void ProceedCheck_Click(object sender, EventArgs e)
        {

        }
        protected void RemoveProduct_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("Delete  from proorder where c_name= '"+(Session["Account"] as User).UserName+"' and product.p_id='"+ button.ID.Split('*').First() + "'", conn);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
        protected void Quantity_Changed(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            TextBox temp = stressTable.FindControl("Subtotal" + textBox.ID) as TextBox;
            subTotals[(Convert.ToInt32(textBox.ID))]=Convert.ToInt32(textBox.Text)*prices[(Convert.ToInt32(textBox.ID))] ;
            temp.Text = "Subtotal: " + subTotals[(Convert.ToInt32(textBox.ID))].ToString();
            GrandTotal.Text = "$"+subTotals.Sum().ToString();   
        }
        Product DataCollect()
        {
            Product product = new Product
            {
                p_id = reader["p_id"].ToString(),
                p_url = reader["p_url"].ToString(),
                p_details = reader["p_details"].ToString(),
                p_name = reader["p_name"].ToString(),
                u_price = float.Parse(reader["u_price"].ToString()),
                quantity = reader["quantity"].ToString(),
                p_image = (byte[])(reader["p_image"]),
                category = reader["category"].ToString()
            };
            prices.Add(float.Parse(reader["u_price"].ToString()));
            shoppingCart.products.Add(product);

            return product;
        }
        void QuickFunction(Product p, int count)
        {
            int width = (Request.Browser.ScreenPixelsWidth) * 2 - 100;
            int height = (Request.Browser.ScreenPixelsHeight) * 2 - 100;
            /*    else
                {
                    if (ProductTable.CssClass.Contains("container"))
                        ProductTable.CssClass.Remove(ProductTable.CssClass.IndexOf("container"), "container".Length - 1);
                }*/

            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            Image image = new Image
            {
                ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(p.p_image),
                CssClass = "img-fluid",
                Height = Unit.Percentage(70)
            };
            tableRow.HorizontalAlign = HorizontalAlign.Justify;
            tableRow.BorderStyle = BorderStyle.Solid;
            tableRow.BorderWidth = Unit.Pixel(3);
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
            TableCell tableCell2 = new TableCell();
            tableCell.Visible = true;
            hyperLink2.Visible = true;
            hyperLink2.NavigateUrl = p.p_url;
            hyperLink2.ForeColor = System.Drawing.Color.WhiteSmoke;
            hyperLink2.CssClass = "nav-link active";
            hyperLink2.Controls.Add(new LiteralControl("Name: " + p.p_name + "<br />"));
            hyperLink2.Controls.Add(new LiteralControl("Unit Price: " + p.u_price + "<br />"));

            TextBox textBox1 = new TextBox
            {
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = System.Drawing.Color.WhiteSmoke,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                ID = "Subtotal" + x.ToString()
            };

            subTotals.Add(float.Parse(p.quantity) * p.u_price);
            textBox1.Text = "Subtotal: "+(float.Parse(p.quantity) * p.u_price).ToString();
            
            hyperLink2.Controls.Add(textBox1 );
            tableCell2.Controls.Add(hyperLink2);
            tableCell2.RowSpan = 1;
            tableCell2.HorizontalAlign = HorizontalAlign.Left;
            tableCell2.Visible = true;
            stressTable.Rows[count].Cells.Add(tableCell2);
            
            DropDownList dropDown= new DropDownList();
            List < ListItem > temp= new List<ListItem>();
            
            Button button = new Button();
            Button button1 = new Button();

            TextBox textBox = new TextBox
            {
                Text = p.quantity,
                ID = (x++).ToString(),
                TextMode = TextBoxMode.Number
            };
            textBox.TextChanged += new EventHandler(Quantity_Changed);
            Button button2= new Button();
            button.Text = "Delete";
            button.CssClass = "btn btn-outline-warning";
            button.Visible = true;
            button.ID = p.p_id + "*Delete";
            TableCell tableCell1 = new TableCell
            {
                HorizontalAlign = HorizontalAlign.Center
            };
            tableCell1.Controls.Add(dropDown);
            tableCell1.Controls.Add(new LiteralControl("&nbsp;"));
            tableCell1.Controls.Add(button);
            tableCell1.Visible= true;
            TableRow tableRow1= new TableRow();
            tableRow1.Cells.Add(tableCell1);
            tableRow1.Visible= true;
            stressTable.Rows.Add(tableRow1);
        }
    }
}