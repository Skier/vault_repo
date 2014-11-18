using System;
using System.Collections.Generic;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain.package
{
    public class VisitSummaryDetailPackage
    {
        #region Task

        private Task m_task;
        public Task Task
        {
            get { return m_task; }
            set { m_task = value; }
        }

        #endregion        

        #region TaskName
        
        public string TaskName
        {
            get { return m_task.TaskTypeText.ToUpper(); }            
        }

        #endregion

        #region TaskNameShort

        public string TaskNameShort
        {
            get
            {
                if (m_task.TaskType == TaskTypeEnum.Miscellaneous)
                    return "MISC";
                else
                    return m_task.TaskTypeText.ToUpper();
            }
        }

        #endregion

        #region TaskDescription

        private string m_taskDescription;
        public string TaskDescription
        {
            get { return m_taskDescription; }
            set { m_taskDescription = value; }
        }

        #endregion
    }

    public class VisitSummaryPackage
    {
        #region Constructor

        public VisitSummaryPackage(Visit visit)
            : this(visit, false)
        {
        }
        
        public VisitSummaryPackage(Visit visit, bool isReschedule)
        {
            VisitPackage package = VisitPackage.GetVisit(visit.ID, null);
            
            m_visit = package;

            if (m_visit.Visit.VisitStatus != VisitStatusEnum.Pending)
                m_workDetail = WorkDetail.FindByVisit(m_visit.Visit);

            m_advertisingSources = AdvertisingSource.FindByVisit(m_visit.Visit);
            m_isReschedule = isReschedule;

            m_projects = new List<Project>();
            foreach (TaskPackage taskPackage in m_visit.Tasks)
            {
                Project project = m_projects.Find(delegate(Project tmpProject) { return tmpProject.ID == taskPackage.Task.Project.ID;});
                if (project ==null)
                    m_projects.Add(taskPackage.Task.Project);
            }
        }

        #endregion

        #region IsReschedule

        private bool m_isReschedule;
        public bool IsReschedule
        {
            get { return m_isReschedule; }
            set { m_isReschedule = value; }
        }

        #endregion

        #region VisitPackage

        private VisitPackage m_visit;
        public VisitPackage Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region Projects

        private List<Project> m_projects;
        private List<Project> Projects
        {
            get {return m_projects;}
        }

        #endregion 

        #region WorkDetail

        private WorkDetail m_workDetail;
        public WorkDetail WorkDetail
        {
            get { return m_workDetail; }
            set { m_workDetail = value; }
        }

        #endregion

        #region AdvertisingSources

        private List<AdvertisingSource> m_advertisingSources;
        public List<AdvertisingSource> AdvertisingSources
        {
            get { return m_advertisingSources; }
            set { m_advertisingSources = value; }
        }

        #endregion

        #region Date

        public string Date
        {
            get
            {
                if (!GetAdjustedServiceDate().HasValue)
                    return "UNKNOWN DATE";
                return GetAdjustedServiceDate().Value.Date.ToString("ddd, MMM d yyyy");
            }
        }

        public DateTime? GetAdjustedServiceDate()
        {
            if (IsReschedule)
            {
                return m_visit.Visit.ServiceDate;
            }

            if (m_workDetail != null)
                return m_workDetail.TimeBegin;
            else
                return m_visit.Visit.ServiceDate;                
        }

        #endregion

        #region Map

        public string Map
        {
            get { return m_visit.ServiceAddress.MapServmanFormat.ToUpper(); }
        }

        #endregion

        #region Advertising 

        private string m_advertising = null;
        public string Advertising
        {
            get
            {
                if (!string.IsNullOrEmpty(m_advertising))
                    return m_advertising;

                m_advertising = string.Empty;

                foreach (Project project in m_projects)
                {
                   if (!string.IsNullOrEmpty(project.QbCustomerTypeListId))
                    {
                        QbCustomerType customerType = QbCustomerType.FindByPrimaryKey(project.QbCustomerTypeListId);
                        m_advertising += (string.IsNullOrEmpty(m_advertising) ? string.Empty : ":") + customerType.Name;
                    }
                }

                return m_advertising;
            }
        }

        #endregion

        #region SalesRep

        private string m_salesRep;
        public string SalesRep
        {
            get
            {
                if (!string.IsNullOrEmpty(m_salesRep))
                    return m_salesRep;

                m_salesRep = string.Empty;

                foreach (Project project in m_projects)
                {
                    if (!string.IsNullOrEmpty(project.QbSalesRepListId))
                    {
                        QbSalesRep salesRep = QbSalesRep.FindByPrimaryKey(project.QbSalesRepListId);
                        m_salesRep += (string.IsNullOrEmpty(m_salesRep) ? string.Empty : ":") + salesRep.LastName + " " + salesRep.FirstName;
                    }
                    else if (project.AdvertisingTechnicianId.HasValue)
                    {
                        Employee employee = Employee.FindByPrimaryKey(project.AdvertisingTechnicianId.Value);
                        m_salesRep += (string.IsNullOrEmpty(m_salesRep) ? string.Empty : ":") + employee.DisplayName;
                    }
                }

                return m_advertising;
            }
        }
        #endregion


        #region TimeFrame

        public string TimeFrame
        {
            get
            {
                if (m_visit.Visit.PreferedTimeFrom.HasValue || m_visit.Visit.PreferedTimeTo.HasValue)
                    return m_visit.Visit.TimeFrameText.ToUpper();
                return "SECTOR";
            }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get
            {
                return m_visit.CustomerName.ToUpper();
            }
        }

        #endregion

        #region AddressSingleLine

        public string AddressSingleLine
        {
            get
            {
                return m_visit.ServiceAddress.AddressSingleLine;
            }
        }

        #endregion

        #region AddressFirstLine

        public string AddressFirstLine
        {
            get
            {
                return m_visit.ServiceAddress.AddressFirstLine;
            }
        }

        #endregion

        #region AddressSecondLine

        public string AddressSecondLine
        {
            get
            {
                return m_visit.ServiceAddress.AddressSecondLine;
            }
        }

        #endregion


        #region VisitNumber

        public string VisitNumber
        {
            get { return m_visit.VisitNumber; }
        }

        #endregion

        #region Phones

        public string Phones
        {
            get { return m_visit.Customer.PhonesText; }
        }

        #endregion

        #region VisitNotes

        public string VisitNotes
        {
            get { return "Notes: " + m_visit.Visit.Notes; }
        }

        #endregion

        #region GetRugsDescription

        private string GetRugsDescription(TaskPackage task)
        {
            if (task.Items == null)
                return string.Empty;                        

            string result = string.Empty;

            foreach (Item item in task.Items)
            {                
                if (item.ItemShape == ItemShapeEnum.Round)
                    result += "D" + Utils.RemoveTrailingZeros(item.Diameter) + " ";
                else
                    result += Utils.RemoveTrailingZeros(item.Width) + "x" 
                        + Utils.RemoveTrailingZeros(item.Height) + " ";

                if (item.IsProtectorApplied)
                    result += "PR ";
                if (item.IsPaddingApplied)
                    result += "PAD ";
                if (item.IsMothRepelApplied)
                    result += "MOTH ";
                if (item.IsRapApplied)
                    result += "RAP ";
            }

            if (result != string.Empty)
            {
                if (task.Task.TaskType == TaskTypeEnum.RugPickup)
                {
                    if (task.Task.TaskStatus == TaskStatusEnum.Completed)
                    {
                        if (task.Task.Project.ProjectType == ProjectTypeEnum.Deflood)
                            result += "- " + task.Task.ClosedAmount.ToString("C");
                        else
                            result += "- " + task.Task.EstimatedClosedAmount.ToString("C");
                    } 
                }
                else if (task.Task.TaskType == TaskTypeEnum.RugDelivery)
                {
                    if (task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                    {
                        if (task.Task.TaskStatus == TaskStatusEnum.Completed)
                            result += "- " + task.Task.ClosedAmount.ToString("C");
                        else
                            result += "- " + task.Task.EstimatedClosedAmount.ToString("C");
                    }
                }
            }

            return result;
        }

        #endregion

        #region Tasks

        public List<VisitSummaryDetailPackage> Tasks
        {
            get
            {
                List<VisitSummaryDetailPackage> result = new List<VisitSummaryDetailPackage>();

                foreach (TaskPackage task in m_visit.Tasks)
                {
                    if (task.Task.TaskType == TaskTypeEnum.Deflood)
                        continue;                    

                    VisitSummaryDetailPackage taskDetail = new VisitSummaryDetailPackage();

                    taskDetail.Task = task.Task;
                    task.Task.InitPreviousTaskNotes();

                    if (task.Task.TaskType == TaskTypeEnum.RugPickup 
                        || task.Task.TaskType == TaskTypeEnum.RugDelivery)
                    {
                        string rugsDescription = GetRugsDescription(task);                        
                        if (rugsDescription != string.Empty)
                        {
                            if (task.Task.TaskType == TaskTypeEnum.RugPickup)
                                taskDetail.TaskDescription += rugsDescription;
                            else
                            {
                                Task pickupTask = Task.FindRugPickup(task.Task);
                                Visit pickupVisit = Domain.Visit.FindByTaskLastFirst(pickupTask, true);

                                taskDetail.TaskDescription += "PU " + taskDetail.Task.CreateDate.Value.ToShortDateString()
                                    + " " + Printer.StartBoldEscapeString + pickupVisit.ID 
                                    + Printer.EndBoldEscapeString
                                    + " " + rugsDescription;
                            }
                            
                            taskDetail.TaskDescription += "\r\n";
                        }
                            
                    }
                    else if (task.Task.TaskType == TaskTypeEnum.Monitoring)
                    {
                        TaskPackage deflood = FindLinkedDeflood(task);
                        bool isFirstMonitoring = Task.IsFirstMonitoring(task.Task);

                        if (isFirstMonitoring)
                            taskDetail.Task = deflood.Task;

                        taskDetail.TaskDescription
                            = Utils.JoinStrings("\r\n", deflood.Task.Notes, task.Task.Notes);
                        
                        result.Add(taskDetail);
                        continue;
                    }

                    taskDetail.TaskDescription += task.Task.Notes;

                    if (!string.IsNullOrEmpty(task.Task.PreviousNotes))
                        taskDetail.TaskDescription += "\r\nPREV NOTES: " + task.Task.PreviousNotes;

                    result.Add(taskDetail);
                }
                

                return result;
            }
        }        

        private TaskPackage FindLinkedDeflood(TaskPackage monitoring)
        {
            foreach (TaskPackage task in m_visit.Tasks)
            {
                if (monitoring.Task.ParentTaskId == task.Task.ID && task.Task.TaskType == TaskTypeEnum.Deflood)
                    return task;
            }

            return null;
        }

        #endregion

        #region TasksPlainText

        private string TasksPlainText
        {
            get
            {
                string result = string.Empty;
                foreach (VisitSummaryDetailPackage task in Tasks)
                    result += task.TaskName + task.TaskDescription;
                return result;
            }
        }

        #endregion
        


        #region IsPrintDataEqual

        public bool IsPrintDataEqual(VisitSummaryPackage summaryPackage)
        {
            return Date.Equals(summaryPackage.Date)
                   && Map.Equals(summaryPackage.Map)
                   && TimeFrame.Equals(summaryPackage.TimeFrame)
                   && CustomerName.Equals(summaryPackage.CustomerName)
                   && AddressSingleLine.Equals(summaryPackage.AddressSingleLine)
                   && VisitNumber.Equals(summaryPackage.VisitNumber)
                   && Phones.Equals(summaryPackage.Phones)
                   && VisitNotes.Equals(summaryPackage.VisitNotes)
                   && TasksPlainText.Equals(summaryPackage.TasksPlainText);
        }

        #endregion

        #region Print

        public void Print()
        {
            using (Printer printer = new Printer(Configuration.VisitPrinter))
            {
                printer.Open("Visit " + VisitNumber);                
                printer.Initialize();                

                //Date and map                
                printer.StartBoldDoubleWidth();
                printer.Write(Date);
                printer.EndBoldDoubleWidth();

                printer.MovePrintHeadTo("MAP: ".Length + Map.Length * 2);
                printer.Write("MAP: ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(Map);
                printer.EndBoldDoubleWidth();
                printer.WriteLine();

                //Advertising and time frame
                int timeSectionLength = "Time: ".Length + TimeFrame.Length*2;

                List<string> advertisingLines = Utils.DivideText("AD: " + Advertising,
                    Printer.PAGE_COLUMNS_COUNT - (timeSectionLength + 1));

                for (int i = 0; i < advertisingLines.Count; i++)
                {
                    if (i == advertisingLines.Count - 1)
                        printer.Write(advertisingLines[i]);
                    else
                        printer.WriteLine(advertisingLines[i]);
                }

                printer.MovePrintHeadTo(timeSectionLength);
                printer.Write("Time: ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(TimeFrame);
                printer.EndBoldDoubleWidth();

                if (!string.IsNullOrEmpty(SalesRep))
                {
                    List<string> salesRepLines = Utils.DivideText("SalesRep: " + SalesRep,
                        Printer.PAGE_COLUMNS_COUNT - (timeSectionLength + 1));

                    for (int i = 0; i < salesRepLines.Count; i++)
                    {
                        printer.WriteLine(salesRepLines[i]);
                    }    
                }
                
                //Customer name
                printer.StartBoldDoubleWidth();
                var name = CustomerName;
                if (name.Length > Printer.PAGE_COLUMNS_COUNT/2 -1)
                    name = name.Substring(0, Printer.PAGE_COLUMNS_COUNT/2 - 1);
                printer.WriteLine(name);
                printer.EndBoldDoubleWidth();

                //Address and visit number (2 lines)
                printer.Write(AddressFirstLine);
               
                if (Projects.Count > 0)
                {
                    string projectString = string.Empty;
                    for (int i = 0; i < Projects.Count; i++)
                    {
                        if (i > 0)
                            projectString += ",";

                        projectString += Projects[i].ID.ToString();
                    }

                    printer.MovePrintHeadTo("Job#:".Length + projectString.Length*2);
                    printer.Write("Job#:");
                    printer.StartBoldDoubleWidth();
                    printer.Write(projectString);
                    printer.EndBoldDoubleWidth();
                }

                printer.WriteLine();

                printer.Write(AddressSecondLine);
                printer.MovePrintHeadTo("Visit: ".Length + VisitNumber.Length * 2);
                printer.Write("Visit: ");
                printer.StartBoldDoubleWidth();
                printer.WriteLine(VisitNumber);
                printer.EndBoldDoubleWidth();

                //Phones
                printer.WriteLine(Phones);

                //Empty line after phones
                printer.WriteLine();

                //Visit Notes
                List<string> visitNotesLines = Utils.DivideText(VisitNotes, Printer.PAGE_COLUMNS_COUNT);
                foreach (string line in visitNotesLines)
                    printer.WriteLine(line);

                //Printing tasks

                foreach (VisitSummaryDetailPackage task in Tasks)
                {
                    printer.StartBold();
                    printer.Write(task.TaskNameShort + " ");
                    printer.EndBold();

                    List<int> lineMaxLengths = new List<int>();
                    lineMaxLengths.Add(Printer.PAGE_COLUMNS_COUNT - 2 - task.TaskNameShort.Length - 1);
                    // -3 adjustment for tab in second and more line of task notes
                    lineMaxLengths.Add(Printer.PAGE_COLUMNS_COUNT - 2 - 3);
                    
                    List<string> formattedDescriptionLines = Utils.DivideText(
                        task.TaskDescription, lineMaxLengths);

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

        #region GetPagerMessageText

        public string GetPagerMessageText()
        {
            string result = CustomerName;
            result += " " + AddressSingleLine;
            if (m_visit.Customer.Phone1 != string.Empty)
                result += " RES.H " + m_visit.Customer.Phone1Formatted;
            if (m_visit.Customer.Phone2 != string.Empty)
                result += " BUS.H " + m_visit.Customer.Phone2Formatted;
            result += " MAP:" + Map;
            result += " /Job:";
            for (int i=0; i< Projects.Count; i++)
            {
                if (i > 0)
                    result += ",";
                result += Projects[i].ID;
            }
            
            result += PagerMessage.BLOCK_SEPARATOR;
            
            if (m_visit.Notes.Replace("\r\n", " ").Trim() != string.Empty)
            {
                result += "NOTES:" + m_visit.Notes.Replace("\r\n", " ").Trim();
                result += PagerMessage.BLOCK_SEPARATOR;
            }

            foreach (VisitSummaryDetailPackage task in Tasks)
            {
                task.TaskDescription = task.TaskDescription.Replace(Printer.StartBoldEscapeString, string.Empty);
                task.TaskDescription = task.TaskDescription.Replace(Printer.EndBoldEscapeString, string.Empty);

                result += "[" + task.TaskName + "]";
                if (task.TaskDescription.Trim() != string.Empty)
                    result += ":" + task.TaskDescription.Replace("\r\n", " ").Trim() + " ";

                result += PagerMessage.BLOCK_SEPARATOR;
            }
            
            return result;
        }

        #endregion
    }
}
