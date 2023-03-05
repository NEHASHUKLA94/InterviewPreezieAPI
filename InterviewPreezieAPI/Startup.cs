using MediatR;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace InterviewPreezieAPI
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<DataStore>();
        }
    }
}
