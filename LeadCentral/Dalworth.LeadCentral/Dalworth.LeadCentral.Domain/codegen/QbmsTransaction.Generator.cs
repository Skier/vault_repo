
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Common.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class QbmsTransaction : ICloneable
    {

        #region Store


        #region Save

        public static QbmsTransaction Save(QbmsTransaction qbmsTransaction, IDbConnection connection)
        {
        	if (!Exists(qbmsTransaction, connection))
        		Insert(qbmsTransaction, connection);
        	else
        		Update(qbmsTransaction, connection);
        	return qbmsTransaction;
        }

        public static QbmsTransaction Save(QbmsTransaction qbmsTransaction)
        {
        	if (!Exists(qbmsTransaction))
        		Insert(qbmsTransaction);
        	else
        		Update(qbmsTransaction);
        	return qbmsTransaction;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into QbmsTransaction ( " +
        
          " CustomerId, " +
        
          " Ticket, " +
        
          " OpId, " +
        
          " Amount, " +
        
          " OpType, " +
        
          " Status, " +
        
          " StatusCode, " +
        
          " StatusMessage, " +
        
          " TxnType, " +
        
          " TxnTimestamp, " +
        
          " MaskedCCN, " +
        
          " AuthCode, " +
        
          " TxnId " +
        
        ") Values (" +
        
          " ?CustomerId, " +
        
          " ?Ticket, " +
        
          " ?OpId, " +
        
          " ?Amount, " +
        
          " ?OpType, " +
        
          " ?Status, " +
        
          " ?StatusCode, " +
        
          " ?StatusMessage, " +
        
          " ?TxnType, " +
        
          " ?TxnTimestamp, " +
        
          " ?MaskedCCN, " +
        
          " ?AuthCode, " +
        
          " ?TxnId " +
        
        ")";

        public static void Insert(QbmsTransaction qbmsTransaction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?CustomerId", qbmsTransaction.CustomerId);
            
            	Database.PutParameter(dbCommand,"?Ticket", qbmsTransaction.Ticket);
            
            	Database.PutParameter(dbCommand,"?OpId", qbmsTransaction.OpId);
            
            	Database.PutParameter(dbCommand,"?Amount", qbmsTransaction.Amount);
            
            	Database.PutParameter(dbCommand,"?OpType", qbmsTransaction.OpType);
            
            	Database.PutParameter(dbCommand,"?Status", qbmsTransaction.Status);
            
            	Database.PutParameter(dbCommand,"?StatusCode", qbmsTransaction.StatusCode);
            
            	Database.PutParameter(dbCommand,"?StatusMessage", qbmsTransaction.StatusMessage);
            
            	Database.PutParameter(dbCommand,"?TxnType", qbmsTransaction.TxnType);
            
            	Database.PutParameter(dbCommand,"?TxnTimestamp", qbmsTransaction.TxnTimestamp);
            
            	Database.PutParameter(dbCommand,"?MaskedCCN", qbmsTransaction.MaskedCCN);
            
            	Database.PutParameter(dbCommand,"?AuthCode", qbmsTransaction.AuthCode);
            
            	Database.PutParameter(dbCommand,"?TxnId", qbmsTransaction.TxnId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		qbmsTransaction.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(QbmsTransaction qbmsTransaction)
        {
          	Insert(qbmsTransaction, null);
        }

        public static void Insert(List<QbmsTransaction>  qbmsTransactionList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(QbmsTransaction qbmsTransaction in  qbmsTransactionList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?CustomerId", qbmsTransaction.CustomerId);
                
                  	Database.PutParameter(dbCommand,"?Ticket", qbmsTransaction.Ticket);
                
                  	Database.PutParameter(dbCommand,"?OpId", qbmsTransaction.OpId);
                
                  	Database.PutParameter(dbCommand,"?Amount", qbmsTransaction.Amount);
                
                  	Database.PutParameter(dbCommand,"?OpType", qbmsTransaction.OpType);
                
                  	Database.PutParameter(dbCommand,"?Status", qbmsTransaction.Status);
                
                  	Database.PutParameter(dbCommand,"?StatusCode", qbmsTransaction.StatusCode);
                
                  	Database.PutParameter(dbCommand,"?StatusMessage", qbmsTransaction.StatusMessage);
                
                  	Database.PutParameter(dbCommand,"?TxnType", qbmsTransaction.TxnType);
                
                  	Database.PutParameter(dbCommand,"?TxnTimestamp", qbmsTransaction.TxnTimestamp);
                
                  	Database.PutParameter(dbCommand,"?MaskedCCN", qbmsTransaction.MaskedCCN);
                
                  	Database.PutParameter(dbCommand,"?AuthCode", qbmsTransaction.AuthCode);
                
                  	Database.PutParameter(dbCommand,"?TxnId", qbmsTransaction.TxnId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?CustomerId",qbmsTransaction.CustomerId);
                
                	Database.UpdateParameter(dbCommand,"?Ticket",qbmsTransaction.Ticket);
                
                	Database.UpdateParameter(dbCommand,"?OpId",qbmsTransaction.OpId);
                
                	Database.UpdateParameter(dbCommand,"?Amount",qbmsTransaction.Amount);
                
                	Database.UpdateParameter(dbCommand,"?OpType",qbmsTransaction.OpType);
                
                	Database.UpdateParameter(dbCommand,"?Status",qbmsTransaction.Status);
                
                	Database.UpdateParameter(dbCommand,"?StatusCode",qbmsTransaction.StatusCode);
                
                	Database.UpdateParameter(dbCommand,"?StatusMessage",qbmsTransaction.StatusMessage);
                
                	Database.UpdateParameter(dbCommand,"?TxnType",qbmsTransaction.TxnType);
                
                	Database.UpdateParameter(dbCommand,"?TxnTimestamp",qbmsTransaction.TxnTimestamp);
                
                	Database.UpdateParameter(dbCommand,"?MaskedCCN",qbmsTransaction.MaskedCCN);
                
                	Database.UpdateParameter(dbCommand,"?AuthCode",qbmsTransaction.AuthCode);
                
                	Database.UpdateParameter(dbCommand,"?TxnId",qbmsTransaction.TxnId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	qbmsTransaction.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<QbmsTransaction>  qbmsTransactionList)
        {
        	Insert(qbmsTransactionList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update QbmsTransaction Set "
          
            + " CustomerId = ?CustomerId, "
          
            + " Ticket = ?Ticket, "
          
            + " OpId = ?OpId, "
          
            + " Amount = ?Amount, "
          
            + " OpType = ?OpType, "
          
            + " Status = ?Status, "
          
            + " StatusCode = ?StatusCode, "
          
            + " StatusMessage = ?StatusMessage, "
          
            + " TxnType = ?TxnType, "
          
            + " TxnTimestamp = ?TxnTimestamp, "
          
            + " MaskedCCN = ?MaskedCCN, "
          
            + " AuthCode = ?AuthCode, "
          
            + " TxnId = ?TxnId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(QbmsTransaction qbmsTransaction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", qbmsTransaction.Id);
            
            	Database.PutParameter(dbCommand,"?CustomerId", qbmsTransaction.CustomerId);
            
            	Database.PutParameter(dbCommand,"?Ticket", qbmsTransaction.Ticket);
            
            	Database.PutParameter(dbCommand,"?OpId", qbmsTransaction.OpId);
            
            	Database.PutParameter(dbCommand,"?Amount", qbmsTransaction.Amount);
            
            	Database.PutParameter(dbCommand,"?OpType", qbmsTransaction.OpType);
            
            	Database.PutParameter(dbCommand,"?Status", qbmsTransaction.Status);
            
            	Database.PutParameter(dbCommand,"?StatusCode", qbmsTransaction.StatusCode);
            
            	Database.PutParameter(dbCommand,"?StatusMessage", qbmsTransaction.StatusMessage);
            
            	Database.PutParameter(dbCommand,"?TxnType", qbmsTransaction.TxnType);
            
            	Database.PutParameter(dbCommand,"?TxnTimestamp", qbmsTransaction.TxnTimestamp);
            
            	Database.PutParameter(dbCommand,"?MaskedCCN", qbmsTransaction.MaskedCCN);
            
            	Database.PutParameter(dbCommand,"?AuthCode", qbmsTransaction.AuthCode);
            
            	Database.PutParameter(dbCommand,"?TxnId", qbmsTransaction.TxnId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(QbmsTransaction qbmsTransaction)
        {
          	Update(qbmsTransaction, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " CustomerId, "
        
          + " Ticket, "
        
          + " OpId, "
        
          + " Amount, "
        
          + " OpType, "
        
          + " Status, "
        
          + " StatusCode, "
        
          + " StatusMessage, "
        
          + " TxnType, "
        
          + " TxnTimestamp, "
        
          + " MaskedCCN, "
        
          + " AuthCode, "
        
          + " TxnId "
        
          + " From QbmsTransaction "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static QbmsTransaction FindByPrimaryKey(
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

            throw new DataNotFoundException("QbmsTransaction not found, search by primary key");
        }

        public static QbmsTransaction FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(QbmsTransaction qbmsTransaction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",qbmsTransaction.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(QbmsTransaction qbmsTransaction)
        {
        	return Exists(qbmsTransaction, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from QbmsTransaction limit 1";

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

        public static QbmsTransaction Load(IDataReader dataReader, int offset)
        {
              QbmsTransaction qbmsTransaction = new QbmsTransaction();

              qbmsTransaction.Id = dataReader.GetInt32(0 + offset);
                  qbmsTransaction.CustomerId = dataReader.GetInt32(1 + offset);
                  qbmsTransaction.Ticket = dataReader.GetString(2 + offset);
                  qbmsTransaction.OpId = dataReader.GetString(3 + offset);
                  qbmsTransaction.Amount = dataReader.GetDecimal(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    qbmsTransaction.OpType = dataReader.GetString(5 + offset);
                  
                    if(!dataReader.IsDBNull(6 + offset))
                    qbmsTransaction.Status = dataReader.GetString(6 + offset);
                  
                    if(!dataReader.IsDBNull(7 + offset))
                    qbmsTransaction.StatusCode = dataReader.GetString(7 + offset);
                  
                    if(!dataReader.IsDBNull(8 + offset))
                    qbmsTransaction.StatusMessage = dataReader.GetString(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    qbmsTransaction.TxnType = dataReader.GetString(9 + offset);
                  
                    if(!dataReader.IsDBNull(10 + offset))
                    qbmsTransaction.TxnTimestamp = dataReader.GetString(10 + offset);
                  
                    if(!dataReader.IsDBNull(11 + offset))
                    qbmsTransaction.MaskedCCN = dataReader.GetString(11 + offset);
                  
                    if(!dataReader.IsDBNull(12 + offset))
                    qbmsTransaction.AuthCode = dataReader.GetString(12 + offset);
                  
                    if(!dataReader.IsDBNull(13 + offset))
                    qbmsTransaction.TxnId = dataReader.GetString(13 + offset);
                  

            return qbmsTransaction;
        }

        public static QbmsTransaction Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From QbmsTransaction "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(QbmsTransaction qbmsTransaction, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", qbmsTransaction.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(QbmsTransaction qbmsTransaction)
        {
        	Delete(qbmsTransaction, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From QbmsTransaction ";

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
              
                + " CustomerId, "
              
                + " Ticket, "
              
                + " OpId, "
              
                + " Amount, "
              
                + " OpType, "
              
                + " Status, "
              
                + " StatusCode, "
              
                + " StatusMessage, "
              
                + " TxnType, "
              
                + " TxnTimestamp, "
              
                + " MaskedCCN, "
              
                + " AuthCode, "
              
                + " TxnId "
              
                + " From QbmsTransaction ";

        public static List<QbmsTransaction> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<QbmsTransaction> rv = new List<QbmsTransaction>();

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

        public static List<QbmsTransaction> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<QbmsTransaction> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<QbmsTransaction> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbmsTransaction));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(QbmsTransaction item in itemsList)
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

        public static List<QbmsTransaction> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbmsTransaction));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<QbmsTransaction> itemsList = new List<QbmsTransaction>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is QbmsTransaction)
              		itemsList.Add(deserializedObject as QbmsTransaction);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected int m_customerId;
              
        protected String m_ticket;
              
        protected String m_opId;
              
        protected decimal m_amount;
              
        protected String m_opType;
              
        protected String m_status;
              
        protected String m_statusCode;
              
        protected String m_statusMessage;
              
        protected String m_txnType;
              
        protected String m_txnTimestamp;
              
        protected String m_maskedCCN;
              
        protected String m_authCode;
              
        protected String m_txnId;
              
        #endregion

        #region Constructors

        public QbmsTransaction(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public QbmsTransaction(
                int 
                  id,int 
                  customerId,String 
                  ticket,String 
                  opId,decimal 
                  amount,String 
                  opType,String 
                  status,String 
                  statusCode,String 
                  statusMessage,String 
                  txnType,String 
                  txnTimestamp,String 
                  maskedCCN,String 
                  authCode,String 
                  txnId
                ) : this()
        {
            
        	m_id = id;
            
        	m_customerId = customerId;
            
        	m_ticket = ticket;
            
        	m_opId = opId;
            
        	m_amount = amount;
            
        	m_opType = opType;
            
        	m_status = status;
            
        	m_statusCode = statusCode;
            
        	m_statusMessage = statusMessage;
            
        	m_txnType = txnType;
            
        	m_txnTimestamp = txnTimestamp;
            
        	m_maskedCCN = maskedCCN;
            
        	m_authCode = authCode;
            
        	m_txnId = txnId;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public int CustomerId
        {
        	get { return m_customerId;}
            set { m_customerId = value; }
        }
        
        public String Ticket
        {
        	get { return m_ticket;}
            set { m_ticket = value; }
        }
        
        public String OpId
        {
        	get { return m_opId;}
            set { m_opId = value; }
        }
        
        public decimal Amount
        {
        	get { return m_amount;}
            set { m_amount = value; }
        }
        
        public String OpType
        {
        	get { return m_opType;}
            set { m_opType = value; }
        }
        
        public String Status
        {
        	get { return m_status;}
            set { m_status = value; }
        }
        
        public String StatusCode
        {
        	get { return m_statusCode;}
            set { m_statusCode = value; }
        }
        
        public String StatusMessage
        {
        	get { return m_statusMessage;}
            set { m_statusMessage = value; }
        }
        
        public String TxnType
        {
        	get { return m_txnType;}
            set { m_txnType = value; }
        }
        
        public String TxnTimestamp
        {
        	get { return m_txnTimestamp;}
            set { m_txnTimestamp = value; }
        }
        
        public String MaskedCCN
        {
        	get { return m_maskedCCN;}
            set { m_maskedCCN = value; }
        }
        
        public String AuthCode
        {
        	get { return m_authCode;}
            set { m_authCode = value; }
        }
        
        public String TxnId
        {
        	get { return m_txnId;}
            set { m_txnId = value; }
        }
        

        public static int FieldsCount
        {
        	get { return 14; }
        }

        public object Clone()
        {
        	return MemberwiseClone();
        }

    #endregion

    }

}

    