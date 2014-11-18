using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.StartDay.VanCheck
{
    public partial class VanCheckView : BaseControl
    {
        internal MenuItem m_menuCancel;
        internal MenuItem m_menuBack;

        public VanCheckView()
        {
            InitializeComponent();
            m_menuCancel = new MenuItem();
            m_menuBack = new MenuItem();
            MenuItemsLeft.Add(m_menuCancel);
            MenuItemsLeft.Add(m_menuBack);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Van Checklist";
            m_menuCancel.Text = "Cancel";
            m_menuBack.Text = "Back";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_chkOilChecked, m_txtSpecialNeeds, m_chkUnitClean, m_txtSpecialNeeds, m_chkVanClean);
            Joystick.Add(m_chkUnitClean, m_chkOilChecked, m_chkVanClean, m_txtSpecialNeeds, m_chkSuppliesStocked);
            Joystick.Add(m_chkVanClean, m_chkUnitClean, m_chkSuppliesStocked, m_chkOilChecked, m_txtOdometer);
            Joystick.Add(m_chkSuppliesStocked, m_chkVanClean, m_txtOdometer, m_chkUnitClean, m_txtOdometer);
            Joystick.Add(m_txtOdometer, m_chkSuppliesStocked, m_txtHoursMeter, m_chkSuppliesStocked, m_txtHoursMeter);
            Joystick.Add(m_txtHoursMeter, m_txtOdometer, m_txtSpecialNeeds, m_txtOdometer, m_txtSpecialNeeds);
            Joystick.Add(m_txtSpecialNeeds, m_txtHoursMeter, m_chkOilChecked, m_txtHoursMeter, m_chkOilChecked);            
        }
    }
}
