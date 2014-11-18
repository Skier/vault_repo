using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class Item:ICounterField
    {
        public Item() { }

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_itemId;
            }
            set
            {
                m_itemId = value;
            }
        }

        public string CounterName
        {
            get { return "Item"; }
        }

        #endregion

        #region FindByQuickBooksId

        private const String SqlSelectByQuickBooksId = "Select "


                                                       + " ItemId, "

                                                       + " QuickBooksListId, "

                                                       + " EditSequence, "

                                                       + " Name, "

                                                       + " SalesPrice "


                                                       + " From Item Where QuickBooksListId = @QuickBooksListId";

        public static Item FindByQuickBooksId(int quickBooksId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByQuickBooksId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksListId", quickBooksId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                { 
                    if(dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("Item not found, search by quickBooksId");
                }
            }
        }

        #endregion

        #region Equals & GetHashCode

        public override int GetHashCode()
        {
            return m_itemId;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Item item = obj as Item;
            if (item == null) return false;
            if (m_itemId != item.m_itemId) return false;
            return true;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}
