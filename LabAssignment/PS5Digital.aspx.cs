using System;

using System.Configuration;
using System.Data.SqlClient;


namespace LabAssignment
{
    public partial class PS5Digital : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "PS5Digital.aspx";
                Response.Redirect(url);
            }
            SqlCommand sqlCommand;
            SqlConnection conn;
            SqlDataReader reader;
            byte[] temp;
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id where product.p_id = 556", conn);
            conn.Open();
            try
            {
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    temp = (byte[])reader["p_image"];
                    Price2.InnerText = "$" + float.Parse(reader["u_price"].ToString()).ToString();
                    CarouselImg1.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                    if (reader.Read())
                    {
                        temp = (byte[])reader["p_image"];
                        CarouselImg2.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                    }
                }
            }
            catch (SqlException)
            {

            }
            conn.Close();
        }
        protected void PS5HomeLaunch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/PS5.aspx");
        }
    }
}