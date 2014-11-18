using System;
using System.Data;
using MobileTech.Data;

namespace MobileTech.Domain
{
    public partial class Session
    {
        public const long NULL_ID = 0;
        public static Session NullSession = new Session(NULL_ID, true);


        #region Constructors

        public Session()
        {

        }

        #endregion

        #region Extra fields

        public bool IsNull
        {
            get
            {
                return m_sessionId == NULL_ID;
            }
        }

        private static Session s_active;
        public static Session ActiveSession
        {
            get
            {
                if (s_active == null)
                    s_active = Session.FindActive();

                return s_active;
            }

            set
            { s_active = value; }
        }

        #endregion

        #region Finders

        #region SqlStatements
        private const String SqlFindActive = "Select SessionId,Active From Session Where Active = 1 and SessionId != 0";
        private const String SqlFindNull = "Select SessionId,Active From Session Where SessionId = 0";
        #endregion

        public static Session FindActive()
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindActive);

            using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (dataReader.Read())
                    return Load(dataReader);

            }

            return null;
        }

        public static Session FindCurrent()
        {
            Session session = FindActive();

            if (session == null)
                session = FindNull();


            return session;
        }

        public static Session FindNull()
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindNull);

            using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (dataReader.Read())
                    return Load(dataReader);

            }

            Insert(Session.NullSession);

            using (IDataReader dataReader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (dataReader.Read())
                    return Load(dataReader);

            }

            throw new MobileTechException("Null Session find error");
        }

        #endregion

        public static void Reset()
        {
            s_active = null;
        }
    }
}
