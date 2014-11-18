using System;
using DPI.Interfaces;

namespace DPI.Components
{
	/// <summary>
	/// Summary description for BankCheckPayment.
	/// </summary>
	public class BankCheckPayment:Payment
	{
		#region Constructors

		public BankCheckPayment() : base()
		{
		}

		public BankCheckPayment(IMap imap) : base(imap) 
		{
		}

		public BankCheckPayment(UOW uow) : base(uow)
		{
		}

		#endregion

		
		public string BankAccountNumber
		{
			get { return _bankAccountNumber; }
			set
			{
				base.setState();
				_bankAccountNumber = value;
			}
		}

		public string BankRoutingNumber
		{
			get { return _bankRoutingNumber; }
			set
			{
				base.setState();
				_bankRoutingNumber = value;
			}
		}

		public string DriverLicenseNumber
		{
			get { return _driverLicenseNumber; }
			set
			{
				base.setState();
				_driverLicenseNumber = value;
			}
		}

		public string DriverLicenseState
		{
			get { return _driverLicenseState; }
			set
			{
				base.setState();
				_driverLicenseState = value;
			}
		}


		protected override SqlGateway loadSql()
		{
			return new BankCheckPaymentSql();
		}

		public override PaymentType PaymentType
		{
			get
			{
				return PaymentType.Check;
			}
		}


		protected class BankCheckPaymentSql : PaymentSql
		{
			protected override Payment CreatePayment()
			{
				return new BankCheckPayment();
			}
		}
	}
}
