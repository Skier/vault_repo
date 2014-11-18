using System.Web.Mvc;
using Dalworth.LeadCentral.SDK;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Partner.Models;

namespace Dalworth.LeadCentral.Web.Partner.Controllers
{
    public class LeadsController : Controller
    {
        //
        // GET: /Leads/

        [HttpGet]
        public ActionResult Index()
        {
            var ticket = (string)Session["CurrentTicket"];
            if (string.IsNullOrEmpty(ticket))
                throw new DalworthException("Ticket is empty");

            var user = BaseService.GetCurrentUser(ticket);
            if (user == null)
                throw new DalworthException("Ticket is incorrect");

            var model = (LeadsList)Session["CurrentModel"];
            
            if (model == null)
            {
                model = new LeadsList();
                model.LoadLeadItems(user);

                Session.Add("CurrentModel", model);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(LeadsList model)
        {


            return View(model);
        }
    }
}
