using System;
using System.ComponentModel;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class ProjectConstructionScope
    {
        public ProjectConstructionScope()
        {
            
        }

        #region ScopeType

        public ProjectConstructionScopeTypeEnum ScopeType
        {
            get { return (ProjectConstructionScopeTypeEnum)m_projectConstructionScopeTypeId; }
            set { m_projectConstructionScopeTypeId = (int)value; }
        }

        #endregion

        #region ScopeTypeText

        public string ScopeTypeText
        {
            get
            {
                return ProjectConstructionScopeType.GetText(ScopeType);
            }
        }

        #endregion

        #region StatusImageIndex

        public int StatusImageIndex
        {
            get
            {
                int result = 0;
                
                if (IsVoided)
                    result = 1;
                else if (ScopeType == ProjectConstructionScopeTypeEnum.ScopeEstimate)
                    result = 2;
                
                return result;
            }
        }

        #endregion

        #region FindByProject

        private const String SqlFindByProject =
                @"SELECT * 
                    FROM projectconstructionscope 
                   WHERE ProjectId = ?ProjectId";

        public static BindingList<ProjectConstructionScope> FindByProject(int projectId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProject))
            {
                Database.PutParameter(dbCommand, "?ProjectId", projectId);

                BindingList<ProjectConstructionScope> result = new BindingList<ProjectConstructionScope>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }

                }
                return result;
            }

        }

        #endregion

    }
}
      