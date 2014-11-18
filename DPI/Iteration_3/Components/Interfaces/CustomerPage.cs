using System;

namespace DPI.Interfaces
{
	[Serializable]  
	public class CustomerPage : ICustomerPage
	{
		#region Member Variables

		string lastName;
		string firstName;
		string birthday;
		string email;
		string contact;
		string contact2;
		string prevPhone;
		string prevILEC;
		int accNumber;

		#endregion

		#region Properties

		public string FormattedName 
		{
			get 
			{
				return lastName;
			}
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

		public int AccNumber
		{
			get { return accNumber; }
			set { accNumber = value; }
		}

		#endregion
	}
}
