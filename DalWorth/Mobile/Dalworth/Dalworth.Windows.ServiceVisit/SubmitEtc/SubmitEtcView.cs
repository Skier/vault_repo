using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.SubmitEtc
{
    public partial class SubmitEtcView : BaseControl
    {
        public SubmitEtcView()
        {
            InitializeComponent();
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Submit ETC";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_curSale, m_txtNotes, m_cmbEtcHH, m_txtNotes, m_cmbEtcMM);
            Joystick.Add(m_cmbEtcHH, m_curSale, m_cmbEtcMM, m_curSale, m_txtNotes);
            Joystick.Add(m_cmbEtcMM, m_cmbEtcHH, m_txtNotes, m_curSale, m_txtNotes);
            Joystick.Add(m_txtNotes, m_cmbEtcMM, m_curSale, m_cmbEtcHH, m_curSale);            
        }
    }
}
