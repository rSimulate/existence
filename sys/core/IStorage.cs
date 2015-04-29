using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace host.Store
{
    public interface IStorage
    {
        IList<String> Query(String path);

        bool Exists(string resource_path);

        Hashtable Create(string resource_path, Nancy.Request Request);

        object Update(string resource_path, Nancy.Request Request);

        object Delete(string resource_path, Nancy.Request Request);
    }
}
