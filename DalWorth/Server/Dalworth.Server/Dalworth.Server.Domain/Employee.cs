using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class Employee
    {
        public Employee(){ }

        #region SecurityPermissions

        private SecurityPermissionCollection m_securityPermissions;
        public SecurityPermissionCollection SecurityPermissions
        {
            get { return m_securityPermissions; }
        }

        #endregion 

        #region EmployeeType

        [XmlIgnore]
        public EmployeeTypeEnum EmployeeType
        {
            get { return (EmployeeTypeEnum)m_employeeTypeId; }
            set { m_employeeTypeId = (int)value; }
        }

        #endregion

        #region LoadSecurityPermissions

        public void LoadSecurityPermissions(IDbConnection connection)
        {
            List<SecurityPermission> permissions = SecurityPermission.FindBySecurityRoleId(SecurityRoleId, connection);
            m_securityPermissions = new SecurityPermissionCollection(permissions);
        }

        #endregion 

        #region FindEmployee

        private const string SqlFindByWorkDate =
            @"select * from Employee
                where EmployeeTypeId = 1
	            and ID not in (
		            select TechnicianEmployeeId from Work
		            where StartDate >= ?StartDateStart
			            and StartDate < ?StartDateEnd
	                )";


        public static List<Employee> FindEmployee(DateTime workDate)
        {
            List<Employee> employees = new List<Employee>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkDate))
            {
                Database.PutParameter(dbCommand, "?StartDateStart", new DateTime(workDate.Year, workDate.Month, workDate.Day, 0, 0, 0, 0));
                Database.PutParameter(dbCommand, "?StartDateEnd", new DateTime(workDate.Year, workDate.Month, workDate.Day, 23, 59, 59, 999));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        employees.Add(Load(dataReader));
                    }
                }
            }
            return employees;
        }

        #endregion        

        #region FindBy Login and type

        private const string SqlFindByFirstNameAndType =
            @"select * from Employee
                where Login = ?Login
                    and EmployeeTypeId = ?EmployeeTypeId";


        public static Employee FindBy(string login, EmployeeTypeEnum employeeType, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByFirstNameAndType, connection))
            {
                Database.PutParameter(dbCommand, "?Login", login);
                Database.PutParameter(dbCommand, "?EmployeeTypeId", (int)employeeType);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        public static Employee FindBy(string firstName, EmployeeTypeEnum employeeType)
        {
            return FindBy(firstName, employeeType, null);
        }

        #endregion        

        #region FindBy EmployeeType

        private const string SqlFindByEmployeeType =
            @"SELECT *
            FROM Employee
                WHERE EmployeeTypeId = ?EmployeeTypeId and IsActive and IsUnknown = 0
            order by LastName, FirstName";

        public static List<Employee> FindBy(EmployeeTypeEnum employeeType, IDbConnection connection)
        {
            List<Employee> emploeeList = new List<Employee>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEmployeeType, connection))
            {
                Database.PutParameter(dbCommand, "?EmployeeTypeId", (int)employeeType);

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

        public static List<Employee> FindBy(EmployeeTypeEnum employeeType)
        {
            return FindBy(employeeType, null);
        }

        #endregion        

        #region FindUnknownTechnicians

        private const string SqlFindUnknowns =
            @"SELECT *
            FROM Employee
                WHERE EmployeeTypeId = 1 and IsActive and IsRestoration and IsUnknown";

        public static List<Employee> FindUnknownTechnicians()
        {
            List<Employee> emploeeList = new List<Employee>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindUnknowns))
            {
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

        #region FindRealTechnicians

        private const string SqlFindRealTechnicians =
            @"SELECT *
            FROM Employee
                WHERE EmployeeTypeId = 1 and IsActive and IsRestoration and not IsUnknown
            order by LastName, FirstName";

        public static List<Employee> FindRealTechnicians()
        {
            List<Employee> emploeeList = new List<Employee>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindRealTechnicians))
            {
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

        #region DisplayName

        public string DisplayName
        {
            get { return Utils.JoinStrings(", ", m_lastName, m_firstName); }
        }

        #endregion

        #region FindEmployeePerformedStartDay

        private const string SqlFindEmployeePerformedStartDay =
            @"SELECT e.* FROM worktransaction w
                inner join Employee e on e.ID = w.EmployeeId
                where WorkId = ?WorkId
                and WorkTransactionTypeId = 1";


        public static Employee FindEmployeePerformedStartDay(Work work)
        {            
            if (work.WorkStatus == WorkStatusEnum.StartDayDone
                || work.WorkStatus == WorkStatusEnum.Completed)
            {
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindEmployeePerformedStartDay))
                {
                    Database.PutParameter(dbCommand, "?WorkId", work.ID);

                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                            return Load(dataReader);
                    }
                }                
            }

            return null;
        }

        #endregion

        #region FindBy ServmanTechId

        private const string SqlFindByServmanTechId =
            @"SELECT *
            FROM Employee
                WHERE ServmanTechId = ?ServmanTechId";

        public static Employee FindByServmanTechId(string servmanTechId)
        {
            return FindByServmanTechId(servmanTechId, null);
        }

        public static Employee FindByServmanTechId(string servmanTechId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServmanTechId, connection))
            {
                Database.PutParameter(dbCommand, "?ServmanTechId", servmanTechId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Technician not found, search by ServmanTechId");
        }

        #endregion        
    }
}
      