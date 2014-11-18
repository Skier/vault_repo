using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
    /// <summary>
    /// The class is a result of optimization of Public Web Site ~/account/summary.aspx page.
    /// The main goal of this class is to give all necessary account information for
    /// displaying on summary.aspx page with ONLY ONE call to database.
    /// </summary>
    public class AccountSummary : CustData
    {
        #region Fields

        private bool _isPtpEnabled;
        private bool _isBillDateNull;
        private DateTime _billDate;
        private bool _isNewOrderDemandIdNull;
        private int _newOrderDemandId;
        private IAcctInfo _acctInfo;

        #endregion

        #region Static Methods

        new public static AccountSummary find(UOW uow, int accNumber)
        {
            if (uow.Imap.keyExists(AccountSummary.getKey(accNumber))) {
                return (AccountSummary) uow.Imap.find(AccountSummary.getKey(accNumber));
            }

            AccountSummary cls = new AccountSummary();
            cls.uow = uow;
            cls.accNumber = accNumber;
            cls = (AccountSummary) DomainObj.addToIMap(uow, ((AccountSummarySql) cls.Sql).getAccountSummary(cls));
            cls.uow = uow;

            return cls;
        }

        #endregion

        #region Constructors

        protected AccountSummary()
        {
            sql = new AccountSummarySql();
            rowState = RowState.New;
        }

        #endregion

        #region Override Methods

        protected override SqlGateway loadSql()
        {
            return new AccountSummarySql();
        }

        #endregion

        #region Properties

        public bool IsPromiseToPayEnabled
        {
            get { return _isPtpEnabled; }
        }

        public bool IsNewOrderDemandIdNull
        {
            get { return _isNewOrderDemandIdNull; }
        }

        public int NewOrderDemandId
        {
            get
            {
                if (_isNewOrderDemandIdNull) {
                    throw new InvalidOperationException("NewOrderDemandId is null.");
                }

                return _newOrderDemandId;
            }
        }

        public string FormattedStreetAddress
        {
            get
            {
                IAddr2 addr = ServiceAddr != null ? ServiceAddr : MailingAddr;
                if (addr == null) {
                    return string.Empty;
                }

                return addr.FormattedStreetAddress;
            }
        }

        public string FormattedCityStateZip
        {
            get
            {
                IAddr2 addr = ServiceAddr != null ? ServiceAddr : MailingAddr;
                if (addr == null) {
                    return string.Empty;
                }

                return addr.FormattedCityStateZip;
            }
        }

        public string Status
        {
            get { return Account.GetStatus(Status1); }
        }

        public bool IsRecurringPaymentEnabled
        {
            get { return Status == "Active" || Status == "Pending Activation" || Status == "Pending Order"; }
        }

        public bool IsPaymentEnabled
        {
            get { return PhNumber != null && PhNumber != string.Empty && Status != "Disconnected"; }
        }

        public bool IsDueDateNull
        {
            get { return Due_Date == DateTime.MinValue; }
        }

        public DateTime DueDate
        {
            get
            {
                if (IsDueDateNull) {
                    throw new InvalidOperationException("DueDate is null.");
                }

                return Due_Date;
            }
        }

        public bool IsDiscoDateNull
        {
            get { return SDiscoDate == DateTime.MinValue; }
        }

        public DateTime DiscoDate
        {
            get
            {
                if (IsDiscoDateNull) {
                    throw new InvalidOperationException("DiscoDate is null.");
                }

                return SDiscoDate;
            }
        }

        public decimal BalForward
        {
            get { return _acctInfo.BalForward; }
        }

        public decimal CurrCharges
        {
            get { return _acctInfo.CurrCharges; }
        }

        public decimal DueAmt
        {
            get { return _acctInfo.DueAmt; }
        }

        public string CustomerBillFileName
        {
            get
            {
                if (_isBillDateNull) {
                    return string.Empty;
                }

                return PastReminderNotice.GetCustomerBillFileName(AccNumber, _billDate, PhNumber);
            }
        }

        #endregion

        #region DataSource Class

        [Serializable]
        private class AccountSummarySql : CustDataSQL
        {
            #region Public Methods

            public AccountSummary getAccountSummary(AccountSummary rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccountSummary_Get_Id";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;

                DomainObj[] objs = execReader(cmd);
                if (objs.Length > 1) {
                    throw new ApplicationException("Multiple account summary rows were found for account number " + rec.accNumber + ".");
                }

                return (AccountSummary) objs[0];
            }

            #endregion

            #region Override Methods

            protected override void loadCustData(SqlDataReader rdr, CustData rec)
            {
                base.loadCustData(rdr, rec);

                AccountSummary accSummary = (AccountSummary) rec;

                if (rdr["PtpEligible"] != DBNull.Value) {
                    accSummary._isPtpEnabled = Boolean.Parse((string) rdr["PtpEligible"]);
                } else {
                    throw new ApplicationException("'PtpEligible' column in the resulting data set of 'spAccountSummary_Get_Id' procedure can not be null.");
                }

                if (rdr["BillDate"] != DBNull.Value) {
                    accSummary._isBillDateNull = false;
                    accSummary._billDate = (DateTime) rdr["BillDate"];
                } else {
                    accSummary._isBillDateNull = true;
                }

                if (rdr["NewOrderDemandId"] != DBNull.Value) {
                    accSummary._isNewOrderDemandIdNull = false;
                    accSummary._newOrderDemandId = (int) rdr["NewOrderDemandId"];
                } else {
                    accSummary._isNewOrderDemandIdNull = true;
                }

                bool isActive;
                if (rdr["IsActive"] != DBNull.Value) {
                    isActive = Boolean.Parse((string) rdr["IsActive"]);
                } else {
                    throw new ApplicationException("'IsActive' column in the resulting data set of 'spAccountSummary_Get_Id' procedure can not be null.");
                }

                decimal pastDueAmt;
                if (rdr["PastDueAmount"] != DBNull.Value) {
                    // -1 To accommodate a new version of fnPastDueAmt
                    // See Account.Refresh() method
                    pastDueAmt = -1.0m * (decimal) rdr["PastDueAmount"];
                } else {
                    pastDueAmt = 0m;
                }

                decimal currDueAmt;
                if (rdr["CurrentDueAmount"] != DBNull.Value) {
                    // -1 To accommodate a new version of fnCurrentChargeAmt
                    // See Account.Refresh() method
                    currDueAmt = -1.0m * (decimal) rdr["CurrentDueAmount"];
                } else {
                    currDueAmt = 0m;
                }

                accSummary._acctInfo = new AcctInfo(rec.AccNumber, rec.PhNumber, isActive, pastDueAmt, currDueAmt, rec.Balance, rec.Due_Date, rec.SDiscoDate, rec.Status1, rec.NameFirst, rec.NameLast);
            }

            protected override DomainObj reader(SqlDataReader rdr)
            {
                AccountSummary rec = new AccountSummary();
                loadCustData(rdr, rec);
                return rec;
            }

            #endregion
        }

        #endregion
    }
}