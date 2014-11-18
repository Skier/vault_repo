using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class Account
	{
        #region Constants

        /// <summary>
        /// Maximum amount of money to use in "Promise To Pay" operation.
        /// </summary>
        public const decimal MAX_PTP_PAY_AMOUNT = 214748.3647M;

        /// <summary>
        /// Minimum amount of money to use in "Promise To Pay" operation.
        /// </summary>
        public const decimal MIN_PTP_PAY_AMOUNT = 0.0M;

        #endregion

		/*        Data        */		
		bool isActive;
		int accNumber;
		string firstName;
		string lastName;
		string phNumber;
		decimal custDataBal;
		decimal pastDueAmt;
		decimal currDueAmt;
		DateTime dueDate;
		DateTime discoDate;
		string status;

		bool empty = true;
		UOW uow;
        
		/*        Properties        */
		public UOW UOW
		{ 
			get { return uow; }
			set 
			{ 
				if(uow == null)
					throw new ArgumentException("Unit Of Work is required", "Account");
				uow = value; 
			}
		}
		public int AccNumber
		{
			get { return accNumber;  }
			set { accNumber = value; }
		}
		public decimal NextPymtAmt { get { return (PastDueAmt + CurrDueAmt - CustDataBal); }}
		public decimal PastDueAmt 
		{ 
			get 
			{  
				if (empty)
					Refresh();
				return pastDueAmt;
			}
		}
		public decimal CurrDueAmt
		{ 
			get 
			{ 
				if (empty)
					Refresh();
				return currDueAmt; 
			}
		}
		public decimal CustDataBal
		{ 
			get 
			{ 
				if (empty)
					Refresh();
				return custDataBal; 
			}
		}
		public string   PhNumber
		{ 
			get 
			{ 
				if (empty)
					Refresh();
				return  phNumber; 
			} 
		}
		public bool     IsActive
		{ 
			get 
			{
				if (empty)
					Refresh();
				return isActive; 
			} 
		}
		public DateTime     DueDate
		{ 
			get 
			{
				if (empty)
					Refresh();
				return dueDate; 
			} 
		}
		public DateTime     DiscoDate
		{ 
			get 
			{
				if (empty)
					Refresh();
				return discoDate; 
			} 
		}

		public AcctInfo AccountInfo
		{
			get
			{ 
				return new AcctInfo(AccNumber, PhNumber, IsActive, PastDueAmt, CurrDueAmt, 
								    CustDataBal, DueDate, DiscoDate, Status, firstName, lastName);
			}
		}
		public string Status 
		{
			get { return GetStatus(status); }
		}

		/*        Constructors			*/
		public Account()
		{
		}
		public Account(UOW uow, int accNumber)
		{
			this.uow = uow;

			if(accNumber == 0)
				throw new ArgumentException("Account Number is required", "Account");

			this.accNumber = accNumber;
			Refresh();
		}
		public Account(UOW uow, string phNumber)
		{
			this.uow = uow;

			if(phNumber == null)
				throw new ArgumentException("Phone Number is required", "Account");
			
			this.phNumber = phNumber;			
			this.accNumber = FindByPhNumber(uow, phNumber);
			if (this.accNumber == 0)
				throw new ArgumentException("Phone number not found");

			Refresh();
		}
		/*        Methods        */
		public static IAcctInfo GetDummyAccount()
		{
			Account account = new Account();
			account.accNumber = 0;
			account.currDueAmt = 0m;
			account.custDataBal = 0m;
			account.discoDate = DateTime.Today;
			account.dueDate = DateTime.Today;
			account.empty = false;
			account.firstName = "Unknown";
			account.isActive = false;
			account.lastName = "";
			account.pastDueAmt = 0m;
			account.phNumber = "000-000-0000";
			account.status = "4";
			account.uow = null;

			return account.AccountInfo;

		}
		public static IAcctInfo GetAccountInfo(UOW uow, string phNumber)
		{
			return new Account(uow, phNumber).AccountInfo;
		}
		public static IAcctInfo GetAccountInfo(UOW uow, int accNumber)
		{
			return new Account(uow, accNumber).AccountInfo;
		}
		public static string GetStatus(string status)
		{
			switch (status.Substring(0,1))
			{
				case "1" :
				case "9" :
					return  "Pending Order";
				case "2" :
					return "Pending Activation";
				case "3" :
					return "Active";
				case "4" :
				case "5":
					return "Pending Disconnect";
				case "6" :
					return "Disconnected";

				default:
					throw new ApplicationException("Unknow account status: " + status.Substring(0,1));
			}
		}

		public static int  FindByPhNumber(UOW uow, string phNumber)
		{
			return new SQL().getAccNumber(uow, phNumber);
		}
		public static bool IsActiveAccount(UOW uow, int accNumber)
		{
			return new SQL().IsActive(uow, accNumber); 
		}
		public static bool IsActiveAccount(UOW uow, string phNumber)
		{
			return new SQL().IsActive(uow, phNumber); 
		}
        public static void MakePromiseToPay(
            UOW uow, int accNumber, DateTime payDate, decimal payAmount, string userId)
        {
            if (uow == null) {
                throw new ArgumentNullException("uow");
            }

            if (payDate.Date <= DateTime.Now.Date) {
                throw new ArgumentException("Pay date must be bigger than " 
                    + DateTime.Now.Date.ToShortDateString() + ".", "payDate");
            }

            if (payAmount < MIN_PTP_PAY_AMOUNT || payAmount > MAX_PTP_PAY_AMOUNT) {
                throw new ArgumentException("Pay amount must be in range between "
                    + MIN_PTP_PAY_AMOUNT + " and " + MAX_PTP_PAY_AMOUNT + ".", "payAmount");
            }

            if (userId == null || userId == string.Empty) {
                throw new ArgumentNullException("userId");
            }

            SQL ds = new SQL();
            ds.MakePromiseToPay(uow, accNumber, payDate, payAmount, userId);
        }

		/*		Implementation		*/
		void Refresh()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Account");
		
			SQL sql = new SQL();
			
			if (accNumber == 0)
				accNumber = sql.getAccNumber(uow, phNumber);
		
			pastDueAmt  = -1 * sql.getPastDueAmt(uow, accNumber); // To accommodate a new version of fnPastDueAmt
			currDueAmt  = -1 * sql.getCurtCharges(uow, accNumber);// To accommodate a new version of fnCurrentChargeAmt
			
			CustData cd = CustData.find(uow, accNumber);
			
			custDataBal = cd.Balance;
			phNumber    = cd.PhNumber;	

			this.firstName = cd.CustInfoExtended.CustInfo.FirstName;
			this.lastName  = cd.CustInfoExtended.CustInfo.LastName;

			isActive    = sql.IsActive(uow,	accNumber);	
			dueDate     = cd.Due_Date;
			discoDate   = cd.SDiscoDate;
			status      = cd.Status1;

			empty = false;
		}
		/*		SQL		*/
		class SQL
		{
			public bool IsActive(UOW uow, string phNumber)
			{
				SqlCommand cmd = getCommand(uow);
				cmd.CommandText = "Select dbo.fnCustomer_IsActive(1, " + phNumber.Trim() + ") AS Res";
				return (bool)FunctRdr(cmd);
			}
			public bool IsActive(UOW uow, int accNumber)
			{
				SqlCommand cmd = getCommand(uow);
				cmd.CommandText = "Select dbo.fnCustomer_IsActive(0, " + accNumber.ToString() + ") AS Res";
				return (bool)FunctRdr(cmd);
			}
			public int getAccNumber(UOW uow, string phNumber)
			{
				SqlCommand cmd = getCommand(uow);
				cmd.CommandText = "Select dbo.fnCustomer_GetCurrentAccNumber('" + phNumber.Trim() + "') AS Res";
				object res = FunctRdr(cmd);
				
				if (res == null)
					return 0;	// not found
				return (int)res;
			}
			public decimal getPastDueAmt(UOW uow, int accNumber)
			{
				SqlCommand cmd = getCommand(uow);
				cmd.CommandText = "Select dbo.fnPastDueAmt(" + accNumber + ", getdate()) AS Res";
				object res = FunctRdr(cmd);
				
				if (res == null)
					return 0;	// not found
				return (decimal)res;			
			}
			public decimal getCurtCharges(UOW uow, int accNumber)
			{
				SqlCommand cmd = getCommand(uow);
				cmd.CommandText = "select dbo.fnCurrentChargeAmt(" + accNumber + ", getdate()) AS Res";
				object res = FunctRdr(cmd);
				
				if (res == null)
					return 0;	// not found
				return (decimal)res;
			}
            public void MakePromiseToPay(UOW uow, int accNumber, DateTime payDate, decimal payAmount, string userId)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();

                cmd.Transaction = uow.Tran; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spAccount_AddPTP";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
                cmd.Parameters.Add("@PTP_Date", SqlDbType.DateTime).Value = payDate;
                cmd.Parameters.Add("@PTP_Amount", SqlDbType.SmallMoney).Value = payAmount;
                cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 20).Value = userId;

                try {
                    cmd.ExecuteScalar();
                } catch (SqlException se) {
                    if (cmd.Transaction != null)
                        if (cmd.Transaction.Connection != null)
                            cmd.Transaction.Rollback();
				
                    if (se.Number == Const.CE)
                        throw new ConcurrencyException();

                    throw se;
                } catch (Exception e) {
                    if (cmd.Transaction != null)
                        if (cmd.Transaction.Connection != null)
                            cmd.Transaction.Rollback();
				
                    throw e;                
                }
            }
			object FunctRdr(SqlCommand cmd )
			{
				SqlDataReader rdr = null;
				try
				{
					rdr = cmd.ExecuteReader();
					rdr.Read();
					if (rdr["Res"] != DBNull.Value)
						return rdr["Res"];

					return null;
				}
				catch (SqlException se)
				{
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
				
					if (se.Number == Const.CE)
						throw new ConcurrencyException();

					throw se;
				}
				catch (Exception e)
				{
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
				
					throw e;                
				}
				finally
				{
					rdr.Close();
				}
			}
			SqlCommand getCommand(UOW uow)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.Text;
				return cmd;
			}
			/*        Implementation        */
		}
	}
}