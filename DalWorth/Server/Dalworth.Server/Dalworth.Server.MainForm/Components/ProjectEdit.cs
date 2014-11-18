using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Data;
using DevExpress.XtraEditors.Controls;

using Dalworth.Server.Domain;
using Dalworth.Server.Windows;


namespace Dalworth.Server.MainForm.Components
{
    public partial class ProjectEdit : BaseControl
    {
        private const int RefCleanerAdvertisingSourceId = 368;

        private List<QbCustomerType> m_customerTypes;
        private List<QbSalesRep> m_salesReps;
        private List<Employee> m_technicians;
        private BindingList<VisitWrapper> m_visitWrappers;
       
        #region Project

        private Project m_project;
        public Project Project
        {
            get
            {
                GetDataFromUI();
                return m_project;
            }
            set
            {
                m_project = value;
            }
        }

        #endregion        

        #region ProjectInsurance

        public ProjectInsurance ProjectInsurance { get; set; }
        
        #endregion

        #region IsInsuranceVisible

        private bool m_isInsuranceVisible = false;
        public bool IsInsuranceVisible
        {
            get { return m_isInsuranceVisible; }
            set { m_isInsuranceVisible = value; }
        }

        #endregion 

        #region IsEditable

        private bool m_isEditable;
        public bool IsEditable
        {
            get { return m_isEditable; }
            set
            {
                m_isEditable = value;
                EnableDisable();
            }
        }

        #endregion

        #region IsQbSalesRepVisible

        private bool m_isQbSalesRepVisible = true;
        public bool IsQbSalesRepVisible
        {
            get { return m_isQbSalesRepVisible; }
            set { m_isQbSalesRepVisible = value; }
        }

        #endregion

        #region IsQbSalesRepRequired

        private bool m_isQbSalesRepRequired = false;

        public bool IsQbSalesRepRequired
        {
            get { return m_isQbSalesRepRequired; }
            set { m_isQbSalesRepRequired = value; }
        }

        #endregion

        #region AreaId

        private int? m_currentAreaId;
        public int? AreaId
        {
            set
            {
                if (DesignMode)
                    return;

                m_currentAreaId = value;

                if (m_technicians == null)
                {
                    m_technicians = Employee.FindBy(EmployeeTypeEnum.Technician);

                    foreach (Employee technician in m_technicians)
                    {
                        m_cmbQbCustomerTypeLevel1.Properties.Items.Add(
                            new ImageComboBoxItem(technician.DisplayName, (object)technician.ID));
                    }
                }
            }
            get
            {
                return m_currentAreaId;
            }
        }

        #endregion

        #region Constructor

        public ProjectEdit()
        {
            InitializeComponent();

            m_cmbQbCustomerTypeLevel0.SelectedIndexChanged += OnQbCustomerTypeLevel0Changed;
            m_cmbQbCustomerTypeLevel1.SelectedIndexChanged += OnQbCustomerTypeLevel1Changed;
            m_cmbQbSalesRep.SelectedIndexChanged += OnQbSalesRepChanged;

            m_cmbQbCustomerTypeLevel0.Validating += OnQbCustomerTypeValidating;
            m_cmbQbCustomerTypeLevel1.Validating += OnQbCustomerTypeValidating;
            m_cmbQbSalesRep.Validating += OnQbCustomerTypeValidating;
        }

        #endregion

        #region LoadDataToUI

        private void LoadDataToUI()
        {
            m_ctlProjectInsurance.ProjectInsurance = ProjectInsurance;
        }

        #endregion

        #region GetDataFromUI

        private void GetDataFromUI()
        {
            if (m_project == null)
                return;

            ProjectInsurance = m_ctlProjectInsurance.ProjectInsurance;

            if (m_cmbQbCustomerTypeLevel1.Enabled && !string.IsNullOrEmpty((string)m_cmbQbCustomerTypeLevel1.EditValue))
                m_project.QbCustomerTypeListId = (string)m_cmbQbCustomerTypeLevel1.EditValue;
            else if (m_cmbQbCustomerTypeLevel0.Enabled && !string.IsNullOrEmpty((string)m_cmbQbCustomerTypeLevel0.EditValue))
                m_project.QbCustomerTypeListId = (string)m_cmbQbCustomerTypeLevel0.EditValue;

            m_project.QbSalesRepListId = (string)m_cmbQbSalesRep.EditValue;
            if (m_project.QbSalesRepListId == string.Empty)
                m_project.QbSalesRepListId = null;

            if (m_project.QbSalesRepListId != null)
            {
                QbSalesRep salesRep = m_salesReps.Find(delegate(QbSalesRep temp)
                                                           { return temp.ListId == m_project.QbSalesRepListId; });

                if (m_visitWrappers.Count > 0 && m_visitWrappers[0].Technician != null)
                {
                    QbSalesRep expectedSalesRep = m_salesReps.Find(delegate(QbSalesRep temp)
                                                                       {
                                                                           if (!temp.EmployeeId.HasValue)
                                                                               return false;
                                                                         
                                                                           return temp.EmployeeId ==
                                                                                  m_visitWrappers[0].Technician.ID;
                                                                       });
                    if (expectedSalesRep != null)
                        m_project.ExpectedQbSalesRepListId = expectedSalesRep.ListId;
                }

                if (salesRep.EmployeeId.HasValue)
                {
                    m_project.AdvertisingSourceId = salesRep.EmployeeId.Value;
                    m_project.AdvertisingSourceId = RefCleanerAdvertisingSourceId;
                }
             }
        }

