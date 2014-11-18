
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  

namespace Dalworth.LeadCentral.Domain
{

    public partial class Transaction : ICloneable
    {

        #region Store


        #region Save

        public static Transaction Save(Transaction transaction, IDbConnection connection)
        {
        	if (!Exists(transaction, connection))
        		Insert(transaction, connection);
        	else
        		Update(transaction, connection);
        	return transaction;
        }

        public static Transaction Save(Transaction transaction)
        {
        	if (!Exists(transaction))
        		Insert(transaction);
        	else
        		Update(transaction);
        	return transaction;
        }

        #endregion


        #region Insert

        private const String SqlInsert = "Insert Into Transaction ( " +
        
          " TransactionDate, " +
        
          " TransactionTypeId, " +
        
          " TrackingPhoneId, " +
        
          " PhoneCallId, " +
        
          " PhoneSmsId, " +
        
          " Quantity, " +
        
          " Amount, " +
        
          " CurrentBalance, " +
        
          " QbmsTransactionId " +
        
        ") Values (" +
        
          " ?TransactionDate, " +
        
          " ?TransactionTypeId, " +
        
          " ?TrackingPhoneId, " +
        
          " ?PhoneCallId, " +
        
          " ?PhoneSmsId, " +
        
          " ?Quantity, " +
        
          " ?Amount, " +
        
          " ?CurrentBalance, " +
        
          " ?QbmsTransactionId " +
        
        ")";

        public static void Insert(Transaction transaction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
            
            	Database.PutParameter(dbCommand,"?TransactionDate", transaction.TransactionDate);
            
            	Database.PutParameter(dbCommand,"?TransactionTypeId", transaction.TransactionTypeId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", transaction.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", transaction.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", transaction.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?Quantity", transaction.Quantity);
            
            	Database.PutParameter(dbCommand,"?Amount", transaction.Amount);
            
            	Database.PutParameter(dbCommand,"?CurrentBalance", transaction.CurrentBalance);
            
            	Database.PutParameter(dbCommand,"?QbmsTransactionId", transaction.QbmsTransactionId);
            
            	dbCommand.ExecuteNonQuery();
            
              	using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
              	{
              		transaction.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
              	}
            
            }
        }

        public static void Insert(Transaction transaction)
        {
          	Insert(transaction, null);
        }

        public static void Insert(List<Transaction>  transactionList, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                bool parametersAdded = false;

                foreach(Transaction transaction in  transactionList)
                {
                	if(!parametersAdded)
                {
                
                  	Database.PutParameter(dbCommand,"?TransactionDate", transaction.TransactionDate);
                
                  	Database.PutParameter(dbCommand,"?TransactionTypeId", transaction.TransactionTypeId);
                
                  	Database.PutParameter(dbCommand,"?TrackingPhoneId", transaction.TrackingPhoneId);
                
                  	Database.PutParameter(dbCommand,"?PhoneCallId", transaction.PhoneCallId);
                
                  	Database.PutParameter(dbCommand,"?PhoneSmsId", transaction.PhoneSmsId);
                
                  	Database.PutParameter(dbCommand,"?Quantity", transaction.Quantity);
                
                  	Database.PutParameter(dbCommand,"?Amount", transaction.Amount);
                
                  	Database.PutParameter(dbCommand,"?CurrentBalance", transaction.CurrentBalance);
                
                  	Database.PutParameter(dbCommand,"?QbmsTransactionId", transaction.QbmsTransactionId);
                
                	parametersAdded = true;
                }
                else
                {
                
                	Database.UpdateParameter(dbCommand,"?TransactionDate",transaction.TransactionDate);
                
                	Database.UpdateParameter(dbCommand,"?TransactionTypeId",transaction.TransactionTypeId);
                
                	Database.UpdateParameter(dbCommand,"?TrackingPhoneId",transaction.TrackingPhoneId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneCallId",transaction.PhoneCallId);
                
                	Database.UpdateParameter(dbCommand,"?PhoneSmsId",transaction.PhoneSmsId);
                
                	Database.UpdateParameter(dbCommand,"?Quantity",transaction.Quantity);
                
                	Database.UpdateParameter(dbCommand,"?Amount",transaction.Amount);
                
                	Database.UpdateParameter(dbCommand,"?CurrentBalance",transaction.CurrentBalance);
                
                	Database.UpdateParameter(dbCommand,"?QbmsTransactionId",transaction.QbmsTransactionId);
                
                }

                dbCommand.ExecuteNonQuery();

                
                    using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
                    {
                    	transaction.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
                    }
                
                }
            }
        }

