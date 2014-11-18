using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class SelectServices : RechargeServicePlanBasePage
    {
        #region Web Form Designer generated code

        protected ImageButton m_btnPrevious;
        protected ImageButton m_btnNext;
        protected Table m_tblPackage;
        protected Label m_lblPackageTotal;
        protected Label m_lblPackageName;
        protected Table m_tblFeatures;
        protected Footer _footer;
        protected PlaceHolder phldProductsTable;
        protected System.Web.UI.WebControls.PlaceHolder phldFeaturesTable;
        protected System.Web.UI.WebControls.Label m_lblTotalUpgrages;
        protected System.Web.UI.WebControls.Label m_lblGrandTotal;
        protected System.Web.UI.HtmlControls.HtmlGenericControl divMessage;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnMessage;
        protected ImageButton m_btnOnEnterStub;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();

            EnsureOneClickBehaviour(m_btnNext);
            EnsureOneClickBehaviour(m_btnPrevious);

            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.m_btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.OnPreviousClick);
            this.m_btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.OnNextClick);
            this.Load += new System.EventHandler(this.OnPageLoad);

        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e)
        {
            LoadProductsTable();
            LoadQuoteTable();

            if (!IsPostBack) {
                bool isPinAvailable = Model.IsPinAvailable();
                if (!isPinAvailable) {
                    divMessage.Style.Add("DISPLAY", "INLINE");
                    spnMessage.InnerText = "No pin is available for the selected products";
                    m_btnNext.Enabled = false;
                    m_btnNext.ImageUrl = "~/images/btn_proceed_disabled.gif";
                }
            }
        }

        #endregion

        #region OnPreviousClick

        private void OnPreviousClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.RDP_SELECT_PLAN_URL);
        }

        #endregion	    	    

        #region OnNextClick

        private void OnNextClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.RDP_ORDER_SUMMARY_URL);
        }

        #endregion

        #region LoadProductsTable

        private void LoadProductsTable()
        {
            IWireless_Products[] optionalProducts = Model.GetOptionalProducts(Model.SelectedMainProduct.Vendor_Name);

            IWireless_Products[] combinableProducts = ProductCompositionResolver.GetCombinableProducts(Map, Model.SelectedMainProduct, (IWireless_Products[])Model.SelectedOptionalProducts.ToArray(typeof(IWireless_Products)));

            ProductsTable productsTable = new ProductsTable(optionalProducts, Model.SelectedOptionalProducts, combinableProducts);
            productsTable.ID = "m_tblProducts";

            phldProductsTable.Controls.Clear();
            phldProductsTable.Controls.Add(productsTable);
        }

        #endregion

        #region LoadQuoteTable

        private void LoadQuoteTable()
        {
            decimal packageCost = LoadQuotePackageTable();
            decimal featuresCost = LoadFeaturesTable();

            m_lblGrandTotal.Text = "Grand Total: " + (packageCost + featuresCost).ToString("C");
        }

        #endregion

        #region LoadQuotePackageTable

        private decimal LoadQuotePackageTable()
        {                        
            m_lblPackageName.Text = Model.SelectedMainProduct.Vendor_Name;
            m_lblPackageTotal.Text = "Total: " + Model.SelectedMainProduct.Price.ToString("C");
            
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.CssClass = "midgray_normal";

            cell.Text = Model.SelectedMainProduct.Product_name;

            row.Cells.Add(cell);
            m_tblPackage.Rows.Add(row);//

            return Model.SelectedMainProduct.Price;
        }

        #endregion

        #region LoadFeaturesTable

        private decimal LoadFeaturesTable()
        {
            SelectedProductsTable featuresTable = new SelectedProductsTable(Model.SelectedOptionalProducts);
            featuresTable.ID = "m_tblFeatures";

            phldFeaturesTable.Controls.Clear();
            phldFeaturesTable.Controls.Add(featuresTable);

            m_lblTotalUpgrages.Text = "Total Upgrades: " + Model.GetSelectedOptionalProductsPrice().ToString("C");

            return Model.GetSelectedOptionalProductsPrice();
        }

        #endregion
    }
}