using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.MakePayment
{
    public partial class MakePaymentView : BaseForm
    {
        internal NavigatorButton m_btnAddPayment;
        internal NavigatorCustomButton m_btnDeletePayment;


        public MakePaymentView()
        {
            InitializeComponent();

            m_btnAddPayment = m_gridPayments.EmbeddedNavigator.Buttons.Append;
            m_btnDeletePayment = m_gridPayments.EmbeddedNavigator.CustomButtons[0];
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Dalworth - Make Payment";
        }
    }
}