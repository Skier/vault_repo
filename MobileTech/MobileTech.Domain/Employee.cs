using System;
using System.Data;
using MobileTech.Data;
using System.Collections.Generic;


namespace MobileTech.Domain
{
    public partial class Employee
    {
        #region Sql Statements
        static String SqlFindByFLName = "Select LocationId, EmployeeId,FirstName,LastName From Employee Where FirstName = @FirstName and LastName = @LastName";
        static String SqlFindByLocation = "Select LocationId, EmployeeId,FirstName,LastName From Employee Where LocationId = @LocationId";

        #endregion

        #region Constructors

        public Employee()
        {

        }

        #endregion

        #region Extra fields
        public override string ToString()
        {
            return String.Format("{0} {1}", m_firstName,m_lastName);
        }
        #endregion

        #region Finders
        public static void FindBy(Location location, List<Employee> rv)
        {
            IDbCommand command = Database.PrepareCommand(SqlFindByLocation);
            Database.PutParameter(command, "@LocationId", location.LocationId);

            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rv.Add(Load(reader));
                }
            }
        }


        public static Employee FindBy(String name, String lastName)
        {
            IDbCommand command = Database.PrepareCommand(SqlFindByFLName);

            Database.PutParameter(command, "@FirstName", name);
            Database.PutParameter(command, "@LastName", lastName);

            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return Load(reader);
                }
            }

            return null;
        }

        public static Employee FindDefault()
        {
            IDbCommand command = Database.PrepareCommand(SqlSelectAll);

            using (IDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader.Read())
                {
                    return Load(reader);
                }
            }

            return null;

        }
        #endregion

    }
}
