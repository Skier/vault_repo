using System;

using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{	
	public class UserAccountSvc
	{
		public static IUserAccountExtension GetExtendedUser(int acctId, string entityName)
		{
			UOW uow = null; 
			
			try
			{
				uow = new UOW("UserAccountSvc.GetExtendedUser");
				
				return UserAccountExtension.GetExtendedUser(uow, acctId, entityName);
			}
			finally
			{
				uow.close();
			}
		}		
	}
}