using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using MobileTech.ServiceLayer;
using System.Globalization;

namespace MobileTech.Windows.UI.TComm
{
    public partial class TCommView : BaseForm
    {
        #region Constructors

        public TCommView()
		{
			InitializeComponent();
        }
        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            Resources.Culture = cultureInfo;

            Text = Resources.Title7000;

            m_btBegin.Text = Resources.Begin;
            m_btDone.Text = Resources.Done;
            m_message.Text = Resources.Message_HitBegin;
        }       
#endregion

        #region Event Handlers

        private void OnBeginClick(object sender, EventArgs e)
		{
            using (WaitCursor cursor = new WaitCursor())
            {

                try
                {
                    m_lstDetail.Items.Clear();
                    if (m_model.DoTCom())
                    {
                        m_btDone.Focus();
                    }
                    else
                    {
                        m_btBegin.Focus();
                    }
                }
                catch (MobileTechException ex)
                {
                    EventService.AddEvent(ex);
                }
                catch (Exception ex)
                {
                    EventService.AddEvent(new MobileTechException(ex));
                }
            }
		}

        private void OnDoneClick(object sender, EventArgs e)
        {
            WinAPI.CloseWindow(this);
        }
        private void OnClosing(object sender, CancelEventArgs e)
        {
            App.Execute(CommandName.MainMenu);
        }

        #endregion

        #region IView Members

        TCommModel m_model;

        public override void BindData(Object data)
        {
            if (!(data is TCommModel))
                throw new MobileTechInvalidModelExeption();

            m_model = (TCommModel)data;
            m_model.Messages += new TCommMessageEvent(m_model_Messages); 
            m_model.Progress += new TCommProgressEvent(m_model_Progress);
            m_model.DetailMessages += new TCommDetailMessageEvent(m_model_DetailMessages);
            m_model.DetailProgress += new TCommDetailProgressEvent(m_model_DetailProgress);
        }

        void m_model_Progress(int percentComplete)
        {
            //m_progress.Value = percentComplete;
        }

        void m_model_Messages(string message)
        {
            m_message.Text = message;
            m_lstDetail.Items.Add(message);
            m_lstDetail.SelectedIndex = m_lstDetail.Items.Count-1;
            m_message.Refresh();
            m_lstDetail.Refresh();
        }
        void m_model_DetailProgress(int percentComplete)
        {
            if (percentComplete == 100)
            {
                m_progress.Visible = false;
            }
            else
            {
                m_progress.Visible = true;
                m_progress.Value = percentComplete;
            }
        }

        void m_model_DetailMessages(string message)
        {
            m_lstDetail.Items.Add(message);
            m_lstDetail.SelectedIndex = m_lstDetail.Items.Count-1;
            m_lstDetail.Refresh();
        }
        #endregion
    }
}