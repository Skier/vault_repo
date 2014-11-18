
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class PhoneBlackList : ICloneable
    {

        #region Store


        #region Save

        public static PhoneBlackList Save(PhoneBlackList phoneBlackList, IDbConnection connection)
        {
        	if (!Exists(phoneBlackList, connection))
        		Insert(phoneBlackList, connection);
        	else
        		Update(phoneBlackList, connection);
        	return phoneBlackList;
        }

        public static PhoneBlackList Save(PhoneBlackList phoneBlackList)
        {
        	if (!Exists(phoneBlackList))
        		Insert(phoneBlackList);
        	else
        		Update(phoneBlackList);
        	return phoneBlackList;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into PhoneBlackList ( " +
        
          " PhoneNumber, " +
        
          " Description, " +
        
          " PhoneDigits " +
        
        ") Values (" +
        
          " ?PhoneNumber, " +
        
          " ?Description, " +
        
          " ?PhoneDigits " +
        
        ")";

        public static void Insert(PhoneBlackList phoneBlackList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", phoneBlackList.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Description", phoneBlackList.Description);
            
            	Database.PutParameter(dbCommand,"?PhoneDigits", phoneBlackList.PhoneDigits);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		phoneBlackList.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(PhoneBlackList phoneBlackList)
        {
          	Insert(phoneBlackList, null);
        }

        public static void Insert(List<PhoneBlackList>  phoneBlackListList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(PhoneBlackList phoneBlackList in  phoneBlackListList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?PhoneNumber", phoneBlackList.PhoneNumber);
                
                  	Database.PutParameter(dbCommand,"?Description", phoneBlackList.Description);
                
                  	Database.PutParameter(dbCommand,"?PhoneDigits", phoneBlackList.PhoneDigits);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?PhoneNumber",phoneBlackList.PhoneNumber);
                
                	Database.UpdateParameter(dbCommand,"?Description",phoneBlackList.Description);
                
                	Database.UpdateParameter(dbCommand,"?PhoneDigits",phoneBlackList.PhoneDigits);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	phoneBlackList.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<PhoneBlackList>  phoneBlackListList)
        {
        	Insert(phoneBlackListList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update PhoneBlackList Set "
          
            + " PhoneNumber = ?PhoneNumber, "
          
            + " Description = ?Description, "
          
            + " PhoneDigits = ?PhoneDigits "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(PhoneBlackList phoneBlackList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", phoneBlackList.Id);
            
            	Database.PutParameter(dbCommand,"?PhoneNumber", phoneBlackList.PhoneNumber);
            
            	Database.PutParameter(dbCommand,"?Description", phoneBlackList.Description);
            
            	Database.PutParameter(dbCommand,"?PhoneDigits", phoneBlackList.PhoneDigits);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(PhoneBlackList phoneBlackList)
        {
          	Update(phoneBlackList, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " PhoneNumber, "
        
          + " Description, "
        
          + " PhoneDigits "
        
          + " From PhoneBlackList "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static PhoneBlackList FindByPrimaryKey(
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

            throw new DataNotFoundException("PhoneBlackList not found, search by primary key");
        }

        public static PhoneBlackList FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(PhoneBlackList phoneBlackList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",phoneBlackList.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(PhoneBlackList phoneBlackList)
        {
        	return Exists(phoneBlackList, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from PhoneBlackList limit 1";

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

        public static PhoneBlackList Load(IDataReader dataReader, int offset)
        {
              PhoneBlackList phoneBlackList = new PhoneBlackList();

              phoneBlackList.Id = dataReader.GetInt32(0 + offset);
                  phoneBlackList.PhoneNumber = dataReader.GetString(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    phoneBlackList.Description = dataReader.GetString(2 + offset);
                  phoneBlackList.PhoneDigits = dataReader.GetString(3 + offset);
                  

            return phoneBlackList;
        }

        public static PhoneBlackList Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From PhoneBlackList "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(PhoneBlackList phoneBlackList, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", phoneBlackList.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(PhoneBlackList phoneBlackList)
        {
        	Delete(phoneBlackList, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From PhoneBlackList ";

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
              
                + " Description, "
              
                + " PhoneDigits "
              
                + " From PhoneBlackList ";

        public static List<PhoneBlackList> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<PhoneBlackList> rv = new List<PhoneBlackList>();

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

        public static List<PhoneBlackList> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<PhoneBlackList> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<PhoneBlackList> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneBlackList));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(PhoneBlackList item in itemsList)
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

        public static List<PhoneBlackList> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneBlackList));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<PhoneBlackList> itemsList = new List<PhoneBlackList>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is PhoneBlackList)
              		itemsList.Add(deserializedObject as PhoneBlackList);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_phoneNumber;
              
        protected String m_description;
              
        protected String m_phoneDigits;
              
        #endregion

        #region Constructors

        public PhoneBlackList(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public PhoneBlackList(
                int 
                  id,String 
                  phoneNumber,String 
                  description,String 
                  phoneDigits
                ) : this()
        {
            
        	m_id = id;
            
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
        	get { return 4; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    