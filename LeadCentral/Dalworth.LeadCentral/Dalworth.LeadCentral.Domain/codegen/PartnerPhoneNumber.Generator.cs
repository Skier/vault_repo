
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class PartnerPhoneNumber : ICloneable
    {

        #region Store


        #region Save

        public static PartnerPhoneNumber Save(PartnerPhoneNumber partnerPhoneNumber, IDbConnection connection)
        {
        	if (!Exists(partnerPhoneNumber, connection))
        		Insert(partnerPhoneNumber, connection);
        	else
        		Update(partnerPhoneNumber, connection);
        	return partnerPhoneNumber;
        }

        public static PartnerPhoneNumber Save(PartnerPhoneNumber partnerPhoneNumber)
        {
        	if (!Exists(partnerPhoneNumber))
        		Insert(partnerPhoneNumber);
        	else
        		Update(partnerPhoneNumber);
        	return partnerPhoneNumber;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into PartnerPhoneNumber ( " +
        
          " BusinessPartnerId, " +
        
          " PhoneNumber, " +
        
          " Description, " +
        
          " PhoneDigits " +
        
        ") Values (" +
        
          " ?BusinessPartnerId, " +
        
          " ?PhoneNumber, " +
        
          " ?Description, " +
        
          " ?PhoneDigits " +
        
        ")";

        public static void Insert(PartnerPhoneNumber partnerPhoneNumber, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", partnerPhoneNumber.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", partnerPhoneNumber.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Description", partnerPhoneNumber.Description);
            
            	Database.PutParameter(dbCommand,"?PhoneDigits", partnerPhoneNumber.PhoneDigits);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		partnerPhoneNumber.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(PartnerPhoneNumber partnerPhoneNumber)
        {
          	Insert(partnerPhoneNumber, null);
        }

        public static void Insert(List<PartnerPhoneNumber>  partnerPhoneNumberList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(PartnerPhoneNumber partnerPhoneNumber in  partnerPhoneNumberList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?BusinessPartnerId", partnerPhoneNumber.BusinessPartnerId);
                
                  	Database.PutParameter(dbCommand,"?PhoneNumber", partnerPhoneNumber.PhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?Description", partnerPhoneNumber.Description);
                
                  	Database.PutParameter(dbCommand,"?PhoneDigits", partnerPhoneNumber.PhoneDigits);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?BusinessPartnerId",partnerPhoneNumber.BusinessPartnerId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneNumber",partnerPhoneNumber.PhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?Description",partnerPhoneNumber.Description);
                
                	Database.UpdateParameter(dbCommand,"?PhoneDigits",partnerPhoneNumber.PhoneDigits);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	partnerPhoneNumber.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<PartnerPhoneNumber>  partnerPhoneNumberList)
        {
        	Insert(partnerPhoneNumberList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update PartnerPhoneNumber Set "
          
            + " BusinessPartnerId = ?BusinessPartnerId, "
          
            + " PhoneNumber = ?PhoneNumber, "
          
            + " Description = ?Description, "
          
            + " PhoneDigits = ?PhoneDigits "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(PartnerPhoneNumber partnerPhoneNumber, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", partnerPhoneNumber.Id);
            
            	Database.PutParameter(dbCommand,"?BusinessPartnerId", partnerPhoneNumber.BusinessPartnerId);
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", partnerPhoneNumber.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Description", partnerPhoneNumber.Description);
            
            	Database.PutParameter(dbCommand,"?PhoneDigits", partnerPhoneNumber.PhoneDigits);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(PartnerPhoneNumber partnerPhoneNumber)
        {
          	Update(partnerPhoneNumber, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " BusinessPartnerId, "
        
          + " PhoneNumber, "
        
          + " Description, "
        
          + " PhoneDigits "
        
          + " From PartnerPhoneNumber "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static PartnerPhoneNumber FindByPrimaryKey(
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

            throw new DataNotFoundException("PartnerPhoneNumber not found, search by primary key");
        }

        public static PartnerPhoneNumber FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(PartnerPhoneNumber partnerPhoneNumber, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",partnerPhoneNumber.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(PartnerPhoneNumber partnerPhoneNumber)
        {
        	return Exists(partnerPhoneNumber, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from PartnerPhoneNumber limit 1";

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

        public static PartnerPhoneNumber Load(IDataReader dataReader, int offset)
        {
              PartnerPhoneNumber partnerPhoneNumber = new PartnerPhoneNumber();

              partnerPhoneNumber.Id = dataReader.GetInt32(0 + offset);
                  partnerPhoneNumber.BusinessPartnerId = dataReader.GetInt32(1 + offset);
                  partnerPhoneNumber.PhoneNumber = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    partnerPhoneNumber.Description = dataReader.GetString(3 + offset);
                  partnerPhoneNumber.PhoneDigits = dataReader.GetString(4 + offset);
                  

            return partnerPhoneNumber;
        }

        public static PartnerPhoneNumber Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From PartnerPhoneNumber "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(PartnerPhoneNumber partnerPhoneNumber, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", partnerPhoneNumber.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(PartnerPhoneNumber partnerPhoneNumber)
        {
        	Delete(partnerPhoneNumber, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From PartnerPhoneNumber ";

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
              
                + " BusinessPartnerId, "
              
                + " PhoneNumber, "
              
                + " Description, "
              
                + " PhoneDigits "
              
                + " From PartnerPhoneNumber ";

        public static List<PartnerPhoneNumber> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<PartnerPhoneNumber> rv = new List<PartnerPhoneNumber>();

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

        public static List<PartnerPhoneNumber> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<PartnerPhoneNumber> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<PartnerPhoneNumber> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerPhoneNumber));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(PartnerPhoneNumber item in itemsList)
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

        public static List<PartnerPhoneNumber> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerPhoneNumber));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<PartnerPhoneNumber> itemsList = new List<PartnerPhoneNumber>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is PartnerPhoneNumber)
              		itemsList.Add(deserializedObject as PartnerPhoneNumber);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_businessPartnerId;
              
        protected String m_phoneNumber;
              
        protected String m_description;
              
        protected String m_phoneDigits;
              
        #endregion

        #region Constructors

        public PartnerPhoneNumber(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public PartnerPhoneNumber(
                int 
                  id,int 
                  businessPartnerId,String 
                  phoneNumber,String 
                  description,String 
                  phoneDigits
                ) : this()
        {
            
        	m_id = id;
            
        	m_businessPartnerId = businessPartnerId;
            
        	m_phoneNumber = phoneNumber;
            
        	m_description = description;
            
        	m_phoneDigits = phoneDigits;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int BusinessPartnerId
        {
        	get { return m_businessPartnerId;}
            set { m_businessPartnerId = value; }
        }
        
        public String PhoneNumber
        {
        	get { return m_phoneNumber;}
            set { m_phoneNumber = value; }
        }
        
        public String Description
        {
        	get { return m_description;}
            set { m_description = value; }
        }
        
        public String PhoneDigits
        {
        	get { return m_phoneDigits;}
            set { m_phoneDigits = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 5; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    