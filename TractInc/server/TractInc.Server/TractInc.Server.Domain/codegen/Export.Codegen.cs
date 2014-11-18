
        namespace TractInc.Server.Domain
        {
        
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
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

        
          #region AccountType

          filePath = String.Format(@"{0}\AccountType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting AccountType", 5);

          exportedRows += AccountType.Export(filePath);

          #endregion
        
          #region Client

          filePath = String.Format(@"{0}\Client.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Client", 10);

          exportedRows += Client.Export(filePath);

          #endregion
        
          #region Company

          filePath = String.Format(@"{0}\Company.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Company", 14);

          exportedRows += Company.Export(filePath);

          #endregion
        
          #region ClientCompany

          filePath = String.Format(@"{0}\ClientCompany.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ClientCompany", 19);

          exportedRows += ClientCompany.Export(filePath);

          #endregion
        
          #region Account

          filePath = String.Format(@"{0}\Account.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Account", 24);

          exportedRows += Account.Export(filePath);

          #endregion
        
          #region ContractStatus

          filePath = String.Format(@"{0}\ContractStatus.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ContractStatus", 29);

          exportedRows += ContractStatus.Export(filePath);

          #endregion
        
          #region Contract

          filePath = String.Format(@"{0}\Contract.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Contract", 33);

          exportedRows += Contract.Export(filePath);

          #endregion
        
          #region Person

          filePath = String.Format(@"{0}\Person.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Person", 38);

          exportedRows += Person.Export(filePath);

          #endregion
        
          #region ClientContact

          filePath = String.Format(@"{0}\ClientContact.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ClientContact", 43);

          exportedRows += ClientContact.Export(filePath);

          #endregion
        
          #region CompanyContact

          filePath = String.Format(@"{0}\CompanyContact.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting CompanyContact", 48);

          exportedRows += CompanyContact.Export(filePath);

          #endregion
        
          #region User

          filePath = String.Format(@"{0}\User.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting User", 52);

          exportedRows += User.Export(filePath);

          #endregion
        
          #region UserPreference

          filePath = String.Format(@"{0}\UserPreference.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting UserPreference", 57);

          exportedRows += UserPreference.Export(filePath);

          #endregion
        
          #region Role

          filePath = String.Format(@"{0}\Role.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Role", 62);

          exportedRows += Role.Export(filePath);

          #endregion
        
          #region UserRole

          filePath = String.Format(@"{0}\UserRole.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting UserRole", 67);

          exportedRows += UserRole.Export(filePath);

          #endregion
        
          #region Module

          filePath = String.Format(@"{0}\Module.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Module", 71);

          exportedRows += Module.Export(filePath);

          #endregion
        
          #region Permission

          filePath = String.Format(@"{0}\Permission.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Permission", 76);

          exportedRows += Permission.Export(filePath);

          #endregion
        
          #region PermissionAssignment

          filePath = String.Format(@"{0}\PermissionAssignment.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting PermissionAssignment", 81);

          exportedRows += PermissionAssignment.Export(filePath);

          #endregion
        
          #region InvoiceItemType

          filePath = String.Format(@"{0}\InvoiceItemType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting InvoiceItemType", 86);

          exportedRows += InvoiceItemType.Export(filePath);

          #endregion
        
          #region ContractRate

          filePath = String.Format(@"{0}\ContractRate.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ContractRate", 90);

          exportedRows += ContractRate.Export(filePath);

          #endregion
        
          #region State

          filePath = String.Format(@"{0}\State.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting State", 95);

          exportedRows += State.Export(filePath);

          #endregion
        
          #region County

          filePath = String.Format(@"{0}\County.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting County", 100);

          exportedRows += County.Export(filePath);

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
      