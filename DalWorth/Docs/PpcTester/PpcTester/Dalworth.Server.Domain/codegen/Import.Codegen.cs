
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
        
          #region KeywordStats
          AddMessage("Removing KeywordStats", 9);
          KeywordStats.Clear();
          #endregion
        
          #region Keyword
          AddMessage("Removing Keyword", 18);
          Keyword.Clear();
          #endregion
        
          #region TestResultDetail
          AddMessage("Removing TestResultDetail", 27);
          TestResultDetail.Clear();
          #endregion
        
          #region TestResult
          AddMessage("Removing TestResult", 36);
          TestResult.Clear();
          #endregion
        
          #region TestBatch
          AddMessage("Removing TestBatch", 45);
          TestBatch.Clear();
          #endregion
        
          #region SearchEngine
          AddMessage("Removing SearchEngine", 55);
          SearchEngine.Clear();
          #endregion
        
          #region Company
          AddMessage("Removing Company", 64);
          Company.Clear();
          #endregion
        
          #region Campaign
          AddMessage("Removing Campaign", 73);
          Campaign.Clear();
          #endregion
        
          #region AdGroup
          AddMessage("Removing AdGroup", 82);
          AdGroup.Clear();
          #endregion
        
          #region AdgroupKeyword
          AddMessage("Removing AdgroupKeyword", 91);
          AdgroupKeyword.Clear();
          #endregion
        
          #region Action
          AddMessage("Removing Action", 100);
          Action.Clear();
          #endregion
        
        }
        #endregion

        
          #region Action

          filePath = String.Format(@"{0}\Action.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Action", 9);

          insertedRows += Action.Import(filePath);
          }
          #endregion
        
          #region AdgroupKeyword

          filePath = String.Format(@"{0}\AdgroupKeyword.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing AdgroupKeyword", 18);

          insertedRows += AdgroupKeyword.Import(filePath);
          }
          #endregion
        
          #region AdGroup

          filePath = String.Format(@"{0}\AdGroup.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing AdGroup", 27);

          insertedRows += AdGroup.Import(filePath);
          }
          #endregion
        
          #region Campaign

          filePath = String.Format(@"{0}\Campaign.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Campaign", 36);

          insertedRows += Campaign.Import(filePath);
          }
          #endregion
        
          #region Company

          filePath = String.Format(@"{0}\Company.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Company", 45);

          insertedRows += Company.Import(filePath);
          }
          #endregion
        
          #region SearchEngine

          filePath = String.Format(@"{0}\SearchEngine.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing SearchEngine", 55);

          insertedRows += SearchEngine.Import(filePath);
          }
          #endregion
        
          #region TestBatch

          filePath = String.Format(@"{0}\TestBatch.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing TestBatch", 64);

          insertedRows += TestBatch.Import(filePath);
          }
          #endregion
        
          #region TestResult

          filePath = String.Format(@"{0}\TestResult.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing TestResult", 73);

          insertedRows += TestResult.Import(filePath);
          }
          #endregion
        
          #region TestResultDetail

          filePath = String.Format(@"{0}\TestResultDetail.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing TestResultDetail", 82);

          insertedRows += TestResultDetail.Import(filePath);
          }
          #endregion
        
          #region Keyword

          filePath = String.Format(@"{0}\Keyword.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing Keyword", 91);

          insertedRows += Keyword.Import(filePath);
          }
          #endregion
        
          #region KeywordStats

          filePath = String.Format(@"{0}\KeywordStats.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing KeywordStats", 100);

          insertedRows += KeywordStats.Import(filePath);
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
      