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
        private readonly Microsoft.AspNetCore.Identity.SignInManager<IdentityUser> signInManager;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterClick_Click(object sender, EventArgs e)
        {
            UserManager<IdentityUser> Customers;
            Entity entity = new Entity();
            entity.Database.Connection.ConnectionString = ConfigurationManager
                .ConnectionStrings["LIConnectionString"].ConnectionString;
            UserStore<IdentityUser> userStore1 = new UserStore<IdentityUser>(entity);
            int count = entity.Users.Count();
            Customers = new UserManager<IdentityUser>(userStore1);
            
            if (count > 0)
            {
                if (!Customers.Users.Any(x => x.UserName == Name.Text))
                {
                    CreateUser(Customers, count, entity);
                }
            }
            else
            {
                CreateUser(Customers,count,entity);
            }
            
        }
    
        public void CreateUser(UserManager<IdentityUser> Customers,int count, Entity entity)
        {
            //  RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(entity);
            // RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);
            IdentityUser customer = new IdentityUser
            {
                UserName = Name.Text,
                 Id = (count + 1).ToString()
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
            
            try
            {
                //var roleResult = roleManager.CreateAsync(identityRole).Result;
                //if (roleResult.Succeeded)
                //{
                var result = Customers.CreateAsync(customer, Password.Text).Result;
                if (result.Succeeded)
                {
                    Customers.FindByNameAsync(customer.UserName).Result.Roles.Add(userRole);
                    Session["Account"] = "Customer";
                    var x = signInManager.PasswordSignInAsync(customer.UserName, customer.PasswordHash, isPersistent: true, false).Result;
                    if (x.Succeeded)
                        Response.Redirect("~/Default.aspx", false);
                }
                //}
            }
            catch (Exception e)
            {


            }

        }
        protected void CPassword_TextChanged(object sender, EventArgs e)
        {
        }
    }
}