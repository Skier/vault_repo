using System;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.Dashboard;
using Dalworth.Server.MainForm.Leads;
using Dalworth.Server.MainForm.Login;
using Dalworth.Server.MainForm.Projects;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.MainForm.Visits;
using Dalworth.Server.MainForm.Works;
using Dalworth.Server.MainForm.Feedbacks;
using Dalworth.Server.MainForm.Accounting;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using BaseControl=Dalworth.Server.Windows.BaseControl;

namespace Dalworth.Server.MainForm.MainForm
{
    public class MainFormController : Controller<MainFormModel, MainFormView>
    {
        #region Form

        public Form Form
        {
            get { return View; }
        }

        #endregion

        public BaseControl CurrentView
        {
            get { return (BaseControl)View.m_pnlContent.Controls[0]; }
        }

        #region Child Controllers

        #region DashboardController

        private DashboardController m_dashboardController;
        public DashboardController DashboardController
        {
            get { return m_dashboardController; }
            set { m_dashboardController = value; }
        }

        #endregion

        #region ProjectsController

        private ProjectsController m_projectsController;
        public ProjectsController ProjectsController
        {
            get { return m_projectsController; }
            set { m_projectsController = value; }
        }

        #endregion

        #region VisitsController

        private VisitsController m_visitsController;
        public VisitsController VisitsController
        {
            get { return m_visitsController; }
            set { m_visitsController = value; }
        }

        #endregion

        #region WorksController

        private WorksController m_worksController;
        public WorksController WorksController
        {
            get { return m_worksController; }
            set { m_worksController = value; }
        }

        #endregion

        #region ReportsController

        private ReportsController m_reportsController;
        public ReportsController ReportsController
        {
            get { return m_reportsController; }
            set { m_reportsController = value; }
        }

        #endregion

        #region LeadsController

        private LeadsController m_leadsController;
        public LeadsController LeadsController
        {
            get { return m_leadsController; }
            set { m_leadsController = value; }
        }

        #endregion

        #region FeedbacksController

        private FeedbacksController m_feedbacksController;
        public FeedbacksController FeedbacksController
        {
            get { return m_feedbacksController; }
            set { m_feedbacksController = value; }
        }

        #endregion

        #region AccountingController

        private AccountingController m_accountingController;
        public AccountingController AccountingController
        {
            get { return m_accountingController; }
            set {m_accountingController = value; }
        }

        #endregion 

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            LoginController controller = Prepare<LoginController>();
            controller.Execute(false);
            Model.CurrentDispatch = controller.Employee;
            if (!controller.IsLoggedIn)
            {
                View.Destroy();
                return;
            }

            View.m_navDashboard.LinkClicked += OnDashboardClick;
            View.m_navDashboard.LinkPressed += OnDashboardClick;
            View.m_navProjects.LinkClicked += OnProjectsClick;
            View.m_navProjects.LinkPressed += OnProjectsClick;
            View.m_navVisits.LinkClicked += OnVisitsClick;
            View.m_navVisits.LinkPressed += OnVisitsClick;
            View.m_navWorks.LinkClicked += OnWorksClick;
            View.m_navWorks.LinkPressed += OnWorksClick;
            View.m_navReports.LinkClicked += OnReportsClick;
            View.m_navReports.LinkPressed += OnReportsClick;
            
            View.m_navLeads.LinkClicked += OnLeadsClick;
            View.m_navLeads.LinkPressed += OnLeadsClick;

            View.m_navFeedback.LinkClicked += OnFeedbacksClick;
            View.m_navFeedback.LinkPressed += OnFeedbacksClick;

            View.m_navAccounting.LinkClicked += OnAccountingClick;
            View.m_navAccounting.LinkPressed += OnAccountingClick;

            View.m_pnlContent.ControlAdded += OnContentControlAdded;
            View.m_pnlContent.ControlRemoved += OnContentControlRemoved;

            View.KeyDown += OnKeyDown;

            Configuration.MainFormController = this;

            QbItemRugCleaningCatalog.Init();
            QbItemDefloodCatalog.Init();
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblWelcome.Text = "Welcome " + Model.CurrentDispatch.DisplayName;
            ShowDashboard(null, false);
            SelectNavItem(View.m_navDashboard);
            View.m_navReports.Visible = Model.CurrentDispatch.SecurityPermissions.Contains(
                SecurityPermissionEnum.ViewReports);
        }

