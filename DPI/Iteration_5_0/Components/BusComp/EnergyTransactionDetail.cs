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
    public class EnergyTransactionDetail : DomainObj, IEnergyTransactionDetail
    {
    #region Data
        static string iName = "EnergyTransactionDetail";
        int iD;
        int tranId;
        decimal tranAmt;
        string description;
		IEnergy_Transactions engTran;
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
        public int TranId
        {
            get { return tranId; }
            set
            {
                setState();
                tranId = value;
            }
        }
        public decimal TranAmt
        {
            get { return tranAmt; }
            set
            {
                setState();
                tranAmt = Decimal.Round(value, 2);
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                setState();
                description = value;
            }
        }
		public IEnergy_Transactions EngTran	
		{ 
			get { return engTran;  }
			set	
			{ 
				setState();
				engTran = value; 
			}
		}
    #endregion
        
    #region Constructors
        public EnergyTransactionDetail()
        {
            sql = new EnergyTransactionDetailSQL();
            iD = random.Next(Int32.MinValue, -1);
			this.priority = 2200;
            rowState = RowState.New;
        }
        public EnergyTransactionDetail(UOW uow) : this()
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
            return new EnergyTransactionDetailSQL();
        }
        public override void checkExists()
        {
            if ((ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		public override void RefreshForeignKeys()
		{
		}
		
    #endregion

    #region	Static methods
        public static EnergyTransactionDetail find(UOW uow, int iD)
        {
            if (uow.Imap.keyExists(EnergyTransactionDetail.getKey(iD)))
                return (EnergyTransactionDetail)uow.Imap.find(EnergyTransactionDetail.getKey(iD));
            
            EnergyTransactionDetail cls = new EnergyTransactionDetail();
            cls.uow = uow;
            cls.iD = iD;
            cls = (EnergyTransactionDetail)DomainObj.addToIMap(uow, getOne(((EnergyTransactionDetailSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static EnergyTransactionDetail[] getAll(UOW uow)
        {
            EnergyTransactionDetail[] objs = (EnergyTransactionDetail[])DomainObj.addToIMap(uow, (new EnergyTransactionDetailSQL()).getAll(uow));
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
        static EnergyTransactionDetail getOne(EnergyTransactionDetail[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(EnergyTransactionDetail src, EnergyTransactionDetail tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.iD = src.iD;
            tar.tranId = src.tranId;
            tar.tranAmt = src.tranAmt;
            tar.description = src.description;
            tar.rowState = src.rowState;
        }
    #endregion
 
    #region	SQL
        [Serializable]
        class EnergyTransactionDetailSQL : SqlGateway
        {
            public EnergyTransactionDetail[] getKey(EnergyTransactionDetail rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyTransactionDetail_Get_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                EnergyTransactionDetail rec = (EnergyTransactionDetail)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyTransactionDetail_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.iD = (int)cmd.Parameters["@ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                EnergyTransactionDetail rec = (EnergyTransactionDetail)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyTransactionDetail_Del_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                EnergyTransactionDetail rec = (EnergyTransactionDetail)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergyTransactionDetail_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public EnergyTransactionDetail[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spEnergyTransactionDetail_Get_All";
                return convert(execReader(cmd));
            }
        #region Implementation
            void setParam(SqlCommand cmd, EnergyTransactionDetail rec)
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                
                // Numeric, nullable foreign key treatment:
                if (rec.TranId == 0)
                    cmd.Parameters.Add("@TranId", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@TranId", SqlDbType.Int, 0).Value = rec.tranId;
                cmd.Parameters.Add("@TranAmt", SqlDbType.Decimal, 0).Value = rec.tranAmt;
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = rec.description;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                EnergyTransactionDetail rec = new EnergyTransactionDetail();
                
                if (rdr["ID"] != DBNull.Value)
                    rec.iD = (int) rdr["ID"];
 
                if (rdr["TranId"] != DBNull.Value)
                    rec.tranId = (int) rdr["TranId"];
 
                if (rdr["TranAmt"] != DBNull.Value)
                    rec.tranAmt = Decimal.Round((decimal)rdr["TranAmt"], 2);
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            EnergyTransactionDetail[] convert(DomainObj[] objs)
            {
                EnergyTransactionDetail[] acls  = new EnergyTransactionDetail[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        #endregion
        }
    #endregion
    }
}
