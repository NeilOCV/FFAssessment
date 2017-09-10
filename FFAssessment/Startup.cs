using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FFAssessment.Startup))]
namespace FFAssessment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
