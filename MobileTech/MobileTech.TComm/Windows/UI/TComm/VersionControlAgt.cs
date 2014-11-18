using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Xml;
using System.Text;

using MobileTech.ServiceLayer.VCHost;
using System.Diagnostics;

namespace MobileTech.Windows.UI.TComm
{
    public class VersionControlAgt
    {
        private string packageFile = "";
        public bool Start()
        {
            HttpComm httpTransfer = new HttpComm();

            string currentVer = Host.Configuration.GetString(ConfigurationKey.VCPackageVer);
            VCListInfo vc = new VCListInfo();
            VersionControlAgent vcAgent = new VersionControlAgent();

            //GetUpdates
            // Create a New 'NetworkCredential' object.
            NetworkCredential networkCredential = new NetworkCredential(Host.Configuration.GetString(ConfigurationKey.VCLogin),
                                                 Host.Configuration.GetString(ConfigurationKey.VCPassword));
            vcAgent.Credentials = networkCredential;
            vcAgent.Timeout = 30000;
            vc = vcAgent.getVCList(1, 1, currentVer);
            if (vc != null)
            {
                try
                {
                    //string localPath = Host.Configuration.GetString(ConfigurationKey.HttpUpdateTemp);
                    //httpTransfer.LocalFile = localPath + @"\" + vc.VCList[0].PackageFile;
                    //httpTransfer.UpdateUrl = Host.Configuration.GetString(ConfigurationKey.HttpUpdateUrl) + vc.VCList[0].PackageFile;
                    //if (!Directory.Exists(localPath))
                    //{
                    //    Directory.CreateDirectory(localPath);
                    //}
                    httpTransfer.Download(vc.VCList[0].PackageFile);
                    packageFile = vc.VCList[0].PackageFile;
                    Configuration.VCUpdate = true;
                }
                catch (Exception)// ex)
                {

                }
            }


            return true;
        }
        public void ExecuteUpdate()
        {
            if (Configuration.VCUpdate)
            {
                string localPath = Host.Configuration.GetString(ConfigurationKey.HttpUpdateTemp);
                string localFile = localPath + @"\" + packageFile;
                string appDir = Path.GetDirectoryName(Configuration.AppNameFullPath);

                //bool retVal = false;
                //string[] args = new string[3];
                Process currProcess = Process.GetCurrentProcess();
                string args = "\"/DESTFOLDER=" + appDir + "\" \"/SRCFILE=" + localFile + "\" \"/PROGRAM=" + Configuration.AppNameFullPath + "\" ";
                args += "\"/SPROCESSID=" + currProcess.Id.ToString() + "\" ";
                string fileName = appDir + @"\" + "VCUpdate.exe";
                Process process = new Process();

                ProcessStartInfo startInfo = new ProcessStartInfo();
                //startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                startInfo.FileName = fileName;
                startInfo.UseShellExecute = false;
                startInfo.Arguments = args;
                process.StartInfo = startInfo;
                //retVal = CreateProcess(appDir + @"\" + "VCUpdate.exe", args, IntPtr.Zero, IntPtr.Zero, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, m_pinfo);
                if (!process.Start())
                {
                    //MessageBox.Show("Unable to re-start Application", "Application Restart Error");
                }

                process.Close();
                process = null;
                Thread.Sleep(5000);
            }

        }
        //[DllImport("coredll.dll", EntryPoint = "CreateProcess", SetLastError = true)]
        //private extern static bool CreateProcess(string pszImageName, string pszCmdLine, IntPtr psaProcess, IntPtr psaThread, int fInheritHandles, int fdwCreate, IntPtr pvEnvironment, IntPtr pszCurDir, IntPtr psiStartInfo, ProcessInfo pi);
        //#region Process Info
        //private sealed class ProcessInfo
        //{
        //    public IntPtr hProcess = IntPtr.Zero;
        //    public IntPtr hThread = IntPtr.Zero;
        //    public int dwProcessID = 0;
        //    public int dwThreadID = 0;
        //}
        //#endregion 

    }
}
