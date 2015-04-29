using Newtonsoft.Json;
using Orient.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host.Store
{
    public class OrientDbStorage : IStorage
    {
        private static bool db_init = false;

        private static bool initDb()
        {
            if (db_init == false)
            {
                OClient.CreateDatabasePool("orbitbinder.ddns.net", 2424, "orbit", ODatabaseType.Graph, "root", "password", 10, "orbit");
               db_init = true;
            }
            return db_init;
        }

        public OrientDbStorage()
        {
            initDb();


        }


        public IList<string> Query(string path)
        {
            var results = new List<String>();

            using (ODatabase database = new ODatabase("orbit"))
            {
                var dbResults = database.Query("select content from orbitresource where path like '" + path.ToLower() + "%'")
                              .ToList();

                if (dbResults.Any())
                {
                    results = dbResults.Select(d => JsonConvert.SerializeObject(d)).ToList();
                }
            }

            return results;
        }

        public bool Exists(string resource_path)
        {
            return this.Query(resource_path).Any();
        }

        public System.Collections.Hashtable Create(string resource_path, Nancy.Request Request)
        {
            Hashtable result = new Hashtable();
            
            // transaction id
            result.Add("transactionid", Guid.NewGuid());
            
            
            using (ODatabase database = new ODatabase("orbit"))
            {
                database.Insert().Into("orbitresource")
                        .Set("path", resource_path)
                        .Set("content", Request.Body.ReadAsString())
                        .Run();
                result.Add("action", "created");
            }

            return result;
        }

        public object Update(string resource_path, Nancy.Request Request)
        {
            Hashtable result = new Hashtable();

            // transaction id
            result.Add("transactionid", Guid.NewGuid());

            using (ODatabase database = new ODatabase("orbit"))
            {
                database.Update().Class("orbitresource")
                                 .Set("content", Request.Body.ReadAsString())
                                 .Where("path")
                                 .Equals(resource_path.ToLower())
                                 .Run();
                result.Add("action", "updated");
            }

            return result;
        }

        public object Delete(string resource_path, Nancy.Request Request)
        {
            Hashtable result = new Hashtable();

            // transaction id
            result.Add("transactionid", Guid.NewGuid());


            using (ODatabase database = new ODatabase("orbit"))
            {
                database.Delete.Document("orbitResource")
                               .Where("path")
                               .Equals(resource_path)
                               .Run();
            }

            return result;
        }
    }
}
