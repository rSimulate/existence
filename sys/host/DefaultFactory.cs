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
    public class DefaultFactory : IFactory
    {
        public Store.IStorage Storage()
        {
            return new DefaultStorage();
        }

        public Log.ILog Log()
        {
            return new DefaultLog();
        }

        public Auth.IAuthentication Auth()
        {
            return new DefaultAuthentication();
        }
    }
}
