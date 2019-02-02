using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using static WebAPI.JWTAuth.Template.Helpers.ServerConfig;

namespace WebAPI.JWTAuth.Template.Extensions
{
    public static class IWebHostBuilderExtensions
    {
        public static IWebHostBuilder SetupKestrel(this IWebHostBuilder builder, IConfigurationRoot config)
        {
            // Read HTTPS cert configs (command line arguments)
            var certFileName = config.GetValue<string>("certfile");
            var certPassword = config.GetValue<string>("certpass");

            if (!string.IsNullOrEmpty(certFileName) && !string.IsNullOrEmpty(certPassword))
            {
                return builder.UseKestrel(options =>
                {
                    options.Listen(GetIpSettings(config), listenOptions =>
                    {
                        listenOptions.UseHttps(certFileName, certPassword);
                    });
                });
            }

            // Default to HTTP if no certificate configurations was provided
            System.Console.WriteLine("No HTTPS certificate configured, using HTTP instead");

            return builder.UseKestrel(options =>
            {
                options.Listen(GetIpSettings(config));
            });
        }
    }
}
