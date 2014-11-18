using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using AerSysCo.Common;
using AerSysCo.Entity;

namespace AerSysCo.Warehouse
{
    public class ASCUserSvc
    {
        public static ASCUser FindByLoginAndPassword(SqlTransaction tran, string login, string password) {
            Logger.ASSERT(login != null);
            Logger.ASSERT(password != null);
            List<ASCUser> result = Select(tran, login, password);
            if (1 < result.Count) {
                string message = string.Format("Found {0} ASCUsers login {1} and password {2} ", result.Count, login, password);
                Logger.Fatal(Logger.GetAppLogger(), message, new Exception(), null);
                throw new ApplicationException(message);
            }
            
            if (result.Count > 0) {
                return result[0];
            } else {
                return null;
            }
        }


        private static List<ASCUser> Select(SqlTransaction tran, string login, string password) {
            String sql = " select UserId, UserTypeId, BrandId, Login, Password, LastUpdateDate, CreatedByUser, DateCreated  "
                       + "   from ASCUser "
                       + "  where 1=1 ";
            List<SqlParameter> parms = new List<SqlParameter>();
            if (null != login) {
                sql += " and Login = @login ";
                SqlParameter param = new SqlParameter("@login", login);
                parms.Add(param);
            }

            if (null != password) {
                sql += " and Password = @password ";
                SqlParameter param = new SqlParameter("@password", password);
                parms.Add(param);
            }

            List<ASCUser> result = new List<ASCUser>();
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, parms.ToArray())) {
                while (rdr.Read()) {
                    ASCUser u = new ASCUser();
                    u.userId = rdr.GetInt32(rdr.GetOrdinal("UserId"));
                    u.userTypeId = rdr.GetInt32(rdr.GetOrdinal("UserTypeId"));
                    u.brandId = rdr.GetInt32(rdr.GetOrdinal("BrandId"));
                    u.login = rdr.GetString(rdr.GetOrdinal("Login"));
                    u.password = rdr.GetString(rdr.GetOrdinal("Password"));
                    TraceableSvc.FromReader(rdr, u);
                    result.Add(u);
                }
            }

            return result;
        }
    }
}
