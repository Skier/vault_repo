using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.SDK;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public enum SecurityPermissionEnum
    {
        ViewReports = 1,
        ViewDashboardTotal = 2
    }

    public class SecurityPermissionCollection
    {
        private List<SecurityPermission> m_securityPermissions;

        public SecurityPermissionCollection(List<SecurityPermission> securityPermissions)
        {
            m_securityPermissions = securityPermissions;
        }

        public bool Contains(SecurityPermissionEnum securityPermission)
        {
            if (m_securityPermissions == null || m_securityPermissions.Count == 0)
                return false;

            foreach (SecurityPermission permission in m_securityPermissions)
            {
                if (permission.ID == (int)securityPermission)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public partial class SecurityPermission
    {

        public SecurityPermission()
        {

        }

        #region  FindBySecurityRoleId

        private const String SqlSelectBySecurityRoleId =
        @"SELECT sp.* FROM securitypermission sp
            join securityrolepermission srp on sp.id = srp.SecurityPermissionId
            where srp.securityRoleId = ?SecurityRoleId
         ";

        public static List<SecurityPermission> FindBySecurityRoleId(int? securityRoleId, IDbConnection connection)
        {

            List<SecurityPermission> rv = new List<SecurityPermission>();

            if (!securityRoleId.HasValue)
                return rv;

            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, null))
            {
                Database.PutParameter(dbCommand, "?SecurityRoleId", securityRoleId.Value);

                using(IDataReader dataReader = dbCommand.ExecuteReader())
                {

                    while(dataReader.Read())
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
      