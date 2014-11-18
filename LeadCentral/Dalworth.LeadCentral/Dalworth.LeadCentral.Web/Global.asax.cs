using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Web.Controllers;
using Configuration = Dalworth.Common.SDK.Configuration;

namespace Dalworth.LeadCentral.Web
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            LoadConfiguration();
        }

        /*protected void Application_Error(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            var ex = ctx.Server.GetLastError();
            ctx.Response.Clear();

            var rc = ((MvcHandler)ctx.CurrentHandler).RequestContext;
            IController controller = new HomeController(); 
            var context = new ControllerContext(rc, (ControllerBase)controller);

            var viewResult = new ViewResult();

            if (ex is DalworthException)
                viewResult.ViewName = "DalworthError";
            else
                viewResult.ViewName = "Error";

            viewResult.ViewData.Model = new HandleErrorInfo(ex, context.RouteData.GetRequiredString("controller"), context.RouteData.GetRequiredString("action"));
            viewResult.ExecuteResult(context);
            ctx.Server.ClearError();
        }*/

        private static void LoadConfiguration ()
        {
            Configuration.LeadCentralCommon.CallUrl =  ConfigurationManager.AppSettings["callUrl"];
        }
    }
}