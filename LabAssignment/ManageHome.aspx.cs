using LabAssignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class ManageHome : System.Web.UI.Page
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
                string url = ConfigurationManager.AppSettings["SecurePath"] + "ManageHome.aspx";
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
        }
        protected void CarouselAdd_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
    }
}