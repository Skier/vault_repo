using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Menu;
using QuickBooksAgent.Windows.UI.Menu.MainMenu;
using QuickBooksAgent.Windows.UI;
using QuickBooksAgent.QBSDK;
using System.Xml;
using System.Diagnostics;
using QuickBooksAgent.Domain;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace QuickBooksAgent.Mobile
{
    static class Program
    {

        const String REG_PATH = @"HKEY_LOCAL_MACHINE\Software\Apps\Affilia Software Q-Agent";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {


           // QBSessionTicket sessionKey = new QBSessionTicket("95479504",
           //     new QBConnectionTicket("TGT-104-SBT9bsN432Qcg0Tq1bRoMg"));

            //sessionKey.Recieve(
            //"denis.oleynik@gmail.com",
            //"+Winston");

            //Debug.WriteLine(sessionKey.Ticket);

            try
            {
                FileInfo logFile = new FileInfo(Host.GetPath("log.txt"));
                if (logFile.Length > 1000000)
                    logFile.Delete();
            }
            catch (Exception) {}                        
            
                using (StreamWriter streamWriter = File.AppendText(Host.GetPath("log.txt")))
                {
                    Host.LogFileWriter = streamWriter;

                    try
                    {
                        Configuration.RemoveConfigReadOnlyAttribute();

                        Configuration.LoadGlobalConfiguration();

                        Host.Trace("Program::Main", "Application started");

                        Configuration.DBFullPath = Host.GetPath(@"\Database\QAgent.sdf");
                        Configuration.ConnectionString = String.Format(Configuration.ConnectionString, Configuration.DBFullPath);

                        if (Configuration.InitDB)
                        {
                            if (File.Exists(Configuration.DBFullPath))
                            {
                                if (MessageDialog.Show(MessageDialogType.Question,
                                    "Database will be removed.\nDo you want to start application?") == DialogResult.Yes)
                                    File.Delete(Configuration.DBFullPath);
                                else
                                    return;

                            }

                            Configuration.InitDB = false;

                            Configuration.Save();
                        }


                        if (bool.Parse(GetRegistryValue(REG_PATH, "UpdateSettings", "false").ToString()))
                        {
                            bool settingsUpdated = false;
                            Object value = GetRegistryValue(REG_PATH, "ConnectionTicket", String.Empty);

                            if (value.ToString() != String.Empty && 
                                value.ToString() != Configuration.QuickBooks.ConnectionTicket)
                            {
                                Configuration.QuickBooks.ConnectionTicket =
                                    value.ToString();

                                settingsUpdated = true;
                            }


                            value = GetRegistryValue(REG_PATH, "QuickBooksLogin", String.Empty);


                            if (value.ToString() != String.Empty &&
                                value.ToString() != Configuration.QuickBooks.DefaultLogin)
                            {
                                Configuration.QuickBooks.DefaultLogin =
                                    value.ToString();

                                settingsUpdated = true;
                            }

                            if (settingsUpdated)
                            {
                                Configuration.Save();
                            }

                            Registry.SetValue(REG_PATH, "UpdateSettings", "false");
                        }


                        EventService.Instance.Events += new EventInfoEventHandler(OnEvents);

                        MainFormController.Register(SingleFormController.Prepare<MainMenuController>());

                        SessionModel.Instance.LoginInfo.Login =
                            Configuration.QuickBooks.DefaultLogin;

                        Application.Run(MainFormController.Instance.Form);


                        Host.Trace("Program::Main", "Application stoped");
                    }
                    catch (Exception e)
                    {
                        EventService.AddEvent(
                            new QuickBooksAgentException("Unknown application error", e));

                        Host.Trace("Program::Main", "Application crashed");
                    }
                    finally
                    {
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
        }
        
        private static object GetRegistryValue(string keyName, string valueName, object defaultValue)
        {
            object value = Registry.GetValue(keyName, valueName, defaultValue);
            if (value == null)
                return defaultValue;
            return value;
        }

        static void OnEvents(IEventInfo eventInfo)
        {
            if (eventInfo is Exception)
            {
                Exception e = ((Exception)eventInfo);


                MessageDialog.Show(e);

                    Host.Trace("Program::OnEvents",
                        e.Message);

                    StringBuilder stringBuilder = new StringBuilder();

                    while (e != null)
                    {
                        stringBuilder.Append(e.Message);
                        stringBuilder.Append(":\n");
                        stringBuilder.Append(e.StackTrace);
                        stringBuilder.Append(new String('-', 20));

                        e = e.InnerException;
                    }

                    Host.Trace("Program::OnEvents",
                        stringBuilder.ToString());
            }
        }
    }
}