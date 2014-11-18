
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

      public partial class QbItem : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbItem ( " +
      
        " ListId, " +
      
        " IsActive, " +
      
        " AssetAccountName, " +
      
        " AvgCost, " +
      
        " Description, " +
      
        " ParentItemName, " +
      
        " Name, " +
      
        " ManPartNum, " +
      
        " PrefVendorName, " +
      
        " PurchaseCost, " +
      
        " PurchaseDesc, " +
      
        " QtyOnHand, " +
      
        " QtyOnHandSpecified, " +
      
        " QtyOnPurchaseOrder, " +
      
        " QtyOnPurchaseOrderSpecified, " +
      
        " QtyOnSales, " +
      
        " QtyOnSalesSpecified, " +
      
        " ReorderPoint, " +
      
        " ReorderPointSpecified, " +
      
        " Taxable, " +
      
        " TaxableSpecified, " +
      
        " Type, " +
      
        " TypeSpecified, " +
      
        " UnitPrice, " +
      
        " UOMAbbr, " +
      
        " UOMName " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?IsActive, " +
      
        " ?AssetAccountName, " +
      
        " ?AvgCost, " +
      
        " ?Description, " +
      
        " ?ParentItemName, " +
      
        " ?Name, " +
      
        " ?ManPartNum, " +
      
        " ?PrefVendorName, " +
      
        " ?PurchaseCost, " +
      
        " ?PurchaseDesc, " +
      
        " ?QtyOnHand, " +
      
        " ?QtyOnHandSpecified, " +
      
        " ?QtyOnPurchaseOrder, " +
      
        " ?QtyOnPurchaseOrderSpecified, " +
      
        " ?QtyOnSales, " +
      
        " ?QtyOnSalesSpecified, " +
      
        " ?ReorderPoint, " +
      
        " ?ReorderPointSpecified, " +
      
        " ?Taxable, " +
      
        " ?TaxableSpecified, " +
      
        " ?Type, " +
      
        " ?TypeSpecified, " +
      
        " ?UnitPrice, " +
      
        " ?UOMAbbr, " +
      
        " ?UOMName " +
      
      ")";

      public static void Insert(QbItem qbItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      
        Database.PutParameter(dbCommand,"?IsActive", qbItem.IsActive);
      
        Database.PutParameter(dbCommand,"?AssetAccountName", qbItem.AssetAccountName);
      
        Database.PutParameter(dbCommand,"?AvgCost", qbItem.AvgCost);
      
        Database.PutParameter(dbCommand,"?Description", qbItem.Description);
      
        Database.PutParameter(dbCommand,"?ParentItemName", qbItem.ParentItemName);
      
        Database.PutParameter(dbCommand,"?Name", qbItem.Name);
      
        Database.PutParameter(dbCommand,"?ManPartNum", qbItem.ManPartNum);
      
        Database.PutParameter(dbCommand,"?PrefVendorName", qbItem.PrefVendorName);
      
        Database.PutParameter(dbCommand,"?PurchaseCost", qbItem.PurchaseCost);
      
        Database.PutParameter(dbCommand,"?PurchaseDesc", qbItem.PurchaseDesc);
      
        Database.PutParameter(dbCommand,"?QtyOnHand", qbItem.QtyOnHand);
      
        Database.PutParameter(dbCommand,"?QtyOnHandSpecified", qbItem.QtyOnHandSpecified);
      
        Database.PutParameter(dbCommand,"?QtyOnPurchaseOrder", qbItem.QtyOnPurchaseOrder);
      
        Database.PutParameter(dbCommand,"?QtyOnPurchaseOrderSpecified", qbItem.QtyOnPurchaseOrderSpecified);
      
        Database.PutParameter(dbCommand,"?QtyOnSales", qbItem.QtyOnSales);
      
        Database.PutParameter(dbCommand,"?QtyOnSalesSpecified", qbItem.QtyOnSalesSpecified);
      
        Database.PutParameter(dbCommand,"?ReorderPoint", qbItem.ReorderPoint);
      
        Database.PutParameter(dbCommand,"?ReorderPointSpecified", qbItem.ReorderPointSpecified);
      
        Database.PutParameter(dbCommand,"?Taxable", qbItem.Taxable);
      
        Database.PutParameter(dbCommand,"?TaxableSpecified", qbItem.TaxableSpecified);
      
        Database.PutParameter(dbCommand,"?Type", qbItem.Type);
      
        Database.PutParameter(dbCommand,"?TypeSpecified", qbItem.TypeSpecified);
      
        Database.PutParameter(dbCommand,"?UnitPrice", qbItem.UnitPrice);
      
        Database.PutParameter(dbCommand,"?UOMAbbr", qbItem.UOMAbbr);
      
        Database.PutParameter(dbCommand,"?UOMName", qbItem.UOMName);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbItem qbItem)
      {
        Insert(qbItem, null);
      }


      public static void Insert(List<QbItem>  qbItemList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbItem qbItem in  qbItemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      
        Database.PutParameter(dbCommand,"?IsActive", qbItem.IsActive);
      
        Database.PutParameter(dbCommand,"?AssetAccountName", qbItem.AssetAccountName);
      
        Database.PutParameter(dbCommand,"?AvgCost", qbItem.AvgCost);
      
        Database.PutParameter(dbCommand,"?Description", qbItem.Description);
      
        Database.PutParameter(dbCommand,"?ParentItemName", qbItem.ParentItemName);
      
        Database.PutParameter(dbCommand,"?Name", qbItem.Name);
      
        Database.PutParameter(dbCommand,"?ManPartNum", qbItem.ManPartNum);
      
        Database.PutParameter(dbCommand,"?PrefVendorName", qbItem.PrefVendorName);
      
        Database.PutParameter(dbCommand,"?PurchaseCost", qbItem.PurchaseCost);
      
        Database.PutParameter(dbCommand,"?PurchaseDesc", qbItem.PurchaseDesc);
      
        Database.PutParameter(dbCommand,"?QtyOnHand", qbItem.QtyOnHand);
      
        Database.PutParameter(dbCommand,"?QtyOnHandSpecified", qbItem.QtyOnHandSpecified);
      
        Database.PutParameter(dbCommand,"?QtyOnPurchaseOrder", qbItem.QtyOnPurchaseOrder);
      
        Database.PutParameter(dbCommand,"?QtyOnPurchaseOrderSpecified", qbItem.QtyOnPurchaseOrderSpecified);
      
        Database.PutParameter(dbCommand,"?QtyOnSales", qbItem.QtyOnSales);
      
        Database.PutParameter(dbCommand,"?QtyOnSalesSpecified", qbItem.QtyOnSalesSpecified);
      
        Database.PutParameter(dbCommand,"?ReorderPoint", qbItem.ReorderPoint);
      
        Database.PutParameter(dbCommand,"?ReorderPointSpecified", qbItem.ReorderPointSpecified);
      
        Database.PutParameter(dbCommand,"?Taxable", qbItem.Taxable);
      
        Database.PutParameter(dbCommand,"?TaxableSpecified", qbItem.TaxableSpecified);
      
        Database.PutParameter(dbCommand,"?Type", qbItem.Type);
      
        Database.PutParameter(dbCommand,"?TypeSpecified", qbItem.TypeSpecified);
      
        Database.PutParameter(dbCommand,"?UnitPrice", qbItem.UnitPrice);
      
        Database.PutParameter(dbCommand,"?UOMAbbr", qbItem.UOMAbbr);
      
        Database.PutParameter(dbCommand,"?UOMName", qbItem.UOMName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbItem.ListId);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbItem.IsActive);
      
        Database.UpdateParameter(dbCommand,"?AssetAccountName",qbItem.AssetAccountName);
      
        Database.UpdateParameter(dbCommand,"?AvgCost",qbItem.AvgCost);
      
        Database.UpdateParameter(dbCommand,"?Description",qbItem.Description);
      
        Database.UpdateParameter(dbCommand,"?ParentItemName",qbItem.ParentItemName);
      
        Database.UpdateParameter(dbCommand,"?Name",qbItem.Name);
      
        Database.UpdateParameter(dbCommand,"?ManPartNum",qbItem.ManPartNum);
      
        Database.UpdateParameter(dbCommand,"?PrefVendorName",qbItem.PrefVendorName);
      
        Database.UpdateParameter(dbCommand,"?PurchaseCost",qbItem.PurchaseCost);
      
        Database.UpdateParameter(dbCommand,"?PurchaseDesc",qbItem.PurchaseDesc);
      
        Database.UpdateParameter(dbCommand,"?QtyOnHand",qbItem.QtyOnHand);
      
        Database.UpdateParameter(dbCommand,"?QtyOnHandSpecified",qbItem.QtyOnHandSpecified);
      
        Database.UpdateParameter(dbCommand,"?QtyOnPurchaseOrder",qbItem.QtyOnPurchaseOrder);
      
        Database.UpdateParameter(dbCommand,"?QtyOnPurchaseOrderSpecified",qbItem.QtyOnPurchaseOrderSpecified);
      
        Database.UpdateParameter(dbCommand,"?QtyOnSales",qbItem.QtyOnSales);
      
        Database.UpdateParameter(dbCommand,"?QtyOnSalesSpecified",qbItem.QtyOnSalesSpecified);
      
        Database.UpdateParameter(dbCommand,"?ReorderPoint",qbItem.ReorderPoint);
      
        Database.UpdateParameter(dbCommand,"?ReorderPointSpecified",qbItem.ReorderPointSpecified);
      
        Database.UpdateParameter(dbCommand,"?Taxable",qbItem.Taxable);
      
        Database.UpdateParameter(dbCommand,"?TaxableSpecified",qbItem.TaxableSpecified);
      
        Database.UpdateParameter(dbCommand,"?Type",qbItem.Type);
      
        Database.UpdateParameter(dbCommand,"?TypeSpecified",qbItem.TypeSpecified);
      
        Database.UpdateParameter(dbCommand,"?UnitPrice",qbItem.UnitPrice);
      
        Database.UpdateParameter(dbCommand,"?UOMAbbr",qbItem.UOMAbbr);
      
        Database.UpdateParameter(dbCommand,"?UOMName",qbItem.UOMName);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbItem>  qbItemList)
      {
        Insert(qbItemList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbItem Set "
      
        + " IsActive = ?IsActive, "
      
        + " AssetAccountName = ?AssetAccountName, "
      
        + " AvgCost = ?AvgCost, "
      
        + " Description = ?Description, "
      
        + " ParentItemName = ?ParentItemName, "
      
        + " Name = ?Name, "
      
        + " ManPartNum = ?ManPartNum, "
      
        + " PrefVendorName = ?PrefVendorName, "
      
        + " PurchaseCost = ?PurchaseCost, "
      
        + " PurchaseDesc = ?PurchaseDesc, "
      
        + " QtyOnHand = ?QtyOnHand, "
      
        + " QtyOnHandSpecified = ?QtyOnHandSpecified, "
      
        + " QtyOnPurchaseOrder = ?QtyOnPurchaseOrder, "
      
        + " QtyOnPurchaseOrderSpecified = ?QtyOnPurchaseOrderSpecified, "
      
        + " QtyOnSales = ?QtyOnSales, "
      
        + " QtyOnSalesSpecified = ?QtyOnSalesSpecified, "
      
        + " ReorderPoint = ?ReorderPoint, "
      
        + " ReorderPointSpecified = ?ReorderPointSpecified, "
      
        + " Taxable = ?Taxable, "
      
        + " TaxableSpecified = ?TaxableSpecified, "
      
        + " Type = ?Type, "
      
        + " TypeSpecified = ?TypeSpecified, "
      
        + " UnitPrice = ?UnitPrice, "
      
        + " UOMAbbr = ?UOMAbbr, "
      
        + " UOMName = ?UOMName "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbItem qbItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      
        Database.PutParameter(dbCommand,"?IsActive", qbItem.IsActive);
      
        Database.PutParameter(dbCommand,"?AssetAccountName", qbItem.AssetAccountName);
      
        Database.PutParameter(dbCommand,"?AvgCost", qbItem.AvgCost);
      
        Database.PutParameter(dbCommand,"?Description", qbItem.Description);
      
        Database.PutParameter(dbCommand,"?ParentItemName", qbItem.ParentItemName);
      
        Database.PutParameter(dbCommand,"?Name", qbItem.Name);
      
        Database.PutParameter(dbCommand,"?ManPartNum", qbItem.ManPartNum);
      
        Database.PutParameter(dbCommand,"?PrefVendorName", qbItem.PrefVendorName);
      
        Database.PutParameter(dbCommand,"?PurchaseCost", qbItem.PurchaseCost);
      
        Database.PutParameter(dbCommand,"?PurchaseDesc", qbItem.PurchaseDesc);
      
        Database.PutParameter(dbCommand,"?QtyOnHand", qbItem.QtyOnHand);
      
        Database.PutParameter(dbCommand,"?QtyOnHandSpecified", qbItem.QtyOnHandSpecified);
      
        Database.PutParameter(dbCommand,"?QtyOnPurchaseOrder", qbItem.QtyOnPurchaseOrder);
      
        Database.PutParameter(dbCommand,"?QtyOnPurchaseOrderSpecified", qbItem.QtyOnPurchaseOrderSpecified);
      
        Database.PutParameter(dbCommand,"?QtyOnSales", qbItem.QtyOnSales);
      
        Database.PutParameter(dbCommand,"?QtyOnSalesSpecified", qbItem.QtyOnSalesSpecified);
      
        Database.PutParameter(dbCommand,"?ReorderPoint", qbItem.ReorderPoint);
      
        Database.PutParameter(dbCommand,"?ReorderPointSpecified", qbItem.ReorderPointSpecified);
      
        Database.PutParameter(dbCommand,"?Taxable", qbItem.Taxable);
      
        Database.PutParameter(dbCommand,"?TaxableSpecified", qbItem.TaxableSpecified);
      
        Database.PutParameter(dbCommand,"?Type", qbItem.Type);
      
        Database.PutParameter(dbCommand,"?TypeSpecified", qbItem.TypeSpecified);
      
        Database.PutParameter(dbCommand,"?UnitPrice", qbItem.UnitPrice);
      
        Database.PutParameter(dbCommand,"?UOMAbbr", qbItem.UOMAbbr);
      
        Database.PutParameter(dbCommand,"?UOMName", qbItem.UOMName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbItem qbItem)
      {
        Update(qbItem, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " IsActive, "
      
        + " AssetAccountName, "
      
        + " AvgCost, "
      
        + " Description, "
      
        + " ParentItemName, "
      
        + " Name, "
      
        + " ManPartNum, "
      
        + " PrefVendorName, "
      
        + " PurchaseCost, "
      
        + " PurchaseDesc, "
      
        + " QtyOnHand, "
      
        + " QtyOnHandSpecified, "
      
        + " QtyOnPurchaseOrder, "
      
        + " QtyOnPurchaseOrderSpecified, "
      
        + " QtyOnSales, "
      
        + " QtyOnSalesSpecified, "
      
        + " ReorderPoint, "
      
        + " ReorderPointSpecified, "
      
        + " Taxable, "
      
        + " TaxableSpecified, "
      
        + " Type, "
      
        + " TypeSpecified, "
      
        + " UnitPrice, "
      
        + " UOMAbbr, "
      
        + " UOMName "
      

      + " From QbItem "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbItem FindByPrimaryKey(
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
      throw new DataNotFoundException("QbItem not found, search by primary key");

      }

      public static QbItem FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbItem qbItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbItem.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbItem qbItem)
      {
      return Exists(qbItem, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbItem limit 1";

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

      public static QbItem Load(IDataReader dataReader, int offset)
      {
      QbItem qbItem = new QbItem();

      qbItem.ListId = dataReader.GetString(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            qbItem.IsActive = dataReader.GetBoolean(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            qbItem.AssetAccountName = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbItem.AvgCost = dataReader.GetDecimal(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            qbItem.Description = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            qbItem.ParentItemName = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbItem.Name = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbItem.ManPartNum = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbItem.PrefVendorName = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbItem.PurchaseCost = dataReader.GetDecimal(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbItem.PurchaseDesc = dataReader.GetString(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            qbItem.QtyOnHand = dataReader.GetDecimal(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbItem.QtyOnHandSpecified = dataReader.GetBoolean(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            qbItem.QtyOnPurchaseOrder = dataReader.GetDecimal(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            qbItem.QtyOnPurchaseOrderSpecified = dataReader.GetBoolean(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            qbItem.QtyOnSales = dataReader.GetDecimal(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            qbItem.QtyOnSalesSpecified = dataReader.GetBoolean(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            qbItem.ReorderPoint = dataReader.GetDecimal(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            qbItem.ReorderPointSpecified = dataReader.GetBoolean(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            qbItem.Taxable = dataReader.GetBoolean(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            qbItem.TaxableSpecified = dataReader.GetBoolean(20 + offset);
          
            if(!dataReader.IsDBNull(21 + offset))
            qbItem.Type = dataReader.GetString(21 + offset);
          
            if(!dataReader.IsDBNull(22 + offset))
            qbItem.TypeSpecified = dataReader.GetBoolean(22 + offset);
          
            if(!dataReader.IsDBNull(23 + offset))
            qbItem.UnitPrice = dataReader.GetDecimal(23 + offset);
          
            if(!dataReader.IsDBNull(24 + offset))
            qbItem.UOMAbbr = dataReader.GetString(24 + offset);
          
            if(!dataReader.IsDBNull(25 + offset))
            qbItem.UOMName = dataReader.GetString(25 + offset);
          

      return qbItem;
      }

      public static QbItem Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbItem "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbItem qbItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbItem qbItem)
      {
        Delete(qbItem, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbItem ";

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
      
        + " IsActive, "
      
        + " AssetAccountName, "
      
        + " AvgCost, "
      
        + " Description, "
      
        + " ParentItemName, "
      
        + " Name, "
      
        + " ManPartNum, "
      
        + " PrefVendorName, "
      
        + " PurchaseCost, "
      
        + " PurchaseDesc, "
      
        + " QtyOnHand, "
      
        + " QtyOnHandSpecified, "
      
        + " QtyOnPurchaseOrder, "
      
        + " QtyOnPurchaseOrderSpecified, "
      
        + " QtyOnSales, "
      
        + " QtyOnSalesSpecified, "
      
        + " ReorderPoint, "
      
        + " ReorderPointSpecified, "
      
        + " Taxable, "
      
        + " TaxableSpecified, "
      
        + " Type, "
      
        + " TypeSpecified, "
      
        + " UnitPrice, "
      
        + " UOMAbbr, "
      
        + " UOMName "
      

      + " From QbItem ";
      public static List<QbItem> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbItem> rv = new List<QbItem>();

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

      public static List<QbItem> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbItem> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<QbItem> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbItem));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbItem item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbItem>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbItem));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbItem> itemsList
      = new List<QbItem>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbItem)
      itemsList.Add(deserializedObject as QbItem);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected bool m_isActive;
      
        protected String m_assetAccountName;
      
        protected decimal m_avgCost;
      
        protected String m_description;
      
        protected String m_parentItemName;
      
        protected String m_name;
      
        protected String m_manPartNum;
      
        protected String m_prefVendorName;
      
        protected decimal m_purchaseCost;
      
        protected String m_purchaseDesc;
      
        protected decimal m_qtyOnHand;
      
        protected bool m_qtyOnHandSpecified;
      
        protected decimal m_qtyOnPurchaseOrder;
      
        protected bool m_qtyOnPurchaseOrderSpecified;
      
        protected decimal m_qtyOnSales;
      
        protected bool m_qtyOnSalesSpecified;
      
        protected decimal m_reorderPoint;
      
        protected bool m_reorderPointSpecified;
      
        protected bool m_taxable;
      
        protected bool m_taxableSpecified;
      
        protected String m_type;
      
        protected bool m_typeSpecified;
      
        protected decimal m_unitPrice;
      
        protected String m_uOMAbbr;
      
        protected String m_uOMName;
      
      #endregion

      #region Constructors
      public QbItem(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbItem(
        String 
          listId,bool 
          isActive,String 
          assetAccountName,decimal 
          avgCost,String 
          description,String 
          parentItemName,String 
          name,String 
          manPartNum,String 
          prefVendorName,decimal 
          purchaseCost,String 
          purchaseDesc,decimal 
          qtyOnHand,bool 
          qtyOnHandSpecified,decimal 
          qtyOnPurchaseOrder,bool 
          qtyOnPurchaseOrderSpecified,decimal 
          qtyOnSales,bool 
          qtyOnSalesSpecified,decimal 
          reorderPoint,bool 
          reorderPointSpecified,bool 
          taxable,bool 
          taxableSpecified,String 
          type,bool 
          typeSpecified,decimal 
          unitPrice,String 
          uOMAbbr,String 
          uOMName
        ) : this()
        {
        
          m_listId = listId;
        
          m_isActive = isActive;
        
          m_assetAccountName = assetAccountName;
        
          m_avgCost = avgCost;
        
          m_description = description;
        
          m_parentItemName = parentItemName;
        
          m_name = name;
        
          m_manPartNum = manPartNum;
        
          m_prefVendorName = prefVendorName;
        
          m_purchaseCost = purchaseCost;
        
          m_purchaseDesc = purchaseDesc;
        
          m_qtyOnHand = qtyOnHand;
        
          m_qtyOnHandSpecified = qtyOnHandSpecified;
        
          m_qtyOnPurchaseOrder = qtyOnPurchaseOrder;
        
          m_qtyOnPurchaseOrderSpecified = qtyOnPurchaseOrderSpecified;
        
          m_qtyOnSales = qtyOnSales;
        
          m_qtyOnSalesSpecified = qtyOnSalesSpecified;
        
          m_reorderPoint = reorderPoint;
        
          m_reorderPointSpecified = reorderPointSpecified;
        
          m_taxable = taxable;
        
          m_taxableSpecified = taxableSpecified;
        
          m_type = type;
        
          m_typeSpecified = typeSpecified;
        
          m_unitPrice = unitPrice;
        
          m_uOMAbbr = uOMAbbr;
        
          m_uOMName = uOMName;
        
        }


      
      #endregion

      
        public String ListId
        {
        get { return m_listId;}
        set { m_listId = value; }
        }
      
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        public String AssetAccountName
        {
        get { return m_assetAccountName;}
        set { m_assetAccountName = value; }
        }
      
        public decimal AvgCost
        {
        get { return m_avgCost;}
        set { m_avgCost = value; }
        }
      
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        public String ParentItemName
        {
        get { return m_parentItemName;}
        set { m_parentItemName = value; }
        }
      
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        public String ManPartNum
        {
        get { return m_manPartNum;}
        set { m_manPartNum = value; }
        }
      
        public String PrefVendorName
        {
        get { return m_prefVendorName;}
        set { m_prefVendorName = value; }
        }
      
        public decimal PurchaseCost
        {
        get { return m_purchaseCost;}
        set { m_purchaseCost = value; }
        }
      
        public String PurchaseDesc
        {
        get { return m_purchaseDesc;}
        set { m_purchaseDesc = value; }
        }
      
        public decimal QtyOnHand
        {
        get { return m_qtyOnHand;}
        set { m_qtyOnHand = value; }
        }
      
        public bool QtyOnHandSpecified
        {
        get { return m_qtyOnHandSpecified;}
        set { m_qtyOnHandSpecified = value; }
        }
      
        public decimal QtyOnPurchaseOrder
        {
        get { return m_qtyOnPurchaseOrder;}
        set { m_qtyOnPurchaseOrder = value; }
        }
      
        public bool QtyOnPurchaseOrderSpecified
        {
        get { return m_qtyOnPurchaseOrderSpecified;}
        set { m_qtyOnPurchaseOrderSpecified = value; }
        }
      
        public decimal QtyOnSales
        {
        get { return m_qtyOnSales;}
        set { m_qtyOnSales = value; }
        }
      
        public bool QtyOnSalesSpecified
        {
        get { return m_qtyOnSalesSpecified;}
        set { m_qtyOnSalesSpecified = value; }
        }
      
        public decimal ReorderPoint
        {
        get { return m_reorderPoint;}
        set { m_reorderPoint = value; }
        }
      
        public bool ReorderPointSpecified
        {
        get { return m_reorderPointSpecified;}
        set { m_reorderPointSpecified = value; }
        }
      
        public bool Taxable
        {
        get { return m_taxable;}
        set { m_taxable = value; }
        }
      
        public bool TaxableSpecified
        {
        get { return m_taxableSpecified;}
        set { m_taxableSpecified = value; }
        }
      
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
        }
      
        public bool TypeSpecified
        {
        get { return m_typeSpecified;}
        set { m_typeSpecified = value; }
        }
      
        public decimal UnitPrice
        {
        get { return m_unitPrice;}
        set { m_unitPrice = value; }
        }
      
        public String UOMAbbr
        {
        get { return m_uOMAbbr;}
        set { m_uOMAbbr = value; }
        }
      
        public String UOMName
        {
        get { return m_uOMName;}
        set { m_uOMName = value; }
        }
      

      public static int FieldsCount
      {
      get { return 26; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    