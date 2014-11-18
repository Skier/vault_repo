using System;
using DPI.Interfaces;

namespace DPI.Components
{
	[Serializable]  
	public class AddressPage : IAddressPageDTO
	{
		#region Member Variables

		int accNumber;
		IAddr mailAddress;

		#endregion

		#region Properties

		public int AccNumber
		{
			get { return accNumber; }
			set { accNumber = value; }
		}

		public IAddr MailAddress
		{
			get { return mailAddress; }
			set { mailAddress = value; }
		}

		#endregion
	}
}
