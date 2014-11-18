using System;
using System.Data;
using System.Threading;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class Message 
    {
        public Message() {}

        #region MessageType

        [XmlIgnore]
        public MessageTypeEnum MessageType
        {
            get { return (MessageTypeEnum) m_messageTypeId; }
            set { m_messageTypeId = (int) value; }
        }

        #endregion        

        #region FindBy

        private const string SqlFindByEmployee =
            @"SELECT *
            FROM Message
                WHERE EmployeeId = ?EmployeeId";

        public static Message FindBy(int employeeId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEmployee, connection))
            {
                Database.PutParameter(dbCommand, "?EmployeeId", employeeId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        return Load(dataReader);
                    }
                }
            }

            return null;
        }

        #endregion        

        #region FindBy

        private const string SqlRemoveMessage =
            @"DELETE
            FROM Message
                WHERE EmployeeId = ?EmployeeId
                and MessageTypeId = ?MessageTypeId
                and VisitId = ?VisitId";

        public static void RemoveMessage(int employeeId, int visitId, MessageTypeEnum messageType)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlRemoveMessage))
            {
                Database.PutParameter(dbCommand, "?EmployeeId", employeeId);
                Database.PutParameter(dbCommand, "?MessageTypeId", (int)messageType);
                Database.PutParameter(dbCommand, "?VisitId", visitId);

                dbCommand.ExecuteNonQuery();
            }            
        }

        #endregion        
    }
}
      