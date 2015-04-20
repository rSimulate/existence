using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host.Auth
{
    public interface IAuthentication
    {
        bool Authorized(Nancy.Request Request);

        bool Authorized(Nancy.Request Request, string resource_path);
    }
}
