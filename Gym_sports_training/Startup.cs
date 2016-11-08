using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gym_sports_training.Startup))]
namespace Gym_sports_training
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
