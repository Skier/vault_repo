using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Web.Common;

namespace Dalworth.Server.Web.RugCleaning
{
    public partial class QuoteRequestReceipt : RugCleaningPage
    {
        protected override void  OnInit(EventArgs e)
        {
 	        base.OnInit(e);
            Master.IsSlideShowRequired = false;
            Master.IsBookmarkingRequired = false;
            Master.IsVideoRequired = false;
            Master.IsShortFormRequired =false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int leadId = int.Parse(Request.Params.Get("id"));

            Lead lead = Lead.FindByPrimaryKey(leadId);

            if (lead != null)
            {
                m_txtLeadId.Text = lead.ID.ToString();
                m_txtFirstName.Text = lead.FirstName;
                m_txtLastName.Text = lead.LastName;
                m_txtEmail.Text = lead.Email;
                m_txtPhone.Text = lead.Phone1;
                m_txtEmailAddress.Text = lead.Email.ToLower();
                m_txtMessage.Text = lead.CustomerNotes;
                m_txtPromoCode.Text = lead.AdvertisingSourceAcronym;
            }
        }
    }
}