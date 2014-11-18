
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

        
          #region QbItem

          filePath = String.Format(@"{0}\QbItem.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting QbItem", 9);

          exportedRows += QbItem.Export(filePath);

          #endregion
        
          #region ProjectType

          filePath = String.Format(@"{0}\ProjectType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ProjectType", 18);

          exportedRows += ProjectType.Export(filePath);

          #endregion
        
          #region ProjectTypeQbItem

          filePath = String.Format(@"{0}\ProjectTypeQbItem.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ProjectTypeQbItem", 27);

          exportedRows += ProjectTypeQbItem.Export(filePath);

          #endregion
        
          #region BusinessPartner

          filePath = String.Format(@"{0}\BusinessPartner.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting BusinessPartner", 36);

          exportedRows += BusinessPartner.Export(filePath);

          #endregion
        
          #region Lead

          filePath = String.Format(@"{0}\Lead.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Lead", 45);

          exportedRows += Lead.Export(filePath);

          #endregion
        
          #region QbCustomer

          filePath = String.Format(@"{0}\QbCustomer.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting QbCustomer", 55);

          exportedRows += QbCustomer.Export(filePath);

          #endregion
        
          #region Customer

          filePath = String.Format(@"{0}\Customer.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Customer", 64);

          exportedRows += Customer.Export(filePath);

          #endregion
        
          #region QbJob

          filePath = String.Format(@"{0}\QbJob.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting QbJob", 73);

          exportedRows += QbJob.Export(filePath);

          #endregion
        
          #region Project

          filePath = String.Format(@"{0}\Project.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Project", 82);

          exportedRows += Project.Export(filePath);

          #endregion
        
          #region PartnerProjectType

          filePath = String.Format(@"{0}\PartnerProjectType.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting PartnerProjectType", 91);

          exportedRows += PartnerProjectType.Export(filePath);

          #endregion
        
          #region LeadToPartner

          filePath = String.Format(@"{0}\LeadToPartner.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting LeadToPartner", 100);

          exportedRows += LeadToPartner.Export(filePath);

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
      