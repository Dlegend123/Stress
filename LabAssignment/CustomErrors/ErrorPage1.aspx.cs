using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabAssignment.CustomErrors
{
    public partial class ErrorPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception err = Session["LastError"] as Exception;
            //Exception err = Server.GetLastError();
            if ( err!= null)
            {
                err = err.GetBaseException();
                ErrorMessage.InnerText = err.Message;
                ErrorSource.InnerText = err.Source;
                InnerEx.InnerText = (err.InnerException != null) ? err.InnerException.ToString() : "";
                StackTrace.InnerText = err.StackTrace;
                Session["LastError"] = null;
            }
        }
    }
}