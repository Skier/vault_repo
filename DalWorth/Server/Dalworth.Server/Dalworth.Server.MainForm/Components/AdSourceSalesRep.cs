using System;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;

using Dalworth.Server.Windows;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Components
{
    public partial class AdSourceSalesRep : BaseControl
    {
        private List<QbCustomerType> m_customerTypes;
        private List<QbSalesRep> m_salesReps;

        #region QbCustomerTypeListId

        private string m_qbCustomerTypeListId;
        public string QbCustomerTypeListId
        {
            get
            {
                string m_qbCustomerTypeListId = (string)m_cmbQbCustomerTypeLevel1.EditValue;

                if (string.IsNullOrEmpty(m_qbCustomerTypeListId))
                    m_qbCustomerTypeListId = string.IsNullOrEmpty((string)m_cmbQbCustomerTypeLevel0.EditValue) ? null : (string)m_cmbQbCustomerTypeLevel0.EditValue;

                return m_qbCustomerTypeListId;
            }
        }

        #endregion 

        #region QbSalesRepListId

        private string m_qbQbSalesRepListId;
        public string QbSalesRepListId
        {
            get
            {
                m_qbQbSalesRepListId = (string)m_cmbQbSalesRep.EditValue;
                return m_qbQbSalesRepListId;
            }
        }

        #endregion 

        #region contructor

        public AdSourceSalesRep()
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

        #region IsQbSalesRepRequired

        private bool m_isQbSalesRepRequired = false;

        public bool IsQbSalesRepRequired
        {
            get { return m_isQbSalesRepRequired; }
            set { m_isQbSalesRepRequired = value; }
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

        public void ClearError()
        {
            m_errorProvider.ClearErrors();
        }

        #endregion

        #region Initialize

        public void Initialize(string qbCustomerTypeListId, string qbSalesRepListId)
        {
            m_qbCustomerTypeListId = qbCustomerTypeListId;
            m_qbQbSalesRepListId = qbSalesRepListId;

            if (!IsQbSalesRepVisible)
                m_cmbQbSalesRep.Visible = false;

            InitializeCustomerTypes();
            InitializeQbSalesReps();
        }

        #region InitializeCustomerTypes

        private void InitializeCustomerTypes()
        {
            m_customerTypes = QbCustomerType.FindActive(null);
            m_salesReps = QbSalesRep.FindActive(null);


            m_cmbQbCustomerTypeLevel0.Properties.Items.Clear();
            m_cmbQbCustomerTypeLevel0.Enabled = false;

            m_cmbQbCustomerTypeLevel1.Properties.Items.Clear();
            m_cmbQbCustomerTypeLevel1.Enabled = false;

            QbCustomerType projectQbCustomerType = null;
            if (!string.IsNullOrEmpty(m_qbCustomerTypeListId))
            {
                projectQbCustomerType = m_customerTypes.Find(delegate(QbCustomerType qbCustomerType)
                { return qbCustomerType.ListId == m_qbCustomerTypeListId; });
            }

            if (projectQbCustomerType == null)
            {
                List<QbCustomerType> customerTypesLevel0 = m_customerTypes.FindAll(delegate(QbCustomerType customerType)
                                                            { return customerType.SubLevel == 0; });

                m_cmbQbCustomerTypeLevel0.Properties.Items.Add(
                        new ImageComboBoxItem("", ""));
                foreach (QbCustomerType qbCustomerType in customerTypesLevel0)
                {
                    m_cmbQbCustomerTypeLevel0.Properties.Items.Add(
                        new ImageComboBoxItem(qbCustomerType.Name, qbCustomerType.ListId));
                }

                m_cmbQbCustomerTypeLevel0.Enabled = true;
                OnQbCustomerTypeLevel0Changed(null, null);
            }
            else
            {
                if (projectQbCustomerType.SubLevel == 0)
                {
                    List<QbCustomerType> customerTypesLevel0 = m_customerTypes.FindAll(delegate(QbCustomerType customerType)
                                                                    { return customerType.SubLevel == 0; });

                    m_cmbQbCustomerTypeLevel0.Properties.Items.Add(
                            new ImageComboBoxItem("", ""));
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
        }
        #endregion

        #region InitializeQbSalesReps

        private void InitializeQbSalesReps()
        {
            m_cmbQbSalesRep.Properties.Items.Clear();

            string customerTypeLevel0ListId = (string)m_cmbQbCustomerTypeLevel0.EditValue;

            List<QbSalesRep> salesRepsToAssign = null;

            if (!string.IsNullOrEmpty(customerTypeLevel0ListId))
            {
                salesRepsToAssign = m_salesReps.FindAll(delegate(QbSalesRep salesRep)
                                    { return salesRep.QbCustomerTypeListId == customerTypeLevel0ListId; });

            }

            if (salesRepsToAssign == null || salesRepsToAssign.Count == 0)
                salesRepsToAssign = m_salesReps;

            QbSalesRep projectSalesRep = null;
            if (!string.IsNullOrEmpty(m_qbCustomerTypeListId))
            {
                projectSalesRep = m_salesReps.Find(delegate(QbSalesRep qbSalesRep)
                            { return qbSalesRep.ListId == m_qbQbSalesRepListId; });
                if (projectSalesRep != null)
                {
                    if (!salesRepsToAssign.Contains(projectSalesRep))
                        salesRepsToAssign.Add(projectSalesRep);

                }
            }

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

        #endregion

        #region OnQbCustomerTypeLevel0Changed

        private void OnQbCustomerTypeLevel0Changed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)m_cmbQbCustomerTypeLevel0.EditValue))
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

                string selectedLevel0ListId = (string)m_cmbQbCustomerTypeLevel0.EditValue;
                List<QbCustomerType> level1CustomerTypes = m_customerTypes.FindAll(
                        delegate(QbCustomerType customerType)
                        {
                            return customerType.ParentRefListId == selectedLevel0ListId;
                        }
                    );

                if (level1CustomerTypes.Count > 0)
                {
                    m_cmbQbCustomerTypeLevel1.Enabled = true;
                    foreach (QbCustomerType qbCustomerType in level1CustomerTypes)
                    {
                        m_cmbQbCustomerTypeLevel1.Properties.Items.Add(
                            new ImageComboBoxItem(qbCustomerType.Name, qbCustomerType.ListId));
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

        #region OnQbCustomerTypeLevel1Validating

        private void OnQbCustomerTypeValidating(object sender, CancelEventArgs e)
        {
            m_errorProvider.ClearErrors();

            if (m_cmbQbCustomerTypeLevel1.Enabled && string.IsNullOrEmpty((string)m_cmbQbCustomerTypeLevel1.EditValue))
                m_errorProvider.SetError(m_cmbQbCustomerTypeLevel1, "Required");
            else
                m_errorProvider.SetError(m_cmbQbCustomerTypeLevel1, string.Empty);

            if (m_isQbSalesRepRequired && string.IsNullOrEmpty((string)
            m_cmbQbSalesRep.EditValue))
                m_errorProvider.SetError(m_cmbQbSalesRep, "Required");

        }

        #endregion

    }
}
