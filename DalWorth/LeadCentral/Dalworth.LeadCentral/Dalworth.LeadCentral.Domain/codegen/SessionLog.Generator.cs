
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class SessionLog : ICloneable
    {

        #region Store


        #region Save

        public static SessionLog Save(SessionLog sessionLog, IDbConnection connection)
        {
        	if (!Exists(sessionLog, connection))
        		Insert(sessionLog, connection);
        	else
        		Update(sessionLog, connection);
        	return sessionLog;
        }

        public static SessionLog Save(SessionLog sessionLog)
        {
        	if (!Exists(sessionLog))
        		Insert(sessionLog);
        	else
        		Update(sessionLog);
        	return sessionLog;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into SessionLog ( " +
        
          " SessionId, " +
        
          " DateLog, " +
        
          " Description, " +
        
          " UserAgent, " +
        
          " RemoteAddress " +
        
        ") Values (" +
        
          " ?SessionId, " +
        
          " ?DateLog, " +
        
          " ?Description, " +
        
          " ?UserAgent, " +
        
          " ?RemoteAddress " +
        
        ")";

        public static void Insert(SessionLog sessionLog, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?SessionId", sessionLog.SessionId);
            
            	Database.PutParameter(dbCommand,"?DateLog", sessionLog.DateLog);
            
            	Database.PutParameter(dbCommand,"?Description", sessionLog.Description);
            
            	Database.PutParameter(dbCommand,"?UserAgent", sessionLog.UserAgent);
            
            	Database.PutParameter(dbCommand,"?RemoteAddress", sessionLog.RemoteAddress);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		sessionLog.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(SessionLog sessionLog)
        {
          	Insert(sessionLog, null);
        }

        public static void Insert(List<SessionLog>  sessionLogList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(SessionLog sessionLog in  sessionLogList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?SessionId", sessionLog.SessionId);
                
                  	Database.PutParameter(dbCommand,"?DateLog", sessionLog.DateLog);
                
                  	Database.PutParameter(dbCommand,"?Description", sessionLog.Description);
                
                  	Database.PutParameter(dbCommand,"?UserAgent", sessionLog.UserAgent);
                
                  	Database.PutParameter(dbCommand,"?RemoteAddress", sessionLog.RemoteAddress);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?SessionId",sessionLog.SessionId);
                
                	Database.UpdateParameter(dbCommand,"?DateLog",sessionLog.DateLog);
                
                	Database.UpdateParameter(dbCommand,"?Description",sessionLog.Description);
                
                	Database.UpdateParameter(dbCommand,"?UserAgent",sessionLog.UserAgent);
                
                	Database.UpdateParameter(dbCommand,"?RemoteAddress",sessionLog.RemoteAddress);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	sessionLog.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<SessionLog>  sessionLogList)
        {
        	Insert(sessionLogList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update SessionLog Set "
          
            + " SessionId = ?SessionId, "
          
            + " DateLog = ?DateLog, "
          
            + " Description = ?Description, "
          
            + " UserAgent = ?UserAgent, "
          
            + " RemoteAddress = ?RemoteAddress "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(SessionLog sessionLog, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", sessionLog.Id);
            
            	Database.PutParameter(dbCommand,"?SessionId", sessionLog.SessionId);
            
            	Database.PutParameter(dbCommand,"?DateLog", sessionLog.DateLog);
            
            	Database.PutParameter(dbCommand,"?Description", sessionLog.Description);
            
            	Database.PutParameter(dbCommand,"?UserAgent", sessionLog.UserAgent);
            
            	Database.PutParameter(dbCommand,"?RemoteAddress", sessionLog.RemoteAddress);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(SessionLog sessionLog)
        {
          	Update(sessionLog, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " SessionId, "
        
          + " DateLog, "
        
          + " Description, "
        
          + " UserAgent, "
        
          + " RemoteAddress "
        
          + " From SessionLog "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static SessionLog FindByPrimaryKey(
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

            throw new DataNotFoundException("SessionLog not found, search by primary key");
        }

        public static SessionLog FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(SessionLog sessionLog, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",sessionLog.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(SessionLog sessionLog)
        {
        	return Exists(sessionLog, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from SessionLog limit 1";

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

        public static SessionLog Load(IDataReader dataReader, int offset)
        {
              SessionLog sessionLog = new SessionLog();

              sessionLog.Id = dataReader.GetInt32(0 + offset);
                  sessionLog.SessionId = dataReader.GetInt32(1 + offset);
                  sessionLog.DateLog = dataReader.GetDateTime(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    sessionLog.Description = dataReader.GetString(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    sessionLog.UserAgent = dataReader.GetString(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    sessionLog.RemoteAddress = dataReader.GetString(5 + offset);
                  

            return sessionLog;
        }

        public static SessionLog Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From SessionLog "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(SessionLog sessionLog, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", sessionLog.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(SessionLog sessionLog)
        {
        	Delete(sessionLog, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From SessionLog ";

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
              
                + " SessionId, "
              
                + " DateLog, "
              
                + " Description, "
              
                + " UserAgent, "
              
                + " RemoteAddress "
              
                + " From SessionLog ";

        public static List<SessionLog> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<SessionLog> rv = new List<SessionLog>();

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

        public static List<SessionLog> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<SessionLog> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<SessionLog> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SessionLog));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(SessionLog item in itemsList)
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

        public static List<SessionLog> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(SessionLog));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<SessionLog> itemsList = new List<SessionLog>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is SessionLog)
              		itemsList.Add(deserializedObject as SessionLog);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_sessionId;
              
        protected DateTime m_dateLog;
              
        protected String m_description;
              
        protected String m_userAgent;
              
        protected String m_remoteAddress;
              
        #endregion

        #region Constructors

        public SessionLog(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public SessionLog(
                int 
                  id,int 
                  sessionId,DateTime 
                  dateLog,String 
                  description,String 
                  userAgent,String 
                  remoteAddress
                ) : this()
        {
            
        	m_id = id;
            
        	m_sessionId = sessionId;
            
        	m_dateLog = dateLog;
            
        	m_description = description;
            
        	m_userAgent = userAgent;
            
        	m_remoteAddress = remoteAddress;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int SessionId
        {
        	get { return m_sessionId;}
            set { m_sessionId = value; }
        }
        
        public DateTime DateLog
        {
        	get { return m_dateLog;}
            set { m_dateLog = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        
        public String UserAgent
        {
        	get { return m_userAgent;}
            set { m_userAgent = value; }
        }
        
        public String RemoteAddress
        {
        	get { return m_remoteAddress;}
            set { m_remoteAddress = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 6; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    