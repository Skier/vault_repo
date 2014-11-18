
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class ServmanSession : ICloneable
    {

        #region Store


        #region Save

        public static ServmanSession Save(ServmanSession servmanSession, IDbConnection connection)
        {
        	if (!Exists(servmanSession, connection))
        		Insert(servmanSession, connection);
        	else
        		Update(servmanSession, connection);
        	return servmanSession;
        }

        public static ServmanSession Save(ServmanSession servmanSession)
        {
        	if (!Exists(servmanSession))
        		Insert(servmanSession);
        	else
        		Update(servmanSession);
        	return servmanSession;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into ServmanSession ( " +
        
          " Id, " +
        
          " ServmanCustomerId, " +
        
          " QbUserId, " +
        
          " Ticket, " +
        
          " AppToken, " +
        
          " IntuitTicket, " +
        
          " SessionStart, " +
        
          " IsActive " +
        
        ") Values (" +
        
          " ?Id, " +
        
          " ?ServmanCustomerId, " +
        
          " ?QbUserId, " +
        
          " ?Ticket, " +
        
          " ?AppToken, " +
        
          " ?IntuitTicket, " +
        
          " ?SessionStart, " +
        
          " ?IsActive " +
        
        ")";

        public static void Insert(ServmanSession servmanSession, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", servmanSession.Id);
            
            	Database.PutParameter(dbCommand,"?ServmanCustomerId", servmanSession.ServmanCustomerId);
            
            	Database.PutParameter(dbCommand,"?QbUserId", servmanSession.QbUserId);
            
            	Database.PutParameter(dbCommand,"?Ticket", servmanSession.Ticket);
            
            	Database.PutParameter(dbCommand,"?AppToken", servmanSession.AppToken);
            
            	Database.PutParameter(dbCommand,"?IntuitTicket", servmanSession.IntuitTicket);
            
            	Database.PutParameter(dbCommand,"?SessionStart", servmanSession.SessionStart);
            
            	Database.PutParameter(dbCommand,"?IsActive", servmanSession.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            
            }
        }

        public static void Insert(ServmanSession servmanSession)
        {
          	Insert(servmanSession, null);
        }

        public static void Insert(List<ServmanSession>  servmanSessionList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(ServmanSession servmanSession in  servmanSessionList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?Id", servmanSession.Id);
                
                  	Database.PutParameter(dbCommand,"?ServmanCustomerId", servmanSession.ServmanCustomerId);
                
                  	Database.PutParameter(dbCommand,"?QbUserId", servmanSession.QbUserId);
                
                  	Database.PutParameter(dbCommand,"?Ticket", servmanSession.Ticket);
                
                  	Database.PutParameter(dbCommand,"?AppToken", servmanSession.AppToken);
                
                  	Database.PutParameter(dbCommand,"?IntuitTicket", servmanSession.IntuitTicket);
                
                  	Database.PutParameter(dbCommand,"?SessionStart", servmanSession.SessionStart);
                
                  	Database.PutParameter(dbCommand,"?IsActive", servmanSession.IsActive);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?Id",servmanSession.Id);
                
                	Database.UpdateParameter(dbCommand,"?ServmanCustomerId",servmanSession.ServmanCustomerId);
                
                	Database.UpdateParameter(dbCommand,"?QbUserId",servmanSession.QbUserId);
                
                	Database.UpdateParameter(dbCommand,"?Ticket",servmanSession.Ticket);
                
                	Database.UpdateParameter(dbCommand,"?AppToken",servmanSession.AppToken);
                
                	Database.UpdateParameter(dbCommand,"?IntuitTicket",servmanSession.IntuitTicket);
                
                	Database.UpdateParameter(dbCommand,"?SessionStart",servmanSession.SessionStart);
                
                	Database.UpdateParameter(dbCommand,"?IsActive",servmanSession.IsActive);
                
                }

                dbCommand.ExecuteNonQuery();

                
                }
            }
        }

        public static void Insert(List<ServmanSession>  servmanSessionList)
        {
        	Insert(servmanSessionList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update ServmanSession Set "
          
            + " ServmanCustomerId = ?ServmanCustomerId, "
          
            + " QbUserId = ?QbUserId, "
          
            + " Ticket = ?Ticket, "
          
            + " AppToken = ?AppToken, "
          
            + " IntuitTicket = ?IntuitTicket, "
          
            + " SessionStart = ?SessionStart, "
          
            + " IsActive = ?IsActive "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(ServmanSession servmanSession, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", servmanSession.Id);
            
            	Database.PutParameter(dbCommand,"?ServmanCustomerId", servmanSession.ServmanCustomerId);
            
            	Database.PutParameter(dbCommand,"?QbUserId", servmanSession.QbUserId);
            
            	Database.PutParameter(dbCommand,"?Ticket", servmanSession.Ticket);
            
            	Database.PutParameter(dbCommand,"?AppToken", servmanSession.AppToken);
            
            	Database.PutParameter(dbCommand,"?IntuitTicket", servmanSession.IntuitTicket);
            
            	Database.PutParameter(dbCommand,"?SessionStart", servmanSession.SessionStart);
            
            	Database.PutParameter(dbCommand,"?IsActive", servmanSession.IsActive);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(ServmanSession servmanSession)
        {
          	Update(servmanSession, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " ServmanCustomerId, "
        
          + " QbUserId, "
        
          + " Ticket, "
        
          + " AppToken, "
        
          + " IntuitTicket, "
        
          + " SessionStart, "
        
          + " IsActive "
        
          + " From ServmanSession "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static ServmanSession FindByPrimaryKey(
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

            throw new DataNotFoundException("ServmanSession not found, search by primary key");
        }

        public static ServmanSession FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(ServmanSession servmanSession, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",servmanSession.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(ServmanSession servmanSession)
        {
        	return Exists(servmanSession, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from ServmanSession limit 1";

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

        public static ServmanSession Load(IDataReader dataReader, int offset)
        {
              ServmanSession servmanSession = new ServmanSession();

              servmanSession.Id = dataReader.GetInt32(0 + offset);
                  servmanSession.ServmanCustomerId = dataReader.GetInt32(1 + offset);
                  servmanSession.QbUserId = dataReader.GetString(2 + offset);
                  servmanSession.Ticket = dataReader.GetString(3 + offset);
                  servmanSession.AppToken = dataReader.GetString(4 + offset);
                  servmanSession.IntuitTicket = dataReader.GetString(5 + offset);
                  servmanSession.SessionStart = dataReader.GetDateTime(6 + offset);
                  servmanSession.IsActive = dataReader.GetBoolean(7 + offset);
                  

            return servmanSession;
        }

        public static ServmanSession Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From ServmanSession "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(ServmanSession servmanSession, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", servmanSession.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(ServmanSession servmanSession)
        {
        	Delete(servmanSession, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From ServmanSession ";

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
              
                + " ServmanCustomerId, "
              
                + " QbUserId, "
              
                + " Ticket, "
              
                + " AppToken, "
              
                + " IntuitTicket, "
              
                + " SessionStart, "
              
                + " IsActive "
              
                + " From ServmanSession ";

        public static List<ServmanSession> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<ServmanSession> rv = new List<ServmanSession>();

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

        public static List<ServmanSession> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<ServmanSession> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<ServmanSession> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServmanSession));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(ServmanSession item in itemsList)
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

        public static List<ServmanSession> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServmanSession));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<ServmanSession> itemsList = new List<ServmanSession>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is ServmanSession)
              		itemsList.Add(deserializedObject as ServmanSession);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_servmanCustomerId;
              
        protected String m_qbUserId;
              
        protected String m_ticket;
              
        protected String m_appToken;
              
        protected String m_intuitTicket;
              
        protected DateTime m_sessionStart;
              
        protected bool m_isActive;
              
        #endregion

        #region Constructors

        public ServmanSession(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public ServmanSession(
                int 
                  id,int 
                  servmanCustomerId,String 
                  qbUserId,String 
                  ticket,String 
                  appToken,String 
                  intuitTicket,DateTime 
                  sessionStart,bool 
                  isActive
                ) : this()
        {
            
        	m_id = id;
            
        	m_servmanCustomerId = servmanCustomerId;
            
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
        
        public int ServmanCustomerId
        {
        	get { return m_servmanCustomerId;}
            set { m_servmanCustomerId = value; }
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

    