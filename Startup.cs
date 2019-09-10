using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RentAndCycleCodeFirst.Startup))]
namespace RentAndCycleCodeFirst
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
