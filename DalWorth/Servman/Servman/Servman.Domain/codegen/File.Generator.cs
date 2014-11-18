
using System;
using System.Data;
using System.Collections.Generic;
using Servman.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Servman.Domain
{

    public partial class File : ICloneable
    {

        #region Store


        #region Save

        public static File Save(File file, IDbConnection connection)
        {
        	if (!Exists(file, connection))
        		Insert(file, connection);
        	else
        		Update(file, connection);
        	return file;
        }

        public static File Save(File file)
        {
        	if (!Exists(file))
        		Insert(file);
        	else
        		Update(file);
        	return file;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into File ( " +
        
          " StorageKey, " +
        
          " OriginalFileName, " +
        
          " FileType, " +
        
          " FileSize " +
        
        ") Values (" +
        
          " ?StorageKey, " +
        
          " ?OriginalFileName, " +
        
          " ?FileType, " +
        
          " ?FileSize " +
        
        ")";

        public static void Insert(File file, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?StorageKey", file.StorageKey);
            
            	Database.PutParameter(dbCommand,"?OriginalFileName", file.OriginalFileName);
            
            	Database.PutParameter(dbCommand,"?FileType", file.FileType);
            
            	Database.PutParameter(dbCommand,"?FileSize", file.FileSize);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		file.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(File file)
        {
          	Insert(file, null);
        }

        public static void Insert(List<File>  fileList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(File file in  fileList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?StorageKey", file.StorageKey);
                
                  	Database.PutParameter(dbCommand,"?OriginalFileName", file.OriginalFileName);
                
                  	Database.PutParameter(dbCommand,"?FileType", file.FileType);
                
                  	Database.PutParameter(dbCommand,"?FileSize", file.FileSize);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?StorageKey",file.StorageKey);
                
                	Database.UpdateParameter(dbCommand,"?OriginalFileName",file.OriginalFileName);
                
                	Database.UpdateParameter(dbCommand,"?FileType",file.FileType);
                
                	Database.UpdateParameter(dbCommand,"?FileSize",file.FileSize);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	file.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<File>  fileList)
        {
        	Insert(fileList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update File Set "
          
            + " StorageKey = ?StorageKey, "
          
            + " OriginalFileName = ?OriginalFileName, "
          
            + " FileType = ?FileType, "
          
            + " FileSize = ?FileSize "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(File file, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", file.Id);
            
            	Database.PutParameter(dbCommand,"?StorageKey", file.StorageKey);
            
            	Database.PutParameter(dbCommand,"?OriginalFileName", file.OriginalFileName);
            
            	Database.PutParameter(dbCommand,"?FileType", file.FileType);
            
            	Database.PutParameter(dbCommand,"?FileSize", file.FileSize);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(File file)
        {
          	Update(file, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " StorageKey, "
        
          + " OriginalFileName, "
        
          + " FileType, "
        
          + " FileSize "
        
          + " From File "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static File FindByPrimaryKey(
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

            throw new DataNotFoundException("File not found, search by primary key");
        }

        public static File FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(File file, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",file.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(File file)
        {
        	return Exists(file, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from File limit 1";

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

        public static File Load(IDataReader dataReader, int offset)
        {
              File file = new File();

              file.Id = dataReader.GetInt32(0 + offset);
                  file.StorageKey = dataReader.GetString(1 + offset);
                  file.OriginalFileName = dataReader.GetString(2 + offset);
                  file.FileType = dataReader.GetString(3 + offset);
                  file.FileSize = dataReader.GetDecimal(4 + offset);
                  

            return file;
        }

        public static File Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From File "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(File file, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", file.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(File file)
        {
        	Delete(file, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From File ";

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
              
                + " StorageKey, "
              
                + " OriginalFileName, "
              
                + " FileType, "
              
                + " FileSize "
              
                + " From File ";

        public static List<File> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<File> rv = new List<File>();

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

        public static List<File> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<File> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<File> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(File));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(File item in itemsList)
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

        public static List<File> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(File));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<File> itemsList = new List<File>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is File)
              		itemsList.Add(deserializedObject as File);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected String m_storageKey;
              
        protected String m_originalFileName;
              
        protected String m_fileType;
              
        protected decimal m_fileSize;
              
        #endregion

        #region Constructors

        public File(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public File(
                int 
                  id,String 
                  storageKey,String 
                  originalFileName,String 
                  fileType,decimal 
                  fileSize
                ) : this()
        {
            
        	m_id = id;
            
        	m_storageKey = storageKey;
            
        	m_originalFileName = originalFileName;
            
        	m_fileType = fileType;
            
        	m_fileSize = fileSize;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public String StorageKey
        {
        	get { return m_storageKey;}
            set { m_storageKey = value; }
        }
        
        public String OriginalFileName
        {
        	get { return m_originalFileName;}
            set { m_originalFileName = value; }
        }
        
        public String FileType
        {
        	get { return m_fileType;}
            set { m_fileType = value; }
        }
        
        public decimal FileSize
        {
        	get { return m_fileSize;}
            set { m_fileSize = value; }
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

    