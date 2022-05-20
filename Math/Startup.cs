using Math.Abstractions;
using Math.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Math
{
    public class Startup : IStartup
    {

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("configuring services for Math Modul");
            services.AddMvc().AddApplicationPart(Assembly.GetAssembly(typeof(Startup))).AddControllersAsServices();
            services.AddScoped<IMathService, DefaultMathService>();

            services.AddSwaggerGen(c => {
                var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                c.IncludeXmlComments(Path.Combine(basePath,"Math.xml"));
            });
            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            Console.WriteLine("Configuring Math Module");
        }
    }
}
