using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KestrelSSLApp
{
    class LoggerMiddleware
    {

        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                Console.WriteLine(reader.ReadToEnd());
            }

            await _next(context);
        }

    }
}
