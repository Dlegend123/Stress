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
    public partial class PS5 : System.Web.UI.Page
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
                string url = ConfigurationManager.AppSettings["SecurePath"] + "PS5.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                    (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                    if (!Page.Master.FindControl("AdminFunc").Visible)
                        Page.Master.FindControl("AdminFunc").Visible = true;
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Cust"))
                {
                    HideCart.Visible = true;
                    if (!Page.Master.FindControl("CartLink").Visible)
                        Page.Master.FindControl("CartLink").Visible = true;
                }
            }
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id where product.p_id = 555", conn);
            conn.Open();
            try
            {
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    temp = (byte[])reader["p_image"];
                    product = new Product
                    {
                        p_id = reader["p_id"].ToString(),
                        p_url = reader["p_url"].ToString(),
                        p_name = reader["p_name"].ToString(),
                        u_price = float.Parse(reader["u_price"].ToString()),
                        quantity = int.Parse(reader["quantity"].ToString()),
                        p_urlM = reader["p_urlM"].ToString()
                    };
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
            }
            catch (Exception x)
            {
                Session["LastError"] = x;

            }
            conn.Close();
        }

        protected void PS5DigitalHomeLaunch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/PS5Digital.aspx");
        }
        protected void AddToCart_ServerClick(object sender, EventArgs e)
        {
            try
            {
                sqlCommand = new SqlCommand("select * from proorder where p_id = " + Convert.ToInt32(product.p_id), conn);
                conn.Open();
                reader = sqlCommand.ExecuteReader();
                reader.Read();
                if (!reader.HasRows)
                {
                    sqlCommand = new SqlCommand("Insert into proorder(p_id,p_name,c_name,u_price,quantity,p_url,p_urlM,subtotal) Values (@p_id,@p_name,@c_name,@u_price,@quantity,@p_url,@p_urlM,@subtotal)", conn);
                    sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(product.p_id));
                    sqlCommand.Parameters.AddWithValue("@p_name", product.p_name);
                    sqlCommand.Parameters.AddWithValue("@c_name", (Session["Account"] as IdentityUser).UserName);
                    sqlCommand.Parameters.AddWithValue("@u_price", product.u_price);
                    sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(Quantity.Text));
                    sqlCommand.Parameters.AddWithValue("@p_url", product.p_url);
                    sqlCommand.Parameters.AddWithValue("@p_urlM", product.p_urlM);
                    sqlCommand.Parameters.AddWithValue("@subtotal", SqlMoney.Parse((Convert.ToInt32(Quantity.Text) * product.u_price).ToString()));

                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    var quantity = Convert.ToInt32(reader["quantity"]);
                    if ((quantity + Convert.ToInt32(Quantity.Text)) > product.quantity)
                        quantity = product.quantity;
                    else
                        quantity += Convert.ToInt32(Quantity.Text);
                    sqlCommand = new SqlCommand("update proorder set quantity = @quantity, subtotal = @subtotal where p_id = @p_id", conn);
                    sqlCommand.Parameters.AddWithValue("@quantity", quantity);
                    sqlCommand.Parameters.AddWithValue("@subtotal", SqlMoney.Parse((quantity * product.u_price).ToString()));
                    sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(product.p_id));
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
            if (int.Parse(Quantity.Text) > 0)
            {
                try
                {
                    conn.Open();
                    Product temp = product;
                    temp.quantity = Convert.ToInt32(Quantity.Text);
                    ShoppingCart shoppingCart = new ShoppingCart();
                    shoppingCart.products.Add(temp);
                    Session["shoppingCart"] = shoppingCart;
                    sqlCommand = new SqlCommand("Insert into proorder(p_id,p_name,u_price,quantity,c_name,p_url,p_urlM,subtotal) Values (@p_id,@p_name,@u_price,@quantity,@c_name,@p_url,@p_urlM,@subtotal)", conn);
                    sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(product.p_id));
                    sqlCommand.Parameters.AddWithValue("@p_name", product.p_name);
                    sqlCommand.Parameters.AddWithValue("@u_price", product.u_price);
                    sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(Quantity.Text));
                    if ((Session["Account"] as IdentityUser).UserName != "Default")
                        sqlCommand.Parameters.AddWithValue("@c_name", (Session["Account"] as IdentityUser).UserName);
                    else
                        sqlCommand.Parameters.AddWithValue("@c_name", "Default");
                    sqlCommand.Parameters.AddWithValue("@p_url", product.p_url);
                    sqlCommand.Parameters.AddWithValue("@p_urlM", product.p_urlM);
                    sqlCommand.Parameters.AddWithValue("@subtotal", SqlMoney.Parse((Convert.ToInt32(Quantity.Text) * product.u_price).ToString()));
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
}