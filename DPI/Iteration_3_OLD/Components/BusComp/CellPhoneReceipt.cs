using System;
using System.Text;
using System.Collections;

using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class CellPhoneReceipt: PinReceipt, ICellPhoneReceipt, IPinReceipt
	{
	#region Data
		bool   pass;
		string phoneNumber;
		string errMsg;
		string status;
		bool   isActivated;
		bool   isRefilled;
		string msl;
		string msid;
		string mdn;
		string controlNumber;

	#endregion
	
		#region Properties
		
		public string ErrMsg	{ get { return errMsg; }}
		public string Status	{ get { return status; }}		
		public bool   Pass		
		{ 
			get { return pass; }
			set { pass = value; }
		}

		public string PhoneNumber		
		{ 
			get { return phoneNumber; }
			set { phoneNumber = value; }
		}		
		public new string Pin
		{
			get { return pin; }
			set { pin = value; }
		}
		public bool   IsActivated 
		{
			get { return isActivated;}
			set { isActivated = value; }
		}
		public bool   IsRefilled 
		{
			get { return isRefilled;}
			set { isRefilled = value; }
		}
		public string Msl
		{ 
			get { return msl; }
			set  { msl = value; } 
		}
		public string Msid
		{ 
			get { return msid; }
			set  { msid = value; } 
		}
		public string Mdn
		{ 
			get { return mdn; }
			set  { mdn = value; } 
		}
		public string ControlNumber
		{ 
			get { return controlNumber; }
			set  { controlNumber = value; } 
		}
		new public DictionaryEntry[] Entries 
		{ 
			get 
			{ 
				ArrayList ar = new ArrayList();
				
				DictionaryEntry de = new DictionaryEntry();
				
				de.Key = Const.MSL;
				de.Value = msl;
				ar.Add(de);

				de = new DictionaryEntry();
				de.Key = Const.MSID;
				de.Value = msid;
				ar.Add(de);

				de = new DictionaryEntry();
				de.Key = Const.MDN;
				de.Value = mdn;
				ar.Add(de);

				de = new DictionaryEntry();
				de.Key = Const.CONTROL_NUMBER;
				de.Value = controlNumber;
				ar.Add(de);

				DictionaryEntry[] entries = new DictionaryEntry[ar.Count];
				ar.CopyTo(entries);

				return entries;
			}	
		}
		#endregion

		#region Constructors
		CellPhoneReceipt() : base()
		{
			pin = receipt_Text = errMsg = status = phoneNumber 
				= msl = msid = mdn = controlNumber = string.Empty;
		}
		public CellPhoneReceipt(bool pass, string msg) : this()
		{
			this.pass = pass;
			if (msg != null)
				this.errMsg  = msg;
		}
		public CellPhoneReceipt(bool pass, string msg,  string status) : this(pass, msg)
		{
			if (status != null)
				this.status = status;
		}

		public CellPhoneReceipt(bool pass, string msg,  string status, string receiptText) : this(pass, msg, status)
		{
			if (receiptText != null)
				this.receipt_Text = receiptText;
		}
		#endregion

		#region Methods

	}

	#endregion
}