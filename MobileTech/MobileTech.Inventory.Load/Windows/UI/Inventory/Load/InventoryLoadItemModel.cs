using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.Windows.UI.Controls;
using MobileTech.Windows.UI.SelectItem;

namespace MobileTech.Windows.UI.Inventory.Load
{

    public enum InventoryLoadEditMode
    {
        TruckStock = 0,
        CurrentLoad = 1,
        Adjustments = 2,
        TotalLoad = 3
    }

    public abstract class InventoryLoadItemModel : InventoryLoadModel, 
        ITableModel, 
        ISelectItemListener
    {
        #region Events

        public event SelectIndexEvent ItemAffected;

        #endregion

        #region Fields

        protected Dictionary<StorageTypeEnum, List<Data>> m_data;

        protected bool m_dualInventory;

        public bool DualInventory
        {
            get { return m_dualInventory; }
            set { m_dualInventory = value; }
        }


        InventoryLoadEditMode m_mode = InventoryLoadEditMode.CurrentLoad;

        public InventoryLoadEditMode Mode
        {
            get { return m_mode; }
            set
            {
                if (value != m_mode)
                {
                    m_mode = value;

                    if (Change != null)
                        Change.Invoke();
                }
            }

        }


        protected StorageTypeEnum m_storageType;

        public StorageTypeEnum StorageType
        {
            get { return m_storageType; }
            set
            {

#if DEBUG
                if (value == StorageTypeEnum.Bin && !DualInventory)
                    throw new MobileTechException("Invalid storage type");
#endif

                m_storageType = value;

                if (m_data != null && Change != null)
                    Change.Invoke();
            }
        }


        #endregion

        #region Save
        public void Save()
        {
            if (!IsTransactionExists)
                AddTransaction();

            List<RouteInventory> routeInventoryList = new List<RouteInventory>();

            foreach (Data data in m_data[StorageTypeEnum.Store])
            {
                data.ApplyAdjustment();

                routeInventoryList.Add(data.RouteInventory);
            }

            UpdateAdjustments(routeInventoryList);

            if (DualInventory)
            {
                routeInventoryList.Clear();

                foreach (Data data in m_data[StorageTypeEnum.Bin])
                {
                    data.ApplyAdjustment();

                    routeInventoryList.Add(data.RouteInventory);
                }

                UpdateAdjustments(routeInventoryList);
            }
        }

        protected abstract void UpdateAdjustments(List<RouteInventory> routeInventoryList);


        #endregion

        #region ITableModel

        #region GetRowCount

        public int GetRowCount()
        {
            return m_data[m_storageType].Count;
        }

        #endregion

        #region GetColumnCount

        public int GetColumnCount()
        {
            return 2;
        }

        #endregion

        #region GetColumnName

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
            {
                if (GetItemType() == ItemTypeEnum.Product)
                    return CommonResources.DcProduct;

                return CommonResources.DcEquipment;
            }

            return CommonResources.DcQty;
        }

        #endregion

        #region GetColumnClass

