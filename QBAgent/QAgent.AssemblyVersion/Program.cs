using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QAgent.AssemblyVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFile("C:\\work\\Q-Agent\\Bin\\WinCE\\qagent.setup.dll");
            string version;

            if (args.Length > 0 && args[0] == "full")
            {
                version = assembly.GetName().Version.ToString(4);                              
            }else
            {
                version = assembly.GetName().Version.ToString(3);
            }
            
            
            Console.WriteLine(version);
        }
    }
}
