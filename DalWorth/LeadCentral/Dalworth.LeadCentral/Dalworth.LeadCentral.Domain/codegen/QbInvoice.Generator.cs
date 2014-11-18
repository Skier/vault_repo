
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class QbInvoice : ICloneable
    {

        #region Store


        #region Save

        public static QbInvoice Save(QbInvoice qbInvoice, IDbConnection connection)
        {
        	if (!Exists(qbInvoice, connection))
        		Insert(qbInvoice, connection);
        	else
        		Update(qbInvoice, connection);
        	return qbInvoice;
        }

        public static QbInvoice Save(QbInvoice qbInvoice)
        {
        	if (!Exists(qbInvoice))
        		Insert(qbInvoice);
        	else
        		Update(qbInvoice);
        	return qbInvoice;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into QbInvoice ( " +
        
          " LeadId, " +
        
          " QbInvoiceId, " +
        
          " Amount, " +
        
          " TaxAmount, " +
        
          " TotalAmount, " +
        
          " Status " +
        
        ") Values (" +
        
          " ?LeadId, " +
        
          " ?QbInvoiceId, " +
        
          " ?Amount, " +
        
          " ?TaxAmount, " +
        
          " ?TotalAmount, " +
        
          " ?Status " +
        
        ")";

        public static void Insert(QbInvoice qbInvoice, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?LeadId", qbInvoice.LeadId);
            
            	Database.PutParameter(dbCommand,"?QbInvoiceId", qbInvoice.QbInvoiceId);
            
            	Database.PutParameter(dbCommand,"?Amount", qbInvoice.Amount);
            
            	Database.PutParameter(dbCommand,"?TaxAmount", qbInvoice.TaxAmount);
            
            	Database.PutParameter(dbCommand,"?TotalAmount", qbInvoice.TotalAmount);
            
            	Database.PutParameter(dbCommand,"?Status", qbInvoice.Status);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		qbInvoice.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(QbInvoice qbInvoice)
        {
          	Insert(qbInvoice, null);
        }

        public static void Insert(List<QbInvoice>  qbInvoiceList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(QbInvoice qbInvoice in  qbInvoiceList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?LeadId", qbInvoice.LeadId);
                
                  	Database.PutParameter(dbCommand,"?QbInvoiceId", qbInvoice.QbInvoiceId);
                
                  	Database.PutParameter(dbCommand,"?Amount", qbInvoice.Amount);
                
                  	Database.PutParameter(dbCommand,"?TaxAmount", qbInvoice.TaxAmount);
                
                  	Database.PutParameter(dbCommand,"?TotalAmount", qbInvoice.TotalAmount);
                
                  	Database.PutParameter(dbCommand,"?Status", qbInvoice.Status);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?LeadId",qbInvoice.LeadId);
                
                	Database.UpdateParameter(dbCommand,"?QbInvoiceId",qbInvoice.QbInvoiceId);
                
                	Database.UpdateParameter(dbCommand,"?Amount",qbInvoice.Amount);
                
                	Database.UpdateParameter(dbCommand,"?TaxAmount",qbInvoice.TaxAmount);
                
                	Database.UpdateParameter(dbCommand,"?TotalAmount",qbInvoice.TotalAmount);
                
                	Database.UpdateParameter(dbCommand,"?Status",qbInvoice.Status);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	qbInvoice.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<QbInvoice>  qbInvoiceList)
        {
        	Insert(qbInvoiceList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update QbInvoice Set "
          
            + " LeadId = ?LeadId, "
          
            + " QbInvoiceId = ?QbInvoiceId, "
          
            + " Amount = ?Amount, "
          
            + " TaxAmount = ?TaxAmount, "
          
            + " TotalAmount = ?TotalAmount, "
          
            + " Status = ?Status "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(QbInvoice qbInvoice, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", qbInvoice.Id);
            
            	Database.PutParameter(dbCommand,"?LeadId", qbInvoice.LeadId);
            
            	Database.PutParameter(dbCommand,"?QbInvoiceId", qbInvoice.QbInvoiceId);
            
            	Database.PutParameter(dbCommand,"?Amount", qbInvoice.Amount);
            
            	Database.PutParameter(dbCommand,"?TaxAmount", qbInvoice.TaxAmount);
            
            	Database.PutParameter(dbCommand,"?TotalAmount", qbInvoice.TotalAmount);
            
            	Database.PutParameter(dbCommand,"?Status", qbInvoice.Status);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(QbInvoice qbInvoice)
        {
          	Update(qbInvoice, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " LeadId, "
        
          + " QbInvoiceId, "
        
          + " Amount, "
        
          + " TaxAmount, "
        
          + " TotalAmount, "
        
          + " Status "
        
          + " From QbInvoice "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static QbInvoice FindByPrimaryKey(
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

            throw new DataNotFoundException("QbInvoice not found, search by primary key");
        }

        public static QbInvoice FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(QbInvoice qbInvoice, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",qbInvoice.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(QbInvoice qbInvoice)
        {
        	return Exists(qbInvoice, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from QbInvoice limit 1";

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

        public static QbInvoice Load(IDataReader dataReader, int offset)
        {
              QbInvoice qbInvoice = new QbInvoice();

              qbInvoice.Id = dataReader.GetInt32(0 + offset);
                  qbInvoice.LeadId = dataReader.GetInt32(1 + offset);
                  qbInvoice.QbInvoiceId = dataReader.GetString(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    qbInvoice.Amount = dataReader.GetDecimal(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    qbInvoice.TaxAmount = dataReader.GetDecimal(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    qbInvoice.TotalAmount = dataReader.GetDecimal(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    qbInvoice.Status = dataReader.GetString(6 + offset);
                  

            return qbInvoice;
        }

        public static QbInvoice Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From QbInvoice "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(QbInvoice qbInvoice, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", qbInvoice.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(QbInvoice qbInvoice)
        {
        	Delete(qbInvoice, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From QbInvoice ";

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
              
                + " QbInvoiceId, "
              
                + " Amount, "
              
                + " TaxAmount, "
              
                + " TotalAmount, "
              
                + " Status "
              
                + " From QbInvoice ";

        public static List<QbInvoice> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<QbInvoice> rv = new List<QbInvoice>();

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

        public static List<QbInvoice> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<QbInvoice> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<QbInvoice> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoice));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(QbInvoice item in itemsList)
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

        public static List<QbInvoice> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoice));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<QbInvoice> itemsList = new List<QbInvoice>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is QbInvoice)
              		itemsList.Add(deserializedObject as QbInvoice);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_leadId;
              
        protected String m_qbInvoiceId;
              
        protected decimal m_amount;
              
        protected decimal m_taxAmount;
              
        protected decimal m_totalAmount;
              
        protected String m_status;
              
        #endregion

        #region Constructors

        public QbInvoice(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public QbInvoice(
                int 
                  id,int 
                  leadId,String 
                  qbInvoiceId,decimal 
                  amount,decimal 
                  taxAmount,decimal 
                  totalAmount,String 
                  status
                ) : this()
        {
            
        	m_id = id;
            
        	m_leadId = leadId;
            
        	m_qbInvoiceId = qbInvoiceId;
            
        	m_amount = amount;
            
        	m_taxAmount = taxAmount;
            
        	m_totalAmount = totalAmount;
            
        	m_status = status;
            
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
        
        public String QbInvoiceId
        {
        	get { return m_qbInvoiceId;}
            set { m_qbInvoiceId = value; }
        }
        
        public decimal Amount
        {
        	get { return m_amount;}
            set { m_amount = value; }
        }
        
        public decimal TaxAmount
        {
        	get { return m_taxAmount;}
            set { m_taxAmount = value; }
        }
        
        public decimal TotalAmount
        {
        	get { return m_totalAmount;}
            set { m_totalAmount = value; }
        }
        
        public String Status
        {
        	get { return m_status;}
            set { m_status = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 7; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    