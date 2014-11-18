using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;
using System.Xml.Serialization;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseEmployeeReader : QBResponseReader<Employee>
    {
        #region Convert

        protected override Employee Convert(object item)
        {
            EmployeeRet employeeRet = (EmployeeRet)item;            
            
            if (employeeRet.EmployeeAddress == null)
                employeeRet.EmployeeAddress = new EmployeeAddress();
                
            
            Employee employee =
                new Employee(
                    null,
                    0,
                    null,
                    employeeRet.ListId,
                    employeeRet.EditSequence,
                    employeeRet.Name,
                    employeeRet.Salutation,
                    employeeRet.FirstName,
                    employeeRet.MiddleName,
                    employeeRet.LastName,
                    employeeRet.Suffix,
                    employeeRet.EmployeeAddress.Addr1,
                    employeeRet.EmployeeAddress.Addr2,
                    employeeRet.EmployeeAddress.Addr3,
                    employeeRet.EmployeeAddress.Addr4,
                    employeeRet.EmployeeAddress.City,
                    employeeRet.EmployeeAddress.State,
                    employeeRet.EmployeeAddress.PostalCode,
                    employeeRet.EmployeeAddress.Country,
                    employeeRet.PrintAs,
                    employeeRet.Phone,
                    employeeRet.Mobile,
                    employeeRet.AltPhone,
                    employeeRet.Email,
                    employeeRet.HiredDate,
                    employeeRet.ReleasedDate);

            return employee;
        }

        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Employee> item)
        {
            QBAffectedObject<Employee> expectedEmployee = null;
            if (item.CommandType == QBCommandTypeEnum.Add || item.CommandType == QBCommandTypeEnum.Update)
            {
                try
                {
                    expectedEmployee = new QBAffectedObject<Employee>(Employee.FindByPrimaryKey(item.RequestId), item.RequestId);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }
            }            
            
            
            #region Process added Employees

            if (item.CommandType == QBCommandTypeEnum.Add)
            {
                Employee employee = item.DomainObject;

                expectedEmployee.DomainObject.EditSequence = employee.EditSequence;
                expectedEmployee.DomainObject.QuickBooksListId = employee.QuickBooksListId;
                expectedEmployee.DomainObject.EntityState = EntityState.Synchronized;

                Employee.Update(expectedEmployee.DomainObject);

                QBEntity qbEntity = new QBEntity(QBEntityType.Employee, (int)employee.QuickBooksListId);
                QBEntity.Insert(qbEntity);
            }
            #endregion

            #region Process updated Employees

            if (item.CommandType == QBCommandTypeEnum.Update)
            {
                Employee employee = item.DomainObject;

                Employee previousEmployee
                    = Employee.FindByPrimaryKey(expectedEmployee.DomainObject.ModifiedEmployeeId.Value);

                Employee.Delete(expectedEmployee);

                expectedEmployee.DomainObject.EmployeeId = previousEmployee.EmployeeId;
                expectedEmployee.DomainObject.EntityState = EntityState.Synchronized;
                expectedEmployee.DomainObject.EditSequence = employee.EditSequence;

                Employee.Update(expectedEmployee.DomainObject);
            }

            #endregion

            #region Process queried Employees

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Employee employee = item.DomainObject;

                try
                {
                    Employee existingEmployee = Employee.FindBy((int)employee.QuickBooksListId);
                    employee.EmployeeId = existingEmployee.EmployeeId;
                    employee.EntityState = EntityState.Synchronized;
                    Employee.Update(employee);
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(employee);

                    employee.EntityState = EntityState.Synchronized;
                    Employee.Insert(employee);

                    QBEntity qbEntity = new QBEntity(QBEntityType.Employee, (int)employee.QuickBooksListId);
                    QBEntity.Insert(qbEntity);
                }
            }

            #endregion            
            
        }

        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return "EmployeeRet"; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(EmployeeRet); }
        }
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "EmployeeQueryRs".Equals(nodeName)
                   || "EmployeeModRs".Equals(nodeName)
                   || "EmployeeAddRs".Equals(nodeName);
        }

        #endregion

        #region EmployeeRet

        [XmlRoot("EmployeeRet")]
        public class EmployeeRet : QBResponseItem
        {
            #region Name

            String m_name;
            [XmlElement("Name")]
            public String Name
            {
                get { return m_name; }
                set { m_name = value; }
            }

            #endregion
            
            #region Salutation

            String m_salutation;
            [XmlElement("Salutation")]
            public String Salutation 
            {
                get { return m_salutation; }
                set { m_salutation = value; }
            }

            #endregion

            #region FirstName
            String m_firstName;
            [XmlElement("FirstName")]
            public String FirstName
            {
                get { return m_firstName; }
                set { m_firstName = value; }
            }
            #endregion
                                   
            #region MiddleName
            String m_middleName;
            [XmlElement("MiddleName")]
            public String MiddleName
            {
                get { return m_middleName; }
                set { m_middleName = value; }
            }
            #endregion
            
            #region LastName
            String m_lastName;
            [XmlElement("LastName")]
            public String LastName
            {
                get { return m_lastName; }
                set { m_lastName = value; }
            }
            #endregion

            #region Suffix
            String m_suffix;
            [XmlElement("Suffix")]
            public String Suffix
            {
                get { return m_suffix; }
                set { m_suffix = value; }
            }
            #endregion

            #region EmployeeAddress
            EmployeeAddress m_employeeAddress;
            [XmlElement("EmployeeAddress")]
            public EmployeeAddress EmployeeAddress
            {
                get { return m_employeeAddress; }
                set { m_employeeAddress = value; }
            }
            #endregion

            #region PrintAs
            String m_printAs;
            [XmlElement("PrintAs")]
            public String PrintAs
            {
                get { return m_printAs; }
                set { m_printAs = value; }
            }
            #endregion
            
            #region Phone
            String m_phone;
            [XmlElement("Phone")]
            public String Phone
            {
                get { return m_phone; }
                set { m_phone = value; }
            }
            #endregion

            #region Mobile
            String m_mobile;
            [XmlElement("Mobile")]
            public String Mobile
            {
                get { return m_mobile; }
                set { m_mobile = value; }
            }
            #endregion
            
            #region AltPhone
            String m_altPhone;
            [XmlElement("AltPhone")]
            public String AltPhone
            {
                get { return m_altPhone; }
                set { m_altPhone = value; }
            }
            #endregion
            
            #region Email
            String m_email;
            [XmlElement("Email")]
            public String Email
            {
                get { return m_email; }
                set { m_email = value; }
            }
            #endregion

            #region HiredDate
            DateTime m_hiredDate;
            [XmlElement("HiredDate")]
            public DateTime HiredDate
            {
                get { return m_hiredDate; }
                set { m_hiredDate = value; }
            }
            #endregion

            #region ReleasedDate
            DateTime m_releasedDate;
            [XmlElement("ReleasedDate")]
            public DateTime ReleasedDate
            {
                get { return m_releasedDate; }
                set { m_releasedDate = value; }
            }
            #endregion
        }        

        #endregion

        #region EmployeeAddress

        public class EmployeeAddress
        {
            #region Addr1
            string m_addr1;
            [XmlElement("Addr1")]
            public string Addr1
            {
                get { return m_addr1; }
                set { m_addr1 = value; }
            }
            #endregion

            #region Addr2
            string m_addr2;
            [XmlElement("Addr2")]
            public string Addr2
            {
                get { return m_addr2; }
                set { m_addr2 = value; }
            }
            #endregion

            #region Addr3
            string m_addr3;
            [XmlElement("Addr3")]
            public string Addr3
            {
                get { return m_addr3; }
                set { m_addr3 = value; }
            }
            #endregion

            #region Addr4
            string m_addr4;
            [XmlElement("Addr4")]
            public string Addr4
            {
                get { return m_addr4; }
                set { m_addr4 = value; }
            }
            #endregion

            #region City
            string m_city;
            [XmlElement("City")]
            public string City
            {
                get { return m_city; }
                set { m_city = value; }
            }
            #endregion

            #region State
            string m_state;
            [XmlElement("State")]
            public string State
            {
                get { return m_state; }
                set { m_state = value; }
            }
            #endregion

            #region PostalCode
            string m_postalCode;
            [XmlElement("PostalCode")]
            public string PostalCode
            {
                get { return m_postalCode; }
                set { m_postalCode = value; }
            }
            #endregion

            #region Country
            string m_country;
            [XmlElement("Country")]
            public string Country
            {
                get { return m_country; }
                set { m_country = value; }
            }
            #endregion
        }

        #endregion
    }
}
