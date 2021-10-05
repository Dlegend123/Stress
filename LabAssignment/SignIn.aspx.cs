using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web;

namespace LabAssignment
{
    public partial class SignIn : System.Web.UI.Page
    {
        
        Entity entity;
        UserStore<IdentityUser> userStore;
        protected UserManager<IdentityUser> Customers;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "SignIn.aspx";
                Response.Redirect(url);
            }

            int width = (Request.Browser.ScreenPixelsWidth) * 2 - 100;
            int height = (Request.Browser.ScreenPixelsHeight) * 2 - 100;

            if (width <= 700)
            {
                if (!SignInTable.CssClass.Contains("container-fluid"))
                    SignInTable.CssClass += "container-fluid";
            }
            if (Session["Account"] != null)
            {
                if ((Session["Account"] as IdentityUser).UserName != "Default")
                {
                    if ((Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText != "Sign Out")
                        (Page.Master.FindControl("SignInLink") as HtmlAnchor).InnerText = "Sign Out";
                    Session["Account"] = null;
                    if (Page.Master.FindControl("CartLink").Visible)
                        Page.Master.FindControl("CartLink").Visible = false;
                    if (Page.Master.FindControl("AdminFunc").Visible)
                        Page.Master.FindControl("AdminFunc").Visible = false;
                    Response.Redirect("~/Default.aspx", false);
                  //  HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                  //  HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                  // HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
                    SignInTable.Visible = false;
                }
            }
        }
        protected void Validate(object sender, EventArgs e)
        {
            entity = new Entity();
            entity.Database.Connection.ConnectionString = ConfigurationManager
                .ConnectionStrings["LIConnectionString"].ConnectionString;
            userStore = new UserStore<IdentityUser>(entity);
            Customers = new UserManager<IdentityUser>(userStore);
            try
            {
                IdentityUser customer = Customers.FindByName(SName.Text);
                if (customer != null)
                {
                    if (customer.Email == SPassword.Text)
                    {
                        if (PasswordNotValid.Visible)
                            PasswordNotValid.Visible = false;
                        Session["Account"] = customer;
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    PasswordNotValid.Text = "Invalid Username/Password";
                    PasswordNotValid.Visible = true;
                }
            }
            catch (Exception x)
            {
                Session["LastError"] = x;
            }
        }

        protected void SPassword_TextChanged(object sender, EventArgs e)
        {
            if (PasswordNotValid.Visible)
                PasswordNotValid.Visible = false;
        }
    }
}