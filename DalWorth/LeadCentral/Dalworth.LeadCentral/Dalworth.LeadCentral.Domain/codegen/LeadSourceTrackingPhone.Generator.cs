
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class LeadSourceTrackingPhone : ICloneable
    {

        #region Store


        #region Save

        public static LeadSourceTrackingPhone Save(LeadSourceTrackingPhone leadSourceTrackingPhone, IDbConnection connection)
        {
        	if (!Exists(leadSourceTrackingPhone, connection))
        		Insert(leadSourceTrackingPhone, connection);
        	else
        		Update(leadSourceTrackingPhone, connection);
        	return leadSourceTrackingPhone;
        }

        public static LeadSourceTrackingPhone Save(LeadSourceTrackingPhone leadSourceTrackingPhone)
        {
        	if (!Exists(leadSourceTrackingPhone))
        		Insert(leadSourceTrackingPhone);
        	else
        		Update(leadSourceTrackingPhone);
        	return leadSourceTrackingPhone;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into LeadSourceTrackingPhone ( " +
        
          " LeadSourceId, " +
        
          " TrackingPhoneId, " +
        
          " Notes " +
        
        ") Values (" +
        
          " ?LeadSourceId, " +
        
          " ?TrackingPhoneId, " +
        
          " ?Notes " +
        
        ")";

        public static void Insert(LeadSourceTrackingPhone leadSourceTrackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourceTrackingPhone.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", leadSourceTrackingPhone.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?Notes", leadSourceTrackingPhone.Notes);
            
            	dbCommand.ExecuteNonQuery();
            
            }
        }

        public static void Insert(LeadSourceTrackingPhone leadSourceTrackingPhone)
        {
          	Insert(leadSourceTrackingPhone, null);
        }

        public static void Insert(List<LeadSourceTrackingPhone>  leadSourceTrackingPhoneList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(LeadSourceTrackingPhone leadSourceTrackingPhone in  leadSourceTrackingPhoneList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourceTrackingPhone.LeadSourceId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", leadSourceTrackingPhone.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?Notes", leadSourceTrackingPhone.Notes);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadSourceId",leadSourceTrackingPhone.LeadSourceId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",leadSourceTrackingPhone.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?Notes",leadSourceTrackingPhone.Notes);
                
                }

                dbCommand.ExecuteNonQuery();

                
                }
            }
        }

        public static void Insert(List<LeadSourceTrackingPhone>  leadSourceTrackingPhoneList)
        {
        	Insert(leadSourceTrackingPhoneList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update LeadSourceTrackingPhone Set "
          
            + " Notes = ?Notes "
          
            + " Where "
            
            + " LeadSourceId = ?LeadSourceId and  "
            
            + " TrackingPhoneId = ?TrackingPhoneId "
            ;

        public static void Update(LeadSourceTrackingPhone leadSourceTrackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourceTrackingPhone.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", leadSourceTrackingPhone.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?Notes", leadSourceTrackingPhone.Notes);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(LeadSourceTrackingPhone leadSourceTrackingPhone)
        {
          	Update(leadSourceTrackingPhone, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " LeadSourceId, "
        
          + " TrackingPhoneId, "
        
          + " Notes "
        
          + " From LeadSourceTrackingPhone "
        
          + " Where "
          
          + " LeadSourceId = ?LeadSourceId and  "
          
          + " TrackingPhoneId = ?TrackingPhoneId "
          ;

        public static LeadSourceTrackingPhone FindByPrimaryKey(
              int leadSourceId,int trackingPhoneId, IDbConnection connection
              )
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
              
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourceId);
              
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneId);
              

              	using(IDataReader dataReader = dbCommand.ExecuteReader())
              	{
              		if(dataReader.Read())
              			return Load(dataReader);
              	}
            }

            throw new DataNotFoundException("LeadSourceTrackingPhone not found, search by primary key");
        }

        public static LeadSourceTrackingPhone FindByPrimaryKey(
              int leadSourceId,int trackingPhoneId
              )
        {
        	return FindByPrimaryKey(
              leadSourceId,trackingPhoneId, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(LeadSourceTrackingPhone leadSourceTrackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?LeadSourceId",leadSourceTrackingPhone.LeadSourceId);
            
              	Database.PutParameter(dbCommand,"?TrackingPhoneId",leadSourceTrackingPhone.TrackingPhoneId);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(LeadSourceTrackingPhone leadSourceTrackingPhone)
        {
        	return Exists(leadSourceTrackingPhone, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from LeadSourceTrackingPhone limit 1";

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

        public static LeadSourceTrackingPhone Load(IDataReader dataReader, int offset)
        {
              LeadSourceTrackingPhone leadSourceTrackingPhone = new LeadSourceTrackingPhone();

              leadSourceTrackingPhone.LeadSourceId = dataReader.GetInt32(0 + offset);
                  leadSourceTrackingPhone.TrackingPhoneId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    leadSourceTrackingPhone.Notes = dataReader.GetString(2 + offset);
                  

            return leadSourceTrackingPhone;
        }

        public static LeadSourceTrackingPhone Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From LeadSourceTrackingPhone "

              
                + " Where "
                
                  + " LeadSourceId = ?LeadSourceId and  "
                
                  + " TrackingPhoneId = ?TrackingPhoneId "
                ;

        public static void Delete(LeadSourceTrackingPhone leadSourceTrackingPhone, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourceTrackingPhone.LeadSourceId);
              
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", leadSourceTrackingPhone.TrackingPhoneId);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(LeadSourceTrackingPhone leadSourceTrackingPhone)
        {
        	Delete(leadSourceTrackingPhone, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From LeadSourceTrackingPhone ";

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
              
                + " LeadSourceId, "
              
                + " TrackingPhoneId, "
              
                + " Notes "
              
                + " From LeadSourceTrackingPhone ";

        public static List<LeadSourceTrackingPhone> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<LeadSourceTrackingPhone> rv = new List<LeadSourceTrackingPhone>();

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

        public static List<LeadSourceTrackingPhone> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<LeadSourceTrackingPhone> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<LeadSourceTrackingPhone> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadSourceTrackingPhone));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(LeadSourceTrackingPhone item in itemsList)
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

        public static List<LeadSourceTrackingPhone> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadSourceTrackingPhone));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<LeadSourceTrackingPhone> itemsList = new List<LeadSourceTrackingPhone>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is LeadSourceTrackingPhone)
              		itemsList.Add(deserializedObject as LeadSourceTrackingPhone);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_leadSourceId;
              
        protected int m_trackingPhoneId;
              
        protected String m_notes;
              
        #endregion

        #region Constructors

        public LeadSourceTrackingPhone(
              int 
                  leadSourceId,int 
                  trackingPhoneId
              ) : this()
        {
            
        	m_leadSourceId = leadSourceId;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        }

        

        public LeadSourceTrackingPhone(
                int 
                  leadSourceId,int 
                  trackingPhoneId,String 
                  notes
                ) : this()
        {
            
        	m_leadSourceId = leadSourceId;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_notes = notes;
            
        }

        

        #endregion

        
        public int LeadSourceId
        {
        	get { return m_leadSourceId;}
            set { m_leadSourceId = value; }
        }
        
        public int TrackingPhoneId
        {
        	get { return m_trackingPhoneId;}
            set { m_trackingPhoneId = value; }
        }
        
        public String Notes
        {
        	get { return m_notes;}
            set { m_notes = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 3; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    