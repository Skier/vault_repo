using System;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class BackgroundJobPending
    {
        public BackgroundJobPending(){ }

        #region BackgroundJobType

        [XmlIgnore]
        public BackgroundJobTypeEnum BackgroundJobType
        {
            get { return (BackgroundJobTypeEnum)m_backgroundJobTypeId; }
            set { m_backgroundJobTypeId = (int)value; }
        }

        #endregion

        #region DeleteByProjectId

        private const String SqlDeleteByProjectId = 
            "Delete From BackgroundJobPending  Where ProjectId = ?ProjectId";

        public static void DeleteByProjectId(int projectId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByProjectId, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectId", projectId);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region DeleteByInvitation

        private const String SqlDeleteByInvitation =
            "Delete From BackgroundJobPending  Where PartnerInvitationKey = ?PartnerInvitationKey";

        public static void DeleteByInvitation(string invitationKey, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByInvitation, connection))
            {
                Database.PutParameter(dbCommand, "?PartnerInvitationKey", invitationKey);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

    }
}
      