using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace LabAssignment
{
    public class UserValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if ("" == userName || "" == password)
            {
                throw new ArgumentNullException();
            }
            SqlCommand sqlCommand;
            SqlConnection conn;
            SqlDataReader reader;
            conn = new SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString
            };
            sqlCommand = new SqlCommand("Select * from WebAdmin", conn);
            conn.Open();
            try
            {
                int found = 0;
                reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    if (userName == reader["a_name"].ToString())
                    {
                        if (password == reader["a_pass"].ToString())
                        {
                            found = 1;
                            conn.Close();
                            break;
                        }
                    }
                }
                if(found!=1)
                    throw new FaultException("Unknown Username or Incorrect Password");
            }
            catch (SqlException)
            {

            }
            if(conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }

    }
}