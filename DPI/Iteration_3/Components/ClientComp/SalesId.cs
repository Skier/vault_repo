using System;
using System.Text;
using System.Collections;

using DPI.Services;
using DPI.Interfaces;

namespace DPI.ClientComp
{
	public class Sales
	{
		public static bool SalesIdReq(IUser user, WIP wip)
		{
			if (!(bool)(wip["SalesIdRequired"]))
				return false;

			if (!LoginSvc.GetIfClerkIDRequested(user))
				return false;

			return true;
		}
		public static bool SalesIdReq(IUser user)
		{
			return (LoginSvc.GetIfClerkIDRequested(user));
		}
	}
}