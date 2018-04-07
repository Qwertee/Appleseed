using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Appleseed.DecisionTree;
using MySql.Data.MySqlClient;
using Nancy.Hosting.Self;

namespace Appleseed.WebServer
{
    class WebServer
    {
        public static DecisionTree.DecisionTree Tree;
        
        public static void Main(string[] args)
        {
            // web server host config
            HostConfiguration hostConfig = new HostConfiguration();
            hostConfig.UrlReservations.CreateAutomatically = true;
            var hostAndPort = args.ElementAtOrDefault(0) ?? "localhost:1234";
            Uri uri = new Uri("http://" + hostAndPort);
            
            Console.WriteLine("Running on http://localhost:1234");
            new Thread(() => { 
                // start host
                using (var host = new NancyHost(hostConfig, uri))
                {
                    host.Start();
                }
            }).Start();
            
            
            Console.Write("Password: ");
            String password = Console.ReadLine();
            
            Console.Write("Limit Dataset (-1 for no limit): ");
            int limit = int.Parse(Console.ReadLine() ?? "100");
            
            // sql connection string for db
            var connStr = "Server=appleseed.keenant.com" +
                          ";Database=appleseed" +
                          ";User ID=johnny" +
                          ";Password=" + password + 
                          ";SslMode=none";

            Tree = CreateTree(connStr, limit);


            var test = new Example("");
            test.AddAttribute("month", 1);
            test.AddAttribute("day", 1);
            test.AddAttribute("dayOfWeek", 4);
            test.AddAttribute("airline", "NK");
            test.AddAttribute("airport", "MSP");

            Console.WriteLine("Delayed? " + Tree.Classify(test));

            Console.ReadKey();
        }

        private static DecisionTree.DecisionTree CreateTree(string connStr, int limit)
        {
            MySqlConnection connection;

            try
            {
                connection = new MySqlConnection(connStr);
                connection.Open();

                var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM flights";
                if (limit >= 0)
                    cmd.CommandText += " LIMIT " + limit;
                cmd.CommandTimeout = int.MaxValue;
                cmd.Prepare();

                
                Stopwatch watch = new Stopwatch();
                watch.Start();

                List<Example> trainingSet = new List<Example>();


                Console.WriteLine("Retrieving data set...");

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var delay = reader.GetInt32("DEPARTURE_DELAY");
                    
                    // it is considered delayed if it leaves more than 5 mins
                    // after scheduled departure time
                    var isDelayed = delay > 5;

                    // construct the example with the classification as
                    // "true" or "false" depending on if it was delayed
                    var example = new Example(isDelayed + "");

                    // fetch attributes
                    var month = reader.GetInt32("MONTH");
                    var day = reader.GetInt32("DAY");
                    var dayOfWeek = reader.GetInt32("DAY_OF_WEEK");
                    var airline = reader.GetString("AIRLINE");
                    var airport = reader.GetString("ORIGIN_AIRPORT");
                    
                    // add attributes
                    example.AddAttribute("month", month);
                    example.AddAttribute("day", day);
                    example.AddAttribute("dayOfWeek", dayOfWeek);
                    example.AddAttribute("airline", airline);
                    example.AddAttribute("airport", airport);

                    trainingSet.Add(example);
                }

                Console.WriteLine("Building decision tree...");
                
                var tree = new DecisionTree.DecisionTree();
                tree.BuildTree(trainingSet);

                Console.WriteLine("Done.");
                
                Console.Write("Elapsed Time (ms): ");
                Console.WriteLine(watch.ElapsedMilliseconds);

                return tree;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}