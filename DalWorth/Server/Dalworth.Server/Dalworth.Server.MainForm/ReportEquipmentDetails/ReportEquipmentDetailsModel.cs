using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.ReportEquipmentSummary;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportEquipmentDetails
{
    public class ReportEquipmentDetailsModel : IModel
    {
        private List<Domain.Equipment> m_equipments;

        #region Location

        private Location m_location;
        public Location Location
        {
            get { return m_location; }
            set { m_location = value; }
        }

        #endregion

        #region EquipmentNumbers

        private BindingList<EquipmentNumber> m_equipmentNumbers;
        public BindingList<EquipmentNumber> EquipmentNumbers
        {
            get { return m_equipmentNumbers; }
        }

        #endregion

        #region EquipmentsPrint

        private List<EquipmentPrint> m_equipmentsPrint;
        public List<EquipmentPrint> EquipmentsPrint
        {
            get { return m_equipmentsPrint; }
        }

        #endregion

        #region LocationPrint

        private LocationPrint m_locationPrint;
        public LocationPrint LocationPrint
        {
            get { return m_locationPrint; }
        }

        #endregion

        #region Init

        public void Init()
        {
            if (m_location.LocationType == EquipmentLocationTypeEnum.Customer)
                m_equipments = Domain.Equipment.FindOnCustomerSite(m_location.Customer.ID, null, null);
            else if (m_location.LocationType == EquipmentLocationTypeEnum.InventoryRoom)
                m_equipments = Domain.Equipment.FindOnInventoryRoom(m_location.InventoryRoom);
            else if (m_location.LocationType == EquipmentLocationTypeEnum.Van)
                m_equipments = Domain.Equipment.FindOnVan(m_location.Van, null, null);
            else if (m_location.LocationType == EquipmentLocationTypeEnum.Lost)
                m_equipments = Domain.Equipment.FindLost();

            m_equipmentNumbers = new BindingList<EquipmentNumber>(
                Domain.Equipment.GetEquipmentNumbers(m_equipments));

            m_locationPrint = new LocationPrint(m_location);
        }

        #endregion

        #region PreparePrintData

        public void PreparePrintData()
        {
            m_equipmentsPrint = new List<EquipmentPrint>();
            foreach (Domain.Equipment equipment in m_equipments)
            {
                m_equipmentsPrint.Add(
                    new EquipmentPrint(equipment.EquipmentTypeText, equipment.SerialNumber));
            }
        }

        #endregion
    }

    public class EquipmentPrint
    {
        #region EquipmentPrint

        public EquipmentPrint(string equipmentTypeText, string serialNumber)
        {
            m_equipmentTypeText = equipmentTypeText;
            m_serialNumber = serialNumber;
        }

        #endregion

        #region EquipmentTypeText

        public string m_equipmentTypeText;        
        public string EquipmentTypeText
        {
            get { return m_equipmentTypeText; }
            set { m_equipmentTypeText = value; }
        }

        #endregion

        #region SerialNumber

        public string m_serialNumber;
        public string SerialNumber
        {
            get { return m_serialNumber; }
            set { m_serialNumber = value; }
        }

        #endregion
    }

    public class LocationPrint
    {
        private Location m_location;

        #region LocationPrint

        public LocationPrint(Location location)
        {
            m_location = location;
        }

        #endregion

        #region Line1

        public string Line1
        {
            get
            {
                if (m_location.LocationText == string.Empty)
                    return  m_location.LocationTypeText;
                return m_location.LocationTypeTextNoS + " - " + m_location.LocationText;                
            }
        }

        #endregion

        #region Line2

        public string Line2
        {
            get
            {
                if (m_location.LocationType == EquipmentLocationTypeEnum.Customer)
                    return  m_location.Address.AddressFirstLine;
                return string.Empty;
            }
        }

        #endregion

        #region Line3

        public string Line3
        {
            get
            {
                if (m_location.LocationType == EquipmentLocationTypeEnum.Customer)
                    return  m_location.Address.AddressSecondLine;
                return string.Empty;
            }
        }

        #endregion
    }
}
