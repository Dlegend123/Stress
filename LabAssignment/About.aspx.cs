﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "About.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] != null)
            {
                if ((Session["Account"] as IdentityUser).Roles.Any(x => x.RoleId == "Admin"))
                    Page.Master.FindControl("AdminFunc").Visible = true;
            }

        }
    }
}