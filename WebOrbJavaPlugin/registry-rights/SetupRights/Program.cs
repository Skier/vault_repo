using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Security;

namespace SetupRights
{
    class Program
    {
        static void Main(string[] args)
        {
             SetupASPNETUserRights(
                Registry.LocalMachine.OpenSubKey("SOFTWARE", true), 
                GetASPNETUserName());
        }

        static void SetupASPNETUserRights(RegistryKey keyForSetupRights, string userName) {
            RegistrySecurity accControl =  keyForSetupRights.GetAccessControl();
            accControl.AddAccessRule(new RegistryAccessRule(userName, 
                RegistryRights.CreateSubKey 
                    | RegistryRights.ReadKey 
                    | RegistryRights.WriteKey 
                    | RegistryRights.Delete
                    | RegistryRights.QueryValues
                    | RegistryRights.SetValue,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, 
                PropagationFlags.None,  
                AccessControlType.Allow));
            keyForSetupRights.SetAccessControl(accControl);
        }

        static string GetASPNETUserName() {
            return Environment.MachineName + "\\ASPNET";
        }
    }
}
