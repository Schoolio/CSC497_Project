using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSC497_Project_JagQuiz.Startup))]
namespace CSC497_Project_JagQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
