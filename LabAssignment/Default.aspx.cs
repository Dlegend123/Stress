using LabAssignment.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace LabAssignment
{
    public partial class _Default : Page
    {
        SqlCommand sqlCommand;
        SqlConnection conn;
        SqlDataReader reader;
        string filePath;
        byte[] temp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Default.aspx";
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
            sqlCommand = new SqlCommand("Select * from Carousel", conn);
            conn.Open();

            try
            {
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Update();
                    temp = (byte[])reader["p_image"];
                    if (reader["p_name"].ToString() == "CarouselImg1")
                    {
                        CarouselImg1.Src = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel1Link.HRef = filePath;
                    }
                    if (reader["p_name"].ToString() == "CarouselImg2")
                    {
                        CarouselImg2.Src = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel2Link.HRef= filePath;
                    }
                    if (reader["p_name"].ToString() == "CarouselImg3")
                    {
                        CarouselImg3.Src = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel3Link.HRef = filePath;
                    }
                    if (reader["p_name"].ToString() == "CarouselImg4")
                    {
                        CarouselImg4.Src = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel4Link.HRef = filePath;
                    }
                }
                conn.Close();
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }
        protected void Update()
        {
            filePath = reader["p_url"].ToString();
        }
        protected void Carousel1Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel1Link.HRef, false);
        }
        protected void Carousel2Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel2Link.HRef, false);
        }
        protected void Carousel3Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel3Link.HRef,false);
        }
        protected void Carousel4Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel4Link.HRef, false);
        }
    }
}