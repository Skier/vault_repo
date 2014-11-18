using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.IO;
using WatiN.Core;
using System.Threading;

using Dalworth.Server.SDK;
using Dalworth.Server.Domain;

namespace Dalworth.PpcTesterWin
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

            Configuration.RemoveConfigReadOnlyAttribute();
            Configuration.LoadGlobalConfiguration();

            Application.Run(new MainForm());
        }
    }
}
