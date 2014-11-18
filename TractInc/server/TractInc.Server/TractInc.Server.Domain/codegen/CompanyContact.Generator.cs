
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class CompanyContact
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [CompanyContact] ( " +
      
        " PersonId, " +
      
        " ContractId, " +
      
        " StartDate, " +
      
        " EndDate " +
      
      ") Values (" +
      
        " @PersonId, " +
      
        " @ContractId, " +
      
        " @StartDate, " +
      
        " @EndDate " +
      
      ")";

      public static void Insert(CompanyContact companyContact)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@PersonId", companyContact.PersonId);
      
        Database.PutParameter(dbCommand,"@ContractId", companyContact.ContractId);
      
        Database.PutParameter(dbCommand,"@StartDate", companyContact.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", companyContact.EndDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          companyContact.CompanyContactId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<CompanyContact>  companyContactList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CompanyContact companyContact in  companyContactList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@PersonId", companyContact.PersonId);
      
        Database.PutParameter(dbCommand,"@ContractId", companyContact.ContractId);
      
        Database.PutParameter(dbCommand,"@StartDate", companyContact.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", companyContact.EndDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@PersonId",companyContact.PersonId);
      
        Database.UpdateParameter(dbCommand,"@ContractId",companyContact.ContractId);
      
        Database.UpdateParameter(dbCommand,"@StartDate",companyContact.StartDate);
      
        Database.UpdateParameter(dbCommand,"@EndDate",companyContact.EndDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        companyContact.CompanyContactId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [CompanyContact] Set "
      
        + " PersonId = @PersonId, "
      
        + " ContractId = @ContractId, "
      
        + " StartDate = @StartDate, "
      
        + " EndDate = @EndDate "
      
        + " Where "
        
          + " CompanyContactId = @CompanyContactId "
        
      ;

      public static void Update(CompanyContact companyContact)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@CompanyContactId", companyContact.CompanyContactId);
      
        Database.PutParameter(dbCommand,"@PersonId", companyContact.PersonId);
      
        Database.PutParameter(dbCommand,"@ContractId", companyContact.ContractId);
      
        Database.PutParameter(dbCommand,"@StartDate", companyContact.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", companyContact.EndDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CompanyContactId, "
      
        + " PersonId, "
      
        + " ContractId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [CompanyContact] "

      
        + " Where "
        
          + " CompanyContactId = @CompanyContactId "
        
      ;

      public static CompanyContact FindByPrimaryKey(
      int companyContactId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CompanyContactId", companyContactId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CompanyContact not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CompanyContact companyContact)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CompanyContactId",companyContact.CompanyContactId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from CompanyContact";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static CompanyContact Load(IDataReader dataReader)
      {
      CompanyContact companyContact = new CompanyContact();

      companyContact.CompanyContactId = dataReader.GetInt32(0);
          companyContact.PersonId = dataReader.GetInt32(1);
          companyContact.ContractId = dataReader.GetInt32(2);
          companyContact.StartDate = dataReader.GetDateTime(3);
          if ( dataReader[4] != DBNull.Value ) {
            companyContact.EndDate = dataReader.GetDateTime(4);
          }
          

      return companyContact;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [CompanyContact] "

      
        + " Where "
        
          + " CompanyContactId = @CompanyContactId "
        
      ;
      public static void Delete(CompanyContact companyContact)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CompanyContactId", companyContact.CompanyContactId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [CompanyContact] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " CompanyContactId, "
      
        + " PersonId, "
      
        + " ContractId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [CompanyContact] ";
      public static List<CompanyContact> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<CompanyContact> rv = new List<CompanyContact>();

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
      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<CompanyContact> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<CompanyContact> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CompanyContact));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CompanyContact item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CompanyContact>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CompanyContact));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CompanyContact> itemsList
      = new List<CompanyContact>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CompanyContact)
      itemsList.Add(deserializedObject as CompanyContact);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_companyContactId;
      
        protected int m_personId;
      
        protected int m_contractId;
      
        protected DateTime m_startDate;
      
        protected DateTime m_endDate;
      
      #endregion

      #region Constructors
      public CompanyContact(
      int 
          companyContactId
      )
      {
      
        m_companyContactId = companyContactId;
      
      }

      


        public CompanyContact(
        int 
          companyContactId,int 
          personId,int 
          contractId,DateTime 
          startDate,DateTime 
          endDate
        )
        {
        
          m_companyContactId = companyContactId;
        
          m_personId = personId;
        
          m_contractId = contractId;
        
          m_startDate = startDate;
        
          m_endDate = endDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int CompanyContactId
        {
        get { return m_companyContactId;}
        set { m_companyContactId = value; }
        }
      
        [XmlElement]
        public int PersonId
        {
        get { return m_personId;}
        set { m_personId = value; }
        }
      
        [XmlElement]
        public int ContractId
        {
        get { return m_contractId;}
        set { m_contractId = value; }
        }
      
        [XmlElement]
        public DateTime StartDate
        {
        get { return m_startDate;}
        set { m_startDate = value; }
        }
      
        [XmlElement]
        public DateTime EndDate
        {
        get { return m_endDate;}
        set { m_endDate = value; }
        }
      
      }
      #endregion
      }

    