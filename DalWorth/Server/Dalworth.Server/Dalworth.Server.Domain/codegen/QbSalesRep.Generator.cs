
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


      public partial class QbSalesRep : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbSalesRep ( " +
      
        " ListId, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence, " +
      
        " Initial, " +
      
        " IsActive, " +
      
        " IsEmployee, " +
      
        " RefListId, " +
      
        " FullName, " +
      
        " EmployeeId, " +
      
        " QbCustomerTypeListId, " +
      
        " QbSalesRepTypeId, " +
      
        " FirstName, " +
      
        " LastName " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence, " +
      
        " ?Initial, " +
      
        " ?IsActive, " +
      
        " ?IsEmployee, " +
      
        " ?RefListId, " +
      
        " ?FullName, " +
      
        " ?EmployeeId, " +
      
        " ?QbCustomerTypeListId, " +
      
        " ?QbSalesRepTypeId, " +
      
        " ?FirstName, " +
      
        " ?LastName " +
      
      ")";

      public static void Insert(QbSalesRep qbSalesRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbSalesRep.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbSalesRep.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbSalesRep.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbSalesRep.EditSequence);
      
        Database.PutParameter(dbCommand,"?Initial", qbSalesRep.Initial);
      
        Database.PutParameter(dbCommand,"?IsActive", qbSalesRep.IsActive);
      
        Database.PutParameter(dbCommand,"?IsEmployee", qbSalesRep.IsEmployee);
      
        Database.PutParameter(dbCommand,"?RefListId", qbSalesRep.RefListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbSalesRep.FullName);
      
        Database.PutParameter(dbCommand,"?EmployeeId", qbSalesRep.EmployeeId);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", qbSalesRep.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepTypeId", qbSalesRep.QbSalesRepTypeId);
      
        Database.PutParameter(dbCommand,"?FirstName", qbSalesRep.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", qbSalesRep.LastName);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbSalesRep qbSalesRep)
      {
        Insert(qbSalesRep, null);
      }


      public static void Insert(List<QbSalesRep>  qbSalesRepList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbSalesRep qbSalesRep in  qbSalesRepList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbSalesRep.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbSalesRep.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbSalesRep.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbSalesRep.EditSequence);
      
        Database.PutParameter(dbCommand,"?Initial", qbSalesRep.Initial);
      
        Database.PutParameter(dbCommand,"?IsActive", qbSalesRep.IsActive);
      
        Database.PutParameter(dbCommand,"?IsEmployee", qbSalesRep.IsEmployee);
      
        Database.PutParameter(dbCommand,"?RefListId", qbSalesRep.RefListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbSalesRep.FullName);
      
        Database.PutParameter(dbCommand,"?EmployeeId", qbSalesRep.EmployeeId);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", qbSalesRep.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepTypeId", qbSalesRep.QbSalesRepTypeId);
      
        Database.PutParameter(dbCommand,"?FirstName", qbSalesRep.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", qbSalesRep.LastName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbSalesRep.ListId);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbSalesRep.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbSalesRep.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbSalesRep.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?Initial",qbSalesRep.Initial);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbSalesRep.IsActive);
      
        Database.UpdateParameter(dbCommand,"?IsEmployee",qbSalesRep.IsEmployee);
      
        Database.UpdateParameter(dbCommand,"?RefListId",qbSalesRep.RefListId);
      
        Database.UpdateParameter(dbCommand,"?FullName",qbSalesRep.FullName);
      
        Database.UpdateParameter(dbCommand,"?EmployeeId",qbSalesRep.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerTypeListId",qbSalesRep.QbCustomerTypeListId);
      
        Database.UpdateParameter(dbCommand,"?QbSalesRepTypeId",qbSalesRep.QbSalesRepTypeId);
      
        Database.UpdateParameter(dbCommand,"?FirstName",qbSalesRep.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",qbSalesRep.LastName);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbSalesRep>  qbSalesRepList)
      {
        Insert(qbSalesRepList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbSalesRep Set "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence, "
      
        + " Initial = ?Initial, "
      
        + " IsActive = ?IsActive, "
      
        + " IsEmployee = ?IsEmployee, "
      
        + " RefListId = ?RefListId, "
      
        + " FullName = ?FullName, "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " QbCustomerTypeListId = ?QbCustomerTypeListId, "
      
        + " QbSalesRepTypeId = ?QbSalesRepTypeId, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbSalesRep qbSalesRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbSalesRep.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbSalesRep.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbSalesRep.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbSalesRep.EditSequence);
      
        Database.PutParameter(dbCommand,"?Initial", qbSalesRep.Initial);
      
        Database.PutParameter(dbCommand,"?IsActive", qbSalesRep.IsActive);
      
        Database.PutParameter(dbCommand,"?IsEmployee", qbSalesRep.IsEmployee);
      
        Database.PutParameter(dbCommand,"?RefListId", qbSalesRep.RefListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbSalesRep.FullName);
      
        Database.PutParameter(dbCommand,"?EmployeeId", qbSalesRep.EmployeeId);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", qbSalesRep.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepTypeId", qbSalesRep.QbSalesRepTypeId);
      
        Database.PutParameter(dbCommand,"?FirstName", qbSalesRep.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", qbSalesRep.LastName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbSalesRep qbSalesRep)
      {
        Update(qbSalesRep, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Initial, "
      
        + " IsActive, "
      
        + " IsEmployee, "
      
        + " RefListId, "
      
        + " FullName, "
      
        + " EmployeeId, "
      
        + " QbCustomerTypeListId, "
      
        + " QbSalesRepTypeId, "
      
        + " FirstName, "
      
        + " LastName "
      

      + " From QbSalesRep "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbSalesRep FindByPrimaryKey(
      String listId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", listId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbSalesRep not found, search by primary key");

      }

      public static QbSalesRep FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbSalesRep qbSalesRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbSalesRep.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbSalesRep qbSalesRep)
      {
      return Exists(qbSalesRep, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbSalesRep limit 1";

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

      public static QbSalesRep Load(IDataReader dataReader, int offset)
      {
      QbSalesRep qbSalesRep = new QbSalesRep();

      qbSalesRep.ListId = dataReader.GetString(0 + offset);
          qbSalesRep.TimeCreated = dataReader.GetDateTime(1 + offset);
          qbSalesRep.TimeModified = dataReader.GetDateTime(2 + offset);
          qbSalesRep.EditSequence = dataReader.GetString(3 + offset);
          qbSalesRep.Initial = dataReader.GetString(4 + offset);
          qbSalesRep.IsActive = dataReader.GetBoolean(5 + offset);
          qbSalesRep.IsEmployee = dataReader.GetBoolean(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbSalesRep.RefListId = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbSalesRep.FullName = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbSalesRep.EmployeeId = dataReader.GetInt32(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbSalesRep.QbCustomerTypeListId = dataReader.GetString(10 + offset);
          qbSalesRep.QbSalesRepTypeId = dataReader.GetInt32(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbSalesRep.FirstName = dataReader.GetString(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            qbSalesRep.LastName = dataReader.GetString(13 + offset);
          

      return qbSalesRep;
      }

      public static QbSalesRep Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbSalesRep "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbSalesRep qbSalesRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbSalesRep.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbSalesRep qbSalesRep)
      {
        Delete(qbSalesRep, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbSalesRep ";

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

      
        + " ListId, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Initial, "
      
        + " IsActive, "
      
        + " IsEmployee, "
      
        + " RefListId, "
      
        + " FullName, "
      
        + " EmployeeId, "
      
        + " QbCustomerTypeListId, "
      
        + " QbSalesRepTypeId, "
      
        + " FirstName, "
      
        + " LastName "
      

      + " From QbSalesRep ";
      public static List<QbSalesRep> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbSalesRep> rv = new List<QbSalesRep>();

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

      public static List<QbSalesRep> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbSalesRep> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbSalesRep obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence && Initial == obj.Initial && IsActive == obj.IsActive && IsEmployee == obj.IsEmployee && RefListId == obj.RefListId && FullName == obj.FullName && EmployeeId == obj.EmployeeId && QbCustomerTypeListId == obj.QbCustomerTypeListId && QbSalesRepTypeId == obj.QbSalesRepTypeId && FirstName == obj.FirstName && LastName == obj.LastName;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbSalesRep> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSalesRep));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbSalesRep item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbSalesRep>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSalesRep));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbSalesRep> itemsList
      = new List<QbSalesRep>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbSalesRep)
      itemsList.Add(deserializedObject as QbSalesRep);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected DateTime m_timeCreated;
      
        protected DateTime m_timeModified;
      
        protected String m_editSequence;
      
        protected String m_initial;
      
        protected bool m_isActive;
      
        protected bool m_isEmployee;
      
        protected String m_refListId;
      
        protected String m_fullName;
      
        protected int? m_employeeId;
      
        protected String m_qbCustomerTypeListId;
      
        protected int m_qbSalesRepTypeId;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
      #endregion

      #region Constructors
      public QbSalesRep(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbSalesRep(
        String 
          listId,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence,String 
          initial,bool 
          isActive,bool 
          isEmployee,String 
          refListId,String 
          fullName,int? 
          employeeId,String 
          qbCustomerTypeListId,int 
          qbSalesRepTypeId,String 
          firstName,String 
          lastName
        ) : this()
        {
        
          m_listId = listId;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
          m_initial = initial;
        
          m_isActive = isActive;
        
          m_isEmployee = isEmployee;
        
          m_refListId = refListId;
        
          m_fullName = fullName;
        
          m_employeeId = employeeId;
        
          m_qbCustomerTypeListId = qbCustomerTypeListId;
        
          m_qbSalesRepTypeId = qbSalesRepTypeId;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ListId
        {
        get { return m_listId;}
        set { m_listId = value; }
        }
      
        [XmlElement]
        public DateTime TimeCreated
        {
        get { return m_timeCreated;}
        set { m_timeCreated = value; }
        }
      
        [XmlElement]
        public DateTime TimeModified
        {
        get { return m_timeModified;}
        set { m_timeModified = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      
        [XmlElement]
        public String Initial
        {
        get { return m_initial;}
        set { m_initial = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public bool IsEmployee
        {
        get { return m_isEmployee;}
        set { m_isEmployee = value; }
        }
      
        [XmlElement]
        public String RefListId
        {
        get { return m_refListId;}
        set { m_refListId = value; }
        }
      
        [XmlElement]
        public String FullName
        {
        get { return m_fullName;}
        set { m_fullName = value; }
        }
      
        [XmlElement]
        public int? EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public String QbCustomerTypeListId
        {
        get { return m_qbCustomerTypeListId;}
        set { m_qbCustomerTypeListId = value; }
        }
      
        [XmlElement]
        public int QbSalesRepTypeId
        {
        get { return m_qbSalesRepTypeId;}
        set { m_qbSalesRepTypeId = value; }
        }
      
        [XmlElement]
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        [XmlElement]
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      

      public static int FieldsCount
      {
      get { return 14; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    