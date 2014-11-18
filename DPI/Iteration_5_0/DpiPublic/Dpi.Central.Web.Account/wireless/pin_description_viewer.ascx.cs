using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless
{
    public class PinDescriptionViewer : UserControl
    {
        #region Web Form Designer generated code

        protected Label lblProd1;
        protected Label lblProd2;
        protected Label lblProd3;
        protected Label lblProd4;
        protected Label lblProd5;
        protected Label lblProd6;
        protected Label lblProd0;

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

        public void LoadFrom(IWireless_Products[] products)
        {
            if (products == null) {
                throw new ArgumentNullException("products");
            }
            
            
            
            lblProd0.Text = products.Length > 0 ? products[0].Product_name : string.Empty;
            lblProd1.Text = products.Length > 1 ? products[1].Product_name : string.Empty;
            lblProd2.Text = products.Length > 2 ? products[2].Product_name : string.Empty;
            lblProd3.Text = products.Length > 3 ? products[3].Product_name : string.Empty;
            lblProd4.Text = products.Length > 4 ? products[4].Product_name : string.Empty;
            lblProd5.Text = products.Length > 5 ? products[5].Product_name : string.Empty;
            lblProd6.Text = products.Length > 6 ? products[6].Product_name : string.Empty;
        }
    }
}