        public static void Insert(List<Transaction>  transactionList)
        {
        	Insert(transactionList, null);
        }

        #endregion

        #region Update

        private const String SqlUpdate = "Update Transaction Set "
          
            + " TransactionDate = ?TransactionDate, "
          
            + " TransactionTypeId = ?TransactionTypeId, "
          
            + " TrackingPhoneId = ?TrackingPhoneId, "
          
            + " PhoneCallId = ?PhoneCallId, "
          
            + " PhoneSmsId = ?PhoneSmsId, "
          
            + " Quantity = ?Quantity, "
          
            + " Amount = ?Amount, "
          
            + " CurrentBalance = ?CurrentBalance, "
          
            + " QbmsTransactionId = ?QbmsTransactionId "
          
            + " Where "
            
            + " Id = ?Id "
            ;

        public static void Update(Transaction transaction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
            {
            
            	Database.PutParameter(dbCommand,"?Id", transaction.Id);
            
            	Database.PutParameter(dbCommand,"?TransactionDate", transaction.TransactionDate);
            
            	Database.PutParameter(dbCommand,"?TransactionTypeId", transaction.TransactionTypeId);
            
            	Database.PutParameter(dbCommand,"?TrackingPhoneId", transaction.TrackingPhoneId);
            
            	Database.PutParameter(dbCommand,"?PhoneCallId", transaction.PhoneCallId);
            
            	Database.PutParameter(dbCommand,"?PhoneSmsId", transaction.PhoneSmsId);
            
            	Database.PutParameter(dbCommand,"?Quantity", transaction.Quantity);
            
            	Database.PutParameter(dbCommand,"?Amount", transaction.Amount);
            
            	Database.PutParameter(dbCommand,"?CurrentBalance", transaction.CurrentBalance);
            
            	Database.PutParameter(dbCommand,"?QbmsTransactionId", transaction.QbmsTransactionId);
            
            	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Update(Transaction transaction)
        {
          	Update(transaction, null);
        }

        #endregion

        #region FindByPrimaryKey

        private const String SqlSelectByPk = "Select "

        
          + " Id, "
        
          + " TransactionDate, "
        
          + " TransactionTypeId, "
        
          + " TrackingPhoneId, "
        
          + " PhoneCallId, "
        
          + " PhoneSmsId, "
        
          + " Quantity, "
        
          + " Amount, "
        
          + " CurrentBalance, "
        
          + " QbmsTransactionId "
        
          + " From Transaction "
        
          + " Where "
          
          + " Id = ?Id "
          ;

        public static Transaction FindByPrimaryKey(
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

            throw new DataNotFoundException("Transaction not found, search by primary key");
        }

        public static Transaction FindByPrimaryKey(
              int id
              )
        {
        	return FindByPrimaryKey(
              id, null
            );
        }


        #endregion

        #region Exists

        public static bool Exists(Transaction transaction, IDbConnection connection)
        {
            using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
            
              	Database.PutParameter(dbCommand,"?Id",transaction.Id);
            

            	using(IDataReader dataReader = dbCommand.ExecuteReader())
            	{
            		return dataReader.Read();
            	}
            }
        }

        public static bool Exists(Transaction transaction)
        {
        	return Exists(transaction, null);
        }

        #endregion

        #region IsContainsData

        public static bool IsContainsData(IDbConnection connection)
        {
        	String sql = "select * from Transaction limit 1";

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

        public static Transaction Load(IDataReader dataReader, int offset)
        {
              Transaction transaction = new Transaction();

              transaction.Id = dataReader.GetInt32(0 + offset);
                  transaction.TransactionDate = dataReader.GetDateTime(1 + offset);
                  transaction.TransactionTypeId = dataReader.GetInt32(2 + offset);
                  
                    if(!dataReader.IsDBNull(3 + offset))
                    transaction.TrackingPhoneId = dataReader.GetInt32(3 + offset);
                  
                    if(!dataReader.IsDBNull(4 + offset))
                    transaction.PhoneCallId = dataReader.GetInt32(4 + offset);
                  
                    if(!dataReader.IsDBNull(5 + offset))
                    transaction.PhoneSmsId = dataReader.GetInt32(5 + offset);
                  transaction.Quantity = dataReader.GetDecimal(6 + offset);
                  transaction.Amount = dataReader.GetDecimal(7 + offset);
                  transaction.CurrentBalance = dataReader.GetDecimal(8 + offset);
                  
                    if(!dataReader.IsDBNull(9 + offset))
                    transaction.QbmsTransactionId = dataReader.GetInt32(9 + offset);
                  

            return transaction;
        }

        public static Transaction Load(IDataReader dataReader)
        {
        	return Load(dataReader, 0);
        }

        #endregion

        #region Delete

        private const String SqlDelete = "Delete From Transaction "

              
                + " Where "
                
                  + " Id = ?Id "
                ;

        public static void Delete(Transaction transaction, IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {

              
            	Database.PutParameter(dbCommand,"?Id", transaction.Id);
              
              	dbCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(Transaction transaction)
        {
        	Delete(transaction, null);
        }

        #endregion

        #region Clear

        private const String SqlDeleteAll = "Delete From Transaction ";

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
              
                + " TransactionDate, "
              
                + " TransactionTypeId, "
              
                + " TrackingPhoneId, "
              
                + " PhoneCallId, "
              
                + " PhoneSmsId, "
              
                + " Quantity, "
              
                + " Amount, "
              
                + " CurrentBalance, "
              
                + " QbmsTransactionId "
              
                + " From Transaction ";

        public static List<Transaction> Find(IDbConnection connection)
        {
        	using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
            {
            	List<Transaction> rv = new List<Transaction>();

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

        public static List<Transaction> Find()
        {
        	return Find(null);
        }

        #endregion

        #region Import from file

        public static int Import(String xmlFilePath)
        {
        	List<Transaction> itemsList = Load(xmlFilePath);

            if(itemsList.Count != 0)
            	Insert(itemsList);

        	return itemsList.Count;
        }

        #endregion

        #region Export to file

        public static int Export(String xmlFilePath)
        {
        	List<Transaction> itemsList = Find();

            if (itemsList.Count == 0)
              	return 0;


            XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Transaction));

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Root");

            foreach(Transaction item in itemsList)
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

        public static List<Transaction> Load(String xmlFilePath)
        {
        	XmlSerializer xmlSerializer = new XmlSerializer(typeof(Transaction));
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.Load(xmlFilePath);

            List<Transaction> itemsList = new List<Transaction>();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
            {
              	Object deserializedObject = xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

              	if (deserializedObject is Transaction)
              		itemsList.Add(deserializedObject as Transaction);
            }

            return itemsList;
        }

        #endregion

        #endregion

        #region Biz
            

        #region Fields
              
        protected int m_id;
              
        protected DateTime m_transactionDate;
              
        protected int m_transactionTypeId;
              
        protected int? m_trackingPhoneId;
              
        protected int? m_phoneCallId;
              
        protected int? m_phoneSmsId;
              
        protected decimal m_quantity;
              
        protected decimal m_amount;
              
        protected decimal m_currentBalance;
              
        protected int? m_qbmsTransactionId;
              
        #endregion

        #region Constructors

        public Transaction(
              int 
                  id
              ) : this()
        {
            
        	m_id = id;
            
        }

        

        public Transaction(
                int 
                  id,DateTime 
                  transactionDate,int 
                  transactionTypeId,int? 
                  trackingPhoneId,int? 
                  phoneCallId,int? 
                  phoneSmsId,decimal 
                  quantity,decimal 
                  amount,decimal 
                  currentBalance,int? 
                  qbmsTransactionId
                ) : this()
        {
            
        	m_id = id;
            
        	m_transactionDate = transactionDate;
            
        	m_transactionTypeId = transactionTypeId;
            
        	m_trackingPhoneId = trackingPhoneId;
            
        	m_phoneCallId = phoneCallId;
            
        	m_phoneSmsId = phoneSmsId;
            
        	m_quantity = quantity;
            
        	m_amount = amount;
            
        	m_currentBalance = currentBalance;
            
        	m_qbmsTransactionId = qbmsTransactionId;
            
        }

        

        #endregion

        
        public int Id
        {
        	get { return m_id;}
            set { m_id = value; }
        }
        
        public DateTime TransactionDate
        {
        	get { return m_transactionDate;}
            set { m_transactionDate = value; }
        }
        
        public int TransactionTypeId
        {
        	get { return m_transactionTypeId;}
            set { m_transactionTypeId = value; }
        }
        
        public int? TrackingPhoneId
        {
        	get { return m_trackingPhoneId;}
            set { m_trackingPhoneId = value; }
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
        
        public decimal Quantity
        {
        	get { return m_quantity;}
            set { m_quantity = value; }
        }
        
        public decimal Amount
        {
        	get { return m_amount;}
            set { m_amount = value; }
        }
        
        public decimal CurrentBalance
        {
        	get { return m_currentBalance;}
            set { m_currentBalance = value; }
        }
        
        public int? QbmsTransactionId
        {
        	get { return m_qbmsTransactionId;}
            set { m_qbmsTransactionId = value; }
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

    