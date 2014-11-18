using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Web.Common;

namespace Dalworth.Server.Web.Restoration
{
    public partial class FooterControl : System.Web.UI.UserControl
    {
        private string m_text;
        public string Text
        {
            set { m_text = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            DalworthPage page = (DalworthPage)this.Page;

            m_imgDki.Attributes.Add("src", page.GetFullUrl("img/dki.png"));
            m_imgRia.Attributes.Add("src", page.GetFullUrl("img/ria.png"));
            m_imgBbb.Attributes.Add("src", page.GetFullUrl("img/bbb.png"));
            m_imgIicrc.Attributes.Add("src", page.GetFullUrl("img/inst.png"));
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnInit(e);
            m_txtText.Text = m_text;            
        }
    }
}