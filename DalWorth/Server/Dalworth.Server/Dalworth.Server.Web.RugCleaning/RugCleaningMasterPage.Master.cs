using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.RugCleaning
{
 
    public partial class RugCleaningMasterPage: System.Web.UI.MasterPage
    {
        public enum SlideshowTypeEnum
        {
            Home,
            Process,
            Video_Rug_Cleaning
        }

        public enum LinkEnum
        {
            Process,
            Repairs,
            Emergency,
            HomeCare,
            RugPad,
            FiberProtector,
            MothRepellent,
            RugStorage,
            Interesting,
            PrivacyPolicy
        }

        private Dictionary<string, Label> m_errorLables = new Dictionary<string, Label>();

        public bool IsLinksVisible
        {
            set { m_dvLinks.Visible = false; }
            get { return m_dvLinks.Visible; }
        }

        public bool IsTestimonialVisible
        {
            set { m_dvTestinonial.Visible = value; }
            get { return m_dvTestinonial.Visible; }
        }

        private bool m_isSlideshowRequired = false;
        public bool IsSlideShowRequired
        {
            set {m_isSlideshowRequired = value;}
            get { return m_isSlideshowRequired; }
        }

        private bool m_isBookmarkingRequired = false;
        public bool IsBookmarkingRequired
        {
            set { m_isBookmarkingRequired = value; }
            get { return m_isBookmarkingRequired; }
        }

        private bool m_isVideoRequired = false;
        public bool IsVideoRequired
        {
            set { m_isVideoRequired = value; }
            get { return m_isVideoRequired; }
        }

        private bool m_isShortFormRequired = false;
        public bool IsShortFormRequired
        {
            set { m_isShortFormRequired = value; }
            get { return m_isShortFormRequired; }
        }

        private string m_callbackMessage;
        public string CallbackMessage
        {
            get { return m_callbackMessage;}
        }

        private SlideshowTypeEnum m_slideshowType;
        public SlideshowTypeEnum SlideshowType
        {
            set { m_slideshowType = value; }
            get { return m_slideshowType; }
        }

        private string m_phoneNumber;
        public string PhoneNumber
        {
            get { return m_phoneNumber; }
        }

        #region OnInit

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string offer = Request.Params.Get("offer");
            if (Session["offer"] == null
                && !string.IsNullOrEmpty(offer))
                Session["offer"] = offer;
                
            m_btn_Submit.ServerClick += btnSubmit_Click;

            m_errorLables.Add(Lead.FIELD_FIRSTNAME, m_lblErrorFirstName);
            m_errorLables.Add(Lead.FIELD_LASTNAME, m_lblErrorLastName);
            m_errorLables.Add(Lead.FIELD_PHONE1, m_lblErrorPhone1);
            m_errorLables.Add(Lead.FIELD_EMAIL, m_lblErrorEmail);

            if (Session["offer"] != null)
            {
                m_txtPromoCode.Value = (string) Session["offer"];
                m_txtPromoCode.Disabled = true;
            }

            DateTime now = DateTime.Now;

            if (now.DayOfWeek == DayOfWeek.Saturday || 
                now.DayOfWeek == DayOfWeek.Sunday ||
                now.TimeOfDay.Hours < 8 ||
                now.TimeOfDay.Hours > 17 
                )
            {
                    m_callbackMessage = "We will call you as soon as possible";
            }
            else
            {
                m_callbackMessage = "We will call you within <strong>10 minutes</strong>";
            }

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                SetLatestFeedback(connection);
                SetPhone(connection);       
            }
           
            m_lnkTestimonialReadMore.NavigateUrl = Link.CUSTOMER_REVIEWS;
        }

        #endregion

        public void SetLinkInvisible(LinkEnum link)
        {
            switch (link)
            {
                case LinkEnum.Emergency:
                    m_lnkEmergency.Visible = false;
                    break;
                case LinkEnum.FiberProtector:
                    m_lnkFiberProtector.Visible = false;
                    break;
                case LinkEnum.HomeCare:
                    m_lnkHomeCare.Visible = false;
                    break;
                case LinkEnum.Interesting:
                    m_lnkInteresting.Visible = false;
                    break;
                case LinkEnum.MothRepellent:
                    m_lnkMothRepellent.Visible = false;
                    break;
                case LinkEnum.Process:
                    m_lnkProcess.Visible = false;
                    break;
                case LinkEnum.Repairs:
                    m_lnkRepairs.Visible = false;
                    break;
                case LinkEnum.RugPad:
                    m_lnkRugPad.Visible = false;
                    break;
                case LinkEnum.RugStorage:
                    m_lnkRugStorage.Visible = false;
                    break;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            Lead lead = new Lead();

            object key = Session["key"];
            int? keywordId = null;
            if (key != null)
            {
                int result;
                string strKey = key.ToString();
                if (strKey.Length > 0 && int.TryParse(strKey, out result))
                    keywordId = result;
            }

            int? webLogId = null;
            if (Session["weblog_id"] != null)
                webLogId = (int) Session["weblog_id"];

            string promoCode = m_txtPromoCode.Value;

            string offer = string.Empty;
            if (Session["offer"] != null)
                offer = (string)Session["offer"];

            if (offer.Length > 0)
                promoCode = offer;

            Dictionary<string, string> errors = lead.Submit(1, 1, m_txtFirstName.Value, m_txtLastName.Value,
                m_txtPhone1.Value, m_txtEmail.Value, m_txtCustomerNotes.Value, promoCode, keywordId, webLogId);

            if (errors.Count > 0)
            {
                m_txtErrorMessage.Text = "Please correct errors";
                if (errors.ContainsKey(Lead.FIELD_SYSTEMERROR))
                    m_txtErrorMessage.Text += errors[Lead.FIELD_SYSTEMERROR];
                m_txtErrorMessage.Visible = true;

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

            Response.Redirect("~/QuoteRequestReceipt.aspx?id=" + lead.ID);
        }

        #region SetPhone

        private void SetPhone(IDbConnection connection)
        {
            WebSitePhone webSitePhone = null;
            string key = (string)Session["key"];
            if (!string.IsNullOrEmpty(key))
            {
                try
                {
                    webSitePhone = WebSitePhone.FindByPhoneKey("ppc", connection);
                }
                catch (DataNotFoundException)
                {
                }
            }
            
            if (Session["offer"] != null)
            {
                try
                {
                    webSitePhone = WebSitePhone.FindByPhoneKey((string)Session["offer"], connection);
                }
                catch (DataNotFoundException)
                {
                }
            }

            if (webSitePhone == null)
            {
                try
                {
                    webSitePhone = WebSitePhone.FindByPhoneKey("default", connection);
                }
                catch (DataNotFoundException)
                {
                }
            }

            m_phoneNumber = webSitePhone.PhoneNumber;
        }

        #endregion

        #region SetLatestFeedback

        private void SetLatestFeedback(IDbConnection connection)
        {
            BindingList<ProjectFeedbackWrapper> feedbacks =
                    ProjectFeedbackWrapper.FindApprovedFeedbacks(ProjectTypeEnum.RugCleaning,
                                                                 connection);
            m_txtTestimonialDate.Text = feedbacks[0].DatePosted;
            m_txtTestimonialCity.Text = feedbacks[0].City;
            m_txtTestomonialCustomerNote.Text = feedbacks[0].ProjectFeedback.EditedCustomerNote;
            m_txtTestimonialFirstLastName.Text = feedbacks[0].FirstLastName;

        }
        #endregion
    }
}
