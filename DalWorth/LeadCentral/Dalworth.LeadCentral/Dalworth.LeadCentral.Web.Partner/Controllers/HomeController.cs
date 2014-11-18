using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Partner.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(string ticket, string logoutUrl)
        {
            // for testing only
            if (string.IsNullOrEmpty(ticket))
                ticket = BaseService.Init(null, "182801782", "bfit8895q");

            Session.Add("CurrentTicket", ticket);
            Session.Add("LogoutUrl", logoutUrl);
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
