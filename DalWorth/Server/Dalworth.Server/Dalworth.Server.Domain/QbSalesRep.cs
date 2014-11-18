using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class QbSalesRep
    {
        public QbSalesRep()
        {

        }

        #region Properties

        public string DisplayName
        {
            get
            {

                string dispayName = string.Empty;
                if (LastName != null)
                {
                    dispayName += LastName + " ";
                    if (this.FirstName != null)
                        dispayName += FirstName + " ";
                }

                if (dispayName == string.Empty) 
                    dispayName += FullName;

                return dispayName;
            }
        }

        #endregion

        #region FindByEmployeeId

        private const String SqlSelectByEmployeeId =
            SqlSelectAll + @" where employeeId = ?EmployeeId";

        public static QbSalesRep FindByEmployeeId(
            int employeeId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByEmployeeId, connection))
            {

                Database.PutParameter(dbCommand, "?EmployeeId", employeeId);


                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("QbSalesRep not found, FindByEmployeeId");
        }

        #endregion

        #region FindActive

        private const string SqlSelectActive = SqlSelectAll + " where isActive = true order by fullname";
        public static List<QbSalesRep> FindActive(IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectActive, connection))
            {
                List<QbSalesRep> rv = new List<QbSalesRep>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                }

                return rv;
            }
        }

        #endregion
    }
}
      