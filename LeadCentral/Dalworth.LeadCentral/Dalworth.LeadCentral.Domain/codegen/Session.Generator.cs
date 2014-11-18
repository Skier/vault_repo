
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class Session : ICloneable
    {

        #region Store


        #region Save

        public static Session Save(Session session, IDbConnection connection)
        {
        	if (!Exists(session, connection))
        		Insert(session, connection);
        	else
        		Update(session, connection);
        	return session;
        }

        public static Session Save(Session session)
        {
        	if (!Exists(session))
        		Insert(session);
        	else
        		Update(session);
        	return session;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Session ( " +
        
          " CustomerId, " +
        
          " QbUserId, " +
        
          " Ticket, " +
        
          " AppToken, " +
        
          " IntuitTicket, " +
        
          " SessionStart, " +
        
          " IsActive " +
        
        ") Values (" +
        
          " ?CustomerId, " +
        
          " ?QbUserId, " +
        
          " ?Ticket, " +
        
          " ?AppToken, " +
        
          " ?IntuitTicket, " +
        
          " ?SessionStart, " +
        
          " ?IsActive " +
        
        ")";

        public static void Insert(Session session, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CustomerId", session.CustomerId);
            
            	Database.PutParameter(dbCommand,"?QbUserId", session.QbUserId);
            
            	Database.PutParameter(dbCommand,"?Ticket", session.Ticket);
            
            	Database.PutParameter(dbCommand,"?AppToken", session.AppToken);
            
            	Database.PutParameter(dbCommand,"?IntuitTicket", session.IntuitTicket);
            
            	Database.PutParameter(dbCommand,"?SessionStart", session.SessionStart);
            
            	Database.PutParameter(dbCommand,"?IsActive", session.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		session.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Session session)
        {
          	Insert(session, null);
        }

        public static void Insert(List<Session>  sessionList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Session session in  sessionList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CustomerId", session.CustomerId);
                
                  	Database.PutParameter(dbCommand,"?QbUserId", session.QbUserId);
                
                  	Database.PutParameter(dbCommand,"?Ticket", session.Ticket);
                
                  	Database.PutParameter(dbCommand,"?AppToken", session.AppToken);
                
                  	Database.PutParameter(dbCommand,"?IntuitTicket", session.IntuitTicket);
                
                  	Database.PutParameter(dbCommand,"?SessionStart", session.SessionStart);
                
                  	Database.PutParameter(dbCommand,"?IsActive", session.IsActive);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CustomerId",session.CustomerId);
                
                	Database.UpdateParameter(dbCommand,"?QbUserId",session.QbUserId);
                
                	Database.UpdateParameter(dbCommand,"?Ticket",session.Ticket);
                
                	Database.UpdateParameter(dbCommand,"?AppToken",session.AppToken);
                
                	Database.UpdateParameter(dbCommand,"?IntuitTicket",session.IntuitTicket);
                
                	Database.UpdateParameter(dbCommand,"?SessionStart",session.SessionStart);
                
                	Database.UpdateParameter(dbCommand,"?IsActive",session.IsActive);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	session.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Session>  sessionList)
        {
        	Insert(sessionList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Session Set "
          
            + " CustomerId = ?CustomerId, "
          
            + " QbUserId = ?QbUserId, "
          
            + " Ticket = ?Ticket, "
          
            + " AppToken = ?AppToken, "
          
            + " IntuitTicket = ?IntuitTicket, "
          
            + " SessionStart = ?SessionStart, "
          
            + " IsActive = ?IsActive "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Session session, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", session.Id);
            
            	Database.PutParameter(dbCommand,"?CustomerId", session.CustomerId);
            
            	Database.PutParameter(dbCommand,"?QbUserId", session.QbUserId);
            
            	Database.PutParameter(dbCommand,"?Ticket", session.Ticket);
            
            	Database.PutParameter(dbCommand,"?AppToken", session.AppToken);
            
            	Database.PutParameter(dbCommand,"?IntuitTicket", session.IntuitTicket);
            
            	Database.PutParameter(dbCommand,"?SessionStart", session.SessionStart);
            
            	Database.PutParameter(dbCommand,"?IsActive", session.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Session session)
        {
          	Update(session, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CustomerId, "
        
          + " QbUserId, "
        
          + " Ticket, "
        
          + " AppToken, "
        
          + " IntuitTicket, "
        
          + " SessionStart, "
        
          + " IsActive "
        
          + " From Session "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Session FindByPrimaryKey(
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

            throw new DataNotFoundException("Session not found, search by primary key");
        }

        public static Session FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Session session, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",session.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Session session)
        {
        	return Exists(session, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Session limit 1";

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

        public static Session Load(IDataReader dataReader, int offset)
        {
              Session session = new Session();

              session.Id = dataReader.GetInt32(0 + offset);
                  session.CustomerId = dataReader.GetInt32(1 + offset);
                  session.QbUserId = dataReader.GetString(2 + offset);
                  session.Ticket = dataReader.GetString(3 + offset);
                  session.AppToken = dataReader.GetString(4 + offset);
                  session.IntuitTicket = dataReader.GetString(5 + offset);
                  session.SessionStart = dataReader.GetDateTime(6 + offset);
                  session.IsActive = dataReader.GetBoolean(7 + offset);
                  

            return session;
        }

        public static Session Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Session "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Session session, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", session.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Session session)
        {
        	Delete(session, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Session ";

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
              
                + " QbUserId, "
              
                + " Ticket, "
              
                + " AppToken, "
              
                + " IntuitTicket, "
              
                + " SessionStart, "
              
                + " IsActive "
              
                + " From Session ";

        public static List<Session> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Session> rv = new List<Session>();

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

        public static List<Session> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Session> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Session> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Session));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Session item in itemsList)
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

        public static List<Session> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Session));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Session> itemsList = new List<Session>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Session)
              		itemsList.Add(deserializedObject as Session);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_customerId;
              
        protected String m_qbUserId;
              
        protected String m_ticket;
              
        protected String m_appToken;
              
        protected String m_intuitTicket;
              
        protected DateTime m_sessionStart;
              
        protected bool m_isActive;
              
        #endregion

        #region Constructors

        public Session(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Session(
                int 
                  id,int 
                  customerId,String 
                  qbUserId,String 
                  ticket,String 
                  appToken,String 
                  intuitTicket,DateTime 
                  sessionStart,bool 
                  isActive
                ) : this()
        {
            
        	m_id = id;
            
        	m_customerId = customerId;
            
        	m_qbUserId = qbUserId;
            
        	m_ticket = ticket;
            
        	m_appToken = appToken;
            
        	m_intuitTicket = intuitTicket;
            
        	m_sessionStart = sessionStart;
            
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
        
        public String QbUserId
        {
        	get { return m_qbUserId;}
            set { m_qbUserId = value; }
        }
        
        public String Ticket
        {
        	get { return m_ticket;}
            set { m_ticket = value; }
        }
        
        public String AppToken
        {
        	get { return m_appToken;}
            set { m_appToken = value; }
        }
        
        public String IntuitTicket
        {
        	get { return m_intuitTicket;}
            set { m_intuitTicket = value; }
        }
        
        public DateTime SessionStart
        {
        	get { return m_sessionStart;}
            set { m_sessionStart = value; }
        }
        
        public bool IsActive
        {
        	get { return m_isActive;}
            set { m_isActive = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 8; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    