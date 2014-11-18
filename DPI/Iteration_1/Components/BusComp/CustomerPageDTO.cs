using System;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class CustomerPageDTO : ICustomerPageDTO
	{
		#region Member Variables

		private int accNumber;
		private string lastName;
		private string firstName;
		private string birthday;
		private string email;
		private string contact;
		private string contact2;
		private string prevPhone;
		private string prevILEC;		
		private string status;
		private string storeName;

		#endregion

		#region Properties

		public int AccNumber
		{
			get { return accNumber; }
			set { accNumber = value; }
		}

		public string LastName 
		{
			get { return lastName; }
			set { lastName = value; }
		}

		public string FirstName 
		{
			get { return firstName; }
			set { firstName = value; }
		}

		public string Birthday
		{
			get { return birthday; }
			set { birthday = value; }
		}

		public string Email 
		{
			get { return email; }
			set { email = value; }
		}

		public string Contact 
		{
			get { return contact; }
			set { contact = value; }
		}

		public string Contact2 
		{
			get { return contact2; }
			set { contact2 = value; }
		}

		public string PrevPhone
		{
			get { return prevPhone; }
			set { prevPhone = value; }
		}

		public string PrevILEC
		{
			get { return prevILEC; }
			set { prevILEC = value; }
		}

		public string Status
		{
			get { return status; }
			set { status = value; }
		}

		public string StoreName
		{
			get { return storeName; }
			set { storeName = value; }
		}

		#endregion
	}
}
