using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigiLocker3.Startup))]
namespace DigiLocker3
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
