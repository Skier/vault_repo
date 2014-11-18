using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Dalworth.Server.SDK;

namespace Dalworth.Server
{
    public class Impersonisation : IDisposable
    {
        public Impersonisation()
        {
            ImpersonateValidUser(Configuration.Login, string.Empty, Configuration.Password);
        }

        public void Dispose()
        {
            if (m_impersonationContext != null)
            {
                m_impersonationContext.Undo();
                m_impersonationContext.Dispose();
            }                
        }

        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        private WindowsImpersonationContext m_impersonationContext;


        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken, int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        private void ImpersonateValidUser(string userName, string domain, string password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        m_impersonationContext = tempWindowsIdentity.Impersonate();
                        if (m_impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return;
                        }
                    }
                }
            }

            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);

            throw new DalworthException("Unable to Impersonate User");
        }
    }
}
