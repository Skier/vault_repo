
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

        public class Export:Task
        {

        public Export(){}
        public Export(String path)
        {
          exportFolder = path;
        }


        private String exportFolder;

        public String ExportFolder
        {
        get { return exportFolder; }
        set { exportFolder = value; }
        }

        private int exportedRows;

        public int ExportedRows
        {
        get { return exportedRows; }
        set { exportedRows = value; }
        }

        protected override void Main()
        {

        try
        {

        Database.Begin();

        String filePath = String.Empty;

        exportedRows = 0;

        
          #region BusinessTransactionStatus

          filePath = String.Format(@"{0}\BusinessTransactionStatus.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting BusinessTransactionStatus", 3);

          exportedRows += BusinessTransactionStatus.Export(filePath);
          
          #endregion
        
          #region BusinessTransactionType

          filePath = String.Format(@"{0}\BusinessTransactionType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting BusinessTransactionType", 5);

          exportedRows += BusinessTransactionType.Export(filePath);
          
          #endregion
        
          #region Location

          filePath = String.Format(@"{0}\Location.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Location", 8);

          exportedRows += Location.Export(filePath);
          
          #endregion
        
          #region Employee

          filePath = String.Format(@"{0}\Employee.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Employee", 10);

          exportedRows += Employee.Export(filePath);
          
          #endregion
        
          #region RouteStatus

          filePath = String.Format(@"{0}\RouteStatus.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteStatus", 13);

          exportedRows += RouteStatus.Export(filePath);
          
          #endregion
        
          #region RouteType

          filePath = String.Format(@"{0}\RouteType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteType", 15);

          exportedRows += RouteType.Export(filePath);
          
          #endregion
        
          #region Route

          filePath = String.Format(@"{0}\Route.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Route", 18);

          exportedRows += Route.Export(filePath);
          
          #endregion
        
          #region Session

          filePath = String.Format(@"{0}\Session.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Session", 20);

          exportedRows += Session.Export(filePath);
          
          #endregion
        
          #region ItemType

          filePath = String.Format(@"{0}\ItemType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ItemType", 23);

          exportedRows += ItemType.Export(filePath);
          
          #endregion
        
          #region ItemCategory

          filePath = String.Format(@"{0}\ItemCategory.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ItemCategory", 25);

          exportedRows += ItemCategory.Export(filePath);
          
          #endregion
        
          #region Item

          filePath = String.Format(@"{0}\Item.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Item", 28);

          exportedRows += Item.Export(filePath);
          
          #endregion
        
          #region CustomerTransactionType

          filePath = String.Format(@"{0}\CustomerTransactionType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting CustomerTransactionType", 30);

          exportedRows += CustomerTransactionType.Export(filePath);
          
          #endregion
        
          #region Customer

          filePath = String.Format(@"{0}\Customer.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Customer", 33);

          exportedRows += Customer.Export(filePath);
          
          #endregion
        
          #region RouteCustomer

          filePath = String.Format(@"{0}\RouteCustomer.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteCustomer", 35);

          exportedRows += RouteCustomer.Export(filePath);
          
          #endregion
        
          #region CustomerVisit

          filePath = String.Format(@"{0}\CustomerVisit.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting CustomerVisit", 38);

          exportedRows += CustomerVisit.Export(filePath);
          
          #endregion
        
          #region BusinessTransaction

          filePath = String.Format(@"{0}\BusinessTransaction.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting BusinessTransaction", 40);

          exportedRows += BusinessTransaction.Export(filePath);
          
          #endregion
        
          #region CustomerTransaction

          filePath = String.Format(@"{0}\CustomerTransaction.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting CustomerTransaction", 43);

          exportedRows += CustomerTransaction.Export(filePath);
          
          #endregion
        
          #region CustomerOptionDescription

          filePath = String.Format(@"{0}\CustomerOptionDescription.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting CustomerOptionDescription", 45);

          exportedRows += CustomerOptionDescription.Export(filePath);
          
          #endregion
        
          #region CustomerOption

          filePath = String.Format(@"{0}\CustomerOption.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting CustomerOption", 48);

          exportedRows += CustomerOption.Export(filePath);
          
          #endregion
        
          #region DayOfWeek

          filePath = String.Format(@"{0}\DayOfWeek.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting DayOfWeek", 50);

          exportedRows += DayOfWeek.Export(filePath);
          
          #endregion
        
          #region RouteSchedule

          filePath = String.Format(@"{0}\RouteSchedule.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteSchedule", 53);

          exportedRows += RouteSchedule.Export(filePath);
          
          #endregion
        
          #region RouteScheduleQueueStatus

          filePath = String.Format(@"{0}\RouteScheduleQueueStatus.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteScheduleQueueStatus", 55);

          exportedRows += RouteScheduleQueueStatus.Export(filePath);
          
          #endregion
        
          #region RouteScheduleQueue

          filePath = String.Format(@"{0}\RouteScheduleQueue.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteScheduleQueue", 58);

          exportedRows += RouteScheduleQueue.Export(filePath);
          
          #endregion
        
          #region CustomerTransactionDetail

          filePath = String.Format(@"{0}\CustomerTransactionDetail.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting CustomerTransactionDetail", 60);

          exportedRows += CustomerTransactionDetail.Export(filePath);
          
          #endregion
        
          #region EventLog

          filePath = String.Format(@"{0}\EventLog.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting EventLog", 63);

          exportedRows += EventLog.Export(filePath);
          
          #endregion
        
          #region StorageType

          filePath = String.Format(@"{0}\StorageType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting StorageType", 65);

          exportedRows += StorageType.Export(filePath);
          
          #endregion
        
          #region RouteInventory

          filePath = String.Format(@"{0}\RouteInventory.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteInventory", 68);

          exportedRows += RouteInventory.Export(filePath);
          
          #endregion
        
          #region InventoryTransactionType

          filePath = String.Format(@"{0}\InventoryTransactionType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting InventoryTransactionType", 70);

          exportedRows += InventoryTransactionType.Export(filePath);
          
          #endregion
        
          #region InventoryTransaction

          filePath = String.Format(@"{0}\InventoryTransaction.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting InventoryTransaction", 73);

          exportedRows += InventoryTransaction.Export(filePath);
          
          #endregion
        
          #region InventoryTransactionDetailType

          filePath = String.Format(@"{0}\InventoryTransactionDetailType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting InventoryTransactionDetailType", 75);

          exportedRows += InventoryTransactionDetailType.Export(filePath);
          
          #endregion
        
          #region InventoryTransactionDetailXRef

          filePath = String.Format(@"{0}\InventoryTransactionDetailXRef.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting InventoryTransactionDetailXRef", 78);

          exportedRows += InventoryTransactionDetailXRef.Export(filePath);
          
          #endregion
        
          #region InventoryTransactionDetail

          filePath = String.Format(@"{0}\InventoryTransactionDetail.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting InventoryTransactionDetail", 80);

          exportedRows += InventoryTransactionDetail.Export(filePath);
          
          #endregion
        
          #region Equipment

          filePath = String.Format(@"{0}\Equipment.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Equipment", 83);

          exportedRows += Equipment.Export(filePath);
          
          #endregion
        
          #region Product

          filePath = String.Format(@"{0}\Product.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Product", 85);

          exportedRows += Product.Export(filePath);
          
          #endregion
        
          #region RouteOptionDescription

          filePath = String.Format(@"{0}\RouteOptionDescription.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteOptionDescription", 88);

          exportedRows += RouteOptionDescription.Export(filePath);
          
          #endregion
        
          #region RouteOption

          filePath = String.Format(@"{0}\RouteOption.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting RouteOption", 90);

          exportedRows += RouteOption.Export(filePath);
          
          #endregion
        
          #region PeriodTransactionType

          filePath = String.Format(@"{0}\PeriodTransactionType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting PeriodTransactionType", 93);

          exportedRows += PeriodTransactionType.Export(filePath);
          
          #endregion
        
          #region PeriodTransaction

          filePath = String.Format(@"{0}\PeriodTransaction.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting PeriodTransaction", 95);

          exportedRows += PeriodTransaction.Export(filePath);
          
          #endregion
        
          #region Counter

          filePath = String.Format(@"{0}\Counter.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Counter", 98);

          exportedRows += Counter.Export(filePath);
          
          #endregion
        
          #region Password

          filePath = String.Format(@"{0}\Password.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Password", 100);

          exportedRows += Password.Export(filePath);
          
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
      