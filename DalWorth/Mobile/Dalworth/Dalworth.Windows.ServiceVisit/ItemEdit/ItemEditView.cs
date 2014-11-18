using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.ItemEdit
{
    public partial class ItemEditView : BaseControl
    {
        public ItemEditView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Add Rug";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_cmbShape, m_curOther, m_txtDiameter, m_curOther, m_txtDiameter);
            Joystick.Add(m_txtDiameter, m_cmbShape, m_txtWidth, m_cmbShape, m_txtWidth);
            Joystick.Add(m_txtWidth, m_txtDiameter, m_txtHeight, m_txtDiameter, m_chkProtector);
            Joystick.Add(m_txtHeight, m_txtWidth, m_chkProtector, m_cmbShape, m_chkProtector);
            Joystick.Add(m_chkProtector, m_txtHeight, m_chkPadding, m_txtWidth, m_chkPadding);
            Joystick.Add(m_chkPadding, m_chkProtector, m_chkMothRepel, m_chkProtector, m_chkMothRepel);
            Joystick.Add(m_chkMothRepel, m_chkPadding, m_chkRap, m_chkPadding, m_chkRap);
            Joystick.Add(m_chkRap, m_chkMothRepel, m_curOther, m_chkMothRepel, m_curOther);
            Joystick.Add(m_curOther, m_chkRap, m_cmbShape, m_chkRap, m_cmbShape);            
        }
    }
}
