using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host.Auth
{
    public class DefaultAuthentication : IAuthentication
    {
        public bool Authorized(Nancy.Request Request)
        {
            return true;
        }

        public bool Authorized(Nancy.Request Request, string resource_path)
        {
            return true;
        }
    }
}
