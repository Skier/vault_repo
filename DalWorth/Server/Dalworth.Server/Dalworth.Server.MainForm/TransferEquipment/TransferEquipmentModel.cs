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
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.TransferEquipment
{
    public class TransferEquipmentModel : IModel
    {
        #region InitialEquipment

        public List<EquipmentWrapper> m_initialEquipment;
        public List<EquipmentWrapper> InitialEquipment
        {
            get { return m_initialEquipment; }
            set { m_initialEquipment = value; }
        }

        #endregion        

        #region EquipmentNumbers

        private BindingList<EquipmentNumber> m_equipmentNumbers;
        public BindingList<EquipmentNumber> EquipmentNumbers
        {
            get { return m_equipmentNumbers; }
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

        private BindingList<InventoryRoom> m_inventoryRooms;
        public BindingList<InventoryRoom> InventoryRooms
        {
            get { return m_inventoryRooms; }
        }

        #endregion

        #region Vans

        private BindingList<Van> m_vans;
        public BindingList<Van> Vans
        {
            get { return m_vans; }
        }

        #endregion

        #region SelectedCustomer

        private Customer m_selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return m_selectedCustomer; }
            set { m_selectedCustomer = value; }
        }

        #endregion

        #region Init

        public void Init()
        {                
            List<EquipmentType> equipmentTypes = EquipmentType.Find();

            m_equipmentNumbers = new BindingList<EquipmentNumber>();
            foreach (EquipmentType equipmentType in equipmentTypes)
                m_equipmentNumbers.Add(new EquipmentNumber(equipmentType));


            foreach (EquipmentWrapper equipment in m_initialEquipment)
            {
                foreach (EquipmentNumber number in m_equipmentNumbers)
                {
                    if (number.EquipmentType.Equals(equipment.Equipment.EquipmentType)
                        && !number.IsFilled)
                    {
                        number.PutIntoFirstEmptyCell(equipment.Equipment.SerialNumber);

                        if (number.IsFilled)
                        {
                            EquipmentNumber newRow = new EquipmentNumber(number.EquipmentType);                            
                            m_equipmentNumbers.Add(newRow);
                        }                        

                        break;
                    }
                }
            }                

            m_areas = Area.Find();
        }

        #endregion

        #region UpdateData

        public void UpdateData(EquipmentLocationTypeEnum locationType)
        {                     
            switch (locationType)
            {
                case EquipmentLocationTypeEnum.InventoryRoom:
                    m_inventoryRooms = new BindingList<InventoryRoom>(InventoryRoom.Find());
                    break;
                case EquipmentLocationTypeEnum.Van:
                    m_vans = new BindingList<Van>(Van.Find());            
                    break;
                }                                            
        }

        #endregion

        #region GetCustomerAddresses

        public BindingList<Address> GetCustomerAddresses(Customer customer)
        {
            Customer customerReal = Customer.FindByPrimaryKey(customer.ID);

            BindingList<Address> addresses = new BindingList<Address>();
            if (customerReal.AddressId.HasValue)
            {
                Address address =
                    Address.FindByPrimaryKey(customerReal.AddressId.Value);
                
                addresses.Add(address);
            }
                

            List<Address> additional = Address.FindAdditionalBy(customerReal);
            foreach (Address address in additional)
            {
                addresses.Add(address);
            }
                
            return addresses;
        }

        #endregion

        #region IsAllEquipmentInInventoryRoom

        public bool IsAllEquipmentInInventoryRoom()
        {
            foreach (EquipmentWrapper equipment in m_initialEquipment)
            {
                if (equipment.Equipment.AddressId.HasValue
                    || equipment.Equipment.VanId.HasValue)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region GetEquipmentWrappers

        public List<EquipmentWrapper> GetEquipmentWrappers()
        {
            List<string> serialNumbers = Domain.Equipment.GetCollectionSerialNumbers(m_equipmentNumbers);
            List<Domain.Equipment> equipments = Domain.Equipment.FindBy(serialNumbers);

            List<EquipmentWrapper> result = new List<EquipmentWrapper>();
            foreach (Domain.Equipment equipment in equipments)
                result.Add(new EquipmentWrapper(equipment));

            return result;
        }

        #endregion        

        #region EquipmentNumber Validation

        public bool IsEquipmentNumberExist(string number)
        {
            try
            {
                Domain.Equipment.FindBy(number);
                return true;
            }
            catch (DataNotFoundException)
            {
                return false;
            }            
        }

        public bool IsEquipmentNumberExist(string number, EquipmentType equipmentType)
        {
            try
            {
                Domain.Equipment.FindBy(equipmentType, number);
                return true;
            }
            catch (DataNotFoundException)
            {
                return false;
            }            
        }

        private bool IsEquipmentNumberExistInList(BindingList<EquipmentNumber> collection, string number)
        {
            foreach (EquipmentNumber equipmentNumber in collection)
            {
                if (equipmentNumber.SerialNumber1 == number
                    || equipmentNumber.SerialNumber2 == number
                    || equipmentNumber.SerialNumber3 == number
                    || equipmentNumber.SerialNumber4 == number)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsLoadAndKeepNumberDuplicated(string number)
        {
            return IsEquipmentNumberExistInList(m_equipmentNumbers, number);
        }

        #endregion
    }
}
