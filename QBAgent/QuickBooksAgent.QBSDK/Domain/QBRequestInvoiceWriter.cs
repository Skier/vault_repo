using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBRequestInvoiceWriter:QBRequestWriter<Invoice>
    {
        #region OnProcess

        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType, List<QBAffectedObject<Invoice>> returnList)
        {
            List<Invoice> invoiceList;

            if (commandType == QBCommandTypeEnum.Add)
                invoiceList = Invoice.FindBy(EntityState.Created);
            else return;            
                  
            if (invoiceList.Count > 0)
            {                
                foreach (Invoice invoice in invoiceList)
                {
                    int requestId = invoice.InvoiceId;
                    
                    xmlWriter.WriteStartElement("InvoiceAddRq");
                    xmlWriter.WriteAttributeString("requestID", requestId.ToString());
                    xmlWriter.WriteStartElement("InvoiceAdd");

                    xmlWriter.WriteStartElement("CustomerRef");                                       
                    xmlWriter.WriteElementString("ListID", invoice.Customer.QuickBooksListId.ToString());
                    xmlWriter.WriteEndElement();

                    if (invoice.Account.QuickBooksListId != null)
                    {
                        xmlWriter.WriteStartElement("ARAccountRef");
                        xmlWriter.WriteElementString("ListID", invoice.Account.QuickBooksListId.ToString());
                        xmlWriter.WriteEndElement();                        
                    }

                    if (invoice.TxnDate.HasValue)                        
                        xmlWriter.WriteElementString("TxnDate", invoice.TxnDate.Value.ToString("yyyy-MM-dd"));
                    
                    WriteElement(xmlWriter, "RefNumber", invoice.RefNumber);

                    if (string.IsNullOrEmpty(invoice.BillAddr1)
                        && string.IsNullOrEmpty(invoice.BillAddr2)
                        && string.IsNullOrEmpty(invoice.BillAddr3)
                        && string.IsNullOrEmpty(invoice.BillAddr4)
                        && string.IsNullOrEmpty(invoice.BillCity)
                        && string.IsNullOrEmpty(invoice.BillState)
                        && string.IsNullOrEmpty(invoice.BillPostalCode)
                        && string.IsNullOrEmpty(invoice.BillCountry)) {}
                        else{
                            xmlWriter.WriteStartElement("BillAddress");                            
                            WriteElement(xmlWriter, "Addr1", invoice.BillAddr1);
                            WriteElement(xmlWriter, "Addr2", invoice.BillAddr2);
                            WriteElement(xmlWriter, "Addr3", invoice.BillAddr3);
                            WriteElement(xmlWriter, "Addr4", invoice.BillAddr4);
                            WriteElement(xmlWriter, "City", invoice.BillCity);
                            WriteElement(xmlWriter, "State", invoice.BillState);
                            WriteElement(xmlWriter, "PostalCode", invoice.BillPostalCode);
                            WriteElement(xmlWriter, "Country", invoice.BillCountry);
                            xmlWriter.WriteEndElement();                        
                    }

                    if (string.IsNullOrEmpty(invoice.ShipAddr1)
                        && string.IsNullOrEmpty(invoice.ShipAddr2)
                        && string.IsNullOrEmpty(invoice.ShipAddr3)
                        && string.IsNullOrEmpty(invoice.ShipAddr4)
                        && string.IsNullOrEmpty(invoice.ShipCity)
                        && string.IsNullOrEmpty(invoice.ShipState)
                        && string.IsNullOrEmpty(invoice.ShipPostalCode)
                        && string.IsNullOrEmpty(invoice.ShipCountry)) { }
                    else
                    {
                        xmlWriter.WriteStartElement("ShipAddress");
                        WriteElement(xmlWriter, "Addr1", invoice.ShipAddr1);
                        WriteElement(xmlWriter, "Addr2", invoice.ShipAddr2);
                        WriteElement(xmlWriter, "Addr3", invoice.ShipAddr3);
                        WriteElement(xmlWriter, "Addr4", invoice.ShipAddr4);
                        WriteElement(xmlWriter, "City", invoice.ShipCity);
                        WriteElement(xmlWriter, "State", invoice.ShipState);
                        WriteElement(xmlWriter, "PostalCode", invoice.ShipPostalCode);
                        WriteElement(xmlWriter, "Country", invoice.ShipCountry);
                        xmlWriter.WriteEndElement();
                    }
                                               
                    if (invoice.Terms != null)
                    {
                        xmlWriter.WriteStartElement("TermsRef");
                        xmlWriter.WriteElementString("ListID", invoice.Terms.QuickBooksListId.ToString());
                        xmlWriter.WriteEndElement();
                    }

                    if (invoice.DueDate.HasValue)
                        xmlWriter.WriteElementString("DueDate", invoice.DueDate.Value.ToString("yyyy-MM-dd"));

                    if (invoice.ShipDate.HasValue)
                        xmlWriter.WriteElementString("ShipDate", invoice.ShipDate.Value.ToString("yyyy-MM-dd"));

                    WriteElement(xmlWriter, "Memo", invoice.Memo);
                    xmlWriter.WriteElementString("IsToBePrinted", invoice.IsToBePrinted.ToString()); 

                    foreach (InvoiceLine invoiceLine in invoice.InvoiceLines)
                    {
                        xmlWriter.WriteStartElement("InvoiceLineAdd");

                        if (invoiceLine.Item != null)
                        {
                            xmlWriter.WriteStartElement("ItemRef");
                            xmlWriter.WriteElementString("ListID",
                                                         invoiceLine.Item.QuickBooksListId.ToString());
                            xmlWriter.WriteEndElement();                            
                        }

                        WriteElement(xmlWriter, "Desc", invoiceLine.LineDescription);
                        
                        
                        if (invoiceLine.Quantity != null)
                            WriteElement(xmlWriter, "Quantity", QBDataType.RoundTripFormat(invoiceLine.Quantity, QBDataType.USCulture));

                        if (invoiceLine.Rate.HasValue)
                            WriteElement(xmlWriter, "Rate", QBDataType.RoundTripFormat(invoiceLine.Rate, QBDataType.USCulture));
                        else if (invoiceLine.RatePercent.HasValue)
                            WriteElement(xmlWriter, "RatePercent", QBDataType.RoundTripFormat(invoiceLine.RatePercent, QBDataType.USCulture));                            
                        
                        if (invoiceLine.Amount.HasValue)
                            WriteElement(xmlWriter, "Amount", invoiceLine.Amount.Value.ToString("0.00", QBDataType.USCulture));            
            
                        if (invoiceLine.ServiceDate.HasValue)                        
                            xmlWriter.WriteElementString("ServiceDate", 
                                                         invoiceLine.ServiceDate.Value.ToString("yyyy-MM-dd"));

                        WriteElement(xmlWriter, "IsTaxable", invoiceLine.IsTaxable.ToString());                                    
                        xmlWriter.WriteEndElement();
                    }

                    if (invoice.DiscountLineAmount.HasValue || invoice.DiscountLineRatePercent.HasValue)
                    {
                        xmlWriter.WriteStartElement("DiscountLineAdd");

                        if (invoice.DiscountLineRatePercent.HasValue)
                            WriteElement(xmlWriter, "RatePercent", QBDataType.RoundTripFormat(invoice.DiscountLineRatePercent, QBDataType.USCulture));
                        else if (invoice.DiscountLineAmount.HasValue)
                            WriteElement(xmlWriter, "Amount", invoice.DiscountLineAmount.Value.ToString("0.00", QBDataType.USCulture));

                        WriteElement(xmlWriter, "IsTaxable", (!invoice.TaxCalculationType).ToString());
                        if (invoice.DiscountLineAccount != null)
                        {
                            xmlWriter.WriteStartElement("AccountRef");
                            WriteElement(xmlWriter, "ListID", invoice.DiscountLineAccount.QuickBooksListId.ToString());
                            xmlWriter.WriteEndElement();                            
                        }
                        
                        xmlWriter.WriteEndElement();                        
                    }
                        

                    if (invoice.SalesTaxLineAmount.HasValue || invoice.SalesTaxLineRatePercent.HasValue)
                    {
                        xmlWriter.WriteStartElement("SalesTaxLineAdd");

                        if (invoice.SalesTaxLineRatePercent.HasValue)
                            WriteElement(xmlWriter, "RatePercent", QBDataType.RoundTripFormat(invoice.SalesTaxLineRatePercent, QBDataType.USCulture));
                        else if (invoice.SalesTaxLineAmount.HasValue)
                            WriteElement(xmlWriter, "Amount", invoice.SalesTaxLineAmount.Value.ToString("0.00", QBDataType.USCulture));
                        
                        if (invoice.SalesTaxLineAccount != null)
                        {
                            xmlWriter.WriteStartElement("AccountRef");
                            WriteElement(xmlWriter, "ListID", invoice.SalesTaxLineAccount.QuickBooksListId.ToString());
                            xmlWriter.WriteEndElement();
                        }
                                                
                        xmlWriter.WriteEndElement();
                    }

                    if (invoice.ShippingLineAmount.HasValue)
                    {
                        xmlWriter.WriteStartElement("ShippingLineAdd");
                        if (invoice.ShippingLineAmount.HasValue)
                            WriteElement(xmlWriter, "Amount", invoice.ShippingLineAmount.Value.ToString("0.00", QBDataType.USCulture));
                        
                        if (invoice.ShippingLineAccount != null)
                        {
                            xmlWriter.WriteStartElement("AccountRef");
                            WriteElement(xmlWriter, "ListID", invoice.ShippingLineAccount.QuickBooksListId.ToString());
                            xmlWriter.WriteEndElement();
                        }
                        
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }
            }
        }

        #endregion

    }
}
