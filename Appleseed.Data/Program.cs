using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Appleseed.DecisionTree;

namespace Appleseed.Data
{
    class Program
    {
        public static void Main(string[] args)
        {
            MySqlConnection connection;

            Console.WriteLine("Host");
            String host = Console.ReadLine();
            
            Console.WriteLine("Database");
            String db = Console.ReadLine();
            
            Console.WriteLine("Username");
            String username = Console.ReadLine();
            
            Console.WriteLine("Password");
            String password = Console.ReadLine();
            

            var str = @"server=" + host + "userid=" + username + 
                           ";password=" + password +
                           ";database=" + db + ";SslMode=none";

            try
            {
                connection = new MySqlConnection(str);
                connection.Open();

                var cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM flights LIMIT 100";
                cmd.CommandTimeout = int.MaxValue;
                cmd.Prepare();

                var tree = new DecisionTree.DecisionTree();
                
                Stopwatch watch = new Stopwatch();
                watch.Start();
                
                

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                }
                
                Console.WriteLine(watch.ElapsedMilliseconds);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}