using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MobileTech.Domain
{
    public partial class InventoryTransactionDetailXRef
    {
        public InventoryTransactionDetailXRef()
        {

        }

        public InventoryTransactionDetailXRef(InventoryTransactionTypeEnum
            inventoryTransactionTypeEnum,
            InventoryTransactionDetailTypeEnum
            inventoryTransactionDetailTypeEnum)
        {
            m_inventoryTransactionDetailTypeId = (int)inventoryTransactionDetailTypeEnum;
            m_inventoryTransactionTypeId = (int)inventoryTransactionTypeEnum;
        }

        [XmlIgnore]
        public InventoryTransactionTypeEnum InventoryTransactionType
        {
            get
            {
                return (InventoryTransactionTypeEnum)m_inventoryTransactionTypeId;
            }
            set
            {
                m_inventoryTransactionTypeId = (int)value;
            }
        }

        [XmlIgnore]
        public InventoryTransactionDetailTypeEnum InventoryTransactionDetailType
        {
            get
            {
                return (InventoryTransactionDetailTypeEnum)m_inventoryTransactionDetailTypeId;
            }
            set
            {
                m_inventoryTransactionDetailTypeId = (int)value;
            }
        }
    }
}
