using System;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class FormatPhone
	{
	//	public static string PhoneNum(string npa, string nxx, string line)
	//	{
	//		return string.Concat(npa, nxx, line);
	//	}

		public static string ShowPhone(PhoneNumParts part, string stringVal)
		{
			if (stringVal == null)
				return null;

			if (stringVal.Trim().Length != 10)
				return null;

			switch (part) 
			{
				case PhoneNumParts.Npa :
					return stringVal.Substring(0, 3); 
				
				case PhoneNumParts.Nxx :
					return stringVal.Substring(3, 3); 

				case PhoneNumParts.Line :
					return stringVal.Substring(6, 4); 

				default:
					throw new ArgumentException("No such attribute: " + ((PhoneNumParts)part).ToString());
			}
		}
		public static string Format(string phone)
		{
			if (phone.Length != 10)
				throw new ArgumentException("Phone must 10 numbers long.");
			//string.Format("format", params, params)
			//string.Format("{format1}-{format2}", format1, format2)
			return string.Format("{0}-{1}-{2}",
				phone.Substring(0, 3),
				phone.Substring(3, 3), 
				phone.Substring(6, 4)
				);
		}
		public static string Format(int phone)
		{
			return Format(phone.ToString());
		}
	}
}