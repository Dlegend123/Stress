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
    public partial class Checkout : System.Web.UI.Page
    {
        ShoppingCart shoppingCart;
        IdentityUser user;
        List<float> subTotals;
        SqlCommand sqlCommand;
        SqlConnection conn;
        SqlDataReader reader;
        int temp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Checkout.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                user = Session["Account"] as IdentityUser;
                (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                    Page.Master.FindControl("AdminFunc").Visible = true;
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Cust"))
                    Page.Master.FindControl("CartLink").Visible = true;
            }

            if (Session["shoppingCart"] != null)
            {
                shoppingCart = Session["shoppingCart"] as ShoppingCart;
                subTotals = Session["subTotals"] as List<float>;
                int count = 0;
                shoppingCart.products.ForEach(x => QuickFunction(x, count++));
            }
            else
            {
                Exception x = new Exception
                {
                    Source = "Invalid Navigation"
                };
                x.Source = "Invalid Navigation";
                Session["LastError"] = x;
                Response.Redirect("~/CustomErrors/ErrorPage1.aspx", false);
            }
        }
        void QuickFunction(Product p, int count)
        {
            TableRow tableRow = new TableRow();
            TableCell tableCell = new TableCell();
            Image image = new Image
            {
                ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(p.p_image),
                CssClass = "img-fluid",
            };
            image.Style.Add("max-height", "40vh");
            image.Style.Add("max-width", "30vw");
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
            hyperLink2.Controls.Add(new LiteralControl("<br /> ID: " + p.p_id + "<br />"));
            hyperLink2.Controls.Add(new LiteralControl("Quantity: " + p.quantity + "<br />"));
            hyperLink2.Controls.Add(new LiteralControl("Subtotal: $" + subTotals[count]));
            tableCell2.Controls.Add(hyperLink2);
            tableCell2.RowSpan = 1;
            tableCell2.HorizontalAlign = HorizontalAlign.Left;
            tableCell2.Visible = true;
            stressTable.Rows[stressTable.Rows.Count - 1].Cells.Add(tableCell2);
        }
        protected void ProcessOrder_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            int x;
            try
            {
                sqlCommand = new SqlCommand("select top 1 o_id from CustOrder order by o_id desc", conn);
                conn.Open();
                reader = sqlCommand.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                    x = int.Parse(reader["o_id"].ToString())+1;
                else
                    x = 0;
                var t = DateTime.Now;
                sqlCommand = new SqlCommand("Insert into CustOrder(c_name,p_date,o_id) Values(@c_name,@p_date,@o_id)", conn);
                sqlCommand.Parameters.AddWithValue("@c_name", user.UserName);
                sqlCommand.Parameters.AddWithValue("@p_date", t);
                sqlCommand.Parameters.AddWithValue("@o_id", x);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Parameters.Clear();
                shoppingCart.products.ForEach(v => ProcessO(v, t, x));
                shoppingCart.products.Clear();
                Session["shoppingCart"] = shoppingCart;
                sqlCommand = new SqlCommand("Insert into CustOrder(c_name,p_date,o_id) Values(@c_name,@p_date,@o_id)", conn);

                conn.Close();
            }
            catch (Exception f)
            {
                Session["LastError"] = f;
            }
        }
        void ProcessO(Product v,DateTime t,int x)
        {
            sqlCommand = new SqlCommand("Update proorder set o_id=@o_id,p_date=@p_date where p_id=@p_id and c_name=@c_name", conn);
            sqlCommand.Parameters.AddWithValue("@c_name", user.UserName);
            sqlCommand.Parameters.AddWithValue("@p_date", t);
            sqlCommand.Parameters.AddWithValue("@o_id", x);
            sqlCommand.Parameters.AddWithValue("@p_id", v.p_id);
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
        }
    }
    
}