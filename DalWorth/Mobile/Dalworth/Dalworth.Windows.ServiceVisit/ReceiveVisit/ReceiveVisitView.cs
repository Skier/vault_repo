using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.ReceiveVisit
{
    public partial class ReceiveVisitView : BaseControl
    {
        public ReceiveVisitView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Receive Visit";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_txtNotes, m_btnAccept, m_btnAccept, m_btnAccept, m_btnAccept);
            Joystick.Add(m_btnAccept, m_txtNotes, m_txtNotes, m_txtNotes, m_txtNotes);            
        }
    }
}
