
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
        
          #region County
          AddMessage("Removing County", 5);
          County.Clear();
          #endregion
        
          #region State
          AddMessage("Removing State", 10);
          State.Clear();
          #endregion
        
          #region ContractRate
          AddMessage("Removing ContractRate", 14);
          ContractRate.Clear();
          #endregion
        
          #region InvoiceItemType
          AddMessage("Removing InvoiceItemType", 19);
          InvoiceItemType.Clear();
          #endregion
        
          #region PermissionAssignment
          AddMessage("Removing PermissionAssignment", 24);
          PermissionAssignment.Clear();
          #endregion
        
          #region Permission
          AddMessage("Removing Permission", 29);
          Permission.Clear();
          #endregion
        
          #region Module
          AddMessage("Removing Module", 33);
          Module.Clear();
          #endregion
        
          #region UserRole
          AddMessage("Removing UserRole", 38);
          UserRole.Clear();
          #endregion
        
          #region Role
          AddMessage("Removing Role", 43);
          Role.Clear();
          #endregion
        
          #region UserPreference
          AddMessage("Removing UserPreference", 48);
          UserPreference.Clear();
          #endregion
        
          #region User
          AddMessage("Removing User", 52);
          User.Clear();
          #endregion
        
          #region CompanyContact
          AddMessage("Removing CompanyContact", 57);
          CompanyContact.Clear();
          #endregion
        
          #region ClientContact
          AddMessage("Removing ClientContact", 62);
          ClientContact.Clear();
          #endregion
        
          #region Person
          AddMessage("Removing Person", 67);
          Person.Clear();
          #endregion
        
          #region Contract
          AddMessage("Removing Contract", 71);
          Contract.Clear();
          #endregion
        
          #region ContractStatus
          AddMessage("Removing ContractStatus", 76);
          ContractStatus.Clear();
          #endregion
        
          #region Account
          AddMessage("Removing Account", 81);
          Account.Clear();
          #endregion
        
          #region ClientCompany
          AddMessage("Removing ClientCompany", 86);
          ClientCompany.Clear();
          #endregion
        
          #region Company
          AddMessage("Removing Company", 90);
          Company.Clear();
          #endregion
        
          #region Client
          AddMessage("Removing Client", 95);
          Client.Clear();
          #endregion
        
          #region AccountType
          AddMessage("Removing AccountType", 100);
          AccountType.Clear();
          #endregion
        
        }
        #endregion

        
          #region AccountType

          filePath = String.Format(@"{0}\AccountType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing AccountType", 5);

          insertedRows += AccountType.Import(filePath);
          }
          #endregion
        
          #region Client

          filePath = String.Format(@"{0}\Client.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Client", 10);

          insertedRows += Client.Import(filePath);
          }
          #endregion
        
          #region Company

          filePath = String.Format(@"{0}\Company.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Company", 14);

          insertedRows += Company.Import(filePath);
          }
          #endregion
        
          #region ClientCompany

          filePath = String.Format(@"{0}\ClientCompany.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ClientCompany", 19);

          insertedRows += ClientCompany.Import(filePath);
          }
          #endregion
        
          #region Account

          filePath = String.Format(@"{0}\Account.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Account", 24);

          insertedRows += Account.Import(filePath);
          }
          #endregion
        
          #region ContractStatus

          filePath = String.Format(@"{0}\ContractStatus.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ContractStatus", 29);

          insertedRows += ContractStatus.Import(filePath);
          }
          #endregion
        
          #region Contract

          filePath = String.Format(@"{0}\Contract.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Contract", 33);

          insertedRows += Contract.Import(filePath);
          }
          #endregion
        
          #region Person

          filePath = String.Format(@"{0}\Person.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Person", 38);

          insertedRows += Person.Import(filePath);
          }
          #endregion
        
          #region ClientContact

          filePath = String.Format(@"{0}\ClientContact.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ClientContact", 43);

          insertedRows += ClientContact.Import(filePath);
          }
          #endregion
        
          #region CompanyContact

          filePath = String.Format(@"{0}\CompanyContact.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing CompanyContact", 48);

          insertedRows += CompanyContact.Import(filePath);
          }
          #endregion
        
          #region User

          filePath = String.Format(@"{0}\User.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing User", 52);

          insertedRows += User.Import(filePath);
          }
          #endregion
        
          #region UserPreference

          filePath = String.Format(@"{0}\UserPreference.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing UserPreference", 57);

          insertedRows += UserPreference.Import(filePath);
          }
          #endregion
        
          #region Role

          filePath = String.Format(@"{0}\Role.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Role", 62);

          insertedRows += Role.Import(filePath);
          }
          #endregion
        
          #region UserRole

          filePath = String.Format(@"{0}\UserRole.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing UserRole", 67);

          insertedRows += UserRole.Import(filePath);
          }
          #endregion
        
          #region Module

          filePath = String.Format(@"{0}\Module.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Module", 71);

          insertedRows += Module.Import(filePath);
          }
          #endregion
        
          #region Permission

          filePath = String.Format(@"{0}\Permission.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Permission", 76);

          insertedRows += Permission.Import(filePath);
          }
          #endregion
        
          #region PermissionAssignment

          filePath = String.Format(@"{0}\PermissionAssignment.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing PermissionAssignment", 81);

          insertedRows += PermissionAssignment.Import(filePath);
          }
          #endregion
        
          #region InvoiceItemType

          filePath = String.Format(@"{0}\InvoiceItemType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing InvoiceItemType", 86);

          insertedRows += InvoiceItemType.Import(filePath);
          }
          #endregion
        
          #region ContractRate

          filePath = String.Format(@"{0}\ContractRate.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ContractRate", 90);

          insertedRows += ContractRate.Import(filePath);
          }
          #endregion
        
          #region State

          filePath = String.Format(@"{0}\State.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing State", 95);

          insertedRows += State.Import(filePath);
          }
          #endregion
        
          #region County

          filePath = String.Format(@"{0}\County.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing County", 100);

          insertedRows += County.Import(filePath);
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
      