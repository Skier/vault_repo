using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class CustomerFilter : ICustomerFilter
	{
		#region Member Variable

		private string _accNumber;
		private string _phoneNumber;
		private string _lastName;
		private string _firstName;
		private IAddr _addr;

		#endregion

		#region Properties

		public string AccNumber
		{
			get { return _accNumber; }
			set { _accNumber = value; }
		}

		public string PhoneNumber
		{
			get { return _phoneNumber; }
			set { _phoneNumber = value; }
		}

		public string LastName
		{
			get { return _lastName; }
			set { _lastName = value; }
		}

		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		public IAddr Addr
		{
			get { return _addr; }
			set { _addr = value; }
		}

		#endregion
	}
}
