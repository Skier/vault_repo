
        namespace Dalworth.Server.Domain
        {
        
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
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

        
          #region Action

          filePath = String.Format(@"{0}\Action.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Action", 9);

          exportedRows += Action.Export(filePath);

          #endregion
        
          #region AdgroupKeyword

          filePath = String.Format(@"{0}\AdgroupKeyword.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting AdgroupKeyword", 18);

          exportedRows += AdgroupKeyword.Export(filePath);

          #endregion
        
          #region AdGroup

          filePath = String.Format(@"{0}\AdGroup.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting AdGroup", 27);

          exportedRows += AdGroup.Export(filePath);

          #endregion
        
          #region Campaign

          filePath = String.Format(@"{0}\Campaign.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Campaign", 36);

          exportedRows += Campaign.Export(filePath);

          #endregion
        
          #region Company

          filePath = String.Format(@"{0}\Company.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Company", 45);

          exportedRows += Company.Export(filePath);

          #endregion
        
          #region SearchEngine

          filePath = String.Format(@"{0}\SearchEngine.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting SearchEngine", 55);

          exportedRows += SearchEngine.Export(filePath);

          #endregion
        
          #region TestBatch

          filePath = String.Format(@"{0}\TestBatch.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting TestBatch", 64);

          exportedRows += TestBatch.Export(filePath);

          #endregion
        
          #region TestResult

          filePath = String.Format(@"{0}\TestResult.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting TestResult", 73);

          exportedRows += TestResult.Export(filePath);

          #endregion
        
          #region TestResultDetail

          filePath = String.Format(@"{0}\TestResultDetail.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting TestResultDetail", 82);

          exportedRows += TestResultDetail.Export(filePath);

          #endregion
        
          #region Keyword

          filePath = String.Format(@"{0}\Keyword.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting Keyword", 91);

          exportedRows += Keyword.Export(filePath);

          #endregion
        
          #region KeywordStats

          filePath = String.Format(@"{0}\KeywordStats.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting KeywordStats", 100);

          exportedRows += KeywordStats.Export(filePath);

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
      