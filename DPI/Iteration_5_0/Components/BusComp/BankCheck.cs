using System;
using System.Collections.Specialized;

namespace DPI.Components
{
	/// <summary>
	/// Summary description for BankCheck.
	/// </summary>
	public class BankCheck
	{
		private string _bankRoutingNumber;
		private string _bankAccountNumber;
		
		private string _driverLicenseNumber;
		private string _driverLicenseState;

		private string _firstName;
		private string _lastName;
		private string _zip;
		private string _state;
		private string _city;
		private string _streetAddress;
		private string _phoneNumber;
		private string _email;

		public BankCheck(string bankRoutingNumber, string bankAccountNumber, 
			string driverLicenseNumber,
			string driverLicenseState,
			string firstName, string lastName,
			string zip, string state, string city, string address,
			string phoneNumber, string email)
		{
			_bankRoutingNumber = bankRoutingNumber;
			_bankAccountNumber = bankAccountNumber;

			_driverLicenseNumber = driverLicenseNumber;
			_driverLicenseState = driverLicenseState;

			_firstName = firstName;
			_lastName = lastName;
			_zip = zip;
			_state = state;
			_city = city;
			_streetAddress = address;
			_phoneNumber = phoneNumber;
			_email = email;
		}

		public string BankRoutingNumber
		{
			get {return _bankRoutingNumber;}
		}

		public string BankAccountNumber
		{
			get {return _bankAccountNumber;}
		}

		public string DriverLicenseNumber
		{
			get {return _driverLicenseNumber;}
		}

		public string DriverLicenseState
		{
			get {return _driverLicenseState;}
		}

		public string FirstName
		{
			get { return _firstName; }
		}

		public string LastName
		{
			get { return _lastName; }
		}

		public string Zip
		{
			get { return _zip; }
		}

		public string State
		{
			get { return _state; }
		}

		public string City
		{
			get { return _city; }
		}

		public string StreetAddress
		{
			get { return _streetAddress; }
		}

		public string PhoneNumber
		{
			get { return _phoneNumber; }
		}

		public string Email
		{
			get { return _email; }
		}

		public StringCollection Validate()
		{
			StringCollection errors = new StringCollection();


			bool routingNumberPass = false;

			if(BankRoutingNumber != null	
				&& BankRoutingNumber.Length == 9)
			{
				int checkSum = 0;

				for (int i = 0; i < BankRoutingNumber.Length; i += 3) 
				{
					checkSum += int.Parse( BankRoutingNumber[i].ToString()) * 3
						+  int.Parse(BankRoutingNumber[i + 1].ToString()) * 7
						+  int.Parse(BankRoutingNumber[i + 2].ToString());
				}

				routingNumberPass = (checkSum != 0 && checkSum % 10 == 0);
			}
			
			
			if(!routingNumberPass)
				errors.Add("Invalid Bank Routing Number");

			return errors;
		}
	}
}
