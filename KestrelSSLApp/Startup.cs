using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace KestrelSSLApp
{
    class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            app.Use((context, next) =>
            {
                var cert = context.Connection.GetClientCertificateAsync();

                Console.WriteLine(cert.Result.Thumbprint); 

                return next();
            });


            app.UseMiddleware<LoggerMiddleware>();

            

            app.Run(context =>
            {
                Console.WriteLine("Sent response");

                return context.Response.WriteAsync("1");
            });
        }

    }
}
