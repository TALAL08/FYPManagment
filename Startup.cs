using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FYPManagment.Startup))]
namespace FYPManagment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
