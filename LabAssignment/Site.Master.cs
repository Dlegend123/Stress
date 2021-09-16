using System;
using System.Web.UI;


namespace LabAssignment
{
    public partial class SiteMaster : MasterPage
    {
        public string Account;
        public string _Account
        {
            set { Account = "Visitor"; }
            get { return Account; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Account == "Admin")
            {
                DynamicHyperLink1.Visible = true;
            }
        }
    }
}