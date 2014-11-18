
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class TrackingPhone : ICloneable
    {

        #region Store


        #region Save

        public static TrackingPhone Save(TrackingPhone trackingPhone, IDbConnection connection)
        {
        	if (!Exists(trackingPhone, connection))
        		Insert(trackingPhone, connection);
        	else
        		Update(trackingPhone, connection);
        	return trackingPhone;
        }

        public static TrackingPhone Save(TrackingPhone trackingPhone)
        {
        	if (!Exists(trackingPhone))
        		Insert(trackingPhone);
        	else
        		Update(trackingPhone);
        	return trackingPhone;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into TrackingPhone ( " +
        
          " PhoneNumber, " +
        
          " FriendlyNumber, " +
        
          " Description, " +
        
          " RedirectPhoneNumber, " +
        
          " TwilioNumberId, " +
        
          " DateCreated, " +
        
          " BusinessPartnerId, " +
        
          " IsSuspended, " +
        
          " IsRemoved, " +
        
          " CallerIdLookup, " +
        
          " TranscribeCalls, " +
        
          " IsTollFree " +
        
        ") Values (" +
        
          " ?PhoneNumber, " +
        
          " ?FriendlyNumber, " +
        
          " ?Description, " +
        
          " ?RedirectPhoneNumber, " +
        
          " ?TwilioNumberId, " +
        
          " ?DateCreated, " +
        
          " ?BusinessPartnerId, " +
        
          " ?IsSuspended, " +
        
          " ?IsRemoved, " +
        
          " ?CallerIdLookup, " +
        
          " ?TranscribeCalls, " +
        
          " ?IsTollFree " +
        
        ")";

        public static void Insert(TrackingPhone trackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", trackingPhone.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?FriendlyNumber", trackingPhone.FriendlyNumber);
            
            	Database.PutParameter(dbCommand,"?Description", trackingPhone.Description);
            
            	Database.PutParameter(dbCommand,"?RedirectPhoneNumber", trackingPhone.RedirectPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?TwilioNumberId", trackingPhone.TwilioNumberId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", trackingPhone.DateCreated);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", trackingPhone.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?IsSuspended", trackingPhone.IsSuspended);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", trackingPhone.IsRemoved);
            
            	Database.PutParameter(dbCommand,"?CallerIdLookup", trackingPhone.CallerIdLookup);
            
            	Database.PutParameter(dbCommand,"?TranscribeCalls", trackingPhone.TranscribeCalls);
            
            	Database.PutParameter(dbCommand,"?IsTollFree", trackingPhone.IsTollFree);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		trackingPhone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(TrackingPhone trackingPhone)
        {
          	Insert(trackingPhone, null);
        }

        public static void Insert(List<TrackingPhone>  trackingPhoneList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(TrackingPhone trackingPhone in  trackingPhoneList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?PhoneNumber", trackingPhone.PhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?FriendlyNumber", trackingPhone.FriendlyNumber);
                
                  	Database.PutParameter(dbCommand,"?Description", trackingPhone.Description);
                
                  	Database.PutParameter(dbCommand,"?RedirectPhoneNumber", trackingPhone.RedirectPhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?TwilioNumberId", trackingPhone.TwilioNumberId);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", trackingPhone.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?BusinessPartnerId", trackingPhone.BusinessPartnerId);
                
                  	Database.PutParameter(dbCommand,"?IsSuspended", trackingPhone.IsSuspended);
                
                  	Database.PutParameter(dbCommand,"?IsRemoved", trackingPhone.IsRemoved);
                
                  	Database.PutParameter(dbCommand,"?CallerIdLookup", trackingPhone.CallerIdLookup);
                
                  	Database.PutParameter(dbCommand,"?TranscribeCalls", trackingPhone.TranscribeCalls);
                
                  	Database.PutParameter(dbCommand,"?IsTollFree", trackingPhone.IsTollFree);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?PhoneNumber",trackingPhone.PhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?FriendlyNumber",trackingPhone.FriendlyNumber);
                
                	Database.UpdateParameter(dbCommand,"?Description",trackingPhone.Description);
                
                	Database.UpdateParameter(dbCommand,"?RedirectPhoneNumber",trackingPhone.RedirectPhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?TwilioNumberId",trackingPhone.TwilioNumberId);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",trackingPhone.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?BusinessPartnerId",trackingPhone.BusinessPartnerId);
                
                	Database.UpdateParameter(dbCommand,"?IsSuspended",trackingPhone.IsSuspended);
                
                	Database.UpdateParameter(dbCommand,"?IsRemoved",trackingPhone.IsRemoved);
                
                	Database.UpdateParameter(dbCommand,"?CallerIdLookup",trackingPhone.CallerIdLookup);
                
                	Database.UpdateParameter(dbCommand,"?TranscribeCalls",trackingPhone.TranscribeCalls);
                
                	Database.UpdateParameter(dbCommand,"?IsTollFree",trackingPhone.IsTollFree);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	trackingPhone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<TrackingPhone>  trackingPhoneList)
        {
        	Insert(trackingPhoneList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update TrackingPhone Set "
          
            + " PhoneNumber = ?PhoneNumber, "
          
            + " FriendlyNumber = ?FriendlyNumber, "
          
            + " Description = ?Description, "
          
            + " RedirectPhoneNumber = ?RedirectPhoneNumber, "
          
            + " TwilioNumberId = ?TwilioNumberId, "
          
            + " DateCreated = ?DateCreated, "
          
            + " BusinessPartnerId = ?BusinessPartnerId, "
          
            + " IsSuspended = ?IsSuspended, "
          
            + " IsRemoved = ?IsRemoved, "
          
            + " CallerIdLookup = ?CallerIdLookup, "
          
            + " TranscribeCalls = ?TranscribeCalls, "
          
            + " IsTollFree = ?IsTollFree "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(TrackingPhone trackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", trackingPhone.Id);
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", trackingPhone.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?FriendlyNumber", trackingPhone.FriendlyNumber);
            
            	Database.PutParameter(dbCommand,"?Description", trackingPhone.Description);
            
            	Database.PutParameter(dbCommand,"?RedirectPhoneNumber", trackingPhone.RedirectPhoneNumber);
            
            	Database.PutParameter(dbCommand,"?TwilioNumberId", trackingPhone.TwilioNumberId);
            
            	Database.PutParameter(dbCommand,"?DateCreated", trackingPhone.DateCreated);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", trackingPhone.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?IsSuspended", trackingPhone.IsSuspended);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", trackingPhone.IsRemoved);
            
            	Database.PutParameter(dbCommand,"?CallerIdLookup", trackingPhone.CallerIdLookup);
            
            	Database.PutParameter(dbCommand,"?TranscribeCalls", trackingPhone.TranscribeCalls);
            
            	Database.PutParameter(dbCommand,"?IsTollFree", trackingPhone.IsTollFree);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(TrackingPhone trackingPhone)
        {
          	Update(trackingPhone, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " PhoneNumber, "
        
          + " FriendlyNumber, "
        
          + " Description, "
        
          + " RedirectPhoneNumber, "
        
          + " TwilioNumberId, "
        
          + " DateCreated, "
        
          + " BusinessPartnerId, "
        
          + " IsSuspended, "
        
          + " IsRemoved, "
        
          + " CallerIdLookup, "
        
          + " TranscribeCalls, "
        
          + " IsTollFree "
        
          + " From TrackingPhone "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static TrackingPhone FindByPrimaryKey(
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

            throw new DataNotFoundException("TrackingPhone not found, search by primary key");
        }

        public static TrackingPhone FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(TrackingPhone trackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",trackingPhone.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(TrackingPhone trackingPhone)
        {
        	return Exists(trackingPhone, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from TrackingPhone limit 1";

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

        public static TrackingPhone Load(IDataReader dataReader, int offset)
        {
              TrackingPhone trackingPhone = new TrackingPhone();

              trackingPhone.Id = dataReader.GetInt32(0 + offset);
                  trackingPhone.PhoneNumber = dataReader.GetString(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    trackingPhone.FriendlyNumber = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    trackingPhone.Description = dataReader.GetString(3 + offset);
                  trackingPhone.RedirectPhoneNumber = dataReader.GetString(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    trackingPhone.TwilioNumberId = dataReader.GetString(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    trackingPhone.DateCreated = dataReader.GetDateTime(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    trackingPhone.BusinessPartnerId = dataReader.GetInt32(7 + offset);
                  trackingPhone.IsSuspended = dataReader.GetBoolean(8 + offset);
                  trackingPhone.IsRemoved = dataReader.GetBoolean(9 + offset);
                  trackingPhone.CallerIdLookup = dataReader.GetBoolean(10 + offset);
                  trackingPhone.TranscribeCalls = dataReader.GetBoolean(11 + offset);
                  trackingPhone.IsTollFree = dataReader.GetBoolean(12 + offset);
                  

            return trackingPhone;
        }

        public static TrackingPhone Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From TrackingPhone "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(TrackingPhone trackingPhone, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", trackingPhone.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(TrackingPhone trackingPhone)
        {
        	Delete(trackingPhone, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From TrackingPhone ";

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
              
                + " PhoneNumber, "
              
                + " FriendlyNumber, "
              
                + " Description, "
              
                + " RedirectPhoneNumber, "
              
                + " TwilioNumberId, "
              
                + " DateCreated, "
              
                + " BusinessPartnerId, "
              
                + " IsSuspended, "
              
                + " IsRemoved, "
              
                + " CallerIdLookup, "
              
                + " TranscribeCalls, "
              
                + " IsTollFree "
              
                + " From TrackingPhone ";

        public static List<TrackingPhone> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<TrackingPhone> rv = new List<TrackingPhone>();

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

        public static List<TrackingPhone> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<TrackingPhone> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<TrackingPhone> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhone));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(TrackingPhone item in itemsList)
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

        public static List<TrackingPhone> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhone));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<TrackingPhone> itemsList = new List<TrackingPhone>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is TrackingPhone)
              		itemsList.Add(deserializedObject as TrackingPhone);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_phoneNumber;
              
        protected String m_friendlyNumber;
              
        protected String m_description;
              
        protected String m_redirectPhoneNumber;
              
        protected String m_twilioNumberId;
              
        protected DateTime? m_dateCreated;
              
        protected int? m_businessPartnerId;
              
        protected bool m_isSuspended;
              
        protected bool m_isRemoved;
              
        protected bool m_callerIdLookup;
              
        protected bool m_transcribeCalls;
              
        protected bool m_isTollFree;
              
        #endregion

        #region Constructors

        public TrackingPhone(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public TrackingPhone(
                int 
                  id,String 
                  phoneNumber,String 
                  friendlyNumber,String 
                  description,String 
                  redirectPhoneNumber,String 
                  twilioNumberId,DateTime? 
                  dateCreated,int? 
                  businessPartnerId,bool 
                  isSuspended,bool 
                  isRemoved,bool 
                  callerIdLookup,bool 
                  transcribeCalls,bool 
                  isTollFree
                ) : this()
        {
            
        	m_id = id;
            
        	m_phoneNumber = phoneNumber;
            
        	m_friendlyNumber = friendlyNumber;
            
        	m_description = description;
            
        	m_redirectPhoneNumber = redirectPhoneNumber;
            
        	m_twilioNumberId = twilioNumberId;
            
        	m_dateCreated = dateCreated;
            
        	m_businessPartnerId = businessPartnerId;
            
        	m_isSuspended = isSuspended;
            
        	m_isRemoved = isRemoved;
            
        	m_callerIdLookup = callerIdLookup;
            
        	m_transcribeCalls = transcribeCalls;
            
        	m_isTollFree = isTollFree;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String PhoneNumber
        {
        	get { return m_phoneNumber;}
            set { m_phoneNumber = value; }
        }
        
        public String FriendlyNumber
        {
        	get { return m_friendlyNumber;}
            set { m_friendlyNumber = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        
        public String RedirectPhoneNumber
        {
        	get { return m_redirectPhoneNumber;}
            set { m_redirectPhoneNumber = value; }
        }
        
        public String TwilioNumberId
        {
        	get { return m_twilioNumberId;}
            set { m_twilioNumberId = value; }
        }
        
        public DateTime? DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public int? BusinessPartnerId
        {
        	get { return m_businessPartnerId;}
            set { m_businessPartnerId = value; }
        }
        
        public bool IsSuspended
        {
        	get { return m_isSuspended;}
            set { m_isSuspended = value; }
        }
        
        public bool IsRemoved
        {
        	get { return m_isRemoved;}
            set { m_isRemoved = value; }
        }
        
        public bool CallerIdLookup
        {
        	get { return m_callerIdLookup;}
            set { m_callerIdLookup = value; }
        }
        
        public bool TranscribeCalls
        {
        	get { return m_transcribeCalls;}
            set { m_transcribeCalls = value; }
        }
        
        public bool IsTollFree
        {
        	get { return m_isTollFree;}
            set { m_isTollFree = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 13; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    