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
    public partial class QbTemplate
    {
        public QbTemplate()
        {

        }

        #region IsDefault 

        private bool m_isDefault;
        public bool IsDefault
        {
            get { return m_isDefault; }
            set { m_isDefault = value; }
        }

        #endregion 

        #region FindByProjectType

        private const string SqlFindByProjectType =
            @"select t.*, ptt.isdefault
                from qbtemplate t
                join projecttypeqbtemplate ptt on ptt.qbtemplatelistid = t.listid
                where ptt.projecttypeid = ?ProjectTypeId";

        public static List<QbTemplate> FindByProjectType(ProjectTypeEnum projectType, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProjectType, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectTypeId", (int)projectType);

                List<QbTemplate> rv = new List<QbTemplate>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        QbTemplate qbTemplate = Load(dataReader);
                        qbTemplate.IsDefault = dataReader.GetBoolean(QbAccount.FieldsCount);
                        rv.Add(qbTemplate);
                    }
                }

                return rv;
            }

        }

        #endregion
    }
}
      