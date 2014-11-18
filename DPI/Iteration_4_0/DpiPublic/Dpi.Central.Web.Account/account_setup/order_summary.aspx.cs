using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.ClientComp;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
    public class OrderSummaryPage : BaseAccountSetupPage
    {
        #region Classes

        public class TableTaxSum : Table
        {
            public TableTaxSum(IOrderSum orderSummary) : base()
            {
                BorderWidth = 1;
                CellPadding = 0;
                CellSpacing = 0;
                BorderColor = Color.Gainsboro;
                GridLines = GridLines.Both;
                Font.Size = 10;
                Font.Bold = true;
                Width = Unit.Percentage(100);

                Rows.Add(new TaxHeaderRow());
                Rows.Add(new TaxRow("Sales Tax", orderSummary.GetTaxAmt(1), orderSummary.GetTaxAmt(2), Color.WhiteSmoke));
            }
        }

        public class TaxHeaderRow : TableRow 
        {
            public TaxHeaderRow() : base() 
            {
                BackColor = Color.Chocolate;

                TableCell cell = new TableCell();
                cell.Text = "&nbsp;&nbsp;Taxes";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Font.Bold = true;
                cell.ForeColor = Color.White;
                cell.Width = Unit.Percentage(52);
                Cells.Add(cell);

                cell = new TableCell();
                cell.Font.Bold = true;
                cell.Text = "";
                cell.ForeColor = Color.White;
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Width = Unit.Percentage(12);
                Cells.Add(cell);

                cell = new TableCell();
                cell.Font.Bold = true;
                cell.Text = "";
                cell.ForeColor = Color.White;
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Width = Unit.Percentage(18);
                Cells.Add(cell);

                cell = new TableCell();
                cell.Font.Bold = true;
                cell.ForeColor = Color.White;
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Width = Unit.Percentage(18);
                Cells.Add(cell);
            }
        }

        public class TaxRow : TableRow
        {
            private const string CSS_CLASS = "subitems";

            public TaxRow(string name, decimal value1, decimal value2, Color back) : base()
            {
                BackColor = back;

                // Name cell.
                TableCell cell = new TableCell();
                cell.CssClass = CSS_CLASS;
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.Text = "&nbsp;&nbsp;" + name;
                Cells.Add(cell);

                // Value 1 cell.
                cell = new TableCell();
                cell.ForeColor = Color.Red;
                cell.CssClass = CSS_CLASS;
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Text = value1.ToString("C") + "&nbsp;&nbsp;";
                Cells.Add(cell);

                // Value 2 cell.
                cell = new TableCell();
                cell.ForeColor = Color.Red;
                cell.CssClass = CSS_CLASS;
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Text = value2.ToString("C") + "&nbsp;&nbsp;";
                Cells.Add(cell);

                cell = new TableCell();
                cell.ForeColor = Color.SlateGray;
                cell.CssClass = CSS_CLASS;
                cell.HorizontalAlign = HorizontalAlign.Right;
                Cells.Add(cell);
            }
        }

        #endregion

        #region Constants

        private const int MONTH_NUMBER = 9;

        #endregion

        #region Web Form Designer generated code

        protected ImageButton btnPrevious;
        protected ImageButton btnNext;
        protected Label lblOrderSummary;
        protected Label lblZipCode;
        protected PlaceHolder phldrOrdrDetails;
        protected Label lblOrderTotalM2;
        protected Label lblOrderTotal;
        protected Label Label1;
        protected Label Label2;
        protected Label Label3;
        protected Label Label4;
        protected PlaceHolder phldrTaxes;
        protected PlaceHolder phldrMonthChart;
        protected Dpi.Central.Web.Controls.Footer _footer;
        protected System.Web.UI.WebControls.ImageButton btnTaxDetails;
        protected System.Web.UI.WebControls.PlaceHolder phldrTaxDetails;
        protected System.Web.UI.WebControls.Label lblAmountDue1;
        protected System.Web.UI.WebControls.Label lblAmountDue2;
        protected System.Web.UI.WebControls.ImageButton btnMonthChart;

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
            this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.OnPreviousClick);
            this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.OnNextClick);
            this.Load += new System.EventHandler(this.OnPageLoad);

        }

        #endregion

        #region Event Handler

        private void OnPageLoad(object sender, EventArgs e)
        {
            LoadOrderSummary();
        }

        private void OnPreviousClick(object sender, ImageClickEventArgs e)
        {
            Model.ResetVisitedGroups();
            Response.Redirect(SiteMap.NEW_ACC_SELECT_SERVICES_URL);
        }

        private void OnNextClick(object sender, ImageClickEventArgs e)
        {
            if (Model.Info.IsMoveExistingPhone) {
                Response.Redirect(SiteMap.NEW_ACC_TPV_AGREEMENT_URL);
            } else {
                Response.Redirect(SiteMap.NEW_ACC_SERVICE_ADDRESS_URL);
            }
        }

        private void OnRowCreated(object sender, ProductSummaryTable.RowCreatedEventArgs e)
        {
            IProdPrice product = e.Row.Product;

            bool found = false;
            foreach (IProdPrice service in Model.Info.SelectedServices) {
                if (service.ProdId == product.ProdId) {
                    found = true;
                    break;
                }
            }

            if (!found) {
                e.Row.ModifyLinkButton.Visible = e.Row.RemoveLinkButton.Visible = false;
            } else {
                if (product.ProdType.ToUpper() == "LOCAL SERVICE") {
                    e.Row.RemoveLinkButton.Visible = false;
                } else {
                    e.Row.RemoveLinkButton.Click += new EventHandler(OnRemoveLinkButtonClick);
                    e.Row.RemoveLinkButton.Attributes.Add("OnClick", "return ConfirmAction();");
                }

                e.Row.ModifyLinkButton.Click += new EventHandler(OnModifyLinkButtonClick);
            }
        }

        private void OnModifyLinkButtonClick(object sender, EventArgs e)
        {
            Model.ResetVisitedGroups();

            IProdPrice product = ((ProductSummaryTableRow)((LinkButton)sender).Parent.Parent).Product;
            if (product.ProdType.ToUpper() == "LOCAL SERVICE") {
                Model.Info.OrderSummary = null;
                Response.Redirect(SiteMap.NEW_ACC_SELECT_PACKAGE_URL);
            } else {
                int productGroupNumber = Model.GetProductGroupNumber(product.ProdId);
                Model.Info.CurrentServiceGroup = productGroupNumber;
            
                Response.Redirect(SiteMap.NEW_ACC_SELECT_SERVICES_URL);
            }
        }

        private void OnRemoveLinkButtonClick(object sender, EventArgs e) 
        {
            IProdPrice product = ((ProductSummaryTableRow)((LinkButton)sender).Parent.Parent).Product;
            Model.RemoveService(product.ProdId);
            LoadOrderSummary();
        }

        #endregion

        #region Implementation

        private void LoadOrderSummary()
        {
            Model.EnsureOrderSummary();

            IOrderSum orderSummary = Model.Info.OrderSummary;

            // Zip.
            lblZipCode.Text = string.Format(ZIP_CODE_FORMAT, Model.Info.Zip, Model.Info.Provider.ILECName);

            // Product table.
            if (orderSummary.Products != null) {
                ProductSummaryTable productTable = new ProductSummaryTable(orderSummary.Products, new ProductSummaryTable.RowCreatedEventHandler(OnRowCreated));
                phldrOrdrDetails.Controls.Clear();
                phldrOrdrDetails.Controls.Add(productTable);
            }

            // Product total.
            lblOrderTotal.Text = orderSummary.GetProdSubTotal(1).ToString("C");
            lblOrderTotalM2.Text = orderSummary.GetProdSubTotal(2).ToString("C");

            // Tax table.
            Table taxTable = new TableTaxSum(orderSummary);
            phldrTaxes.Controls.Clear();
            phldrTaxes.Controls.Add(taxTable);

            // Total amount.
            decimal amountDue = orderSummary.GetTotalAmtDue(1);
            lblAmountDue1.Text = decimal.Round(amountDue, 2).ToString("C");
            amountDue = orderSummary.GetTotalAmtDue(2);
            lblAmountDue2.Text = decimal.Round(amountDue, 2).ToString("C");

            // Payment Forecast Table.
            Table forecastTable = new TableMonthChart(orderSummary, MONTH_NUMBER);
            phldrMonthChart.Controls.Clear();
            phldrMonthChart.Controls.Add(forecastTable);

            btnMonthChart.Attributes.Add("onclick", "TogglePanel('forecastTable', 1,'" + btnMonthChart.ClientID + "');return false;");

            // TaxDetails
            string[] lines = orderSummary.GetSumTaxDesc(1);
            TaxDetailsTable taxDetailsTable1 = new TaxDetailsTable(lines, 1);
            
            phldrTaxDetails.Controls.Clear();
            phldrTaxDetails.Controls.Add(taxDetailsTable1);

            btnTaxDetails.Attributes.Add("onclick", "TogglePanel('taxDetailsTable', 1,'" + btnTaxDetails.ClientID + "');return false;");
        }

        #endregion

        #region Test Staff

        internal override AccountSetupModel Model 
        {
            get 
            {
                if (Mode == OperatingMode.Test) {
                    if (Session["AccountSetupModelTest"] == null) {
                        AccountSetupModel model = new TestModel(Map);
                        model.Info.SelectedServices = GetTestSelectedServices();
                        model.Info.Zip = "29115";
                        model.Info.Provider = new TestILECInfo();
                        Session["AccountSetupModelTest"] = model;
                    }

                    return (AccountSetupModel) Session["AccountSetupModelTest"];
                }

                return base.Model;
            }
        }

        internal class TestTaxInfo : ITaxInfo
        {
            Random _rnd = new Random(unchecked((int) DateTime.Now.Ticks));

            public string TaxId
            {
                get { return _rnd.Next(100).ToString(); }
                set { }
            }

            public string TaxCode
            {
                get { return _rnd.Next(100).ToString(); }
            }

            public int TaxProd
            {
                get { return _rnd.Next(100); }
                set { }
            }

            public decimal TaxAmount
            {
                get { return _rnd.Next(100); }
                set { }
            }

            public string Description
            {
                get { return "Description" + _rnd.Next(100).ToString(); }
            }
        }

        internal class TestOrderSummary : IOrderSum
        {
            private Random _rnd;
            private IProdPrice[] _products;

            public TestOrderSummary(IProdPrice[] products)
            {
                _rnd = new Random(unchecked((int) DateTime.Now.Ticks));
                _products = products;
            }

            public IDmdItem[] Items
            {
                get { return null; }
            }

            public IDemand Demand
            {
                get { return null; }
            }

            public IProdPrice[] Products
            {
                get { return _products; }
            }

            #region Methods

            public decimal GetTotalAmtDue(int month)
            {
                return _rnd.Next(100);
            }

            public decimal GetProdSubTotal(int month)
            {
                return _rnd.Next(100);
            }

            private string[] ProdSubRow(int months)
            {
                string[] row = new string[months + 1];
                row[0] = "Subtotal Product";

                for (int i = 1; i < months + 1; i++) {
                    row[i] = GetProdSubTotal(i).ToString();
                }

                return row;
            }

            private string[] TotalRow(int months)
            {
                string[] row = new string[months + 1];
                row[0] = "Total";
                for (int i = 1; i < months + 1; i++) {
                    row[i] = this.GetTotalAmtDue(i).ToString();
                }

                return row;
            }

            private string[] FeesTaxesRow(int months)
            {
                string[] row = new string[months + 1];
                row[0] = "Taxes, Fees and Surcharges";
                for (int i = 1; i < months + 1; i++) {
                    row[i] = GetTaxAmt(i).ToString();
                }

                return row;
            }

            public string[][] GetMonthlySummary(int months)
            {
                int count = _rnd.Next(22, 25);
                string[][] res = new string[count][];

                for (int i = 0; i < count; i++) {
                    string[] col = new string[months + 1];
                    if (i == 4) {
                        col[0] = "Product Name " + (i - 1);
                        for (int j = 5; j < months + 1; j++) {
                            col[j] = _rnd.Next(100).ToString("C");
                        }
                    } else {
                        col[0] = "Product Name " + i;
                        for (int j = 1; j < months + 1; j++) {
                            col[j] = _rnd.Next(100).ToString("C");
                        }
                    }

                    res[i] = col;
                }

                res[res.Length - 3] = ProdSubRow(months);
                res[res.Length - 2] = FeesTaxesRow(months);
                res[res.Length - 1] = TotalRow(months);

                return res;
            }

            public IDmdItem[] GetProducts(int month)
            {
                throw new NotImplementedException();
            }

            public IDmdItem[] GetProducts()
            {
                throw new NotImplementedException();
            }

            public string[][] GetMonthlySummary()
            {
                throw new NotImplementedException();
            }

            public decimal GetProdSubTotal()
            {
                return _rnd.Next(100);
            }

            public decimal GetTotalAmtDue()
            {
                return _rnd.Next(100);
            }

            public decimal GetProdAmt(int month)
            {
                return _rnd.Next(100);
            }

            public decimal GetProdAmt()
            {
                return _rnd.Next(100);
            }

            public decimal GetFeeAmt(int month)
            {
                return _rnd.Next(100);
            }

            public decimal GetFeeAmt()
            {
                return _rnd.Next(100);
            }

            public ITaxInfo[] GetTaxes(int month)
            {
                return new ITaxInfo[] {new TestTaxInfo(), new TestTaxInfo(), new TestTaxInfo(), new TestTaxInfo()};
            }

            public ITaxInfo[] GetTaxSummary(int month)
            {
                return new ITaxInfo[] {new TestTaxInfo(), new TestTaxInfo(), new TestTaxInfo(), new TestTaxInfo()};
            }

            public ITaxInfo[] GetTaxes()
            {
                return GetTaxes(1);
            }

            public ITaxInfo[] GetTaxSummary()
            {
                return GetTaxSummary(1);
            }

            public decimal GetTaxAmt(int month)
            {
                return _rnd.Next(100);
            }

            public decimal GetTaxAmt(int prod, int month)
            {
                return _rnd.Next(100);
            }

            public decimal GetTaxAmt()
            {
                return GetTaxAmt(1);
            }

            public string[] GetSumTaxDesc(int month)
            {
                string[] taxes = new string[_rnd.Next(10)];

                for (int i = 0; i < taxes.Length; i++)
                    taxes[i] = GetDescrWithAmt(i);

                return taxes;
            }

            string GetDescrWithAmt(int i) 
            {
                return "Tax Test #" + i + "  " + _rnd.Next(100).ToString("C");
            }

            public string[] GetSumTaxDesc()
            {
                return GetSumTaxDesc(1);
            }

            #endregion
        }

        internal class TestILECInfo : IILECInfo
        {
            public int OrgId
            {
                get { return 12; }
            }

            public string ILECCode
            {
                get { return "1234"; }
            }

            public string ILECName
            {
                get { return "BellSouth"; }
            }

            public bool IsDefault
            {
                get { return false; }
            }
        }

        internal class TestModel : AccountSetupModel
        {
            public TestModel(IMap map) : base(map) {}
            public override void EnsureOrderSummary()
            {
                Info.OrderSummary = new TestOrderSummary(Info.SelectedServices);
            }
        }

        private IProdPrice[] GetTestSelectedServices()
        {
            Random rnd = new Random(unchecked((int) DateTime.Now.Ticks));

            int count = 10;

            IProdPrice[] products = new IProdPrice[count];

            for (int i = 0; i < count; i++) {
                TestProdPrice product = new TestProdPrice(
                    i,
                    (ProdSelectionState) (rnd.Next(3) - 1),
                    rnd.Next(2) == 1,
                    (decimal) rnd.Next(100),
                    "Product #" + i,
                    "Description #" + i,
                    "Type #" + i);

                product.BillText = "Bill Text #" + 1;

                products[i] = product;
            }

            return products;
        }

        #endregion
    }
}