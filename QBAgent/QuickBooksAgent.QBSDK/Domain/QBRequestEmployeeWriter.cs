using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBRequestEmployeeWriter : QBRequestWriter<Employee>
    {
        #region OnProcess

        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType,
                                          List<QBAffectedObject<Employee>> returnList)
        {
            List<Employee> emploeeList = new List<Employee>();

            if (commandType == QBCommandTypeEnum.Add)
                emploeeList = Employee.FindBy(EntityState.Created);
            else if (commandType == QBCommandTypeEnum.Update)
                emploeeList = Employee.FindBy(EntityState.Modified);
            else return;
            
            foreach (Employee employee in emploeeList)
            {
                int requestId = employee.EmployeeId;
                
                xmlWriter.WriteStartElement(commandType == QBCommandTypeEnum.Add ?
                                            "EmployeeAddRq" : "EmployeeModRq");

                xmlWriter.WriteAttributeString("requestID", requestId.ToString());

                xmlWriter.WriteStartElement(commandType == QBCommandTypeEnum.Add ?
                                            "EmployeeAdd" : "EmployeeMod");

                if (commandType == QBCommandTypeEnum.Update)
                {
                    xmlWriter.WriteElementString("ListID", employee.QuickBooksListId.ToString());
                    xmlWriter.WriteElementString("EditSequence", employee.EditSequence.ToString());
                }

                WriteAttribute(xmlWriter, "Name", employee.Name);
                WriteAttribute(xmlWriter, "Salutation", employee.Salutation);
                WriteAttribute(xmlWriter, "FirstName", employee.FirstName);
                WriteAttribute(xmlWriter, "MiddleName", employee.MiddleName);
                WriteAttribute(xmlWriter, "LastName", employee.LastName);
                WriteAttribute(xmlWriter, "Suffix", employee.Suffix);
                
                if (!IsAllEmptyOrNull(employee.Addr1, employee.Addr2, employee.Addr3, 
                                     employee.Addr4, employee.City, employee.State, 
                                     employee.PostalCode, employee.Country))
                {
                    xmlWriter.WriteStartElement("EmployeeAddress");
                    WriteAttribute(xmlWriter, "Addr1", employee.Addr1);
                    WriteAttribute(xmlWriter, "Addr2", employee.Addr2);
                    WriteAttribute(xmlWriter, "Addr3", employee.Addr3);
                    WriteAttribute(xmlWriter, "Addr4", employee.Addr4);
                    WriteAttribute(xmlWriter, "City", employee.City);
                    WriteAttribute(xmlWriter, "State", employee.State);
                    WriteAttribute(xmlWriter, "PostalCode", employee.PostalCode);
                    WriteAttribute(xmlWriter, "Country", employee.Country);
                    xmlWriter.WriteEndElement();
                }

                WriteAttribute(xmlWriter, "PrintAs", employee.PrintAs);
                WriteAttribute(xmlWriter, "Phone", employee.Phone);
                WriteAttribute(xmlWriter, "Mobile", employee.Mobile);
                WriteAttribute(xmlWriter, "AltPhone", employee.AltPhone);
                WriteAttribute(xmlWriter, "Email", employee.Email);

                if (commandType == QBCommandTypeEnum.Add)
                {
                    if (employee.HiredDate.HasValue)
                        WriteAttribute(xmlWriter, "HiredDate", employee.HiredDate.Value.ToString("yyyy-MM-dd"));

                    if (employee.ReleasedDate.HasValue)
                        WriteAttribute(xmlWriter, "ReleasedDate", employee.ReleasedDate.Value.ToString("yyyy-MM-dd"));
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }

        }

        #endregion
    }
}
