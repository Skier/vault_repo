using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class SelectServices : BaseAccountSetupPage
    {
        #region Web Form Designer generated code

        protected ImageButton m_btnPrevious;
        protected ImageButton m_btnNext;
        protected Table m_tblPackage;
        protected Label m_lblPackageTotal;
        protected Label m_lblPackageName;
        protected Label m_lblTotalUpgrages;
        protected Label m_lblGrandTotal;
        protected Table m_tblFeatures;
        protected Footer _footer;
        protected PlaceHolder phldProductsTable;
        protected System.Web.UI.WebControls.PlaceHolder phldFeaturesTable;
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
        }

        #endregion

        #region OnPreviousClick

        private void OnPreviousClick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(SiteMap.NEW_ACC_SELECT_PACKAGE_URL);
        }

        #endregion	    	    

        #region OnNextClick

        private void OnNextClick(object sender, ImageClickEventArgs e)
        {
            Model.FillSelectedServices();
            Response.Redirect(SiteMap.NEW_ACC_ORDER_SUMMARY_URL);
        }

        #endregion

        #region LoadProductsTable

        private void LoadProductsTable()
        {
            ProductsTable productsTable = new ProductsTable(Model);
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
            m_lblPackageName.Text = Model.Info.Package.PackageName;
            m_lblPackageTotal.Text = "Total: " + Model.Info.Package.Package.UnitPrice.ToString("C");

            foreach (string feature in Model.Info.Package.Features) {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.CssClass = "midgray_normal";

                cell.Text = feature;

                row.Cells.Add(cell);
                m_tblPackage.Rows.Add(row);

            }

            if (Model.Info.Package.Price != Model.Info.Package.Package.UnitPrice) {
                TableRow row = new TableRow();
                TableCell cell = new TableCell();
                cell.CssClass = "midgray_normal";

                cell.Text = "Third Month Rate with prompt pay discount: " + Model.Info.Package.Price.ToString("C");

                row.Cells.Add(cell);
                m_tblPackage.Rows.Add(row);
            }

            return Model.Info.Package.Package.UnitPrice;
        }

        #endregion

        #region LoadFeaturesTable

        private decimal LoadFeaturesTable()
        {
            FeaturesTable featuresTable = new FeaturesTable(Model);
            featuresTable.ID = "m_tblFeatures";

            phldFeaturesTable.Controls.Clear();
            phldFeaturesTable.Controls.Add(featuresTable);

            decimal totalPrice = Model.GetQuoteTotal();
            m_lblTotalUpgrages.Text = "Total Upgrades: " + totalPrice.ToString("C");

            return totalPrice;
        }

        #endregion
    }
}