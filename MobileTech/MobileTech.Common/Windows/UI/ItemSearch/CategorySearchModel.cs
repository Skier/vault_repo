using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;

namespace MobileTech.Windows.UI.ItemSearch
{
    internal class CategorySearchModel:ListTableModel<ItemCategory>
    {
        #region Fields

        static ItemFieldName m_field = ItemFieldName.Name;

        #region SelectedCategory

        ItemCategory m_selectedCategory;

        public ItemCategory SelectedCategory
        {
            get { return m_selectedCategory; }
            set { m_selectedCategory = value; }
        }

        #endregion

        #region Mode

        public ItemFieldName Mode
        {
            get { return m_field; }
            set
            {
                if (m_field != value)
                {
                    m_field = value;

                    Load();
                }
            }
        }

        #endregion

        #region Listener

        ICategorySearchListener m_listener;

        public ICategorySearchListener Listener
        {
            get { return m_listener; }
            set { m_listener = value; }
        }

        #endregion

        #endregion

        #region Search

        public int Search(String query)
        {
            int searchIndex = -1;

            if (m_list.Count == 0)
                Load();

            if (!(String.Empty.Equals(query)))
            {

                if (m_field == ItemFieldName.Name)
                {
                    for (int i = 0; i < m_list.Count; i++)
                    {
                        if (m_list[i].Name.StartsWith(query,
                            StringComparison.CurrentCultureIgnoreCase))
                        {
                            searchIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < m_list.Count; i++)
                    {
                        if (m_list[i].ItemCategoryId.ToString().StartsWith(query))
                        {
                            searchIndex = i;
                            break;
                        }
                    }
                }
            }

            FireChangeEvent();

            return searchIndex;
        }

        #endregion

        #region Load

        private void Load()
        {
            m_list = ItemCategory.Find(m_field,
                m_listener.GetItemType());

            FireChangeEvent();
        }

        #endregion

        #region ITable

        public override int GetColumnCount()
        {
            return 2;
        }

        public override string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Number";

            return "Name";
        }

        public override Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 0)
                return int.MaxValue.GetType();

            return String.Empty.GetType();
        }

        public override object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_list[rowIndex].ItemCategoryId;

            return m_list[rowIndex].Name;
        }

        #endregion
    }
}
