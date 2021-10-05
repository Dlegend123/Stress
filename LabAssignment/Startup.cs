using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabAssignment.Startup))]
namespace LabAssignment
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
