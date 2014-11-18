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
    public partial class HeadControl : System.Web.UI.UserControl
    {
        private string m_title;
        public string Title
        {
            get { return this.m_title; }
            set { m_title = value; }
        }

        private string m_keyowrds;
        public string Keywords
        {
            get { return this.m_keyowrds; }
            set { m_keyowrds = value; }
        }

        private string m_description;
        public string Description
        {
            get { return this.m_description; }
            set { m_description = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            m_txtTitle.Text = Title;
            m_metaDescription.Attributes.Add("content", Description);
            m_metaKeywords.Attributes.Add("content", Keywords);

            string cssUrl = "/style.css";

            if (Request.ApplicationPath != null && Request.ApplicationPath != String.Empty && !Request.ApplicationPath.Equals("/"))
                cssUrl = Request.ApplicationPath + cssUrl;

            string port = string.Empty;
            if (Request.Url.Port != 80)
                port = ":" + Request.Url.Port;

            m_lnkCss.Attributes.Add("href", Request.Url.Scheme + "://" + Request.Url.Host + port + cssUrl);
        }
    }
}