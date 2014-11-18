using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.VisitInfo
{
    public partial class VisitInfoView : BaseControl
    {
        public VisitInfoView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Visit Info";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_txtAddress, m_txtNotes, m_linkPhone1, m_txtNotes, m_linkPhone1);
            Joystick.Add(m_linkPhone1, m_txtAddress, m_linkPhone2, m_txtAddress, m_txtNotes);
            Joystick.Add(m_linkPhone2, m_linkPhone1, m_txtNotes, m_txtAddress, m_txtNotes);
            Joystick.Add(m_txtNotes, m_linkPhone2, m_txtAddress, m_linkPhone1, m_txtAddress);            
        }
    }
}