        #endregion

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch(e.KeyCode)
                {
                    case Keys.D1:
                        OnDashboardClick(null, null);
                        break;
                    case Keys.D2:
                        OnAccountingClick(null, null);
                        break;
                    case Keys.D3:
                        OnLeadsClick(null, null);
                        break;
                    case Keys.D4:
                        OnFeedbacksClick(null, null);
                        break;
                    case Keys.D5: 
                        OnProjectsClick(null, null);
                        break;
                    case Keys.D6: 
                        OnVisitsClick(null, null);
                        break;
                    case Keys.D7: 
                        OnWorksClick(null, null);
                        break;
                    case Keys.D8: 
                        OnReportsClick(null, null);
                        break;
                }                    

            }

        }

        #endregion

        #region Start & Stop changes listener

        private void OnContentControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control is DashboardView)
                DashboardController.IsChangesTrackListenerActive = false;
        }

        private void OnContentControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control is DashboardView)
                DashboardController.IsChangesTrackListenerActive = true;
        }

        #endregion

        #region ShowDashboard

        public void ShowDashboard(Visit visit)
        {
            ShowDashboard(visit, true);
        }

        public void ShowDashboard(Visit visit, bool needRefresh)
        {
            SelectNavItem(View.m_navDashboard);
            View.m_pnlContent.Controls.Clear();

            if (DashboardController == null)
                DashboardController = Prepare<DashboardController>(Model, this);            
            View.m_pnlContent.Controls.Add(DashboardController.View);
            DashboardController.View.Focus();
            DashboardController.SelectVisit(visit, needRefresh);
        }
        
        private void OnDashboardClick(object sender, NavBarLinkEventArgs e)
        {
            ShowDashboard(null);            
        }

        #endregion

        #region ShowProjects

        public void ShowProjectsForm(Project project)
        {
            SelectNavItem(View.m_navProjects);
            View.m_pnlContent.Controls.Clear();

            if (ProjectsController == null)
                ProjectsController = Prepare<ProjectsController>(this);
            View.m_pnlContent.Controls.Add(ProjectsController.View);
            ProjectsController.View.Focus();

            if (project == null)
                ProjectsController.SelectProject(null, false);
            else
                ProjectsController.SelectProject(project, true);
        }

        private void OnProjectsClick(object sender, NavBarLinkEventArgs e)
        {
            ShowProjectsForm(null);
        }

        #endregion

        #region ShowVisits

        public void ShowVisitsForm(Visit visit)
        {
            SelectNavItem(View.m_navVisits);
            View.m_pnlContent.Controls.Clear();

            if (VisitsController == null)
                VisitsController = Prepare<VisitsController>(this);
            View.m_pnlContent.Controls.Add(VisitsController.View);
            VisitsController.View.Focus();

            if (visit == null)
                VisitsController.SelectVisit(null, false);
            else
                VisitsController.SelectVisit(visit, true);
        }

        private void OnVisitsClick(object sender, NavBarLinkEventArgs e)
        {
            ShowVisitsForm(null);            
        }

        #endregion

        #region ShowWorks

        public void ShowWorksForm(Work work)
        {
            SelectNavItem(View.m_navWorks);
            View.m_pnlContent.Controls.Clear();

            if (WorksController == null)
                WorksController = Prepare<WorksController>(this);
            View.m_pnlContent.Controls.Add(WorksController.View);
            WorksController.View.Focus();

            if (work == null)
                WorksController.SelectWork(null, false);
            else
                WorksController.SelectWork(work, true);            
        }

        private void OnWorksClick(object sender, NavBarLinkEventArgs e)
        {
            ShowWorksForm(null);
        }

        #endregion

        #region OnReportsClick

        private void OnReportsClick(object sender, NavBarLinkEventArgs e)
        {
            SelectNavItem(View.m_navReports);
            View.m_pnlContent.Controls.Clear();

            if (ReportsController == null)
                ReportsController = Prepare<ReportsController>(this);
            
            ReportsController.RefreshData();
            View.m_pnlContent.Controls.Add(ReportsController.View);
            ReportsController.View.Focus();
        }

        #endregion

        #region OnLeadsClick

        private void OnLeadsClick(object sender, NavBarLinkEventArgs e)
        {
            SelectNavItem(View.m_navLeads);
            View.m_pnlContent.Controls.Clear();

            if (LeadsController == null)
                LeadsController = Prepare<LeadsController>(this);

            LeadsController.RefreshData();
            View.m_pnlContent.Controls.Add(LeadsController.View);
            LeadsController.View.Focus();
        }

        #endregion

        #region OnFeedbacksClick

        private void OnFeedbacksClick(object sender, NavBarLinkEventArgs e)
        {
            SelectNavItem(View.m_navFeedback);
            View.m_pnlContent.Controls.Clear();

            if (FeedbacksController == null)
                FeedbacksController = Prepare<FeedbacksController>(this);

            FeedbacksController.RefreshData();
            View.m_pnlContent.Controls.Add(FeedbacksController.View);
            FeedbacksController.View.Focus();
        }

        #endregion

        #region OnAccountingClick

        private void OnAccountingClick(object sender, NavBarLinkEventArgs e)
        {
            SelectNavItem(View.m_navAccounting);
            View.m_pnlContent.Controls.Clear();

            if (AccountingController == null)
                AccountingController = Prepare<AccountingController>(this);

            AccountingController.Refresh(false);
            View.m_pnlContent.Controls.Add(AccountingController.View);
            AccountingController.View.Focus();
        }

        #endregion 

        #region SelectNavItem

        private void SelectNavItem(NavBarItem item)
        {
            View.m_navigationControl.SelectedLink = item.Links[0];            
        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {   
            DialogResult dialogResult = XtraMessageBox.Show("Do you want to Log Out? Press No to quit the application", 
                "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Yes)
            {
                LoginController controller = Prepare<LoginController>();
                controller.Execute(false);
                Model.CurrentDispatch = controller.Employee;
                if (!controller.IsLoggedIn)
                    return false;                
                if (CurrentView is DashboardView)
                    ShowDashboard(null, true);
                View.m_navReports.Visible = Model.CurrentDispatch.SecurityPermissions.Contains(
                    SecurityPermissionEnum.ViewReports);
                return true;
            }

            if (dialogResult == DialogResult.No)
                return false;
            return true;                        
        }

        #endregion

    }
}
