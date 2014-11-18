
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class LeadForm : ICloneable
    {

        #region Store


        #region Save

        public static LeadForm Save(LeadForm leadForm, IDbConnection connection)
        {
        	if (!Exists(leadForm, connection))
        		Insert(leadForm, connection);
        	else
        		Update(leadForm, connection);
        	return leadForm;
        }

        public static LeadForm Save(LeadForm leadForm)
        {
        	if (!Exists(leadForm))
        		Insert(leadForm);
        	else
        		Update(leadForm);
        	return leadForm;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into LeadForm ( " +
        
          " LeadSourceId, " +
        
          " FirstName, " +
        
          " LastName, " +
        
          " Phone, " +
        
          " Message, " +
        
          " DateCreated, " +
        
          " ReferralUri " +
        
        ") Values (" +
        
          " ?LeadSourceId, " +
        
          " ?FirstName, " +
        
          " ?LastName, " +
        
          " ?Phone, " +
        
          " ?Message, " +
        
          " ?DateCreated, " +
        
          " ?ReferralUri " +
        
        ")";

        public static void Insert(LeadForm leadForm, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadForm.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?FirstName", leadForm.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", leadForm.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", leadForm.Phone);
            
            	Database.PutParameter(dbCommand,"?Message", leadForm.Message);
            
            	Database.PutParameter(dbCommand,"?DateCreated", leadForm.DateCreated);
            
            	Database.PutParameter(dbCommand,"?ReferralUri", leadForm.ReferralUri);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		leadForm.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(LeadForm leadForm)
        {
          	Insert(leadForm, null);
        }

        public static void Insert(List<LeadForm>  leadFormList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(LeadForm leadForm in  leadFormList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?LeadSourceId", leadForm.LeadSourceId);
                
                  	Database.PutParameter(dbCommand,"?FirstName", leadForm.FirstName);
                
                  	Database.PutParameter(dbCommand,"?LastName", leadForm.LastName);
                
                  	Database.PutParameter(dbCommand,"?Phone", leadForm.Phone);
                
                  	Database.PutParameter(dbCommand,"?Message", leadForm.Message);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", leadForm.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?ReferralUri", leadForm.ReferralUri);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadSourceId",leadForm.LeadSourceId);
                
                	Database.UpdateParameter(dbCommand,"?FirstName",leadForm.FirstName);
                
                	Database.UpdateParameter(dbCommand,"?LastName",leadForm.LastName);
                
                	Database.UpdateParameter(dbCommand,"?Phone",leadForm.Phone);
                
                	Database.UpdateParameter(dbCommand,"?Message",leadForm.Message);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",leadForm.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?ReferralUri",leadForm.ReferralUri);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	leadForm.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<LeadForm>  leadFormList)
        {
        	Insert(leadFormList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update LeadForm Set "
          
            + " LeadSourceId = ?LeadSourceId, "
          
            + " FirstName = ?FirstName, "
          
            + " LastName = ?LastName, "
          
            + " Phone = ?Phone, "
          
            + " Message = ?Message, "
          
            + " DateCreated = ?DateCreated, "
          
            + " ReferralUri = ?ReferralUri "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(LeadForm leadForm, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", leadForm.Id);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadForm.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?FirstName", leadForm.FirstName);
            
            	Database.PutParameter(dbCommand,"?LastName", leadForm.LastName);
            
            	Database.PutParameter(dbCommand,"?Phone", leadForm.Phone);
            
            	Database.PutParameter(dbCommand,"?Message", leadForm.Message);
            
            	Database.PutParameter(dbCommand,"?DateCreated", leadForm.DateCreated);
            
            	Database.PutParameter(dbCommand,"?ReferralUri", leadForm.ReferralUri);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(LeadForm leadForm)
        {
          	Update(leadForm, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " LeadSourceId, "
        
          + " FirstName, "
        
          + " LastName, "
        
          + " Phone, "
        
          + " Message, "
        
          + " DateCreated, "
        
          + " ReferralUri "
        
          + " From LeadForm "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static LeadForm FindByPrimaryKey(
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

            throw new DataNotFoundException("LeadForm not found, search by primary key");
        }

        public static LeadForm FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(LeadForm leadForm, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",leadForm.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(LeadForm leadForm)
        {
        	return Exists(leadForm, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from LeadForm limit 1";

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

        public static LeadForm Load(IDataReader dataReader, int offset)
        {
              LeadForm leadForm = new LeadForm();

              leadForm.Id = dataReader.GetInt32(0 + offset);
                  leadForm.LeadSourceId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    leadForm.FirstName = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    leadForm.LastName = dataReader.GetString(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    leadForm.Phone = dataReader.GetString(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    leadForm.Message = dataReader.GetString(5 + offset);
                  leadForm.DateCreated = dataReader.GetDateTime(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    leadForm.ReferralUri = dataReader.GetString(7 + offset);
                  

            return leadForm;
        }

        public static LeadForm Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From LeadForm "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(LeadForm leadForm, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", leadForm.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(LeadForm leadForm)
        {
        	Delete(leadForm, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From LeadForm ";

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
              
                + " LeadSourceId, "
              
                + " FirstName, "
              
                + " LastName, "
              
                + " Phone, "
              
                + " Message, "
              
                + " DateCreated, "
              
                + " ReferralUri "
              
                + " From LeadForm ";

        public static List<LeadForm> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<LeadForm> rv = new List<LeadForm>();

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

        public static List<LeadForm> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<LeadForm> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<LeadForm> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadForm));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(LeadForm item in itemsList)
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

        public static List<LeadForm> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadForm));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<LeadForm> itemsList = new List<LeadForm>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is LeadForm)
              		itemsList.Add(deserializedObject as LeadForm);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_leadSourceId;
              
        protected String m_firstName;
              
        protected String m_lastName;
              
        protected String m_phone;
              
        protected String m_message;
              
        protected DateTime m_dateCreated;
              
        protected String m_referralUri;
              
        #endregion

        #region Constructors

        public LeadForm(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public LeadForm(
                int 
                  id,int 
                  leadSourceId,String 
                  firstName,String 
                  lastName,String 
                  phone,String 
                  message,DateTime 
                  dateCreated,String 
                  referralUri
                ) : this()
        {
            
        	m_id = id;
            
        	m_leadSourceId = leadSourceId;
            
        	m_firstName = firstName;
            
        	m_lastName = lastName;
            
        	m_phone = phone;
            
        	m_message = message;
            
        	m_dateCreated = dateCreated;
            
        	m_referralUri = referralUri;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int LeadSourceId
        {
        	get { return m_leadSourceId;}
            set { m_leadSourceId = value; }
        }
        
        public String FirstName
        {
        	get { return m_firstName;}
            set { m_firstName = value; }
        }
        
        public String LastName
        {
        	get { return m_lastName;}
            set { m_lastName = value; }
        }
        
        public String Phone
        {
        	get { return m_phone;}
            set { m_phone = value; }
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
        
        public String ReferralUri
        {
        	get { return m_referralUri;}
            set { m_referralUri = value; }
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

    