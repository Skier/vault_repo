
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class LeadSourcePhone : ICloneable
    {

        #region Store


        #region Save

        public static LeadSourcePhone Save(LeadSourcePhone leadSourcePhone, IDbConnection connection)
        {
        	if (!Exists(leadSourcePhone, connection))
        		Insert(leadSourcePhone, connection);
        	else
        		Update(leadSourcePhone, connection);
        	return leadSourcePhone;
        }

        public static LeadSourcePhone Save(LeadSourcePhone leadSourcePhone)
        {
        	if (!Exists(leadSourcePhone))
        		Insert(leadSourcePhone);
        	else
        		Update(leadSourcePhone);
        	return leadSourcePhone;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into LeadSourcePhone ( " +
        
          " LeadSourceId, " +
        
          " PhoneNumber, " +
        
          " SimplePhoneNumber, " +
        
          " Description, " +
        
          " IsRemoved " +
        
        ") Values (" +
        
          " ?LeadSourceId, " +
        
          " ?PhoneNumber, " +
        
          " ?SimplePhoneNumber, " +
        
          " ?Description, " +
        
          " ?IsRemoved " +
        
        ")";

        public static void Insert(LeadSourcePhone leadSourcePhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourcePhone.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", leadSourcePhone.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?SimplePhoneNumber", leadSourcePhone.SimplePhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Description", leadSourcePhone.Description);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", leadSourcePhone.IsRemoved);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		leadSourcePhone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(LeadSourcePhone leadSourcePhone)
        {
          	Insert(leadSourcePhone, null);
        }

        public static void Insert(List<LeadSourcePhone>  leadSourcePhoneList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(LeadSourcePhone leadSourcePhone in  leadSourcePhoneList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourcePhone.LeadSourceId);
                
                  	Database.PutParameter(dbCommand,"?PhoneNumber", leadSourcePhone.PhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?SimplePhoneNumber", leadSourcePhone.SimplePhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?Description", leadSourcePhone.Description);
                
                  	Database.PutParameter(dbCommand,"?IsRemoved", leadSourcePhone.IsRemoved);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadSourceId",leadSourcePhone.LeadSourceId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneNumber",leadSourcePhone.PhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?SimplePhoneNumber",leadSourcePhone.SimplePhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?Description",leadSourcePhone.Description);
                
                	Database.UpdateParameter(dbCommand,"?IsRemoved",leadSourcePhone.IsRemoved);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	leadSourcePhone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<LeadSourcePhone>  leadSourcePhoneList)
        {
        	Insert(leadSourcePhoneList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update LeadSourcePhone Set "
          
            + " LeadSourceId = ?LeadSourceId, "
          
            + " PhoneNumber = ?PhoneNumber, "
          
            + " SimplePhoneNumber = ?SimplePhoneNumber, "
          
            + " Description = ?Description, "
          
            + " IsRemoved = ?IsRemoved "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(LeadSourcePhone leadSourcePhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", leadSourcePhone.Id);
            
            	Database.PutParameter(dbCommand,"?LeadSourceId", leadSourcePhone.LeadSourceId);
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", leadSourcePhone.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?SimplePhoneNumber", leadSourcePhone.SimplePhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Description", leadSourcePhone.Description);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", leadSourcePhone.IsRemoved);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(LeadSourcePhone leadSourcePhone)
        {
          	Update(leadSourcePhone, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " LeadSourceId, "
        
          + " PhoneNumber, "
        
          + " SimplePhoneNumber, "
        
          + " Description, "
        
          + " IsRemoved "
        
          + " From LeadSourcePhone "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static LeadSourcePhone FindByPrimaryKey(
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

            throw new DataNotFoundException("LeadSourcePhone not found, search by primary key");
        }

        public static LeadSourcePhone FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(LeadSourcePhone leadSourcePhone, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",leadSourcePhone.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(LeadSourcePhone leadSourcePhone)
        {
        	return Exists(leadSourcePhone, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from LeadSourcePhone limit 1";

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

        public static LeadSourcePhone Load(IDataReader dataReader, int offset)
        {
              LeadSourcePhone leadSourcePhone = new LeadSourcePhone();

              leadSourcePhone.Id = dataReader.GetInt32(0 + offset);
                  leadSourcePhone.LeadSourceId = dataReader.GetInt32(1 + offset);
                  leadSourcePhone.PhoneNumber = dataReader.GetString(2 + offset);
                  leadSourcePhone.SimplePhoneNumber = dataReader.GetString(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    leadSourcePhone.Description = dataReader.GetString(4 + offset);
                  leadSourcePhone.IsRemoved = dataReader.GetBoolean(5 + offset);
                  

            return leadSourcePhone;
        }

        public static LeadSourcePhone Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From LeadSourcePhone "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(LeadSourcePhone leadSourcePhone, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", leadSourcePhone.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(LeadSourcePhone leadSourcePhone)
        {
        	Delete(leadSourcePhone, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From LeadSourcePhone ";

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
              
                + " PhoneNumber, "
              
                + " SimplePhoneNumber, "
              
                + " Description, "
              
                + " IsRemoved "
              
                + " From LeadSourcePhone ";

        public static List<LeadSourcePhone> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<LeadSourcePhone> rv = new List<LeadSourcePhone>();

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

        public static List<LeadSourcePhone> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<LeadSourcePhone> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<LeadSourcePhone> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadSourcePhone));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(LeadSourcePhone item in itemsList)
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

        public static List<LeadSourcePhone> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadSourcePhone));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<LeadSourcePhone> itemsList = new List<LeadSourcePhone>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is LeadSourcePhone)
              		itemsList.Add(deserializedObject as LeadSourcePhone);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_leadSourceId;
              
        protected String m_phoneNumber;
              
        protected String m_simplePhoneNumber;
              
        protected String m_description;
              
        protected bool m_isRemoved;
              
        #endregion

        #region Constructors

        public LeadSourcePhone(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public LeadSourcePhone(
                int 
                  id,int 
                  leadSourceId,String 
                  phoneNumber,String 
                  simplePhoneNumber,String 
                  description,bool 
                  isRemoved
                ) : this()
        {
            
        	m_id = id;
            
        	m_leadSourceId = leadSourceId;
            
        	m_phoneNumber = phoneNumber;
            
        	m_simplePhoneNumber = simplePhoneNumber;
            
        	m_description = description;
            
        	m_isRemoved = isRemoved;
            
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
        
        public String PhoneNumber
        {
        	get { return m_phoneNumber;}
            set { m_phoneNumber = value; }
        }
        
        public String SimplePhoneNumber
        {
        	get { return m_simplePhoneNumber;}
            set { m_simplePhoneNumber = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        
        public bool IsRemoved
        {
        	get { return m_isRemoved;}
            set { m_isRemoved = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 6; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    