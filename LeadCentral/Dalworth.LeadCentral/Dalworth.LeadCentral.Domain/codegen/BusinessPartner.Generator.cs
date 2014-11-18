
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class BusinessPartner : ICloneable
    {

        #region Store


        #region Save

        public static BusinessPartner Save(BusinessPartner businessPartner, IDbConnection connection)
        {
        	if (!Exists(businessPartner, connection))
        		Insert(businessPartner, connection);
        	else
        		Update(businessPartner, connection);
        	return businessPartner;
        }

        public static BusinessPartner Save(BusinessPartner businessPartner)
        {
        	if (!Exists(businessPartner))
        		Insert(businessPartner);
        	else
        		Update(businessPartner);
        	return businessPartner;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into BusinessPartner ( " +
        
          " PartnerName, " +
        
          " Email, " +
        
          " Phone, " +
        
          " PhoneDigits, " +
        
          " SalesRepId, " +
        
          " IsRemoved, " +
        
          " DateCreated, " +
        
          " Address, " +
        
          " IsExcludedFromReports " +
        
        ") Values (" +
        
          " ?PartnerName, " +
        
          " ?Email, " +
        
          " ?Phone, " +
        
          " ?PhoneDigits, " +
        
          " ?SalesRepId, " +
        
          " ?IsRemoved, " +
        
          " ?DateCreated, " +
        
          " ?Address, " +
        
          " ?IsExcludedFromReports " +
        
        ")";

        public static void Insert(BusinessPartner businessPartner, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?PartnerName", businessPartner.PartnerName);
            
            	Database.PutParameter(dbCommand,"?Email", businessPartner.Email);
            
            	Database.PutParameter(dbCommand,"?Phone", businessPartner.Phone);
            
            	Database.PutParameter(dbCommand,"?PhoneDigits", businessPartner.PhoneDigits);
            
            	Database.PutParameter(dbCommand,"?SalesRepId", businessPartner.SalesRepId);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", businessPartner.IsRemoved);
            
            	Database.PutParameter(dbCommand,"?DateCreated", businessPartner.DateCreated);
            
            	Database.PutParameter(dbCommand,"?Address", businessPartner.Address);
            
            	Database.PutParameter(dbCommand,"?IsExcludedFromReports", businessPartner.IsExcludedFromReports);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		businessPartner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(BusinessPartner businessPartner)
        {
          	Insert(businessPartner, null);
        }

        public static void Insert(List<BusinessPartner>  businessPartnerList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(BusinessPartner businessPartner in  businessPartnerList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?PartnerName", businessPartner.PartnerName);
                
                  	Database.PutParameter(dbCommand,"?Email", businessPartner.Email);
                
                  	Database.PutParameter(dbCommand,"?Phone", businessPartner.Phone);
                
                  	Database.PutParameter(dbCommand,"?PhoneDigits", businessPartner.PhoneDigits);
                
                  	Database.PutParameter(dbCommand,"?SalesRepId", businessPartner.SalesRepId);
                
                  	Database.PutParameter(dbCommand,"?IsRemoved", businessPartner.IsRemoved);
                
                  	Database.PutParameter(dbCommand,"?DateCreated", businessPartner.DateCreated);
                
                  	Database.PutParameter(dbCommand,"?Address", businessPartner.Address);
                
                  	Database.PutParameter(dbCommand,"?IsExcludedFromReports", businessPartner.IsExcludedFromReports);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?PartnerName",businessPartner.PartnerName);
                
                	Database.UpdateParameter(dbCommand,"?Email",businessPartner.Email);
                
                	Database.UpdateParameter(dbCommand,"?Phone",businessPartner.Phone);
                
                	Database.UpdateParameter(dbCommand,"?PhoneDigits",businessPartner.PhoneDigits);
                
                	Database.UpdateParameter(dbCommand,"?SalesRepId",businessPartner.SalesRepId);
                
                	Database.UpdateParameter(dbCommand,"?IsRemoved",businessPartner.IsRemoved);
                
                	Database.UpdateParameter(dbCommand,"?DateCreated",businessPartner.DateCreated);
                
                	Database.UpdateParameter(dbCommand,"?Address",businessPartner.Address);
                
                	Database.UpdateParameter(dbCommand,"?IsExcludedFromReports",businessPartner.IsExcludedFromReports);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	businessPartner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<BusinessPartner>  businessPartnerList)
        {
        	Insert(businessPartnerList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update BusinessPartner Set "
          
            + " PartnerName = ?PartnerName, "
          
            + " Email = ?Email, "
          
            + " Phone = ?Phone, "
          
            + " PhoneDigits = ?PhoneDigits, "
          
            + " SalesRepId = ?SalesRepId, "
          
            + " IsRemoved = ?IsRemoved, "
          
            + " DateCreated = ?DateCreated, "
          
            + " Address = ?Address, "
          
            + " IsExcludedFromReports = ?IsExcludedFromReports "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(BusinessPartner businessPartner, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", businessPartner.Id);
            
            	Database.PutParameter(dbCommand,"?PartnerName", businessPartner.PartnerName);
            
            	Database.PutParameter(dbCommand,"?Email", businessPartner.Email);
            
            	Database.PutParameter(dbCommand,"?Phone", businessPartner.Phone);
            
            	Database.PutParameter(dbCommand,"?PhoneDigits", businessPartner.PhoneDigits);
            
            	Database.PutParameter(dbCommand,"?SalesRepId", businessPartner.SalesRepId);
            
            	Database.PutParameter(dbCommand,"?IsRemoved", businessPartner.IsRemoved);
            
            	Database.PutParameter(dbCommand,"?DateCreated", businessPartner.DateCreated);
            
            	Database.PutParameter(dbCommand,"?Address", businessPartner.Address);
            
            	Database.PutParameter(dbCommand,"?IsExcludedFromReports", businessPartner.IsExcludedFromReports);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(BusinessPartner businessPartner)
        {
          	Update(businessPartner, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " PartnerName, "
        
          + " Email, "
        
          + " Phone, "
        
          + " PhoneDigits, "
        
          + " SalesRepId, "
        
          + " IsRemoved, "
        
          + " DateCreated, "
        
          + " Address, "
        
          + " IsExcludedFromReports "
        
          + " From BusinessPartner "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static BusinessPartner FindByPrimaryKey(
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

            throw new DataNotFoundException("BusinessPartner not found, search by primary key");
        }

        public static BusinessPartner FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(BusinessPartner businessPartner, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",businessPartner.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(BusinessPartner businessPartner)
        {
        	return Exists(businessPartner, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from BusinessPartner limit 1";

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

        public static BusinessPartner Load(IDataReader dataReader, int offset)
        {
              BusinessPartner businessPartner = new BusinessPartner();

              businessPartner.Id = dataReader.GetInt32(0 + offset);
                  
                    if(!dataReader.IsDBNull(1 + offset))
                    businessPartner.PartnerName = dataReader.GetString(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    businessPartner.Email = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    businessPartner.Phone = dataReader.GetString(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    businessPartner.PhoneDigits = dataReader.GetString(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    businessPartner.SalesRepId = dataReader.GetInt32(5 + offset);
                  businessPartner.IsRemoved = dataReader.GetBoolean(6 + offset);
                  businessPartner.DateCreated = dataReader.GetDateTime(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    businessPartner.Address = dataReader.GetString(8 + offset);
                  businessPartner.IsExcludedFromReports = dataReader.GetBoolean(9 + offset);
                  

            return businessPartner;
        }

        public static BusinessPartner Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From BusinessPartner "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(BusinessPartner businessPartner, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", businessPartner.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(BusinessPartner businessPartner)
        {
        	Delete(businessPartner, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From BusinessPartner ";

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
              
                + " PartnerName, "
              
                + " Email, "
              
                + " Phone, "
              
                + " PhoneDigits, "
              
                + " SalesRepId, "
              
                + " IsRemoved, "
              
                + " DateCreated, "
              
                + " Address, "
              
                + " IsExcludedFromReports "
              
                + " From BusinessPartner ";

        public static List<BusinessPartner> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<BusinessPartner> rv = new List<BusinessPartner>();

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

        public static List<BusinessPartner> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<BusinessPartner> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<BusinessPartner> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessPartner));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(BusinessPartner item in itemsList)
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

        public static List<BusinessPartner> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessPartner));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<BusinessPartner> itemsList = new List<BusinessPartner>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is BusinessPartner)
              		itemsList.Add(deserializedObject as BusinessPartner);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_partnerName;
              
        protected String m_email;
              
        protected String m_phone;
              
        protected String m_phoneDigits;
              
        protected int? m_salesRepId;
              
        protected bool m_isRemoved;
              
        protected DateTime m_dateCreated;
              
        protected String m_address;
              
        protected bool m_isExcludedFromReports;
              
        #endregion

        #region Constructors

        public BusinessPartner(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public BusinessPartner(
                int 
                  id,String 
                  partnerName,String 
                  email,String 
                  phone,String 
                  phoneDigits,int? 
                  salesRepId,bool 
                  isRemoved,DateTime 
                  dateCreated,String 
                  address,bool 
                  isExcludedFromReports
                ) : this()
        {
            
        	m_id = id;
            
        	m_partnerName = partnerName;
            
        	m_email = email;
            
        	m_phone = phone;
            
        	m_phoneDigits = phoneDigits;
            
        	m_salesRepId = salesRepId;
            
        	m_isRemoved = isRemoved;
            
        	m_dateCreated = dateCreated;
            
        	m_address = address;
            
        	m_isExcludedFromReports = isExcludedFromReports;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String PartnerName
        {
        	get { return m_partnerName;}
            set { m_partnerName = value; }
        }
        
        public String Email
        {
        	get { return m_email;}
            set { m_email = value; }
        }
        
        public String Phone
        {
        	get { return m_phone;}
            set { m_phone = value; }
        }
        
        public String PhoneDigits
        {
        	get { return m_phoneDigits;}
            set { m_phoneDigits = value; }
        }
        
        public int? SalesRepId
        {
        	get { return m_salesRepId;}
            set { m_salesRepId = value; }
        }
        
        public bool IsRemoved
        {
        	get { return m_isRemoved;}
            set { m_isRemoved = value; }
        }
        
        public DateTime DateCreated
        {
        	get { return m_dateCreated;}
            set { m_dateCreated = value; }
        }
        
        public String Address
        {
        	get { return m_address;}
            set { m_address = value; }
        }
        
        public bool IsExcludedFromReports
        {
        	get { return m_isExcludedFromReports;}
            set { m_isExcludedFromReports = value; }
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

    