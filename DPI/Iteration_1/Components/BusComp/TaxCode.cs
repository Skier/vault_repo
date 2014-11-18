using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;
 
namespace DPI.Components
{
    [Serializable]
    public class TaxCode : DomainObj, ITaxCode
    {
        /*        Data        */
        static string iName = "TaxCode";
        string taxCode;
        string description;
        int billSoftTran;
        int billSoftServ;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, taxCode); }
        }
        public string TxCode
        {
            get { return taxCode; }
            set
            {
                setState();
                taxCode = value;
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
        public int BillSoftTran
        {
            get { return billSoftTran; }
            set
            {
                setState();
                billSoftTran = value;
            }
        }
        public int BillSoftServ
        {
            get { return billSoftServ; }
            set
            {
                setState();
                billSoftServ = value;
            }
        }
        
        /*        Constructors			*/
        public TaxCode()
        {
            sql = new TaxCodeSQL();
            rowState = RowState.New;
        }
        public TaxCode(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
        
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new TaxCodeSQL();
        }
        public override void checkExists()
        {
            if ((TxCode ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static TaxCode find(UOW uow, string taxCode)
        {
            if (uow.Imap.keyExists(TaxCode.getKey(taxCode)))
                return (TaxCode)uow.Imap.find(TaxCode.getKey(taxCode));
            
            TaxCode cls = new TaxCode();
            cls.uow = uow;
            cls.taxCode = taxCode;
            cls = (TaxCode)DomainObj.addToIMap(uow, getOne(((TaxCodeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static TaxCode[] getAll(UOW uow)
        {
            TaxCode[] objs = (TaxCode[])DomainObj.addToIMap(uow, (new TaxCodeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string taxCode)
        {
            return new Key(iName, taxCode.ToString());
        }
        /*		Implementation		*/
        static TaxCode getOne(TaxCode[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(TaxCode src, TaxCode tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.taxCode = src.taxCode;
            tar.description = src.description;
            tar.billSoftTran = src.billSoftTran;
            tar.billSoftServ = src.billSoftServ;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class TaxCodeSQL : SqlGateway
        {
            public TaxCode[] getKey(TaxCode rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTaxCode_Get_Id";
                cmd.Parameters.Add("@TaxCode", SqlDbType.Char, 2).Value = rec.taxCode;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                TaxCode rec = (TaxCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTaxCode_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                TaxCode rec = (TaxCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTaxCode_Del_Id";
                cmd.Parameters.Add("@TaxCode", SqlDbType.Char, 2).Value = rec.taxCode;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                TaxCode rec = (TaxCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spTaxCode_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public TaxCode[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spTaxCode_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, TaxCode rec)
            {
 
                cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = rec.taxCode;
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100).Value = rec.description;
                }

                cmd.Parameters.Add("@billSoftTran", SqlDbType.Int, 0).Value = rec.billSoftTran;
                cmd.Parameters.Add("@BillSoftServ", SqlDbType.Int, 0).Value = rec.billSoftServ;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                TaxCode rec = new TaxCode();
                
                if (rdr["TaxCode"] != DBNull.Value)
                    rec.taxCode = (string) rdr["TaxCode"];
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                if (rdr["billSoftTran"] != DBNull.Value)
                    rec.billSoftTran = (int) rdr["billSoftTran"];
 
                if (rdr["BillSoftServ"] != DBNull.Value)
                    rec.billSoftServ = (int) rdr["BillSoftServ"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            TaxCode[] convert(DomainObj[] objs)
            {
                TaxCode[] acls  = new TaxCode[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
