using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host.Log
{
    public interface ILog
    {
        void LogHttpGet(Nancy.Request Request);

        void LogError(Exception x);

        void LogNotAuthorized(Nancy.Request Request);

        void LogHttpPost(Nancy.Request Request);

        void LogHttpPut(Nancy.Request Request);

        void LogHttpDelete(Nancy.Request Request);

        void LogCreated(Nancy.Request Request, System.Collections.Hashtable response_obj);

        void LogUpdated(Nancy.Request Request, System.Collections.Hashtable response_obj);

        void LogDeleted(Nancy.Request Request, System.Collections.Hashtable response_obj);

        void LogError(Exception x, Guid ErrorId);

        void LogError(Nancy.Request Request, Exception x, Guid ErrorId);

        void LogNotFound(Nancy.Request Request);

        void LogUnAuthorized(Nancy.Request Request);

        void LogOk(Nancy.Request Request);
    }
}
