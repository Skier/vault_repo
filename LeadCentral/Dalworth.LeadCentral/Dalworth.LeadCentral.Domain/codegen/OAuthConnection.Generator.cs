
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class OAuthConnection : ICloneable
    {

        #region Store


        #region Save

        public static OAuthConnection Save(OAuthConnection oAuthConnection, IDbConnection connection)
        {
        	if (!Exists(oAuthConnection, connection))
        		Insert(oAuthConnection, connection);
        	else
        		Update(oAuthConnection, connection);
        	return oAuthConnection;
        }

        public static OAuthConnection Save(OAuthConnection oAuthConnection)
        {
        	if (!Exists(oAuthConnection))
        		Insert(oAuthConnection);
        	else
        		Update(oAuthConnection);
        	return oAuthConnection;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into OAuthConnection ( " +
        
          " CustomerId, " +
        
          " ParentConsumerKey, " +
        
          " RequestTokenUrl, " +
        
          " DynamicKeyRetrievalUrl, " +
        
          " AccessTokenUrl, " +
        
          " AuthorizeRequestUrl, " +
        
          " ConsumerKey, " +
        
          " ConsumerSecret, " +
        
          " AccessToken, " +
        
          " AccessTokenSecret, " +
        
          " DateCreated, " +
        
          " IsActive " +
        
        ") Values (" +
        
          " ?CustomerId, " +
        
          " ?ParentConsumerKey, " +
        
          " ?RequestTokenUrl, " +
        
          " ?DynamicKeyRetrievalUrl, " +
        
          " ?AccessTokenUrl, " +
        
          " ?AuthorizeRequestUrl, " +
        
          " ?ConsumerKey, " +
        
          " ?ConsumerSecret, " +
        
          " ?AccessToken, " +
        
          " ?AccessTokenSecret, " +
        
          " ?DateCreated, " +
        
          " ?IsActive " +
        
        ")";

        public static void Insert(OAuthConnection oAuthConnection, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CustomerId", oAuthConnection.CustomerId);
            
            	Database.PutParameter(dbCommand,"?ParentConsumerKey", oAuthConnection.ParentConsumerKey);
            
            	Database.PutParameter(dbCommand,"?RequestTokenUrl", oAuthConnection.RequestTokenUrl);
            
            	Database.PutParameter(dbCommand,"?DynamicKeyRetrievalUrl", oAuthConnection.DynamicKeyRetrievalUrl);
            
            	Database.PutParameter(dbCommand,"?AccessTokenUrl", oAuthConnection.AccessTokenUrl);
            
            	Database.PutParameter(dbCommand,"?AuthorizeRequestUrl", oAuthConnection.AuthorizeRequestUrl);
            
            	Database.PutParameter(dbCommand,"?ConsumerKey", oAuthConnection.ConsumerKey);
            
            	Database.PutParameter(dbCommand,"?ConsumerSecret", oAuthConnection.ConsumerSecret);
            
            	Database.PutParameter(dbCommand,"?AccessToken", oAuthConnection.AccessToken);
            
            	Database.PutParameter(dbCommand,"?AccessTokenSecret", oAuthConnection.AccessTokenSecret);
            
            	Database.PutParameter(dbCommand,"?DateCreated", oAuthConnection.DateCreated);
            
            	Database.PutParameter(dbCommand,"?IsActive", oAuthConnection.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		oAuthConnection.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(OAuthConnection oAuthConnection)
        {
          	Insert(oAuthConnection, null);
        }

        public static void Insert(List<OAuthConnection>  oAuthConnectionList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(OAuthConnection oAuthConnection in  oAuthConnectionList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CustomerId", oAuthConnection.CustomerId);
                
                  	Database.PutParameter(dbCommand,"?ParentConsumerKey", oAuthConnection.ParentConsumerKey);
                
                  	Database.PutParameter(dbCommand,"?RequestTokenUrl", oAuthConnection.RequestTokenUrl);
                
                  	Database.PutParameter(dbCommand,"?DynamicKeyRetrievalUrl", oAuthConnection.DynamicKeyRetrievalUrl);
                
                  	Database.PutParameter(dbCommand,"?AccessTokenUrl", oAuthConnection.AccessTokenUrl);
                
                  	Database.PutParameter(dbCommand,"?AuthorizeRequestUrl", oAuthConnection.AuthorizeRequestUrl);
                
                  	Database.PutParameter(dbCommand,"?ConsumerKey", oAuthConnection.ConsumerKey);
                
                  	Database.PutParameter(dbCommand,"?ConsumerSecret", oAuthConnection.ConsumerSecret);
                
                  	Database.PutParameter(dbCommand,"?AccessToken", oAuthConnection.AccessToken);
                
                  	Database.PutParameter(dbCommand,"?AccessTokenSecret", oAuthConnection.AccessTokenSecret);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", oAuthConnection.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?IsActive", oAuthConnection.IsActive);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CustomerId",oAuthConnection.CustomerId);
                
                	Database.UpdateParameter(dbCommand,"?ParentConsumerKey",oAuthConnection.ParentConsumerKey);
                
                	Database.UpdateParameter(dbCommand,"?RequestTokenUrl",oAuthConnection.RequestTokenUrl);
                
                	Database.UpdateParameter(dbCommand,"?DynamicKeyRetrievalUrl",oAuthConnection.DynamicKeyRetrievalUrl);
                
                	Database.UpdateParameter(dbCommand,"?AccessTokenUrl",oAuthConnection.AccessTokenUrl);
                
                	Database.UpdateParameter(dbCommand,"?AuthorizeRequestUrl",oAuthConnection.AuthorizeRequestUrl);
                
                	Database.UpdateParameter(dbCommand,"?ConsumerKey",oAuthConnection.ConsumerKey);
                
                	Database.UpdateParameter(dbCommand,"?ConsumerSecret",oAuthConnection.ConsumerSecret);
                
                	Database.UpdateParameter(dbCommand,"?AccessToken",oAuthConnection.AccessToken);
                
                	Database.UpdateParameter(dbCommand,"?AccessTokenSecret",oAuthConnection.AccessTokenSecret);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",oAuthConnection.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?IsActive",oAuthConnection.IsActive);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	oAuthConnection.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<OAuthConnection>  oAuthConnectionList)
        {
        	Insert(oAuthConnectionList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update OAuthConnection Set "
          
            + " CustomerId = ?CustomerId, "
          
            + " ParentConsumerKey = ?ParentConsumerKey, "
          
            + " RequestTokenUrl = ?RequestTokenUrl, "
          
            + " DynamicKeyRetrievalUrl = ?DynamicKeyRetrievalUrl, "
          
            + " AccessTokenUrl = ?AccessTokenUrl, "
          
            + " AuthorizeRequestUrl = ?AuthorizeRequestUrl, "
          
            + " ConsumerKey = ?ConsumerKey, "
          
            + " ConsumerSecret = ?ConsumerSecret, "
          
            + " AccessToken = ?AccessToken, "
          
            + " AccessTokenSecret = ?AccessTokenSecret, "
          
            + " DateCreated = ?DateCreated, "
          
            + " IsActive = ?IsActive "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(OAuthConnection oAuthConnection, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", oAuthConnection.Id);
            
            	Database.PutParameter(dbCommand,"?CustomerId", oAuthConnection.CustomerId);
            
            	Database.PutParameter(dbCommand,"?ParentConsumerKey", oAuthConnection.ParentConsumerKey);
            
            	Database.PutParameter(dbCommand,"?RequestTokenUrl", oAuthConnection.RequestTokenUrl);
            
            	Database.PutParameter(dbCommand,"?DynamicKeyRetrievalUrl", oAuthConnection.DynamicKeyRetrievalUrl);
            
            	Database.PutParameter(dbCommand,"?AccessTokenUrl", oAuthConnection.AccessTokenUrl);
            
            	Database.PutParameter(dbCommand,"?AuthorizeRequestUrl", oAuthConnection.AuthorizeRequestUrl);
            
            	Database.PutParameter(dbCommand,"?ConsumerKey", oAuthConnection.ConsumerKey);
            
            	Database.PutParameter(dbCommand,"?ConsumerSecret", oAuthConnection.ConsumerSecret);
            
            	Database.PutParameter(dbCommand,"?AccessToken", oAuthConnection.AccessToken);
            
            	Database.PutParameter(dbCommand,"?AccessTokenSecret", oAuthConnection.AccessTokenSecret);
            
            	Database.PutParameter(dbCommand,"?DateCreated", oAuthConnection.DateCreated);
            
            	Database.PutParameter(dbCommand,"?IsActive", oAuthConnection.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(OAuthConnection oAuthConnection)
        {
          	Update(oAuthConnection, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CustomerId, "
        
          + " ParentConsumerKey, "
        
          + " RequestTokenUrl, "
        
          + " DynamicKeyRetrievalUrl, "
        
          + " AccessTokenUrl, "
        
          + " AuthorizeRequestUrl, "
        
          + " ConsumerKey, "
        
          + " ConsumerSecret, "
        
          + " AccessToken, "
        
          + " AccessTokenSecret, "
        
          + " DateCreated, "
        
          + " IsActive "
        
          + " From OAuthConnection "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static OAuthConnection FindByPrimaryKey(
              int id, IDbConnection connection
              )
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
              
            	Database.PutParameter(dbCommand,"?Id", id);
              

              	using(IDataReader dataReader = dbCommand.ExecuteReader())
              	{
              		if(dataReader.Read())
              			return Load(dataReader);
              	}
            }

            throw new DataNotFoundException("OAuthConnection not found, search by primary key");
        }

        public static OAuthConnection FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(OAuthConnection oAuthConnection, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",oAuthConnection.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(OAuthConnection oAuthConnection)
        {
        	return Exists(oAuthConnection, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from OAuthConnection limit 1";

            using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
            	using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
              	{
              		return reader.Read();
              	}
            }
        }

        public static bool IsContainsData()
        {
        	return IsContainsData(null);
        }

        #endregion

        #region Load

        public static OAuthConnection Load(IDataReader dataReader, int offset)
        {
              OAuthConnection oAuthConnection = new OAuthConnection();

              oAuthConnection.Id = dataReader.GetInt32(0 + offset);
                  oAuthConnection.CustomerId = dataReader.GetInt32(1 + offset);
                  oAuthConnection.ParentConsumerKey = dataReader.GetString(2 + offset);
                  oAuthConnection.RequestTokenUrl = dataReader.GetString(3 + offset);
                  oAuthConnection.DynamicKeyRetrievalUrl = dataReader.GetString(4 + offset);
                  oAuthConnection.AccessTokenUrl = dataReader.GetString(5 + offset);
                  oAuthConnection.AuthorizeRequestUrl = dataReader.GetString(6 + offset);
                  oAuthConnection.ConsumerKey = dataReader.GetString(7 + offset);
                  oAuthConnection.ConsumerSecret = dataReader.GetString(8 + offset);
                  oAuthConnection.AccessToken = dataReader.GetString(9 + offset);
                  oAuthConnection.AccessTokenSecret = dataReader.GetString(10 + offset);
                  oAuthConnection.DateCreated = dataReader.GetDateTime(11 + offset);
                  oAuthConnection.IsActive = dataReader.GetBoolean(12 + offset);
                  

            return oAuthConnection;
        }

        public static OAuthConnection Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From OAuthConnection "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(OAuthConnection oAuthConnection, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", oAuthConnection.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(OAuthConnection oAuthConnection)
        {
        	Delete(oAuthConnection, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From OAuthConnection ";

        public static void Clear(IDbConnection connection)
        {
        	using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
            {
             	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Clear()
        {
        	Clear(null);
        }

        #endregion

        #region Find

        private const String SqlSelectAll = "Select "
              
                + " Id, "
              
                + " CustomerId, "
              
                + " ParentConsumerKey, "
              
                + " RequestTokenUrl, "
              
                + " DynamicKeyRetrievalUrl, "
              
                + " AccessTokenUrl, "
              
                + " AuthorizeRequestUrl, "
              
                + " ConsumerKey, "
              
                + " ConsumerSecret, "
              
                + " AccessToken, "
              
                + " AccessTokenSecret, "
              
                + " DateCreated, "
              
                + " IsActive "
              
                + " From OAuthConnection ";

        public static List<OAuthConnection> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<OAuthConnection> rv = new List<OAuthConnection>();

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

        public static List<OAuthConnection> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<OAuthConnection> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<OAuthConnection> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(OAuthConnection));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(OAuthConnection item in itemsList)
            {
            	xmlSerializer.Serialize(xmlWriter, item);
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Flush();
            xmlWriter.Close();

            return itemsList.Count;
        }

        #endregion

        #region Load from file

        public static List<OAuthConnection> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(OAuthConnection));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<OAuthConnection> itemsList = new List<OAuthConnection>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is OAuthConnection)
              		itemsList.Add(deserializedObject as OAuthConnection);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_customerId;
              
        protected String m_parentConsumerKey;
              
        protected String m_requestTokenUrl;
              
        protected String m_dynamicKeyRetrievalUrl;
              
        protected String m_accessTokenUrl;
              
        protected String m_authorizeRequestUrl;
              
        protected String m_consumerKey;
              
        protected String m_consumerSecret;
              
        protected String m_accessToken;
              
        protected String m_accessTokenSecret;
              
        protected DateTime m_dateCreated;
              
        protected bool m_isActive;
              
        #endregion

        #region Constructors

        public OAuthConnection(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public OAuthConnection(
                int 
                  id,int 
                  customerId,String 
                  parentConsumerKey,String 
                  requestTokenUrl,String 
                  dynamicKeyRetrievalUrl,String 
                  accessTokenUrl,String 
                  authorizeRequestUrl,String 
                  consumerKey,String 
                  consumerSecret,String 
                  accessToken,String 
                  accessTokenSecret,DateTime 
                  dateCreated,bool 
                  isActive
                ) : this()
        {
            
        	m_id = id;
            
        	m_customerId = customerId;
            
        	m_parentConsumerKey = parentConsumerKey;
            
        	m_requestTokenUrl = requestTokenUrl;
            
        	m_dynamicKeyRetrievalUrl = dynamicKeyRetrievalUrl;
            
        	m_accessTokenUrl = accessTokenUrl;
            
        	m_authorizeRequestUrl = authorizeRequestUrl;
            
        	m_consumerKey = consumerKey;
            
        	m_consumerSecret = consumerSecret;
            
        	m_accessToken = accessToken;
            
        	m_accessTokenSecret = accessTokenSecret;
            
        	m_dateCreated = dateCreated;
            
        	m_isActive = isActive;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int CustomerId
        {
        	get { return m_customerId;}
            set { m_customerId = value; }
        }
        
        public String ParentConsumerKey
        {
        	get { return m_parentConsumerKey;}
            set { m_parentConsumerKey = value; }
        }
        
        public String RequestTokenUrl
        {
        	get { return m_requestTokenUrl;}
            set { m_requestTokenUrl = value; }
        }
        
        public String DynamicKeyRetrievalUrl
        {
        	get { return m_dynamicKeyRetrievalUrl;}
            set { m_dynamicKeyRetrievalUrl = value; }
        }
        
        public String AccessTokenUrl
        {
        	get { return m_accessTokenUrl;}
            set { m_accessTokenUrl = value; }
        }
        
        public String AuthorizeRequestUrl
        {
        	get { return m_authorizeRequestUrl;}
            set { m_authorizeRequestUrl = value; }
        }
        
        public String ConsumerKey
        {
        	get { return m_consumerKey;}
            set { m_consumerKey = value; }
        }
        
        public String ConsumerSecret
        {
        	get { return m_consumerSecret;}
            set { m_consumerSecret = value; }
        }
        
        public String AccessToken
        {
        	get { return m_accessToken;}
            set { m_accessToken = value; }
        }
        
        public String AccessTokenSecret
        {
        	get { return m_accessTokenSecret;}
            set { m_accessTokenSecret = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public bool IsActive
        {
        	get { return m_isActive;}
            set { m_isActive = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 13; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    