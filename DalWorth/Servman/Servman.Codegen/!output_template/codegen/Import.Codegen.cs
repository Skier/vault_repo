
        namespace Servman.Domain
        {
        
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Servman.Data;
    using Servman.SDK;
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
        
          #region Project
          AddMessage("Removing Project", 8);
          Project.Clear();
          #endregion
        
          #region LeadToPartner
          AddMessage("Removing LeadToPartner", 17);
          LeadToPartner.Clear();
          #endregion
        
          #region PartnerProjectType
          AddMessage("Removing PartnerProjectType", 25);
          PartnerProjectType.Clear();
          #endregion
        
          #region Lead
          AddMessage("Removing Lead", 33);
          Lead.Clear();
          #endregion
        
          #region QbJob
          AddMessage("Removing QbJob", 42);
          QbJob.Clear();
          #endregion
        
          #region LeadStatus
          AddMessage("Removing LeadStatus", 50);
          LeadStatus.Clear();
          #endregion
        
          #region Customer
          AddMessage("Removing Customer", 58);
          Customer.Clear();
          #endregion
        
          #region QbCustomer
          AddMessage("Removing QbCustomer", 67);
          QbCustomer.Clear();
          #endregion
        
          #region BusinessPartner
          AddMessage("Removing BusinessPartner", 75);
          BusinessPartner.Clear();
          #endregion
        
          #region ProjectTypeQbItem
          AddMessage("Removing ProjectTypeQbItem", 83);
          ProjectTypeQbItem.Clear();
          #endregion
        
          #region ProjectType
          AddMessage("Removing ProjectType", 92);
          ProjectType.Clear();
          #endregion
        
          #region QbItem
          AddMessage("Removing QbItem", 100);
          QbItem.Clear();
          #endregion
        
        }
        #endregion

        
          #region QbItem

          filePath = String.Format(@"{0}\QbItem.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing QbItem", 8);

          insertedRows += QbItem.Import(filePath);
          }
          #endregion
        
          #region ProjectType

          filePath = String.Format(@"{0}\ProjectType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ProjectType", 17);

          insertedRows += ProjectType.Import(filePath);
          }
          #endregion
        
          #region ProjectTypeQbItem

          filePath = String.Format(@"{0}\ProjectTypeQbItem.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ProjectTypeQbItem", 25);

          insertedRows += ProjectTypeQbItem.Import(filePath);
          }
          #endregion
        
          #region BusinessPartner

          filePath = String.Format(@"{0}\BusinessPartner.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing BusinessPartner", 33);

          insertedRows += BusinessPartner.Import(filePath);
          }
          #endregion
        
          #region QbCustomer

          filePath = String.Format(@"{0}\QbCustomer.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing QbCustomer", 42);

          insertedRows += QbCustomer.Import(filePath);
          }
          #endregion
        
          #region Customer

          filePath = String.Format(@"{0}\Customer.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Customer", 50);

          insertedRows += Customer.Import(filePath);
          }
          #endregion
        
          #region LeadStatus

          filePath = String.Format(@"{0}\LeadStatus.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing LeadStatus", 58);

          insertedRows += LeadStatus.Import(filePath);
          }
          #endregion
        
          #region QbJob

          filePath = String.Format(@"{0}\QbJob.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing QbJob", 67);

          insertedRows += QbJob.Import(filePath);
          }
          #endregion
        
          #region Lead

          filePath = String.Format(@"{0}\Lead.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Lead", 75);

          insertedRows += Lead.Import(filePath);
          }
          #endregion
        
          #region PartnerProjectType

          filePath = String.Format(@"{0}\PartnerProjectType.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing PartnerProjectType", 83);

          insertedRows += PartnerProjectType.Import(filePath);
          }
          #endregion
        
          #region LeadToPartner

          filePath = String.Format(@"{0}\LeadToPartner.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing LeadToPartner", 92);

          insertedRows += LeadToPartner.Import(filePath);
          }
          #endregion
        
          #region Project

          filePath = String.Format(@"{0}\Project.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Project", 100);

          insertedRows += Project.Import(filePath);
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
      