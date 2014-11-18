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

namespace Dalworth.Server.Web.RugCleaning
{
    public partial class Error : RugCleaningPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Master.IsSlideShowRequired = false;
            Master.SlideshowType = RugCleaningMasterPage.SlideshowTypeEnum.Home;
            Master.IsBookmarkingRequired = false;
            Master.IsVideoRequired = false;
            Master.IsShortFormRequired = false;
        }
    }
}
