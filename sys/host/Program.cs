using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting host");
            string host_path = ConfigurationManager.AppSettings["host_path"];
            Console.WriteLine("host path: {0}",host_path);

            using (var host = new NancyHost(new Uri(host_path)))
            {
                Console.WriteLine("Begin start Host");
                host.Start();
                Console.ReadLine();
            }
        }
    }
}
