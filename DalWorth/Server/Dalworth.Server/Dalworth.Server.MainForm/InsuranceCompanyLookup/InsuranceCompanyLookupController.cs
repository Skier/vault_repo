using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace Dalworth.Server.MainForm.CustomerLookup
{
    public class InsuranceCompanyLookupController : Controller<InsuranceCompanyLookupModel, InsuranceCompanyLookupView>
    {
        private bool m_isAutoRefreshEnabled = true;

        #region IsCompanySelected

        private bool m_isCompanySelected;
        public bool IsCompanySelected
        {
            get { return m_isCompanySelected; }
        }

        #endregion

        #region Company

        private InsuranceCompanyAndAddress m_company;
        public InsuranceCompanyAndAddress Company
        {
            get { return m_company; }
        }

        #endregion

        #region SelectedCompany

        private InsuranceCompanyAndAddress SelectedCompany
        {
            get
            {
                if (View.m_gridViewCompanies.FocusedRowHandle >= 0)
                    return (InsuranceCompanyAndAddress)View.m_gridViewCompanies.GetRow(
                        View.m_gridViewCompanies.FocusedRowHandle);
                return null;
            }
        }

        #endregion        

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data[0] != null)
                m_company = (InsuranceCompanyAndAddress)data[0];
                
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_lnkCompany.Click += OnCompanyClick;

            View.m_btnAdd.Click += OnAddClick;
            View.m_btnDelete.Click += OnDeleteClick;
            View.m_btnCancel.Click += OnCancelClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridCompanies.DataSource = Model.InsuranceCompanies;
        }

        #endregion

        #region UpdateUI

        private void UpdateUI()
        {
            if (!m_isAutoRefreshEnabled)
                return;

            View.m_gridCompanies.BeginUpdate();
            Model.Refresh(string.Empty, string.Empty, string.Empty);
            View.m_gridCompanies.DataSource = Model.InsuranceCompanies;
            View.m_gridCompanies.EndUpdate();

            View.m_btnDelete.Enabled = Model.InsuranceCompanies.Count > 0;
        }

        #endregion

        #region SelectCompany

        private void SelectCompany(InsuranceCompanyAndAddress company)
        {
            UpdateUI();
            
            int dataSourceIndex = Model.InsuranceCompanies.IndexOf(company);

            int rowHandle = View.m_gridViewCompanies.GetRowHandle(dataSourceIndex);
            if (rowHandle < 0)
            {
                m_isAutoRefreshEnabled = false;
                m_isAutoRefreshEnabled = true;
                UpdateUI();
                rowHandle = View.m_gridViewCompanies.GetRowHandle(dataSourceIndex);
            }

            View.m_gridViewCompanies.FocusedRowHandle = rowHandle;
        }

        #endregion

        #region OnCompanyClick

        private void OnCompanyClick(object sender, EventArgs e)
        {
            if (SelectedCompany == null)
            {
                XtraMessageBox.Show("Please select Company", "No Company Selected", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return;
            }

            m_company = SelectedCompany;
            m_isCompanySelected = true;
            View.Destroy();
        }

        #endregion

        #region OnAddClick

        private void OnAddClick(object sender, EventArgs e)
        {
            using (InsuranceCompanyEditController controller = Prepare<InsuranceCompanyEditController>())
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                {
                    try
                    {
                        Database.Begin();
                        Model.CreateNewCompany(controller.Company);
                        Database.Commit();
                        SelectCompany(controller.Company);
                    }
                    catch (Exception)
                    {
                        Database.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion

        #region OnDeleteClick

        private void OnDeleteClick(object sender, EventArgs e)
        {
            InsuranceCompanyAndAddress selectedCompany = SelectedCompany;
            if (selectedCompany == null)
            {
                XtraMessageBox.Show("Please select Company to delete", "Company Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }


            if (m_company != null && selectedCompany.InsuranceCompany.ID == m_company.InsuranceCompany.ID)
            {
                XtraMessageBox.Show("This Company cannot be deleted", "Company Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                Database.Begin();
                InsuranceCompany.Delete(selectedCompany.InsuranceCompany);
                Address.Delete(selectedCompany.Address);
                Database.Commit();
                UpdateUI();
            }
            catch (Exception)
            {
                Database.Rollback();
                XtraMessageBox.Show("This Company cannot be deleted", "Company Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion
    }
}
