using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Web.UI;


namespace LabAssignment
{
    public partial class SiteMaster : MasterPage
    {
       // private readonly SignInManager<User> signInManager;
        public User Account;
        public User _Account
        {
            set { Account.Id = "Visitor"; }
            get { return Account; }
        }
        public SiteMaster()
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}