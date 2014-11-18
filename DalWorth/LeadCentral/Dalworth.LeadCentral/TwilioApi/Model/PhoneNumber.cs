using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twilio.Model
{
	/// <summary>
	/// Placeholder for now
	/// </summary>
	public class PhoneNumber
	{
		public PhoneNumber(string number)
		{
			Raw = number;
		}

		public string AreaCode { get; set; }
		public string Raw { get; set; }
		public string E164 { get; set; }
		public string Formatted { get; set; }
		public string CountryCode { get; set; }
	}
}
