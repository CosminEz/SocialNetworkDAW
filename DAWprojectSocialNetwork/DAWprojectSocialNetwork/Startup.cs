using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DAWprojectSocialNetwork.Startup))]
namespace DAWprojectSocialNetwork
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
