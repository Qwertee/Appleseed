using Nancy;

namespace Appleseed.WebServer
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get("/", test => "Tet");
        }
    }
}