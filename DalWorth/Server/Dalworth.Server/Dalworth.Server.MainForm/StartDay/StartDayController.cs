using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.StartDay
{
    public class StartDayController : Controller<StartDayModel, StartDayView>
    {
        #region IsCancelled 

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion 

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Work = (Work) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_gridViewEquipmentSummary.RowCellStyle += OnEquipmentSummaryRowCellStyle;            
            View.m_timeStartDay.Validating += OnTimeStartDayValidating;
            View.m_txtVanEquipment.TextChanged += OnVanEquipmentChanged;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblTechnicianName.Text = Model.Technician.DisplayName;
            View.m_lblVan.Text = Model.Van.LicensePlateNumber;
            View.m_lblWorkDate.Text = Model.Work.StartDate.Value.ToShortDateString();
            View.m_gridRugs.DataSource = Model.WorkItems;

            View.m_txtVanEquipment.Quantities = Model.VanEquipment;
            View.m_gridEquipmentSummary.DataSource = Model.EquipmentSummaries;

            View.m_timeStartDay.Time = Model.Work.StartDayDate.HasValue ? 
                Model.Work.StartDayDate.Value : Model.GetDefaultStartDayTime();
        }

        #endregion

        #region OnTimeStartDayValidating

        private void OnTimeStartDayValidating(object sender, CancelEventArgs e)
        {
            if (Model.Work.StartDate.Value.Date == DateTime.Now.Date)
            {
                if (View.m_timeStartDay.Time.TimeOfDay > DateTime.Now.TimeOfDay)
                    View.m_errorProvider.SetError(View.m_timeStartDay, "Start Day time cannot be in the future");
                else
                    View.m_errorProvider.SetError(View.m_timeStartDay, string.Empty);
            }
        }

        #endregion

        #region OnVanEquipmentChanged

        private void OnVanEquipmentChanged(object sender, EventArgs eventArgs)
        {
            Model.UpdateSummaries(View.m_txtVanEquipment.Quantities);
        }

        #endregion


        #region Red Cell

        private void OnEquipmentSummaryRowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.Name == View.m_colDueQuantity.Name)
            {
                int value = (int)View.m_gridViewEquipmentSummary.GetRowCellValue(e.RowHandle, e.Column);
                if (value > 0)
                {
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.BackColor = Color.Red;                                
                }
            }                        
        }

        #endregion        


        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();                        
            if (View.m_errorProvider.HasErrors)
                return;

            DateTime startDayDate = Model.Work.StartDate.Value.Date.AddHours(
                View.m_timeStartDay.Time.Hour).AddMinutes(View.m_timeStartDay.Time.Minute);

            if (Model.IsVisitsExistsBefore(startDayDate))
            {
                XtraMessageBox.Show("There are visits scheduled before " + startDayDate.ToShortTimeString()
                                    + ". Please rearrange visits or modify Start Day time",
                                    "Illegal visits arrangements", MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                View.m_timeStartDay.Focus();
                return;
            }
                       
            StartDayDonePackage package = new StartDayDonePackage();
            package.WorkTransaction = new WorkTransaction(0,
                Model.Work.ID,
                Configuration.CurrentDispatchId,
                null,
                (int)WorkTransactionTypeEnum.StartDayDone,
                DateTime.Now,
                decimal.Zero,
                false);

            package.LoadAndKeepEquipment = View.m_txtVanEquipment.Quantities;
            package.EmployeeExecutedId = Configuration.CurrentDispatchId;

            try
            {
                Database.Begin();
                StartDayDonePackage.SaveStartDayDone(package, startDayDate);
                Database.Commit();
                Host.TraceUserAction("Start Day " + Model.Technician.DisplayName);
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion                
    }
}
