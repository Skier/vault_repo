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
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ChangeEquipmentStatus
{
    public class ChangeEquipmentStatusModel : IModel
    {
        #region InitialEquipment

        public List<EquipmentWrapper> m_initialEquipment;
        public List<EquipmentWrapper> InitialEquipment
        {
            get { return m_initialEquipment; }
            set { m_initialEquipment = value; }
        }

        #endregion        

        #region ModifiedEquipment

        public BindingList<EquipmentWrapper> m_modifiedEquipment;
        public BindingList<EquipmentWrapper> ModifiedEquipment
        {
            get { return m_modifiedEquipment; }
            set { m_modifiedEquipment = value; }
        }

        #endregion        

        #region EquipmentStatuses

        private List<EquipmentStatus> m_equipmentStatuses;
        public List<EquipmentStatus> EquipmentStatuses
        {
            get { return m_equipmentStatuses; }
        }

        #endregion        

        #region Init

        public void Init()
        {
            m_equipmentStatuses = EquipmentStatus.Find();
            ResetEquipment();
        }

        #endregion

        #region ResetEquipment

        public void ResetEquipment()
        {
            m_modifiedEquipment = new BindingList<EquipmentWrapper>();

            foreach (EquipmentWrapper equipment in m_initialEquipment)
                m_modifiedEquipment.Add((EquipmentWrapper)equipment.Clone());            
        }

        #endregion

        #region GetEquipmentsStatus

        //returns null if equipments has different statuses
        public int? GetEquipmentsStatus()
        {
            if (m_modifiedEquipment.Count == 0)
                return null;

            int firstStatus = m_modifiedEquipment[0].StatusId;

            foreach (EquipmentWrapper equipment in m_modifiedEquipment)
            {
                if (equipment.StatusId != firstStatus)
                    return null;
            }

            return firstStatus;
        }

        #endregion

        #region SetAllStatus

        public void SetAllStatus(int statusId)
        {
            foreach (EquipmentWrapper equipment in m_modifiedEquipment)
                equipment.StatusId = statusId;
        }

        #endregion

    }
}
