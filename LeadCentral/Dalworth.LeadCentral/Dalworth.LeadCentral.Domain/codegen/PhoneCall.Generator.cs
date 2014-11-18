
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class PhoneCall : ICloneable
    {

        #region Store


        #region Save

        public static PhoneCall Save(PhoneCall phoneCall, IDbConnection connection)
        {
        	if (!Exists(phoneCall, connection))
        		Insert(phoneCall, connection);
        	else
        		Update(phoneCall, connection);
        	return phoneCall;
        }

        public static PhoneCall Save(PhoneCall phoneCall)
        {
        	if (!Exists(phoneCall))
        		Insert(phoneCall);
        	else
        		Update(phoneCall);
        	return phoneCall;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into PhoneCall ( " +
        
          " TrackingPhoneId, " +
        
          " TrackingPhoneNumber, " +
        
          " CallDuration, " +
        
          " RecordingUrl, " +
        
          " DateCreated, " +
        
          " Status, " +
        
          " CampaignId, " +
        
          " CallerName, " +
        
          " FromPhone, " +
        
          " FromCity, " +
        
          " FromState, " +
        
          " FromZip, " +
        
          " FromCountry, " +
        
          " TwilioCallId, " +
        
          " TrackingPhoneRotationId, " +
        
          " TwilioRecordingUrl, " +
        
          " IsProcessed, " +
        
          " PhoneBlackListId, " +
        
          " Notes " +
        
        ") Values (" +
        
          " ?TrackingPhoneId, " +
        
          " ?TrackingPhoneNumber, " +
        
          " ?CallDuration, " +
        
          " ?RecordingUrl, " +
        
          " ?DateCreated, " +
        
          " ?Status, " +
        
          " ?CampaignId, " +
        
          " ?CallerName, " +
        
          " ?FromPhone, " +
        
          " ?FromCity, " +
        
          " ?FromState, " +
        
          " ?FromZip, " +
        
          " ?FromCountry, " +
        
          " ?TwilioCallId, " +
        
          " ?TrackingPhoneRotationId, " +
        
          " ?TwilioRecordingUrl, " +
        
          " ?IsProcessed, " +
        
          " ?PhoneBlackListId, " +
        
          " ?Notes " +
        
        ")";

        public static void Insert(PhoneCall phoneCall, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCall.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneNumber", phoneCall.TrackingPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?CallDuration", phoneCall.CallDuration);
            
            	Database.PutParameter(dbCommand,"?RecordingUrl", phoneCall.RecordingUrl);
            
            	Database.PutParameter(dbCommand,"?DateCreated", phoneCall.DateCreated);
            
            	Database.PutParameter(dbCommand,"?Status", phoneCall.Status);
            
            	Database.PutParameter(dbCommand,"?CampaignId", phoneCall.CampaignId);
            
            	Database.PutParameter(dbCommand,"?CallerName", phoneCall.CallerName);
            
            	Database.PutParameter(dbCommand,"?FromPhone", phoneCall.FromPhone);
            
            	Database.PutParameter(dbCommand,"?FromCity", phoneCall.FromCity);
            
            	Database.PutParameter(dbCommand,"?FromState", phoneCall.FromState);
            
            	Database.PutParameter(dbCommand,"?FromZip", phoneCall.FromZip);
            
            	Database.PutParameter(dbCommand,"?FromCountry", phoneCall.FromCountry);
            
            	Database.PutParameter(dbCommand,"?TwilioCallId", phoneCall.TwilioCallId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneRotationId", phoneCall.TrackingPhoneRotationId);
            
            	Database.PutParameter(dbCommand,"?TwilioRecordingUrl", phoneCall.TwilioRecordingUrl);
            
            	Database.PutParameter(dbCommand,"?IsProcessed", phoneCall.IsProcessed);
            
            	Database.PutParameter(dbCommand,"?PhoneBlackListId", phoneCall.PhoneBlackListId);
            
            	Database.PutParameter(dbCommand,"?Notes", phoneCall.Notes);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		phoneCall.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(PhoneCall phoneCall)
        {
          	Insert(phoneCall, null);
        }

        public static void Insert(List<PhoneCall>  phoneCallList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(PhoneCall phoneCall in  phoneCallList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCall.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneNumber", phoneCall.TrackingPhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?CallDuration", phoneCall.CallDuration);
                
                  	Database.PutParameter(dbCommand,"?RecordingUrl", phoneCall.RecordingUrl);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", phoneCall.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?Status", phoneCall.Status);
                
                  	Database.PutParameter(dbCommand,"?CampaignId", phoneCall.CampaignId);
                
                  	Database.PutParameter(dbCommand,"?CallerName", phoneCall.CallerName);
                
                  	Database.PutParameter(dbCommand,"?FromPhone", phoneCall.FromPhone);
                
                  	Database.PutParameter(dbCommand,"?FromCity", phoneCall.FromCity);
                
                  	Database.PutParameter(dbCommand,"?FromState", phoneCall.FromState);
                
                  	Database.PutParameter(dbCommand,"?FromZip", phoneCall.FromZip);
                
                  	Database.PutParameter(dbCommand,"?FromCountry", phoneCall.FromCountry);
                
                  	Database.PutParameter(dbCommand,"?TwilioCallId", phoneCall.TwilioCallId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneRotationId", phoneCall.TrackingPhoneRotationId);
                
                  	Database.PutParameter(dbCommand,"?TwilioRecordingUrl", phoneCall.TwilioRecordingUrl);
                
                  	Database.PutParameter(dbCommand,"?IsProcessed", phoneCall.IsProcessed);
                
                  	Database.PutParameter(dbCommand,"?PhoneBlackListId", phoneCall.PhoneBlackListId);
                
                  	Database.PutParameter(dbCommand,"?Notes", phoneCall.Notes);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",phoneCall.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneNumber",phoneCall.TrackingPhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?CallDuration",phoneCall.CallDuration);
                
                	Database.UpdateParameter(dbCommand,"?RecordingUrl",phoneCall.RecordingUrl);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",phoneCall.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?Status",phoneCall.Status);
                
                	Database.UpdateParameter(dbCommand,"?CampaignId",phoneCall.CampaignId);
                
                	Database.UpdateParameter(dbCommand,"?CallerName",phoneCall.CallerName);
                
                	Database.UpdateParameter(dbCommand,"?FromPhone",phoneCall.FromPhone);
                
                	Database.UpdateParameter(dbCommand,"?FromCity",phoneCall.FromCity);
                
                	Database.UpdateParameter(dbCommand,"?FromState",phoneCall.FromState);
                
                	Database.UpdateParameter(dbCommand,"?FromZip",phoneCall.FromZip);
                
                	Database.UpdateParameter(dbCommand,"?FromCountry",phoneCall.FromCountry);
                
                	Database.UpdateParameter(dbCommand,"?TwilioCallId",phoneCall.TwilioCallId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneRotationId",phoneCall.TrackingPhoneRotationId);
                
                	Database.UpdateParameter(dbCommand,"?TwilioRecordingUrl",phoneCall.TwilioRecordingUrl);
                
                	Database.UpdateParameter(dbCommand,"?IsProcessed",phoneCall.IsProcessed);
                
                	Database.UpdateParameter(dbCommand,"?PhoneBlackListId",phoneCall.PhoneBlackListId);
                
                	Database.UpdateParameter(dbCommand,"?Notes",phoneCall.Notes);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	phoneCall.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<PhoneCall>  phoneCallList)
        {
        	Insert(phoneCallList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update PhoneCall Set "
          
            + " TrackingPhoneId = ?TrackingPhoneId, "
          
            + " TrackingPhoneNumber = ?TrackingPhoneNumber, "
          
            + " CallDuration = ?CallDuration, "
          
            + " RecordingUrl = ?RecordingUrl, "
          
            + " DateCreated = ?DateCreated, "
          
            + " Status = ?Status, "
          
            + " CampaignId = ?CampaignId, "
          
            + " CallerName = ?CallerName, "
          
            + " FromPhone = ?FromPhone, "
          
            + " FromCity = ?FromCity, "
          
            + " FromState = ?FromState, "
          
            + " FromZip = ?FromZip, "
          
            + " FromCountry = ?FromCountry, "
          
            + " TwilioCallId = ?TwilioCallId, "
          
            + " TrackingPhoneRotationId = ?TrackingPhoneRotationId, "
          
            + " TwilioRecordingUrl = ?TwilioRecordingUrl, "
          
            + " IsProcessed = ?IsProcessed, "
          
            + " PhoneBlackListId = ?PhoneBlackListId, "
          
            + " Notes = ?Notes "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(PhoneCall phoneCall, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", phoneCall.Id);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCall.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneNumber", phoneCall.TrackingPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?CallDuration", phoneCall.CallDuration);
            
            	Database.PutParameter(dbCommand,"?RecordingUrl", phoneCall.RecordingUrl);
            
            	Database.PutParameter(dbCommand,"?DateCreated", phoneCall.DateCreated);
            
            	Database.PutParameter(dbCommand,"?Status", phoneCall.Status);
            
            	Database.PutParameter(dbCommand,"?CampaignId", phoneCall.CampaignId);
            
            	Database.PutParameter(dbCommand,"?CallerName", phoneCall.CallerName);
            
            	Database.PutParameter(dbCommand,"?FromPhone", phoneCall.FromPhone);
            
            	Database.PutParameter(dbCommand,"?FromCity", phoneCall.FromCity);
            
            	Database.PutParameter(dbCommand,"?FromState", phoneCall.FromState);
            
            	Database.PutParameter(dbCommand,"?FromZip", phoneCall.FromZip);
            
            	Database.PutParameter(dbCommand,"?FromCountry", phoneCall.FromCountry);
            
            	Database.PutParameter(dbCommand,"?TwilioCallId", phoneCall.TwilioCallId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneRotationId", phoneCall.TrackingPhoneRotationId);
            
            	Database.PutParameter(dbCommand,"?TwilioRecordingUrl", phoneCall.TwilioRecordingUrl);
            
            	Database.PutParameter(dbCommand,"?IsProcessed", phoneCall.IsProcessed);
            
            	Database.PutParameter(dbCommand,"?PhoneBlackListId", phoneCall.PhoneBlackListId);
            
            	Database.PutParameter(dbCommand,"?Notes", phoneCall.Notes);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(PhoneCall phoneCall)
        {
          	Update(phoneCall, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " TrackingPhoneId, "
        
          + " TrackingPhoneNumber, "
        
          + " CallDuration, "
        
          + " RecordingUrl, "
        
          + " DateCreated, "
        
          + " Status, "
        
          + " CampaignId, "
        
          + " CallerName, "
        
          + " FromPhone, "
        
          + " FromCity, "
        
          + " FromState, "
        
          + " FromZip, "
        
          + " FromCountry, "
        
          + " TwilioCallId, "
        
          + " TrackingPhoneRotationId, "
        
          + " TwilioRecordingUrl, "
        
          + " IsProcessed, "
        
          + " PhoneBlackListId, "
        
          + " Notes "
        
          + " From PhoneCall "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static PhoneCall FindByPrimaryKey(
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

            throw new DataNotFoundException("PhoneCall not found, search by primary key");
        }

        public static PhoneCall FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(PhoneCall phoneCall, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",phoneCall.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(PhoneCall phoneCall)
        {
        	return Exists(phoneCall, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from PhoneCall limit 1";

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

        public static PhoneCall Load(IDataReader dataReader, int offset)
        {
              PhoneCall phoneCall = new PhoneCall();

              phoneCall.Id = dataReader.GetInt32(0 + offset);
                  phoneCall.TrackingPhoneId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    phoneCall.TrackingPhoneNumber = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    phoneCall.CallDuration = dataReader.GetDecimal(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    phoneCall.RecordingUrl = dataReader.GetString(4 + offset);
                  phoneCall.DateCreated = dataReader.GetDateTime(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    phoneCall.Status = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    phoneCall.CampaignId = dataReader.GetInt32(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    phoneCall.CallerName = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    phoneCall.FromPhone = dataReader.GetString(9 + offset);
                  
                    if(!dataReader.IsDBNull(10 + offset))
                    phoneCall.FromCity = dataReader.GetString(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    phoneCall.FromState = dataReader.GetString(11 + offset);
                  
                    if(!dataReader.IsDBNull(12 + offset))
                    phoneCall.FromZip = dataReader.GetString(12 + offset);
                  
                    if(!dataReader.IsDBNull(13 + offset))
                    phoneCall.FromCountry = dataReader.GetString(13 + offset);
                  
                    if(!dataReader.IsDBNull(14 + offset))
                    phoneCall.TwilioCallId = dataReader.GetString(14 + offset);
                  
                    if(!dataReader.IsDBNull(15 + offset))
                    phoneCall.TrackingPhoneRotationId = dataReader.GetInt32(15 + offset);
                  
                    if(!dataReader.IsDBNull(16 + offset))
                    phoneCall.TwilioRecordingUrl = dataReader.GetString(16 + offset);
                  phoneCall.IsProcessed = dataReader.GetBoolean(17 + offset);
                  
                    if(!dataReader.IsDBNull(18 + offset))
                    phoneCall.PhoneBlackListId = dataReader.GetInt32(18 + offset);
                  
                    if(!dataReader.IsDBNull(19 + offset))
                    phoneCall.Notes = dataReader.GetString(19 + offset);
                  

            return phoneCall;
        }

        public static PhoneCall Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From PhoneCall "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(PhoneCall phoneCall, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", phoneCall.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(PhoneCall phoneCall)
        {
        	Delete(phoneCall, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From PhoneCall ";

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
              
                + " CallDuration, "
              
                + " RecordingUrl, "
              
                + " DateCreated, "
              
                + " Status, "
              
                + " CampaignId, "
              
                + " CallerName, "
              
                + " FromPhone, "
              
                + " FromCity, "
              
                + " FromState, "
              
                + " FromZip, "
              
                + " FromCountry, "
              
                + " TwilioCallId, "
              
                + " TrackingPhoneRotationId, "
              
                + " TwilioRecordingUrl, "
              
                + " IsProcessed, "
              
                + " PhoneBlackListId, "
              
                + " Notes "
              
                + " From PhoneCall ";

        public static List<PhoneCall> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<PhoneCall> rv = new List<PhoneCall>();

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

        public static List<PhoneCall> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<PhoneCall> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<PhoneCall> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneCall));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(PhoneCall item in itemsList)
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

        public static List<PhoneCall> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneCall));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<PhoneCall> itemsList = new List<PhoneCall>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is PhoneCall)
              		itemsList.Add(deserializedObject as PhoneCall);
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
              
        protected decimal m_callDuration;
              
        protected String m_recordingUrl;
              
        protected DateTime m_dateCreated;
              
        protected String m_status;
              
        protected int? m_campaignId;
              
        protected String m_callerName;
              
        protected String m_fromPhone;
              
        protected String m_fromCity;
              
        protected String m_fromState;
              
        protected String m_fromZip;
              
        protected String m_fromCountry;
              
        protected String m_twilioCallId;
              
        protected int? m_trackingPhoneRotationId;
              
        protected String m_twilioRecordingUrl;
              
        protected bool m_isProcessed;
              
        protected int? m_phoneBlackListId;
              
        protected String m_notes;
              
        #endregion

        #region Constructors

        public PhoneCall(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public PhoneCall(
                int 
                  id,int 
                  trackingPhoneId,String 
                  trackingPhoneNumber,decimal 
                  callDuration,String 
                  recordingUrl,DateTime 
                  dateCreated,String 
                  status,int? 
                  campaignId,String 
                  callerName,String 
                  fromPhone,String 
                  fromCity,String 
                  fromState,String 
                  fromZip,String 
                  fromCountry,String 
                  twilioCallId,int? 
                  trackingPhoneRotationId,String 
                  twilioRecordingUrl,bool 
                  isProcessed,int? 
                  phoneBlackListId,String 
                  notes
                ) : this()
        {
            
        	m_id = id;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_trackingPhoneNumber = trackingPhoneNumber;
            
        	m_callDuration = callDuration;
            
        	m_recordingUrl = recordingUrl;
            
        	m_dateCreated = dateCreated;
            
        	m_status = status;
            
        	m_campaignId = campaignId;
            
        	m_callerName = callerName;
            
        	m_fromPhone = fromPhone;
            
        	m_fromCity = fromCity;
            
        	m_fromState = fromState;
            
        	m_fromZip = fromZip;
            
        	m_fromCountry = fromCountry;
            
        	m_twilioCallId = twilioCallId;
            
        	m_trackingPhoneRotationId = trackingPhoneRotationId;
            
        	m_twilioRecordingUrl = twilioRecordingUrl;
            
        	m_isProcessed = isProcessed;
            
        	m_phoneBlackListId = phoneBlackListId;
            
        	m_notes = notes;
            
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
        
        public decimal CallDuration
        {
        	get { return m_callDuration;}
            set { m_callDuration = value; }
        }
        
        public String RecordingUrl
        {
        	get { return m_recordingUrl;}
            set { m_recordingUrl = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public String Status
        {
        	get { return m_status;}
            set { m_status = value; }
        }
        
        public int? CampaignId
        {
        	get { return m_campaignId;}
            set { m_campaignId = value; }
        }
        
        public String CallerName
        {
        	get { return m_callerName;}
            set { m_callerName = value; }
        }
        
        public String FromPhone
        {
        	get { return m_fromPhone;}
            set { m_fromPhone = value; }
        }
        
        public String FromCity
        {
        	get { return m_fromCity;}
            set { m_fromCity = value; }
        }
        
        public String FromState
        {
        	get { return m_fromState;}
            set { m_fromState = value; }
        }
        
        public String FromZip
        {
        	get { return m_fromZip;}
            set { m_fromZip = value; }
        }
        
        public String FromCountry
        {
        	get { return m_fromCountry;}
            set { m_fromCountry = value; }
        }
        
        public String TwilioCallId
        {
        	get { return m_twilioCallId;}
            set { m_twilioCallId = value; }
        }
        
        public int? TrackingPhoneRotationId
        {
        	get { return m_trackingPhoneRotationId;}
            set { m_trackingPhoneRotationId = value; }
        }
        
        public String TwilioRecordingUrl
        {
        	get { return m_twilioRecordingUrl;}
            set { m_twilioRecordingUrl = value; }
        }
        
        public bool IsProcessed
        {
        	get { return m_isProcessed;}
            set { m_isProcessed = value; }
        }
        
        public int? PhoneBlackListId
        {
        	get { return m_phoneBlackListId;}
            set { m_phoneBlackListId = value; }
        }
        
        public String Notes
        {
        	get { return m_notes;}
            set { m_notes = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 20; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    