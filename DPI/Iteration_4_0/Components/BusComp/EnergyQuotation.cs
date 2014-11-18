using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
    [Serializable]
    public class EnergyQuotation : DomainObj, IEnergyQuotation
    {
    #region Data
        static string iName = "EnergyQuotation";
        int iD;
        decimal prepayAmt;
        int estimatedUsage;
        decimal ratePerKwh;
    #endregion
        
    #region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, iD.ToString()); }
        }
        public int ID
        {
            get { return iD; }
        }
        public decimal PrepayAmt
        {
            get { return prepayAmt; }
            set
            {
                setState();
                prepayAmt = Decimal.Round(value, 2);
            }
        }
        public int EstimatedUsage
        {
            get { return estimatedUsage; }
            set
            {
                setState();
                estimatedUsage = value;
            }
        }
        public decimal RatePerKwh
        {
            get { return ratePerKwh; }
            set
            {
                setState();
                ratePerKwh = Decimal.Round(value, 2);
            }
        }
    #endregion
        
    #region Constructors
        public EnergyQuotation()
        {
            sql = new EnergyQuotationSQL();
            iD = random.Next(Int32.MinValue, -1);
			this.priority = 2300;
            rowState = RowState.New;
        }
        public EnergyQuotation(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
    #endregion
        
    #region Methods
        protected override SqlGateway loadSql()
        {
            return new EnergyQuotationSQL();
        }
        public override void checkExists()
        {
            if ((ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
    #endregion

    #region	Static methods
        public static EnergyQuotation find(UOW uow, int iD)
        {
            if (uow.Imap.keyExists(EnergyQuotation.getKey(iD)))
                return (EnergyQuotation)uow.Imap.find(EnergyQuotation.getKey(iD));
            
            EnergyQuotation cls = new EnergyQuotation();
            cls.uow = uow;
            cls.iD = iD;
            cls = (EnergyQuotation)DomainObj.addToIMap(uow, getOne(((EnergyQuotationSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static EnergyQuotation[] getAll(UOW uow)
        {
            EnergyQuotation[] objs = (EnergyQuotation[])DomainObj.addToIMap(uow, (new EnergyQuotationSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int iD)
        {
            return new Key(iName, iD.ToString());
        }
    #endregion

    #region Implementation
        static EnergyQuotation getOne(EnergyQuotation[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(EnergyQuotation src, EnergyQuotation tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.iD = src.iD;
            tar.prepayAmt = src.prepayAmt;
            tar.estimatedUsage = src.estimatedUsage;
            tar.ratePerKwh = src.ratePerKwh;
            tar.rowState = src.rowState;
        }
    #endregion
 
    #region	SQL
        [Serializable]
        class EnergyQuotationSQL : SqlGateway
        {
            public EnergyQuotation[] getKey(EnergyQuotation rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyQuotation_Get_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                EnergyQuotation rec = (EnergyQuotation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyQuotation_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.iD = (int)cmd.Parameters["@ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                EnergyQuotation rec = (EnergyQuotation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyQuotation_Del_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                EnergyQuotation rec = (EnergyQuotation)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyQuotation_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public EnergyQuotation[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spEnergyQuotation_Get_All";
                return convert(execReader(cmd));
            }
        #region Implementation
            void setParam(SqlCommand cmd, EnergyQuotation rec)
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                cmd.Parameters.Add("@PrepayAmt", SqlDbType.Decimal, 0).Value = rec.prepayAmt;
                cmd.Parameters.Add("@EstimatedUsage", SqlDbType.Int, 0).Value = rec.estimatedUsage;
                cmd.Parameters.Add("@RatePerKwh", SqlDbType.Decimal, 0).Value = rec.ratePerKwh;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                EnergyQuotation rec = new EnergyQuotation();
                
                if (rdr["ID"] != DBNull.Value)
                    rec.iD = (int) rdr["ID"];
 
                if (rdr["PrepayAmt"] != DBNull.Value)
                    rec.prepayAmt = Decimal.Round((decimal)rdr["PrepayAmt"], 2);
 
                if (rdr["EstimatedUsage"] != DBNull.Value)
                    rec.estimatedUsage = (int) rdr["EstimatedUsage"];
 
                if (rdr["RatePerKwh"] != DBNull.Value)
                    rec.ratePerKwh = Decimal.Round((decimal)rdr["RatePerKwh"], 2);
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            EnergyQuotation[] convert(DomainObj[] objs)
            {
                EnergyQuotation[] acls  = new EnergyQuotation[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        #endregion
        }
    #endregion
    }
}
