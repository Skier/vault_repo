using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.ServiceVisit.VisitReceipt
{
    public partial class VisitReceiptView : BaseControl
    {
        internal MenuItem m_menuPayByCC;
        internal MenuItem m_menuPayByCheck;
        internal MenuItem m_menuPayByCash;

        public VisitReceiptView()
        {
            InitializeComponent();

            m_menuPayByCC = new MenuItem();
            m_menuPayByCheck = new MenuItem();
            m_menuPayByCash = new MenuItem();

            m_menuPayByCC.Text = "Pay By CC";
            m_menuPayByCheck.Text = "Pay By Check";
            m_menuPayByCash.Text = "Pay By Cash";

            MenuItemsRight.Add(m_menuPayByCC);
            MenuItemsRight.Add(m_menuPayByCheck);
            MenuItemsRight.Add(m_menuPayByCash);
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Visit Receipt";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_curService, m_btnPayByCash, m_btnPayByCC, m_btnPayByCash, m_btnPayByCash);
            Joystick.Add(m_btnPayByCash, m_btnPayByCheck, m_curService, m_curService, m_curService);
            Joystick.Add(m_btnPayByCC, m_curService, m_btnPayByCheck, m_curService, m_curService);
            Joystick.Add(m_btnPayByCheck, m_btnPayByCC, m_btnPayByCash, m_curService, m_curService);            
        }
    }
}
