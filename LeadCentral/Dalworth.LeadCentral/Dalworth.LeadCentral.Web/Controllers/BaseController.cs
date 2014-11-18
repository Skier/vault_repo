using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalworth.LeadCentral.Web.Controllers
{
    public class BaseController : Controller
    {
        protected bool CanEditLeads { get; set; }
        protected bool CanListenRecords { get; set; }
        protected bool CanMatchInvoices { get; set; }
    }
}
