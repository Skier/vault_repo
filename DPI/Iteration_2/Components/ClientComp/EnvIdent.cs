using DPI.Interfaces;
using DPI.Components;

namespace DPI.ClientComp
{
	public class EnvIdent
	{
		public static string ReceiptHead1
		{ 
			get 
			{
  				string s = ""; 
				if (Conn.Env == Const.PROD)
					s = ": ";
				
				return "Customer Service Toll Free" + s + " 1-800-350-4009";
			}
		}
	}
}