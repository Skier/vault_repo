using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.SubmitEtc
{
    public class SubmitEtcController : SingleFormController<SubmitEtcModel, SubmitEtcView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Visit = (VisitPackage) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = true;
            IsRightActionExist = true;
            LeftActionName = "Cancel";
            RightActionName = "Done";

            View.m_lblTaskNumber.Text = Model.Visit.Tasks[0].Task.Number;
            View.m_lblCustomerName.Text = Model.Visit.Customer.FirstName + ", " + Model.Visit.Customer.LastName;
            View.m_lblTaskType.Text = TaskType.GetText((TaskTypeEnum)Model.Visit.Tasks[0].Task.TaskTypeId);

            View.m_curSale.Focus();
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            View.Destroy();            
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            try
            {
                using (new WaitCursor())
                {
                    Database.Begin();
                    Model.SubmitEtc(View.m_curSale.Value ?? decimal.Zero, 
                        View.m_cmbEtcHH.SelectedIndex <= 0 ? (int?)null : int.Parse(View.m_cmbEtcHH.SelectedItem.ToString()),
                        View.m_cmbEtcMM.SelectedIndex <= 0 ? (int?)null : int.Parse(View.m_cmbEtcMM.SelectedItem.ToString()),
                        View.m_txtNotes.Text);
                    Database.Commit();
                    WorkTransaction.Send();
                    View.Destroy();
                }
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("SubmitEtcController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }
        }

        #endregion
    }
}