        public Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 0)
                return String.Empty.GetType();

            return int.MaxValue.GetType();
        }

        #endregion

        #region IsCellEditable

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return columnIndex == 1;
        }

        #endregion

        #region GetValueAt

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 1)
            {
                switch (Mode)
                {
                    case InventoryLoadEditMode.TruckStock:
                        return m_data[m_storageType][rowIndex].TruckQty;
                    case InventoryLoadEditMode.CurrentLoad:
                        return m_data[m_storageType][rowIndex].CurrentLoad;
                    case InventoryLoadEditMode.Adjustments:
                        return m_data[m_storageType][rowIndex].Adjustments;
                    case InventoryLoadEditMode.TotalLoad:
                        return m_data[m_storageType][rowIndex].Total;
                }
            }


            return m_data[m_storageType][rowIndex].RouteInventory.Item.Name;
        }
        #endregion

        #region SetValueAt

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            if (columnIndex != 1)
                return;


            switch (Mode)
            {
                case InventoryLoadEditMode.TruckStock:
                    break;
                case InventoryLoadEditMode.CurrentLoad:
                    break;
                case InventoryLoadEditMode.Adjustments:
                    m_data[m_storageType][rowIndex].Adjustments = (int)aValue;
                    break;
                case InventoryLoadEditMode.TotalLoad:
                    break;
            }
        }

        #endregion

        #region GetObjectAt

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_data[m_storageType][rowIndex];
        }

        #endregion

        public event TableModelChangeHandler Change;

        #endregion

        #region Init

        public override void Init()
        {
            base.Init();

            m_mode = InventoryLoadEditMode.Adjustments;

            m_storageType = StorageTypeEnum.Store;

            m_data = new Dictionary<StorageTypeEnum, List<Data>>();
            m_dualInventory = RouteOption.ValueIs(RouteOptionEnum.EnableDualInventory, 1);


            List<RouteInventory> inventory = RouteInventory.FindBy(StorageTypeEnum.Store,
                GetItemType());

            m_data[StorageTypeEnum.Store] = new List<Data>();

            foreach (RouteInventory i in inventory)
                m_data[StorageTypeEnum.Store].Add(CreateDataInstance(i));


            if (DualInventory)
            {
                m_data[StorageTypeEnum.Bin] = new List<Data>();

                inventory = RouteInventory.FindBy(StorageTypeEnum.Bin,
                    GetItemType());

                foreach (RouteInventory i in inventory)
                    m_data[StorageTypeEnum.Bin].Add(CreateDataInstance(i));
            }
        }

        #endregion

        #region CleanUp

        public override void CleanUp()
        {
            base.CleanUp();

            if (m_data != null)
            {
                m_data.Clear();

                m_data = null;
            }
        }

        #endregion

        #region Helper classes

        public abstract class Data
        {
            public RouteInventory RouteInventory;
            public int CurrentLoad;
            public int Adjustments;

            /*public Data(RouteInventory routeInventory)
            {
                RouteInventory = routeInventory;
                CurrentLoad = routeInventory.LoadQty;
            }

            public int Total
            {
                get
                {
                    return RouteInventory.LoadAdjustmentQty + Adjustments + CurrentLoad + RouteInventory.StartQty - RouteInventory.SaleQty;
                }
            }*/

            public abstract int Total
            {
                get;
            }

            public abstract int TruckQty
            {
                get;
            }

            public abstract void ApplyAdjustment();
        }

        protected abstract Data CreateDataInstance(RouteInventory routeInventory);

        #endregion

        #region ISelectItemListener Members

        public int GetQuantity(Item item)
        {
            foreach (Data i in m_data[m_storageType])
            {
                if (i.RouteInventory.ItemNumber == item.ItemNumber)
                {
                    return i.Adjustments;
                }
            }

            return 0;
        }

        public void SetQuantity(Item item, int quantity)
        {
            int itemAffectedIndex = 0;

            Data affectedItem = null;

            foreach (Data i in m_data[m_storageType])
            {
                if (i.RouteInventory.ItemNumber == item.ItemNumber)
                {
                    affectedItem = i;

                    break;
                }

                ++itemAffectedIndex;
            }

            if (affectedItem == null)
            {
                RouteInventory routeInventory = RouteInventory.Prepare(m_storageType);

                routeInventory.Item = item;

                affectedItem = CreateDataInstance(routeInventory);

                itemAffectedIndex = 0;

                m_data[m_storageType].Insert(0, affectedItem);
            }

            affectedItem.Adjustments = quantity;

            if (Change != null)
                Change.Invoke();

            if (ItemAffected != null)
                ItemAffected.Invoke(itemAffectedIndex);

        }

        public abstract ItemTypeEnum GetItemType();

        #endregion
    }
}
