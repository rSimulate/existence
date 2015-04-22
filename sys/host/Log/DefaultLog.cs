using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host.Log
{
    public class DefaultLog : ILog
    {
        public void LogHttpGet(Nancy.Request Request)
        {
            Console.WriteLine("HTTP {0} {1}", Request.Method, Request.Path);
        }

        public void LogError(Exception x)
        {
            Console.WriteLine("HTTP 500 {0}",x.Message);
        }

        public void LogNotAuthorized(Nancy.Request Request)
        {
            Console.WriteLine("HTTP 401 {0} {1}", Request.Method, Request.Path);
        }

        public void LogHttpPost(Nancy.Request Request)
        {
            Console.WriteLine("HTTP {0} {1}", Request.Method, Request.Path);
        }

        public void LogHttpPut(Nancy.Request Request)
        {
            Console.WriteLine("HTTP {0} {1}", Request.Method, Request.Path);
        }

        public void LogHttpDelete(Nancy.Request Request)
        {
            Console.WriteLine("HTTP {0} {1}", Request.Method, Request.Path);
        }

        public void LogCreated(Nancy.Request Request, System.Collections.Hashtable response_obj)
        {
            Console.WriteLine("HTTP 201 {0} {1}", Request.Method, Request.Path);
        }

        public void LogUpdated(Nancy.Request Request, System.Collections.Hashtable response_obj)
        {
            Console.WriteLine("HTTP 201.2 {0} {1}", Request.Method, Request.Path);
        }

        public void LogDeleted(Nancy.Request Request, System.Collections.Hashtable response_obj)
        {
            Console.WriteLine("HTTP {0} {1}", Request.Method, Request.Path);
        }

        public void LogError(Exception x, Guid ErrorId)
        {
            Console.WriteLine("HTTP 500 {0} {1}",ErrorId, x.Message);
        }

        public void LogError(Nancy.Request Request, Exception x, Guid ErrorId)
        {
            Console.WriteLine("HTTP 500 {0} {1} {2} {3}", Request.Method, Request.Path,ErrorId, x.Message);
        }

        public void LogNotFound(Nancy.Request Request)
        {
            Console.WriteLine("HTTP 404 {0} {1}", Request.Method, Request.Path);
        }

        public void LogUnAuthorized(Nancy.Request Request)
        {
            Console.WriteLine("HTTP 401 {0} {1}", Request.Method, Request.Path);
        }

        public void LogOk(Nancy.Request Request)
        {
            Console.WriteLine("HTTP 200 {0} {1}", Request.Method, Request.Path);
        }
    }
}
