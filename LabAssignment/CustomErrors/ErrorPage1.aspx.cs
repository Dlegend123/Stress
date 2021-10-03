using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace LabAssignment.CustomErrors
{
    public partial class ErrorPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            Exception err = Session["LastError"] as Exception;
            //Exception err = Server.GetLastError();
            if ( err!= null)
            {
                err = err.GetBaseException();
                ErrorMessage.InnerText = err.Message;
                ErrorSource.InnerText = err.Source;
                InnerEx.InnerText = (err.InnerException != null) ? err.InnerException.ToString() : "";
                StackTrace.InnerText = err.StackTrace;
                Session["LastError"] = null;
            }
        }
    }
}