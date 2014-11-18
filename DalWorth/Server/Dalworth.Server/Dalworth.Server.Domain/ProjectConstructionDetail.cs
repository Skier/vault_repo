using System;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class ProjectConstructionDetail
    {
        public ProjectConstructionDetail(){ }

        #region ProjectConstructionProgress

        public ProjectConstructionProgressEnum ProjectConstructionProgress
        {
            get { return (ProjectConstructionProgressEnum)m_projectConstructionProgressId; }
            set { m_projectConstructionProgressId = (int)value; }
        }

        #endregion

        #region FindBy

        private const String SqlFindByJobNumber =
                @"Select * from ProjectConstructionDetail
                where JobNumber = ?JobNumber";

        public static ProjectConstructionDetail FindByJobNumber(string jobNumber)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByJobNumber))
            {
                Database.PutParameter(dbCommand, "?JobNumber", jobNumber);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return Load(dataReader);
                    }

                }
                return null;
            }

        }

        #endregion

        #region ConstructionDamageType

        [XmlIgnore]
        public ConstructionDamageTypeEnum? ConstructionDamageType
        {
            get { return (ConstructionDamageTypeEnum?)m_constructionDamageTypeId; }
            set { m_constructionDamageTypeId = (int?)value; }
        }

        #endregion
    }
}
      