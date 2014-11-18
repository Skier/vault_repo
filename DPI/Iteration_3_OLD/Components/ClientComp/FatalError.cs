using System;
using System.Text;
using System.Collections;
using System.Web.UI;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.ClientComp
{
	public class FatalError
	{
		public static void ShowErr(System.Web.UI.WebControls.PlaceHolder phError, string msg)
		{
			phError.Controls.Add(new ErrorTable(msg));
			phError.Visible = true;
		}
		public static void SaveErr(IMap imap, string msg, object user, string sender)
		{
			ErrLogSvc.LogError(imap, sender, GetClerk(user),  msg);
		}
		public static void SaveErr(IMap imap, Exception ex, object user, string sender)
		{
			ErrLogSvc.LogError(imap, sender, GetClerk(user),  ex.Message + ", " + ex.StackTrace);
		}
		public static void SaveErr(Exception ex, object user, string sender)
		{
			IMap imap = IMapFactory.getIMap();
			SaveErr(imap, ex, GetClerk(user), sender);
		}
		static string GetClerk(object user)
		{
			if (user == null)
				return "N/A";

			if (!(user is IUser))
				return "N/A";

			return "Storecode: " + ((IUser)user).LoginStoreCode	+ ", clerkid: " + ((IUser)user).ClerkId;
		}
	}
}