using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;
using System.Xml.Serialization;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseTermsReader : QBResponseReader<Terms>
    {
        #region Convert
        
        protected override Terms Convert(object item)
        {
            TermsRet termsRet = (TermsRet)item;

            Terms terms = new Terms(
                0, 
                termsRet.ListId, 
                termsRet.Name,
                termsRet.TimeCreated,
                termsRet.TimeModified,
                termsRet.EditSequence,
                termsRet.StdDueDays,
                termsRet.StdDiscountDays,
                termsRet.DiscountPct);

            return terms;
        }
        
        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Terms> item)
        {
            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Terms terms = item.DomainObject;
                
                try
                {
                    Terms existingItem = Terms.FindByQuickBooksId(terms.QuickBooksListId);
                    terms.TermsId = existingItem.TermsId;
                    Terms.Update(terms);
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(terms);
                    Terms.Insert(terms);
                }
            }            
            
        }

        #endregion

        #region TargetNodeName
        
        protected override string TargetNodeName
        {
            get { return "StandardTermsRet"; }
        }
        
        #endregion

        #region TargetClassType
        
        protected override Type TargetClassType
        {
            get { return typeof(TermsRet); }
        }
        
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "StandardTermsQueryRs".Equals(nodeName)
                   || "StandardTermsAddRs".Equals(nodeName);                        
        }

        #endregion

        #region TermsRet

        [XmlRoot("StandardTermsRet")]
        public class TermsRet : QBResponseItem
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

            #region StdDueDays

            int? m_stdDueDays;
            [XmlElement("StdDueDays")]
            public int? StdDueDays
            {
                get { return m_stdDueDays; }
                set { m_stdDueDays = value; }
            }

            #endregion

            #region StdDiscountDays

            int? m_stdDiscountDays;
            [XmlElement("StdDiscountDays")]
            public int? StdDiscountDays
            {
                get { return m_stdDiscountDays; }
                set { m_stdDiscountDays = value; }
            }

            #endregion

            #region DiscountPct

            decimal? m_discountPct;
            [XmlElement("DiscountPct")]
            public decimal? DiscountPct
            {
                get { return m_discountPct; }
                set { m_discountPct = value; }
            }

            #endregion
        }

        #endregion        
    }
}
