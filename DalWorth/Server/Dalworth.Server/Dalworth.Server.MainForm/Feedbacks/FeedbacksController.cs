using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

using Dalworth.Server.MainForm.FeedbackEdit;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;


namespace Dalworth.Server.MainForm.Feedbacks
{
    public class FeedbacksController : NestedController<FeedbacksModel, FeedbacksView>
    {
        private ProjectFeedbackWrapper FocusedProjectFeedbackWrapper
        {
            get
            {
                if (View.m_gridViewFeedbacks.FocusedRowHandle < 0)
                    return null;

                return (ProjectFeedbackWrapper)View.m_gridViewFeedbacks.GetRow(
                    View.m_gridViewFeedbacks.FocusedRowHandle);
            }
        }

        #region EventHandlers

        protected override void OnInitialize()
        {
            base.OnInitialize();

            View.m_gridFeedbacks.DoubleClick += OnFeedbacksDoubleClick;
            View.m_gridFeedbacks.KeyDown += OnGridFeedbacksKeyDown;
            View.m_gridFeedbacks.DataSource = Model.ProjectFeedbackWrappers;
            
            View.m_txtCustomer.KeyDown += OnFieldsKeyDown;

            View.m_cmbStatus.SelectedIndexChanged += OnFiltersChanged;
            View.m_dateRange.DateRangeValueChanged += OnFiltersChanged;
            View.m_dateCallbackRange.DateRangeValueChanged += OnFiltersChanged;

            View.m_btnClear.Click += OnClearClick;
        }

        private void OnFieldsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            OnFiltersChanged(sender, e);
        }

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            ClearFilters();
            RefreshData();
        }

        protected void OpenProjectFeedback(ProjectFeedbackWrapper projectFeedbackWrapper)
        {
            using (FeedbackEditController controller = Prepare<FeedbackEditController>(projectFeedbackWrapper))
            {
                controller.Execute(false);
            }
        }

        private void OnFeedbacksDoubleClick(object sender, EventArgs e)
        {
            ProjectFeedbackWrapper feedbackWrapper = FocusedProjectFeedbackWrapper;
            if (feedbackWrapper != null)
                OpenProjectFeedback(feedbackWrapper);
        }

        private void OnGridFeedbacksKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridFeedbacks.Focused)
            {
                ProjectFeedbackWrapper feedbackWrapper = FocusedProjectFeedbackWrapper;
                if (feedbackWrapper != null)
                    OpenProjectFeedback(feedbackWrapper);
            }
        }

        #endregion

        public void RefreshData()
        {
            string firstName = Utils.ParseFirstName(View.m_txtCustomer.Text);
            string lastName = Utils.ParseLastName(View.m_txtCustomer.Text);

            int statusValue = (int)View.m_cmbStatus.EditValue;

            Model.UpdateProjectFeedbackWrappers(firstName, lastName, statusValue, View.m_dateRange.EditValue, View.m_dateCallbackRange.EditValue);

            View.m_gridFeedbacks.DataSource = Model.ProjectFeedbackWrappers;
        }

        private void ClearFilters()
        {
            View.m_txtCustomer.Text = string.Empty;
            View.m_cmbStatus.SelectedIndex = 0;
            View.m_dateRange.Clear();
            View.m_dateRange.Clear();
            View.m_dateCallbackRange.Clear();
        }
    }
}
