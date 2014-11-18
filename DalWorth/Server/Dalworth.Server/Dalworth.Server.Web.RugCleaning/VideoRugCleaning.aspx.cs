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
    public partial class VideoRugCleaning : RugCleaningPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Master.IsSlideShowRequired = true;
            Master.SlideshowType = RugCleaningMasterPage.SlideshowTypeEnum.Video_Rug_Cleaning;
            Master.IsBookmarkingRequired = true;
            Master.IsVideoRequired = false;
            Master.IsShortFormRequired = true;
        }
    }
}
