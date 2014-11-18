using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class AdSourceByYear
    {
        #region Constructor

        public AdSourceByYear(string adSourceName,
            int janQty, decimal janAmt,
            int febQty, decimal febAmt,
            int marQty, decimal marAmt,
            int aprQty, decimal aprAmt,
            int mayQty, decimal mayAmt,
            int junQty, decimal junAmt,
            int julQty, decimal julAmt,
            int augQty, decimal augAmt,
            int sepQty, decimal sepAmt,
            int octQty, decimal octAmt,
            int novQty, decimal novAmt,
            int decQty, decimal decAmt)
        {
            this.m_adSourceName = adSourceName;

            this.m_janAmt = janAmt;
            this.m_janQty = janQty;

            this.m_febAmt = febAmt;
            this.m_febQty = febQty;

            this.m_marAmt = marAmt;
            this.m_marQty = marQty;

            this.m_aprAmt = aprAmt;
            this.m_aprQty = aprQty;

            this.m_mayAmt = mayAmt;
            this.m_mayQty = mayQty;

            this.m_junAmt = junAmt;
            this.m_junQty = junQty;

            this.m_julAmt = julAmt;
            this.m_julQty = julQty;

            this.m_augAmt = augAmt;
            this.m_augQty = augQty;

            this.m_sepAmt = sepAmt;
            this.m_sepQty = sepQty;

            this.m_octAmt = octAmt;
            this.m_octQty = octQty;

            this.m_novAmt = novAmt;
            this.m_novQty = novQty;

            this.m_decAmt = decAmt;
            this.m_decQty = decQty;
        }

        #endregion

        #region AdSourceName

        private string m_adSourceName;
        public string AdSourceName
        {
            get { return m_adSourceName; }
        }

        #endregion

        #region Totals And Quanitities For Each Month

        #region January

        private decimal m_janAmt;
        public decimal JanAmt
        {
            get { return m_janAmt; }
        }

        private int m_janQty;
        public int JanQty
        {
            get { return m_janQty; }
        }

        #endregion

        #region February

        private decimal m_febAmt;
        public decimal FebAmt
        {
            get { return m_febAmt; }
        }

        private int m_febQty;
        public int FebQty
        {
            get { return m_febQty; }
        }

        #endregion

        #region March

        private decimal m_marAmt;
        public decimal MarAmt
        {
            get { return m_marAmt; }
        }

        private int m_marQty;
        public int MarQty
        {
            get { return m_marQty; }
        }

        #endregion

        #region April

        private decimal m_aprAmt;
        public decimal AprAmt
        {
            get { return m_aprAmt; }
            set { m_aprAmt = value; }
        }

        private int m_aprQty;
        public int AprQty
        {
            get { return m_aprQty; }
        }

        #endregion

        #region May

        private decimal m_mayAmt;
        public decimal MayAmt
        {
            get { return m_mayAmt; }
        }

        private int m_mayQty;
        public int MayQty
        {
            get { return m_mayQty; }
        }

        #endregion

        #region June

        private decimal m_junAmt;
        public decimal JunAmt
        {
            get { return m_junAmt; }
        }

        private int m_junQty;
        public int JunQty
        {
            get { return m_junQty; }
        }

        #endregion

        #region July

        private decimal m_julAmt;
        public decimal JulAmt
        {
            get { return m_julAmt; }
            set { m_julAmt = value; }
        }

        private int m_julQty;
        public int JulQty
        {
            get { return m_julQty; }
            set { m_julQty = value; }
        }

        #endregion

        #region August

        private decimal m_augAmt;
        public decimal AugAmt
        {
            get { return m_augAmt; }
        }

        private int m_augQty;
        public int AugQty
        {
            get { return m_augQty; }
        }

        #endregion

        #region September

        private decimal m_sepAmt;
        public decimal SepAmt
        {
            get { return m_sepAmt; }
            set { m_sepAmt = value; }
        }

        private int m_sepQty;
        public int SepQty
        {
            get { return m_sepQty; }
        }

        #endregion

        #region October

        private decimal m_octAmt;
        public decimal OctAmt
        {
            get { return m_octAmt; }
        }

        private int m_octQty;
        public int OctQty
        {
            get { return m_octQty; }
        }

        #endregion

        #region November

        private decimal m_novAmt;
        public decimal NovAmt
        {
            get { return m_novAmt; }
        }

        private int m_novQty;
        public int NovQty
        {
            get { return m_novQty; }
        }

        #endregion

        #region December

        private decimal m_decAmt;
        public decimal DecAmt
        {
            get { return m_decAmt; }
        }

        private int m_decQty;
        public int DecQty
        {
            get { return m_decQty; }
        }

        #endregion

        #endregion

        #region Find

        public static List<AdSourceByYear> Find(int year, int projectTypeId)
        {
            string sqlFind = @"call ReportAdvertisingSourceByYear(?Year);";

            if (projectTypeId != 0)
                sqlFind = @"call ReportAdvertisingSourceByYearProjectTypeId(?Year, ?ProjectTypeId);";

            List<AdSourceByYear> result = new List<AdSourceByYear>();

            using (IDbCommand dbCommand = Database.PrepareCommand(sqlFind))
            {
                Database.PutParameter(dbCommand, "?Year", year);

                if (projectTypeId != 0)
                    Database.PutParameter(dbCommand, "?ProjectTypeId", projectTypeId);

                System.DateTime startLoop = System.DateTime.Now;
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    int i = 0;
                    while (dataReader.Read())
                    {
                        AdSourceByYear record = new AdSourceByYear(
                        dataReader.GetString(0),
                        dataReader.GetInt32(1), dataReader.GetDecimal(2),
                        dataReader.GetInt32(3), dataReader.GetDecimal(4),
                        dataReader.GetInt32(5), dataReader.GetDecimal(6),
                        dataReader.GetInt32(7), dataReader.GetDecimal(8),
                        dataReader.GetInt32(9), dataReader.GetDecimal(10),
                        dataReader.GetInt32(11), dataReader.GetDecimal(12),
                        dataReader.GetInt32(13), dataReader.GetDecimal(14),
                        dataReader.GetInt32(15), dataReader.GetDecimal(16),
                        dataReader.GetInt32(17), dataReader.GetDecimal(18),
                        dataReader.GetInt32(19), dataReader.GetDecimal(20),
                        dataReader.GetInt32(21), dataReader.GetDecimal(22),
                        dataReader.GetInt32(23), dataReader.GetDecimal(24));
                        result.Add(record);
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
