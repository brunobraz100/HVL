using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace hvlMailer
{
    public class Settings 
    {
        public static string xApiKey = "hvlV3nc!m3nt0s2022";

        public static string Email
        {
            get 
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                string email = configuration.GetValue<string>("MailSettings:Mail");

                if(string.IsNullOrWhiteSpace(email))
                {
                    throw new NotImplementedException("E-mail não encontrado no appsettings");
                }

                return email;
            }
        }

        public static string Password
        {
            get 
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                string password = configuration.GetValue<string>("MailSettings:Password");

                if(string.IsNullOrWhiteSpace(password))
                {
                    throw new NotImplementedException("Senha não encontrada no appsettings");
                }

                return password;
            }
        }


        public static string SMTP
        {
            get 
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                string smtp = configuration.GetValue<string>("MailSettings:Host");

                if(string.IsNullOrWhiteSpace(smtp))
                {
                    throw new NotImplementedException("SMTP não encontrado no appsettings");
                }

                return smtp;
            }
        }


        public static string DisplayName
        {
            get 
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                string displayName = configuration.GetValue<string>("MailSettings:DisplayName");

                if(string.IsNullOrWhiteSpace(displayName))
                {
                    throw new NotImplementedException("DisplayName não encontrado no appsettings");
                }

                return displayName;
            }
        }

        public static int Port
        {
            get 
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                int port = configuration.GetValue<int>("MailSettings:Port");

                if(port == 0)
                {
                    throw new NotImplementedException("Porta não encontrada no appsettings");
                }

                return port;
            }
        }


        public static string Url
        {
            get 
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                string displayName = configuration.GetValue<string>("Api:url");

                if(string.IsNullOrWhiteSpace(displayName))
                {
                    throw new NotImplementedException("Url não encontrado no appsettings");
                }

                return displayName;
            }
        }


    }
}