using System;
using System.Web.UI;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless
{
    public class CustomerInfoViewer : UserControl
    {
        #region Web Form Designer generated code

        protected System.Web.UI.WebControls.Label lblFirstName;
        protected System.Web.UI.WebControls.Label lblAddress1;
        protected System.Web.UI.WebControls.Label lblLastName;
        protected System.Web.UI.WebControls.Label lblAddress2;
        protected System.Web.UI.WebControls.Label lblContactNumber;
        protected System.Web.UI.WebControls.Label lblCity;
        protected System.Web.UI.WebControls.Label lblEmail;
        protected System.Web.UI.WebControls.Label lblState;
        protected System.Web.UI.WebControls.Label lblZipCode;

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion

        #region Private Methods

        public void LoadFrom(IWireless_Custdata customerInfo)
        {
            if (customerInfo == null) {
                throw new ArgumentNullException("customerInfo");
            }

            lblFirstName.Text = customerInfo.NameFirst;
            lblLastName.Text = customerInfo.NameLast;
            lblContactNumber.Text = customerInfo.ContactNumber;
            lblAddress1.Text = customerInfo.Addr1;
            lblAddress2.Text = customerInfo.Addr2;
            lblCity.Text = customerInfo.City;
            lblEmail.Text = customerInfo.Email;
            lblZipCode.Text = customerInfo.Zip;
            lblState.Text = customerInfo.State;		
        }

        #endregion
    }
}