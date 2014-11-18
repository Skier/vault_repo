
using System;
using System.Data;
using System.Collections.Generic;
using Servman.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Servman.Domain
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
        
          " IsAnsweredByUser, " +
        
          " AnsweredByUserId, " +
        
          " CallSid, " +
        
          " AccountSid, " +
        
          " PhoneFrom, " +
        
          " PhoneTo, " +
        
          " CallStatus, " +
        
          " ApiVersion, " +
        
          " Direction, " +
        
          " ForwardedFrom, " +
        
          " FromCity, " +
        
          " FromState, " +
        
          " FromZip, " +
        
          " FromCountry, " +
        
          " ToCity, " +
        
          " ToState, " +
        
          " ToZip, " +
        
          " ToCountry, " +
        
          " CallDuration, " +
        
          " RecordingUrl, " +
        
          " CallerName, " +
        
          " LeadSourceId, " +
        
          " DateCreated " +
        
        ") Values (" +
        
          " ?TrackingPhoneId, " +
        
          " ?IsAnsweredByUser, " +
        
          " ?AnsweredByUserId, " +
        
          " ?CallSid, " +
        
          " ?AccountSid, " +
        
          " ?PhoneFrom, " +
        
          " ?PhoneTo, " +
        
          " ?CallStatus, " +
        
          " ?ApiVersion, " +
        
          " ?Direction, " +
        
          " ?ForwardedFrom, " +
        
          " ?FromCity, " +
        
          " ?FromState, " +
        
          " ?FromZip, " +
        
          " ?FromCountry, " +
        
          " ?ToCity, " +
        
          " ?ToState, " +
        
          " ?ToZip, " +
        
          " ?ToCountry, " +
        
          " ?CallDuration, " +
        
          " ?RecordingUrl, " +
        
          " ?CallerName, " +
        
          " ?LeadSourceId, " +
        
          " ?DateCreated " +
        
        ")";

        public static void Insert(PhoneCall phoneCall, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCall.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?IsAnsweredByUser", phoneCall.IsAnsweredByUser);
            
            	Database.PutParameter(dbCommand,"?AnsweredByUserId", phoneCall.AnsweredByUserId);
            
            	Database.PutParameter(dbCommand,"?CallSid", phoneCall.CallSid);
            
            	Database.PutParameter(dbCommand,"?AccountSid", phoneCall.AccountSid);
            
            	Database.PutParameter(dbCommand,"?PhoneFrom", phoneCall.PhoneFrom);
            
            	Database.PutParameter(dbCommand,"?PhoneTo", phoneCall.PhoneTo);
            
            	Database.PutParameter(dbCommand,"?CallStatus", phoneCall.CallStatus);
            
            	Database.PutParameter(dbCommand,"?ApiVersion", phoneCall.ApiVersion);
            
            	Database.PutParameter(dbCommand,"?Direction", phoneCall.Direction);
            
            	Database.PutParameter(dbCommand,"?ForwardedFrom", phoneCall.ForwardedFrom);
            
            	Database.PutParameter(dbCommand,"?FromCity", phoneCall.FromCity);
            
            	Database.PutParameter(dbCommand,"?FromState", phoneCall.FromState);
            
            	Database.PutParameter(dbCommand,"?FromZip", phoneCall.FromZip);
            
            	Database.PutParameter(dbCommand,"?FromCountry", phoneCall.FromCountry);
            
            	Database.PutParameter(dbCommand,"?ToCity", phoneCall.ToCity);
            
            	Database.PutParameter(dbCommand,"?ToState", phoneCall.ToState);
            
            	Database.PutParameter(dbCommand,"?ToZip", phoneCall.ToZip);
            
            	Database.PutParameter(dbCommand,"?ToCountry", phoneCall.ToCountry);
            
            	Database.PutParameter(dbCommand,"?CallDuration", phoneCall.CallDuration);
            
            	Database.PutParameter(dbCommand,"?RecordingUrl", phoneCall.RecordingUrl);
            
            	Database.PutParameter(dbCommand,"?CallerName", phoneCall.CallerName);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", phoneCall.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", phoneCall.DateCreated);
            
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
                
                  	Database.PutParameter(dbCommand,"?IsAnsweredByUser", phoneCall.IsAnsweredByUser);
                
                  	Database.PutParameter(dbCommand,"?AnsweredByUserId", phoneCall.AnsweredByUserId);
                
                  	Database.PutParameter(dbCommand,"?CallSid", phoneCall.CallSid);
                
                  	Database.PutParameter(dbCommand,"?AccountSid", phoneCall.AccountSid);
                
                  	Database.PutParameter(dbCommand,"?PhoneFrom", phoneCall.PhoneFrom);
                
                  	Database.PutParameter(dbCommand,"?PhoneTo", phoneCall.PhoneTo);
                
                  	Database.PutParameter(dbCommand,"?CallStatus", phoneCall.CallStatus);
                
                  	Database.PutParameter(dbCommand,"?ApiVersion", phoneCall.ApiVersion);
                
                  	Database.PutParameter(dbCommand,"?Direction", phoneCall.Direction);
                
                  	Database.PutParameter(dbCommand,"?ForwardedFrom", phoneCall.ForwardedFrom);
                
                  	Database.PutParameter(dbCommand,"?FromCity", phoneCall.FromCity);
                
                  	Database.PutParameter(dbCommand,"?FromState", phoneCall.FromState);
                
                  	Database.PutParameter(dbCommand,"?FromZip", phoneCall.FromZip);
                
                  	Database.PutParameter(dbCommand,"?FromCountry", phoneCall.FromCountry);
                
                  	Database.PutParameter(dbCommand,"?ToCity", phoneCall.ToCity);
                
                  	Database.PutParameter(dbCommand,"?ToState", phoneCall.ToState);
                
                  	Database.PutParameter(dbCommand,"?ToZip", phoneCall.ToZip);
                
                  	Database.PutParameter(dbCommand,"?ToCountry", phoneCall.ToCountry);
                
                  	Database.PutParameter(dbCommand,"?CallDuration", phoneCall.CallDuration);
                
                  	Database.PutParameter(dbCommand,"?RecordingUrl", phoneCall.RecordingUrl);
                
                  	Database.PutParameter(dbCommand,"?CallerName", phoneCall.CallerName);
                
                  	Database.PutParameter(dbCommand,"?LeadSourceId", phoneCall.LeadSourceId);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", phoneCall.DateCreated);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",phoneCall.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?IsAnsweredByUser",phoneCall.IsAnsweredByUser);
                
                	Database.UpdateParameter(dbCommand,"?AnsweredByUserId",phoneCall.AnsweredByUserId);
                
                	Database.UpdateParameter(dbCommand,"?CallSid",phoneCall.CallSid);
                
                	Database.UpdateParameter(dbCommand,"?AccountSid",phoneCall.AccountSid);
                
                	Database.UpdateParameter(dbCommand,"?PhoneFrom",phoneCall.PhoneFrom);
                
                	Database.UpdateParameter(dbCommand,"?PhoneTo",phoneCall.PhoneTo);
                
                	Database.UpdateParameter(dbCommand,"?CallStatus",phoneCall.CallStatus);
                
                	Database.UpdateParameter(dbCommand,"?ApiVersion",phoneCall.ApiVersion);
                
                	Database.UpdateParameter(dbCommand,"?Direction",phoneCall.Direction);
                
                	Database.UpdateParameter(dbCommand,"?ForwardedFrom",phoneCall.ForwardedFrom);
                
                	Database.UpdateParameter(dbCommand,"?FromCity",phoneCall.FromCity);
                
                	Database.UpdateParameter(dbCommand,"?FromState",phoneCall.FromState);
                
                	Database.UpdateParameter(dbCommand,"?FromZip",phoneCall.FromZip);
                
                	Database.UpdateParameter(dbCommand,"?FromCountry",phoneCall.FromCountry);
                
                	Database.UpdateParameter(dbCommand,"?ToCity",phoneCall.ToCity);
                
                	Database.UpdateParameter(dbCommand,"?ToState",phoneCall.ToState);
                
                	Database.UpdateParameter(dbCommand,"?ToZip",phoneCall.ToZip);
                
                	Database.UpdateParameter(dbCommand,"?ToCountry",phoneCall.ToCountry);
                
                	Database.UpdateParameter(dbCommand,"?CallDuration",phoneCall.CallDuration);
                
                	Database.UpdateParameter(dbCommand,"?RecordingUrl",phoneCall.RecordingUrl);
                
                	Database.UpdateParameter(dbCommand,"?CallerName",phoneCall.CallerName);
                
                	Database.UpdateParameter(dbCommand,"?LeadSourceId",phoneCall.LeadSourceId);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",phoneCall.DateCreated);
                
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
          
            + " IsAnsweredByUser = ?IsAnsweredByUser, "
          
            + " AnsweredByUserId = ?AnsweredByUserId, "
          
            + " CallSid = ?CallSid, "
          
            + " AccountSid = ?AccountSid, "
          
            + " PhoneFrom = ?PhoneFrom, "
          
            + " PhoneTo = ?PhoneTo, "
          
            + " CallStatus = ?CallStatus, "
          
            + " ApiVersion = ?ApiVersion, "
          
            + " Direction = ?Direction, "
          
            + " ForwardedFrom = ?ForwardedFrom, "
          
            + " FromCity = ?FromCity, "
          
            + " FromState = ?FromState, "
          
            + " FromZip = ?FromZip, "
          
            + " FromCountry = ?FromCountry, "
          
            + " ToCity = ?ToCity, "
          
            + " ToState = ?ToState, "
          
            + " ToZip = ?ToZip, "
          
            + " ToCountry = ?ToCountry, "
          
            + " CallDuration = ?CallDuration, "
          
            + " RecordingUrl = ?RecordingUrl, "
          
            + " CallerName = ?CallerName, "
          
            + " LeadSourceId = ?LeadSourceId, "
          
            + " DateCreated = ?DateCreated "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(PhoneCall phoneCall, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", phoneCall.Id);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", phoneCall.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?IsAnsweredByUser", phoneCall.IsAnsweredByUser);
            
            	Database.PutParameter(dbCommand,"?AnsweredByUserId", phoneCall.AnsweredByUserId);
            
            	Database.PutParameter(dbCommand,"?CallSid", phoneCall.CallSid);
            
            	Database.PutParameter(dbCommand,"?AccountSid", phoneCall.AccountSid);
            
            	Database.PutParameter(dbCommand,"?PhoneFrom", phoneCall.PhoneFrom);
            
            	Database.PutParameter(dbCommand,"?PhoneTo", phoneCall.PhoneTo);
            
            	Database.PutParameter(dbCommand,"?CallStatus", phoneCall.CallStatus);
            
            	Database.PutParameter(dbCommand,"?ApiVersion", phoneCall.ApiVersion);
            
            	Database.PutParameter(dbCommand,"?Direction", phoneCall.Direction);
            
            	Database.PutParameter(dbCommand,"?ForwardedFrom", phoneCall.ForwardedFrom);
            
            	Database.PutParameter(dbCommand,"?FromCity", phoneCall.FromCity);
            
            	Database.PutParameter(dbCommand,"?FromState", phoneCall.FromState);
            
            	Database.PutParameter(dbCommand,"?FromZip", phoneCall.FromZip);
            
            	Database.PutParameter(dbCommand,"?FromCountry", phoneCall.FromCountry);
            
            	Database.PutParameter(dbCommand,"?ToCity", phoneCall.ToCity);
            
            	Database.PutParameter(dbCommand,"?ToState", phoneCall.ToState);
            
            	Database.PutParameter(dbCommand,"?ToZip", phoneCall.ToZip);
            
            	Database.PutParameter(dbCommand,"?ToCountry", phoneCall.ToCountry);
            
            	Database.PutParameter(dbCommand,"?CallDuration", phoneCall.CallDuration);
            
            	Database.PutParameter(dbCommand,"?RecordingUrl", phoneCall.RecordingUrl);
            
            	Database.PutParameter(dbCommand,"?CallerName", phoneCall.CallerName);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", phoneCall.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", phoneCall.DateCreated);
            
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
        
          + " IsAnsweredByUser, "
        
          + " AnsweredByUserId, "
        
          + " CallSid, "
        
          + " AccountSid, "
        
          + " PhoneFrom, "
        
          + " PhoneTo, "
        
          + " CallStatus, "
        
          + " ApiVersion, "
        
          + " Direction, "
        
          + " ForwardedFrom, "
        
          + " FromCity, "
        
          + " FromState, "
        
          + " FromZip, "
        
          + " FromCountry, "
        
          + " ToCity, "
        
          + " ToState, "
        
          + " ToZip, "
        
          + " ToCountry, "
        
          + " CallDuration, "
        
          + " RecordingUrl, "
        
          + " CallerName, "
        
          + " LeadSourceId, "
        
          + " DateCreated "
        
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
                    phoneCall.IsAnsweredByUser = dataReader.GetBoolean(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    phoneCall.AnsweredByUserId = dataReader.GetInt32(3 + offset);
                  phoneCall.CallSid = dataReader.GetString(4 + offset);
                  phoneCall.AccountSid = dataReader.GetString(5 + offset);
                  phoneCall.PhoneFrom = dataReader.GetString(6 + offset);
                  phoneCall.PhoneTo = dataReader.GetString(7 + offset);
                  phoneCall.CallStatus = dataReader.GetString(8 + offset);
                  phoneCall.ApiVersion = dataReader.GetString(9 + offset);
                  phoneCall.Direction = dataReader.GetString(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    phoneCall.ForwardedFrom = dataReader.GetString(11 + offset);
                  
                    if(!dataReader.IsDBNull(12 + offset))
                    phoneCall.FromCity = dataReader.GetString(12 + offset);
                  
                    if(!dataReader.IsDBNull(13 + offset))
                    phoneCall.FromState = dataReader.GetString(13 + offset);
                  
                    if(!dataReader.IsDBNull(14 + offset))
                    phoneCall.FromZip = dataReader.GetString(14 + offset);
                  
                    if(!dataReader.IsDBNull(15 + offset))
                    phoneCall.FromCountry = dataReader.GetString(15 + offset);
                  
                    if(!dataReader.IsDBNull(16 + offset))
                    phoneCall.ToCity = dataReader.GetString(16 + offset);
                  
                    if(!dataReader.IsDBNull(17 + offset))
                    phoneCall.ToState = dataReader.GetString(17 + offset);
                  
                    if(!dataReader.IsDBNull(18 + offset))
                    phoneCall.ToZip = dataReader.GetString(18 + offset);
                  
                    if(!dataReader.IsDBNull(19 + offset))
                    phoneCall.ToCountry = dataReader.GetString(19 + offset);
                  
                    if(!dataReader.IsDBNull(20 + offset))
                    phoneCall.CallDuration = dataReader.GetString(20 + offset);
                  
                    if(!dataReader.IsDBNull(21 + offset))
                    phoneCall.RecordingUrl = dataReader.GetString(21 + offset);
                  
                    if(!dataReader.IsDBNull(22 + offset))
                    phoneCall.CallerName = dataReader.GetString(22 + offset);
                  
                    if(!dataReader.IsDBNull(23 + offset))
                    phoneCall.LeadSourceId = dataReader.GetInt32(23 + offset);
                  phoneCall.DateCreated = dataReader.GetDateTime(24 + offset);
                  

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
              
                + " IsAnsweredByUser, "
              
                + " AnsweredByUserId, "
              
                + " CallSid, "
              
                + " AccountSid, "
              
                + " PhoneFrom, "
              
                + " PhoneTo, "
              
                + " CallStatus, "
              
                + " ApiVersion, "
              
                + " Direction, "
              
                + " ForwardedFrom, "
              
                + " FromCity, "
              
                + " FromState, "
              
                + " FromZip, "
              
                + " FromCountry, "
              
                + " ToCity, "
              
                + " ToState, "
              
                + " ToZip, "
              
                + " ToCountry, "
              
                + " CallDuration, "
              
                + " RecordingUrl, "
              
                + " CallerName, "
              
                + " LeadSourceId, "
              
                + " DateCreated "
              
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
              
        protected bool m_isAnsweredByUser;
              
        protected int? m_answeredByUserId;
              
        protected String m_callSid;
              
        protected String m_accountSid;
              
        protected String m_phoneFrom;
              
        protected String m_phoneTo;
              
        protected String m_callStatus;
              
        protected String m_apiVersion;
              
        protected String m_direction;
              
        protected String m_forwardedFrom;
              
        protected String m_fromCity;
              
        protected String m_fromState;
              
        protected String m_fromZip;
              
        protected String m_fromCountry;
              
        protected String m_toCity;
              
        protected String m_toState;
              
        protected String m_toZip;
              
        protected String m_toCountry;
              
        protected String m_callDuration;
              
        protected String m_recordingUrl;
              
        protected String m_callerName;
              
        protected int? m_leadSourceId;
              
        protected DateTime m_dateCreated;
              
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
                  trackingPhoneId,bool 
                  isAnsweredByUser,int? 
                  answeredByUserId,String 
                  callSid,String 
                  accountSid,String 
                  phoneFrom,String 
                  phoneTo,String 
                  callStatus,String 
                  apiVersion,String 
                  direction,String 
                  forwardedFrom,String 
                  fromCity,String 
                  fromState,String 
                  fromZip,String 
                  fromCountry,String 
                  toCity,String 
                  toState,String 
                  toZip,String 
                  toCountry,String 
                  callDuration,String 
                  recordingUrl,String 
                  callerName,int? 
                  leadSourceId,DateTime 
                  dateCreated
                ) : this()
        {
            
        	m_id = id;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_isAnsweredByUser = isAnsweredByUser;
            
        	m_answeredByUserId = answeredByUserId;
            
        	m_callSid = callSid;
            
        	m_accountSid = accountSid;
            
        	m_phoneFrom = phoneFrom;
            
        	m_phoneTo = phoneTo;
            
        	m_callStatus = callStatus;
            
        	m_apiVersion = apiVersion;
            
        	m_direction = direction;
            
        	m_forwardedFrom = forwardedFrom;
            
        	m_fromCity = fromCity;
            
        	m_fromState = fromState;
            
        	m_fromZip = fromZip;
            
        	m_fromCountry = fromCountry;
            
        	m_toCity = toCity;
            
        	m_toState = toState;
            
        	m_toZip = toZip;
            
        	m_toCountry = toCountry;
            
        	m_callDuration = callDuration;
            
        	m_recordingUrl = recordingUrl;
            
        	m_callerName = callerName;
            
        	m_leadSourceId = leadSourceId;
            
        	m_dateCreated = dateCreated;
            
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
        
        public bool IsAnsweredByUser
        {
        	get { return m_isAnsweredByUser;}
            set { m_isAnsweredByUser = value; }
        }
        
        public int? AnsweredByUserId
        {
        	get { return m_answeredByUserId;}
            set { m_answeredByUserId = value; }
        }
        
        public String CallSid
        {
        	get { return m_callSid;}
            set { m_callSid = value; }
        }
        
        public String AccountSid
        {
        	get { return m_accountSid;}
            set { m_accountSid = value; }
        }
        
        public String PhoneFrom
        {
        	get { return m_phoneFrom;}
            set { m_phoneFrom = value; }
        }
        
        public String PhoneTo
        {
        	get { return m_phoneTo;}
            set { m_phoneTo = value; }
        }
        
        public String CallStatus
        {
        	get { return m_callStatus;}
            set { m_callStatus = value; }
        }
        
        public String ApiVersion
        {
        	get { return m_apiVersion;}
            set { m_apiVersion = value; }
        }
        
        public String Direction
        {
        	get { return m_direction;}
            set { m_direction = value; }
        }
        
        public String ForwardedFrom
        {
        	get { return m_forwardedFrom;}
            set { m_forwardedFrom = value; }
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
        
        public String ToCity
        {
        	get { return m_toCity;}
            set { m_toCity = value; }
        }
        
        public String ToState
        {
        	get { return m_toState;}
            set { m_toState = value; }
        }
        
        public String ToZip
        {
        	get { return m_toZip;}
            set { m_toZip = value; }
        }
        
        public String ToCountry
        {
        	get { return m_toCountry;}
            set { m_toCountry = value; }
        }
        
        public String CallDuration
        {
        	get { return m_callDuration;}
            set { m_callDuration = value; }
        }
        
        public String RecordingUrl
        {
        	get { return m_recordingUrl;}
            set { m_recordingUrl = value; }
        }
        
        public String CallerName
        {
        	get { return m_callerName;}
            set { m_callerName = value; }
        }
        
        public int? LeadSourceId
        {
        	get { return m_leadSourceId;}
            set { m_leadSourceId = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 25; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    