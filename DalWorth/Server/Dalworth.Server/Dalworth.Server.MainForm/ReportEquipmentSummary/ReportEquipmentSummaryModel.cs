using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportEquipmentSummary
{
    public class ReportEquipmentSummaryModel : IModel
    {
        #region EquipmentSummary

        private Dictionary<Location, List<int>> m_equipmentSummary;
        public Dictionary<Location, List<int>> EquipmentSummary
        {
            get { return m_equipmentSummary; }
        }

        #endregion

        #region EquipmentTypes

        private List<EquipmentType> m_equipmentTypes;
        public List<EquipmentType> EquipmentTypes
        {
            get { return m_equipmentTypes; }
        }

        #endregion

        #region Locations

        private BindingList<Location> m_locations;
        public BindingList<Location> Locations
        {
            get { return m_locations; }
        }

        #endregion

        #region Areas

        private List<Area> m_areas;
        public List<Area> Areas
        {
            get { return m_areas; }
        }

        #endregion

        #region PrintQuantities

        private List<EquipmentQuantityPrint> m_printQuantities;
        public List<EquipmentQuantityPrint> PrintQuantities
        {
            get { return m_printQuantities; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_areas = Area.Find();
            m_equipmentTypes = EquipmentType.Find();            
        }

        #endregion

        #region RefreshReportData

        public void RefreshReportData()
        {
            Dictionary<EquipmentType, int> equipmentTypesSequence = new Dictionary<EquipmentType, int>();

            for (int i = 0; i < m_equipmentTypes.Count; i++)
                equipmentTypesSequence.Add(m_equipmentTypes[i], i);

            List<EquipmentTransactionDetail> details = EquipmentTransactionDetail.FindLatestOnVans();
            details.AddRange(EquipmentTransactionDetail.FindLatestOnCustomers());
            m_equipmentSummary = new Dictionary<Location, List<int>>();

            for (int i = 0; i < details.Count; i += m_equipmentTypes.Count)
            {
                if (details[i].Quantity == 0 && details[i + 1].Quantity == 0 
                    && details[i + 2].Quantity == 0)
                {
                    continue;
                }

                List<int> quantities = new List<int>();
                quantities.Add(details[i].Quantity);
                quantities.Add(details[i + 1].Quantity);
                quantities.Add(details[i + 2].Quantity);                

                Location location = new Location(details[i].Van, details[i].Customer);
                m_equipmentSummary.Add(location, quantities);
            }

            m_locations = new BindingList<Location>();
            foreach (Location key in EquipmentSummary.Keys)
                m_locations.Add(key);
            
        }

        #endregion

        #region PreparePrintData

        public void PreparePrintData()
        {
            m_printQuantities = new List<EquipmentQuantityPrint>();

            foreach (KeyValuePair<Location, List<int>> pair in m_equipmentSummary)
                foreach (int quantity in pair.Value)
                    m_printQuantities.Add(new EquipmentQuantityPrint(pair.Key.LocationText, quantity));
        }

        #endregion
    }

    public class Location
    {
        private Van m_van;
        private Customer m_customer;

        #region Constructor

        public Location(Van van, Customer customer)
        {
            m_van = van;
            m_customer = customer;
        }

        #endregion

        #region LocationType

        public EquipmentLocationTypeEnum LocationType
        {
            get
            {
                if (m_van != null)
                    return EquipmentLocationTypeEnum.Van;
                if (m_customer != null)
                    return EquipmentLocationTypeEnum.Customer;

                return EquipmentLocationTypeEnum.Lost;
            }
        }

        #endregion

        #region LocationTypeText

        public string LocationTypeText
        {
            get
            {
                if (LocationType == EquipmentLocationTypeEnum.InventoryRoom)
                    return "Inventory Rooms";
                if (LocationType == EquipmentLocationTypeEnum.Van)
                    return "Vans";
                if (LocationType == EquipmentLocationTypeEnum.Customer)
                    return "Customers";

                return "Lost";
            }
        }

        #endregion

        #region LocationTypeTextNoS

        public string LocationTypeTextNoS
        {
            get
            {
                if (LocationType == EquipmentLocationTypeEnum.InventoryRoom)
                    return "Inventory Room";
                if (LocationType == EquipmentLocationTypeEnum.Van)
                    return "Van";
                if (LocationType == EquipmentLocationTypeEnum.Customer)
                    return "Customer";

                return "Lost";
            }
        }

        #endregion

        #region LocationId

        public int LocationId
        {
            get
            {
                if (m_van != null)
                    return m_van.ID;
                if (m_customer != null)
                    return m_customer.ID;

                return -1;
            }
        }

        #endregion

        #region LocationText

        public string LocationText
        {
            get
            {
                if (LocationType == EquipmentLocationTypeEnum.Van)
                    return m_van.LicensePlateNumber;
                if (LocationType == EquipmentLocationTypeEnum.Customer)
                    return m_customer.DisplayName;

                return string.Empty;
            }
        }

        #endregion

        #region Van

        public Van Van
        {
            get { return m_van; }
        }

        #endregion

        #region Customer

        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region Equals & GetHashCode

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Location location = obj as Location;
            if (location == null) return false;

            if (location.LocationType == LocationType
                && location.LocationId == LocationId)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return LocationType.GetHashCode() + 29 * LocationId.GetHashCode();
        }

        #endregion
    }

    public class EquipmentQuantityPrint
    {
        private string m_location;
        private int m_quantity;

        #region Constructor

        public EquipmentQuantityPrint(string location, int quantity)
        {
            m_location = location;
            m_quantity = quantity;
        }

        #endregion

        #region Location

        public string Location
        {
            get { return m_location; }
            set { m_location = value; }
        }

        #endregion

        #region Quantity

        public int Quantity
        {
            get { return m_quantity; }
            set { m_quantity = value; }
        }

        #endregion
    }

    public enum EquipmentLocationTypeEnum
    {
        InventoryRoom = 1,
        Van = 2,
        Customer = 3,
        Lost = 4
    }
}
