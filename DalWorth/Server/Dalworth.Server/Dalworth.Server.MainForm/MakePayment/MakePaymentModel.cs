using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.Domain.Sync;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using Address=Dalworth.Server.Domain.Address;
using Customer=Dalworth.Server.Domain.Customer;
using Item=Dalworth.Server.Domain.Item;
using Task=Dalworth.Server.Domain.Task;
using TaskPackage=Dalworth.Server.Domain.package.TaskPackage;
using Visit=Dalworth.Server.Domain.Visit;
using VisitCompletePackage=Dalworth.Server.Domain.package.VisitCompletePackage;
using Work=Dalworth.Server.Domain.Work;
using WorkTransactionPayment=Dalworth.Server.Domain.WorkTransactionPayment;

namespace Dalworth.Server.MainForm.MakePayment
{    
    public class MakePaymentModel : IModel
    {
        #region CompletePackage

        private VisitCompletePackage m_completePackage;
        public VisitCompletePackage CompletePackage
        {
            get { return m_completePackage; }
            set { m_completePackage = value; }
        }

        #endregion

        #region Payments

        private BindingList<PaymentInfo> m_payments;
        public BindingList<PaymentInfo> Payments
        {
            get { return m_payments; }
        }

        #endregion

        #region Projects

        private List<Project> m_initialProjects;
        private BindingList<ProjectWrapper> m_projects;
        public BindingList<ProjectWrapper> Projects
        {
            get { return m_projects; }
            set { m_projects = value; }
        }

        #endregion

        #region CurrentPaymentAmount

        public decimal CurrentPaymentAmount
        {
            get
            {
                decimal result = decimal.Zero;
                foreach (PaymentInfo payment in m_payments)
                    result += payment.PaymentAmount;

                return result;
            }
        }

        #endregion

        #region CurrentDistibutedAmount

        public decimal CurrentDistibutedAmount
        {
            get
            {
                decimal result = decimal.Zero;

                foreach (ProjectWrapper project in m_projects)
                    result += project.PaidAmount;

                return result;
            }
        }

        #endregion

        #region OtherProjects

        private List<ProjectWrapper> m_otherProjects;
        public List<ProjectWrapper> OtherProjects
        {
            get { return m_otherProjects; }
        }

        #endregion

        #region IsRecomplete

        private bool m_isRecomplete;
        public bool IsRecomplete
        {
            get { return m_isRecomplete; }
        }

        #endregion

        #region IsOtherProjectsPaidLastTime

        private bool m_isOtherProjectsPaidLastTime;
        public bool IsOtherProjectsPaidLastTime
        {
            get { return m_isOtherProjectsPaidLastTime; }
        }

        #endregion

        #region Init

        public void Init()
        {   
            Visit visit = Visit.FindByPrimaryKey(m_completePackage.Visit.ID);
            WorkTransaction completeTransaction = null;
            if (visit.VisitStatus == VisitStatusEnum.Completed)
            {
                m_isRecomplete = true;
                completeTransaction = WorkTransaction.FindBy(visit.ID, 
                    WorkTransactionTypeEnum.VisitCompleted);
            }                            

            m_payments = new BindingList<PaymentInfo>();            
            if (m_isRecomplete)
            {
                List<WorkTransactionPayment> existingPayments
                    = WorkTransactionPayment.FindBy(completeTransaction);

                foreach (WorkTransactionPayment payment in existingPayments)
                    m_payments.Add(payment.Export<PaymentInfo>());
            } 

            if (m_payments.Count == 0)
            {
                PaymentInfo defaultPayment = new PaymentInfo();
                defaultPayment.WorkTransactionPaymentType = WorkTransactionPaymentTypeEnum.CreditCard;
                defaultPayment.PaymentAmount = decimal.Zero;
                m_payments.Add(defaultPayment);                
            }

            m_projects = new BindingList<ProjectWrapper>();
            m_initialProjects = new List<Project>();
            foreach (TaskProjectWrapperComplete task in m_completePackage.Tasks)
            {
                if (task.IsProject && task.IsIncludedInVisit)
                {
                    task.Task.Project.PaidAmount = decimal.Zero;
                    m_initialProjects.Add(task.Task.Project);
                    m_projects.Add(task.Task.Project.Export<ProjectWrapper>());
                }                    
            }
            
            List<Project> allOtherProjects = Project.FindByCustomerId(visit.CustomerId.Value);
            m_otherProjects = new List<ProjectWrapper>();
            foreach (Project otherProject in allOtherProjects)
            {
                otherProject.PaidAmount = decimal.Zero;
                m_otherProjects.Add(otherProject.Export<ProjectWrapper>());
            }                           

            foreach (Project initialProject in m_initialProjects)
            {
                foreach (ProjectWrapper otherProject in m_otherProjects)
                {
                    if (otherProject.ID == initialProject.ID)
                    {
                        m_otherProjects.Remove(otherProject);
                        break;
                    }
                }                
            }


            if (m_isRecomplete)
            {
                List<Project> createdProjects = Project.FindBy(completeTransaction, false, true);
                foreach (Project affectedProject in createdProjects)
                {
                    foreach (ProjectWrapper otherProject in m_otherProjects)
                    {
                        if (affectedProject.ID == otherProject.ID)
                        {
                            m_otherProjects.Remove(otherProject);
                            break;
                        }
                    }                                    
                }
            }
            
            

            m_otherProjects.Sort(
                delegate(ProjectWrapper x, ProjectWrapper y)
                { return x.CreateDate.CompareTo(y.CreateDate) * -1;});

            if (m_isRecomplete)
            {
                List<ProjectPayment> previousPayments = ProjectPayment.FindBy(completeTransaction);

                foreach (ProjectPayment previousPayment in previousPayments)
                {
                    foreach (ProjectWrapper project in m_projects)
                    {
                        if (project.ID == previousPayment.ProjectId)
                        {
                            project.PaidAmount = previousPayment.Amount;
                            break;
                        }
                    }

                    foreach (ProjectWrapper otherProject in m_otherProjects)
                    {
                        if (otherProject.ID == previousPayment.ProjectId)
                        {
                            m_isOtherProjectsPaidLastTime = true;
                            otherProject.PaidAmount = previousPayment.Amount;
                            break;
                        }                        
                    }
                }
            }
        }

