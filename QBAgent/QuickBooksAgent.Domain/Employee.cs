using System;
using System.Collections.Generic;
using QuickBooksAgent.Data;
using System.Data;
  
namespace QuickBooksAgent.Domain
{
    public partial class Employee : ICounterField
    {
        #region Default constructor

        public Employee()
        {
            this.EntityState = new EntityState();
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_employeeId; }
            set { m_employeeId = value; }
        }

        public string CounterName
        {
            get { return "Employee"; }
        }

        #endregion        

        #region FindBy Entity State
        
        private const string SqlFindByEntityState  = 
            @"SELECT EmployeeId, ModifiedEmployeeId, QuickBooksListId, EntityStateId, 
                EditSequence, Name, Salutation, FirstName, MiddleName, LastName, Suffix, 
                Addr1, Addr2, Addr3, Addr4, City, State, PostalCode, Country, 
                PrintAs, Phone, Mobile, AltPhone, Email, HiredDate, ReleasedDate 
            FROM Employee
                WHERE EntityStateId = @EntityStateId";

        public static List<Employee> FindBy(EntityState entityState)
        {
            List<Employee> emploeeList = new List<Employee>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

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
        
        #region FindBy QuickBooksListId

        private const string SqlFindByQuickBooksId =
            @"SELECT EmployeeId, ModifiedEmployeeId, QuickBooksListId, EntityStateId, 
                EditSequence, Name, Salutation, FirstName, MiddleName, LastName, Suffix, 
                Addr1, Addr2, Addr3, Addr4, City, State, PostalCode, Country, 
                PrintAs, Phone, Mobile, AltPhone, Email, HiredDate, ReleasedDate 
            FROM Employee
                WHERE QuickBooksListId = @QuickBooksListId";

        public static Employee FindBy(int quickBooksListId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksListId", quickBooksListId.ToString());

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("Employee with List ID "
                        + quickBooksListId.ToString() + " not found");
                }
            }
        }

        #endregion                

        #region ToString

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Equals & GetHashCode

        public override int GetHashCode()
        {
            return m_employeeId;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Employee employee = obj as Employee;
            if (employee == null) return false;
            if (m_employeeId != employee.m_employeeId) return false;
            return true;
        }

        #endregion
    }
}
      