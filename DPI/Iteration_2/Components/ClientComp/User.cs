using System;
using System.Web;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	[Serializable] 
	public class User : IUser
	{
	#region Data
		int	   acctId;
		string displayName;	
		string clerkId;
		string acctType;
		bool hasCertificate;
		string loginStoreCode;
		string jobTitle;
		string token;
		string storeNumber;
		string role; 

	#endregion

	#region Properties
		public int AcctId
		{
			get { return acctId;  }
			set { acctId = value; }
		}
		public string DisplayName 
		{ 
			get { return displayName; }	
			set { displayName = value; }
		}
		public string ClerkId
		{ 
			get { return clerkId; }	
			set { clerkId = value; }
		}
		public string AcctType
		{
			get { return acctType;  }
			set { acctType = value; }
		}
		public bool HasCertificate
		{
			get { return hasCertificate;  }
			set { hasCertificate = value; }
		}
		public string LoginStoreCode
		{
			get { return loginStoreCode;  }
			set { loginStoreCode = value; }
		}
		public string JobTitle
		{
			get { return jobTitle; }
			set { jobTitle = value; }
		}
		public string Token
		{
			get { return token; }
			set { token = value; }
		}
		public string StoreNumber
		{
			get { return storeNumber;	}
			set { storeNumber = value;	}
		}
		public string Role
		{
			get { return role;	}
			set { role = value;	}
		}
	#endregion

	#region Constructors
	#endregion

	#region Methods
		public static IUser GetUser(System.Web.UI.UserControl control)
		{
			IUser user = (IUser)(control.Session["User"]);
			
			if (user == null) 
				throw new ApplicationException("Session expired");
			
			return user;
		}
		public static IUser GetUser(System.Web.UI.Page page)
		{
			IUser user = (IUser)(page.Session["User"]);
			
			if (user == null) 
				throw new ApplicationException("Session expired");
			
			return user;
		}
		public static void Save(System.Web.UI.Page page, IUser user)
		{
			if (user == null) 
				throw new  ArgumentException("User is required");

			page.Session["User"] = user;
		}
		public static void Save(System.Web.UI.UserControl control, IUser user)
		{
			if (user == null) 
				throw new  ArgumentException("User is required");
			
			control.Session["User"] = user;
		}
	#endregion
	}
}