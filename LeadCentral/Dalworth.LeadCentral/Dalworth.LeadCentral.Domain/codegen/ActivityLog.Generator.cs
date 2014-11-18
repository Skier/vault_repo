
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class ActivityLog : ICloneable
    {

        #region Store


        #region Save

        public static ActivityLog Save(ActivityLog activityLog, IDbConnection connection)
        {
        	if (!Exists(activityLog, connection))
        		Insert(activityLog, connection);
        	else
        		Update(activityLog, connection);
        	return activityLog;
        }

        public static ActivityLog Save(ActivityLog activityLog)
        {
        	if (!Exists(activityLog))
        		Insert(activityLog);
        	else
        		Update(activityLog);
        	return activityLog;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into ActivityLog ( " +
        
          " DateCreated, " +
        
          " UserId, " +
        
          " ActivityNotes " +
        
        ") Values (" +
        
          " ?DateCreated, " +
        
          " ?UserId, " +
        
          " ?ActivityNotes " +
        
        ")";

        public static void Insert(ActivityLog activityLog, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?DateCreated", activityLog.DateCreated);
            
            	Database.PutParameter(dbCommand,"?UserId", activityLog.UserId);
            
            	Database.PutParameter(dbCommand,"?ActivityNotes", activityLog.ActivityNotes);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		activityLog.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(ActivityLog activityLog)
        {
          	Insert(activityLog, null);
        }

        public static void Insert(List<ActivityLog>  activityLogList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(ActivityLog activityLog in  activityLogList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?DateCreated", activityLog.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?UserId", activityLog.UserId);
                
                  	Database.PutParameter(dbCommand,"?ActivityNotes", activityLog.ActivityNotes);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",activityLog.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?UserId",activityLog.UserId);
                
                	Database.UpdateParameter(dbCommand,"?ActivityNotes",activityLog.ActivityNotes);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	activityLog.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<ActivityLog>  activityLogList)
        {
        	Insert(activityLogList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update ActivityLog Set "
          
            + " DateCreated = ?DateCreated, "
          
            + " UserId = ?UserId, "
          
            + " ActivityNotes = ?ActivityNotes "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(ActivityLog activityLog, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", activityLog.Id);
            
            	Database.PutParameter(dbCommand,"?DateCreated", activityLog.DateCreated);
            
            	Database.PutParameter(dbCommand,"?UserId", activityLog.UserId);
            
            	Database.PutParameter(dbCommand,"?ActivityNotes", activityLog.ActivityNotes);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(ActivityLog activityLog)
        {
          	Update(activityLog, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " DateCreated, "
        
          + " UserId, "
        
          + " ActivityNotes "
        
          + " From ActivityLog "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static ActivityLog FindByPrimaryKey(
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

            throw new DataNotFoundException("ActivityLog not found, search by primary key");
        }

        public static ActivityLog FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(ActivityLog activityLog, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",activityLog.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(ActivityLog activityLog)
        {
        	return Exists(activityLog, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from ActivityLog limit 1";

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

        public static ActivityLog Load(IDataReader dataReader, int offset)
        {
              ActivityLog activityLog = new ActivityLog();

              activityLog.Id = dataReader.GetInt32(0 + offset);
                  activityLog.DateCreated = dataReader.GetDateTime(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    activityLog.UserId = dataReader.GetInt32(2 + offset);
                  activityLog.ActivityNotes = dataReader.GetString(3 + offset);
                  

            return activityLog;
        }

        public static ActivityLog Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From ActivityLog "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(ActivityLog activityLog, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", activityLog.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(ActivityLog activityLog)
        {
        	Delete(activityLog, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From ActivityLog ";

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
              
                + " DateCreated, "
              
                + " UserId, "
              
                + " ActivityNotes "
              
                + " From ActivityLog ";

        public static List<ActivityLog> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<ActivityLog> rv = new List<ActivityLog>();

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

        public static List<ActivityLog> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<ActivityLog> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<ActivityLog> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ActivityLog));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(ActivityLog item in itemsList)
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

        public static List<ActivityLog> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(ActivityLog));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<ActivityLog> itemsList = new List<ActivityLog>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is ActivityLog)
              		itemsList.Add(deserializedObject as ActivityLog);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected DateTime m_dateCreated;
              
        protected int? m_userId;
              
        protected String m_activityNotes;
              
        #endregion

        #region Constructors

        public ActivityLog(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public ActivityLog(
                int 
                  id,DateTime 
                  dateCreated,int? 
                  userId,String 
                  activityNotes
                ) : this()
        {
            
        	m_id = id;
            
        	m_dateCreated = dateCreated;
            
        	m_userId = userId;
            
        	m_activityNotes = activityNotes;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public int? UserId
        {
        	get { return m_userId;}
            set { m_userId = value; }
        }
        
        public String ActivityNotes
        {
        	get { return m_activityNotes;}
            set { m_activityNotes = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 4; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    