using System;
using System.Text;
using System.Collections;

using DPI.Interfaces;
using DPI.Components;

namespace DPI.Services
{
	public class LoginSvc
	{
		public static bool Validate(string acctName, string pw)
		{
			UOW uow = null; 
			IMap imap = IMapFactory.getIMap();
			
			try
			{
				uow = new UOW(imap, "Login.Validate"); 				
				
				return UserAccount.ValidateUA(uow, acctName, pw); 
			}			
			catch (Exception)
			{
				return false;
			}
			finally
			{	
				imap.ClearDomainObjs();
				uow.close();
			}					
		}		
		public static bool Validate(string acctName, object password, ref IUser user,
			out IPermission[] iperms, out string msg)
		{
			UOW uow = null; 
			iperms = null;
			msg = null;
			IMap imap = IMapFactory.getIMap();
			UserAccount ua = null; 

			try
			{
				uow = new UOW(imap, "Login.Validate"); 				
				
				if (!GetUserAccount(uow, acctName, out ua))
					return false;
				
				if (!ua.Login(uow, ua.AcctName, password, ref user, out iperms, out msg))
					return false;
				
				if (!ValidateCertStoreCode(uow, ua, user.LoginStoreCode))
				{
					iperms = null;
					return false;
				}				
				if (!ValidateCert(ua, user.HasCertificate))
				{
					iperms = null;
					return false;
				}
				return true; 
			}
			catch ( ArgumentNullException)
			{
				msg = "Account configuration error. Please contact you account administrator." ;
				return false;
			}
			catch (Exception)
			{
				return false;
			}
			finally
			{	
				imap.ClearDomainObjs();
				uow.close();
			}					
		}

		public static IUserAcctType GetAcctType(string acctType)
		{
			UOW uow = null;
			if (acctType == null)
				throw new ApplicationException("Acctount type is required");

			try
			{
				uow = new UOW();
				return UserAcctType.find(uow, acctType.Trim());
			}
			finally
			{
				uow.close();
			}
		}
		static bool ValidateCert(UserAccount ua, bool hasCertificate)
		{
			if (!ua.IsCertRequired)
				return true;

			if (hasCertificate)
				return true;

			return false;
		}
		static bool GetUserAccount(UOW uow, string acctName, out UserAccount acct)
		{
			acct = null;
			
		
			if (acctName == null)
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "Account Name is Null");
				throw new ApplicationException("Account Name is required");
			}
			UserAccount[] uas = UserAccount.GetByName(uow, acctName);

