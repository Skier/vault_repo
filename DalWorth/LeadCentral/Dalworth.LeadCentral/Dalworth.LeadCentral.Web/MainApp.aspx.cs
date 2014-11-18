using System;
using Dalworth.LeadCentral.SDK;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web
{
    public partial class MainApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var ticket = Request.QueryString["ticket"];

            var servmanCustomer = ServmanCustomerService.GetByTicketId(ticket);
            if (servmanCustomer == null)
                throw  new DalworthException("Ticket number is incorrect or expired");

            var realmId = servmanCustomer.RealmId;
            var dbId = servmanCustomer.AppDbId;

            Response.Redirect(string.Format("lc/LeadCentral.aspx?Ticket={0}&RealmId={1}&DbId={2}", ticket, realmId, dbId));
        }
    }
}