﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabAssignment
{
    public class ShoppingCart
    {
        public List<Product> products { get; set; }
        public ShoppingCart()
        {
            products = new List<Product>();
        }
    }
}