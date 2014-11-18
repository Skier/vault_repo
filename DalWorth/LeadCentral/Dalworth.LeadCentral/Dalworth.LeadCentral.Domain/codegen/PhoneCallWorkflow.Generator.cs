
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class PhoneCallWorkflow : ICloneable
    {

        #region Store


        #region Save

        public static PhoneCallWorkflow Save(PhoneCallWorkflow phoneCallWorkflow, IDbConnection connection)
        {
        	if (!Exists(phoneCallWorkflow, connection))
        		Insert(phoneCallWorkflow, connection);
        	else
        		Update(phoneCallWorkflow, connection);
        	return phoneCallWorkflow;
        }

        public static PhoneCallWorkflow Save(PhoneCallWorkflow phoneCallWorkflow)
        {
        	if (!Exists(phoneCallWorkflow))
        		Insert(phoneCallWorkflow);
        	else
        		Update(phoneCallWorkflow);
        	return phoneCallWorkflow;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into PhoneCallWorkflow ( " +
        
          " TrackingPhoneId, " +
        
          " CallWorkflowId, " +
        
          " FromPhoneNumber, " +
        
          " FromWeekDay, " +
        
          " ToWeekDay, " +
        
          " FromTime, " +
        
          " ToTime, " +
        
          " Priority " +
        
        ") Values (" +
        
          " ?TrackingPhoneId, " +
        
          " ?CallWorkflowId, " +
        
          " ?FromPhoneNumber, " +
        
          " ?FromWeekDay, " +
        
          " ?ToWeekDay, " +
        
          " ?FromTime, " +
        
          " ?ToTime, " +
        
          " ?Priority " +
        
        ")";

        public static void Insert(PhoneCallWorkflow phoneCallWorkflow, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCallWorkflow.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?CallWorkflowId", phoneCallWorkflow.CallWorkflowId);
            
            	Database.PutParameter(dbCommand,"?FromPhoneNumber", phoneCallWorkflow.FromPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?FromWeekDay", phoneCallWorkflow.FromWeekDay);
            
            	Database.PutParameter(dbCommand,"?ToWeekDay", phoneCallWorkflow.ToWeekDay);
            
            	Database.PutParameter(dbCommand,"?FromTime", phoneCallWorkflow.FromTime);
            
            	Database.PutParameter(dbCommand,"?ToTime", phoneCallWorkflow.ToTime);
            
            	Database.PutParameter(dbCommand,"?Priority", phoneCallWorkflow.Priority);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		phoneCallWorkflow.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(PhoneCallWorkflow phoneCallWorkflow)
        {
          	Insert(phoneCallWorkflow, null);
        }

        public static void Insert(List<PhoneCallWorkflow>  phoneCallWorkflowList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(PhoneCallWorkflow phoneCallWorkflow in  phoneCallWorkflowList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCallWorkflow.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?CallWorkflowId", phoneCallWorkflow.CallWorkflowId);
                
                  	Database.PutParameter(dbCommand,"?FromPhoneNumber", phoneCallWorkflow.FromPhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?FromWeekDay", phoneCallWorkflow.FromWeekDay);
                
                  	Database.PutParameter(dbCommand,"?ToWeekDay", phoneCallWorkflow.ToWeekDay);
                
                  	Database.PutParameter(dbCommand,"?FromTime", phoneCallWorkflow.FromTime);
                
                  	Database.PutParameter(dbCommand,"?ToTime", phoneCallWorkflow.ToTime);
                
                  	Database.PutParameter(dbCommand,"?Priority", phoneCallWorkflow.Priority);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",phoneCallWorkflow.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?CallWorkflowId",phoneCallWorkflow.CallWorkflowId);
                
                	Database.UpdateParameter(dbCommand,"?FromPhoneNumber",phoneCallWorkflow.FromPhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?FromWeekDay",phoneCallWorkflow.FromWeekDay);
                
                	Database.UpdateParameter(dbCommand,"?ToWeekDay",phoneCallWorkflow.ToWeekDay);
                
                	Database.UpdateParameter(dbCommand,"?FromTime",phoneCallWorkflow.FromTime);
                
                	Database.UpdateParameter(dbCommand,"?ToTime",phoneCallWorkflow.ToTime);
                
                	Database.UpdateParameter(dbCommand,"?Priority",phoneCallWorkflow.Priority);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	phoneCallWorkflow.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<PhoneCallWorkflow>  phoneCallWorkflowList)
        {
        	Insert(phoneCallWorkflowList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update PhoneCallWorkflow Set "
          
            + " TrackingPhoneId = ?TrackingPhoneId, "
          
            + " CallWorkflowId = ?CallWorkflowId, "
          
            + " FromPhoneNumber = ?FromPhoneNumber, "
          
            + " FromWeekDay = ?FromWeekDay, "
          
            + " ToWeekDay = ?ToWeekDay, "
          
            + " FromTime = ?FromTime, "
          
            + " ToTime = ?ToTime, "
          
            + " Priority = ?Priority "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(PhoneCallWorkflow phoneCallWorkflow, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", phoneCallWorkflow.Id);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCallWorkflow.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?CallWorkflowId", phoneCallWorkflow.CallWorkflowId);
            
            	Database.PutParameter(dbCommand,"?FromPhoneNumber", phoneCallWorkflow.FromPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?FromWeekDay", phoneCallWorkflow.FromWeekDay);
            
            	Database.PutParameter(dbCommand,"?ToWeekDay", phoneCallWorkflow.ToWeekDay);
            
            	Database.PutParameter(dbCommand,"?FromTime", phoneCallWorkflow.FromTime);
            
            	Database.PutParameter(dbCommand,"?ToTime", phoneCallWorkflow.ToTime);
            
            	Database.PutParameter(dbCommand,"?Priority", phoneCallWorkflow.Priority);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(PhoneCallWorkflow phoneCallWorkflow)
        {
          	Update(phoneCallWorkflow, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " TrackingPhoneId, "
        
          + " CallWorkflowId, "
        
          + " FromPhoneNumber, "
        
          + " FromWeekDay, "
        
          + " ToWeekDay, "
        
          + " FromTime, "
        
          + " ToTime, "
        
          + " Priority "
        
          + " From PhoneCallWorkflow "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static PhoneCallWorkflow FindByPrimaryKey(
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

            throw new DataNotFoundException("PhoneCallWorkflow not found, search by primary key");
        }

        public static PhoneCallWorkflow FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(PhoneCallWorkflow phoneCallWorkflow, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",phoneCallWorkflow.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(PhoneCallWorkflow phoneCallWorkflow)
        {
        	return Exists(phoneCallWorkflow, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from PhoneCallWorkflow limit 1";

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

        public static PhoneCallWorkflow Load(IDataReader dataReader, int offset)
        {
              PhoneCallWorkflow phoneCallWorkflow = new PhoneCallWorkflow();

              phoneCallWorkflow.Id = dataReader.GetInt32(0 + offset);
                  phoneCallWorkflow.TrackingPhoneId = dataReader.GetInt32(1 + offset);
                  phoneCallWorkflow.CallWorkflowId = dataReader.GetInt32(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    phoneCallWorkflow.FromPhoneNumber = dataReader.GetString(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    phoneCallWorkflow.FromWeekDay = dataReader.GetInt32(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    phoneCallWorkflow.ToWeekDay = dataReader.GetInt32(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    phoneCallWorkflow.FromTime = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    phoneCallWorkflow.ToTime = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    phoneCallWorkflow.Priority = dataReader.GetInt32(8 + offset);
                  

            return phoneCallWorkflow;
        }

        public static PhoneCallWorkflow Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From PhoneCallWorkflow "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(PhoneCallWorkflow phoneCallWorkflow, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", phoneCallWorkflow.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(PhoneCallWorkflow phoneCallWorkflow)
        {
        	Delete(phoneCallWorkflow, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From PhoneCallWorkflow ";

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
              
                + " TrackingPhoneId, "
              
                + " CallWorkflowId, "
              
                + " FromPhoneNumber, "
              
                + " FromWeekDay, "
              
                + " ToWeekDay, "
              
                + " FromTime, "
              
                + " ToTime, "
              
                + " Priority "
              
                + " From PhoneCallWorkflow ";

        public static List<PhoneCallWorkflow> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<PhoneCallWorkflow> rv = new List<PhoneCallWorkflow>();

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

        public static List<PhoneCallWorkflow> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<PhoneCallWorkflow> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<PhoneCallWorkflow> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneCallWorkflow));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(PhoneCallWorkflow item in itemsList)
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

        public static List<PhoneCallWorkflow> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneCallWorkflow));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<PhoneCallWorkflow> itemsList = new List<PhoneCallWorkflow>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is PhoneCallWorkflow)
              		itemsList.Add(deserializedObject as PhoneCallWorkflow);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_trackingPhoneId;
              
        protected int m_callWorkflowId;
              
        protected String m_fromPhoneNumber;
              
        protected int? m_fromWeekDay;
              
        protected int? m_toWeekDay;
              
        protected String m_fromTime;
              
        protected String m_toTime;
              
        protected int? m_priority;
              
        #endregion

        #region Constructors

        public PhoneCallWorkflow(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public PhoneCallWorkflow(
                int 
                  id,int 
                  trackingPhoneId,int 
                  callWorkflowId,String 
                  fromPhoneNumber,int? 
                  fromWeekDay,int? 
                  toWeekDay,String 
                  fromTime,String 
                  toTime,int? 
                  priority
                ) : this()
        {
            
        	m_id = id;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_callWorkflowId = callWorkflowId;
            
        	m_fromPhoneNumber = fromPhoneNumber;
            
        	m_fromWeekDay = fromWeekDay;
            
        	m_toWeekDay = toWeekDay;
            
        	m_fromTime = fromTime;
            
        	m_toTime = toTime;
            
        	m_priority = priority;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int TrackingPhoneId
        {
        	get { return m_trackingPhoneId;}
            set { m_trackingPhoneId = value; }
        }
        
        public int CallWorkflowId
        {
        	get { return m_callWorkflowId;}
            set { m_callWorkflowId = value; }
        }
        
        public String FromPhoneNumber
        {
        	get { return m_fromPhoneNumber;}
            set { m_fromPhoneNumber = value; }
        }
        
        public int? FromWeekDay
        {
        	get { return m_fromWeekDay;}
            set { m_fromWeekDay = value; }
        }
        
        public int? ToWeekDay
        {
        	get { return m_toWeekDay;}
            set { m_toWeekDay = value; }
        }
        
        public String FromTime
        {
        	get { return m_fromTime;}
            set { m_fromTime = value; }
        }
        
        public String ToTime
        {
        	get { return m_toTime;}
            set { m_toTime = value; }
        }
        
        public int? Priority
        {
        	get { return m_priority;}
            set { m_priority = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 9; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    