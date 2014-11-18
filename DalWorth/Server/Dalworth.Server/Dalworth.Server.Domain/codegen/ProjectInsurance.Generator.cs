
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class ProjectInsurance : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectInsurance ( " +
      
        " ProjectId, " +
      
        " Company, " +
      
        " Address1, " +
      
        " Address2, " +
      
        " Contact, " +
      
        " Phone, " +
      
        " Fax, " +
      
        " ClaimNumber, " +
      
        " DeductibleAmount " +
      
      ") Values (" +
      
        " ?ProjectId, " +
      
        " ?Company, " +
      
        " ?Address1, " +
      
        " ?Address2, " +
      
        " ?Contact, " +
      
        " ?Phone, " +
      
        " ?Fax, " +
      
        " ?ClaimNumber, " +
      
        " ?DeductibleAmount " +
      
      ")";

      public static void Insert(ProjectInsurance projectInsurance, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectInsurance.ProjectId);
      
        Database.PutParameter(dbCommand,"?Company", projectInsurance.Company);
      
        Database.PutParameter(dbCommand,"?Address1", projectInsurance.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", projectInsurance.Address2);
      
        Database.PutParameter(dbCommand,"?Contact", projectInsurance.Contact);
      
        Database.PutParameter(dbCommand,"?Phone", projectInsurance.Phone);
      
        Database.PutParameter(dbCommand,"?Fax", projectInsurance.Fax);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", projectInsurance.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", projectInsurance.DeductibleAmount);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectInsurance projectInsurance)
      {
        Insert(projectInsurance, null);
      }


      public static void Insert(List<ProjectInsurance>  projectInsuranceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectInsurance projectInsurance in  projectInsuranceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectInsurance.ProjectId);
      
        Database.PutParameter(dbCommand,"?Company", projectInsurance.Company);
      
        Database.PutParameter(dbCommand,"?Address1", projectInsurance.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", projectInsurance.Address2);
      
        Database.PutParameter(dbCommand,"?Contact", projectInsurance.Contact);
      
        Database.PutParameter(dbCommand,"?Phone", projectInsurance.Phone);
      
        Database.PutParameter(dbCommand,"?Fax", projectInsurance.Fax);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", projectInsurance.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", projectInsurance.DeductibleAmount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectId",projectInsurance.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?Company",projectInsurance.Company);
      
        Database.UpdateParameter(dbCommand,"?Address1",projectInsurance.Address1);
      
        Database.UpdateParameter(dbCommand,"?Address2",projectInsurance.Address2);
      
        Database.UpdateParameter(dbCommand,"?Contact",projectInsurance.Contact);
      
        Database.UpdateParameter(dbCommand,"?Phone",projectInsurance.Phone);
      
        Database.UpdateParameter(dbCommand,"?Fax",projectInsurance.Fax);
      
        Database.UpdateParameter(dbCommand,"?ClaimNumber",projectInsurance.ClaimNumber);
      
        Database.UpdateParameter(dbCommand,"?DeductibleAmount",projectInsurance.DeductibleAmount);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectInsurance>  projectInsuranceList)
      {
        Insert(projectInsuranceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectInsurance Set "
      
        + " Company = ?Company, "
      
        + " Address1 = ?Address1, "
      
        + " Address2 = ?Address2, "
      
        + " Contact = ?Contact, "
      
        + " Phone = ?Phone, "
      
        + " Fax = ?Fax, "
      
        + " ClaimNumber = ?ClaimNumber, "
      
        + " DeductibleAmount = ?DeductibleAmount "
      
        + " Where "
        
          + " ProjectId = ?ProjectId "
        
      ;

      public static void Update(ProjectInsurance projectInsurance, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectInsurance.ProjectId);
      
        Database.PutParameter(dbCommand,"?Company", projectInsurance.Company);
      
        Database.PutParameter(dbCommand,"?Address1", projectInsurance.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", projectInsurance.Address2);
      
        Database.PutParameter(dbCommand,"?Contact", projectInsurance.Contact);
      
        Database.PutParameter(dbCommand,"?Phone", projectInsurance.Phone);
      
        Database.PutParameter(dbCommand,"?Fax", projectInsurance.Fax);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", projectInsurance.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", projectInsurance.DeductibleAmount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectInsurance projectInsurance)
      {
        Update(projectInsurance, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ProjectId, "
      
        + " Company, "
      
        + " Address1, "
      
        + " Address2, "
      
        + " Contact, "
      
        + " Phone, "
      
        + " Fax, "
      
        + " ClaimNumber, "
      
        + " DeductibleAmount "
      

      + " From ProjectInsurance "

      
        + " Where "
        
          + " ProjectId = ?ProjectId "
        
      ;

      public static ProjectInsurance FindByPrimaryKey(
      int projectId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectInsurance not found, search by primary key");

      }

      public static ProjectInsurance FindByPrimaryKey(
      int projectId
      )
      {
      return FindByPrimaryKey(
      projectId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectInsurance projectInsurance, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId",projectInsurance.ProjectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectInsurance projectInsurance)
      {
      return Exists(projectInsurance, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectInsurance limit 1";

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

      public static ProjectInsurance Load(IDataReader dataReader, int offset)
      {
      ProjectInsurance projectInsurance = new ProjectInsurance();

      projectInsurance.ProjectId = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            projectInsurance.Company = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            projectInsurance.Address1 = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            projectInsurance.Address2 = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            projectInsurance.Contact = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            projectInsurance.Phone = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            projectInsurance.Fax = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            projectInsurance.ClaimNumber = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            projectInsurance.DeductibleAmount = dataReader.GetDecimal(8 + offset);
          

      return projectInsurance;
      }

      public static ProjectInsurance Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectInsurance "

      
        + " Where "
        
          + " ProjectId = ?ProjectId "
        
      ;
      public static void Delete(ProjectInsurance projectInsurance, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ProjectId", projectInsurance.ProjectId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectInsurance projectInsurance)
      {
        Delete(projectInsurance, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectInsurance ";

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

      
        + " ProjectId, "
      
        + " Company, "
      
        + " Address1, "
      
        + " Address2, "
      
        + " Contact, "
      
        + " Phone, "
      
        + " Fax, "
      
        + " ClaimNumber, "
      
        + " DeductibleAmount "
      

      + " From ProjectInsurance ";
      public static List<ProjectInsurance> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectInsurance> rv = new List<ProjectInsurance>();

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

      public static List<ProjectInsurance> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectInsurance> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectInsurance obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ProjectId == obj.ProjectId && Company == obj.Company && Address1 == obj.Address1 && Address2 == obj.Address2 && Contact == obj.Contact && Phone == obj.Phone && Fax == obj.Fax && ClaimNumber == obj.ClaimNumber && DeductibleAmount == obj.DeductibleAmount;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectInsurance> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectInsurance));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectInsurance item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectInsurance>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectInsurance));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectInsurance> itemsList
      = new List<ProjectInsurance>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectInsurance)
      itemsList.Add(deserializedObject as ProjectInsurance);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_projectId;
      
        protected String m_company;
      
        protected String m_address1;
      
        protected String m_address2;
      
        protected String m_contact;
      
        protected String m_phone;
      
        protected String m_fax;
      
        protected String m_claimNumber;
      
        protected decimal m_deductibleAmount;
      
      #endregion

      #region Constructors
      public ProjectInsurance(
      int 
          projectId
      ) : this()
      {
      
        m_projectId = projectId;
      
      }

      


        public ProjectInsurance(
        int 
          projectId,String 
          company,String 
          address1,String 
          address2,String 
          contact,String 
          phone,String 
          fax,String 
          claimNumber,decimal 
          deductibleAmount
        ) : this()
        {
        
          m_projectId = projectId;
        
          m_company = company;
        
          m_address1 = address1;
        
          m_address2 = address2;
        
          m_contact = contact;
        
          m_phone = phone;
        
          m_fax = fax;
        
          m_claimNumber = claimNumber;
        
          m_deductibleAmount = deductibleAmount;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public String Company
        {
        get { return m_company;}
        set { m_company = value; }
        }
      
        [XmlElement]
        public String Address1
        {
        get { return m_address1;}
        set { m_address1 = value; }
        }
      
        [XmlElement]
        public String Address2
        {
        get { return m_address2;}
        set { m_address2 = value; }
        }
      
        [XmlElement]
        public String Contact
        {
        get { return m_contact;}
        set { m_contact = value; }
        }
      
        [XmlElement]
        public String Phone
        {
        get { return m_phone;}
        set { m_phone = value; }
        }
      
        [XmlElement]
        public String Fax
        {
        get { return m_fax;}
        set { m_fax = value; }
        }
      
        [XmlElement]
        public String ClaimNumber
        {
        get { return m_claimNumber;}
        set { m_claimNumber = value; }
        }
      
        [XmlElement]
        public decimal DeductibleAmount
        {
        get { return m_deductibleAmount;}
        set { m_deductibleAmount = value; }
        }
      

      public static int FieldsCount
      {
      get { return 9; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    