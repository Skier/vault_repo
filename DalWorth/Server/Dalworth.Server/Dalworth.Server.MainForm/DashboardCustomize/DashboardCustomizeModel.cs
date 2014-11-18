using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.DXErrorProvider;

namespace Dalworth.Server.MainForm.DashboardCustomize
{
    public class DashboardCustomizeModel : IModel
    {
        //key - index, value - technician ID
        private Dictionary<int, int> m_initialTechnicianSequence;

        #region DashboardDate

        private DateTime m_dashboardDate;
        public DateTime DashboardDate
        {
            get { return m_dashboardDate; }
            set { m_dashboardDate = value; }
        }

        #endregion

        #region Technicians

        private BindingList<TechnicianCustomize> m_technicians;
        public BindingList<TechnicianCustomize> Technicians
        {
            get { return m_technicians; }
        }

        #endregion

        #region TechnicianChoiceList

        private List<Employee> m_technicianChoiceList;
        public List<Employee> TechnicianChoiceList
        {
            get { return m_technicianChoiceList; }
        }

        #endregion

        #region UnknownTechnicianMap

        private Dictionary<int, Employee> m_unknownTechnicianMap;
        public Dictionary<int, Employee> UnknownTechnicianMap
        {
            get { return m_unknownTechnicianMap; }
        }

        #endregion

        #region Init

        public void Init()
        {
            List<Employee> unknownsTechnicians = Employee.FindUnknownTechnicians();
            m_unknownTechnicianMap = new Dictionary<int, Employee>();
            foreach (Employee employee in unknownsTechnicians)
                m_unknownTechnicianMap.Add(employee.ID, employee);
     
            m_technicians = new BindingList<TechnicianCustomize>();

            List<DashboardSharedSetting> settings = DashboardSharedSetting.FindSettings(m_dashboardDate);
            if (settings.Count == 0)
            {                
                for (int i = 0; i < unknownsTechnicians.Count; i++)
                {
                    DashboardSharedSetting setting = new DashboardSharedSetting(
                        m_dashboardDate, unknownsTechnicians[i].ID, unknownsTechnicians[i].ID, true, i);
                    setting.Technician = unknownsTechnicians[i];
                    Work work = Work.FindWorkByTechAndDate(setting.Technician.ID, m_dashboardDate);
                    m_technicians.Add(new TechnicianCustomize(setting, work, i));
                }                
            } else
            {
                for (int i = 0; i < settings.Count; i++)
                {
                    Work work = Work.FindWorkByTechAndDate(settings[i].Technician.ID, m_dashboardDate);
                    m_technicians.Add(new TechnicianCustomize(settings[i], work, i));                    
                }
            }

            m_initialTechnicianSequence = new Dictionary<int, int>();
            for (int i = 0; i < m_technicians.Count; i++)
                m_initialTechnicianSequence.Add(i, m_technicians[i].Setting.TechnicianId);

            m_technicianChoiceList = Employee.FindRealTechnicians();
        }

        #endregion

        #region Save

