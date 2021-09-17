using System;

using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class XboxS : System.Web.UI.Page
    {
        SqlCommand sqlCommand;
        SqlConnection conn;
        SqlDataReader reader;
        byte[] temp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "XboxS.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] == "Admin")
                Page.Master.FindControl("AdminFunc").Visible = true;
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("select * from product inner join proimage on product.p_id=proimage.p_id where product.p_id = 351", conn);
            conn.Open();
            try
            {
                reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    temp = (byte[])reader["p_image"];
                    Price2.InnerText = "$" + float.Parse(reader["u_price"].ToString()).ToString();
                    TableCell tableCell = new TableCell();
                    tableCell.HorizontalAlign = HorizontalAlign.Left;
                    tableCell.Controls.Add(new LiteralControl(reader["p_details"].ToString()));
                    TableRow tableRow = new TableRow();
                    tableCell.Font.Size = FontUnit.Medium;
                    tableRow.Cells.Add(tableCell);
                    Description.Rows.Add(tableRow);
                    CarouselImg1.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                    if (reader.Read())
                    {
                        temp = (byte[])reader["p_image"];
                        CarouselImg2.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);

                    }
                    if (reader.Read())
                    {
                        temp = (byte[])reader["p_image"];
                        CarouselImg3.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                    }
                }
            }
            catch (SqlException)
            {

            }
            conn.Close();
        }
        protected void SeriesXHomeLaunch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/XboxX.aspx");
        }
    }
}