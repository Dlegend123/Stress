using Microsoft.AspNet.Identity.EntityFramework;
using System;

using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
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
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                    Page.Master.FindControl("AdminFunc").Visible = true;
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
                        quantity = reader["quantity"].ToString(),
                        p_urlM = reader["p_urlM"].ToString()
                    };
                    TableCell tableCell = new TableCell();
                    tableCell.HorizontalAlign = HorizontalAlign.Center;
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
            catch (SqlException)
            {

            }
            conn.Close();
        }

        protected void PS5DigitalHomeLaunch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/PS5Digital.aspx");
        }
        protected void AddToCart_ServerClick(object sender, EventArgs e)
        {
            sqlCommand = new SqlCommand("Insert into proorder(p_id,c_name,u_price,quantity,p_url,p_urlM) Values (@p_id,@c_name,@u_price,@quantity,@p_url,@p_urlM)", conn);
            sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(product.p_id));
            sqlCommand.Parameters.AddWithValue("@c_name", (Session["Account"] as IdentityUser).UserName);
            sqlCommand.Parameters.AddWithValue("@u_price", product.p_id);
            sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(Quantity.Text));
            sqlCommand.Parameters.AddWithValue("@p_url", product.p_url);
            sqlCommand.Parameters.AddWithValue("@p_urlM", product.p_urlM);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}