using System;
using Nancy;
using Nancy.ModelBinding;

namespace Appleseed.WebServer
{
    public class RequestObject 
    {
        public string Start { get; set; }
    }
    
    public class ApiModule : NancyModule
    {
        public ApiModule()
        {
            // CORS
            After.AddItemToEndOfPipeline((ctx) => ctx.Response
                .WithHeader("Access-Control-Allow-Origin", "*")
                .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));

            Get("/", ctx =>
            {
                var req = this.Bind<RequestObject>();
                Console.WriteLine(req.ToString());
                return "Success";
            });
        }
    }
}