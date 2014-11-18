using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickBooksAgent.Windows.UI.Setup.Menu
{
    public partial class SetupMenuView : BaseControl
    {

        internal MenuItem m_menuConnection = new MenuItem();
        internal MenuItem m_menuRegister = new MenuItem();
        internal MenuItem m_menuApplication = new MenuItem();
        internal MenuItem m_menuAbout = new MenuItem();

        public SetupMenuView()
        {
            InitializeComponent();


            m_menuConnection.Enabled = false;
            m_menuConnection.Text = "Connection";
            m_menuRegister.Text = "Register";
            m_menuApplication.Text = "Application";
            m_menuAbout.Text = "About";

            //MenuItems.Add(m_menuConnection);

            MenuItems.Add(m_menuRegister);            
            MenuItems.Add(m_menuApplication);            
            MenuItems.Add(m_menuAbout);            
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Setup - Q-Agent";
        }

        protected override void OnInit()
        {
            base.OnInit();

            //Joystick.Add(m_mbConnection, m_mbApplication, m_mbApplication, m_mbConnection, m_mbConnection);
            Joystick.Add(m_btnRegister, m_btnAbout, m_mbApplication, m_btnAbout, m_btnAbout);
            Joystick.Add(m_mbApplication, m_btnRegister, m_btnAbout, m_btnAbout, m_btnAbout);
            Joystick.Add(m_btnAbout, m_mbApplication, m_btnRegister, m_btnRegister, m_btnRegister);
        }
    }
}
