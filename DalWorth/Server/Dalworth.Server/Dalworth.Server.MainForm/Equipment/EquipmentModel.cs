using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Equipment
{
    public class EquipmentModel : IModel
    {
        #region Equipments

        private List<EquipmentWrapper> m_equipments;
        public List<EquipmentWrapper> Equipments
        {
            get { return m_equipments; }
        }

        #endregion

        #region EquipmentTypes

        private List<EquipmentType> m_equipmentTypes;
        public List<EquipmentType> EquipmentTypes
        {
            get { return m_equipmentTypes; }
        }

        #endregion

        #region Areas

        private List<Area> m_areas;
        public List<Area> Areas
        {
            get { return m_areas; }
        }

        #endregion

        #region EquipmentIssues

        //key - EquipmentTransaction
        private Dictionary<int, BindingList<EquipmentIssueWrapper>> m_equipmentIssueItems;
        private BindingList<EquipmentIssueWrapper> m_equipmentIssueTransactions;

        public Dictionary<int, BindingList<EquipmentIssueWrapper>> EquipmentIssueItems
        {
            get { return m_equipmentIssueItems; }
        }

        public BindingList<EquipmentIssueWrapper> EquipmentIssueTransactions
        {
            get { return m_equipmentIssueTransactions; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_equipments = Domain.Equipment.FindEquipmentWrappers();
            m_equipmentTypes = EquipmentType.Find();
            m_areas = Area.Find();
            
        }

        #endregion

        #region AddEquipment

        public void AddEquipment(Domain.Equipment equipment)
        {
            Domain.Equipment.Insert(equipment);            

            Domain.Equipment.UpdateTransactional(Configuration.CurrentDispatchId, null,
                equipment, string.Empty, new DateTime(1900, 01, 01));
        }

        #endregion

        #region UpdateEquipment

        public void UpdateEquipment(List<EquipmentWrapper> equipmentWrappers, string notes)
        {
            List<Domain.Equipment> equipments = new List<Domain.Equipment>();
            foreach (EquipmentWrapper equipmentWrapper in equipmentWrappers)
                equipments.Add(equipmentWrapper.Equipment);

            Domain.Equipment.UpdateTransactional(Configuration.CurrentDispatchId, null,
                equipments, notes, null);
        }

        #endregion

        #region InitEquipmentIssues

        public void InitEquipmentIssues(DateTime? startDate,
                                         EquipmentIssueStatusEnum issueStatus)
        {
            List<EquipmentIssueWrapper> equipmentIssueWrappers
                = Domain.Equipment.FindEquipmentIssueWrappers(startDate, issueStatus);

            m_equipmentIssueTransactions = new BindingList<EquipmentIssueWrapper>();
            m_equipmentIssueItems = new Dictionary<int, BindingList<EquipmentIssueWrapper>>();

            foreach (EquipmentIssueWrapper wrapper in equipmentIssueWrappers)
            {
                if (!m_equipmentIssueItems.ContainsKey(wrapper.EquipmentTransaction.ID))
                {
                    m_equipmentIssueTransactions.Add(wrapper);
                    m_equipmentIssueItems.Add(wrapper.EquipmentTransaction.ID, new BindingList<EquipmentIssueWrapper>());
                }

                if (!wrapper.IsResolved)
                    m_equipmentIssueTransactions[m_equipmentIssueTransactions.Count - 1].HasUnresolvedIssues = true;

                m_equipmentIssueItems[wrapper.EquipmentTransaction.ID].Add(wrapper);
            }

        }

        #endregion
    }
}
