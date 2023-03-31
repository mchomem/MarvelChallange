using MarvelChallange.Service.Services;
using MarvelChallange.Service.Services.External;
using MarvelChallange.Service.Services.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

DependenceInjectionSetup(builder);

SwaggerSetup(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void SwaggerSetup(WebApplicationBuilder builder)
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(
            "v1"
            , new OpenApiInfo
            {
                Title = "MarvelChallenge.Api",
                Version = "v1",
                Description = "MarvelChallenge api.",
                Contact = new OpenApiContact
                {
                    Name = "Misael C. Homem",
                    Url = new Uri("https://www.github.com/mchomem")
                },
            });

        string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
}

static void DependenceInjectionSetup(WebApplicationBuilder builder)
{
    builder.Services.AddScoped(typeof(MarvelService));
    builder.Services.AddScoped<IMarvelChallangeService, MarvelChallangeService>();
}
