using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class PayInfoLocal : PayInfo, IPayInfoLocal
	{
		/*        Amounts Properties        */
		public override decimal LocalAmountDue
		{
			get { return localDue; }
			set
			{
				setState();
				SetAmts(this.amtPaid, Decimal.Round(value, 2), this.amtLDDue, this.amtTendered);
			}
		}
		public override decimal LocalAmountPaid
		{
			get { return localPaid; }
			set
			{
				throw new Exception("Can't set LocalAmountPaid directly");
			}
		}

		public override decimal LdAmount
		{
			get { return amdLDPaid; }
			set
			{
				throw new Exception("Can't set LDAmountPaid directly");
			}
		}
		
		public override decimal LdAmountDue
		{
			get { return amtLDDue; }
			set
			{
				SetAmts(amtPaid, localDue, decimal.Round(value, 2), amtTendered);
			}
		}
		public override decimal TotalAmountPaid 
		{ 
			get { return amtPaid; }
			set { throw new ApplicationException("TotalAmountPaid (set) is not supported");}
		}
		/*        Constructors			*/
		internal PayInfoLocal() : base()
		{
			// ovveride
			payClass = PayInfoClass.PayInfoLocal.ToString();
		}
		internal PayInfoLocal(IMap imap) : base(imap)
		{
			// ovveride
			payClass = PayInfoClass.PayInfoLocal.ToString();
		}
		internal PayInfoLocal(UOW uow) : base(uow)
		{
			// ovveride
			payClass = PayInfoClass.PayInfoLocal.ToString();
		}
        
		/*        Methods        */
		public override void SetAmts(decimal amtPaid, decimal localDue, decimal amtLDDue, decimal amtTendered)
		{
			setState();

			this.amtPaid = amtPaid;
			this.amtLDDue = this.amdLDPaid = amtLDDue; // ld is always paid in full
			this.localDue = localDue;
			this.localPaid = this.amtPaid - this.amdLDPaid;
			this.amtTendered = amtTendered;

			Recalc();
		}
		public void PayInFull(decimal localDue, decimal amtLDDue, decimal amtTendered)
		{
			setState();

			this.localPaid = this.localDue = localDue;
			this.amdLDPaid  = this.amtLDDue = amtLDDue;
			this.amtPaid   = this.localPaid + this.amdLDPaid;
			this.amtTendered = amtTendered;

			Recalc();
		}
		protected override void Recalc()
		{
			this.totAmtDue = this.localDue + this.amtLDDue;
			decimal ava = this.amtPaid;

			if (!(ava > 0))
				return;

			ava = ApplyToLD(ava);
			
			if (!(ava > 0))
				return;

			localPaid = ava;
		}
		protected override decimal ApplyToLD(decimal amt)
		{
			amdLDPaid = 0m;

			if (!(amtLDDue > 0))
				return amt;

			if (amtLDDue > amt)
			{
				amdLDPaid = amt;
				return 0m;
			}

			amdLDPaid = amtLDDue; // paid
			return amt - amtLDDue;
		}
		protected override decimal ApplyToLocal(decimal amt)
		{
			localPaid = 0m;
			if (!(localDue > 0))
				return amt;

			if (localDue > amt)
			{
				localPaid = amt;
				return 0m;
			}

			localPaid = localDue; // paid
			return amt - localPaid;
		}
		public override string Validate()
		{
			string msg = base.Validate();
			
			if (msg !=  string.Empty)
				return msg;

			if (amtPaid < this.LdAmountDue) 
				return "Amount Paid cannot be less than Long Distance Calling Card amount";;

			return string.Empty;
		}
	}
}
