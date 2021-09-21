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
using System.Data.Entity.Validation;

namespace LabAssignment
{
    public partial class Registration : System.Web.UI.Page
    {
        UserManager<IdentityUser> Customers;
        Entity entity;
        //private readonly Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if ((Session["Account"] as User).Roles.Any(x => x.RoleId == "Admin"))
                Page.Master.FindControl("AdminFunc").Visible = true;
            entity = new Entity();
            entity.Database.Connection.ConnectionString = ConfigurationManager
                .ConnectionStrings["LIConnectionString"].ConnectionString;
            UserStore<IdentityUser> userStore1 = new UserStore<IdentityUser>(entity);
            Customers = new UserManager<IdentityUser>(userStore1);
            Customers.PasswordValidator = new PasswordValidator
            {
                RequireDigit = true,
                RequiredLength = 6,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonLetterOrDigit = false
            };
            PasswordNotValid.Text = "Password must contain atleast one digit, atleast 6 characters long,include lowercase and uppercase letters";
        }

        protected void RegisterClick_Click(object sender, EventArgs e)
        {
            var result = Customers.PasswordValidator.ValidateAsync(Password.Text).Result;
            if (!result.Succeeded)
                PasswordNotValid.Visible = true;
            else
            {
                if(PasswordNotValid.Visible)
                    PasswordNotValid.Visible = false;
                int count = entity.Users.Count();
                if (count > 0)
                {
                    if (!Customers.Users.Any(x => x.UserName == Name.Text))
                    {
                        CreateUser(Customers, count, entity);
                    }
                }
                else
                {
                    CreateUser(Customers, count, entity);
                }
            }
            
        }
    
        public void CreateUser(UserManager<IdentityUser> Customers,int count, Entity entity)
        {
            //  RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(entity);
            // RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
            IdentityUser customer = new IdentityUser
            {
                UserName = Name.Text,
                 Id = (count + 1).ToString(),
            };
          /*  IdentityRole identityRole = new IdentityRole
            {
                Id= "Cust",
                Name = "Customer"
            };*/
            IdentityUserRole userRole = new IdentityUserRole
            {
                UserId = customer.Id,
                RoleId = "Cust"
            };
            
            if (Password.Text == CPassword.Text)
            {
                try
                {
                    //var roleResult = roleManager.CreateAsync(identityRole).Result;
                    //if (roleResult.Succeeded)
                    //{
                    var result = Customers.CreateAsync(customer, Password.Text).Result;
                    if (result.Succeeded)
                    {
                        Customers.FindByNameAsync(customer.UserName).Result.Email = Password.Text;
                        Customers.FindByNameAsync(customer.UserName).Result.Roles.Add(userRole);
                        result=Customers.UpdateAsync(customer).Result;
                        if (result.Succeeded)
                        {
                            Session["Account"] = "Customer";
                            //var x = signInManager.PasswordSignInAsync(customer.UserName, customer.PasswordHash, isPersistent: true, false).Result;
                            Response.Redirect("~/Default.aspx", false);
                        }
                    }
                    //}
                }
                catch (Exception e)
                {


                }
            }
        }
        protected void CPassword_TextChanged(object sender, EventArgs e)
        {
        }

        protected void Password_TextChanged(object sender, EventArgs e)
        {
            PasswordValidator passwordValidator = new PasswordValidator();
            var result=Customers.PasswordValidator.ValidateAsync(Password.Text).Result;
            if (!result.Succeeded)
                PasswordNotValid.Visible = true;
            else
                if (PasswordNotValid.Visible)
                    PasswordNotValid.Visible = false;
        }
    }
}