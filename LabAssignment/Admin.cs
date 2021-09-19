using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LabAssignment
{
    [Authorize(Roles = "Administrator")]
    [Table("WebAdmin")]
    public class Admin:User
    {
        public Admin() : base()
        {

        }
    }
}