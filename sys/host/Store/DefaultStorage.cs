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
        public static Dictionary<string, string> Resources = new Dictionary<string, string>();


        public IList<string> Query(string path)
        {
            return Resources.Where(r => r.Key.ToLower().StartsWith(path.ToLower()))
                            .Select(r => r.Value)
                            .ToList();
        }

        public bool Exists(string resource_path)
        {
            return Resources.Where(r => r.Key.ToLower() == resource_path.ToLower()).Any();
        }

        public Hashtable Create(string resource_path, Nancy.Request Request)
        {
            Hashtable results = new Hashtable();

            string value = Utilities.ReadAsString(Request.Body);
            string key = Request.Path;

            if (this.Exists(key))
            {
                Resources[key] = value;
                results.Add("action", "update");
            }
            else
            {
                bool added = true;
                Resources.Add(key, value);

                if (added)
                {
                    results.Add("action", "created");
                }
            }

            return results;
        }

        public object Update(string resource_path, Nancy.Request Request)
        {
            Hashtable results = new Hashtable();

            string value = Utilities.ReadAsString(Request.Body);
            string key = Request.Path;

            if (this.Exists(key))
            {
                Resources[key] = value;
                results.Add("action", "update");
            }
            else
            {
                bool added = true; Resources.Add(key, value);

                if (added)
                {
                    results.Add("action", "created");
                }
            }

            return results;
        }

        public object Delete(string resource_path, Nancy.Request Request)
        {
            Hashtable results = new Hashtable();

            string value = "";
            string key = Request.Path;

            if (this.Exists(key))
            {
                Resources.Remove(key);
                results.Add("action", "deleted");
            }
            else
            {
                results.Add("results", "not deleted");
            }

            return results;
        }
    }
}
