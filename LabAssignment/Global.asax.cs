using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace LabAssignment
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            if (Context != null)
            {
                // Of course, you don't need to use both conditions bellow
                // If you want, you can use only your user name or only role name
                if (Context.User.IsInRole("Developers") ||
                (Context.User.Identity.Name == "YourUserName"))
                {
                    // Use Server.GetLastError to recieve current exception object
                    Exception CurrentException = Server.GetLastError();

                    // We need this line to avoid real error page
                    Server.ClearError();

                    // Clear current output
                    Response.Clear();

                    // Show error message as a title
                    Response.Write("<h1>Error message: " + CurrentException.Message + "</h1>");
                    // Show error details
                    Response.Write("<p>Error details:</p>");
                    Response.Write(CurrentException.ToString());
                }
            }
        }
    }
}