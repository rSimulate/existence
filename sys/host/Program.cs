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

            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            using (var host = new NancyHost(new Uri(host_path)))
            {
                Console.WriteLine("Begin start Host");
                host.Start();
                Console.WriteLine("Host is online.");
                Console.ReadLine();
            }
        }

        static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            Console.WriteLine("ERROR : {0}", e.Exception.Message);
        }
    }
}
