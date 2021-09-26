using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;

namespace LabAssignment
{
    public partial class SignIn : System.Web.UI.Page
    {
        UserStore<IdentityUser> userStore;
        Entity entity;

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
            entity = new Entity();
            entity.Database.Connection.ConnectionString = ConfigurationManager
                .ConnectionStrings["LIConnectionString"].ConnectionString;

            userStore = new UserStore<IdentityUser>(entity);
            Customers = new UserManager<IdentityUser>(userStore);

            if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                Page.Master.FindControl("AdminFunc").Visible = true;
            if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Cust"))
                Page.Master.FindControl("CartLink").Visible = true;
        }
        protected void Validate(object sender, EventArgs e)
        {
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