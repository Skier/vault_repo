using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Dalworth.Domain;
using Dalworth.SDK;
using Dalworth.Windows;
using Dalworth.Windows.Menu.ConnectionKeyPassword;
using Dalworth.Windows.Menu.MainMenu;
using Microsoft.Win32;
using Application = System.Windows.Forms.Application;

namespace Dalworth.Mobile
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {            
            try
            {
                FileInfo logFile = new FileInfo(Host.GetPath("log.txt"));
                if (logFile.Length > 1000000)
                    logFile.Delete();
            }
            catch (Exception) { }

            using (StreamWriter streamWriter = File.AppendText(Host.GetPath("log.txt")))
            {
                Host.LogFileWriter = streamWriter;

                try
                {
                    Configuration.RemoveConfigReadOnlyAttribute();

                    Configuration.LoadGlobalConfiguration();

                    Host.Trace("Program::Main", "Application started");

                    Configuration.DBFullPath = Host.GetPath(@"\Database\Dalworth.sdf");
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

                    EventService.Instance.Events += new EventInfoEventHandler(OnEvents);

                    MainFormController.Register(SingleFormController.Prepare<ConnectionKeyPasswordController>());

                    Application.Run(MainFormController.Instance.Form);


                    Host.Trace("Program::Main", "Application stoped");
                }
                catch (Exception e)
                {
                    EventService.AddEvent(
                        new DalworthException("Unknown application error", e));

                    Host.Trace("Program::Main", "Application crashed");
                }
                finally
                {
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
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