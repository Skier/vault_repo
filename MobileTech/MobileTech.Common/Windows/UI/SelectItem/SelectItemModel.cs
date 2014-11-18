using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Data;
using MobileTech.Domain;
using MobileTech.Windows.UI.ItemSearch;

namespace MobileTech.Windows.UI.SelectItem
{
    public class SelectItemModel:IItemSearchListener
    {
        #region Fields

        #region TruckStock

        int m_truckStock;
        public int TruckStock
        {
            get { return m_truckStock; }
            set { m_truckStock = value; }
        }

        #endregion

        #region Listener

        ISelectItemListener m_listener;
        public ISelectItemListener Listener
        {
            get { return m_listener; }
            set { m_listener = value; }
        }

        #endregion

        #region SelectedItem

        Item m_selectedItem;
        public Item SelectedItem
        {
            get { return m_selectedItem; }
            set { m_selectedItem = value; }
        }

        #endregion

        #region Quantity

        int m_quantity;
        public int Quantity
        {
            get { return m_quantity; }
            set { m_quantity = value; }
        }

        #endregion

        #endregion

        #region Search

        public void Search(String itemNumber)
        {
            Item item = Item.FindBy(itemNumber);

            if (item.Type != m_listener.GetItemType())
                throw new MobileTechInvalidItemTypeException(
                    m_listener.GetItemType(),
                    item.Type);

            m_selectedItem = item;

            m_quantity = m_listener.GetQuantity(m_selectedItem);
        }

        #endregion

        #region Enter

        public void Enter()
        {
            m_listener.SetQuantity(m_selectedItem, m_quantity);

            m_quantity = 0;
            m_selectedItem = null;
        }

        #endregion

        #region IItemSearchListener

        public void SetItem(Item item)
        {

            if (item.Type != m_listener.GetItemType())
                throw new MobileTechInvalidItemTypeException(
                    m_listener.GetItemType(),
                    item.Type);

            m_selectedItem = item;

            m_quantity = m_listener.GetQuantity(item);
        }

        public ItemTypeEnum GetItemType()
        {
            return m_listener.GetItemType();
        }

        #endregion
    }
}