namespace MarvelChallange.Domain.Models;

public static class AppSettings
{
    public static string AuthorProfile { get => GetValueFromKey("AuthorProfile"); }

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

    public static class FileExportData
    {
        public static string FileName { get => GetValueFromKey("FileExportData:FileName"); }

        public static string FileExtension { get => GetValueFromKey("FileExportData:FileExtension"); }

        public static string FileOutputDirectory { get => GetValueFromKey("FileExportData:FileOutputDirectory"); }
    }

    private static string GetValueFromKey(string key) => GetAppSettings().GetSection(key).Value;

    private static IConfigurationRoot GetAppSettings()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder();
        var appSettingFile = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

        if (!File.Exists(appSettingFile))
            throw new Exception("appsettings.json file not found in project.");

        builder.AddJsonFile(appSettingFile);
        IConfigurationRoot root = builder.Build();
        return root;
    }
}
