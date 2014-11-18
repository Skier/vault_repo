using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;
using System.Xml;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBRequestVendorWriter : QBRequestWriter<Vendor>
    {
        #region OnProcess

        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType,
                                          List<QBAffectedObject<Vendor>> returnList)
        {
            List<Vendor> vendorList = new List<Vendor>();

            if (commandType == QBCommandTypeEnum.Add)
                vendorList = Vendor.FindBy(EntityState.Created);
            else if (commandType == QBCommandTypeEnum.Update)
                vendorList = Vendor.FindBy(EntityState.Modified);
            else return;            

            foreach (Vendor vendor in vendorList)
            {
                int requestId = vendor.VendorId;
                
                xmlWriter.WriteStartElement(commandType == QBCommandTypeEnum.Add ?
                                            "VendorAddRq" : "VendorModRq");

                xmlWriter.WriteAttributeString("requestID", requestId.ToString());

                xmlWriter.WriteStartElement(commandType == QBCommandTypeEnum.Add ?
                                            "VendorAdd" : "VendorMod");

                if (commandType == QBCommandTypeEnum.Update)
                {
                    xmlWriter.WriteElementString("ListID", vendor.QuickBooksListId.ToString());
                    xmlWriter.WriteElementString("EditSequence", vendor.EditSequence.ToString());
                }

                WriteAttribute(xmlWriter, "Name", vendor.Name);
                WriteAttribute(xmlWriter, "CompanyName", vendor.CompanyName);
                WriteAttribute(xmlWriter, "Salutation", vendor.Salutation);
                WriteAttribute(xmlWriter, "FirstName", vendor.FirstName);
                WriteAttribute(xmlWriter, "MiddleName", vendor.MiddleName);
                WriteAttribute(xmlWriter, "LastName", vendor.LastName);
                WriteAttribute(xmlWriter, "Suffix", vendor.Suffix);

                if (!IsAllEmptyOrNull(vendor.Addr1, vendor.Addr2, vendor.Addr3,
                                     vendor.Addr4, vendor.City, vendor.State,
                                     vendor.PostalCode, vendor.Country))
                {
                    xmlWriter.WriteStartElement("VendorAddress");
                    WriteAttribute(xmlWriter, "Addr1", vendor.Addr1);
                    WriteAttribute(xmlWriter, "Addr2", vendor.Addr2);
                    WriteAttribute(xmlWriter, "Addr3", vendor.Addr3);
                    WriteAttribute(xmlWriter, "Addr4", vendor.Addr4);
                    WriteAttribute(xmlWriter, "City", vendor.City);
                    WriteAttribute(xmlWriter, "State", vendor.State);
                    WriteAttribute(xmlWriter, "PostalCode", vendor.PostalCode);
                    WriteAttribute(xmlWriter, "Country", vendor.Country);
                    xmlWriter.WriteEndElement();
                }

                WriteAttribute(xmlWriter, "Phone", vendor.Phone);
                WriteAttribute(xmlWriter, "Mobile", vendor.Mobile);
                WriteAttribute(xmlWriter, "Pager", vendor.Pager);
                WriteAttribute(xmlWriter, "AltPhone", vendor.AltPhone);
                WriteAttribute(xmlWriter, "Fax", vendor.Fax);
                WriteAttribute(xmlWriter, "Email", vendor.Email);
                WriteAttribute(xmlWriter, "NameOnCheck", vendor.NameOnCheck);

                if (commandType == QBCommandTypeEnum.Add)
                {
                    WriteAttribute(xmlWriter, "VendorTaxIdent", vendor.VendorTaxIdent);
                    WriteAttribute(xmlWriter, "IsVendorEligibleFor1099", 
                                   vendor.IsVendorEligibleFor1099.ToString().ToLower());
                }
                
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }

        }

        #endregion
    }
}