        #endregion        

        #region RefreshProjects

        public void RefreshProjects(bool showOtherProjects)
        {
            while (m_projects.Count > m_initialProjects.Count)
            {
                if (m_initialProjects.Count == 1 && !m_isRecomplete)
                    m_projects[m_initialProjects.Count].PaidAmount = decimal.Zero;
                m_projects.RemoveAt(m_initialProjects.Count);
            }                

            if (showOtherProjects)
                foreach (ProjectWrapper otherProject in m_otherProjects)
                    m_projects.Add(otherProject);
            m_projects.ResetBindings();
        }

        #endregion

        #region PutPaymentInfoToCompletePackage

        public void PutPaymentInfoToCompletePackage()
        {
            m_completePackage.Payments = new List<WorkTransactionPayment>();
            foreach (PaymentInfo paymentInfo in m_payments)
                m_completePackage.Payments.Add(paymentInfo);

            m_completePackage.PaidProjects = new List<Project>();
            foreach (ProjectWrapper project in m_projects)
            {
                if (project.PaidAmount > decimal.Zero)
                    m_completePackage.PaidProjects.Add(project);
            }
        }

        #endregion
    }

    public class PaymentInfo : WorkTransactionPayment, IDataErrorInfo
    {
        #region ShowAmountError

        private bool m_showAmountError;
        public bool ShowAmountError
        {
            get { return m_showAmountError; }
            set { m_showAmountError = value; }
        }

        #endregion

        #region ShowPaymentMethodError

        private bool m_showPaymentMethodError;
        public bool ShowPaymentMethodError
        {
            get { return m_showPaymentMethodError; }
            set { m_showPaymentMethodError = value; }
        }

        #endregion

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                if (m_showPaymentMethodError && columnName == "WorkTransactionPaymentTypeId" 
                    && WorkTransactionPaymentTypeId == 0)
                {
                    return "Please select payment method";
                }

                if (m_showAmountError && columnName == "PaymentAmount" && PaymentAmount <= 0)
                    return "Please enter positive payment amount";


                return string.Empty;
            }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        #endregion

        #region IsValid

        public bool IsValid
        {
            get
            {
                if (WorkTransactionPaymentTypeId == 0 || PaymentAmount <= 0)
                    return false;
                return true;
            }
        }

        #endregion

    }

    public class ProjectWrapper : Project
    {
        #region ClosedAmountTax

        public decimal ClosedAmountTax
        {
            get { return ClosedAmount * QbItemRugCleaningCatalog.QbItemSalesTax.TaxRate/ 100; }
        }

        #endregion

        #region ClosedAmountWithTax

        public decimal ClosedAmountWithTax
        {
            get { return ClosedAmount + ClosedAmountTax; }
        }

        #endregion
    }
}
