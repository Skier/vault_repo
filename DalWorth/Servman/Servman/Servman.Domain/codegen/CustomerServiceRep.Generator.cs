
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Servman.Data;
    using Servman.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Servman.Domain
      {

      public partial class CustomerServiceRep : ICloneable
      {

      #region Store


      #region Save

      public static CustomerServiceRep Save(CustomerServiceRep customerServiceRep, IDbConnection connection)
      {
      	if (!Exists(customerServiceRep, connection))
      		Insert(customerServiceRep, connection);
      	else
      		Update(customerServiceRep, connection);
      	return customerServiceRep;
      }

      public static CustomerServiceRep Save(CustomerServiceRep customerServiceRep)
      {
      	if (!Exists(customerServiceRep))
      		Insert(customerServiceRep);
      	else
      		Update(customerServiceRep);
      	return customerServiceRep;
      }

      #endregion


      #region Insert

      private const String SqlInsert = "Insert Into CustomerServiceRep ( " +
      
        " UserId, " +
      
        " ShowAs, " +
      
        " QbEmployeeRecordId, " +
      
        " QbVendorRecordId, " +
      
        " IsActive " +
      
      ") Values (" +
      
        " ?UserId, " +
      
        " ?ShowAs, " +
      
        " ?QbEmployeeRecordId, " +
      
        " ?QbVendorRecordId, " +
      
        " ?IsActive " +
      
      ")";

      public static void Insert(CustomerServiceRep customerServiceRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?UserId", customerServiceRep.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", customerServiceRep.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbEmployeeRecordId", customerServiceRep.QbEmployeeRecordId);
      
        Database.PutParameter(dbCommand,"?QbVendorRecordId", customerServiceRep.QbVendorRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", customerServiceRep.IsActive);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customerServiceRep.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(CustomerServiceRep customerServiceRep)
      {
        Insert(customerServiceRep, null);
      }


      public static void Insert(List<CustomerServiceRep>  customerServiceRepList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(CustomerServiceRep customerServiceRep in  customerServiceRepList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?UserId", customerServiceRep.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", customerServiceRep.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbEmployeeRecordId", customerServiceRep.QbEmployeeRecordId);
      
        Database.PutParameter(dbCommand,"?QbVendorRecordId", customerServiceRep.QbVendorRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", customerServiceRep.IsActive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?UserId",customerServiceRep.UserId);
      
        Database.UpdateParameter(dbCommand,"?ShowAs",customerServiceRep.ShowAs);
      
        Database.UpdateParameter(dbCommand,"?QbEmployeeRecordId",customerServiceRep.QbEmployeeRecordId);
      
        Database.UpdateParameter(dbCommand,"?QbVendorRecordId",customerServiceRep.QbVendorRecordId);
      
        Database.UpdateParameter(dbCommand,"?IsActive",customerServiceRep.IsActive);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customerServiceRep.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<CustomerServiceRep>  customerServiceRepList)
      {
        Insert(customerServiceRepList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update CustomerServiceRep Set "
      
        + " UserId = ?UserId, "
      
        + " ShowAs = ?ShowAs, "
      
        + " QbEmployeeRecordId = ?QbEmployeeRecordId, "
      
        + " QbVendorRecordId = ?QbVendorRecordId, "
      
        + " IsActive = ?IsActive "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(CustomerServiceRep customerServiceRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", customerServiceRep.Id);
      
        Database.PutParameter(dbCommand,"?UserId", customerServiceRep.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", customerServiceRep.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbEmployeeRecordId", customerServiceRep.QbEmployeeRecordId);
      
        Database.PutParameter(dbCommand,"?QbVendorRecordId", customerServiceRep.QbVendorRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", customerServiceRep.IsActive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(CustomerServiceRep customerServiceRep)
      {
        Update(customerServiceRep, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " UserId, "
      
        + " ShowAs, "
      
        + " QbEmployeeRecordId, "
      
        + " QbVendorRecordId, "
      
        + " IsActive "
      

      + " From CustomerServiceRep "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static CustomerServiceRep FindByPrimaryKey(
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
      throw new DataNotFoundException("CustomerServiceRep not found, search by primary key");

      }

      public static CustomerServiceRep FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(CustomerServiceRep customerServiceRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",customerServiceRep.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(CustomerServiceRep customerServiceRep)
      {
      return Exists(customerServiceRep, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from CustomerServiceRep limit 1";

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

      public static CustomerServiceRep Load(IDataReader dataReader, int offset)
      {
      CustomerServiceRep customerServiceRep = new CustomerServiceRep();

      customerServiceRep.Id = dataReader.GetInt32(0 + offset);
          customerServiceRep.UserId = dataReader.GetInt32(1 + offset);
          customerServiceRep.ShowAs = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            customerServiceRep.QbEmployeeRecordId = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            customerServiceRep.QbVendorRecordId = dataReader.GetString(4 + offset);
          customerServiceRep.IsActive = dataReader.GetBoolean(5 + offset);
          

      return customerServiceRep;
      }

      public static CustomerServiceRep Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerServiceRep "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(CustomerServiceRep customerServiceRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", customerServiceRep.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(CustomerServiceRep customerServiceRep)
      {
        Delete(customerServiceRep, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From CustomerServiceRep ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
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
      
        + " UserId, "
      
        + " ShowAs, "
      
        + " QbEmployeeRecordId, "
      
        + " QbVendorRecordId, "
      
        + " IsActive "
      

      + " From CustomerServiceRep ";
      public static List<CustomerServiceRep> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<CustomerServiceRep> rv = new List<CustomerServiceRep>();

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

      public static List<CustomerServiceRep> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<CustomerServiceRep> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<CustomerServiceRep> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerServiceRep));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CustomerServiceRep item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerServiceRep>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerServiceRep));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CustomerServiceRep> itemsList
      = new List<CustomerServiceRep>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerServiceRep)
      itemsList.Add(deserializedObject as CustomerServiceRep);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int m_userId;
      
        protected String m_showAs;
      
        protected String m_qbEmployeeRecordId;
      
        protected String m_qbVendorRecordId;
      
        protected bool m_isActive;
      
      #endregion

      #region Constructors
      public CustomerServiceRep(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public CustomerServiceRep(
        int 
          id,int 
          userId,String 
          showAs,String 
          qbEmployeeRecordId,String 
          qbVendorRecordId,bool 
          isActive
        ) : this()
        {
        
          m_id = id;
        
          m_userId = userId;
        
          m_showAs = showAs;
        
          m_qbEmployeeRecordId = qbEmployeeRecordId;
        
          m_qbVendorRecordId = qbVendorRecordId;
        
          m_isActive = isActive;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        public String ShowAs
        {
        get { return m_showAs;}
        set { m_showAs = value; }
        }
      
        public String QbEmployeeRecordId
        {
        get { return m_qbEmployeeRecordId;}
        set { m_qbEmployeeRecordId = value; }
        }
      
        public String QbVendorRecordId
        {
        get { return m_qbVendorRecordId;}
        set { m_qbVendorRecordId = value; }
        }
      
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    