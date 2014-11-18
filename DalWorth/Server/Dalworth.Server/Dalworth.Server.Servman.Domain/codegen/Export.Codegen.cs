
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

        
          #region ad_src

          filePath = String.Format(@"{0}\ad_src.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ad_src", 5);

          exportedRows += ad_src.Export(filePath);

          #endregion
        
          #region area

          filePath = String.Format(@"{0}\area.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting area", 11);

          exportedRows += area.Export(filePath);

          #endregion
        
          #region company

          filePath = String.Format(@"{0}\company.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting company", 16);

          exportedRows += company.Export(filePath);

          #endregion
        
          #region contmast

          filePath = String.Format(@"{0}\contmast.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting contmast", 21);

          exportedRows += contmast.Export(filePath);

          #endregion
        
          #region custmast

          filePath = String.Format(@"{0}\custmast.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting custmast", 26);

          exportedRows += custmast.Export(filePath);

          #endregion
        
          #region ddeflood

          filePath = String.Format(@"{0}\ddeflood.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting ddeflood", 32);

          exportedRows += ddeflood.Export(filePath);

          #endregion
        
          #region df_dt

          filePath = String.Format(@"{0}\df_dt.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting df_dt", 37);

          exportedRows += df_dt.Export(filePath);

          #endregion
        
          #region disp_que

          filePath = String.Format(@"{0}\disp_que.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting disp_que", 42);

          exportedRows += disp_que.Export(filePath);

          #endregion
        
          #region h_order

          filePath = String.Format(@"{0}\h_order.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting h_order", 47);

          exportedRows += h_order.Export(filePath);

          #endregion
        
          #region hdeflood

          filePath = String.Format(@"{0}\hdeflood.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting hdeflood", 53);

          exportedRows += hdeflood.Export(filePath);

          #endregion
        
          #region m_alt_ad

          filePath = String.Format(@"{0}\m_alt_ad.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting m_alt_ad", 58);

          exportedRows += m_alt_ad.Export(filePath);

          #endregion
        
          #region mapsco

          filePath = String.Format(@"{0}\mapsco.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting mapsco", 63);

          exportedRows += mapsco.Export(filePath);

          #endregion
        
          #region rights

          filePath = String.Format(@"{0}\rights.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting rights", 68);

          exportedRows += rights.Export(filePath);

          #endregion
        
          #region techmast

          filePath = String.Format(@"{0}\techmast.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting techmast", 74);

          exportedRows += techmast.Export(filePath);

          #endregion
        
          #region techschd

          filePath = String.Format(@"{0}\techschd.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting techschd", 79);

          exportedRows += techschd.Export(filePath);

          #endregion
        
          #region timeslot

          filePath = String.Format(@"{0}\timeslot.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting timeslot", 84);

          exportedRows += timeslot.Export(filePath);

          #endregion
        
          #region truck

          filePath = String.Format(@"{0}\truck.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting truck", 89);

          exportedRows += truck.Export(filePath);

          #endregion
        
          #region trucknum

          filePath = String.Format(@"{0}\trucknum.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting trucknum", 95);

          exportedRows += trucknum.Export(filePath);

          #endregion
        
          #region zip_data

          filePath = String.Format(@"{0}\zip_data.xml",exportFolder);

          if(File.Exists(filePath))
          File.Delete(filePath);

          AddMessage("Exporting zip_data", 100);

          exportedRows += zip_data.Export(filePath);

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
      