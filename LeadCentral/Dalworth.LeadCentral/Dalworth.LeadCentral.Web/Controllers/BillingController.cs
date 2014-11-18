using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models;
using Dalworth.LeadCentral.Web.Models.Billing;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class BillingController : Controller
    {
        //
        // GET: /Billing/

        public ActionResult Index()
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };
            if (ContextHelper.ActionDenied(allowedRoles))
            {
                Flash.GetCurrent().PushError(string.Format("You havn't access to this resource."));
                return RedirectToAction("Dashboard", "Home");
            }

            var model = new TransactionList();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                model.Load(connection);
            }
            return View(model);
        }

    }
}
