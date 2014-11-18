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
    public partial class CustomerReviews : RugCleaningPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Master.IsSlideShowRequired = false;
            Master.SlideshowType = RugCleaningMasterPage.SlideshowTypeEnum.Home;
            Master.IsBookmarkingRequired = true;
            Master.IsVideoRequired = true;
            Master.IsShortFormRequired = true;

            BindingList<ProjectFeedbackWrapper> projectFeedbacks = ProjectFeedbackWrapper.FindApprovedFeedbacks(ProjectTypeEnum.RugCleaning, DbConnection);

            m_repeater.DataSource = projectFeedbacks;
            m_repeater.DataBind();

        }
    }
}
