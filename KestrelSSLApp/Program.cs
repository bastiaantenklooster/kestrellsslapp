using Microsoft.AspNetCore.Hosting;
using System;
using System.Security.Cryptography.X509Certificates;

namespace KestrelSSLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseEnvironment("Development")
                
                .UseKestrel(config =>
                {
                    config.ListenAnyIP(8008, listenOptions =>
                    {
                        listenOptions.UseHttps(httpsOptions =>
                        {
                            var serverCertificate = new X509Certificate2(AppDomain.CurrentDomain.BaseDirectory + "/certificate.pfx", "Geheim");

                            httpsOptions.CheckCertificateRevocation = false;   
                            httpsOptions.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.AllowCertificate;
                            httpsOptions.ServerCertificate = serverCertificate;
                            httpsOptions.ClientCertificateValidation = (cert, chain, errors) =>
                            {
                                Console.WriteLine("Wow a certificate");
                                return cert.Thumbprint == "3";
                            };
                        });

                        listenOptions.NoDelay = true;
                    });
                })
                .UseStartup<Startup>()
                .Build();

            host.Run();

            host.WaitForShutdown();
        }
    }
}
