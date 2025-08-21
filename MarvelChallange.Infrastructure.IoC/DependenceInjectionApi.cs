namespace MarvelChallange.Infrastructure.IoC;

public static class DependenceInjectionApi
{
    public static IServiceCollection AddInfrastructureApi(this IServiceCollection services)
    {
        #region Services

        services.AddScoped<IMarvelService, MarvelService>();
        services.AddScoped<IMarvelChallangeService, MarvelChallangeService>();

        #endregion

        return services;
    }

    public static IServiceCollection AddInfrastructureSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
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
                        Url = new Uri(configuration.GetSection("AuthorProfile").Value!)
                    },
                });

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
