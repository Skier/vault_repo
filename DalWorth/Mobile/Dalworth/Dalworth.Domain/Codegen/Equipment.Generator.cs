
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


      public partial class Equipment : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Equipment] ( " +
      
        " ID, " +
      
        " EquipmentTypeId, " +
      
        " SerialNumber " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @EquipmentTypeId, " +
      
        " @SerialNumber " +
      
      ")";

      public static void Insert(Equipment equipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", equipment.ID);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", equipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@SerialNumber", equipment.SerialNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Equipment equipment)
      {
        Insert(equipment, null);
      }

      public static void Insert(List<Equipment>  equipmentList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Equipment equipment in  equipmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", equipment.ID);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", equipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@SerialNumber", equipment.SerialNumber);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",equipment.ID);
      
        Database.UpdateParameter(dbCommand,"@EquipmentTypeId",equipment.EquipmentTypeId);
      
        Database.UpdateParameter(dbCommand,"@SerialNumber",equipment.SerialNumber);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Equipment>  equipmentList)
      {
      Insert(equipmentList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Equipment] Set "
      
        + " EquipmentTypeId = @EquipmentTypeId, "
      
        + " SerialNumber = @SerialNumber "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Equipment equipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", equipment.ID);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", equipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@SerialNumber", equipment.SerialNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Equipment equipment)
      {
      Update(equipment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EquipmentTypeId, "
      
        + " SerialNumber "
      

      + " From [Equipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Equipment FindByPrimaryKey(
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
      throw new DataNotFoundException("Equipment not found, search by primary key");

      }

      public static Equipment FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Equipment equipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",equipment.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Equipment equipment)
      {
      return Exists(equipment, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Equipment";

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

      public static Equipment Load(IDataReader dataReader)
      {
      Equipment equipment = new Equipment();

      equipment.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            equipment.EquipmentTypeId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            equipment.SerialNumber = dataReader.GetString(2);
          

      return equipment;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Equipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Equipment equipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", equipment.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Equipment equipment)
      {
      Delete(equipment, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Equipment] ";

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
      
        + " EquipmentTypeId, "
      
        + " SerialNumber "
      

      + " From [Equipment] ";
      public static List<Equipment> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<Equipment> rv = new List<Equipment>();

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

      public static List<Equipment> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Equipment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Equipment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Equipment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Equipment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Equipment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Equipment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Equipment> itemsList
      = new List<Equipment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Equipment)
      itemsList.Add(deserializedObject as Equipment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_equipmentTypeId;
      
        protected String m_serialNumber;
      
      #endregion

      #region Constructors
      public Equipment(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Equipment(
        int 
          iD,int? 
          equipmentTypeId,String 
          serialNumber
        )
        {
        
          m_iD = iD;
        
          m_equipmentTypeId = equipmentTypeId;
        
          m_serialNumber = serialNumber;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? EquipmentTypeId
        {
        get { return m_equipmentTypeId;}
        set { m_equipmentTypeId = value; }
        }
      
        [XmlElement]
        public String SerialNumber
        {
        get { return m_serialNumber;}
        set { m_serialNumber = value; }
        }
      
      }
      #endregion
      }

    