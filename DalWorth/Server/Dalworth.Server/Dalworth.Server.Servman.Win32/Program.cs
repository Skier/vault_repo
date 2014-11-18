using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.SDK;
using Dalworth.Server.Servman.Win32.MainForm;
using Dalworth.Server.Windows;

namespace Dalworth.Server.Servman.Win32
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run();


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