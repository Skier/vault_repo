using System;
using System.Windows.Forms;
using System.ComponentModel;
using Dalworth.Server.Data;
using DevExpress.XtraEditors;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Accounting
{
    public delegate void VisitSelectedHandler(Visit visit);

    public partial class ProjectInfo : UserControl
    {
        

        private CustomerProjectWrapper m_customerProjectWrapper;
        public event CustomerProjectWrapperHandler EditProject;
        internal event VisitSelectedHandler VisitSelected;

        #region Constructor

        public ProjectInfo()
        {
            InitializeComponent();

            m_btnEditProject.Click += OnEidtProjectClick;
            m_gridVisitsDashboardLink.Click += OnDashboardLinkClick;
        }

        #endregion

        #region Clear

        public void Clear()
        {
            m_gridVisits.DataSource = null;

            foreach (Control control in m_grpPRoject.Controls)
            {
                if (control.Name.StartsWith("m_"))
                {
                    if (control is LabelControl)
                        ((LabelControl)control).Text = string.Empty;

                    if (control is MemoEdit)
                        ((MemoEdit)control).Text = string.Empty;
                }
            }
        }

        #endregion

        #region Initialize

        public void Initialize(CustomerProjectWrapper wrapper, ProjectWrapper projectWrapper)
        {

            

            m_customerProjectWrapper = wrapper;

            m_lnkEmail.Text = wrapper.Customer.Email;
            m_lblPhoneHome.Text = Utils.FormatPhone(wrapper.Customer.Phone1);
            m_lblPhoneWork.Text = Utils.FormatPhone(wrapper.Customer.Phone2);

            m_lblDateCreated.Text = wrapper.Project.CreateDate.ToString("MM/dd/yyyy");
            m_lblCustomerName.Text = wrapper.CustomerName;
            m_lblAddress.Text = string.Format("{0}\n{1}", wrapper.CustomerAddress.AddressFirstLine,
                wrapper.CustomerAddress.AddressSecondLine);

            BindingList<VisitWrapper> visitWrappers = Visit.FindVisitWrappersByProjectId(wrapper.ProjectId);
            ProjectType projectType = ProjectType.FindByPrimaryKey(wrapper.Project.ProjectTypeId, null);

            m_lblSalesRep.Text = string.Empty;
            m_lblAdvertisingSource.Text = string.Empty;

            if (!string.IsNullOrEmpty(projectWrapper.Project.QbCustomerTypeListId))
            {
                QbCustomerType qbCustomerType = QbCustomerType.FindByPrimaryKey(projectWrapper.Project.QbCustomerTypeListId);
                m_lblAdvertisingSource.Text = qbCustomerType.Name;
            }

            if (!string.IsNullOrEmpty(projectWrapper.Project.QbSalesRepListId))
            {
                QbSalesRep salesRep = QbSalesRep.FindByPrimaryKey(projectWrapper.Project.QbSalesRepListId);
                m_lblSalesRep.Text = salesRep.DisplayName;
            }

            m_gridVisits.DataSource = visitWrappers;
            m_lblProjectId.Text = wrapper.Caption.Trim();

            m_lblProjectStatus.Text = projectWrapper.ProjectStatusText;
            m_lblProjectProgress.Text = projectWrapper.Progress;
            m_lblProjectManager.Text = projectWrapper.ProjectManagerName;
            m_lblProjectBalance.Text = wrapper.ProjectBalance.HasValue
                                           ? wrapper.ProjectBalance.Value.ToString("C")
                                           : string.Empty;
            m_lblClosedAmount.Text = wrapper.Project.ClosedAmount.ToString("c");

            m_lblProjectType.Text = projectType.Type;

            m_btnEditProject.Visible = false;
            if (wrapper.Project.ProjectType == ProjectTypeEnum.BasementSystems || wrapper.Project.ProjectType == ProjectTypeEnum.Construction
                || wrapper.Project.ProjectType == ProjectTypeEnum.Content)
                m_btnEditProject.Visible = true;
        }

        #endregion

        #region SelectVisit

        public void SelectVisit(int visitId)
        {
            BindingList<VisitWrapper> visitWrappers = (BindingList<VisitWrapper>)m_gridVisits.DataSource;
            for (int j = 0; j < visitWrappers.Count; j++)
            {
                if (visitWrappers[j].Visit.ID == visitId)
                {
                    int rowHandleVisit = m_gridVisitsView.GetRowHandle(j);
                    m_gridVisitsView.FocusedRowHandle = rowHandleVisit;
                    m_gridVisitsView.SelectRow(rowHandleVisit);
                    break;
                }
            }
        }

        #endregion

        #region OnEidtProjectClick

        private void OnEidtProjectClick(object sender, EventArgs args)
        {
            if (EditProject != null)
            {
                EditProject.Invoke(m_customerProjectWrapper);
            }
        }

        #endregion

        #region OnDashboardLinkClick

        private void OnDashboardLinkClick(object sender, EventArgs e)
        {
            var focusedVisit = (VisitWrapper)m_gridVisitsView.GetRow(
                    m_gridVisitsView.FocusedRowHandle);

            if (VisitSelected != null && focusedVisit != null)
                    VisitSelected.Invoke(focusedVisit.Visit);
        }

        #endregion 
    }
}
