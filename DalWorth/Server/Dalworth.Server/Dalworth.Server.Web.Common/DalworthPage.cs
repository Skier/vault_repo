using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using Dalworth.Server.Domain;
using Dalworth.Server.Data;

namespace Dalworth.Server.Web.Common
{
    public abstract class DalworthPage : Page
    {
        public class HeaderMenuItem
        {
            public int Id;
            public string Name;
            public int WebSiteArticleId;
            public bool IsHighlighted;
            public bool isSelected;
            public WebSiteArticle Article;
        }

        public enum WebSiteEnum
        {
            RugCleaning,
            Restoration
        }

        #region properties

        private IDbConnection m_dbConnection;
        public IDbConnection DbConnection
        {
            get { return m_dbConnection; }
        }

        private int m_articleId;
        public int ArticleId
        {
            get { return m_articleId; }
        }

        protected abstract ProjectTypeEnum ProjectType { get; }

        protected abstract WebSiteEnum WebSite { get;}

        protected virtual string PromoCode
        {
            get { return null; }
        }

        protected virtual string FirstName
        {
            get { return null; }
        }

        protected virtual string LastName
        {
            get { return null; }
        }

        protected virtual string Phone1
        {
            get { return null; }
        }

        protected virtual string Email
        {
            get { return null; }
        }

        protected virtual string CustomerNotes
        {
            get { return null; }
        }

        protected virtual Label ErrorMessage
        {
            get { return null; }
        }

        protected virtual Label ErrorFirstName
        {
            get { return null; }
        }

        protected virtual Label ErrorLastName
        {
            get { return null; }
        }

        protected virtual Label ErrorPhone1
        {
            get { return null; }
        }

        protected virtual Label ErrorEmail
        {
            get { return null; }
        }

        #endregion 

        private Dictionary<string, Label> m_errorLables = new Dictionary<string, Label>();

        #region page events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            EnableViewState = false;

            string strArticleId = Request.Params.Get("articleid");

            if (strArticleId != null)
                m_articleId = int.Parse(strArticleId);
            m_dbConnection = Connection.Instance.GetTemporaryDbConnection();
            m_dbConnection.Open();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string key = Request.Params.Get("key");
            if (key != null && key.Length > 0)
                Session["key"] = key;

            string keyword = Request.Params.Get("keyword");
            if (keyword == null)
                keyword = string.Empty;
            
            try
            {
                int? intKey = null;
                if (key != null && key.Length > 0)
                    intKey = int.Parse(key);

                if (Session["session_id"] == null)
                {
                    WebLog webLog = new Dalworth.Server.Domain.WebLog();

                    switch (WebSite)
                    {
                        case WebSiteEnum.RugCleaning:
                            webLog.WebSiteId = 1;
                            break;
                        case WebSiteEnum.Restoration:
                            webLog.WebSiteId = 2;
                            break;
                    }

                    webLog.DateCreated = DateTime.Now;
                    webLog.Keyword = keyword;
                    webLog.KeywordId = intKey;
                    webLog.URL = Request.Url.AbsoluteUri;

                    if (Request.UrlReferrer != null)
                        webLog.ReferrerHost = Request.UrlReferrer.AbsoluteUri;
                    else
                        webLog.ReferrerHost = string.Empty;

                    webLog.SessionId = "-1";
                    WebLog.Insert(webLog, DbConnection);
                    webLog.SessionId = webLog.ID.ToString();
                    WebLog.Update(webLog, DbConnection);
                    Session["session_id"] = webLog.ID;
                    Session["weblog_id"] = webLog.ID;
                }
                else
                {
                    WebLog webLog = new WebLog();

                    switch (WebSite)
                    {
                        case WebSiteEnum.RugCleaning:
                             webLog.WebSiteId = 1;
                             break;
                        case WebSiteEnum.Restoration:
                            webLog.WebSiteId = 2;
                            break;
                    }

                    webLog.DateCreated = DateTime.Now;
                    webLog.Keyword = keyword;
                    webLog.KeywordId = intKey;
                    webLog.URL = Request.Url.AbsolutePath;

                    if (Request.UrlReferrer != null)
                        webLog.ReferrerHost = Request.UrlReferrer.AbsoluteUri;
                    else
                        webLog.ReferrerHost = string.Empty;

                    webLog.SessionId = Session["session_id"].ToString();
                    WebLog.Insert(webLog, DbConnection);
                    Session["weblog_id"] = webLog.ID;
                }               
            }
            catch (Exception ex)
            { }
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            if (m_dbConnection != null)
                m_dbConnection.Dispose();
        }

        #endregion 

        #region Helper Methods

        public string GetFullUrl(string url)
        {
            string port = string.Empty;
            if (Request.Url.Port != 80)
                port = ":" + Request.Url.Port;

            string fullUrl = Request.Url.Scheme + "://" + Request.Url.Host + port;

            if (Request.ApplicationPath != null && Request.ApplicationPath != String.Empty && !Request.ApplicationPath.Equals("/"))   
                fullUrl += Request.ApplicationPath + "/" + url;
            else
                fullUrl += "/" + url;

            return fullUrl;
        }

