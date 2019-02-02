using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace WebAPI.JWTAuth.Template.Helpers
{
    public static class ServerConfig
    {
        public static IPEndPoint GetIpSettings(IConfigurationRoot config)
        {
            // Determine IP
            var ip = IPAddress.Loopback;
            var port = 5000;

            try
            {
                var url = config.GetValue<string>("urls");
                var uri = new Uri(url);
                port = uri.Port;
                ip = IPAddress.Parse(uri.Host);
            }
            catch
            {
                Console.WriteLine("No server url specified, using loopback address.");
            }

            return new IPEndPoint(ip, port);
        }
    }
}
