using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LabAssignment
{
    public partial class SignIn : System.Web.UI.Page
    {
        SqlCommand sqlCommand;
        SqlConnection conn;
        SqlDataReader reader;
        string filePath;
        byte[] temp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.IsSecureConnection)
            {
                string url = ConfigurationManager.AppSettings["SecurePath"] + "SignIn.aspx";
                Response.Redirect(url);
            }
            if (Session["Account"] == "Admin")
                Page.Master.FindControl("DynamicHyperLink1").Visible = true;

        }
        protected void Validate(object sender, EventArgs e)
        {
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
                            Response.Redirect("~/ManageProduct.aspx",false);
                            break;
                        }
                    }
                }
            }
            catch (SqlException)
            {

            }
            if(conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }
    }
}