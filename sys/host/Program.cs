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
            string host_path = ConfigurationManager.AppSettings["host_path"];

            using (var host = new NancyHost(new Uri(host_path)))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }
}
