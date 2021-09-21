using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabAssignment
{
    public class Customer : User
    {
        ShoppingCart cart;
        public Customer():base(){
            
        }
    }
}