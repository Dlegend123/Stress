﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabAssignment
{
    public class User:IdentityUser
    {

        public User() : base()
        {

        } 
    }
}