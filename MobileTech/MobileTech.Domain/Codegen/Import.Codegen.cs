
      namespace MobileTech.Domain
      {
        
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
        using System.IO;

        public class Import:Task
        {

        public Import(){}
        public Import(String path)
        {
        importFolder = path;
        }


        private String importFolder;

        public String ImportFolder
        {
        get { return importFolder; }
        set { importFolder = value; }
        }

        private int insertedRows;

        public int InsertedRows
        {
        get { return insertedRows; }
        set { insertedRows = value; }
        }

        private bool clear;
        public bool Clear
        {
        get {return clear;}
        set {clear = value;}

        }

        protected override void Main()
        {
        Database.Begin();
        try
        {

        String filePath = String.Empty;
        insertedRows = 0;

        #region Cleaning
        if(Clear)
        {
        
          #region Password
          AddMessage("Removing Password", 3);
          Password.Clear();
          #endregion
        
          #region Counter
          AddMessage("Removing Counter", 5);
          Counter.Clear();
          #endregion
        
          #region PeriodTransaction
          AddMessage("Removing PeriodTransaction", 8);
          PeriodTransaction.Clear();
          #endregion
        
          #region PeriodTransactionType
          AddMessage("Removing PeriodTransactionType", 10);
          PeriodTransactionType.Clear();
          #endregion
        
          #region RouteOption
          AddMessage("Removing RouteOption", 13);
          RouteOption.Clear();
          #endregion
        
          #region RouteOptionDescription
          AddMessage("Removing RouteOptionDescription", 15);
          RouteOptionDescription.Clear();
          #endregion
        
          #region Product
          AddMessage("Removing Product", 18);
          Product.Clear();
          #endregion
        
          #region Equipment
          AddMessage("Removing Equipment", 20);
          Equipment.Clear();
          #endregion
        
          #region InventoryTransactionDetail
          AddMessage("Removing InventoryTransactionDetail", 23);
          InventoryTransactionDetail.Clear();
          #endregion
        
          #region InventoryTransactionDetailXRef
          AddMessage("Removing InventoryTransactionDetailXRef", 25);
          InventoryTransactionDetailXRef.Clear();
          #endregion
        
          #region InventoryTransactionDetailType
          AddMessage("Removing InventoryTransactionDetailType", 28);
          InventoryTransactionDetailType.Clear();
          #endregion
        
          #region InventoryTransaction
          AddMessage("Removing InventoryTransaction", 30);
          InventoryTransaction.Clear();
          #endregion
        
          #region InventoryTransactionType
          AddMessage("Removing InventoryTransactionType", 33);
          InventoryTransactionType.Clear();
          #endregion
        
          #region RouteInventory
          AddMessage("Removing RouteInventory", 35);
          RouteInventory.Clear();
          #endregion
        
          #region StorageType
          AddMessage("Removing StorageType", 38);
          StorageType.Clear();
          #endregion
        
          #region EventLog
          AddMessage("Removing EventLog", 40);
          EventLog.Clear();
          #endregion
        
          #region CustomerTransactionDetail
          AddMessage("Removing CustomerTransactionDetail", 43);
          CustomerTransactionDetail.Clear();
          #endregion
        
          #region RouteScheduleQueue
          AddMessage("Removing RouteScheduleQueue", 45);
          RouteScheduleQueue.Clear();
          #endregion
        
          #region RouteScheduleQueueStatus
          AddMessage("Removing RouteScheduleQueueStatus", 48);
          RouteScheduleQueueStatus.Clear();
          #endregion
        
          #region RouteSchedule
          AddMessage("Removing RouteSchedule", 50);
          RouteSchedule.Clear();
          #endregion
        
          #region DayOfWeek
          AddMessage("Removing DayOfWeek", 53);
          DayOfWeek.Clear();
          #endregion
        
          #region CustomerOption
          AddMessage("Removing CustomerOption", 55);
          CustomerOption.Clear();
          #endregion
        
          #region CustomerOptionDescription
          AddMessage("Removing CustomerOptionDescription", 58);
          CustomerOptionDescription.Clear();
          #endregion
        
          #region CustomerTransaction
          AddMessage("Removing CustomerTransaction", 60);
          CustomerTransaction.Clear();
          #endregion
        
          #region BusinessTransaction
          AddMessage("Removing BusinessTransaction", 63);
          BusinessTransaction.Clear();
          #endregion
        
          #region CustomerVisit
          AddMessage("Removing CustomerVisit", 65);
          CustomerVisit.Clear();
          #endregion
        
          #region RouteCustomer
          AddMessage("Removing RouteCustomer", 68);
          RouteCustomer.Clear();
          #endregion
        
          #region Customer
          AddMessage("Removing Customer", 70);
          Customer.Clear();
          #endregion
        
          #region CustomerTransactionType
          AddMessage("Removing CustomerTransactionType", 73);
          CustomerTransactionType.Clear();
          #endregion
        
          #region Item
          AddMessage("Removing Item", 75);
          Item.Clear();
          #endregion
        
          #region ItemCategory
          AddMessage("Removing ItemCategory", 78);
          ItemCategory.Clear();
          #endregion
        
          #region ItemType
          AddMessage("Removing ItemType", 80);
          ItemType.Clear();
          #endregion
        
          #region Session
          AddMessage("Removing Session", 83);
          Session.Clear();
          #endregion
        
          #region Route
          AddMessage("Removing Route", 85);
          Route.Clear();
          #endregion
        
          #region RouteType
          AddMessage("Removing RouteType", 88);
          RouteType.Clear();
          #endregion
        
          #region RouteStatus
          AddMessage("Removing RouteStatus", 90);
          RouteStatus.Clear();
          #endregion
        
          #region Employee
          AddMessage("Removing Employee", 93);
          Employee.Clear();
          #endregion
        
          #region Location
          AddMessage("Removing Location", 95);
          Location.Clear();
          #endregion
        
          #region BusinessTransactionType
          AddMessage("Removing BusinessTransactionType", 98);
          BusinessTransactionType.Clear();
          #endregion
        
          #region BusinessTransactionStatus
          AddMessage("Removing BusinessTransactionStatus", 100);
          BusinessTransactionStatus.Clear();
          #endregion
         
        }
        #endregion

        
          #region BusinessTransactionStatus

          filePath = String.Format(@"{0}\BusinessTransactionStatus.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing BusinessTransactionStatus", 3);

          insertedRows += BusinessTransactionStatus.Import(filePath);
          }
        #endregion
            
          #region BusinessTransactionType

          filePath = String.Format(@"{0}\BusinessTransactionType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing BusinessTransactionType", 5);

          insertedRows += BusinessTransactionType.Import(filePath);
          }
        #endregion
            
          #region Location

          filePath = String.Format(@"{0}\Location.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Location", 8);

          insertedRows += Location.Import(filePath);
          }
        #endregion
            
          #region Employee

          filePath = String.Format(@"{0}\Employee.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Employee", 10);

          insertedRows += Employee.Import(filePath);
          }
        #endregion
            
          #region RouteStatus

          filePath = String.Format(@"{0}\RouteStatus.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteStatus", 13);

          insertedRows += RouteStatus.Import(filePath);
          }
        #endregion
            
          #region RouteType

          filePath = String.Format(@"{0}\RouteType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteType", 15);

          insertedRows += RouteType.Import(filePath);
          }
        #endregion
            
          #region Route

          filePath = String.Format(@"{0}\Route.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Route", 18);

          insertedRows += Route.Import(filePath);
          }
        #endregion
            
          #region Session

          filePath = String.Format(@"{0}\Session.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Session", 20);

          insertedRows += Session.Import(filePath);
          }
        #endregion
            
          #region ItemType

          filePath = String.Format(@"{0}\ItemType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ItemType", 23);

          insertedRows += ItemType.Import(filePath);
          }
        #endregion
            
          #region ItemCategory

          filePath = String.Format(@"{0}\ItemCategory.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ItemCategory", 25);

          insertedRows += ItemCategory.Import(filePath);
          }
        #endregion
            
          #region Item

          filePath = String.Format(@"{0}\Item.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Item", 28);

          insertedRows += Item.Import(filePath);
          }
        #endregion
            
          #region CustomerTransactionType

          filePath = String.Format(@"{0}\CustomerTransactionType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing CustomerTransactionType", 30);

          insertedRows += CustomerTransactionType.Import(filePath);
          }
        #endregion
            
          #region Customer

          filePath = String.Format(@"{0}\Customer.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Customer", 33);

          insertedRows += Customer.Import(filePath);
          }
        #endregion
            
          #region RouteCustomer

          filePath = String.Format(@"{0}\RouteCustomer.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteCustomer", 35);

          insertedRows += RouteCustomer.Import(filePath);
          }
        #endregion
            
          #region CustomerVisit

          filePath = String.Format(@"{0}\CustomerVisit.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing CustomerVisit", 38);

          insertedRows += CustomerVisit.Import(filePath);
          }
        #endregion
            
          #region BusinessTransaction

          filePath = String.Format(@"{0}\BusinessTransaction.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing BusinessTransaction", 40);

          insertedRows += BusinessTransaction.Import(filePath);
          }
        #endregion
            
          #region CustomerTransaction

          filePath = String.Format(@"{0}\CustomerTransaction.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing CustomerTransaction", 43);

          insertedRows += CustomerTransaction.Import(filePath);
          }
        #endregion
            
          #region CustomerOptionDescription

          filePath = String.Format(@"{0}\CustomerOptionDescription.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing CustomerOptionDescription", 45);

          insertedRows += CustomerOptionDescription.Import(filePath);
          }
        #endregion
            
          #region CustomerOption

          filePath = String.Format(@"{0}\CustomerOption.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing CustomerOption", 48);

          insertedRows += CustomerOption.Import(filePath);
          }
        #endregion
            
          #region DayOfWeek

          filePath = String.Format(@"{0}\DayOfWeek.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing DayOfWeek", 50);

          insertedRows += DayOfWeek.Import(filePath);
          }
        #endregion
            
          #region RouteSchedule

          filePath = String.Format(@"{0}\RouteSchedule.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteSchedule", 53);

          insertedRows += RouteSchedule.Import(filePath);
          }
        #endregion
            
          #region RouteScheduleQueueStatus

          filePath = String.Format(@"{0}\RouteScheduleQueueStatus.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteScheduleQueueStatus", 55);

          insertedRows += RouteScheduleQueueStatus.Import(filePath);
          }
        #endregion
            
          #region RouteScheduleQueue

          filePath = String.Format(@"{0}\RouteScheduleQueue.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteScheduleQueue", 58);

          insertedRows += RouteScheduleQueue.Import(filePath);
          }
        #endregion
            
          #region CustomerTransactionDetail

          filePath = String.Format(@"{0}\CustomerTransactionDetail.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing CustomerTransactionDetail", 60);

          insertedRows += CustomerTransactionDetail.Import(filePath);
          }
        #endregion
            
          #region EventLog

          filePath = String.Format(@"{0}\EventLog.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing EventLog", 63);

          insertedRows += EventLog.Import(filePath);
          }
        #endregion
            
          #region StorageType

          filePath = String.Format(@"{0}\StorageType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing StorageType", 65);

          insertedRows += StorageType.Import(filePath);
          }
        #endregion
            
          #region RouteInventory

          filePath = String.Format(@"{0}\RouteInventory.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteInventory", 68);

          insertedRows += RouteInventory.Import(filePath);
          }
        #endregion
            
          #region InventoryTransactionType

          filePath = String.Format(@"{0}\InventoryTransactionType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing InventoryTransactionType", 70);

          insertedRows += InventoryTransactionType.Import(filePath);
          }
        #endregion
            
          #region InventoryTransaction

          filePath = String.Format(@"{0}\InventoryTransaction.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing InventoryTransaction", 73);

          insertedRows += InventoryTransaction.Import(filePath);
          }
        #endregion
            
          #region InventoryTransactionDetailType

          filePath = String.Format(@"{0}\InventoryTransactionDetailType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing InventoryTransactionDetailType", 75);

          insertedRows += InventoryTransactionDetailType.Import(filePath);
          }
        #endregion
            
          #region InventoryTransactionDetailXRef

          filePath = String.Format(@"{0}\InventoryTransactionDetailXRef.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing InventoryTransactionDetailXRef", 78);

          insertedRows += InventoryTransactionDetailXRef.Import(filePath);
          }
        #endregion
            
          #region InventoryTransactionDetail

          filePath = String.Format(@"{0}\InventoryTransactionDetail.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing InventoryTransactionDetail", 80);

          insertedRows += InventoryTransactionDetail.Import(filePath);
          }
        #endregion
            
          #region Equipment

          filePath = String.Format(@"{0}\Equipment.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Equipment", 83);

          insertedRows += Equipment.Import(filePath);
          }
        #endregion
            
          #region Product

          filePath = String.Format(@"{0}\Product.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Product", 85);

          insertedRows += Product.Import(filePath);
          }
        #endregion
            
          #region RouteOptionDescription

          filePath = String.Format(@"{0}\RouteOptionDescription.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteOptionDescription", 88);

          insertedRows += RouteOptionDescription.Import(filePath);
          }
        #endregion
            
          #region RouteOption

          filePath = String.Format(@"{0}\RouteOption.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing RouteOption", 90);

          insertedRows += RouteOption.Import(filePath);
          }
        #endregion
            
          #region PeriodTransactionType

          filePath = String.Format(@"{0}\PeriodTransactionType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing PeriodTransactionType", 93);

          insertedRows += PeriodTransactionType.Import(filePath);
          }
        #endregion
            
          #region PeriodTransaction

          filePath = String.Format(@"{0}\PeriodTransaction.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing PeriodTransaction", 95);

          insertedRows += PeriodTransaction.Import(filePath);
          }
        #endregion
            
          #region Counter

          filePath = String.Format(@"{0}\Counter.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Counter", 98);

          insertedRows += Counter.Import(filePath);
          }
        #endregion
            
          #region Password

          filePath = String.Format(@"{0}\Password.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Password", 100);

          insertedRows += Password.Import(filePath);
          }
        #endregion
            

        Database.Commit();

        AddMessage("Complete",100);

        }catch(Exception e)
        {
        Database.Rollback();
        AddMessage("Complete with errors",100);
        throw e;
        }
        }
        }
        }
      