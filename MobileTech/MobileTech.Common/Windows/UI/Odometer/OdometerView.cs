using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MobileTech.ServiceLayer;
using System.Globalization;

namespace MobileTech.Windows.UI.Odometer
{
    public partial class OdometerView : BaseForm
    {
        #region Constructor

        public OdometerView()
        {
            InitializeComponent();
        }

        #endregion

        #region ApplyUIResources
        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            m_mbDone.Text = CommonResources.BtnDone;
            m_lbOdometerReading.Text = Resources.OdometerReading;

            Text = Resources.Title;
        }
        #endregion

        #region BindData

        IOdometerModel m_model;

        public override void BindData(Object data)
        {
            if (!(data is IOdometerModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (IOdometerModel)data;
            m_txtReading.Text = m_model.OdometerReading.ToString();
        }

        #endregion

        #region Event handlers

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

#if WINCE
            m_txtReading.Focus();
#else
            m_txtReading.Select();
#endif
            m_txtReading.SelectAll();
        }

        #endregion


        #region OnCancel

        protected override bool OnCancel()
        {
            // true means do not allow the form to be
            // closed by means of an X or an Esc keypress
            return true;
        }


        #endregion
        
        #region OnDone

        void OnDone()
        {
            try
            {
                m_model.OdometerReading = int.Parse(m_txtReading.Text);

                if (m_model.OdometerReading == 0)
                {
                    MessageDialog.Show(MessageDialogType.Information,
                        CommonResources.MsgInvalidEntry);
                }
                else
                {
                    Destroy();
                }

            }
            catch (Exception ex)
            {
                EventService.AddEvent(new MobileTechException(
                    Resources.ExEnableToApplyOdometer,
                    ex));
            }

        }

        #endregion

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnDone();
        }

        #endregion

        #region OnKeyPress

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                e.Handled = true;
        }

        #endregion

        #region OnDoneClick

        private void OnDoneClick(object sender, EventArgs e)
        {
            OnDone();
        }

        #endregion

        #endregion

    }
}