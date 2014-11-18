
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

      public partial class LeadToPartner : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into LeadToPartner ( " +
      
        " LeadStatusId, " +
      
        " BusinessPartnerId, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Phone, " +
      
        " CustomerNotes, " +
      
        " EmployeeNotes, " +
      
        " PartnerProjectTypeId, " +
      
        " ClosedAmount, " +
      
        " CommissionAmount " +
      
      ") Values (" +
      
        " ?LeadStatusId, " +
      
        " ?BusinessPartnerId, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?Phone, " +
      
        " ?CustomerNotes, " +
      
        " ?EmployeeNotes, " +
      
        " ?PartnerProjectTypeId, " +
      
        " ?ClosedAmount, " +
      
        " ?CommissionAmount " +
      
      ")";

      public static void Insert(LeadToPartner leadToPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?LeadStatusId", leadToPartner.LeadStatusId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", leadToPartner.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?FirstName", leadToPartner.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", leadToPartner.LastName);
      
        Database.PutParameter(dbCommand,"?Phone", leadToPartner.Phone);
      
        Database.PutParameter(dbCommand,"?CustomerNotes", leadToPartner.CustomerNotes);
      
        Database.PutParameter(dbCommand,"?EmployeeNotes", leadToPartner.EmployeeNotes);
      
        Database.PutParameter(dbCommand,"?PartnerProjectTypeId", leadToPartner.PartnerProjectTypeId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", leadToPartner.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?CommissionAmount", leadToPartner.CommissionAmount);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        leadToPartner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(LeadToPartner leadToPartner)
      {
        Insert(leadToPartner, null);
      }


      public static void Insert(List<LeadToPartner>  leadToPartnerList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(LeadToPartner leadToPartner in  leadToPartnerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?LeadStatusId", leadToPartner.LeadStatusId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", leadToPartner.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?FirstName", leadToPartner.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", leadToPartner.LastName);
      
        Database.PutParameter(dbCommand,"?Phone", leadToPartner.Phone);
      
        Database.PutParameter(dbCommand,"?CustomerNotes", leadToPartner.CustomerNotes);
      
        Database.PutParameter(dbCommand,"?EmployeeNotes", leadToPartner.EmployeeNotes);
      
        Database.PutParameter(dbCommand,"?PartnerProjectTypeId", leadToPartner.PartnerProjectTypeId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", leadToPartner.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?CommissionAmount", leadToPartner.CommissionAmount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?LeadStatusId",leadToPartner.LeadStatusId);
      
        Database.UpdateParameter(dbCommand,"?BusinessPartnerId",leadToPartner.BusinessPartnerId);
      
        Database.UpdateParameter(dbCommand,"?FirstName",leadToPartner.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",leadToPartner.LastName);
      
        Database.UpdateParameter(dbCommand,"?Phone",leadToPartner.Phone);
      
        Database.UpdateParameter(dbCommand,"?CustomerNotes",leadToPartner.CustomerNotes);
      
        Database.UpdateParameter(dbCommand,"?EmployeeNotes",leadToPartner.EmployeeNotes);
      
        Database.UpdateParameter(dbCommand,"?PartnerProjectTypeId",leadToPartner.PartnerProjectTypeId);
      
        Database.UpdateParameter(dbCommand,"?ClosedAmount",leadToPartner.ClosedAmount);
      
        Database.UpdateParameter(dbCommand,"?CommissionAmount",leadToPartner.CommissionAmount);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        leadToPartner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<LeadToPartner>  leadToPartnerList)
      {
        Insert(leadToPartnerList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update LeadToPartner Set "
      
        + " LeadStatusId = ?LeadStatusId, "
      
        + " BusinessPartnerId = ?BusinessPartnerId, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " Phone = ?Phone, "
      
        + " CustomerNotes = ?CustomerNotes, "
      
        + " EmployeeNotes = ?EmployeeNotes, "
      
        + " PartnerProjectTypeId = ?PartnerProjectTypeId, "
      
        + " ClosedAmount = ?ClosedAmount, "
      
        + " CommissionAmount = ?CommissionAmount "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(LeadToPartner leadToPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", leadToPartner.Id);
      
        Database.PutParameter(dbCommand,"?LeadStatusId", leadToPartner.LeadStatusId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", leadToPartner.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?FirstName", leadToPartner.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", leadToPartner.LastName);
      
        Database.PutParameter(dbCommand,"?Phone", leadToPartner.Phone);
      
        Database.PutParameter(dbCommand,"?CustomerNotes", leadToPartner.CustomerNotes);
      
        Database.PutParameter(dbCommand,"?EmployeeNotes", leadToPartner.EmployeeNotes);
      
        Database.PutParameter(dbCommand,"?PartnerProjectTypeId", leadToPartner.PartnerProjectTypeId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", leadToPartner.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?CommissionAmount", leadToPartner.CommissionAmount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(LeadToPartner leadToPartner)
      {
        Update(leadToPartner, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " LeadStatusId, "
      
        + " BusinessPartnerId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone, "
      
        + " CustomerNotes, "
      
        + " EmployeeNotes, "
      
        + " PartnerProjectTypeId, "
      
        + " ClosedAmount, "
      
        + " CommissionAmount "
      

      + " From LeadToPartner "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static LeadToPartner FindByPrimaryKey(
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
      throw new DataNotFoundException("LeadToPartner not found, search by primary key");

      }

      public static LeadToPartner FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(LeadToPartner leadToPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",leadToPartner.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(LeadToPartner leadToPartner)
      {
      return Exists(leadToPartner, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from LeadToPartner limit 1";

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

      public static LeadToPartner Load(IDataReader dataReader, int offset)
      {
      LeadToPartner leadToPartner = new LeadToPartner();

      leadToPartner.Id = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            leadToPartner.LeadStatusId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            leadToPartner.BusinessPartnerId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            leadToPartner.FirstName = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            leadToPartner.LastName = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            leadToPartner.Phone = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            leadToPartner.CustomerNotes = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            leadToPartner.EmployeeNotes = dataReader.GetString(7 + offset);
          leadToPartner.PartnerProjectTypeId = dataReader.GetInt32(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            leadToPartner.ClosedAmount = dataReader.GetDecimal(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            leadToPartner.CommissionAmount = dataReader.GetDecimal(10 + offset);
          

      return leadToPartner;
      }

      public static LeadToPartner Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From LeadToPartner "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(LeadToPartner leadToPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", leadToPartner.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(LeadToPartner leadToPartner)
      {
        Delete(leadToPartner, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From LeadToPartner ";

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
      
        + " LeadStatusId, "
      
        + " BusinessPartnerId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone, "
      
        + " CustomerNotes, "
      
        + " EmployeeNotes, "
      
        + " PartnerProjectTypeId, "
      
        + " ClosedAmount, "
      
        + " CommissionAmount "
      

      + " From LeadToPartner ";
      public static List<LeadToPartner> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<LeadToPartner> rv = new List<LeadToPartner>();

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

      public static List<LeadToPartner> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<LeadToPartner> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<LeadToPartner> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadToPartner));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(LeadToPartner item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<LeadToPartner>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadToPartner));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<LeadToPartner> itemsList
      = new List<LeadToPartner>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is LeadToPartner)
      itemsList.Add(deserializedObject as LeadToPartner);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int? m_leadStatusId;
      
        protected int? m_businessPartnerId;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_phone;
      
        protected String m_customerNotes;
      
        protected String m_employeeNotes;
      
        protected int m_partnerProjectTypeId;
      
        protected decimal m_closedAmount;
      
        protected decimal m_commissionAmount;
      
      #endregion

      #region Constructors
      public LeadToPartner(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public LeadToPartner(
        int 
          id,int? 
          leadStatusId,int? 
          businessPartnerId,String 
          firstName,String 
          lastName,String 
          phone,String 
          customerNotes,String 
          employeeNotes,int 
          partnerProjectTypeId,decimal 
          closedAmount,decimal 
          commissionAmount
        ) : this()
        {
        
          m_id = id;
        
          m_leadStatusId = leadStatusId;
        
          m_businessPartnerId = businessPartnerId;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_phone = phone;
        
          m_customerNotes = customerNotes;
        
          m_employeeNotes = employeeNotes;
        
          m_partnerProjectTypeId = partnerProjectTypeId;
        
          m_closedAmount = closedAmount;
        
          m_commissionAmount = commissionAmount;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int? LeadStatusId
        {
        get { return m_leadStatusId;}
        set { m_leadStatusId = value; }
        }
      
        public int? BusinessPartnerId
        {
        get { return m_businessPartnerId;}
        set { m_businessPartnerId = value; }
        }
      
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      
        public String Phone
        {
        get { return m_phone;}
        set { m_phone = value; }
        }
      
        public String CustomerNotes
        {
        get { return m_customerNotes;}
        set { m_customerNotes = value; }
        }
      
        public String EmployeeNotes
        {
        get { return m_employeeNotes;}
        set { m_employeeNotes = value; }
        }
      
        public int PartnerProjectTypeId
        {
        get { return m_partnerProjectTypeId;}
        set { m_partnerProjectTypeId = value; }
        }
      
        public decimal ClosedAmount
        {
        get { return m_closedAmount;}
        set { m_closedAmount = value; }
        }
      
        public decimal CommissionAmount
        {
        get { return m_commissionAmount;}
        set { m_commissionAmount = value; }
        }
      

      public static int FieldsCount
      {
      get { return 11; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    