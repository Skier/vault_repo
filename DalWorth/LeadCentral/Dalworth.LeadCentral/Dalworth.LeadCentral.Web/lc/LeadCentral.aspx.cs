using System;

namespace Dalworth.LeadCentral.Web.lc
{
    public partial class LeadCentral : System.Web.UI.Page
    {
        protected string Ticket { get; set; }
        protected string RealmId { get; set; }
        protected string DbId { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            Ticket = Request.QueryString["Ticket"];
            RealmId = Request.QueryString["RealmId"];
            DbId = Request.QueryString["DbId"];
        }
    }
}