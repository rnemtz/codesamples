using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeExercises.Mvc.Web.Startup))]
namespace CodeExercises.Mvc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
