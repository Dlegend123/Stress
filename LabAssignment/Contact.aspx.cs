using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int width = (Request.Browser.ScreenPixelsWidth)*2-100;
            int height = (Request.Browser.ScreenPixelsHeight)*2-100;
            if (width <= 700)
            {
                Response.Redirect("~/ContactM.aspx");
            }
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "Contact.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] == "Admin")
                Page.Master.FindControl("AdminFunc").Visible = true;

        }
    }
}