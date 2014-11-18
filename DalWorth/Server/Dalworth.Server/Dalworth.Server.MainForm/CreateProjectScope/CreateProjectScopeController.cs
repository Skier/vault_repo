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

namespace Dalworth.Server.MainForm.CreateProjectScope
{
    public class CreateProjectScopeController : Controller<CreateProjectScopeModel, CreateProjectScopeView>
    {

        public delegate void AddScopeRequestHandler(ProjectConstructionScope scope);
        public event AddScopeRequestHandler AddScopeRequest;

        public delegate void VoidScopeRequestHandler(ProjectConstructionScope scope);
        public event VoidScopeRequestHandler VoidScopeRequest;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region CreatedProjectBillPay

        public ProjectConstructionScope CreatedProjectScope
        {
            get { return Model.ProjectScope; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length >= 1 && data[0] != null)
                Model.ProjectScope = (ProjectConstructionScope) data[0];

            if (data != null && data.Length >= 2 && data[1] != null)
                Model.BasedOn = (ProjectConstructionScope)data[1];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnSave.Click += OnSaveClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnSaveAndAdd.Click += OnSaveAndAddClick;

            View.m_txtAmount.TextChanged += OnCollectedAmountChanged;
        
            View.m_txtJobType.Validated += OnJobTypeValidate;
            View.m_dtpScopeDate.Validated += OnScopeDateValidate;
            View.m_cmbScopeType.Validated += OnScopeTypeValidate;
            View.m_txtAmount.Validated += OnAmountValidate;
            View.m_txtNotes.Validated += OnNotesValidate;

            View.m_txtJobType.Focus();
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtJobType.EditValue = Model.ProjectScope.JobType;
            View.m_dtpScopeDate.Properties.MaxValue = DateTime.Now.Date;
            View.m_txtAmount.EditValue = Model.ProjectScope.Amount;
            View.m_txtNotes.EditValue = Model.ProjectScope.Notes;
            if ((int)Model.ProjectScope.ScopeType > 0)
                View.m_cmbScopeType.EditValue = (int)Model.ProjectScope.ScopeType;

            foreach (ProjectConstructionScopeType type in Model.ScopeTypes)
            {
                View.m_cmbScopeType.Properties.Items.Add(
                    new ImageComboBoxItem(ProjectConstructionScopeType.GetText((ProjectConstructionScopeTypeEnum)type.ID), 
                    (object)type.ID));
            }

            if (!Model.IsNewProjectBillPay)
            {
                View.Text = "Dalworth - Edit Scope";
                View.m_dtpScopeDate.DateTime = Model.ProjectScope.ScopeDate;

                if (Model.ProjectScope.ScopeType != ProjectConstructionScopeTypeEnum.ScopeEstimate)
                {
                    View.m_txtJobType.Enabled = false;
                    View.m_dtpScopeDate.Enabled = false;
                    View.m_txtAmount.Enabled = false;
                    View.m_cmbScopeType.Enabled = false;
                }

                View.m_btnSaveAndAdd.Visible = false;
            } else
            {
                View.Text = "Dalworth - Create Scope";
            }
        }

        #endregion

        #region OnCollectedAmountChanged

        private void OnCollectedAmountChanged(object sender, EventArgs e)
        {
            if (View.m_dtpScopeDate.EditValue == null)
                View.m_dtpScopeDate.DateTime = DateTime.Now.Date;
        }

        #endregion

        #region Validate

        private void OnJobTypeValidate(object sender, EventArgs e)
        {
            int maxLenght = 50;

            if (View.m_txtJobType.EditValue == null || View.m_txtJobType.EditValue.ToString().Length == 0)
            {
                View.m_errorProvider.SetError(View.m_txtJobType, "Job Type can't be empty.");
                return;
            }
            else if (View.m_txtJobType.EditValue.ToString().Length > maxLenght)
            {
                View.m_errorProvider.SetError(View.m_txtJobType, "Job Type can't be longer than " + maxLenght);
                return;
            }

            View.m_errorProvider.SetError(View.m_txtJobType, string.Empty);
        }

