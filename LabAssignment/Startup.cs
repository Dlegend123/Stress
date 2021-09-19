using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Google;
using System.Data.SqlClient;
using Fluent.Infrastructure.FluentStartup;
using Fluent.Infrastructure.FluentModel;
using Microsoft.Owin.Builder;

[assembly: OwinStartup(typeof(LabAssignment.Startup))]

namespace LabAssignment
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddScoped<SignInManager<IdentityUser>>();
            services.AddIdentity<IdentityUser, IdentityRole>()
        .AddDefaultTokenProviders();
            services.AddIdentity<IdentityUser, IdentityRole>()
        .AddDefaultTokenProviders();
            IdentityBuilder builder = services.AddIdentityCore<IdentityUser>();

            builder = new IdentityBuilder(builder.UserType, builder.Services);

            builder.AddUserStore<Microsoft.AspNet.Identity.EntityFramework.IdentityDbContext>();
            builder.AddRoleValidator<Microsoft.AspNetCore.Identity.RoleValidator<IdentityRole>>();
            builder.AddRoleManager<Microsoft.AspNetCore.Identity.RoleManager<IdentityRole>>();
            builder.AddSignInManager<SignInManager<IdentityUser>>();

            services.AddTransient<SqlConnection>(e => new SqlConnection(ConfigurationManager.ConnectionStrings["LIConnectionString"].ConnectionString));


            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Administrator"));
            });
            services.AddAuthorizationCore(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
/*
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "~/SignIn";
                options.AccessDeniedPath = "~/Default";
                options.SlidingExpiration = true;
            });*/
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and also store information about a user logging in with a third party login provider.
            // This is required if your application allows users to login
            /*GoogleOAuth2AuthenticationOptions options = new GoogleOAuth2AuthenticationOptions
                (
                    options.
                );*/

            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.Build();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/SignIn")
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");
            // app.UseGoogleAuthentication();
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
