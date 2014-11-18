using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using MobileTech.ServiceLayer;
using System.Text;
using System.IO;
using System.Reflection;
using MobileTech.Windows.UI.Forms;
using MobileTech.Data;
using MobileTech.Domain;
using System.Xml.Serialization;


namespace MobileTech.Windows.UI
{

    static class Program
    {
        [MTAThread]
        static void Main(string[] args)
        {


            if (args != null)
            {
                foreach (string s in args)
                {
                    string[] split;
                    char[] seperator = { '=' };
                    split = s.Split(seperator);
                    switch (split[0].Trim().ToUpper())
                    {
                        case "/VCRESTART":
                            {
                                if (split[1].ToString().ToUpper() != "TRUE")
                                {
                                    Configuration.VCRestart = false;
                                }
                                else
                                {
                                    Configuration.VCRestart = true;
                                }
                                break;
                            }
                    }
                }
            }

            EventService.Instance.Events += new EventInfoEventHandler(OnEvents);

            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
                    Initialize();
                }
            }
            catch (Exception e)
            {
#if WINCE
                EventService.AddEvent(new MobileTechException(e));

                return;
#else
                Debug.Print(e.Message + e.StackTrace);
#endif
            }

#if WINCE
            Application.Run(Controller.Instance.MainForm);
#else
            SingletonApp.Run(Controller.Instance.MainForm);
#endif
        }


        static void RemoveReadOnlyAttribute(String filePath)
        {
            
            FileInfo fileInfo = new FileInfo(filePath);

            if ((fileInfo.Attributes & FileAttributes.ReadOnly) != 0)
                fileInfo.Attributes -= System.IO.FileAttributes.ReadOnly;
            
        }

        static void Initialize()
        {

            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            Configuration.AppNameFullPath = Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName;
            string dbLocalFile = Host.Configuration.GetString(ConfigurationKey.DbLocalFile);
            Configuration.LoadGlobalConfiguration();
            Configuration.LoadImages();
#if WINCE
            Configuration.DBFullPath = appDir + @"\Database\" + dbLocalFile + ".sdf";
            //Update CE ConnectionString
            Configuration.ConnectionString = String.Format(Configuration.ConnectionString, Configuration.DBFullPath);

#else
            Configuration.DBFullPath = appDir + @"\Database\" + dbLocalFile + ".mdf";
            Configuration.DBLogFullPath = appDir + @"\Database\" + dbLocalFile + "_log.ldf";
            Database.DetachDatabase();
#endif
            if (File.Exists(Configuration.DBFullPath))
            {
                if (Configuration.InitDB)
                {
                    File.Delete(Configuration.DBFullPath);
#if !WINCE

                    if (File.Exists(Configuration.DBLogFullPath))
                    {
                        File.Delete(Configuration.DBLogFullPath);
                    }
#endif

                }
                else
                {
                    if (!Route.IsContainsData())
                    {

                        Database.DetachDatabase();
                        File.Delete(Configuration.DBFullPath);

                        if (File.Exists(Configuration.DBLogFullPath))
                        {
                            File.Delete(Configuration.DBLogFullPath);
                        }
                    }
                    else
                    {
                        Configuration.AppStart = false;

                        Configuration.Route = Route.Current.RouteNumber;
                        Configuration.Location = Route.Current.LocationId;
                    }
                }
            }
#if WINCE
            // This code is to make sure windows animation is turned off
            try
            {
                RegistryKey system = Registry.LocalMachine.OpenSubKey("System");
                RegistryKey gwe = system.OpenSubKey("GWE", true);
                if (gwe == null)
                {
                    system.CreateSubKey("GWE");
                    gwe = system.OpenSubKey("GWE", true);
                }
                gwe.SetValue("Animate", 0);
            }
            catch
            {

            }

#endif
        }
        #region Event handler

        static void OnEvents(IEventInfo eventInfo)
        {
            switch (eventInfo.EventType)
            {
                case EventType.Debug:
                    break;
                case EventType.Log:
                    break;
                case EventType.Warning:
                    MessageBox.Show(eventInfo.EventMessage,
                        Properties.Resources.ApplicationName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                    break;
                case EventType.Exception:
                    if (eventInfo is MobileTechAccessInvalidPasswordException)
                    {
                        MessageBox.Show("Invalid password",
                            Properties.Resources.ApplicationName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
                    }
                    else if (eventInfo is MobileTechAccessDeniedException)
                    {
 
                    }
                    else if (eventInfo is Exception)
                    {
                        ErrorView errorView = new ErrorView(eventInfo as Exception);

                        errorView.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(eventInfo.EventMessage,
                            Properties.Resources.ApplicationName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Hand,
                            MessageBoxDefaultButton.Button1);

                    }

                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}