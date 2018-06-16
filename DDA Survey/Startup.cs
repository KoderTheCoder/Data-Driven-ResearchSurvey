using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DDA_Survey.Startup))]
namespace DDA_Survey
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
