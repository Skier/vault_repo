using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace QuickBooksAgent.Windows.UI.Setup.About
{
    public class AboutController : SingleFormController<AboutModel, AboutView>
    {
        public override void OnViewLoad()
        {
            base.OnViewLoad();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            View.m_lblVersion.Text = version.Major + "." + version.Minor;
            View.m_lblBuild.Text = version.Build.ToString();
        }
    }
}
