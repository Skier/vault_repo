using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.ServiceLayer;
using MobileTech.Data;
using MobileTech.Windows.UI.SelectItem;
using MobileTech.Windows.UI.Odometer;
using MobileTech.Windows.UI.Controls;



namespace MobileTech.Windows.UI.CustomerOperations.Invoice
{

    public delegate void QuantityChangedHandler(CustomerTransactionDetail item, int oldQuantity);

	public class InvoiceModel:CustomerOperationsModel, 
        ISelectItemListener,
        ITableModel
    {

        #region Fields

        List<CustomerTransactionDetail> m_data;

        #endregion

        #region Events

        public event QuantityChangedHandler QuantityChanged;

        #endregion

        #region Init

        public override void Init(CustomerOperationsCommonData commonData)
        {
            base.Init(commonData);

            if (IsTransactionExists(CustomerTransactionTypeEnum.Sales))
            {
                m_data = CustomerTransactionDetail.FindBy(
                    Transactions[CustomerTransactionTypeEnum.Sales]);
            }
            else
                m_data = new List<CustomerTransactionDetail>();
        }

        #endregion

        #region Save

        public void Save()
        {
            if (!IsTransactionExists(CustomerTransactionTypeEnum.Sales))
                AddTransaction(CustomerTransactionTypeEnum.Sales);

            CustomerTransaction transaction = Transactions[CustomerTransactionTypeEnum.Sales];

            CustomerTransactionDetail.Clear(transaction);

            List<CustomerTransactionDetail> customerTransactionDetailList =
                new List<CustomerTransactionDetail>();

            foreach (CustomerTransactionDetail customerTransactionDetail in
                m_data)
            {
                if (customerTransactionDetail.Quantity < 1)
                    continue;

                customerTransactionDetail.CustomerTransaction = transaction;

                customerTransactionDetailList.Add(customerTransactionDetail);
            }


            if (customerTransactionDetailList.Count > 0)
                CustomerTransactionDetail.Insert(customerTransactionDetailList);
            else
                RemoveTransaction(CustomerTransactionTypeEnum.Sales);

        }

        #endregion

        #region ISelectItemListener

        #region GetQuantity

        public int GetQuantity(Item item)
        {
            foreach (CustomerTransactionDetail customerTransactionDetail in m_data)
                if (customerTransactionDetail.ItemNumber.Equals(item.ItemNumber))
                    return customerTransactionDetail.Quantity;

            return 0;
        }

        #endregion

        #region SetQuantity

        public void SetQuantity(Item item, int quantity)
        {
            for(int i = 0; i < m_data.Count;i++)
            {
                if (m_data[i].ItemNumber.Equals(
                    item.ItemNumber))
                {
                    SetValueAt(quantity, 
                        i, 1);

                    return;
                }
            }

            CustomerTransactionDetail newItem = new CustomerTransactionDetail(item);

            try
            {
                newItem.InventoryQuantity = RouteInventory.FindBy(item, StorageTypeEnum.Store).TruckQty;
            }
            catch (DataNotFoundException) { }

            m_data.Insert(0, newItem);

            SetValueAt(quantity, 0, 1);

            if (Change != null)
                Change.Invoke();
        }

        #endregion

        #region GetItemType

        public ItemTypeEnum GetItemType()
        {
            return ItemTypeEnum.Product;
        }

        #endregion

        #endregion

        #region ITableModel

        public int GetRowCount()
        {
            return m_data.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return Resources.Name;

            return Resources.Qnt;
        }

        public Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 0)
                return typeof(String);

            return typeof(int);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return columnIndex == 1;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_data[rowIndex].Item.Name;

            return m_data[rowIndex].Quantity;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            switch (columnIndex)
            {
                case 1:
                    int oldValue = m_data[rowIndex].Quantity;

                    m_data[rowIndex].Quantity = (int)aValue;

                    if (QuantityChanged != null)
                    {
                        QuantityChanged.Invoke(m_data[rowIndex], oldValue);
                    }

                    break;
            }
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_data[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion
    }
}
