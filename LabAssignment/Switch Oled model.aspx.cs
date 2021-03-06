using LabAssignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class Switch_Oled_model : System.Web.UI.Page
    { 
        SqlCommand sqlCommand;
        SqlConnection conn;
        SqlDataReader reader;
        byte[] temp;
        Product product;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Swich Oled model.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Admin"))
                {
                    if (!Page.Master.FindControl("AdminFunc").Visible)
                        Page.Master.FindControl("AdminFunc").Visible = true;
                    if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                        (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                }
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Cust"))
                {
                    HideCart.Visible = true;
                    if (!Page.Master.FindControl("CustFunc").Visible)
                        Page.Master.FindControl("CustFunc").Visible = true;
                    if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                        (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                    (Page.Master.FindControl("CustList") as HtmlAnchor).InnerText = (Session["Account"] as ApplicationUser).UserName;
                }
            }
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id where product.p_id = 550", conn);
            conn.Open();
            try
            {
                reader = sqlCommand.ExecuteReader();
                reader.Read();
                temp = (byte[])reader["p_image"];
                product = new Product
                {
                    p_id = reader["p_id"].ToString(),
                    p_url = reader["p_url"].ToString(),
                    p_name = reader["p_name"].ToString(),
                    u_price = float.Parse(reader["u_price"].ToString()),
                    quantity = int.Parse(reader["quantity"].ToString()),
                    p_urlM = reader["p_urlM"].ToString(),
                    p_image = temp
                };
                for (int i = 1; i <= product.quantity; i++)
                {
                    QuantityList.Items.Add(new ListItem(i.ToString()));
                }
                
                TableCell tableCell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Center
                };
                tableCell.Controls.Add(new LiteralControl(reader["p_details"].ToString()));
                TableRow tableRow = new TableRow();
                tableCell.Font.Size = FontUnit.Medium;
                tableRow.Cells.Add(tableCell);
                Description.Rows.Add(tableRow);
                Price2.InnerText = "$" + float.Parse(reader["u_price"].ToString()).ToString();
                CarouselImg1.Src = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                if (reader.Read())
                {
                    temp = (byte[])reader["p_image"];
                    CarouselImg2.Src = "data:image/jpeg;base64," + Convert.ToBase64String(temp);

                }
                if (reader.Read())
                {
                    temp = (byte[])reader["p_image"];
                    CarouselImg3.Src = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                }
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
            conn.Close();
        }
        protected void AddToCart_ServerClick(object sender, EventArgs e)
        {
            try
            {
                sqlCommand = new SqlCommand("select * from proorder where p_id = " + Convert.ToInt32(product.p_id) + "and c_name= '" + (Session["Account"] as ApplicationUser).UserName + "'" + " and o_id IS NULL", conn);
                conn.Open();
                reader = sqlCommand.ExecuteReader();
                reader.Read();
                if (!reader.HasRows)
                {
                    sqlCommand = new SqlCommand("Insert into proorder(p_id,p_name,c_name,u_price,quantity,p_url,p_urlM,subtotal) Values (@p_id,@p_name,@c_name,@u_price,@quantity,@p_url,@p_urlM,@subtotal)", conn);
                    sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(product.p_id));
                    sqlCommand.Parameters.AddWithValue("@p_name", product.p_name);
                    sqlCommand.Parameters.AddWithValue("@c_name", (Session["Account"] as ApplicationUser).UserName);
                    sqlCommand.Parameters.AddWithValue("@u_price", product.u_price);
                    sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(QuantityList.SelectedItem.Text));
                    sqlCommand.Parameters.AddWithValue("@p_url", product.p_url);
                    sqlCommand.Parameters.AddWithValue("@p_urlM", product.p_urlM);
                    sqlCommand.Parameters.AddWithValue("@subtotal", SqlMoney.Parse((Convert.ToInt32(QuantityList.SelectedItem.Text) * product.u_price).ToString()));

                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    var quantity = Convert.ToInt32(reader["quantity"]);
                    if ((quantity + Convert.ToInt32(QuantityList.SelectedItem.Text)) > product.quantity)
                        quantity = product.quantity;
                    else
                        quantity += Convert.ToInt32(QuantityList.SelectedItem.Text);
                    sqlCommand = new SqlCommand("update proorder set quantity = @quantity, subtotal = @subtotal where p_id = @p_id and c_name = @c_name and o_id IS NULL", conn);
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity);
                    sqlCommand.Parameters.AddWithValue("@subtotal", SqlMoney.Parse((quantity * product.u_price).ToString()));
                    sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(product.p_id));
                    sqlCommand.Parameters.AddWithValue("@c_name", (Session["Account"] as ApplicationUser).UserName);
                    sqlCommand.ExecuteNonQuery();
                }
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
        protected void BuyNow_ServerClick(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                Product temp = product;
                temp.quantity = Convert.ToInt32(QuantityList.SelectedItem.Text);
                ShoppingCart shoppingCart = new ShoppingCart();
                shoppingCart.products.Add(temp);
                Session["shoppingCart"] = shoppingCart;
                sqlCommand = new SqlCommand("Insert into proorder(p_id,p_name,u_price,quantity,c_name,p_url,p_urlM,subtotal) Values (@p_id,@p_name,@u_price,@quantity,@c_name,@p_url,@p_urlM,@subtotal)", conn);
                sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(product.p_id));
                sqlCommand.Parameters.AddWithValue("@p_name", product.p_name);
                sqlCommand.Parameters.AddWithValue("@u_price", product.u_price);
                sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(QuantityList.SelectedItem.Text));
                if ((Session["Account"] as ApplicationUser).UserName != "Default")
                    sqlCommand.Parameters.AddWithValue("@c_name", (Session["Account"] as ApplicationUser).UserName);
                else
                    sqlCommand.Parameters.AddWithValue("@c_name", "Default");
                sqlCommand.Parameters.AddWithValue("@p_url", product.p_url);
                sqlCommand.Parameters.AddWithValue("@p_urlM", product.p_urlM);
                sqlCommand.Parameters.AddWithValue("@subtotal", SqlMoney.Parse((Convert.ToInt32(QuantityList.SelectedItem.Text) * product.u_price).ToString()));
                sqlCommand.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("~/Checkout.aspx", false);
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
    }
}