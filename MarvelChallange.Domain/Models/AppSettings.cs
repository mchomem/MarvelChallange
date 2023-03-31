using Microsoft.Extensions.Configuration;

namespace MarvelChallange.Domain.Models
{
    public static class AppSettings
    {
        public static class ExternalServices
        {
            public static class MarvelApi
            {
                public static string UrlBase { get => GetValueFromKey("ExternalServices:MarvelApi:UrlBase"); }
                public static string Apikey { get => GetValueFromKey("ExternalServices:MarvelApi:Apikey"); }
                public static string Timestamp { get => GetValueFromKey("ExternalServices:MarvelApi:Timestamp"); }
                public static string Hash { get => GetValueFromKey("ExternalServices:MarvelApi:Hash"); }
            }
        }

        public static string FileName { get => GetValueFromKey("FileName"); }

        private static string GetValueFromKey(string key) => GetAppSettings().GetSection(key).Value;

        private static IConfigurationRoot GetAppSettings()
        {
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
                IConfigurationRoot root = builder.Build();
                return root;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
