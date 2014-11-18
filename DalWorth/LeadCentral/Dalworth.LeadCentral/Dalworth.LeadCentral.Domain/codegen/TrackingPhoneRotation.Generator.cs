
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class TrackingPhoneRotation : ICloneable
    {

        #region Store


        #region Save

        public static TrackingPhoneRotation Save(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
        	if (!Exists(trackingPhoneRotation, connection))
        		Insert(trackingPhoneRotation, connection);
        	else
        		Update(trackingPhoneRotation, connection);
        	return trackingPhoneRotation;
        }

        public static TrackingPhoneRotation Save(TrackingPhoneRotation trackingPhoneRotation)
        {
        	if (!Exists(trackingPhoneRotation))
        		Insert(trackingPhoneRotation);
        	else
        		Update(trackingPhoneRotation);
        	return trackingPhoneRotation;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into TrackingPhoneRotation ( " +
        
          " TimeDisplay, " +
        
          " UserHostAddress, " +
        
          " ParentReferralUri, " +
        
          " ReferralUri, " +
        
          " SessionIdUid, " +
        
          " TrackingPhoneId, " +
        
          " PhoneCallId, " +
        
          " PhoneSmsId, " +
        
          " LeadFormId, " +
        
          " LeadSourceId " +
        
        ") Values (" +
        
          " ?TimeDisplay, " +
        
          " ?UserHostAddress, " +
        
          " ?ParentReferralUri, " +
        
          " ?ReferralUri, " +
        
          " ?SessionIdUid, " +
        
          " ?TrackingPhoneId, " +
        
          " ?PhoneCallId, " +
        
          " ?PhoneSmsId, " +
        
          " ?LeadFormId, " +
        
          " ?LeadSourceId " +
        
        ")";

        public static void Insert(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TimeDisplay", trackingPhoneRotation.TimeDisplay);
            
            	Database.PutParameter(dbCommand,"?UserHostAddress", trackingPhoneRotation.UserHostAddress);
            
            	Database.PutParameter(dbCommand,"?ParentReferralUri", trackingPhoneRotation.ParentReferralUri);
            
            	Database.PutParameter(dbCommand,"?ReferralUri", trackingPhoneRotation.ReferralUri);
            
            	Database.PutParameter(dbCommand,"?SessionIdUid", trackingPhoneRotation.SessionIdUid);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneRotation.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", trackingPhoneRotation.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", trackingPhoneRotation.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?LeadFormId", trackingPhoneRotation.LeadFormId);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", trackingPhoneRotation.LeadSourceId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		trackingPhoneRotation.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(TrackingPhoneRotation trackingPhoneRotation)
        {
          	Insert(trackingPhoneRotation, null);
        }

        public static void Insert(List<TrackingPhoneRotation>  trackingPhoneRotationList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(TrackingPhoneRotation trackingPhoneRotation in  trackingPhoneRotationList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?TimeDisplay", trackingPhoneRotation.TimeDisplay);
                
                  	Database.PutParameter(dbCommand,"?UserHostAddress", trackingPhoneRotation.UserHostAddress);
                
                  	Database.PutParameter(dbCommand,"?ParentReferralUri", trackingPhoneRotation.ParentReferralUri);
                
                  	Database.PutParameter(dbCommand,"?ReferralUri", trackingPhoneRotation.ReferralUri);
                
                  	Database.PutParameter(dbCommand,"?SessionIdUid", trackingPhoneRotation.SessionIdUid);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneRotation.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?PhoneCallId", trackingPhoneRotation.PhoneCallId);
                
                  	Database.PutParameter(dbCommand,"?PhoneSmsId", trackingPhoneRotation.PhoneSmsId);
                
                  	Database.PutParameter(dbCommand,"?LeadFormId", trackingPhoneRotation.LeadFormId);
                
                  	Database.PutParameter(dbCommand,"?LeadSourceId", trackingPhoneRotation.LeadSourceId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TimeDisplay",trackingPhoneRotation.TimeDisplay);
                
                	Database.UpdateParameter(dbCommand,"?UserHostAddress",trackingPhoneRotation.UserHostAddress);
                
                	Database.UpdateParameter(dbCommand,"?ParentReferralUri",trackingPhoneRotation.ParentReferralUri);
                
                	Database.UpdateParameter(dbCommand,"?ReferralUri",trackingPhoneRotation.ReferralUri);
                
                	Database.UpdateParameter(dbCommand,"?SessionIdUid",trackingPhoneRotation.SessionIdUid);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",trackingPhoneRotation.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneCallId",trackingPhoneRotation.PhoneCallId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneSmsId",trackingPhoneRotation.PhoneSmsId);
                
                	Database.UpdateParameter(dbCommand,"?LeadFormId",trackingPhoneRotation.LeadFormId);
                
                	Database.UpdateParameter(dbCommand,"?LeadSourceId",trackingPhoneRotation.LeadSourceId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	trackingPhoneRotation.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<TrackingPhoneRotation>  trackingPhoneRotationList)
        {
        	Insert(trackingPhoneRotationList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update TrackingPhoneRotation Set "
          
            + " TimeDisplay = ?TimeDisplay, "
          
            + " UserHostAddress = ?UserHostAddress, "
          
            + " ParentReferralUri = ?ParentReferralUri, "
          
            + " ReferralUri = ?ReferralUri, "
          
            + " SessionIdUid = ?SessionIdUid, "
          
            + " TrackingPhoneId = ?TrackingPhoneId, "
          
            + " PhoneCallId = ?PhoneCallId, "
          
            + " PhoneSmsId = ?PhoneSmsId, "
          
            + " LeadFormId = ?LeadFormId, "
          
            + " LeadSourceId = ?LeadSourceId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", trackingPhoneRotation.Id);
            
            	Database.PutParameter(dbCommand,"?TimeDisplay", trackingPhoneRotation.TimeDisplay);
            
            	Database.PutParameter(dbCommand,"?UserHostAddress", trackingPhoneRotation.UserHostAddress);
            
            	Database.PutParameter(dbCommand,"?ParentReferralUri", trackingPhoneRotation.ParentReferralUri);
            
            	Database.PutParameter(dbCommand,"?ReferralUri", trackingPhoneRotation.ReferralUri);
            
            	Database.PutParameter(dbCommand,"?SessionIdUid", trackingPhoneRotation.SessionIdUid);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneRotation.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", trackingPhoneRotation.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", trackingPhoneRotation.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?LeadFormId", trackingPhoneRotation.LeadFormId);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", trackingPhoneRotation.LeadSourceId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(TrackingPhoneRotation trackingPhoneRotation)
        {
          	Update(trackingPhoneRotation, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " TimeDisplay, "
        
          + " UserHostAddress, "
        
          + " ParentReferralUri, "
        
          + " ReferralUri, "
        
          + " SessionIdUid, "
        
          + " TrackingPhoneId, "
        
          + " PhoneCallId, "
        
          + " PhoneSmsId, "
        
          + " LeadFormId, "
        
          + " LeadSourceId "
        
          + " From TrackingPhoneRotation "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static TrackingPhoneRotation FindByPrimaryKey(
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

            throw new DataNotFoundException("TrackingPhoneRotation not found, search by primary key");
        }

        public static TrackingPhoneRotation FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",trackingPhoneRotation.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(TrackingPhoneRotation trackingPhoneRotation)
        {
        	return Exists(trackingPhoneRotation, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from TrackingPhoneRotation limit 1";

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

        public static TrackingPhoneRotation Load(IDataReader dataReader, int offset)
        {
              TrackingPhoneRotation trackingPhoneRotation = new TrackingPhoneRotation();

              trackingPhoneRotation.Id = dataReader.GetInt32(0 + offset);
                  trackingPhoneRotation.TimeDisplay = dataReader.GetDateTime(1 + offset);
                  trackingPhoneRotation.UserHostAddress = dataReader.GetString(2 + offset);
                  trackingPhoneRotation.ParentReferralUri = dataReader.GetString(3 + offset);
                  trackingPhoneRotation.ReferralUri = dataReader.GetString(4 + offset);
                  trackingPhoneRotation.SessionIdUid = dataReader.GetString(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    trackingPhoneRotation.TrackingPhoneId = dataReader.GetInt32(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    trackingPhoneRotation.PhoneCallId = dataReader.GetInt32(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    trackingPhoneRotation.PhoneSmsId = dataReader.GetInt32(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    trackingPhoneRotation.LeadFormId = dataReader.GetInt32(9 + offset);
                  
                    if(!dataReader.IsDBNull(10 + offset))
                    trackingPhoneRotation.LeadSourceId = dataReader.GetInt32(10 + offset);
                  

            return trackingPhoneRotation;
        }

        public static TrackingPhoneRotation Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From TrackingPhoneRotation "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(TrackingPhoneRotation trackingPhoneRotation, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", trackingPhoneRotation.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(TrackingPhoneRotation trackingPhoneRotation)
        {
        	Delete(trackingPhoneRotation, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From TrackingPhoneRotation ";

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
              
                + " TimeDisplay, "
              
                + " UserHostAddress, "
              
                + " ParentReferralUri, "
              
                + " ReferralUri, "
              
                + " SessionIdUid, "
              
                + " TrackingPhoneId, "
              
                + " PhoneCallId, "
              
                + " PhoneSmsId, "
              
                + " LeadFormId, "
              
                + " LeadSourceId "
              
                + " From TrackingPhoneRotation ";

        public static List<TrackingPhoneRotation> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<TrackingPhoneRotation> rv = new List<TrackingPhoneRotation>();

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

        public static List<TrackingPhoneRotation> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<TrackingPhoneRotation> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<TrackingPhoneRotation> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhoneRotation));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(TrackingPhoneRotation item in itemsList)
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

        public static List<TrackingPhoneRotation> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhoneRotation));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<TrackingPhoneRotation> itemsList = new List<TrackingPhoneRotation>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is TrackingPhoneRotation)
              		itemsList.Add(deserializedObject as TrackingPhoneRotation);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected DateTime m_timeDisplay;
              
        protected String m_userHostAddress;
              
        protected String m_parentReferralUri;
              
        protected String m_referralUri;
              
        protected String m_sessionIdUid;
              
        protected int? m_trackingPhoneId;
              
        protected int? m_phoneCallId;
              
        protected int? m_phoneSmsId;
              
        protected int? m_leadFormId;
              
        protected int? m_leadSourceId;
              
        #endregion

        #region Constructors

        public TrackingPhoneRotation(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public TrackingPhoneRotation(
                int 
                  id,DateTime 
                  timeDisplay,String 
                  userHostAddress,String 
                  parentReferralUri,String 
                  referralUri,String 
                  sessionIdUid,int? 
                  trackingPhoneId,int? 
                  phoneCallId,int? 
                  phoneSmsId,int? 
                  leadFormId,int? 
                  leadSourceId
                ) : this()
        {
            
        	m_id = id;
            
        	m_timeDisplay = timeDisplay;
            
        	m_userHostAddress = userHostAddress;
            
        	m_parentReferralUri = parentReferralUri;
            
        	m_referralUri = referralUri;
            
        	m_sessionIdUid = sessionIdUid;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_phoneCallId = phoneCallId;
            
        	m_phoneSmsId = phoneSmsId;
            
        	m_leadFormId = leadFormId;
            
        	m_leadSourceId = leadSourceId;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public DateTime TimeDisplay
        {
        	get { return m_timeDisplay;}
            set { m_timeDisplay = value; }
        }
        
        public String UserHostAddress
        {
        	get { return m_userHostAddress;}
            set { m_userHostAddress = value; }
        }
        
        public String ParentReferralUri
        {
        	get { return m_parentReferralUri;}
            set { m_parentReferralUri = value; }
        }
        
        public String ReferralUri
        {
        	get { return m_referralUri;}
            set { m_referralUri = value; }
        }
        
        public String SessionIdUid
        {
        	get { return m_sessionIdUid;}
            set { m_sessionIdUid = value; }
        }
        
        public int? TrackingPhoneId
        {
        	get { return m_trackingPhoneId;}
            set { m_trackingPhoneId = value; }
        }
        
        public int? PhoneCallId
        {
        	get { return m_phoneCallId;}
            set { m_phoneCallId = value; }
        }
        
        public int? PhoneSmsId
        {
        	get { return m_phoneSmsId;}
            set { m_phoneSmsId = value; }
        }
        
        public int? LeadFormId
        {
        	get { return m_leadFormId;}
            set { m_leadFormId = value; }
        }
        
        public int? LeadSourceId
        {
        	get { return m_leadSourceId;}
            set { m_leadSourceId = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 11; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    