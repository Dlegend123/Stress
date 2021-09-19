using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LabAssignment
{
    
    public class Entity:IdentityDbContext
    {
        public Entity() : base(){
            Database.SetInitializer<Entity>(null);
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Admin> admins;
        public DbSet<Customer> customers;
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Seed();
            
        }
    }
}