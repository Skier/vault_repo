using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain.package
{
    public class ProjectPrint
    {
        private readonly Project m_project;
        private readonly ProjectConstructionDetail m_projectDetail;
        private readonly Employee m_projectManager;
        private readonly Customer m_customer;
        private readonly Address m_address;
        private readonly string m_progress;

        #region Constructor

        public ProjectPrint(ProjectWrapper projectWrapper)
        {
            m_project = projectWrapper.Project;
            m_projectDetail = projectWrapper.ConstructionDetail;

            if (m_projectDetail.ProjectManagerEmployeeId.HasValue && projectWrapper.ProjectManager == null)
                projectWrapper.ProjectManager = Employee.FindByPrimaryKey(m_projectDetail.ProjectManagerEmployeeId.Value, null);
            m_projectManager = projectWrapper.ProjectManager;

            m_customer = projectWrapper.Customer;
            m_address = projectWrapper.CustomerAddress;
            m_progress = projectWrapper.Progress;
        }

        #endregion

        #region ProjectId

        public string ProjectId
        {
            get { return m_project.ID.ToString(); }
        }

        #endregion

        #region Name

        public string Name
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region ProjectName

        private string m_projectName;
        public string ProjectName
        {
            get 
            {
                if (!string.IsNullOrEmpty(m_projectName))
                {
                    return m_projectName;
                }

                ProjectType projectType = ProjectType.FindByPrimaryKey((int)m_project.ProjectType);
                m_projectName = (projectType.Type + " " + m_progress).ToUpper();
                return m_projectName;
            }
        }

        #endregion

        #region JobDate

        public string JobDate
        {
            get
            {
                if (!m_projectDetail.SignUpDate.HasValue)
                    return "UNKNOWN DATE";
                return m_projectDetail.SignUpDate.Value.Date.ToString("d");
            }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region Notes

        public string Notes
        {
            get { return m_project.Description; }
        }

        #endregion

        #region AddressFirstLine

        public string AddressFirstLine
        {
            get { return m_address.AddressFirstLine; }
        }

        #endregion

        #region AddressSecondLine

        public string AddressSecondLine
        {
            get { return m_address.AddressSecondLine; }
        }

        #endregion

        #region Mapsco

        public string Mapsco
        {
            get { return m_address.Map; }
        }

        #endregion

        #region Phones

        public string Phones
        {
            get
            {
                string result = string.Empty;

                if (m_customer.Phone1Formatted != string.Empty)
                    result += "HM " + m_customer.Phone1Formatted + "   ";

                if (m_customer.Phone2Formatted != string.Empty)
                    result += "BS " + m_customer.Phone2Formatted;

                return result;
            }
        }

        #endregion

        #region InsuranceCompany

        public string InsuranceCompany
        {
            get
            {
                if (m_project.InsuranceCompany == null)
                    return string.Empty;
                return m_project.InsuranceCompany.ToUpper();
            }
        }

        #endregion

        #region ProjectManager

        public string ProjectManager
        {
            get
            {
                if (m_projectManager != null)
                    return m_projectManager.DisplayName;
                return string.Empty;
            }
        }

        #endregion

        #region LeadDate

        public string LeadDate
        {
            get { return m_project.CreateDate.ToString("d"); }
        }

        #endregion

        #region LeadDateLong

        public string LeadDateLong
        {
            get { return m_project.CreateDate.ToLongDateString(); }
        }

        #endregion

        #region ScopeDate

        public string ScopeDate
        {
            get
            {
                return m_projectDetail.ScopeDate == null ? string.Empty
                           : m_projectDetail.ScopeDate.Value.ToString("d");
            }
        }

        #endregion

        #region JobNumber

        public string JobNumber
        {
            get
            {
                if (m_projectDetail.JobNumber == null || m_projectDetail.JobNumber == String.Empty)
                    return "N/A";
                return m_projectDetail.JobNumber.ToUpper();
            }
        }

        #endregion

        #region EstimatedAmount

        public string ProjectAmount
        {
            get
            {
                string result;

                if (m_project.ClosedAmount > 0)
                {
                    result = "Scope Amount: " + m_project.ClosedAmount.ToString("C");
                }
                else
                {
                    result = "Estimate Amount: ";
                    if (m_projectDetail.EstimatedAmount == decimal.Zero)
                        result += "N/A";
                    else
                        result +=  m_projectDetail.EstimatedAmount.ToString("C");
                }
                return result;
            }
        }

        #endregion

        #region AdvertisingSource

        private string m_advertisingSource;
        public string AdvertisingSource
        {
            get
            {
                if (m_advertisingSource != null)
                    return m_advertisingSource;

                m_advertisingSource = string.Empty;

                if (m_project.AdvertisingSourceId.HasValue)
                    m_advertisingSource = Domain.AdvertisingSource.FindByPrimaryKey(
                        m_project.AdvertisingSourceId.Value).Name;

                if (m_project.AdvertisingTechnicianId.HasValue)
                    m_advertisingSource += ", " + Employee.FindByPrimaryKey(
                        m_project.AdvertisingTechnicianId.Value).DisplayName;

                return m_advertisingSource;
            }
        }

        #endregion

        #region DamageType

        public string DamageType
        {
            get
            {
                if (m_projectDetail.ConstructionDamageType.HasValue)
                    return m_projectDetail.ConstructionDamageType.Value.ToString();
                return m_projectDetail.DamageTypeText;
            }
        }

        #endregion

        #region LossDate

        public string LossDate
        {
            get
            {
                if (m_projectDetail.LossDate == null)
                    return string.Empty;

                return m_projectDetail.LossDate.Value.ToString("d");
            }
        }

        #endregion

        #region DamageOrigin

        public string DamageOrigin
        {
            get
            {
                return m_projectDetail.DamageOrigin;
            }
        }

        #endregion

        #region DeductibleAmount

        public string DeductibleAmount
        {
            get
            {
                if (m_project.DeductibleAmount == decimal.Zero)
                    return string.Empty;

                return m_project.DeductibleAmount.ToString("C");
            }
        }

        #endregion

        #region Adjuster

        public string Adjuster
        {
            get { return m_project.InsuranceAdjustor; }
        }

        #endregion

        #region ClaimNumber

        public string ClaimNumber
        {
            get { return m_project.ClaimNumber; }
        }

        #endregion

        #region AdditionalNotes

        public string AdditionalNotes
        {
            get { return m_project.Description; }
        }

        #endregion

        #region Print

        public void Print()
        {
            using (Printer printer = new Printer(Configuration.VisitPrinter))
            {
                
                printer.Open("Project " + m_project.ID);
                printer.Initialize();

                printer.StartBoldDoubleWidth();
                printer.Write(ProjectName);
                printer.MovePrintHeadTo(ProjectId.Length * 2);
                printer.WriteLine(ProjectId);
                printer.EndBoldDoubleWidth();
                printer.WriteLine();

                printer.StartBoldDoubleWidth();
                printer.Write(CustomerName);
                printer.EndBoldDoubleWidth();
                printer.MovePrintHeadTo("MAP: ".Length + Mapsco.Length * 2);
                printer.Write("MAP: ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(Mapsco);
                printer.EndBoldDoubleWidth();

                printer.StartBoldDoubleWidth();
                printer.WriteLine(AddressFirstLine);
                printer.WriteLine(AddressSecondLine);
                printer.EndBoldDoubleWidth();
                printer.WriteLine(Phones);
                printer.WriteLine();

                printer.Write("Project Manager: ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(ProjectManager);
                printer.EndBoldDoubleWidth();
                printer.Write("Insurance: ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(InsuranceCompany);
                printer.EndBoldDoubleWidth();
                printer.WriteLine(ProjectAmount);

                if (m_progress == "Job")
                {
                    printer.Write("Job Date: " + JobDate);
                    string jobNumber = "Job Number: " + JobNumber;
                    printer.MovePrintHeadTo(jobNumber.Length);
                    printer.WriteLine(jobNumber);
                }

                if (m_progress == "Lead")
                {
                    printer.Write("Lead Date: " + LeadDate);
                    printer.MovePrintHeadTo(Printer.PAGE_COLUMNS_COUNT - 30);
                    printer.WriteLine("Scope Appt: " + ScopeDate);
                }
                
                printer.WriteLine();

                if (Notes != null && Notes.Length > 0)
                {
                    List<string> formattedDescriptionLines = Utils.DivideText(
                           Notes, Printer.PAGE_COLUMNS_COUNT);

                    for (int i = 0; i < formattedDescriptionLines.Count; i++)
                    {
                        string descriptionLine = formattedDescriptionLines[i];
                        if (i > 0)
                            descriptionLine = "   " + descriptionLine;

                        printer.WriteLine(descriptionLine);
                    }
                }

                printer.WriteLine();

                printer.WriteLine("Printed: " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());
                printer.GoToNextPage();
            }
        }

        #endregion

    }
}
