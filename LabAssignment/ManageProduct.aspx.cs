using Microsoft.AspNet.Identity.EntityFramework;
using System;

using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace LabAssignment
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        private SqlCommand sqlCommand;
        private SqlConnection conn;
        private SqlDataReader reader;
        private string filePath;
        private BinaryReader b;
        private byte[] binData;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "ManageProduct.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                    Page.Master.FindControl("AdminFunc").Visible = true;
            }
        }

        protected void ProductAdd_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            if (ProductVid.HasFile)
            {
                sqlCommand = new SqlCommand("INSERT INTO Product(p_id,p_name,p_details,category,u_price,quantity,p_video,p_url,p_urlM) Values (@p_id,@p_name,@p_details,@category,@u_price,@quantity,@p_video,@p_url,@p_urlM)", conn);
                b = new BinaryReader(ProductVid.PostedFile.InputStream);
                binData = b.ReadBytes(ProductVid.PostedFile.ContentLength);
                sqlCommand.Parameters.AddWithValue("@p_video", binData);
            }
            else
            {
                sqlCommand = new SqlCommand("INSERT INTO Product(p_id,p_name,p_details,category,u_price,quantity,p_url,p_urlM) Values (@p_id,@p_name,@p_details,@category,@u_price,@quantity,@p_url,@p_urlM)", conn);
            }
            sqlCommand.Parameters.AddWithValue("@p_id",Convert.ToInt32( ProductID.Text));
            sqlCommand.Parameters.AddWithValue("@p_name", ProductName.Text);
            sqlCommand.Parameters.AddWithValue("@p_details", ProductDetail.Text);
            sqlCommand.Parameters.AddWithValue("@category", ProductCat.Text);
            sqlCommand.Parameters.AddWithValue("@u_price", ProductPrice.Text);
            
            sqlCommand.Parameters.AddWithValue("@quantity", Convert.ToInt32(ProductQuantity.Text));
            sqlCommand.Parameters.AddWithValue("@p_url", "~/" + ProductDesk.Text);
            sqlCommand.Parameters.AddWithValue("@p_urlM", "~/" + ProductMob.Text);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlCommand = new SqlCommand("INSERT INTO ProImage(p_id,p_image) Values (@p_id,@p_image)", conn);
            sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
            b = new BinaryReader(ProductImage1.PostedFile.InputStream);
            binData = b.ReadBytes(ProductImage1.PostedFile.ContentLength);
            //sqlCommand.Parameters.AddWithValue("@imageType", ProductImage1.PostedFile.ContentType);
            sqlCommand.Parameters.AddWithValue("@p_image", binData);
            sqlCommand.ExecuteNonQuery();
            sqlCommand.Parameters.Clear();
            b = new BinaryReader(ProductImage2.PostedFile.InputStream);
            binData = b.ReadBytes(ProductImage2.PostedFile.ContentLength);
            sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
           // sqlCommand.Parameters.AddWithValue("@imageType", ProductImage2.PostedFile.ContentType);
            sqlCommand.Parameters.AddWithValue("@p_image", binData);
            sqlCommand.ExecuteNonQuery();
            
            if (ProductImage3.HasFile)
            {
                sqlCommand.Parameters.Clear();
                b = new BinaryReader(ProductImage3.PostedFile.InputStream);
                binData = b.ReadBytes(ProductImage3.PostedFile.ContentLength);
                sqlCommand.Parameters.AddWithValue("@p_id", Convert.ToInt32(ProductID.Text));
                // sqlCommand.Parameters.AddWithValue("@imageType", ProductImage3.PostedFile.ContentType);
                sqlCommand.Parameters.AddWithValue("@p_image", binData);
                sqlCommand.ExecuteNonQuery();
            }
            conn.Close();
        }

        protected void CarouselAdd_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("INSERT INTO Carousel(p_name,p_image,p_url,p_urlM) Values (@p_name,@p_image,@p_url,@p_urlM)", conn);
            sqlCommand.Parameters.AddWithValue("@p_name", CarouselName.SelectedItem.Text);
            b = new BinaryReader(CarouselImage.PostedFile.InputStream);
            binData = b.ReadBytes(CarouselImage.PostedFile.ContentLength);
            sqlCommand.Parameters.AddWithValue("@p_image", binData);
            sqlCommand.Parameters.AddWithValue("@p_url", "~/" + CarouselDesk.Text);
            sqlCommand.Parameters.AddWithValue("@p_urlM", "~/" + CarouselMob.Text);
            conn.Open();
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }
    }
}