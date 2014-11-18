
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

      public partial class Owner : ICloneable
      {

      #region Store


      #region Save

      public static Owner Save(Owner owner, IDbConnection connection)
      {
      	if (!Exists(owner, connection))
      		Insert(owner, connection);
      	else
      		Update(owner, connection);
      	return owner;
      }

      public static Owner Save(Owner owner)
      {
      	if (!Exists(owner))
      		Insert(owner);
      	else
      		Update(owner);
      	return owner;
      }

      #endregion


      #region Insert

      private const String SqlInsert = "Insert Into Owner ( " +
      
        " UserId, " +
      
        " ShowAs, " +
      
        " IsActive " +
      
      ") Values (" +
      
        " ?UserId, " +
      
        " ?ShowAs, " +
      
        " ?IsActive " +
      
      ")";

      public static void Insert(Owner owner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?UserId", owner.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", owner.ShowAs);
      
        Database.PutParameter(dbCommand,"?IsActive", owner.IsActive);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        owner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Owner owner)
      {
        Insert(owner, null);
      }


      public static void Insert(List<Owner>  ownerList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Owner owner in  ownerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?UserId", owner.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", owner.ShowAs);
      
        Database.PutParameter(dbCommand,"?IsActive", owner.IsActive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?UserId",owner.UserId);
      
        Database.UpdateParameter(dbCommand,"?ShowAs",owner.ShowAs);
      
        Database.UpdateParameter(dbCommand,"?IsActive",owner.IsActive);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        owner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Owner>  ownerList)
      {
        Insert(ownerList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Owner Set "
      
        + " UserId = ?UserId, "
      
        + " ShowAs = ?ShowAs, "
      
        + " IsActive = ?IsActive "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Owner owner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", owner.Id);
      
        Database.PutParameter(dbCommand,"?UserId", owner.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", owner.ShowAs);
      
        Database.PutParameter(dbCommand,"?IsActive", owner.IsActive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Owner owner)
      {
        Update(owner, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " UserId, "
      
        + " ShowAs, "
      
        + " IsActive "
      

      + " From Owner "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Owner FindByPrimaryKey(
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
      throw new DataNotFoundException("Owner not found, search by primary key");

      }

      public static Owner FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Owner owner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",owner.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Owner owner)
      {
      return Exists(owner, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Owner limit 1";

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

      public static Owner Load(IDataReader dataReader, int offset)
      {
      Owner owner = new Owner();

      owner.Id = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            owner.UserId = dataReader.GetInt32(1 + offset);
          owner.ShowAs = dataReader.GetString(2 + offset);
          owner.IsActive = dataReader.GetBoolean(3 + offset);
          

      return owner;
      }

      public static Owner Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Owner "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Owner owner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", owner.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Owner owner)
      {
        Delete(owner, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Owner ";

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
      
        + " IsActive "
      

      + " From Owner ";
      public static List<Owner> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Owner> rv = new List<Owner>();

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

      public static List<Owner> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Owner> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Owner> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Owner));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Owner item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Owner>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Owner));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Owner> itemsList
      = new List<Owner>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Owner)
      itemsList.Add(deserializedObject as Owner);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int? m_userId;
      
        protected String m_showAs;
      
        protected bool m_isActive;
      
      #endregion

      #region Constructors
      public Owner(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Owner(
        int 
          id,int? 
          userId,String 
          showAs,bool 
          isActive
        ) : this()
        {
        
          m_id = id;
        
          m_userId = userId;
        
          m_showAs = showAs;
        
          m_isActive = isActive;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int? UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        public String ShowAs
        {
        get { return m_showAs;}
        set { m_showAs = value; }
        }
      
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    