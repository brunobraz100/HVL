using System.IO;
using Microsoft.Extensions.Configuration;
using System;

namespace hvlFront
{
    public class Settings
    {
        public static string baseUrl
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, false)
                    .Build();


                string url = configuration.GetValue<string>("urlBaseServer:url");
                if(string.IsNullOrWhiteSpace(url))
                {
                    throw new NotImplementedException("Url do serviço não encontrada!");
                }
                return url;
            }
        }
    }
}