
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class EquipmentType : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [EquipmentType] ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Type, " +
      
        " @Description " +
      
      ")";

      public static void Insert(EquipmentType equipmentType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", equipmentType.ID);
      
        Database.PutParameter(dbCommand,"@Type", equipmentType.Type);
      
        Database.PutParameter(dbCommand,"@Description", equipmentType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(EquipmentType equipmentType)
      {
        Insert(equipmentType, null);
      }

      public static void Insert(List<EquipmentType>  equipmentTypeList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(EquipmentType equipmentType in  equipmentTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", equipmentType.ID);
      
        Database.PutParameter(dbCommand,"@Type", equipmentType.Type);
      
        Database.PutParameter(dbCommand,"@Description", equipmentType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",equipmentType.ID);
      
        Database.UpdateParameter(dbCommand,"@Type",equipmentType.Type);
      
        Database.UpdateParameter(dbCommand,"@Description",equipmentType.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<EquipmentType>  equipmentTypeList)
      {
      Insert(equipmentTypeList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [EquipmentType] Set "
      
        + " Type = @Type, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(EquipmentType equipmentType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", equipmentType.ID);
      
        Database.PutParameter(dbCommand,"@Type", equipmentType.Type);
      
        Database.PutParameter(dbCommand,"@Description", equipmentType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(EquipmentType equipmentType)
      {
      Update(equipmentType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From [EquipmentType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static EquipmentType FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("EquipmentType not found, search by primary key");

      }

      public static EquipmentType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(EquipmentType equipmentType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",equipmentType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(EquipmentType equipmentType)
      {
      return Exists(equipmentType, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from EquipmentType";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
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

      public static EquipmentType Load(IDataReader dataReader)
      {
      EquipmentType equipmentType = new EquipmentType();

      equipmentType.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            equipmentType.Type = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            equipmentType.Description = dataReader.GetString(2);
          

      return equipmentType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [EquipmentType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(EquipmentType equipmentType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", equipmentType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(EquipmentType equipmentType)
      {
      Delete(equipmentType, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [EquipmentType] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
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

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From [EquipmentType] ";
      public static List<EquipmentType> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<EquipmentType> rv = new List<EquipmentType>();

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

      public static List<EquipmentType> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<EquipmentType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<EquipmentType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(EquipmentType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<EquipmentType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<EquipmentType> itemsList
      = new List<EquipmentType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is EquipmentType)
      itemsList.Add(deserializedObject as EquipmentType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public EquipmentType(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public EquipmentType(
        int 
          iD,String 
          type,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_type = type;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
      }
      #endregion
      }

    