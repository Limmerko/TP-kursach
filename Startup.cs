using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Computer_Store.Startup))]
namespace Computer_Store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
