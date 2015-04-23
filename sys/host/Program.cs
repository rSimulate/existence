using Nancy.Hosting.Self;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
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

                //Under mono if you daemonize a process a Console.ReadLine will cause an EOF 
                //so we need to block another way
                if (args.Any(s => s.Equals("-d", StringComparison.CurrentCultureIgnoreCase)))
                {
                    Thread.Sleep(Timeout.Infinite);
                }
                else
                {
                    Console.ReadKey();
                }

                host.Stop();  // stop hosting
            }
        }
    }
}
