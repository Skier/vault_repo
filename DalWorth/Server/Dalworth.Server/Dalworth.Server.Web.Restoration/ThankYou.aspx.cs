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

using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;
using Dalworth.Server.Data;

namespace Dalworth.Server.Web.Restoration 
{
    public partial class ThankYou : DalworthPage
    {
        protected override WebSiteEnum WebSite
        {
            get { return WebSiteEnum.Restoration; }
        }

        protected override ProjectTypeEnum ProjectType
        {
            get { throw new NotImplementedException(); }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            m_latestArticles.ListName = "Latest Articles";
            m_latestArticles.DataSource = WebSiteArticle.GetWebSiteArticlePartWrapperWithBreadCrumTitle(null, 3, DbConnection);

            m_header.MenuItems = GetHeaderMenuItems(DbConnection, 0);

            List<WebSiteArticlePart> footerParts = WebSiteArticlePart.FindByArticleId(8, DbConnection);
            m_footer.Text = this.ReplaceUrl(footerParts[0].ContentText);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int leadId = int.Parse(Request.Params.Get("id"));
            Lead lead;

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                lead = Lead.FindByPrimaryKey(leadId,connection);
            }

            if (lead != null)
            {
                m_txtLeadId.Text = lead.ID.ToString();
                m_txtFirstName.Text = lead.FirstName;
                m_txtLastName.Text = lead.LastName;
                m_txtEmail.Text = lead.Email;
                m_txtPhone.Text = lead.Phone1;
                m_txtEmailAddress.Text = lead.Email.ToLower();
                m_txtMessageText.Value = lead.CustomerNotes;
            }
        }
    }
}
