using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Account.Payment
{
    public class PaymentHistoryPage : BaseAccountPage
    {
        #region Web Form Designer generated code

        protected DropDownList ddlPaymentType;
        protected DateBox dtFrom;
        protected DateBox dtTo;
        protected ImageButton btnSubmit;
        protected CustomValidator vldCstPaymentDateRange;
        protected DataGrid dgPayments;

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
            this.vldCstPaymentDateRange.ServerValidate += new ServerValidateEventHandler(this.vldCstPaymentDateRange_ServerValidate);
            this.btnSubmit.Click += new ImageClickEventHandler(this.btnSubmit_Click);
            this.dgPayments.DataBinding += new EventHandler(this.dgPayments_DataBinding);
            this.dgPayments.ItemDataBound += new DataGridItemEventHandler(this.dgPayments_ItemDataBound);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ddlPaymentType.SelectedIndex = 0;
                FromDate = DateTime.Now.AddMonths(-3);
                ToDate = DateTime.Now;
                dgPayments.DataBind();
            }
        }

        private void dgPayments_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.DataItem != null) {
                e.Item.Cells[1].Text = ((DPI.Components.Payment) e.Item.DataItem).PaymentType.ToString();
                e.Item.Cells[3].Text = ((DPI.Components.Payment) e.Item.DataItem).Demand.Status;
            }
        }

        private void vldCstPaymentDateRange_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (dtFrom.IsNull || dtFrom.IsValid) && (dtTo.IsNull || dtTo.IsValid);

            if (args.IsValid && !dtFrom.IsNull && !dtTo.IsNull) {
                args.IsValid = dtFrom.Date.Date <= dtTo.Date.Date;
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid) {
                dgPayments.DataBind();
            }
        }

        private void dgPayments_DataBinding(object sender, EventArgs e)
        {
            int accNumber = GetAccountNumber();
            DPI.Components.Payment[] payments = PaymentSvc.GetPaymentsByAccNumber(Map, accNumber);
            payments = FilterPayments(payments);
            dgPayments.DataSource = payments;
        }

        #endregion

        private DPI.Components.Payment[] FilterPayments(DPI.Components.Payment[] payments)
        {
            ArrayList filteredPayments = new ArrayList();

            foreach (DPI.Components.Payment payment in payments) {
                bool isPaymentMatched = true;

                if (IsPaymentTypeSelected && !IsPaymentTypeSelectedAll) {
                    isPaymentMatched = (payment.PaymentType == PaymentType);
                }

                if (IsFromDateSelected && isPaymentMatched) {
                    isPaymentMatched = (payment.PaymentDate.Date >= FromDate.Date);
                }

                if (IsToDateSelected && isPaymentMatched) {
                    isPaymentMatched = (payment.PaymentDate.Date <= ToDate.Date);
                }

                if (isPaymentMatched) {
                    filteredPayments.Add(payment);
                }
            }

            return (DPI.Components.Payment[]) filteredPayments.ToArray(typeof (DPI.Components.Payment));
        }

        private bool IsPaymentTypeSelected
        {
            get { return ddlPaymentType.SelectedValue != string.Empty; }
        }

        private bool IsPaymentTypeSelectedAll
        {
            get { return ddlPaymentType.SelectedValue == "All"; }
        }

        private PaymentType PaymentType
        {
            get
            {
                if (!IsPaymentTypeSelected) {
                    throw new InvalidOperationException("Payment type is not selected.");
                }

                return (PaymentType) Enum.Parse(typeof (PaymentType), ddlPaymentType.SelectedValue, true);
            }
        }

        private bool IsFromDateSelected
        {
            get { return !dtFrom.IsNull; }
        }

        private bool IsToDateSelected
        {
            get { return !dtTo.IsNull; }
        }

        private DateTime FromDate
        {
            get
            {
                if (!IsFromDateSelected) {
                    throw new InvalidOperationException("From date is not selected.");
                }

                return dtFrom.Date;
            }
            set { dtFrom.Date = value; }
        }

        private DateTime ToDate
        {
            get
            {
                if (!IsToDateSelected) {
                    throw new InvalidOperationException("To date is not selected.");
                }

                return dtTo.Date;
            }
            set { dtTo.Date = value; }
        }
    }
}