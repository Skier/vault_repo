
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class Notification : ICloneable
    {

        #region Store


        #region Save

        public static Notification Save(Notification notification, IDbConnection connection)
        {
        	if (!Exists(notification, connection))
        		Insert(notification, connection);
        	else
        		Update(notification, connection);
        	return notification;
        }

        public static Notification Save(Notification notification)
        {
        	if (!Exists(notification))
        		Insert(notification);
        	else
        		Update(notification);
        	return notification;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Notification ( " +
        
          " NotificationTypeId, " +
        
          " DateCreated, " +
        
          " DateProcessed, " +
        
          " IsProcessed, " +
        
          " FromEmail, " +
        
          " ToEmail, " +
        
          " Message " +
        
        ") Values (" +
        
          " ?NotificationTypeId, " +
        
          " ?DateCreated, " +
        
          " ?DateProcessed, " +
        
          " ?IsProcessed, " +
        
          " ?FromEmail, " +
        
          " ?ToEmail, " +
        
          " ?Message " +
        
        ")";

        public static void Insert(Notification notification, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?NotificationTypeId", notification.NotificationTypeId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", notification.DateCreated);
            
            	Database.PutParameter(dbCommand,"?DateProcessed", notification.DateProcessed);
            
            	Database.PutParameter(dbCommand,"?IsProcessed", notification.IsProcessed);
            
            	Database.PutParameter(dbCommand,"?FromEmail", notification.FromEmail);
            
            	Database.PutParameter(dbCommand,"?ToEmail", notification.ToEmail);
            
            	Database.PutParameter(dbCommand,"?Message", notification.Message);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		notification.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Notification notification)
        {
          	Insert(notification, null);
        }

        public static void Insert(List<Notification>  notificationList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Notification notification in  notificationList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?NotificationTypeId", notification.NotificationTypeId);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", notification.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?DateProcessed", notification.DateProcessed);
                
                  	Database.PutParameter(dbCommand,"?IsProcessed", notification.IsProcessed);
                
                  	Database.PutParameter(dbCommand,"?FromEmail", notification.FromEmail);
                
                  	Database.PutParameter(dbCommand,"?ToEmail", notification.ToEmail);
                
                  	Database.PutParameter(dbCommand,"?Message", notification.Message);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?NotificationTypeId",notification.NotificationTypeId);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",notification.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?DateProcessed",notification.DateProcessed);
                
                	Database.UpdateParameter(dbCommand,"?IsProcessed",notification.IsProcessed);
                
                	Database.UpdateParameter(dbCommand,"?FromEmail",notification.FromEmail);
                
                	Database.UpdateParameter(dbCommand,"?ToEmail",notification.ToEmail);
                
                	Database.UpdateParameter(dbCommand,"?Message",notification.Message);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	notification.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Notification>  notificationList)
        {
        	Insert(notificationList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Notification Set "
          
            + " NotificationTypeId = ?NotificationTypeId, "
          
            + " DateCreated = ?DateCreated, "
          
            + " DateProcessed = ?DateProcessed, "
          
            + " IsProcessed = ?IsProcessed, "
          
            + " FromEmail = ?FromEmail, "
          
            + " ToEmail = ?ToEmail, "
          
            + " Message = ?Message "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Notification notification, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", notification.Id);
            
            	Database.PutParameter(dbCommand,"?NotificationTypeId", notification.NotificationTypeId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", notification.DateCreated);
            
            	Database.PutParameter(dbCommand,"?DateProcessed", notification.DateProcessed);
            
            	Database.PutParameter(dbCommand,"?IsProcessed", notification.IsProcessed);
            
            	Database.PutParameter(dbCommand,"?FromEmail", notification.FromEmail);
            
            	Database.PutParameter(dbCommand,"?ToEmail", notification.ToEmail);
            
            	Database.PutParameter(dbCommand,"?Message", notification.Message);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Notification notification)
        {
          	Update(notification, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " NotificationTypeId, "
        
          + " DateCreated, "
        
          + " DateProcessed, "
        
          + " IsProcessed, "
        
          + " FromEmail, "
        
          + " ToEmail, "
        
          + " Message "
        
          + " From Notification "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Notification FindByPrimaryKey(
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

            throw new DataNotFoundException("Notification not found, search by primary key");
        }

        public static Notification FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Notification notification, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",notification.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Notification notification)
        {
        	return Exists(notification, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Notification limit 1";

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

        public static Notification Load(IDataReader dataReader, int offset)
        {
              Notification notification = new Notification();

              notification.Id = dataReader.GetInt32(0 + offset);
                  notification.NotificationTypeId = dataReader.GetInt32(1 + offset);
                  notification.DateCreated = dataReader.GetDateTime(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    notification.DateProcessed = dataReader.GetDateTime(3 + offset);
                  notification.IsProcessed = dataReader.GetBoolean(4 + offset);
                  notification.FromEmail = dataReader.GetString(5 + offset);
                  notification.ToEmail = dataReader.GetString(6 + offset);
                  notification.Message = dataReader.GetString(7 + offset);
                  

            return notification;
        }

        public static Notification Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Notification "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Notification notification, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", notification.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Notification notification)
        {
        	Delete(notification, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Notification ";

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
              
                + " NotificationTypeId, "
              
                + " DateCreated, "
              
                + " DateProcessed, "
              
                + " IsProcessed, "
              
                + " FromEmail, "
              
                + " ToEmail, "
              
                + " Message "
              
                + " From Notification ";

        public static List<Notification> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Notification> rv = new List<Notification>();

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

        public static List<Notification> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Notification> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Notification> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Notification));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Notification item in itemsList)
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

        public static List<Notification> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Notification));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Notification> itemsList = new List<Notification>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Notification)
              		itemsList.Add(deserializedObject as Notification);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_notificationTypeId;
              
        protected DateTime m_dateCreated;
              
        protected DateTime? m_dateProcessed;
              
        protected bool m_isProcessed;
              
        protected String m_fromEmail;
              
        protected String m_toEmail;
              
        protected String m_message;
              
        #endregion

        #region Constructors

        public Notification(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Notification(
                int 
                  id,int 
                  notificationTypeId,DateTime 
                  dateCreated,DateTime? 
                  dateProcessed,bool 
                  isProcessed,String 
                  fromEmail,String 
                  toEmail,String 
                  message
                ) : this()
        {
            
        	m_id = id;
            
        	m_notificationTypeId = notificationTypeId;
            
        	m_dateCreated = dateCreated;
            
        	m_dateProcessed = dateProcessed;
            
        	m_isProcessed = isProcessed;
            
        	m_fromEmail = fromEmail;
            
        	m_toEmail = toEmail;
            
        	m_message = message;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int NotificationTypeId
        {
        	get { return m_notificationTypeId;}
            set { m_notificationTypeId = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public DateTime? DateProcessed
        {
        	get { return m_dateProcessed;}
            set { m_dateProcessed = value; }
        }
        
        public bool IsProcessed
        {
        	get { return m_isProcessed;}
            set { m_isProcessed = value; }
        }
        
        public String FromEmail
        {
        	get { return m_fromEmail;}
            set { m_fromEmail = value; }
        }
        
        public String ToEmail
        {
        	get { return m_toEmail;}
            set { m_toEmail = value; }
        }
        
        public String Message
        {
        	get { return m_message;}
            set { m_message = value; }
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

    