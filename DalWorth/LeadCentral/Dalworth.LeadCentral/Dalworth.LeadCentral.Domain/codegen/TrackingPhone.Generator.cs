
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
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
        
          " Number, " +
        
          " TwilioId, " +
        
          " Description, " +
        
          " IsTollFree, " +
        
          " IsSuspended, " +
        
          " IsRemoved, " +
        
          " ScreenNumber, " +
        
          " TimeLastDisplay, " +
        
          " SmsResponse, " +
        
          " DenyTranscription, " +
        
          " DenyCallerId " +
        
        ") Values (" +
        
          " ?Number, " +
        
          " ?TwilioId, " +
        
          " ?Description, " +
        
          " ?IsTollFree, " +
        
          " ?IsSuspended, " +
        
          " ?IsRemoved, " +
        
          " ?ScreenNumber, " +
        
          " ?TimeLastDisplay, " +
        
          " ?SmsResponse, " +
        
          " ?DenyTranscription, " +
        
          " ?DenyCallerId " +
        
        ")";

        public static void Insert(TrackingPhone trackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Number", trackingPhone.Number);
            
            	Database.PutParameter(dbCommand,"?TwilioId", trackingPhone.TwilioId);
            
            	Database.PutParameter(dbCommand,"?Description", trackingPhone.Description);
            
            	Database.PutParameter(dbCommand,"?IsTollFree", trackingPhone.IsTollFree);
            
            	Database.PutParameter(dbCommand,"?IsSuspended", trackingPhone.IsSuspended);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", trackingPhone.IsRemoved);
            
            	Database.PutParameter(dbCommand,"?ScreenNumber", trackingPhone.ScreenNumber);
            
            	Database.PutParameter(dbCommand,"?TimeLastDisplay", trackingPhone.TimeLastDisplay);
            
            	Database.PutParameter(dbCommand,"?SmsResponse", trackingPhone.SmsResponse);
            
            	Database.PutParameter(dbCommand,"?DenyTranscription", trackingPhone.DenyTranscription);
            
            	Database.PutParameter(dbCommand,"?DenyCallerId", trackingPhone.DenyCallerId);
            
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
                
                  	Database.PutParameter(dbCommand,"?Number", trackingPhone.Number);
                
                  	Database.PutParameter(dbCommand,"?TwilioId", trackingPhone.TwilioId);
                
                  	Database.PutParameter(dbCommand,"?Description", trackingPhone.Description);
                
                  	Database.PutParameter(dbCommand,"?IsTollFree", trackingPhone.IsTollFree);
                
                  	Database.PutParameter(dbCommand,"?IsSuspended", trackingPhone.IsSuspended);
                
                  	Database.PutParameter(dbCommand,"?IsRemoved", trackingPhone.IsRemoved);
                
                  	Database.PutParameter(dbCommand,"?ScreenNumber", trackingPhone.ScreenNumber);
                
                  	Database.PutParameter(dbCommand,"?TimeLastDisplay", trackingPhone.TimeLastDisplay);
                
                  	Database.PutParameter(dbCommand,"?SmsResponse", trackingPhone.SmsResponse);
                
                  	Database.PutParameter(dbCommand,"?DenyTranscription", trackingPhone.DenyTranscription);
                
                  	Database.PutParameter(dbCommand,"?DenyCallerId", trackingPhone.DenyCallerId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?Number",trackingPhone.Number);
                
                	Database.UpdateParameter(dbCommand,"?TwilioId",trackingPhone.TwilioId);
                
                	Database.UpdateParameter(dbCommand,"?Description",trackingPhone.Description);
                
                	Database.UpdateParameter(dbCommand,"?IsTollFree",trackingPhone.IsTollFree);
                
                	Database.UpdateParameter(dbCommand,"?IsSuspended",trackingPhone.IsSuspended);
                
                	Database.UpdateParameter(dbCommand,"?IsRemoved",trackingPhone.IsRemoved);
                
                	Database.UpdateParameter(dbCommand,"?ScreenNumber",trackingPhone.ScreenNumber);
                
                	Database.UpdateParameter(dbCommand,"?TimeLastDisplay",trackingPhone.TimeLastDisplay);
                
                	Database.UpdateParameter(dbCommand,"?SmsResponse",trackingPhone.SmsResponse);
                
                	Database.UpdateParameter(dbCommand,"?DenyTranscription",trackingPhone.DenyTranscription);
                
                	Database.UpdateParameter(dbCommand,"?DenyCallerId",trackingPhone.DenyCallerId);
                
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
          
            + " Number = ?Number, "
          
            + " TwilioId = ?TwilioId, "
          
            + " Description = ?Description, "
          
            + " IsTollFree = ?IsTollFree, "
          
            + " IsSuspended = ?IsSuspended, "
          
            + " IsRemoved = ?IsRemoved, "
          
            + " ScreenNumber = ?ScreenNumber, "
          
            + " TimeLastDisplay = ?TimeLastDisplay, "
          
            + " SmsResponse = ?SmsResponse, "
          
            + " DenyTranscription = ?DenyTranscription, "
          
            + " DenyCallerId = ?DenyCallerId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(TrackingPhone trackingPhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", trackingPhone.Id);
            
            	Database.PutParameter(dbCommand,"?Number", trackingPhone.Number);
            
            	Database.PutParameter(dbCommand,"?TwilioId", trackingPhone.TwilioId);
            
            	Database.PutParameter(dbCommand,"?Description", trackingPhone.Description);
            
            	Database.PutParameter(dbCommand,"?IsTollFree", trackingPhone.IsTollFree);
            
            	Database.PutParameter(dbCommand,"?IsSuspended", trackingPhone.IsSuspended);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", trackingPhone.IsRemoved);
            
            	Database.PutParameter(dbCommand,"?ScreenNumber", trackingPhone.ScreenNumber);
            
            	Database.PutParameter(dbCommand,"?TimeLastDisplay", trackingPhone.TimeLastDisplay);
            
            	Database.PutParameter(dbCommand,"?SmsResponse", trackingPhone.SmsResponse);
            
            	Database.PutParameter(dbCommand,"?DenyTranscription", trackingPhone.DenyTranscription);
            
            	Database.PutParameter(dbCommand,"?DenyCallerId", trackingPhone.DenyCallerId);
            
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
        
          + " Number, "
        
          + " TwilioId, "
        
          + " Description, "
        
          + " IsTollFree, "
        
          + " IsSuspended, "
        
          + " IsRemoved, "
        
          + " ScreenNumber, "
        
          + " TimeLastDisplay, "
        
          + " SmsResponse, "
        
          + " DenyTranscription, "
        
          + " DenyCallerId "
        
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
                  trackingPhone.Number = dataReader.GetString(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    trackingPhone.TwilioId = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    trackingPhone.Description = dataReader.GetString(3 + offset);
                  trackingPhone.IsTollFree = dataReader.GetBoolean(4 + offset);
                  trackingPhone.IsSuspended = dataReader.GetBoolean(5 + offset);
                  trackingPhone.IsRemoved = dataReader.GetBoolean(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    trackingPhone.ScreenNumber = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    trackingPhone.TimeLastDisplay = dataReader.GetDateTime(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    trackingPhone.SmsResponse = dataReader.GetString(9 + offset);
                  trackingPhone.DenyTranscription = dataReader.GetBoolean(10 + offset);
                  trackingPhone.DenyCallerId = dataReader.GetBoolean(11 + offset);
                  

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
              
                + " Number, "
              
                + " TwilioId, "
              
                + " Description, "
              
                + " IsTollFree, "
              
                + " IsSuspended, "
              
                + " IsRemoved, "
              
                + " ScreenNumber, "
              
                + " TimeLastDisplay, "
              
                + " SmsResponse, "
              
                + " DenyTranscription, "
              
                + " DenyCallerId "
              
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
              
        protected String m_number;
              
        protected String m_twilioId;
              
        protected String m_description;
              
        protected bool m_isTollFree;
              
        protected bool m_isSuspended;
              
        protected bool m_isRemoved;
              
        protected String m_screenNumber;
              
        protected DateTime? m_timeLastDisplay;
              
        protected String m_smsResponse;
              
        protected bool m_denyTranscription;
              
        protected bool m_denyCallerId;
              
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
                  number,String 
                  twilioId,String 
                  description,bool 
                  isTollFree,bool 
                  isSuspended,bool 
                  isRemoved,String 
                  screenNumber,DateTime? 
                  timeLastDisplay,String 
                  smsResponse,bool 
                  denyTranscription,bool 
                  denyCallerId
                ) : this()
        {
            
        	m_id = id;
            
        	m_number = number;
            
        	m_twilioId = twilioId;
            
        	m_description = description;
            
        	m_isTollFree = isTollFree;
            
        	m_isSuspended = isSuspended;
            
        	m_isRemoved = isRemoved;
            
        	m_screenNumber = screenNumber;
            
        	m_timeLastDisplay = timeLastDisplay;
            
        	m_smsResponse = smsResponse;
            
        	m_denyTranscription = denyTranscription;
            
        	m_denyCallerId = denyCallerId;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String Number
        {
        	get { return m_number;}
            set { m_number = value; }
        }
        
        public String TwilioId
        {
        	get { return m_twilioId;}
            set { m_twilioId = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        
        public bool IsTollFree
        {
        	get { return m_isTollFree;}
            set { m_isTollFree = value; }
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
        
        public String ScreenNumber
        {
        	get { return m_screenNumber;}
            set { m_screenNumber = value; }
        }
        
        public DateTime? TimeLastDisplay
        {
        	get { return m_timeLastDisplay;}
            set { m_timeLastDisplay = value; }
        }
        
        public String SmsResponse
        {
        	get { return m_smsResponse;}
            set { m_smsResponse = value; }
        }
        
        public bool DenyTranscription
        {
        	get { return m_denyTranscription;}
            set { m_denyTranscription = value; }
        }
        
        public bool DenyCallerId
        {
        	get { return m_denyCallerId;}
            set { m_denyCallerId = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 12; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    