        public void Save()
        {                        
            List<Work> initialWorks = Work.FindBy(m_dashboardDate);            
            Dictionary<int, List<WorkDetail>> initialAssignedDetails = new Dictionary<int, List<WorkDetail>>();
            foreach (Work work in initialWorks)
            {
                List<WorkDetail> workDetails = WorkDetail.FindByWorkAssigned(work);
                initialAssignedDetails.Add(work.ID, workDetails);
            }

            for (int i = 0; i < Technicians.Count; i++)
            {
                int index = Technicians[i].ItemIndex;
                int indexInit = Technicians[i].ItemIndexInitial;

                if (indexInit == index)
                    continue;


                Work workSource = null;
                List<WorkDetail> detailsSource = null;

                foreach (Work initialWork in initialWorks)
                {
                    if (initialWork.TechnicianEmployeeId == m_initialTechnicianSequence[index])
                    {
                        workSource = (Work) initialWork.Clone();
                        detailsSource = initialAssignedDetails[workSource.ID];
                        foreach (WorkDetail detail in detailsSource)
                            WorkDetail.Delete(detail);                            
                        break;
                    }
                }

                Work currentWork = null;
                foreach (Work initialWork in initialWorks)
                {
                    if (initialWork.TechnicianEmployeeId == Technicians[index].Setting.TechnicianId)
                    {
                        currentWork = (Work)initialWork.Clone();
                        List<WorkDetail> currentDetails = WorkDetail.FindByWorkAssigned(currentWork);
                        foreach (WorkDetail detail in currentDetails)
                            WorkDetail.Delete(detail);
                        break;
                    }
                }


                if (workSource != null)
                {                    
                    if (currentWork == null)
                    {
                        currentWork = (Work)workSource.Clone();
                        currentWork.TechnicianEmployeeId = Technicians[index].Setting.Technician.ID;
                        currentWork.VanId = null;
                        currentWork.StartMessage = string.Empty;
                        currentWork.EndMessage = string.Empty;
                        currentWork.EquipmentNotes = string.Empty;
                        currentWork.IsSentToServman = false;
                        currentWork.CreateDate = DateTime.Now;
                        currentWork.WorkStatus = WorkStatusEnum.Pending;
                        Work.Insert(currentWork);                        
                    }

                    foreach (WorkDetail detail in detailsSource)
                    {
                        detail.WorkId = currentWork.ID;
                        WorkDetail.InsertAndLog(detail);
                    }

                } else
                {
                    //trying to delete current work if possible
                    if (currentWork != null && currentWork.WorkStatus == WorkStatusEnum.Pending)
                        Work.Delete(currentWork);                    
                }


                             
            }

            initialWorks = Work.FindBy(m_dashboardDate);
            DashboardSharedSetting.DeleteSettings(m_dashboardDate);
            for (int i = 0; i < Technicians.Count; i++)
            {
                if (Technicians[i].Setting.TechnicianId != Technicians[i].Setting.Technician.ID)
                {
                    foreach (Work initialWork in initialWorks)
                    {
                        if (initialWork.TechnicianEmployeeId == Technicians[i].Setting.TechnicianId)
                        {
                            Work work = (Work) initialWork.Clone();
                            work.TechnicianEmployeeId = Technicians[i].Setting.Technician.ID;
                            Work.Update(work);
                            break;
                        }
                    }
                }

                Technicians[i].Sequence = i;
                Technicians[i].Setting.TechnicianId = Technicians[i].Setting.Technician.ID;
                DashboardSharedSetting.Insert(Technicians[i].Setting);
            }
        }

        #endregion

        #region IsEveryoneOff

        public bool IsEveryoneOff()
        {
            foreach (TechnicianCustomize customize in m_technicians)
            {
                if (customize.Setting.IsVisible)
                    return false;
            }
            return true;
        }

        #endregion

        #region FindDuplicateTechnician

        public TechnicianCustomize FindDuplicateTechnician()
        {
            List<int> duplicateIds = new List<int>();

            foreach (TechnicianCustomize customize in m_technicians)
            {
                if (duplicateIds.Contains(customize.Setting.Technician.ID))
                    return customize;
                duplicateIds.Add(customize.Setting.Technician.ID);
            }

            return null;
        }

        #endregion

        #region FindFirstInvisibleWithWork

        public TechnicianCustomize FindFirstInvisibleWithWork()
        {
            foreach (TechnicianCustomize customize in m_technicians)
            {
                if (!customize.Setting.IsVisible)
                {
                    if (Work.FindWorkByTechAndDate(
                        customize.Setting.TechnicianId, m_dashboardDate) != null)
                    {
                        return customize;
                    }
                }
            }
            return null;
        }

        #endregion
    }

    public class TechnicianCustomize
    {
        #region ItemIndex

        private int m_itemIndex;
        public int ItemIndex
        {
            get { return m_itemIndex; }
            set { m_itemIndex = value; }
        }

        #endregion

        #region InitialIndex

        private int m_itemIndexInitial;
        public int ItemIndexInitial
        {
            get { return m_itemIndexInitial; }
        }

        #endregion

        #region Setting

        private DashboardSharedSetting m_setting;
        public DashboardSharedSetting Setting
        {
            get { return m_setting; }
        }

        #endregion

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
        }

        #endregion       

        #region Constructor

        public TechnicianCustomize(DashboardSharedSetting setting, Work work, int itemIndex)
        {
            m_itemIndex = itemIndex;
            m_setting = setting;
            m_work = work;
            m_itemIndexInitial = itemIndex;
        }

        #endregion

        #region Name

        public string Name
        {
            get
            {
                string spaces = "   ";
                if ((m_itemIndex + 1).ToString().Length == 2)
                    spaces = " ";
                else if ((m_itemIndex + 1).ToString().Length > 2)
                    spaces = "";

                return m_itemIndex + 1 + ":" + spaces + m_setting.Technician.DisplayName;
            }
        }

        #endregion

        #region ImageIndex

        public int ImageIndex
        {
            get { return m_setting.IsVisible ? 1 : 0; }
            set { m_setting.IsVisible = value == 0 ? false : true; }
        }

        #endregion

        #region Sequence

        public int Sequence
        {
            get { return m_setting.Sequence; }
            set { m_setting.Sequence = value; }
        }

        #endregion
    }
}
