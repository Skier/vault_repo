using System;

namespace Dpi.Central.Web.Account
{
	/// <summary>
	/// This class will contain everyting that is put in session object.
	/// </summary>
	public class DpiSession
	{
		public const string DPI_SESSION_KEY = "dpi_session_key";


		private  bool _isAuthenticated = false;

		public DpiSession()
		{
		}

		public bool IsAuthenticated 
		{
			get 
			{
				return _isAuthenticated;
			}
			set 
			{
				_isAuthenticated = value;
			}
		}
	}
}
