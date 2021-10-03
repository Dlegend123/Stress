using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class ContactM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "ContactM.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                    (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                    if (!Page.Master.FindControl("AdminFunc").Visible)
                        Page.Master.FindControl("AdminFunc").Visible = true;
                if (!Page.Master.FindControl("CartLink").Visible)
                    Page.Master.FindControl("CartLink").Visible = true;
            }
        }
    }
}