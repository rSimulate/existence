using host.Auth;
using host.Log;
using host.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host
{
    public interface IFactory
    {
        IStorage Storage();

        ILog Log();

        IAuthentication Auth();
    }
}
