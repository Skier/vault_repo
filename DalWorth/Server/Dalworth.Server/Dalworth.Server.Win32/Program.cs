using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;

namespace Dalworth.Server.Win32
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           /* Process[] RunningProcesses = Process.GetProcessesByName("Dalworth.Server.Win32");
            if (RunningProcesses.Length > 1)
            {
                WinAPI.ShowWindowAsync(
                    RunningProcesses[0].MainWindowHandle, (int)WinAPI.ShowWindowConstants.SW_SHOWMINIMIZED);
                WinAPI.ShowWindowAsync(
                    RunningProcesses[0].MainWindowHandle, (int)WinAPI.ShowWindowConstants.SW_RESTORE);
                return;
            }*/
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run();

            try
            {
                FileInfo logFile = new FileInfo(Host.GetPath("log.txt"));
                if (DateTime.Now.Date > logFile.CreationTime.Date)
                {
                    if (!Directory.Exists(Host.GetPath(@"Log\")))
                        Directory.CreateDirectory(Host.GetPath(@"Log\"));
                    DateTime arcivedLogDate = logFile.CreationTime.Date;
                    logFile.CreationTime = DateTime.Now;
                    logFile.MoveTo(Host.GetPath(@"Log\") + string.Format("log{0}-{1}-{2}.txt",
                        arcivedLogDate.Year, arcivedLogDate.Month.ToString("00"), arcivedLogDate.Day.ToString("00")));
                }
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

                    using (MainFormController controller = Controller.Prepare<MainFormController>())
                    {   
                        Application.Run(controller.Form);
                    }

                    Host.Trace("Program::Main", "Application stoped");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Unknown Application Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    Host.Trace("Program::Main", "Application crashed " + e);
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