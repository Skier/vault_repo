using System;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web
{
    public class TermsAndConditions : BaseAccountPage
    {
        #region Web Form Designer generated code


        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region EventHandlers

        private void Page_Load(object sender, System.EventArgs e) 
        {
        }

        #endregion

    }
}