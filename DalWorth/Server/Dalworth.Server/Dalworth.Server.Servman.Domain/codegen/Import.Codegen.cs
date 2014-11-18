
        namespace Dalworth.Server.Servman.Domain
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
        
          #region zip_data
          AddMessage("Removing zip_data", 5);
          zip_data.Clear();
          #endregion
        
          #region trucknum
          AddMessage("Removing trucknum", 11);
          trucknum.Clear();
          #endregion
        
          #region truck
          AddMessage("Removing truck", 16);
          truck.Clear();
          #endregion
        
          #region timeslot
          AddMessage("Removing timeslot", 21);
          timeslot.Clear();
          #endregion
        
          #region techschd
          AddMessage("Removing techschd", 26);
          techschd.Clear();
          #endregion
        
          #region techmast
          AddMessage("Removing techmast", 32);
          techmast.Clear();
          #endregion
        
          #region rights
          AddMessage("Removing rights", 37);
          rights.Clear();
          #endregion
        
          #region mapsco
          AddMessage("Removing mapsco", 42);
          mapsco.Clear();
          #endregion
        
          #region m_alt_ad
          AddMessage("Removing m_alt_ad", 47);
          m_alt_ad.Clear();
          #endregion
        
          #region hdeflood
          AddMessage("Removing hdeflood", 53);
          hdeflood.Clear();
          #endregion
        
          #region h_order
          AddMessage("Removing h_order", 58);
          h_order.Clear();
          #endregion
        
          #region disp_que
          AddMessage("Removing disp_que", 63);
          disp_que.Clear();
          #endregion
        
          #region df_dt
          AddMessage("Removing df_dt", 68);
          df_dt.Clear();
          #endregion
        
          #region ddeflood
          AddMessage("Removing ddeflood", 74);
          ddeflood.Clear();
          #endregion
        
          #region custmast
          AddMessage("Removing custmast", 79);
          custmast.Clear();
          #endregion
        
          #region contmast
          AddMessage("Removing contmast", 84);
          contmast.Clear();
          #endregion
        
          #region company
          AddMessage("Removing company", 89);
          company.Clear();
          #endregion
        
          #region area
          AddMessage("Removing area", 95);
          area.Clear();
          #endregion
        
          #region ad_src
          AddMessage("Removing ad_src", 100);
          ad_src.Clear();
          #endregion
        
        }
        #endregion

        
          #region ad_src

          filePath = String.Format(@"{0}\ad_src.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ad_src", 5);

          insertedRows += ad_src.Import(filePath);
          }
          #endregion
        
          #region area

          filePath = String.Format(@"{0}\area.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing area", 11);

          insertedRows += area.Import(filePath);
          }
          #endregion
        
          #region company

          filePath = String.Format(@"{0}\company.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing company", 16);

          insertedRows += company.Import(filePath);
          }
          #endregion
        
          #region contmast

          filePath = String.Format(@"{0}\contmast.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing contmast", 21);

          insertedRows += contmast.Import(filePath);
          }
          #endregion
        
          #region custmast

          filePath = String.Format(@"{0}\custmast.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing custmast", 26);

          insertedRows += custmast.Import(filePath);
          }
          #endregion
        
          #region ddeflood

          filePath = String.Format(@"{0}\ddeflood.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing ddeflood", 32);

          insertedRows += ddeflood.Import(filePath);
          }
          #endregion
        
          #region df_dt

          filePath = String.Format(@"{0}\df_dt.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing df_dt", 37);

          insertedRows += df_dt.Import(filePath);
          }
          #endregion
        
          #region disp_que

          filePath = String.Format(@"{0}\disp_que.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing disp_que", 42);

          insertedRows += disp_que.Import(filePath);
          }
          #endregion
        
          #region h_order

          filePath = String.Format(@"{0}\h_order.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing h_order", 47);

          insertedRows += h_order.Import(filePath);
          }
          #endregion
        
          #region hdeflood

          filePath = String.Format(@"{0}\hdeflood.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing hdeflood", 53);

          insertedRows += hdeflood.Import(filePath);
          }
          #endregion
        
          #region m_alt_ad

          filePath = String.Format(@"{0}\m_alt_ad.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing m_alt_ad", 58);

          insertedRows += m_alt_ad.Import(filePath);
          }
          #endregion
        
          #region mapsco

          filePath = String.Format(@"{0}\mapsco.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing mapsco", 63);

          insertedRows += mapsco.Import(filePath);
          }
          #endregion
        
          #region rights

          filePath = String.Format(@"{0}\rights.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing rights", 68);

          insertedRows += rights.Import(filePath);
          }
          #endregion
        
          #region techmast

          filePath = String.Format(@"{0}\techmast.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing techmast", 74);

          insertedRows += techmast.Import(filePath);
          }
          #endregion
        
          #region techschd

          filePath = String.Format(@"{0}\techschd.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing techschd", 79);

          insertedRows += techschd.Import(filePath);
          }
          #endregion
        
          #region timeslot

          filePath = String.Format(@"{0}\timeslot.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing timeslot", 84);

          insertedRows += timeslot.Import(filePath);
          }
          #endregion
        
          #region truck

          filePath = String.Format(@"{0}\truck.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing truck", 89);

          insertedRows += truck.Import(filePath);
          }
          #endregion
        
          #region trucknum

          filePath = String.Format(@"{0}\trucknum.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing trucknum", 95);

          insertedRows += trucknum.Import(filePath);
          }
          #endregion
        
          #region zip_data

          filePath = String.Format(@"{0}\zip_data.xml",importFolder);

          if(File.Exists(filePath))
          {

          AddMessage("Importing zip_data", 100);

          insertedRows += zip_data.Import(filePath);
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
      