        #endregion

        #region HasErrors

        public bool HasErrors
        {
            get
            {
                return m_errorProvider.HasErrors;
            }
        }

        #endregion

        #region ClearError

        public void ClearErrors()
        {
            m_errorProvider.ClearErrors();
        }

        #endregion

        #region OnQbCustomerTypeLevel0Changed

        private void OnQbCustomerTypeLevel0Changed (object sender, EventArgs e)
        {
            if (m_cmbQbCustomerTypeLevel0.EditValue == null)
            {
                m_cmbQbCustomerTypeLevel1.Enabled = false;
                m_cmbQbCustomerTypeLevel1.EditValue = null;
            }
            else
            {
                m_errorProvider.SetError(m_cmbQbCustomerTypeLevel0, string.Empty);
                m_cmbQbCustomerTypeLevel1.Properties.Items.Clear();
                m_cmbQbCustomerTypeLevel1.Enabled = false;
                m_cmbQbCustomerTypeLevel1.EditValue = string.Empty;

                string selectedLevel0ListId = (string) m_cmbQbCustomerTypeLevel0.EditValue;
                if (!string.IsNullOrEmpty(selectedLevel0ListId))
                {
                    List<QbCustomerType> leval1CustomerTypes = m_customerTypes.FindAll(
                        delegate(QbCustomerType customerType)
                            {
                                return customerType.ParentRefListId == selectedLevel0ListId;
                            }
                        );

                    if (leval1CustomerTypes.Count > 0)
                    {
                        m_cmbQbCustomerTypeLevel1.Enabled = true;
                        foreach (QbCustomerType qbCustomerType in leval1CustomerTypes)
                        {

                            m_cmbQbCustomerTypeLevel1.Properties.Items.Add(
                                new ImageComboBoxItem(qbCustomerType.Name, qbCustomerType.ListId));
                        }
                    }
                }
            }
            InitializeQbSalesReps();
            Validate();
        }

        #endregion

        #region OnQbCustomerTypeLevel1Changed

        private void OnQbCustomerTypeLevel1Changed(object sender, EventArgs e)
        {
            Validate();
        }

        #endregion

        #region OnQbSalesRepChanged

        private void OnQbSalesRepChanged(object sender, EventArgs e)
        {

            string salesRepListId = (string)m_cmbQbSalesRep.EditValue;

            if (salesRepListId != string.Empty)
            {
                QbSalesRep salesRep = m_salesReps.Find(delegate(QbSalesRep tempSalesRep)
                                        { return tempSalesRep.ListId == salesRepListId; });
                if (!string.IsNullOrEmpty(salesRep.QbCustomerTypeListId))
                {
                    m_cmbQbCustomerTypeLevel0.EditValue = salesRep.QbCustomerTypeListId;
                }
            }

            Validate();
        }

        #endregion 

        #region OnQbCustomerTypeLevelValidating

        private void OnQbCustomerTypeValidating(object sender, CancelEventArgs e)
        {
            m_errorProvider.ClearErrors();

            if (m_cmbQbCustomerTypeLevel0.Enabled && string.IsNullOrEmpty((string)m_cmbQbCustomerTypeLevel0.EditValue))
                m_errorProvider.SetError(m_cmbQbCustomerTypeLevel0, "Required");
            
            if (m_cmbQbCustomerTypeLevel1.Enabled && string.IsNullOrEmpty((string) m_cmbQbCustomerTypeLevel1.EditValue))
                m_errorProvider.SetError(m_cmbQbCustomerTypeLevel1, "Required");
            else
                m_errorProvider.SetError(m_cmbQbCustomerTypeLevel1, string.Empty);

            if (m_isQbSalesRepRequired && string.IsNullOrEmpty((string)
            m_cmbQbSalesRep.EditValue))
                m_errorProvider.SetError(m_cmbQbSalesRep, "Required");
        }

        #endregion

        #region Initialize

