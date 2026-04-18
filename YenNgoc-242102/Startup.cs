using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YenNgoc_242102.Startup))]
namespace YenNgoc_242102
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
