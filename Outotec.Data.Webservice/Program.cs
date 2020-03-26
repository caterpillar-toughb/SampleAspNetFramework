using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Outotec.Data.Webservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }


        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:8081")
                .UseStartup<Startup>()
                .UseSetting("detailedErrors", "true") // Use these lines to diagnose issues once deployed
                .CaptureStartupErrors(true)
                .Build();
        }
    }
}