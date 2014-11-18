using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.VisitMerge
{
    public class VisitMergeController : Controller<VisitMergeModel, VisitMergeView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region SelectedVisits

        private List<Visit> m_selectedVisits;
        public List<Visit> SelectedVisits
        {
            get
            {
                return m_selectedVisits;
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.MainVisitInput = (Visit) data[0];
            Model.MergedVisitsInput = (List <Visit>) data[1];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;

            View.m_gridMergedVisitsView.CellValueChanging += OnGridMergedVisitsCellValueChanging;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {                        
            if (Model.IsDashboardVisitsMerge)
            {
                View.m_lblExplanation.Text = string.Format(
                    "There are visits for {0} already placed on dashboard. If you want to merge current visit\r\nwith existing one please select it and press OK, otherwise press Cancel.", 
                    Model.Customer.DisplayName);

                View.m_colServiceDate.Visible = false;
                View.m_colPrefferedTimeFrame.Visible = false;

                View.m_chkVisitSelector.CheckStyle = CheckStyles.Radio;
                View.Text = "Assigned Visits Merge";
            } else
            {
                View.m_lblExplanation.Text = string.Format(View.m_lblExplanation.Text,
                    Model.Customer.DisplayName);
                View.m_colTechnician.Visible = false;
                View.Text = "Pending Visits Merge";
            }

            View.m_gridMergedVisits.DataSource = Model.MergedVisits;
        }

        #endregion

        #region OnGridMergedVisitsCellValueChanging

        private void OnGridMergedVisitsCellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (!Model.IsDashboardVisitsMerge || (bool)e.Value == false)
                return;                

            VisitWrapper currentVisit = (VisitWrapper)View.m_gridMergedVisitsView.GetRow(e.RowHandle);

            foreach (VisitWrapper visit in Model.MergedVisits)
            {
                if (visit != currentVisit && visit.IsSelected)
                {
                    visit.IsSelected = false;
                    Model.MergedVisits.ResetItem(Model.MergedVisits.IndexOf(visit));
                    break;
                }
            }
        }

        #endregion


        #region IsFormValid

        private bool IsFormValid()
        {
            if (!Model.IsAnyVisitSelected())
            {
                if (Model.IsDashboardVisitsMerge)
                {
                    XtraMessageBox.Show("Please select visit to merge", "No visit selected",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                } else
                {
                    XtraMessageBox.Show("Please select visit(s) to merge", "No visit(s) selected",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);                                        
                }

                return false;
            }

            return true;
        }

        #endregion
      
               
        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {   
            if (!IsFormValid())
                return;

            m_selectedVisits = Model.GetSelectedVisits();

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