			if (uas == null)
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "UserAccount not found");
				return false;
			}

			if (uas.Length == 0)
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "UserAccount not found");
				return false;
			}
			
			acct = uas[0];
			return true;
		}
		static bool ValidateCertStoreCode(UOW uow, UserAccount ua, string storeCode)
		{
			if (!ua.IsCertWithStoreReq)
				return true;
			
			if (storeCode == null)
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "StoreCode is required");
				return false;
			}

			try
			{
				StoreLocation store = StoreLocation.find(uow, storeCode.Trim());
				if (store.Active)
					return true;

				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "StoreCode is required");
				return false;
			} 
			catch (Exception e)
			{
				string s = e.Message;
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Logon.ToString(), "N/A", "Exception: " + e.Message + ", Stack: " + e.StackTrace);
				return false;
			}
		}

		public static IUserAccount GetUserAccount(string userAccount)
		{
			UOW uow = null; 
			IMap imap = IMapFactory.getIMap();
			
			try
			{
				uow = new UOW(imap, "Login.GetUser"); 

				UserAccount[] uas = UserAccount.GetByName(uow, userAccount);
				if (uas == null)
					return null;

				if (uas.Length == 0)
					return null;
				return uas[0];
			}
			finally
			{	
				imap.ClearDomainObjs();
				uow.close();
			}					
		}
		public static bool GetLoginInfo(string publicIP, string privateIP, 
										out string acct, out object password)			
		{
			UOW uow = null; 
			acct = null;
			password = null;
			IMap imap = IMapFactory.getIMap();			
			try
			{
				uow = new UOW(imap, "Login.GetLoginInfo"); 				
				CorpIP cip = CorpIP.find(uow, publicIP);

				if (cip == null)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", "N/A", "CorpIP not found, public ip:" 
						+ publicIP);	
					return false;		
				}
				
				AcctIP aip = AcctIP.getUsingIP(uow, cip.CorpId, privateIP);

				if (aip == null)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", "N/A", "AcctIP not found, private ip:" 
						+ privateIP);	
					return false;
				}
			
				if (aip.AutoLoginAcct == 0)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", 
						"N/A", "AcctIP.AutoLoginAcct is zero, id: " + aip.Id);
					return false;
				}
				UserAccount ua =  UserAccount.find(uow,  aip.AutoLoginAcct);
				
				if (ua == null)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", 
						"N/A", "User Account not found, id: " + aip.AutoLoginAcct);
					return false;
				}

				if (ua.AcctName == null)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", 
						"N/A", "User Account name is null, id: " + ua.AcctId);
					return false;
				}

				if (ua.AcctName.Trim().Length == 0)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", 
						"N/A", "User Account name is empty string, id: " + ua.AcctId);
					return false;
				}
				acct = ua.AcctName;

				if (ua.Password == null)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", 
						"N/A", "User Account password is null, id: " + ua.AcctId);
					return false;
				}

				if (ua.Password.Trim().Length == 0)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", 
						"N/A", "User Account password is empty string, id: " + ua.AcctId);
					return false;
				}
				if (ua.Password != aip.AutoLoginPw)
				{
					ErrLogSvc.LogError(null, "LoginSvc.GetLoginInfo - autologin", 
						"N/A", "User Account password not equal to AcctIP password, ua.id: " + ua.AcctId);
					return false;
				}
				password = ua.Password;
				return true;

			}
			finally
			{
				uow.close();
			}
		}
		public static bool GetIfClerkIDRequested(IUser user)
		{	
			UOW uow = null;			
			try
			{
				uow = new UOW();				
				
				if (((IUserAcctType)UserAcctType.find(uow, user.AcctType)).RequestClerkId
					&& StoreStatsCol.GetCorporation(user.LoginStoreCode).RequestClerkId)
						return true;
				
				return false;
			}
			finally
			{
				uow.close();
			}

		}
		public static ITempAutologin GetTempLoginInfo(string acctName, string password)
		{
			UOW uow = null;

			try
			{
				uow = new UOW();
				TempAutologin ta = new TempAutologin(uow, acctName, password);
				uow.commit();
				return ta;
			}
			catch (Exception ex)
			{
				ErrLogSvc.LogError(null, "LoginSvc.GetTempLoginInfo - autologin", 
					acctName, "Service LoginSvc.GetTempLoginInfo has this error: " + ex.Message);
				return null;
			}
			finally
			{
				uow.close();
			}
		}
		public static ITempAutologin GetTempLoginInfo(string acctName, string password, string token, string storeCode, string transactionType)
		{
			UOW uow = null;

			try
			{
				uow = new UOW();
				TempAutologin ta = new TempAutologin(uow, acctName, password, token, storeCode, transactionType);
				uow.commit();
				return ta;
			}
			catch (Exception ex)
			{
				ErrLogSvc.LogError(null, "LoginSvc.GetTempLoginInfo - autologin", 
					acctName, "Service LoginSvc.GetTempLoginInfo has this error: " + ex.Message);
				return null;
			}
			finally
			{
				uow.close();
			}
		}
		public static ITempAutologin GetAutoLogonByTempKey(int tempKey)
		{
			UOW uow = null; 
			
			try
			{
				uow = new UOW(); 				
				//return TempAutologin.GetAutoLogonById(uow, tempKey);
				TempAutologin ta = TempAutologin.find(uow, tempKey);
				return (ITempAutologin)ta;
			}
			finally
			{
				uow.close();
			}
		}
		public static void DeleteTempAutoLogin(int id)
		{
			UOW uow = null; 
			
			try
			{
				uow = new UOW();
				TempAutologin ta = TempAutologin.find(uow, id);
				ta.delete();
				uow.commit();
			}
			finally
			{
				uow.close();
			}
		}
	}
}