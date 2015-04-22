using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace host
{
    using Nancy;
    using Nancy.ModelBinding;
    using Nancy.Responses;
    using System.Dynamic;
    using System.Collections;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using System.IO;
    using System.Collections.Concurrent;
    using System.Configuration;
    using host.Store;
    using host.Log;
    using host.Auth;

    public class ApiModule : NancyModule
    {
        private IStorage storage = null;
        private ILog log = null;
        private IAuthentication auth = null;

        #region [ Functions ]
        /// <summary>
        /// Authentication Verification Function
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        private bool Authenticated(Request Request)
        {
            bool result = false;

            result = true;

            return result;
        }
        #endregion

        #region [ Constructor ]
        public ApiModule(ILog _log, IStorage _storage, IAuthentication _auth)
            : base(ConfigurationManager.AppSettings["base_route"])
        {
            string api_route = "/{path*}";
            this.storage = _storage;
            this.log = _log;
            this.auth = _auth;

            Get[api_route] = _ =>
            {
                log.LogHttpGet(Request);

                try
                {
                    #region [ HTTP GET ]
                    Hashtable response_obj = new Hashtable();
                    string path = Request.Path;
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["resource_path"], Request.Path.Replace("/", "_"));

                    if (this.auth.Authorized(Request))
                    {
                        var results = this.storage.Query(resource_path)
                                          .Where(r => r.StartsWith(path))
                                          .OrderBy(r => r);
                     
                        if (results.Any())
                        {
                            response_obj.Add("results", results);
                            return this.Ok(Request, response_obj);
                        }
                        else
                        {
                            return this.NotFound(Request);
                        }
                    }
                    else
                    {
                        return this.NotAuthorized(Request);
                    }
                    #endregion
                }
                catch (Exception x)
                {
                    return this.Error(Request, x);
                }
            };

            Post[api_route] = _ =>
            {
                this.log.LogHttpPost(Request);

                try
                {
                    #region [ HTTP POST ]
                    Hashtable response_obj = new Hashtable();
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.resource.path"], Request.Path.Replace("/", "_"));

                    if (this.storage.Exists(resource_path))
                    {
                        if (this.auth.Authorized(Request, resource_path))
                        {
                            response_obj.Add("result",this.storage.Create(resource_path, Request));
                            return this.Created(Request, response_obj);
                        }
                        else
                        {
                            return this.NotAuthorized(Request);
                        }
                    }
                    else
                    {
                        if (this.auth.Authorized(Request, resource_path))
                        {
                            response_obj.Add("result", this.storage.Create(resource_path, Request));
                            return this.Created(Request, response_obj);
                        }
                        else
                        {
                            return this.NotAuthorized(Request);
                        }
                    }
                    #endregion
                }
                catch (Exception x)
                {
                    return this.Error(Request, x);
                }
            };

            Put[api_route] = _ =>
            {
                this.log.LogHttpPut(Request);

                try
                {
                    #region [ HTTP PUT ]
                    Hashtable response_obj = new Hashtable();
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.resource.path"], Request.Path.Replace("/", "_"));

                    if (this.storage.Exists(resource_path))
                    {
                        if (this.auth.Authorized(Request, resource_path))
                        {
                            response_obj.Add("result", this.storage.Update(resource_path, Request));
                            return this.Updated(Request, response_obj);
                        }
                        else
                        {
                            return this.NotAuthorized(Request);
                        }
                    }
                    else
                    {
                        return this.NotFound(Request);
                    }
                    #endregion
                }
                catch (Exception x)
                {
                    return this.Error(Request, x);
                }
            };

            Delete[api_route] = _ =>
            {
                this.log.LogHttpDelete(Request);

                try
                {
                    #region [ HTTP DELETE ]
                    Hashtable response_obj = new Hashtable();
                    string resource_path = String.Format("{0}/{1}", ConfigurationManager.AppSettings["api.resource.path"], Request.Path.Replace("/", "_"));

                    if (this.storage.Exists(resource_path))
                    {
                        if (this.auth.Authorized(Request, resource_path))
                        {
                            response_obj.Add("result", this.storage.Delete(resource_path, Request));
                            return this.Deleted(Request, response_obj);
                        }
                        else
                        {
                            return this.NotAuthorized(Request);
                        }
                    }
                    else
                    {
                        return this.NotFound(Request);
                    }
                    #endregion
                }
                catch (Exception x)
                {
                    return this.Error(Request, x);
                }
            };
        }

        private dynamic Updated(Nancy.Request Request, Hashtable response_obj)
        {
           this.log.LogUpdated(Request, response_obj);
           return Response.AsJson(response_obj, HttpStatusCode.OK); 
        }

        private dynamic Created(Nancy.Request Request, Hashtable response_obj)
        {
            this.log.LogCreated(Request, response_obj);
            return Response.AsJson(response_obj, HttpStatusCode.Created);
        }

        private dynamic Deleted(Nancy.Request Request, Hashtable response_obj)
        {
            this.log.LogDeleted(Request, response_obj);
            return Response.AsJson(response_obj, HttpStatusCode.Gone);
        }

        private dynamic Ok(Nancy.Request Request, Hashtable response_obj)
        {
            this.log.LogOk(Request);
            return Response.AsJson(response_obj, HttpStatusCode.OK); 
        }

        private dynamic Error(Nancy.Request Request, Exception x)
        {
            Hashtable response_obj = new Hashtable();
            Guid ErrorId = Guid.NewGuid();
            String Message = x.Message;
            response_obj.Add("error_id", ErrorId);
            response_obj.Add("Message", "An Error has occured");
            response_obj.Add("Details", x.Message);

            // log 
            this.log.LogError(Request, x ,ErrorId);

            return Response.AsJson(response_obj, HttpStatusCode.InternalServerError);
        }

        private dynamic NotFound(Nancy.Request Request)
        {
            Hashtable response_obj = new Hashtable();
            response_obj.Add("path", Request.Path);
            response_obj.Add("message", "Path does not exist.");

            this.log.LogNotFound(Request);

            return Response.AsJson(response_obj, HttpStatusCode.NotFound);
        }

        private dynamic NotAuthorized(Nancy.Request Request)
        {
            Hashtable response_obj = new Hashtable();

            this.log.LogUnAuthorized(Request);

            return Response.AsJson(response_obj, HttpStatusCode.Unauthorized);
        }
        #endregion
    }
}
