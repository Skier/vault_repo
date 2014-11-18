using System;
using System.Web.UI;
using Dpi.Central.Web.Account.Wireless.Processes.Rsp;
using Dpi.Central.Web.Controls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rwa
{
    public class ReplenishWirelessAccountBasePage : BaseAccountPage
    {
        #region Constants

        #endregion

        #region Override Methods

        protected override void InitLayout()
        {
            base.InitLayout();

            ProcessMap processMap = new ProcessMap();

            processMap.CssClass = "process_map";
            processMap.CssClassPrevious = "previous_step";
            processMap.CssClassCurrent = "current_step";
            processMap.CssClassNext = "next_step";
            processMap.Provider = ProcessMapProvider;

            Form.Controls.AddAt(2, processMap);
        }

        protected override Control CreateErrorControl() 
        {
            return ErrorControlFactory.Instance.CreatePanelErrorControl();
        }

        protected override void AddErrorControlToPage() 
        {
            Form.Controls.AddAt(3, ErrorControl);
        }

        protected void ResetProcess()
        {
            Session["RWA_Model"] = Session["FromPreviousStep"] = Session["PhoneNumber"] = Session[PAYMENT_AMOUNT_KEY] = null;
        }

        protected PaymentResult MakePayment(CreditCard creditCard)
        {
            Model.LoadCustomerRechargedProducts();
            Model.CreateOrderSummary(RspProcess.RechargeSamePlan.ToString());
            return Model.MakePayment(creditCard, PaymentAmount);
        }

        protected PaymentResult MakePayment(BankCheck bankCheck) 
        {
            Model.LoadCustomerRechargedProducts();
            Model.CreateOrderSummary(RspProcess.RechargeSamePlan.ToString());
            return Model.MakePayment(bankCheck, PaymentAmount);
        }

        #endregion

        #region Properties

        protected string PhoneNumber
        {
            get 
            { 
                object value = Session["PhoneNumber"];
                if (value == null) {
                    throw new ApplicationException(string.Format(SESSION_STATE_IS_INVALID, "PhoneNumber"));
                } else {
                    return (string) value;
                } 
            }

            set { Session["PhoneNumber"] = value; }
        }

        protected bool IsFirstStep
        {
            get { return Session["PhoneNumber"] == null; }
        }

        protected decimal PaymentAmount
        {
            get
            {
                object value = Session[PAYMENT_AMOUNT_KEY];
                if (value == null) {
                    throw new ApplicationException(string.Format(SESSION_STATE_IS_INVALID, PAYMENT_AMOUNT_KEY));
                } else {
                    return (decimal) value;
                }
            }

            set { Session[PAYMENT_AMOUNT_KEY] = value; }
        }

        internal RechargeServicePlanModel Model 
        {
            get {
                if (Session["RWA_Model"] == null) {
                    IWireless_Custdata[] customerDataList = CustSvc.GetWirelessCustDataByPhone(Map, PhoneNumber);
                    if (customerDataList.Length == 0) {
                        throw new ArgumentException("No wireless customer found with " + PhoneNumber + " phone number.");
                    }

                    Session["RWA_Model"] = new RechargeServicePlanModel(Map, customerDataList[0].ID);
                }

                return (RechargeServicePlanModel) Session["RWA_Model"];
            }
        }

        private IProcessMapProvider ProcessMapProvider
        {
            get
            {
                if (Application["RwaProcessMapProvider"] == null) {
                    Application["RwaProcessMapProvider"] = new ReplenishWirelessAccountProcessMapProvider();
                }

                return (ReplenishWirelessAccountProcessMapProvider) Application["RwaProcessMapProvider"];
            }
        }

        #endregion
    }
}