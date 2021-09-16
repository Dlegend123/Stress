using System;

using System.Configuration;
using System.Data.SqlClient;

using System.Threading;

using System.Web.UI;


namespace LabAssignment
{
    public partial class _Default : Page
    {
        Thread CheckNew;
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
            if (Session["Account"] == "Admin")
                Page.Master.FindControl("DynamicHyperLink1").Visible = true;
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
                        CarouselImg1.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel1Link.HRef = filePath;
                    }
                    if (reader["p_name"].ToString() == "CarouselImg2")
                    {
                        CarouselImg2.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel2Link.HRef= filePath;
                    }
                    if (reader["p_name"].ToString() == "CarouselImg3")
                    {
                        CarouselImg3.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel3Link.HRef = filePath;
                    }
                    if (reader["p_name"].ToString() == "CarouselImg4")
                    {
                        CarouselImg4.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                        Carousel4Link.HRef = filePath;
                    }
                }
            }
            catch (SqlException)
            {

            }
            conn.Close();
            CheckNew = new Thread(new ThreadStart(ThreadTask));
        }
       /* protected override void Render(HtmlTextWriter writer)
        {
            // Register controls for event validation
            foreach (Control c in this.Controls)
            {
                this.Page.ClientScript.RegisterForEventValidation(
                        c.UniqueID.ToString()
                );
            }
            base.Render(writer);
        }*/
       /*
        protected void PS5HomeLaunch_ServerClick(object sender, EventArgs e)
        {

            Response.Redirect("~/PS5.aspx");
        }

        protected void XboxHomeLaunch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/XboxX.aspx");
        }

        protected void SwitchOledHomeLaunch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Switch Oled model.aspx");
        }

        protected void SteamDeckHomeLaunch_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/SteamDeck.aspx");
        }
       */
        protected void ThreadTask()
        {
            while (true)
            {
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
                        if (reader["p_name"].ToString() == "CarouselImg1")
                        {
                            temp = (byte[])reader["p_image"];
                            if (("data:image/jpeg;base64," + Convert.ToBase64String(temp)) != CarouselImg1.ImageUrl)
                            {
                                CarouselImg1.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                                Update();
                                Carousel1Link.HRef = filePath;
                            }
                        }
                        if (reader["p_name"].ToString() == "CarouselImg2")
                        {
                            temp = (byte[])reader["p_image"];
                            if (("data:image/jpeg;base64," + Convert.ToBase64String(temp)) != CarouselImg2.ImageUrl)
                            {
                                CarouselImg2.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                                Update();
                                Carousel2Link.HRef = filePath;
                            }
                        }

                        if (reader["p_name"].ToString() == "CarouselImg3")
                        {
                            temp = (byte[])reader["p_image"];
                            if (("data:image/jpeg;base64," + Convert.ToBase64String(temp)) != CarouselImg3.ImageUrl)
                            {
                                CarouselImg3.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                                Update();
                                Carousel3Link.HRef = filePath;
                            }
                        }

                        if (reader["p_name"].ToString() == "CarouselImg4")
                        {
                            temp = (byte[])reader["p_image"];
                            if (("data:image/jpeg;base64," + Convert.ToBase64String(temp)) != CarouselImg4.ImageUrl)
                            {
                                CarouselImg4.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(temp);
                                Update();
                                Carousel4Link.HRef = filePath;
                            }
                        }
                    }
                }
                catch (SqlException)
                {

                }
                conn.Close();
            }
        }
        protected void Update()
        {
            filePath = reader["p_url"].ToString();
        }
        protected void Carousel1Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel1Link.HRef);
        }
        protected void Carousel2Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel2Link.HRef);
        }
        protected void Carousel3Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel3Link.HRef);
        }
        protected void Carousel4Link_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(Carousel4Link.HRef);
        }
    }
}