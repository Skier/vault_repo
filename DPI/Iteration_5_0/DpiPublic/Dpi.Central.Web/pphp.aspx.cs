using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web
{
    public class PphpPage : BasePage
    {
        #region Web Form Designer generated code

        protected HyperLink m_lnkSignUpOnline;
        protected HtmlAnchor signUpLink;
        protected Footer _footer;

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.OnPageLoad);

        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e)
        {
            m_lnkSignUpOnline.Attributes.Add("onMouseOver", "MM_swapImage('Image43','','images/ready_to_b4_on.jpg',1)");
            m_lnkSignUpOnline.Attributes.Add("onMouseOut", "MM_swapImgRestore()");
            
            m_lnkSignUpOnline.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;

            signUpLink.HRef = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;
        }

        #endregion
    }
}