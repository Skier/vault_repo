using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using AerSysCo.Common;
using AerSysCo.Entity;

namespace AerSysCo.Warehouse
{
    public class UserTypeSvc
    {
        public static UserType FindById(SqlTransaction tran, int id)
        {
            Logger.ASSERT(0 != id);
            List<UserType> result = Select(tran, id);
            if (0 == result.Count)
            {
                string message = string.Format("No User Type with id {0} ", id);
                Logger.Error(Logger.GetAppLogger(), message, new Exception(), null);
                throw new ApplicationException(message);

            }
            else if (1 != result.Count)
            {
                string message = string.Format("Found {0} User Types with id {1} ", result.Count, id);
                Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null);
                throw new ApplicationException(message);
            }
            return result[0];
        }


        private static List<UserType> Select(SqlTransaction tran, int id)
        {
            String sql = " select UserTypeId, UserTypeName  "
                       + "   from UserType "
                       + "  where 1=1 ";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (0 != id)
            {
                sql += " and UserTypeId = @id ";
                SqlParameter param = new SqlParameter("@id", id);
                parms.Add(param);
            }

            List<UserType> result = new List<UserType>();
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray()))
            {
                while (rdr.Read())
                {
                    UserType ut = new UserType();
                    ut.userTypeId = rdr.GetInt32(rdr.GetOrdinal("UserTypeId"));
                    ut.userTypeName = rdr.GetString(rdr.GetOrdinal("UserTypeName"));
                    result.Add(ut);
                }
            }

            return result;
        }
    }
}
