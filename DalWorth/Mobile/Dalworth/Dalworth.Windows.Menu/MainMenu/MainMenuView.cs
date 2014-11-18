using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;

namespace Dalworth.Windows.Menu.MainMenu
{
    public partial class MainMenuView : BaseControl
    {       
        public MainMenuView()
        {
            InitializeComponent();
            m_btnStartDay.Picture = ImageKeys.StartDay;
            m_btnEndDay.Picture = ImageKeys.EndDay;            
        }
        
        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Main Menu";
        }
    }
}