        private void OnScopeDateValidate(object sender, EventArgs e)
        {
            if (View.m_dtpScopeDate.EditValue == null)
            {
                View.m_errorProvider.SetError(View.m_dtpScopeDate, "Scope Date can't be empty");
                return;
            }

            View.m_errorProvider.SetError(View.m_dtpScopeDate, string.Empty);
        }

        private void OnScopeTypeValidate(object sender, EventArgs e)
        {
            if (View.m_cmbScopeType.SelectedItem == null)
            {
                View.m_errorProvider.SetError(View.m_cmbScopeType, "Scope Type can't be empty");
                return;
            }

            View.m_errorProvider.SetError(View.m_cmbScopeType, string.Empty);
        }

        private void OnAmountValidate(object sender, EventArgs e)
        {
            if ((decimal)View.m_txtAmount.EditValue == decimal.Zero)
            {
                View.m_errorProvider.SetError(View.m_txtAmount, "Amount can't be equal to zero");
                return;
            }

            View.m_errorProvider.SetError(View.m_txtAmount, string.Empty);
        }

        private void OnNotesValidate(object sender, EventArgs e)
        {
            int maxLenght = 200;

            if (View.m_txtNotes.EditValue != null 
                && View.m_txtNotes.EditValue.ToString().Length > maxLenght)
            {
                View.m_errorProvider.SetError(View.m_txtNotes, "Notes max lenght is " + maxLenght);
                return;
            }

            View.m_errorProvider.SetError(View.m_txtNotes, string.Empty);
        }

        #endregion

        #region CommitScope

        private void CommitScope()
        {
            ProjectConstructionScope scope = Model.ProjectScope;

            if (View.m_txtJobType.EditValue != null)
                scope.JobType = (string)View.m_txtJobType.EditValue;
            else
                scope.JobType = string.Empty;

            if (View.m_dtpScopeDate.EditValue != null)
                scope.ScopeDate = View.m_dtpScopeDate.DateTime;
            else
                scope.ScopeDate = DateTime.Now;

            if (View.m_cmbScopeType.EditValue != null && (int)View.m_cmbScopeType.EditValue != 0)
                scope.ProjectConstructionScopeTypeId = (int)View.m_cmbScopeType.EditValue;

            if (View.m_txtAmount.EditValue != null)
                scope.Amount = (decimal)View.m_txtAmount.EditValue;
            else
                scope.Amount = Decimal.Zero;

            if (View.m_txtNotes.EditValue != null)
                scope.Notes = (string)View.m_txtNotes.EditValue;
            else
                scope.Notes = string.Empty;
        }

        #endregion

        #region CreateNewScope

        private void CreateNewScope()
        {
            Model.ProjectScope = new ProjectConstructionScope();

            View.Text = "Dalworth - Create Scope";
            View.m_txtJobType.EditValue = Model.ProjectScope.JobType;
            View.m_dtpScopeDate.Properties.MaxValue = DateTime.Now.Date;
            View.m_txtAmount.EditValue = Model.ProjectScope.Amount;
            View.m_txtNotes.EditValue = Model.ProjectScope.Notes;
            View.m_cmbScopeType.SelectedIndex = -1;

            View.m_txtJobType.Focus();
        }

        #endregion

        #region OnSaveAndAddClick

        private void OnSaveAndAddClick(object sender, EventArgs e)
        {
            if (Save(CreatedProjectScope))
                CreateNewScope();
        }

        #endregion

        #region Save

        private bool Save(ProjectConstructionScope scope)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return false;

            CommitScope();
            if (AddScopeRequest != null)
                AddScopeRequest.Invoke(scope);

            if (VoidScopeRequest != null && Model.BasedOn != null)
                VoidScopeRequest.Invoke(Model.BasedOn);

            return true;
        }

        #endregion

        #region OnSaveClick

        private void OnSaveClick(object sender, EventArgs e)
        {
            if (Save(CreatedProjectScope))
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
