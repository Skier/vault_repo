
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class Source : ICloneable
    {

        #region Store


        #region Save

        public static Source Save(Source source, IDbConnection connection)
        {
        	if (!Exists(source, connection))
        		Insert(source, connection);
        	else
        		Update(source, connection);
        	return source;
        }

        public static Source Save(Source source)
        {
        	if (!Exists(source))
        		Insert(source);
        	else
        		Update(source);
        	return source;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Source ( " +
        
          " PhoneCallId, " +
        
          " PhoneSmsId, " +
        
          " WebFormId, " +
        
          " UserId " +
        
        ") Values (" +
        
          " ?PhoneCallId, " +
        
          " ?PhoneSmsId, " +
        
          " ?WebFormId, " +
        
          " ?UserId " +
        
        ")";

        public static void Insert(Source source, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", source.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", source.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?WebFormId", source.WebFormId);
            
            	Database.PutParameter(dbCommand,"?UserId", source.UserId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		source.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Source source)
        {
          	Insert(source, null);
        }

        public static void Insert(List<Source>  sourceList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Source source in  sourceList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?PhoneCallId", source.PhoneCallId);
                
                  	Database.PutParameter(dbCommand,"?PhoneSmsId", source.PhoneSmsId);
                
                  	Database.PutParameter(dbCommand,"?WebFormId", source.WebFormId);
                
                  	Database.PutParameter(dbCommand,"?UserId", source.UserId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?PhoneCallId",source.PhoneCallId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneSmsId",source.PhoneSmsId);
                
                	Database.UpdateParameter(dbCommand,"?WebFormId",source.WebFormId);
                
                	Database.UpdateParameter(dbCommand,"?UserId",source.UserId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	source.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Source>  sourceList)
        {
        	Insert(sourceList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Source Set "
          
            + " PhoneCallId = ?PhoneCallId, "
          
            + " PhoneSmsId = ?PhoneSmsId, "
          
            + " WebFormId = ?WebFormId, "
          
            + " UserId = ?UserId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Source source, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", source.Id);
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", source.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", source.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?WebFormId", source.WebFormId);
            
            	Database.PutParameter(dbCommand,"?UserId", source.UserId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Source source)
        {
          	Update(source, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " PhoneCallId, "
        
          + " PhoneSmsId, "
        
          + " WebFormId, "
        
          + " UserId "
        
          + " From Source "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Source FindByPrimaryKey(
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

            throw new DataNotFoundException("Source not found, search by primary key");
        }

        public static Source FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Source source, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",source.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Source source)
        {
        	return Exists(source, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Source limit 1";

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

        public static Source Load(IDataReader dataReader, int offset)
        {
              Source source = new Source();

              source.Id = dataReader.GetInt32(0 + offset);
                  
                    if(!dataReader.IsDBNull(1 + offset))
                    source.PhoneCallId = dataReader.GetInt32(1 + offset);
                  
                    if(!dataReader.IsDBNull(2 + offset))
                    source.PhoneSmsId = dataReader.GetInt32(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    source.WebFormId = dataReader.GetInt32(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    source.UserId = dataReader.GetInt32(4 + offset);
                  

            return source;
        }

        public static Source Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Source "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Source source, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", source.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Source source)
        {
        	Delete(source, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Source ";

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
              
                + " PhoneCallId, "
              
                + " PhoneSmsId, "
              
                + " WebFormId, "
              
                + " UserId "
              
                + " From Source ";

        public static List<Source> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Source> rv = new List<Source>();

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

        public static List<Source> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Source> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Source> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Source));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Source item in itemsList)
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

        public static List<Source> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Source));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Source> itemsList = new List<Source>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Source)
              		itemsList.Add(deserializedObject as Source);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int? m_phoneCallId;
              
        protected int? m_phoneSmsId;
              
        protected int? m_webFormId;
              
        protected int? m_userId;
              
        #endregion

        #region Constructors

        public Source(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Source(
                int 
                  id,int? 
                  phoneCallId,int? 
                  phoneSmsId,int? 
                  webFormId,int? 
                  userId
                ) : this()
        {
            
        	m_id = id;
            
        	m_phoneCallId = phoneCallId;
            
        	m_phoneSmsId = phoneSmsId;
            
        	m_webFormId = webFormId;
            
        	m_userId = userId;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
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
        
        public int? WebFormId
        {
        	get { return m_webFormId;}
            set { m_webFormId = value; }
        }
        
        public int? UserId
        {
        	get { return m_userId;}
            set { m_userId = value; }
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

    