using System.Web.Mvc;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models.Dashboard;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ContextHelper.InitContext();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public ActionResult Index(FormCollection args)
        {
            ContextHelper.InitContext();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult Fault()
        {
            ContextHelper.Fault();

            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            var customer = ContextHelper.GetCurrentCustomer();
            if (!customer.IsCompanyProfileInited
                || !customer.IsCampaignsInited
                || !customer.IsOAuthInited
                || !customer.IsTrackingPhonesInited
                && ContextHelper.GetCurrentUser().IsAdmin())
                return RedirectToAction("Startup");

            var model = new DashboardModel();
            var currentUser = ContextHelper.GetCurrentUser();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(currentUser, connection);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Dashboard(DashboardModel model)
        {
            var customer = ContextHelper.GetCurrentCustomer();
            if (!customer.IsCompanyProfileInited
                || !customer.IsCampaignsInited
                || !customer.IsOAuthInited
                || !customer.IsTrackingPhonesInited
                && ContextHelper.GetCurrentUser().IsAdmin())
                return RedirectToAction("Startup");

            var user = ContextHelper.GetCurrentUser();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(user, connection);
            }
            return View(model);
        }

        public ActionResult Startup()
        {
            if (!ContextHelper.GetCurrentUser().IsAdmin())
                return RedirectToAction("Dashboard");

            var customer = ContextHelper.GetCurrentCustomer();
            if (customer.IsCompanyProfileInited
                && customer.IsCampaignsInited
                && customer.IsOAuthInited
                && customer.IsTrackingPhonesInited)
                return RedirectToAction("Dashboard");

            var model = new StartupModel();
            return View(model);
        }
    }
}
