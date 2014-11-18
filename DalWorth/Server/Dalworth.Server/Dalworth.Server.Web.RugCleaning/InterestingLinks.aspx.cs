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
using Dalworth.Server.Web.Common;

namespace Dalworth.Server.Web.RugCleaning
{
    public partial class InterestingLinks : RugCleaningPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Master.IsSlideShowRequired = false;
            Master.IsBookmarkingRequired = true;
            Master.IsVideoRequired = true;
            Master.IsShortFormRequired = true;
            Master.SetLinkInvisible(RugCleaningMasterPage.LinkEnum.Interesting);
        }
    }
}
