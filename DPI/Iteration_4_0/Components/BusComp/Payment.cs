using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
    /// <summary>
    /// Represents a payment of the external payment service 
    /// provided by <see cref="PaymentServiceProvider"/> class.
    /// </summary>
    public abstract class Payment : DomainObj
    {
        #region Static Members

        private static string iName = "Payment";

        public static Payment[] GetPaymentsByAccNumber(UOW uow, int accNumber)
        {
            if (uow == null) {
                throw new ArgumentNullException("uow");
            }

            return PaymentSql.GetPaymentsByAccNumber(uow, accNumber);
        }

        public static void GetMinMaxPaymentAmount(out decimal minValue, out decimal maxValue)
        {
            PaymentSql.GetMinMaxPaymentAmount(out minValue, out maxValue);
        }

        protected static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }

        protected static Payment find(UOW uow, int id, ref Payment cls)
        {
            if (uow == null) {
                throw new ArgumentNullException("uow");
            }

            if (cls == null) {
                throw new ArgumentNullException("cls");
            }

            if (uow.Imap.keyExists(Payment.getKey(id))) {
                return (Payment) uow.Imap.find(Payment.getKey(id));
            }

            cls.uow = uow;
            cls._id = id;
            cls = (Payment) DomainObj.addToIMap(uow, getOne(((PaymentSql) cls.Sql).getKey(cls)));
            cls.uow = uow;

            return cls;
        }

        private static Payment getOne(Payment[] acls)
        {
            if (acls.Length == 1) {
                return acls[0];
            }

            if (acls.Length == 0) {
                throw new ArgumentException("Row not found");
            }

            throw new ArgumentException("More than one row found");
        }

        #endregion

        #region Fields

        #region General Fields

        protected int _id;
        protected decimal _amount;
        protected int _demandId;
        protected int _confNum;
        protected IDemand _demand;
        protected int _paymentTransactionId;
        protected PaymentTransaction _paymentTransaction;
        protected DateTime _paymentDate;
        protected string _application;
        protected string _nameFirst;
        protected string _nameLast;
        protected string _address;
        protected string _zip;
        protected int _paymentInfoId;
        protected IPayInfo _paymentInfo;

        #endregion

        #region Credit Card Related Fields

        protected CreditCardType _ccType;
        protected string _ccNumber;
        protected string _cvNumber;
        protected int _expYear;
        protected int _expMonth;

        #endregion

        #region Bank Check Related Fields

        protected string _bankAccountNumber;
        protected string _bankRoutingNumber;

        protected string _driverLicenseNumber;
        protected string _driverLicenseState;

        #endregion

        #endregion

        #region Constructors

        public Payment()
        {
            base.sql = loadSql();
            this._id = random.Next(Int32.MinValue, -1);
            base.rowState = RowState.New;
            this._paymentDate = DateTime.Now;
        }

        public Payment(IMap imap) : this()
        {
            if (imap == null) {
                throw new ArgumentNullException("imap");
            }

            imap.add(this);
        }

        public Payment(UOW uow) : this(uow.Imap)
        {
            if (uow == null) {
                throw new ArgumentNullException("uow");
            }

            base.uow = uow;
        }

        #endregion

        #region Properties

        public override IDomKey IKey
        {
            get { return new Key(iName, _id.ToString()); }
        }

        public int Id
        {
            get { return _id; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                base.setState();
                _amount = value;
            }
        }

        public int ConfNum
        {
            get { return _confNum; }
            set { _confNum = value; }
        }

        public DateTime PaymentDate
        {
            get { return this._paymentDate; }
            set
            {
                base.setState();
                this._paymentDate = value;
            }
        }

        public string Application
        {
            get { return this._application; }
            set
            {
                base.setState();
                this._application = value;
            }
        }

        public string NameFirst
        {
            get { return this._nameFirst; }
            set
            {
                base.setState();
                this._nameFirst = value;
            }
        }

        public string NameLast
        {
            get { return this._nameLast; }
            set
            {
                base.setState();
                this._nameLast = value;
            }
        }

        public string Address
        {
            get { return this._address; }
            set
            {
                base.setState();
                this._address = value;
            }
        }

        public string Zip
        {
            get { return this._zip; }
            set
            {
                base.setState();
                this._zip = value;
            }
        }

        public int PaymentInfoId
        {
            get { return this._paymentInfoId; }
            set
            {
                base.setState();
                this._paymentInfoId = value;
            }
        }

        public IPayInfo PaymentInfo
        {
            get { return this._paymentInfo; }
            set
            {
                base.setState();
                this._paymentInfo = value;
            }
        }

        public int DemandId
        {
            get
            {
                if (Demand == null) {
                    return 0;
                }

                return Demand.Id;
            }

            set
            {
                if (_demand != null) {
                    if (_demand.Id != value) {
                        throw new ApplicationException("Demand id of Set propery conflicts with Demand already in this Payment");
                    }
                }

                _demandId = value;
            }
        }

        public IDemand Demand
        {
            get
            {
                if (_demand == null && _demandId > 0) {
                    _demand = Components.Demand.find(uow, _demandId);
                }

                return _demand;
            }

            set
            {
                _demand = value;
                _demandId = 0;

                if (_demand != null) {
                    _demandId = _demand.Id;
                }

                base.setState();
            }
        }

        public int PaymentTransactionId
        {
            get
            {
                if (PaymentTransaction == null) {
                    return 0;
                }

                return PaymentTransaction.Id;
            }

            set
            {
                if (_paymentTransaction != null) {
                    if (_paymentTransaction.Id != value) {
                        throw new ApplicationException("Payment transaction id of Set propery conflicts with PaymentTransaction already in this Payment");
                    }
                }

                _paymentTransactionId = value;
            }
        }

        public PaymentTransaction PaymentTransaction
        {
            get
            {
                if (_paymentTransaction == null && _paymentTransactionId > 0) {
                    _paymentTransaction = PaymentTransaction.find(uow, _paymentTransactionId);
                }

                return _paymentTransaction;
            }

            set
            {
                _paymentTransaction = value;
                _paymentTransactionId = 0;

                if (_paymentTransaction != null) {
                    _paymentTransactionId = _paymentTransaction.Id;
                }

                base.setState();
            }
        }

        public abstract PaymentType PaymentType { get; }

        #endregion

        #region Methods

        public override void delete()
        {
            if (_paymentTransaction != null) {
                _paymentTransaction.delete();
                _paymentTransaction = null;
            }

            base.delete();
        }

        public override void checkExists()
        {
            if (Id < 1) {
                throw new ArgumentException("Valid row is required for update and delete");
            }
        }

        public override void RefreshForeignKeys()
        {
            if (_paymentTransactionId > 0) {
                return;
            }

            if (PaymentTransaction != null) {
                _paymentTransactionId = PaymentTransaction.Id;
            }

            _paymentInfoId = _paymentInfo.Id;
        }

        #endregion

        /// <summary>
        /// Represents Data Access Component for <see cref="Payment"/> class.
        /// </summary>
        protected abstract class PaymentSql : SqlGateway
        {
            public static Payment[] GetPaymentsByAccNumber(UOW uow, int accNumber)
            {
                if (uow == null) {
                    throw new ArgumentNullException("uow");
                }

                SqlCommand cmd = Conn.GetConn().CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spPayment_Get_AllByAccNumber";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = accNumber;

                ArrayList paymentList = new ArrayList();

                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
                    while (rdr.Read()) {
                        if (rdr["PaymentType"] == DBNull.Value) {
                            throw new ApplicationException("PaymentType field is null: account id is " + accNumber + ".");
                        }

                        string value = (string) rdr["PaymentType"];
                        PaymentType paymentType = (PaymentType) Enum.Parse(typeof (PaymentType), value, true);

                        Payment payment = PaymentFactory.GetInstance().CreatePayment(uow, paymentType);

                        reader(rdr, ref payment);

                        paymentList.Add(payment);
                    }

                    return (Payment[]) paymentList.ToArray(typeof (Payment));
                }
            }

            public static void GetMinMaxPaymentAmount(out decimal minValue, out decimal maxValue)
            {
                SqlCommand cmd = Conn.GetConn().CreateCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spGetMinMaxAllowedPaymentAmt";

                using (SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
                    minValue = maxValue = Decimal.MinValue;

                    while (rdr.Read()) {
                        string fieldName = rdr.GetString(0);
                        if (fieldName == "MaxPaymentAmount") {
                            maxValue = rdr.GetDecimal(1);
                        } else if (fieldName == "MinPaymentAmount") {
                            minValue = rdr.GetDecimal(1);
                        } else {
                            throw new ApplicationException("Unknown return field name: " + fieldName);
                        }
                    }

                    if (minValue == Decimal.MinValue || maxValue == Decimal.MinValue) {
                        throw new ApplicationException("spGetMinMaxAllowedPaymentAmt returns invalid values.");
                    }
                }
            }

            private static void reader(SqlDataReader rdr, ref Payment rec)
            {
                // General Fields

                if (rdr["Id"] != DBNull.Value) {
                    rec._id = (int) rdr["Id"];
                }

                if (rdr["PaymentType"] != DBNull.Value) {
                    string value = (string) rdr["PaymentType"];
                    PaymentType paymentType = (PaymentType) Enum.Parse(typeof (PaymentType), value, true);
                    if (rec.PaymentType != paymentType) {
                        throw new ApplicationException("Type of the payment object [" + rec.GetType().Name + "] does not match payment type in the row " + value + ".");
                    }
                }

                if (rdr["Amount"] != DBNull.Value) {
                    rec._amount = (decimal) rdr["Amount"];
                }

                if (rdr["DemandId"] != DBNull.Value) {
                    rec._demandId = (int) rdr["DemandId"];
                }

                if (rdr["PaymentTransactionId"] != DBNull.Value) {
                    rec._paymentTransactionId = (int) rdr["PaymentTransactionId"];
                }

                if (rdr["PaymentDate"] != DBNull.Value) {
                    rec._paymentDate = (DateTime) rdr["PaymentDate"];
                }

                if (rdr["NameFirst"] != DBNull.Value) {
                    rec._nameFirst = (string) rdr["NameFirst"];
                }

                if (rdr["NameLast"] != DBNull.Value) {
                    rec._nameLast = (string) rdr["NameLast"];
                }

                if (rdr["Address"] != DBNull.Value) {
                    rec._address = (string) rdr["Address"];
                }

                if (rdr["Zip"] != DBNull.Value) {
                    rec._zip = (string) rdr["Zip"];
                }

                // Credit Card Related Fields

                if (rdr["CcType"] != DBNull.Value) {
                    string value = (string) rdr["CcType"];
                    rec._ccType = (CreditCardType) Enum.Parse(typeof (CreditCardType), value, true);
                }

                if (rdr["CcNumber"] != DBNull.Value) {
                    rec._ccNumber = (string) rdr["CcNumber"];
                }

                if (rdr["CvNumber"] != DBNull.Value) {
                    rec._cvNumber = (string) rdr["CvNumber"];
                }

                if (rdr["ExpYear"] != DBNull.Value) {
                    rec._expYear = (int) rdr["ExpYear"];
                }

                if (rdr["ExpMonth"] != DBNull.Value) {
                    rec._expMonth = (int) rdr["ExpMonth"];
                }

                // Check Related Fields

                if (rdr["BankRoutingNumber"] != DBNull.Value) {
                    rec._bankRoutingNumber = (string) rdr["BankRoutingNumber"];
                }

                if (rdr["BankAccountNumber"] != DBNull.Value) {
                    rec._bankAccountNumber = (string) rdr["BankAccountNumber"];
                }

                if (rdr["DriverLicenseNumber"] != DBNull.Value) {
                    rec._driverLicenseNumber = (string) rdr["DriverLicenseNumber"];
                }

                if (rdr["DriverLicenseState"] != DBNull.Value) {
                    rec._driverLicenseState = (string) rdr["DriverLicenseState"];
                }

                rec.rowState = RowState.Clean;
            }

            public Payment[] getKey(Payment rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPayment_Get_AllById";
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = rec._id;

                return convert(execReader(cmd));
            }

            public override void insert(DomainObj obj)
            {
                Payment rec = (Payment) obj;

                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPayment_Ins";
                setParam(cmd, rec);
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;

                execScalar(cmd);

                rec._id = (int) cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }

            public override void delete(DomainObj rec)
            {
                throw new NotImplementedException();
            }

            public override void update(DomainObj rec)
            {
                throw new NotImplementedException();
            }

            protected abstract Payment CreatePayment();

            protected override DomainObj reader(SqlDataReader rdr)
            {
                Payment rec = CreatePayment();

                reader(rdr, ref rec);

                return rec;
            }

            private Payment[] convert(DomainObj[] objs)
            {
                Payment[] acls = new Payment[objs.Length];

                objs.CopyTo(acls, 0);

                return acls;
            }

            private void setParam(SqlCommand cmd, Payment rec)
            {
                // General Fields

                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.Id;
                cmd.Parameters.Add("@PaymentType", SqlDbType.VarChar, 20).Value = rec.PaymentType.ToString();
                cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = rec.Amount;

                if (rec.DemandId == 0) {
                    cmd.Parameters.Add("@DemandId", SqlDbType.Int).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@DemandId", SqlDbType.Int).Value = rec.DemandId;
                }

                if (rec.PaymentTransactionId == 0) {
                    cmd.Parameters.Add("@PaymentTransactionId", SqlDbType.Int).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@PaymentTransactionId", SqlDbType.Int).Value = rec.PaymentTransactionId;
                }

                if (rec.NameFirst == null) {
                    cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 25).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 25).Value = rec.NameFirst;
                }

                if (rec.NameLast == null) {
                    cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 25).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 25).Value = rec.NameLast;
                }

                if (rec.Address == null) {
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = rec.Address;
                }

                if (rec.Zip == null) {
                    cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = rec.Zip;
                }

                string merchantId;
                if (rec.PaymentType == PaymentType.Credit) {
                    merchantId = ConfigurationSettings.AppSettings["CreditCardMerchantId"];
                } else {
                    merchantId = ConfigurationSettings.AppSettings["CheckMerchantId"];
                }
                cmd.Parameters.Add("@MerchantId", SqlDbType.VarChar, 32).Value = merchantId;

                if (rec._paymentInfoId == 0) {
                    cmd.Parameters.Add("@PaymentInfoId", SqlDbType.Int).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@PaymentInfoId", SqlDbType.Int).Value = rec._paymentInfoId;
                }

                // Credit Card Related Fields

                if (rec.PaymentType == PaymentType.Credit) {
                    cmd.Parameters.Add("@CcType", SqlDbType.VarChar, 15).Value = rec._ccType;
                } else {
                    cmd.Parameters.Add("@CcType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                }

                if (rec._ccNumber == null || rec._ccNumber.Length == 0) {
                    cmd.Parameters.Add("@CcNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@CcNumber", SqlDbType.VarChar, 20).Value = rec._ccNumber;
                }

                if (rec._cvNumber == null || rec._cvNumber.Length == 0) {
                    cmd.Parameters.Add("@CvNumber", SqlDbType.VarChar, 4).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@CvNumber", SqlDbType.VarChar, 4).Value = rec._cvNumber;
                }

                if (rec._expYear == 0) {
                    cmd.Parameters.Add("@ExpYear", SqlDbType.Int).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@ExpYear", SqlDbType.Int).Value = rec._expYear;
                }

                if (rec._expMonth == 0) {
                    cmd.Parameters.Add("@ExpMonth", SqlDbType.Int).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@ExpMonth", SqlDbType.Int).Value = rec._expMonth;
                }

                // Check Related Fields

                if (rec._bankAccountNumber == null || rec._bankAccountNumber.Length == 0) {
                    cmd.Parameters.Add("@BankAccountNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@BankAccountNumber", SqlDbType.VarChar, 20).Value = rec._bankAccountNumber;
                }

                if (rec._bankRoutingNumber == null || rec._bankRoutingNumber.Length == 0) {
                    cmd.Parameters.Add("@BankRoutingNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@BankRoutingNumber", SqlDbType.VarChar, 20).Value = rec._bankRoutingNumber;
                }

                if (rec._driverLicenseNumber == null || rec._driverLicenseNumber.Length == 0) {
                    cmd.Parameters.Add("@DriverLicenseNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@DriverLicenseNumber", SqlDbType.VarChar, 20).Value = rec._driverLicenseNumber;
                }

                if (rec._driverLicenseState == null || rec._driverLicenseState.Length == 0) {
                    cmd.Parameters.Add("@DriverLicenseState", SqlDbType.VarChar, 20).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@DriverLicenseState", SqlDbType.VarChar, 20).Value = rec._driverLicenseState;
                }

                cmd.Parameters.Add("@PaymentDate", SqlDbType.DateTime).Value = rec._paymentDate;

                if (rec._application == null || rec._application.Length == 0) {
                    cmd.Parameters.Add("@Application", SqlDbType.VarChar, 20).Value = DBNull.Value;
                } else {
                    cmd.Parameters.Add("@Application", SqlDbType.VarChar, 20).Value = rec.Application;
                }
            }
        }
    }
}