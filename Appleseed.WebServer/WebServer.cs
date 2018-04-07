using System;
using System.Linq;
using Nancy.Hosting.Self;

namespace Appleseed.WebServer
{
    class WebServer
    {   
        public static void Main(string[] args)
        {
            var hostAndPort = args.ElementAtOrDefault(0) ?? "localhost:1234";
            Uri uri = new Uri("http://" + hostAndPort);
            
            using (var host = new NancyHost(uri))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1234");
                Console.ReadLine();
            }
        }
    }
}