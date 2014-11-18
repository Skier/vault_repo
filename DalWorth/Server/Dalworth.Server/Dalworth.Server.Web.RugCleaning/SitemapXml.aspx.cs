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
using System.ComponentModel;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.RugCleaning
{
    public partial class SitemapXml : RugCleaningPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Response.ContentType = "text/xml";

            BindingList<ProjectFeedbackWrapper> feedbacks =  ProjectFeedbackWrapper.FindApprovedFeedbacks(ProjectTypeEnum.RugCleaning, 1, DbConnection);
            m_txtHomeDateCreated.Text = feedbacks[0].DatePosted;
            m_txtTestimonialsDateCreated.Text = feedbacks[0].DatePosted;
        }
    }
}
