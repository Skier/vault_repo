using System;
using System.Collections.Generic;
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

namespace Dalworth.Server.MainForm.AddEquipment
{
    public class AddEquipmentModel : IModel
    {
        #region AddedEquipment

        public Domain.Equipment m_addedEquipment;
        public Domain.Equipment AddedEquipment
        {
            get { return m_addedEquipment; }
            set { m_addedEquipment = value; }
        }

        #endregion

        #region EquipmentTypes

        private List<EquipmentType> m_equipmentTypes;
        public List<EquipmentType> EquipmentTypes
        {
            get { return m_equipmentTypes; }
        }

        #endregion

        #region EquipmentStatuses

        private List<EquipmentStatus> m_equipmentStatuses;
        public List<EquipmentStatus> EquipmentStatuses
        {
            get { return m_equipmentStatuses; }
        }

        #endregion

        #region Areas

        private List<Area> m_areas;
        public List<Area> Areas
        {
            get { return m_areas; }
        }

        #endregion

        #region InventoryRooms

        private List<InventoryRoom> m_inventoryRooms;
        public List<InventoryRoom> InventoryRooms
        {
            get { return m_inventoryRooms; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_equipmentTypes = EquipmentType.Find();
            m_equipmentStatuses = EquipmentStatus.Find();
            m_areas = Area.Find();
            UpdateInventotyRooms(null);
        }

        #endregion

        #region UpdateInventotyRooms

        public void UpdateInventotyRooms(byte? areaId)
        {
            if (areaId.HasValue)
                m_inventoryRooms = InventoryRoom.FindBy(new Area(areaId.Value));
            else
                m_inventoryRooms = InventoryRoom.Find();
        }

        #endregion
    }
}
