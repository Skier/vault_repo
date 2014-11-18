using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Collections;
using Weborb.Data.Inspector2.Common;


using Microsoft.Win32;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Security;

namespace Weborb.Data.Inspector2
{
    [WebService(Namespace = "http://themidnightcoders.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Service : System.Web.Services.WebService
    {
        private const string DATABASES_KEY = "databases";

        public Service()
        {
            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
           // InitializeDatabases();
            Hashtable databases = (Hashtable)Application[ Service.DATABASES_KEY ];

            if (databases == null)
            {
                databases = new Hashtable();
                Application[Service.DATABASES_KEY] = databases;

            }
        }

        [WebMethod]
        public void Ping()
        {
        }

        [WebMethod]
        public DatabaseInfoType[] GetSupportedDatabaseTypes()
        {
            return new DatabaseInfoType[] { 
                DatabaseInfoType.MYSQL, 
                DatabaseInfoType.MSSQL,
                DatabaseInfoType.ORACLE,
                DatabaseInfoType.POSTGRESQL};
        }

        [WebMethod]
        public string CheckDatabaseHost( DatabaseInfoType type, string hostname, string port, string userid, string password )
        {
            DatabaseInfo dbInfo = new DatabaseInfo( type, hostname, port, userid, password );
            return dbInfo.Ping();
        }

        [WebMethod]
        public DatabaseInfo AddDatabaseHost( DatabaseInfoType type, string hostname, string port, string userid, string password )
        {
            DatabaseInfo dbInfo = new DatabaseInfo( type, hostname, port, userid, password );

            String result = dbInfo.Ping();

            if( result.Equals(BaseInspector.SUCCESS_CODE) )
            {
                Hashtable databases = (Hashtable)Application[ Service.DATABASES_KEY ];
                databases[ dbInfo.id ] = dbInfo;
                return dbInfo;
            }
            else
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                System.Xml.XmlNode node = doc.CreateNode( XmlNodeType.Element,
                                                            SoapException.DetailElementName.Name,
                                                            SoapException.DetailElementName.Namespace );
                node.AppendChild( doc.CreateTextNode( result ) );
                throw new SoapException( result, new XmlQualifiedName( "error" ), "server", node );
            }
        }

        [WebMethod]
        public DatabaseInfo[] GetDatabaseHosts()
        {
            Hashtable databases = (Hashtable) Application[ Service.DATABASES_KEY ];
            ArrayList list = new ArrayList( databases.Values );
            return (DatabaseInfo[])list.ToArray( typeof( DatabaseInfo ) );
        }

        [WebMethod]
        public String[] GetDatabases( String hostId )
        {
            Hashtable databases = (Hashtable)Application[ Service.DATABASES_KEY ];
            DatabaseInfo dbInfo = (DatabaseInfo)databases[ hostId ];
            IInspector inspector = dbInfo.GetInspector();
            return inspector.GetDatabases();
        }

        [WebMethod]
        public QueryResult TestQuery( String hostId, String database, String query )
        {
            Hashtable databases = (Hashtable)Application[ Service.DATABASES_KEY ];
            DatabaseInfo dbInfo = (DatabaseInfo)databases[ hostId ];
            IInspector inspector = dbInfo.GetInspector();
            return inspector.TestQuery( database, query );
        }

        [WebMethod]
        public String[] GetTables( String hostId, String database )
        {
            Hashtable databases = (Hashtable)Application[ Service.DATABASES_KEY ];
            DatabaseInfo dbInfo = (DatabaseInfo)databases[hostId];
            IInspector inspector = dbInfo.GetInspector();
            return inspector.GetTables( database );
        }

        [WebMethod]
        public ColumnInfo[] GetColumns( String hostId, String database, String table )
        {
            Hashtable databases = (Hashtable)Application[ Service.DATABASES_KEY ];
            DatabaseInfo dbInfo = (DatabaseInfo)databases[ hostId ];
            IInspector inspector = dbInfo.GetInspector();
            return inspector.GetColumns( database, table );
        }

        [WebMethod]
        public void GenerateSourceCode(String hostId, String database, String table, String query)
        {
            Hashtable databases = (Hashtable)Application[Service.DATABASES_KEY];
            DatabaseInfo dbInfo = (DatabaseInfo)databases[hostId];
            IInspector inspector = dbInfo.GetInspector();
            string destination = HttpContext.Current.Request.PhysicalApplicationPath;
            inspector.GenerateSourceCode(destination, database, table, query);
            inspector.CompileGeneratedSources(destination, table);
        }

        private void InitializeDatabases()
        {
            // load config file
            // get all database connections from config
            // for each database, create a connection and get database schema
            // cache databases
            //InspectLocalhost();
            //InspectHostsFromConfig();
        }

/*
        private void InspectLocalhost()
        {
            SqlConnection connection = new SqlConnection();
            connection.GetSchema();

        }
 * 
        private void InspectHostsFromConfig()
        {
        }
*/
    }
}