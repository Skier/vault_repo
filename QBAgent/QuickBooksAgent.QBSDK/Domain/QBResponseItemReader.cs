using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;
using System.Xml.Serialization;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseItemReader:QBResponseReader<Item>
    {
        const String TARGET_NODE = "ItemServiceRet";

        #region Convert
        protected override Item Convert(object item)
        {
            ItemRet itemRet = (ItemRet)item;

            Item _item = 
                new Item(0,
                        itemRet.ListId,
                        itemRet.EditSequence,
                        itemRet.Name,
                        itemRet.SalesOrPurchase != null ? itemRet.SalesOrPurchase.Price : 0
                 );
     
            return _item;
        }
        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Item> item)
        {
            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Item myItem = item.DomainObject;
                
                try
                {
                    Item existingItem = Item.FindByQuickBooksId(myItem.QuickBooksListId);
                    myItem.ItemId = existingItem.ItemId;
                    Item.Update(myItem);
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(myItem);
                    Item.Insert(myItem);
                }
            }            
            
        }

        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return TARGET_NODE; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(ItemRet) ; }
        }
        #endregion

        [XmlRoot("ItemServiceRet")]
        public class ItemRet:QBResponseItem
        {
            #region Name
            String m_Name;
            [XmlElement("Name")]
            public String Name
            {
                get { return m_Name; }
                set { m_Name = value; }
            }
            #endregion

            #region SalesOrPurchase
            SalesOrPurchase m_salesOrPurchase;
            [XmlElement("SalesOrPurchase")]
            public SalesOrPurchase SalesOrPurchase
            {
                get { return m_salesOrPurchase; }
                set { m_salesOrPurchase = value; }
            }
            #endregion
        }

        [XmlRoot("SalesOrPurchase")]
        public class SalesOrPurchase
        {
            public SalesOrPurchase() { }

            #region Price
            decimal m_price;
            [XmlElement("Price")]
            public decimal Price
            {
                get { return m_price; }
                set { m_price = value; }
            }
            #endregion
        }

        public override bool IsRootNode(string nodeName)
        {
            return "ItemQueryRs".Equals(nodeName);
        }
    }
}
