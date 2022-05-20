using Microsoft.OpenApi.Models;
using ModularMonolithSpike.Middlewares;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Load Modules
var moduleList = builder.Configuration.GetSection("LoadModules").AsEnumerable();
List<IStartup> startups = LoadModules(moduleList.Select(kv => kv.Value)).ToList();


// Add services to the container.
startups.ForEach(startup => startup.ConfigureServices(builder.Services));
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Modular Monolith spike Api", Version = "v1" });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

startups.ForEach(startup => startup.Configure(app));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


IEnumerable<IStartup> LoadModules(IEnumerable<string> moduleNames) {

    string mainDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    foreach (var module in moduleNames) {
        if (string.IsNullOrWhiteSpace(module))
            continue;

        var moduleFile = Path.Combine(mainDir, $"{module}.dll");
        if (!File.Exists(moduleFile))
        {
            Console.WriteLine($"{module} module not found");
            continue;
        }

        Assembly assembly = Assembly.LoadFile(moduleFile);
        Type startuptype = assembly.GetType($"{module}.Startup");

        if (startuptype == null)
        {
            Console.WriteLine($"Startup not found for {module} module");
            continue;
        }

        IStartup startup = Activator.CreateInstance(startuptype) as IStartup;

        if (startup == null)
        {
            Console.WriteLine($"Cannot load {module} module");
            continue;
        }

        Console.WriteLine($"Module {module} loaded");
        yield return startup;
         
    }  
}