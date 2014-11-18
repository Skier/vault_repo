using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MobileTech.ServiceLayer;
using MobileTech.Domain;
using System.Diagnostics;

namespace MobileTech.Windows.UI.CustomerOperations.Commit
{
	public partial class CustomerOperationsCommitView : BaseForm
    {

        #region Constructors

        public CustomerOperationsCommitView()
		{
			InitializeComponent();
        }

        protected override void ApplyUIResources(System.Globalization.CultureInfo cultureInfo)
        {

            Debug.Assert(m_model != null);

            Resources.Culture = cultureInfo;

            Text = Resources.Title3050;

            m_mbFinal.Text = CommonResources.BtnFinal;
            m_mbBack.Text = CommonResources.BtnBack;

            if (m_model.Transactions.Count > 0)
                m_lbInfo.Text = Resources.PressFinishToCommit;
            else
                m_lbInfo.Text = Resources.NoTransaction;
        }

        #endregion

        #region Event Handlers

        #region OnBackClick

        private void OnBackClick(object sender, EventArgs e)
        {
			WinAPI.CloseWindow(this);
        }

        #endregion

        #region OnFinalClick

        private void OnFinalClick(object sender, EventArgs e)
        {
            try
            {
                if (m_model.Transactions.Count > 0)
                {
                    m_model.AssignDocumentNumbers();

                    //Print reports 
                    //[skiped]

                    m_model.Commit();
                }
                else
                    m_model.Rollback();

   
                using (WaitCursor cursor = new WaitCursor())
                {
                    App.Execute(CommandName.CustomerSelection);
                }

                Destroy();
            }
            catch (Exception ex)
            {
                EventService.AddEvent(
                    new MobileTechException (Resources.EnableToCommitCustomerOperations, 
                    ex ) 
                    );
            }
        }

        #endregion

        #region OnClosing

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (!IsSelfClose)
                App.Execute(CommandName.CustomerOperations, m_model.CommonData);
        }

        #endregion

        #endregion

        #region InitModel

        CustomerOperationsCommitModel m_model;

        public override void InitModel()
        {
            m_model = new CustomerOperationsCommitModel();
        }

        #endregion

        #region BindData

        public override void BindData(Object data)
        {
            if (!(data is CustomerOperationsCommonData))
                throw new MobileTechInvalidModelExeption();

            try
            {
                m_model.Init(data as CustomerOperationsCommonData);

            }
            catch (Exception ex)
            {
                m_model.Rollback();

                throw ex;
            }
        }
        #endregion
    }
}