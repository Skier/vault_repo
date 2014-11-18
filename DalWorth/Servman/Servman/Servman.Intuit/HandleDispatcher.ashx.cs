using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servman.Intuit
{
    /// <summary>
    /// Summary description for HandleDispatcher
    /// </summary>
    public class HandleDispatcher : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}