using System;
using System.Configuration;
using System.IO;

namespace Dalworth.LeadCentral.Phone
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            Host.LogFileWriter = File.CreateText(ConfigurationManager.AppSettings["LogFile"]);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Host.LogFileWriter.Flush();
            Host.LogFileWriter.Close();
        }
    }
}