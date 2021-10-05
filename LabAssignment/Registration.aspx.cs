﻿using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using LabAssignment.Models;

namespace LabAssignment
{
    public partial class Registration : System.Web.UI.Page
    {
        UserManager<IdentityUser> Customers;
        Entity entity;
        //private readonly Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Account"] != null)
            {
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Admin"))
                    Page.Master.FindControl("AdminFunc").Visible = true;
                if ((Session["Account"] as ApplicationUser).Roles.Any(x => x.RoleId == "Cust"))
                    Page.Master.FindControl("CartLink").Visible = true;
            }
            entity = new Entity();
            entity.Database.Connection.ConnectionString = ConfigurationManager
                .ConnectionStrings["LIConnectionString"].ConnectionString;
            UserStore<IdentityUser> userStore1 = new UserStore<IdentityUser>(entity);
            Customers = new UserManager<IdentityUser>(userStore1)
            {
                PasswordValidator = new PasswordValidator
                {
                    RequireDigit = true,
                    RequiredLength = 6,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonLetterOrDigit = false
                }
            };
            PasswordNotValid.Text = "Password must contain atleast one digit, atleast 6 characters long,include lowercase and uppercase letters";
        }

        protected void RegisterClick_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Name.Text, Email = Password.Text };

            user.Id = (manager.Users.Count() + 1).ToString();
            
            IdentityUserRole userRole = new IdentityUserRole
            {
                UserId = user.Id,
                RoleId = "Cust"
            };
            user.Roles.Add(userRole);
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                Session["Account"] = user;
                try
                {
                    SqlConnection conn = new SqlConnection
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
                    };

                    SqlCommand sqlCommand = new SqlCommand("Insert into Cart(c_owner) Values(@c_owner)", conn);
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("@c_owner", user.UserName);
                    sqlCommand.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception x)
                {
                    Session["LastError"] = x;
                }
                signInManager.SignIn(user, isPersistent: true, rememberBrowser: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["~/Default.aspx"], Response);
            }
            else
            {
                PasswordNotValid.Visible = true;
            }
        }
        protected void CPassword_TextChanged(object sender, EventArgs e)
        {
        }

        protected void Password_TextChanged(object sender, EventArgs e)
        {
            var result = Customers.PasswordValidator.ValidateAsync(Password.Text).Result;
            if (!result.Succeeded)
                PasswordNotValid.Visible = true;
            else
                if (PasswordNotValid.Visible)
                PasswordNotValid.Visible = false;
        }
    }
}