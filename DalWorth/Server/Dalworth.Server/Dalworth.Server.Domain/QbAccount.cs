using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  
namespace Dalworth.Server.Domain
{
    public partial class QbAccount
    {
        public QbAccount()
        {

        }

        private bool m_isDefault;
        public bool IsDefault
        {
            get { return m_isDefault; }
            set { m_isDefault = value; }
        }

        private const string SqlFindByProjectType =
            @"select acc.*,ptacc.IsDefault
                from projecttypeqbaccount ptacc
                join qbaccount acc on ptacc.qbaccountlistid = acc.listid
                where ptacc.projecttypeid = ?ProjectTypeId";

        public static List<QbAccount> FindByProjectType(ProjectTypeEnum projectType, IDbConnection connection)
        {
            return FindByProjectType(projectType, false, connection);
        }

        public static List<QbAccount> FindByProjectType(ProjectTypeEnum projectType, bool defaultOnly, IDbConnection connection)
        {
            string sql = SqlFindByProjectType;

            if (defaultOnly)
            {
                sql += " and ptacc.isDefault = true";
            }
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProjectType, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectTypeId", (int)projectType);

                List<QbAccount> rv = new List<QbAccount>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        QbAccount qbAccount = Load(dataReader);
                        qbAccount.IsDefault = dataReader.GetBoolean(QbAccount.FieldsCount);
                        rv.Add(qbAccount);
                    }
                }

                return rv;
            }

        }


    }
}
      