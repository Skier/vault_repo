using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.SelectItem;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.Inventory.Unload
{

    public enum UnloadEditMode
    {
        Unload,
        Truck,
        Damage
    }

    public abstract class InventoryUnloadItemModel : InventoryUnloadModel, ITableModel, ISelectItemListener
    {
        #region Events

        public event SelectIndexEvent ItemAffected;

        #endregion

        #region Fields

        protected  Dictionary<StorageTypeEnum, List<RouteInventory>> m_data;

        protected bool m_dualInventory;

        public bool DualInventory
        {
            get { return m_dualInventory; }
            set { m_dualInventory = value; }
        }

        protected UnloadEditMode m_mode;

        public UnloadEditMode Mode
        {
            get { return m_mode; }
            set
            {
#if DEBUG
                if (!IsModeAllow(value))
                    throw new MobileTechException("Invalid mode");
#endif

                m_mode = value;

                if (m_data != null && Change != null)
                    Change.Invoke();
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

        #region Init

        public override void Init()
        {
            base.Init();


            m_mode = RouteOption.ValueIs(RouteOptionEnum.AutoCalcLoadIn, 1)
                ? UnloadEditMode.Truck : UnloadEditMode.Unload;

            m_storageType = StorageTypeEnum.Store;

            m_data = new Dictionary<StorageTypeEnum, List<RouteInventory>>();
            m_dualInventory = RouteOption.ValueIs(RouteOptionEnum.EnableDualInventory, 1);

            m_data[StorageTypeEnum.Store] = RouteInventory.FindBy(StorageTypeEnum.Store,
                GetItemType());

            if (DualInventory)
                m_data[StorageTypeEnum.Bin] = RouteInventory.FindBy(StorageTypeEnum.Bin, 
                    GetItemType());


            if (!IsTransactionExists)
            {
                foreach (RouteInventory routeInventory in m_data[StorageTypeEnum.Store])
                {
                    Init(routeInventory);
                }

                if (DualInventory)
                {
                    foreach (RouteInventory routeInventory in m_data[StorageTypeEnum.Bin])
                    {
                        Init(routeInventory);
                    }
                }
            }

            if (Change != null)
                Change.Invoke();
        }

        protected abstract void Init(RouteInventory routeInventory);

        #endregion

        #region IsModeAllow
        public abstract bool IsModeAllow(UnloadEditMode mode);
        #endregion

        #region Save

        internal void Save()
        {
            if (!IsTransactionExists)
                AddTransaction();

            foreach (RouteInventory routeInventory in m_data[StorageTypeEnum.Store])
                RouteInventory.UpdateOrCreate(routeInventory);

            if (DualInventory)
            {
                foreach (RouteInventory routeInventory in m_data[StorageTypeEnum.Bin])
                    RouteInventory.UpdateOrCreate(routeInventory);
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
                if(GetItemType() == ItemTypeEnum.Product)
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

        public abstract object GetValueAt(int rowIndex, int columnIndex);

        #endregion

        #region SetValueAt

        public abstract void SetValueAt(object aValue, int rowIndex, int columnIndex);

        #endregion

        #region GetObjectAt

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_data[m_storageType][rowIndex];
        }

        #endregion

        public event TableModelChangeHandler Change;

        #endregion

        #region ISelectItemListener

        #region GetQuantity

        public int GetQuantity(Item item)
        {
            foreach (RouteInventory routeInventory in m_data[m_storageType])
            {
                if (routeInventory.ItemNumber.Equals(item.ItemNumber))
                {
                    switch (m_mode)
                    {
                        case UnloadEditMode.Damage:
                            return routeInventory.RouteDmgQty;
                        case UnloadEditMode.Truck:
                            return routeInventory.EndQty;
                        case UnloadEditMode.Unload:
                            return routeInventory.UnloadQty;
                    }
                }
            }

            return 0;
        }

        #endregion

        #region SetQuantity

        public void SetQuantity(Item item, int quantity)
        {
            RouteInventory affectedRouteInventory = null;
            int itemAffectedIndex = 0;

            foreach (RouteInventory routeInventory in m_data[m_storageType])
            {
                if (routeInventory.ItemNumber.Equals(item.ItemNumber))
                {
                    affectedRouteInventory = routeInventory;

                    break;
                }

                ++itemAffectedIndex;
            }


            if (affectedRouteInventory == null)
            {
                affectedRouteInventory = RouteInventory.Prepare(m_storageType);
                affectedRouteInventory.Item = item;

                itemAffectedIndex = 0;

                m_data[m_storageType].Insert(0, affectedRouteInventory);
            }


            SetNewItemQuantity(affectedRouteInventory,quantity);


            if (Change != null)
                Change.Invoke();

            if (ItemAffected != null)
                ItemAffected.Invoke(itemAffectedIndex);
        }

        protected abstract void SetNewItemQuantity(RouteInventory routeInventory, int quantity);

        #endregion

        #region GetItemType

        public abstract ItemTypeEnum GetItemType();

        #endregion

        #endregion
    }
}
