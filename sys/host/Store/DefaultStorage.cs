using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host.Store
{
    public class DefaultStorage : IStorage
    {

        public IList<string> Query(string path)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string resource_path)
        {
            throw new NotImplementedException();
        }

        public Hashtable Create(string resource_path, Nancy.Request Request)
        {
            Hashtable results = new Hashtable();

            string value = Request.Body.ReadAsString();


            return results;
        }

        public object Update(string resource_path, Nancy.Request Request)
        {
            throw new NotImplementedException();
        }

        public object Delete(string resource_path, Nancy.Request Request)
        {
            throw new NotImplementedException();
        }
    }
}
