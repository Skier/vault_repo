using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBRequestCustomerWriter:QBRequestWriter<Customer>
    {
        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType, List<QBAffectedObject<Customer>> returnList)
        {

            List<Customer> customerList = null;

            if (commandType == QBCommandTypeEnum.Add)
                customerList = Customer.FindBy(EntityState.Created);
            else if (commandType == QBCommandTypeEnum.Update)
                customerList = Customer.FindBy(EntityState.Modified);
            else return;

            if (customerList.Count > 0)
            {
                foreach (Customer customer in customerList)
                {
                    int requestId = customer.CustomerId;
     
                    xmlWriter.WriteStartElement(commandType == QBCommandTypeEnum.Add ? 
                        "CustomerAddRq" : "CustomerModRq");

                    xmlWriter.WriteAttributeString("requestID", requestId.ToString());

                    xmlWriter.WriteStartElement(commandType == QBCommandTypeEnum.Add ? 
                         "CustomerAdd" : "CustomerMod");

                    if (commandType == QBCommandTypeEnum.Update)
                    {
                        xmlWriter.WriteElementString("ListID", customer.QuickBooksListId.ToString());
                        xmlWriter.WriteElementString("EditSequence", customer.EditSequence.ToString());
                    }

                    xmlWriter.WriteElementString("Name", customer.Name);

                    if (customer.CompanyName != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("CompanyName", customer.CompanyName);

                    if (customer.Salutation != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("Salutation", customer.Salutation);                    

                    xmlWriter.WriteElementString("FirstName", customer.FirstName);

                    if (customer.MiddleName != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("MiddleName", customer.MiddleName);

                    xmlWriter.WriteElementString("LastName", customer.LastName);

                    if (customer.Suffix != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("Suffix", customer.Suffix);   
                    
                    if(!IsAllEmptyOrNull(customer.BillAddr1, customer.BillAddr2, customer.BillAddr3,
                                     customer.BillAddr4, customer.BillCity, customer.BillState,
                                     customer.BillPostalCode, customer.BillCountry) || commandType == QBCommandTypeEnum.Update)                    
                    {
                        xmlWriter.WriteStartElement("BillAddress");
                        xmlWriter.WriteElementString("Addr1", customer.BillAddr1);
                        xmlWriter.WriteElementString("Addr2", customer.BillAddr2);
                        xmlWriter.WriteElementString("Addr3", customer.BillAddr3);
                        xmlWriter.WriteElementString("Addr4", customer.BillAddr4);
                        xmlWriter.WriteElementString("City", customer.BillCity);
                        xmlWriter.WriteElementString("State", customer.BillState);
                        xmlWriter.WriteElementString("PostalCode", customer.BillPostalCode);
                        xmlWriter.WriteElementString("Country", customer.BillCountry);
                        xmlWriter.WriteEndElement();
                    }


                    if (!IsAllEmptyOrNull(customer.ShipAddr1, customer.ShipAddr2, customer.ShipAddr3,
                                     customer.ShipAddr4, customer.ShipCity, customer.ShipState,
                                     customer.ShipPostalCode, customer.ShipCountry) || commandType == QBCommandTypeEnum.Update)
                    {
                        if (!customer.ShippingAddressSameAsBilling)
                        {
                            xmlWriter.WriteStartElement("ShipAddress");
                            xmlWriter.WriteElementString("Addr1", customer.ShipAddr1);
                            xmlWriter.WriteElementString("Addr2", customer.ShipAddr2);
                            xmlWriter.WriteElementString("Addr3", customer.ShipAddr3);
                            xmlWriter.WriteElementString("Addr4", customer.ShipAddr4);
                            xmlWriter.WriteElementString("City", customer.ShipCity);
                            xmlWriter.WriteElementString("State", customer.ShipState);
                            xmlWriter.WriteElementString("PostalCode", customer.ShipPostalCode);
                            xmlWriter.WriteElementString("Country", customer.ShipCountry);
                            xmlWriter.WriteEndElement();
                        }
                    }

                    WriteElement(xmlWriter, "PrintAs", customer.PrintAs);

                    if (customer.Phone != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("Phone", customer.Phone);
                    if (customer.Mobile != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("Mobile", customer.Mobile);
                    if (customer.Pager != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("Pager", customer.Pager);
                    if (customer.AltPhone != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("AltPhone", customer.AltPhone);
                    if (customer.Fax != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("Fax", customer.Fax);
                    if (customer.Email != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("Email", customer.Email);

                    if (customer.Terms != null)
                    {
                        xmlWriter.WriteStartElement("TermsRef");
                        xmlWriter.WriteElementString("ListID", customer.Terms.QuickBooksListId.ToString());
                        xmlWriter.WriteElementString("FullName", customer.Terms.Name);
                        xmlWriter.WriteEndElement();
                    }

                    if (commandType == QBCommandTypeEnum.Add)
                    {
                        xmlWriter.WriteElementString("OpenBalance", (customer.Balance.HasValue)?customer.Balance.Value.ToString("0.00", QBDataType.USCulture):"0.00");
                        xmlWriter.WriteElementString("OpenBalanceDate", customer.BalanceDate.Value.ToString("yyyy-MM-dd"));
                    }

                    if (customer.ResaleNumber != String.Empty || commandType == QBCommandTypeEnum.Update)
                        xmlWriter.WriteElementString("ResaleNumber", customer.ResaleNumber);

                    xmlWriter.WriteElementString("DeliveryMethod", customer.DeliveryMethod);

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }
            }
        }
    }
}
