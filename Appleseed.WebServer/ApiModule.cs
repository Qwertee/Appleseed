using System;
using Appleseed.DecisionTree;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Nancy;
using Nancy.ModelBinding;

namespace Appleseed.WebServer
{
    public class ClassifyRequest 
    {
        public string Month { get; set; }
        public string Day { get; set; }
        public string DayOfWeek { get; set; }
        public string Airline { get; set; }
        public string Airport { get; set; }
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
            
            Get("/classify", ctx =>
            {
                var req = this.Bind<ClassifyRequest>();

                if (req.Month == null || req.Day == null || req.DayOfWeek == null ||
                    req.Airline == null || req.Airport == null)
                    return "{\"error\": \"must provide all parameters\"}";

                // build example based on params
                var example = new Example("");
                
                example.AddAttribute(Attrs.Month, int.Parse(req.Month));
                example.AddAttribute(Attrs.Day, int.Parse(req.Day));
                example.AddAttribute(Attrs.DayOfWeek, int.Parse(req.DayOfWeek));
                example.AddAttribute(Attrs.Airline, req.Airline);
                example.AddAttribute(Attrs.Airport, req.Airport);

                var result = WebServer.Tree.Classify(example);

                var value = result.classification.ToLower();
                var ratio = result.randomnessRatio;

                return "{\"classification\": " + value + ", \"randomnessRatio\": " + ratio + "}";
            });
        }
    }
}