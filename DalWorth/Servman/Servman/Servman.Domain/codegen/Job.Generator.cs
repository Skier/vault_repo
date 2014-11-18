
using System;
using System.Data;
using System.Collections.Generic;
using Servman.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Servman.Domain
{

    public partial class Job : ICloneable
    {

        #region Store


        #region Save

        public static Job Save(Job job, IDbConnection connection)
        {
        	if (!Exists(job, connection))
        		Insert(job, connection);
        	else
        		Update(job, connection);
        	return job;
        }

        public static Job Save(Job job)
        {
        	if (!Exists(job))
        		Insert(job);
        	else
        		Update(job);
        	return job;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Job ( " +
        
          " LeadId, " +
        
          " QbJobRecordId " +
        
        ") Values (" +
        
          " ?LeadId, " +
        
          " ?QbJobRecordId " +
        
        ")";

        public static void Insert(Job job, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadId", job.LeadId);
            
            	Database.PutParameter(dbCommand,"?QbJobRecordId", job.QbJobRecordId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		job.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Job job)
        {
          	Insert(job, null);
        }

        public static void Insert(List<Job>  jobList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Job job in  jobList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?LeadId", job.LeadId);
                
                  	Database.PutParameter(dbCommand,"?QbJobRecordId", job.QbJobRecordId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadId",job.LeadId);
                
                	Database.UpdateParameter(dbCommand,"?QbJobRecordId",job.QbJobRecordId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	job.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Job>  jobList)
        {
        	Insert(jobList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Job Set "
          
            + " LeadId = ?LeadId, "
          
            + " QbJobRecordId = ?QbJobRecordId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Job job, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", job.Id);
            
            	Database.PutParameter(dbCommand,"?LeadId", job.LeadId);
            
            	Database.PutParameter(dbCommand,"?QbJobRecordId", job.QbJobRecordId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Job job)
        {
          	Update(job, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " LeadId, "
        
          + " QbJobRecordId "
        
          + " From Job "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Job FindByPrimaryKey(
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

            throw new DataNotFoundException("Job not found, search by primary key");
        }

        public static Job FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Job job, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",job.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Job job)
        {
        	return Exists(job, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Job limit 1";

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

        public static Job Load(IDataReader dataReader, int offset)
        {
              Job job = new Job();

              job.Id = dataReader.GetInt32(0 + offset);
                  job.LeadId = dataReader.GetInt32(1 + offset);
                  job.QbJobRecordId = dataReader.GetString(2 + offset);
                  

            return job;
        }

        public static Job Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Job "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Job job, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", job.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Job job)
        {
        	Delete(job, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Job ";

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
              
                + " LeadId, "
              
                + " QbJobRecordId "
              
                + " From Job ";

        public static List<Job> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Job> rv = new List<Job>();

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

        public static List<Job> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Job> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Job> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Job));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Job item in itemsList)
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

        public static List<Job> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Job));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Job> itemsList = new List<Job>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Job)
              		itemsList.Add(deserializedObject as Job);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_leadId;
              
        protected String m_qbJobRecordId;
              
        #endregion

        #region Constructors

        public Job(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Job(
                int 
                  id,int 
                  leadId,String 
                  qbJobRecordId
                ) : this()
        {
            
        	m_id = id;
            
        	m_leadId = leadId;
            
        	m_qbJobRecordId = qbJobRecordId;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int LeadId
        {
        	get { return m_leadId;}
            set { m_leadId = value; }
        }
        
        public String QbJobRecordId
        {
        	get { return m_qbJobRecordId;}
            set { m_qbJobRecordId = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 3; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    