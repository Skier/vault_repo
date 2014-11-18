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
using System.ComponentModel;

using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Restoration
{
    public partial class LatestArticlesControl : System.Web.UI.UserControl
    {
        private string m_listName;
        public string ListName
        {
            set { m_listName = value; }
        }

        private BindingList<WebSiteArticlePartWrapper> m_dataSource;
        public BindingList<WebSiteArticlePartWrapper> DataSource
        {
            set { m_dataSource = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            m_txtArticleName.Text = m_listName;
            m_repeater.DataSource = m_dataSource;
            m_repeater.DataBind();
        }
            
    }
}