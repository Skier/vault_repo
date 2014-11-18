using System;
using System.Collections.Generic;
using System.Text;
using Weborb.Security;

namespace Weborb.Data.Inspector2
{
    public class DatabaseInfo
    {
        private const string KEY = "1234567890";
        private static byte[] IVBYTES = new byte[] { 2, 0, 0, 1, 1, 2, 0, 1 };
        public DatabaseInfoType type;
        public string hostname;
        public string port;
        public string username;
        public string id;
        public string password;

        private IInspector inspector;

        public DatabaseInfo()
        {
        }

        public DatabaseInfo(DatabaseInfoType type, string hostname, string port, string username, string password )
        {
            this.type = type;
            this.hostname = hostname;
            this.port = port;
            this.username = username;
            this.password = CryptUtils.encrypt( password, KEY, IVBYTES );
            this.id = Guid.NewGuid().ToString();;
        }

        public IInspector GetInspector()
        {
            if (inspector == null)
                inspector = CreateInspector( this );

            return inspector;
        }

        public string Ping()
        {
            IInspector inspector = CreateInspector( this );
            return inspector.Ping();
        }

        private static IInspector CreateInspector( DatabaseInfo dbInfo )
        {
            switch (dbInfo.type)
            {
                case DatabaseInfoType.MYSQL:
                    return new Weborb.Data.Inspector2.MySQL.Inspector(dbInfo.hostname, dbInfo.port, dbInfo.username, CryptUtils.decrypt(dbInfo.password, KEY, IVBYTES));

                case DatabaseInfoType.MSSQL:
                    return new Weborb.Data.Inspector2.MSSQL.Inspector(dbInfo.hostname, dbInfo.username, CryptUtils.decrypt(dbInfo.password, KEY, IVBYTES));

                case DatabaseInfoType.ORACLE:
                    return new Weborb.Data.Inspector2.Oracle.Inspector(dbInfo.hostname, dbInfo.username, CryptUtils.decrypt(dbInfo.password, KEY, IVBYTES));

                case DatabaseInfoType.POSTGRESQL:
                    return new Weborb.Data.Inspector2.PostgreSQL.Inspector(dbInfo.hostname, dbInfo.port, dbInfo.username, CryptUtils.decrypt(dbInfo.password, KEY, IVBYTES));
            }

            throw new Exception( "unknown database type" );
        }
    }

    public enum DatabaseInfoType
    {
        MYSQL,
        MSSQL,
        ORACLE,
        POSTGRESQL
    }
}
