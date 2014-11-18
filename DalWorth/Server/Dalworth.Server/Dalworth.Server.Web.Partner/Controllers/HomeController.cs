using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Dalworth.Server.Domain;
using Dalworth.Server.Web.Partner.Models;
using Order = Dalworth.Server.Web.Partner.Models.Order;

namespace Dalworth.Server.Web.Partner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            if (!PartnerMembershipProvider.GetCurrentUser().IsOwner)
                return RedirectToAction("PartnerSummary");
            return RedirectToAction("CallLog");
        }

        [HttpGet]
        public ActionResult CallLog(CallLog callLog)
        {
            callLog.LoadLogItems();
            return View(callLog);
        }

        [HttpGet]
        public ActionResult CallLogComplete(CallLogComplete input)
        {
            input.LoadLogItems();
            return View(input);
        }

        public ActionResult TicketDetails(string ticketNumber)
        {
            int dummy;
            List<CallLogItem> calls = CallLogItem.Find(null, null, null, string.Empty, string.Empty,
                string.Empty, ticketNumber, null, null, string.Empty, string.Empty, string.Empty, null, false, false, null, null, false, out dummy);
            Order order = new Order(calls, ticketNumber);

            if (Request.UrlReferrer != null)
                order.BackLinkQueryString = Request.UrlReferrer.Query;

            return View(order);
        }

        public ActionResult CallDetail(string callId)
        {
            int digiumLogItemId;
            try
            {
                digiumLogItemId = int.Parse(callId);
            }
            catch (Exception)
            {
                return RedirectToAction("CallLog");
            }

            int dummy;
            List<CallLogItem> calls = CallLogItem.Find(digiumLogItemId, null, null, string.Empty, string.Empty,
                string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty, null, false, false, null, null, false, out dummy);
            if (calls.Count == 0)
                return RedirectToAction("CallLog");

            CallDetail callDetail = new CallDetail(calls[0]);
            if (Request.UrlReferrer != null)
                callDetail.BackLinkQueryString = Request.UrlReferrer.Query;

            return View(callDetail);
        }

        public ActionResult PartnerSummary(PartnerSummary summary)
        {
            if (summary == null)
                summary = new PartnerSummary();
            summary.LoadItems();
            return View(summary);
        }

        public ActionResult AccountingReport(AccountingReport reportModel)
        {
            if (reportModel == null)
                reportModel = new AccountingReport();
            reportModel.LoadItems();
            return View(reportModel);
        }
    }
}
