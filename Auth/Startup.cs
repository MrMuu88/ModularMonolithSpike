using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Auth.Services;
using Auth.Abstractions;
using Auth.Middlewares;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Auth
{
    public class Startup : IStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            Console.WriteLine("configuring Auth Module");
            app.UseWhen(
                c => !c.Request.Path.StartsWithSegments("/swagger"),
                app=>app.UseMiddleware<AuthorizationMiddleware>()
            );
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("configuring services for Auth Module");
            services.AddSwaggerGen(c =>
            {
                //adding Xml documentations
                var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                c.IncludeXmlComments(Path.Combine(basePath, "Auth.xml"));

                //add Security header requirement
                var securityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                };

                c.AddSecurityDefinition("Authorization", securityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { new OpenApiSecurityScheme{ Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme,Id="Authorization"} }, Array.Empty<string>() }
                });
            });

            services.AddMvc().AddApplicationPart(Assembly.GetAssembly(typeof(Startup))).AddControllersAsServices();
            services.AddScoped<IAuthService, MockAuthService>();
            return services.BuildServiceProvider();
        }
    }
}

