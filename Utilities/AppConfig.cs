using Microsoft.Extensions.Configuration;

namespace Marsqa1Specflow.Utilities
{
    public class AppConfig
    {
        public string Browser { get; set; }
        public string Url { get; set; }

        public static AppConfig LoadConfiguration()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var appConfig = new AppConfig();
            configuration.GetSection("Settings").Bind(appConfig);

            return appConfig;
        }
    }
}
