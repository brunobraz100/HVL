using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace hvl
{
    public class Settings
    {
        public static string secret = "hlvsecret2022documents";

        public static string xApiKey = "hvlV3nc!m3nt0s2022";
        public static string Connection
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                string connectionString = configuration.GetConnectionString("DefaultConnection");

                if(string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new Exception("A connexão não foi encontrada!");
                }

                return connectionString;

            }
        }

        public static string Email
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                string Email = configuration.GetValue<string>("MailSettings:Mail");

                if(string.IsNullOrWhiteSpace(Email))
                {
                    throw new Exception("O E-mail não foi encontrado!");
                }

                return Email;

            }
        }

        public static string Senha
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                string Senha = configuration.GetValue<string>("MailSettings:Password");

                if(string.IsNullOrWhiteSpace(Senha))
                {
                    throw new Exception("A senha do e-mail não foi encontrada!");
                }

                return Senha;

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
                    throw new Exception("Nenhum SMTP foi encontrado.");
                }

                return smtp;

            }
        }


        public static int Porta
        {
            get
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                
                int port = int.Parse(configuration.GetValue<string>("MailSettings:Port"));

                if(port == 0)
                {
                    throw new Exception("Nenhuma porta foi encontrada.");
                }

                return port;

            }
        }
    }
}
