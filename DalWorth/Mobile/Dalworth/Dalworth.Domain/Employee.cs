using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Employee : ICounterField
    {
        public Employee() { }

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Employee"; }
        }

        #endregion        

        #region EmployeeType

        public EmployeeTypeEnum EmployeeType
        {
            get { return (EmployeeTypeEnum)m_employeeTypeId; }
            set { m_employeeTypeId = (int)value; }
        }

        #endregion

        #region DisplayName

        public string DisplayName
        {
            get { return m_firstName + ", " + m_lastName; }
        }

        #endregion

        #region FindBy EmployeeType

        private const string SqlFindByEmployeeType =
            @"SELECT *
            FROM Employee
                WHERE EmployeeTypeId = @EmployeeTypeId";

        public static List<Employee> FindBy(EmployeeTypeEnum employeeType)
        {
            List<Employee> emploeeList = new List<Employee>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEmployeeType))
            {
                Database.PutParameter(dbCommand, "@EmployeeTypeId", (int)employeeType);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        emploeeList.Add(Load(dataReader));
                    }
                }
            }
            return emploeeList;
        }

        #endregion        
    }
}
      