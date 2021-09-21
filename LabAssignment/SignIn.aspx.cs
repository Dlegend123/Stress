using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fluent.Infrastructure.FluentStartup;
using Microsoft.AspNet.Identity.Owin;

namespace LabAssignment
{
    public partial class SignIn : System.Web.UI.Page
    {
        SqlCommand sqlCommand;
        SqlConnection conn;
        //SqlDataReader reader;
        string filePath;
        UserStore<IdentityUser> userStore;
        Entity entity;
        //byte[] temp;
        protected UserManager<IdentityUser> Customers;
        protected UserManager<IdentityUser> WebAdmin;
        

        public SignIn()
        {

        }
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
            WebAdmin = new UserManager<IdentityUser>(userStore);
            Customers = new UserManager<IdentityUser>(userStore);

            if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                Page.Master.FindControl("AdminFunc").Visible = true;

        }
        protected void Validate(object sender, EventArgs e)
        {
            UserManager<IdentityUser> Customers;
            Entity entity = new Entity();
            entity.Database.Connection.ConnectionString = ConfigurationManager
                .ConnectionStrings["LIConnectionString"].ConnectionString;
            UserStore<IdentityUser> userStore1 = new UserStore<IdentityUser>(entity);
            Customers = new UserManager<IdentityUser>(userStore1);
            IdentityUser customer = Customers.FindByName(SName.Text);


            if (customer.Email == SPassword.Text)
            {
                if (PasswordNotValid.Visible)
                    PasswordNotValid.Visible = false;
                Session["Account"] = customer;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                PasswordNotValid.Text = "Invalid Username/Password";
                PasswordNotValid.Visible = true;
            }
            /*  IdentityUser customer = new IdentityUser
              {
                  UserName = SName.Text,
                  PasswordHash = SPassword.Text
              };

              // var result = Customers.Users.Any(x => x.UserName == SName.Text && x.PasswordHash. == y.Succeeded);
              //SName.Text = result.ToString();
              if (y.Succeeded)
              {
                  Session["Account"] = "Customer";
                  Response.Redirect("~/Default.aspx");
              }
              else
              {
                  IdentityUser admin = new IdentityUser
                  {
                      UserName = SName.Text,
                      PasswordHash = SPassword.Text,

                  };
                //  result = WebAdmin.Users.Any(x => x.UserName == SName.Text && x.PasswordHash == temp);
                  if (y.Succeeded)
                  {
                      Session["Account"] = "Admin";
                      Response.Redirect("~/ManageProduct.aspx", false);
                  }

              }

              // }
              //   catch(Exception ex)
              // {

              //    }
            */
            /*
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("Select * from WebAdmin", conn);
            conn.Open();

            try
            {
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (SName.Text == reader["a_name"].ToString())
                    {
                        if (SPassword.Text == reader["a_pass"].ToString())
                        {
                            conn.Close();
                            Session["Account"] = "Admin";
                            Response.Redirect("~/ManageProduct.aspx", false);
                            break;
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }
            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
            */
        }

        protected void SPassword_TextChanged(object sender, EventArgs e)
        {
            if (PasswordNotValid.Visible)
                PasswordNotValid.Visible = false;
        }
    }
}