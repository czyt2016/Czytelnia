using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Czytelnia.Startup))]
namespace Czytelnia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
