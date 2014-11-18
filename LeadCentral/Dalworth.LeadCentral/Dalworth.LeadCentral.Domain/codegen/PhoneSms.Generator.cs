
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class PhoneSms : ICloneable
    {

        #region Store


        #region Save

        public static PhoneSms Save(PhoneSms phoneSms, IDbConnection connection)
        {
        	if (!Exists(phoneSms, connection))
        		Insert(phoneSms, connection);
        	else
        		Update(phoneSms, connection);
        	return phoneSms;
        }

        public static PhoneSms Save(PhoneSms phoneSms)
        {
        	if (!Exists(phoneSms))
        		Insert(phoneSms);
        	else
        		Update(phoneSms);
        	return phoneSms;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into PhoneSms ( " +
        
          " TrackingPhoneId, " +
        
          " TrackingPhoneNumber, " +
        
          " Message, " +
        
          " DateCreated, " +
        
          " CampaignId, " +
        
          " FromPhone, " +
        
          " Status, " +
        
          " TwilioSmsId, " +
        
          " TrackingPhoneRotationId " +
        
        ") Values (" +
        
          " ?TrackingPhoneId, " +
        
          " ?TrackingPhoneNumber, " +
        
          " ?Message, " +
        
          " ?DateCreated, " +
        
          " ?CampaignId, " +
        
          " ?FromPhone, " +
        
          " ?Status, " +
        
          " ?TwilioSmsId, " +
        
          " ?TrackingPhoneRotationId " +
        
        ")";

        public static void Insert(PhoneSms phoneSms, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneSms.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneNumber", phoneSms.TrackingPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Message", phoneSms.Message);
            
            	Database.PutParameter(dbCommand,"?DateCreated", phoneSms.DateCreated);
            
            	Database.PutParameter(dbCommand,"?CampaignId", phoneSms.CampaignId);
            
            	Database.PutParameter(dbCommand,"?FromPhone", phoneSms.FromPhone);
            
            	Database.PutParameter(dbCommand,"?Status", phoneSms.Status);
            
            	Database.PutParameter(dbCommand,"?TwilioSmsId", phoneSms.TwilioSmsId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneRotationId", phoneSms.TrackingPhoneRotationId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		phoneSms.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(PhoneSms phoneSms)
        {
          	Insert(phoneSms, null);
        }

        public static void Insert(List<PhoneSms>  phoneSmsList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(PhoneSms phoneSms in  phoneSmsList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneSms.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneNumber", phoneSms.TrackingPhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?Message", phoneSms.Message);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", phoneSms.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?CampaignId", phoneSms.CampaignId);
                
                  	Database.PutParameter(dbCommand,"?FromPhone", phoneSms.FromPhone);
                
                  	Database.PutParameter(dbCommand,"?Status", phoneSms.Status);
                
                  	Database.PutParameter(dbCommand,"?TwilioSmsId", phoneSms.TwilioSmsId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneRotationId", phoneSms.TrackingPhoneRotationId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",phoneSms.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneNumber",phoneSms.TrackingPhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?Message",phoneSms.Message);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",phoneSms.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?CampaignId",phoneSms.CampaignId);
                
                	Database.UpdateParameter(dbCommand,"?FromPhone",phoneSms.FromPhone);
                
                	Database.UpdateParameter(dbCommand,"?Status",phoneSms.Status);
                
                	Database.UpdateParameter(dbCommand,"?TwilioSmsId",phoneSms.TwilioSmsId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneRotationId",phoneSms.TrackingPhoneRotationId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	phoneSms.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<PhoneSms>  phoneSmsList)
        {
        	Insert(phoneSmsList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update PhoneSms Set "
          
            + " TrackingPhoneId = ?TrackingPhoneId, "
          
            + " TrackingPhoneNumber = ?TrackingPhoneNumber, "
          
            + " Message = ?Message, "
          
            + " DateCreated = ?DateCreated, "
          
            + " CampaignId = ?CampaignId, "
          
            + " FromPhone = ?FromPhone, "
          
            + " Status = ?Status, "
          
            + " TwilioSmsId = ?TwilioSmsId, "
          
            + " TrackingPhoneRotationId = ?TrackingPhoneRotationId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(PhoneSms phoneSms, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", phoneSms.Id);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneSms.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneNumber", phoneSms.TrackingPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Message", phoneSms.Message);
            
            	Database.PutParameter(dbCommand,"?DateCreated", phoneSms.DateCreated);
            
            	Database.PutParameter(dbCommand,"?CampaignId", phoneSms.CampaignId);
            
            	Database.PutParameter(dbCommand,"?FromPhone", phoneSms.FromPhone);
            
            	Database.PutParameter(dbCommand,"?Status", phoneSms.Status);
            
            	Database.PutParameter(dbCommand,"?TwilioSmsId", phoneSms.TwilioSmsId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneRotationId", phoneSms.TrackingPhoneRotationId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(PhoneSms phoneSms)
        {
          	Update(phoneSms, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " TrackingPhoneId, "
        
          + " TrackingPhoneNumber, "
        
          + " Message, "
        
          + " DateCreated, "
        
          + " CampaignId, "
        
          + " FromPhone, "
        
          + " Status, "
        
          + " TwilioSmsId, "
        
          + " TrackingPhoneRotationId "
        
          + " From PhoneSms "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static PhoneSms FindByPrimaryKey(
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

            throw new DataNotFoundException("PhoneSms not found, search by primary key");
        }

        public static PhoneSms FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(PhoneSms phoneSms, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",phoneSms.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(PhoneSms phoneSms)
        {
        	return Exists(phoneSms, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from PhoneSms limit 1";

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

        public static PhoneSms Load(IDataReader dataReader, int offset)
        {
              PhoneSms phoneSms = new PhoneSms();

              phoneSms.Id = dataReader.GetInt32(0 + offset);
                  phoneSms.TrackingPhoneId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    phoneSms.TrackingPhoneNumber = dataReader.GetString(2 + offset);
                  phoneSms.Message = dataReader.GetString(3 + offset);
                  phoneSms.DateCreated = dataReader.GetDateTime(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    phoneSms.CampaignId = dataReader.GetInt32(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    phoneSms.FromPhone = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    phoneSms.Status = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    phoneSms.TwilioSmsId = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    phoneSms.TrackingPhoneRotationId = dataReader.GetInt32(9 + offset);
                  

            return phoneSms;
        }

        public static PhoneSms Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From PhoneSms "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(PhoneSms phoneSms, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", phoneSms.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(PhoneSms phoneSms)
        {
        	Delete(phoneSms, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From PhoneSms ";

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
              
                + " TrackingPhoneNumber, "
              
                + " Message, "
              
                + " DateCreated, "
              
                + " CampaignId, "
              
                + " FromPhone, "
              
                + " Status, "
              
                + " TwilioSmsId, "
              
                + " TrackingPhoneRotationId "
              
                + " From PhoneSms ";

        public static List<PhoneSms> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<PhoneSms> rv = new List<PhoneSms>();

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

        public static List<PhoneSms> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<PhoneSms> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<PhoneSms> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneSms));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(PhoneSms item in itemsList)
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

        public static List<PhoneSms> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneSms));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<PhoneSms> itemsList = new List<PhoneSms>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is PhoneSms)
              		itemsList.Add(deserializedObject as PhoneSms);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_trackingPhoneId;
              
        protected String m_trackingPhoneNumber;
              
        protected String m_message;
              
        protected DateTime m_dateCreated;
              
        protected int? m_campaignId;
              
        protected String m_fromPhone;
              
        protected String m_status;
              
        protected String m_twilioSmsId;
              
        protected int? m_trackingPhoneRotationId;
              
        #endregion

        #region Constructors

        public PhoneSms(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public PhoneSms(
                int 
                  id,int 
                  trackingPhoneId,String 
                  trackingPhoneNumber,String 
                  message,DateTime 
                  dateCreated,int? 
                  campaignId,String 
                  fromPhone,String 
                  status,String 
                  twilioSmsId,int? 
                  trackingPhoneRotationId
                ) : this()
        {
            
        	m_id = id;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_trackingPhoneNumber = trackingPhoneNumber;
            
        	m_message = message;
            
        	m_dateCreated = dateCreated;
            
        	m_campaignId = campaignId;
            
        	m_fromPhone = fromPhone;
            
        	m_status = status;
            
        	m_twilioSmsId = twilioSmsId;
            
        	m_trackingPhoneRotationId = trackingPhoneRotationId;
            
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
        
        public String TrackingPhoneNumber
        {
        	get { return m_trackingPhoneNumber;}
            set { m_trackingPhoneNumber = value; }
        }
        
        public String Message
        {
        	get { return m_message;}
            set { m_message = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public int? CampaignId
        {
        	get { return m_campaignId;}
            set { m_campaignId = value; }
        }
        
        public String FromPhone
        {
        	get { return m_fromPhone;}
            set { m_fromPhone = value; }
        }
        
        public String Status
        {
        	get { return m_status;}
            set { m_status = value; }
        }
        
        public String TwilioSmsId
        {
        	get { return m_twilioSmsId;}
            set { m_twilioSmsId = value; }
        }
        
        public int? TrackingPhoneRotationId
        {
        	get { return m_trackingPhoneRotationId;}
            set { m_trackingPhoneRotationId = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 10; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    