        public void Initialize()
        {
            ProjectInsurance = null;
            if (!IsQbSalesRepVisible)
            {
                m_cmbQbSalesRep.Visible = false;
                m_lblSalesRep.Visible = false;
            }

            if (
                (m_project.ProjectType == ProjectTypeEnum.Deflood
                 || m_project.ProjectType == ProjectTypeEnum.Construction
                 || m_project.ProjectType == ProjectTypeEnum.Content))
            {
                IsInsuranceVisible = true;
            }

            InitializeVisitHistory();
            InitializeQbCustomerTypes();
            SetDefaultSalesRep();
            InitializeQbSalesReps();

            if (m_project.ID > 0)
            {
                try
                {
                    ProjectInsurance = ProjectInsurance.FindByPrimaryKey(m_project.ID, null);
                }
                catch (DataNotFoundException)
                {
                }
            }
            LoadDataToUI();
        }
    
        #endregion

        #region SetDefaultSalesRep

        private void SetDefaultSalesRep ()
        {
            if (string.IsNullOrEmpty(m_project.QbSalesRepListId) && m_visitWrappers != null && m_visitWrappers.Count > 0 && m_visitWrappers[0].Technician != null)
            {
                QbSalesRep expectedSalesRep = m_salesReps.Find(delegate(QbSalesRep temp)
                {
                    if (!temp.EmployeeId.HasValue)
                        return false;

                    return temp.EmployeeId ==
                           m_visitWrappers[0].Technician.ID;
                });
                if (expectedSalesRep != null)
                {
                    m_project.ExpectedQbSalesRepListId = expectedSalesRep.ListId;

                    if (!m_project.AdvertisingTechnicianId.HasValue)
                        m_project.QbSalesRepListId = expectedSalesRep.ListId;
                }

            }
        }

        #endregion

        #region InitializeQbCustomerTypes

        private void InitializeQbCustomerTypes()
        {
            if (m_customerTypes == null)
                m_customerTypes = QbCustomerType.FindActive(null);

            if (m_salesReps == null)
                m_salesReps = QbSalesRep.FindActive(null);

            m_cmbQbCustomerTypeLevel0.SelectedIndexChanged -= OnQbCustomerTypeLevel0Changed;
            m_cmbQbCustomerTypeLevel1.SelectedIndexChanged -= OnQbCustomerTypeLevel1Changed;

            m_cmbQbCustomerTypeLevel0.Properties.Items.Clear();
            m_cmbQbCustomerTypeLevel0.Enabled = false;

            m_cmbQbCustomerTypeLevel1.Properties.Items.Clear();
            m_cmbQbCustomerTypeLevel1.Enabled = false;

            QbCustomerType projectQbCustomerType = null;
            if (!string.IsNullOrEmpty(m_project.QbCustomerTypeListId))
            {
                projectQbCustomerType = m_customerTypes.Find(delegate(QbCustomerType qbCustomerType)
                { return qbCustomerType.ListId == m_project.QbCustomerTypeListId; });
            }

            if (projectQbCustomerType == null)
            {
                List<QbCustomerType> customerTypesLevel0 = m_customerTypes.FindAll(delegate(QbCustomerType customerType)
                                                            { return customerType.SubLevel == 0; });

                if (customerTypesLevel0.Count > 0)
                {
                    m_cmbQbCustomerTypeLevel0.Properties.Items.Add(new ImageComboBoxItem(string.Empty, string.Empty));
                    foreach (QbCustomerType qbCustomerType in customerTypesLevel0)
                    {

                        m_cmbQbCustomerTypeLevel0.Properties.Items.Add(
                            new ImageComboBoxItem(qbCustomerType.Name, qbCustomerType.ListId));
                    }

                    m_cmbQbCustomerTypeLevel0.EditValue = string.Empty;
                    m_cmbQbCustomerTypeLevel0.Enabled = true;
                    OnQbCustomerTypeLevel0Changed(null, null);
                }
            }
            else
            {
                if (projectQbCustomerType.SubLevel == 0)
                {
                    List<QbCustomerType> customerTypesLevel0 = m_customerTypes.FindAll(delegate(QbCustomerType customerType)
                                                                    { return customerType.SubLevel == 0; });

                    foreach (QbCustomerType qbCustomerType in customerTypesLevel0)
                    {

                        m_cmbQbCustomerTypeLevel0.Properties.Items.Add(
                            new ImageComboBoxItem(qbCustomerType.Name, qbCustomerType.ListId));
                    }

                    m_cmbQbCustomerTypeLevel0.EditValue = projectQbCustomerType.ListId;
                    m_cmbQbCustomerTypeLevel0.Enabled = true;
                    OnQbCustomerTypeLevel0Changed(null, null);
                }
                else if (projectQbCustomerType.SubLevel == 1)
                {
                    List<QbCustomerType> customerTypesLevel1 = m_customerTypes.FindAll(delegate(QbCustomerType customerType)
                                                        {
                                                            return customerType.ParentRefListId == projectQbCustomerType.ParentRefListId
                                                                   || customerType.ListId == projectQbCustomerType.ListId;
                                                        });

                    List<QbCustomerType> customerTypesLevel0 = m_customerTypes.FindAll(delegate(QbCustomerType customerType)
                                                        { return customerType.SubLevel == 0; });

                    QbCustomerType defaultLevel0CustomerType = m_customerTypes.Find(delegate(QbCustomerType customerType)
                                                                { return customerType.ListId == projectQbCustomerType.ParentRefListId; });

                    foreach (QbCustomerType qbCustomerType in customerTypesLevel0)
                    {

                        m_cmbQbCustomerTypeLevel0.Properties.Items.Add(
                            new ImageComboBoxItem(qbCustomerType.Name, qbCustomerType.ListId));
                    }
                    m_cmbQbCustomerTypeLevel0.EditValue = defaultLevel0CustomerType.ListId;

                    foreach (QbCustomerType qbCustomerType in customerTypesLevel1)
                    {

                        m_cmbQbCustomerTypeLevel1.Properties.Items.Add(
                            new ImageComboBoxItem(qbCustomerType.Name, qbCustomerType.ListId));
                    }
                    m_cmbQbCustomerTypeLevel1.EditValue = projectQbCustomerType.ListId;

                    m_cmbQbCustomerTypeLevel0.Enabled = true;
                    m_cmbQbCustomerTypeLevel1.Enabled = true;
                    OnQbCustomerTypeLevel1Changed(null, null);
                }
            }

            m_cmbQbCustomerTypeLevel0.SelectedIndexChanged += OnQbCustomerTypeLevel0Changed;
            m_cmbQbCustomerTypeLevel1.SelectedIndexChanged += OnQbCustomerTypeLevel1Changed;
        }