        public string ReplaceUrl(string text)
        {
            int idxStart = text.IndexOf("<URL>");

            while (idxStart > 0)
            {
                int idxEnd = text.IndexOf("</URL>", idxStart);
                string result = text.Substring(idxStart + 5, idxEnd - idxStart - 5);

                int articleId;

                if (!int.TryParse(result, out articleId))
                {
                    return text;
                }

                WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(articleId, DbConnection);

                text = text.Replace("<URL>" + result + "</URL>", GetFullUrl(article.Url));

                idxStart = text.IndexOf("<URL>");
            }

            return text;
        }

        protected void OnLeadSubmitted(object sender, EventArgs e)
        {
            m_errorLables.Add(Lead.FIELD_FIRSTNAME, ErrorFirstName);
            m_errorLables.Add(Lead.FIELD_LASTNAME, ErrorLastName);
            m_errorLables.Add(Lead.FIELD_PHONE1, ErrorPhone1);
            m_errorLables.Add(Lead.FIELD_EMAIL, ErrorEmail);

            foreach (Label label in m_errorLables.Values)
            {
                label.Text = string.Empty;
                label.Visible = false;
            }

             if (!Page.IsValid)
                return;

            Lead lead = new Lead();

            
            int? webLogId = null;
            if (Session["weblog_id"] != null)
                webLogId = (int) Session["weblog_id"];

            string promoCode = PromoCode;

            string offer = string.Empty;
            if (Session["offer"] != null)
                offer = (string)Session["offer"];

            if (offer.Length > 0)
                promoCode = offer;

            int? projectTypeId = null;
            int businessPartnerId = 1;
            
            switch (WebSite)
            {
                case WebSiteEnum.RugCleaning:
                    projectTypeId = (int)ProjectType;
                    if (projectTypeId == 0)
                        projectTypeId = null;
                        
                    businessPartnerId = 1;
                    break;
                case WebSiteEnum.Restoration:
                    projectTypeId = (int)ProjectType;
                    if (projectTypeId == 0)
                        projectTypeId = null;
                    
                    businessPartnerId = 4;
                    break;
            }

            Dictionary<string, string> errors = lead.Submit(projectTypeId, businessPartnerId, FirstName, LastName,
                Phone1, Email, CustomerNotes, promoCode, null, webLogId, DbConnection);

            if (errors.Count > 0)
            {
                ErrorMessage.Text = "Please correct errors";
                if (errors.ContainsKey(Lead.FIELD_SYSTEMERROR))
                    ErrorMessage.Text += errors[Lead.FIELD_SYSTEMERROR];
                ErrorMessage.Visible = true;

                foreach (KeyValuePair<string, string> kvp in errors)
                {
                    string fieldName = string.Empty;

                    switch (kvp.Key)
                    {
                        case Lead.FIELD_FIRSTNAME:
                            fieldName = "First Name ";
                            break;
                        case Lead.FIELD_LASTNAME:
                            fieldName = "Last Name ";
                            break;
                        case Lead.FIELD_PHONE1:
                            fieldName = "Phone ";
                            break;
                        case Lead.FIELD_EMAIL:
                            fieldName = "Email ";
                            break;
                        case Lead.FIELD_ADVERTISING_SOURECE_ACRONYM:
                            fieldName = "Promo code ";
                            break;
                    }

                    if (m_errorLables.ContainsKey(kvp.Key))
                    {
                        Label errorLabel = m_errorLables[kvp.Key];

                        errorLabel.Visible = true;
                        errorLabel.Text = fieldName + kvp.Value;
                    }
                }

                return;
            }

            Response.Redirect("~/thank-you.html?id=" + lead.ID);
        }

        protected List<HeaderMenuItem> GetHeaderMenuItems(IDbConnection connection, int selectedMenuId)
        {
            List<HeaderMenuItem> menuItems = new List<HeaderMenuItem>();

            List<WebSiteArticlePart> parts = WebSiteArticlePart.FindByArticleId(10, connection);

            string config = parts[0].ContentText;

            string[] strMenuItems = config.Split('\n');

            foreach (string str in strMenuItems)
            {
                string[] fields = str.Split(',');

                if (fields.Length == 0)
                    continue;

                HeaderMenuItem menuItem = new HeaderMenuItem();

                menuItem.Id = int.Parse(fields[0]);
                menuItem.Name = fields[1];
                menuItem.WebSiteArticleId = int.Parse(fields[2]);
                menuItem.IsHighlighted = bool.Parse(fields[3]);

                if (menuItem.Id == selectedMenuId)
                    menuItem.isSelected = true;
                else
                    menuItem.isSelected = false;

                WebSiteArticle article = null;
                if (menuItem.WebSiteArticleId != 0)
                    menuItem.Article = WebSiteArticle.FindByPrimaryKey(menuItem.WebSiteArticleId, connection);

                menuItems.Add(menuItem);
            }

            return menuItems;
        }
        public string ReplacePhoneNumber(string text)
        {
            return text.Replace("PHONE_NUMBER", "1 800 326 7913");
        }
        #endregion
    }
}