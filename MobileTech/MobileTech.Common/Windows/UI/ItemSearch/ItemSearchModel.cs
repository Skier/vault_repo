using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Data;
using MobileTech.Domain;
using System.Diagnostics;

namespace MobileTech.Windows.UI.ItemSearch
{
    internal delegate void CategoryChangedHandler();

    internal class ItemSearchModel:ITableModel,ICategorySearchListener
    {
        #region Events
        
        public event CategoryChangedHandler CategoryChanged;
        
        #endregion

        #region Fields

        int m_startIndex;

        int m_endIndex;

        int m_rowCount = -1;

        const int BufferSize = 20;

        List<Item> m_itemBuffer = new List<Item>();

        #region Listener

        IItemSearchListener m_listener;

        public IItemSearchListener Listener
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

        #region Mode

        static ItemFieldName m_field = ItemFieldName.Name;

        public ItemFieldName Mode
        {
            get { return m_field; }
            set 
            {
                if (m_field != value)
                {
                    m_field = value;

                    UpdateBuffer(0);
                }
            }
        }

        #endregion

        #endregion

        #region Search

        public int Search(String query)
        {
#if DEBUG_LEVEL_1
            Debug.WriteLine("ItemSearchModel::Search");
#endif
            m_selectedItem = null;

            int searchIndex = -1;

            if (m_rowCount == -1)
                m_rowCount = Item.Count(Category);

            if (!String.Empty.Equals(query))
            {
                searchIndex = Item.SearchIndex(Category, m_field, query) - 1;
            }

            if (Change != null)
                Change.Invoke();

            return searchIndex;
        }

        #endregion

        #region UpdateBuffer

        private void UpdateBuffer(int targetIndex)
        {
#if DEBUG_LEVEL_1
            Debug.WriteLine("ItemSearchModel::UpdateBuffer");
#endif
            m_startIndex = targetIndex - BufferSize / 2 - 1;
            m_endIndex = targetIndex + BufferSize / 2 - 2;

            if (m_startIndex < 1)
            {
                m_endIndex += m_startIndex * -1;
                m_startIndex = 0;
            }

            if (m_endIndex > m_rowCount)
                m_endIndex = m_rowCount - 1;

            m_itemBuffer.Clear();

            Item.Search(Category,
                m_field,
                BufferSize,
                m_startIndex,
                m_itemBuffer);

            if (Change != null)
            {
                Change.Invoke();
            }
        }
        #endregion

        #region ICategorySearchListener

        static Dictionary<ItemTypeEnum,ItemCategory> s_category;

        public ItemCategory Category
        {
            get
            {
                if (s_category == null)
                    s_category = new Dictionary<ItemTypeEnum, ItemCategory>();

                if(s_category.ContainsKey(GetItemType()))
                    return s_category[GetItemType()];

                return null;
            }
            set
            {
                int oldCategoryId = 0;

                if(s_category == null)
                    s_category = new Dictionary<ItemTypeEnum, ItemCategory>();

                if (s_category.ContainsKey(GetItemType()))
                    oldCategoryId = s_category[GetItemType()].ItemCategoryId;
                else
                    s_category.Add(GetItemType(), new ItemCategory());


                ItemCategory itemCategory = s_category[GetItemType()];

                itemCategory.Description = (String)value.Description.Clone();
                itemCategory.ItemCategoryId = value.ItemCategoryId;
                itemCategory.ItemTypeId = value.ItemTypeId;
                itemCategory.Name = (String)value.Name.Clone();

                if ((oldCategoryId != itemCategory.ItemCategoryId) 
                    || m_rowCount == -1 &&
                    CategoryChanged != null)
                {
                    m_rowCount = Item.Count(itemCategory);

                    CategoryChanged.Invoke();
                }

            }
        }


        public ItemTypeEnum GetItemType()
        {
            return m_listener.GetItemType();
        }

        #endregion

        #region ITableModel

        public int GetRowCount()
        {
            return m_rowCount;
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {

        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            if (rowIndex < m_startIndex || rowIndex > m_endIndex
                || m_itemBuffer.Count == 0)
            {
                UpdateBuffer(rowIndex);
            }

            return m_itemBuffer[rowIndex - m_startIndex];
        }

        public event TableModelChangeHandler Change;

        public Type GetColumnClass(int columnIndex)
        {
            switch (columnIndex)
            {
                case 0:
                    return typeof(int);
                case 1:
                    return typeof(String);
            }

            throw new Exception("Invalid column");
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            switch (columnIndex)
            {
                case 0:
                    return Resources.ProductID;
                case 1:
                    return Resources.ProductName;
            }

            throw new Exception("Invalid column");
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {

            Item item = (Item)GetObjectAt(rowIndex, columnIndex);

            switch (columnIndex)
            {
                case 0:
                    return item.ItemNumber;
                case 1:
                    return item.Name;
            }

            throw new Exception("Invalid column");
        }


        #endregion
    }
}