        #endregion

        #region InitializeQbSalesReps

        private void InitializeQbSalesReps()
        {
            m_cmbQbSalesRep.Properties.Items.Clear();

            var customerTypeLevel0ListId = (string)m_cmbQbCustomerTypeLevel0.EditValue;

            if (string.IsNullOrEmpty(customerTypeLevel0ListId))
                return;

            List<QbSalesRep> salesRepsToAssign = null;

            if (!string.IsNullOrEmpty(customerTypeLevel0ListId))
            {
                salesRepsToAssign = m_salesReps.FindAll(delegate(QbSalesRep salesRep)
                                    { return salesRep.QbCustomerTypeListId == customerTypeLevel0ListId; });

            }

            if (salesRepsToAssign == null || salesRepsToAssign.Count ==0)
                salesRepsToAssign = m_salesReps;

            QbSalesRep projectSalesRep = null;
            if (!string.IsNullOrEmpty(m_project.QbSalesRepListId))
            {
                projectSalesRep = m_salesReps.Find(delegate(QbSalesRep qbSalesRep)
                            { return qbSalesRep.ListId == m_project.QbSalesRepListId;});
                if (projectSalesRep != null)
                {
                    if (!salesRepsToAssign.Contains(projectSalesRep))
                            salesRepsToAssign.Add(projectSalesRep);
                       
                }
            }

            m_cmbQbSalesRep.Properties.Items.Add(new ImageComboBoxItem(string.Empty, string.Empty));
            foreach (QbSalesRep salesRep in salesRepsToAssign)
            {
                m_cmbQbSalesRep.Properties.Items.Add(
                    new ImageComboBoxItem(salesRep.DisplayName, salesRep.ListId));
            }

            if (projectSalesRep != null)
                m_cmbQbSalesRep.EditValue = projectSalesRep.ListId;
            
            m_cmbQbSalesRep.Enabled = true;
        }

        #endregion

        #region InitializeVisitHistory

        private void InitializeVisitHistory ()
        {
            m_visitWrappers = Visit.FindVisitWrappersByProjectId(m_project.ID);

            if (m_visitWrappers.Count == 0)
                m_tabVisitHistory.TabPages.Remove(m_tabPageViewHistory);
            else
                m_gridVisits.DataSource = m_visitWrappers;
           
            if (!IsInsuranceVisible)
            {
                m_tabVisitHistory.TabPages.Remove(m_tabPageInsurance);
            }

            if (m_tabVisitHistory.TabPages.Count == 0)
                m_tabVisitHistory.Visible = false;
        }

        #endregion 

        #region EnableDisable

        private void EnableDisable()
        {
            m_cmbQbCustomerTypeLevel0.Properties.ReadOnly = !m_isEditable;
            m_cmbQbCustomerTypeLevel1.Properties.ReadOnly = !m_isEditable;
            m_cmbQbSalesRep.Properties.ReadOnly = !m_isEditable;
        }

        #endregion
    }
}
