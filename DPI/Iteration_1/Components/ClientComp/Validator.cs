using System;

namespace DPI.ClientComp
{
	public class Validator
	{
		public static bool ReqField(string text)
		{
			if (text == null)
				return false;

			if (text.Trim().Length == 0)
				return false;

			return true;
		}
	}
}			