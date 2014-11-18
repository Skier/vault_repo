using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;

namespace Dalworth.Windows.StartDay
{
    public class StartDayModel
    {
        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region ItemDeliveryInfoList

        private List<ItemDeliveryInfo> m_itemDeliveryInfoList;
        public List<ItemDeliveryInfo> ItemDeliveryInfoList
        {
            get { return m_itemDeliveryInfoList; }
            set { m_itemDeliveryInfoList = value; }
        }

        #endregion

        #region CapturedEquipment

        private List<WorkTransactionEquipment> m_capturedEquipment;
        public List<WorkTransactionEquipment> CapturedEquipment
        {
            get { return m_capturedEquipment; }
            set { m_capturedEquipment = value; }
        }

        #endregion

        #region Van

        private Van m_van;
        public Van Van
        {
            get { return m_van; }
            set { m_van = value; }
        }

        #endregion

        #region IsStartDayDone

        public bool IsStartDayDone()
        {
            if (Work != null && Work.WorkStatus == WorkStatusEnum.StartDayDone)
                return true;
            return false;
        }

        #endregion

        #region IsStartDayCancelled

        private bool m_isStartDayCancelled;
        public bool IsStartDayCancelled
        {
            get { return m_isStartDayCancelled; }
            set { m_isStartDayCancelled = value; }
        }

        #endregion
    }
}
