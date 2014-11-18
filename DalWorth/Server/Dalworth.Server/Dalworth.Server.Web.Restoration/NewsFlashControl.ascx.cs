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

namespace Dalworth.Server.Web.Restoration
{
    public partial class NewsFlashControl : System.Web.UI.UserControl
    {
        private string m_text;
        public string Text
        {
            set { m_text = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnInit(e);
            m_txtText.Text = m_text;
        }
    }
}