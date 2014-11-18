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
    public class ProdType : DomainObj, IProdType
    {
        /*        Data        */
        static string iName = "ProdType";
        string prodType;
        string prodClass;
        string description;
        bool isInstallForEachInstance;
        int orderDisplaySeq;
        bool isFee;
        bool isPrompPayDisc;
        bool isListed;
        string allowsSubcomps;
        string fulfillMethod;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, prodType); }
        }
        public string PrdType
        {
            get { return prodType; }
            set
            {
                setState();
                prodType = value;
            }
        }
        public string ProdClass
        {
            get { return prodClass; }
            set
            {
                setState();
                prodClass = value;
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
        public bool IsInstallForEachInstance
        {
            get { return isInstallForEachInstance; }
            set
            {
                setState();
                isInstallForEachInstance = value;
            }
        }
        public int OrderDisplaySeq
        {
            get { return orderDisplaySeq; }
            set
            {
                setState();
                orderDisplaySeq = value;
            }
        }
        public bool IsFee
        {
            get { return isFee; }
            set
            {
                setState();
                isFee = value;
            }
        }
        public bool IsPrompPayDisc
        {
            get { return isPrompPayDisc; }
            set
            {
                setState();
                isPrompPayDisc = value;
            }
        }
        public bool IsListed
        {
            get { return isListed; }
            set
            {
                setState();
                isListed = value;
            }
        }
        public string AllowsSubcomps
        {
            get { return allowsSubcomps; }
            set
            {
                setState();
                allowsSubcomps = value;
            }
        }
        public string FulfillMethod
        {
            get { return fulfillMethod; }
            set
            {
                setState();
                fulfillMethod = value;
            }
        }
        
        /*        Constructors			*/
        public ProdType()
        {
            sql = new ProdTypeSQL();
            rowState = RowState.New;
        }
        public ProdType(UOW uow) : this()
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
            return new ProdTypeSQL();
        }
        public override void checkExists()
        {
            if ((PrdType ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ProdType find(UOW uow, string prodType)
        {
            if (uow.Imap.keyExists(ProdType.getKey(prodType)))
                return (ProdType)uow.Imap.find(ProdType.getKey(prodType));
            
            ProdType cls = new ProdType();
            cls.uow = uow;
            cls.prodType = prodType;
            cls = (ProdType)DomainObj.addToIMap(uow, getOne(((ProdTypeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ProdType[] getAll(UOW uow)
        {
            ProdType[] objs = (ProdType[])DomainObj.addToIMap(uow, (new ProdTypeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string prodType)
        {
            return new Key(iName, prodType.ToString());
        }
        /*		Implementation		*/
        static ProdType getOne(ProdType[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ProdType src, ProdType tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.prodType = src.prodType;
            tar.prodClass = src.prodClass;
            tar.description = src.description;
            tar.isInstallForEachInstance = src.isInstallForEachInstance;
            tar.orderDisplaySeq = src.orderDisplaySeq;
            tar.isFee = src.isFee;
            tar.isPrompPayDisc = src.isPrompPayDisc;
            tar.isListed = src.isListed;
            tar.allowsSubcomps = src.allowsSubcomps;
            tar.fulfillMethod = src.fulfillMethod;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class ProdTypeSQL : SqlGateway
        {
            public ProdType[] getKey(ProdType rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdType_Get_Id";
                cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 15).Value = rec.prodType;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ProdType rec = (ProdType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdType_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ProdType rec = (ProdType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdType_Del_Id";
                cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 15).Value = rec.prodType;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ProdType rec = (ProdType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdType_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ProdType[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spProdType_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, ProdType rec)
            {
 
                cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 15).Value = rec.prodType;
 
                if (rec.prodClass == null)
                    cmd.Parameters.Add("@ProdClass", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.ProdClass.Length == 0)
                        cmd.Parameters.Add("@ProdClass", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProdClass", SqlDbType.VarChar, 10).Value = rec.prodClass;
                }
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = rec.description;
                }
 
                cmd.Parameters.Add("@IsInstallForEachInstance", SqlDbType.Char, 1).Value = (rec.isInstallForEachInstance == true) ? "T" : "F";
                cmd.Parameters.Add("@OrderDisplaySeq", SqlDbType.Int, 0).Value = rec.orderDisplaySeq;
 
                cmd.Parameters.Add("@IsFee", SqlDbType.Char, 1).Value = (rec.isFee == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsPrompPayDisc", SqlDbType.Char, 1).Value = (rec.isPrompPayDisc == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsListed", SqlDbType.Char, 1).Value = (rec.isListed == true) ? "T" : "F";
 
                if (rec.allowsSubcomps == null)
                    cmd.Parameters.Add("@AllowsSubcomps", SqlDbType.VarChar, 1).Value = DBNull.Value;
                else
                {
                    if (rec.AllowsSubcomps.Length == 0)
                        cmd.Parameters.Add("@AllowsSubcomps", SqlDbType.VarChar, 1).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AllowsSubcomps", SqlDbType.VarChar, 1).Value = rec.allowsSubcomps;
                }
 
                if (rec.fulfillMethod == null)
                    cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.FulfillMethod.Length == 0)
                        cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = rec.fulfillMethod;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ProdType rec = new ProdType();
                
                if (rdr["ProdType"] != DBNull.Value)
                    rec.prodType = (string) rdr["ProdType"];
 
                if (rdr["ProdClass"] != DBNull.Value)
                    rec.prodClass = (string) rdr["ProdClass"];
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                if (rdr["IsInstallForEachInstance"] != DBNull.Value)
                    rec.isInstallForEachInstance = (string) rdr["IsInstallForEachInstance"] == "T" ?  true : false;
 
                if (rdr["OrderDisplaySeq"] != DBNull.Value)
                    rec.orderDisplaySeq = (int) rdr["OrderDisplaySeq"];
 
                if (rdr["IsFee"] != DBNull.Value)
                    rec.isFee = (string) rdr["IsFee"] == "T" ?  true : false;
 
                if (rdr["IsPrompPayDisc"] != DBNull.Value)
                    rec.isPrompPayDisc = (string) rdr["IsPrompPayDisc"] == "T" ?  true : false;
 
                if (rdr["IsListed"] != DBNull.Value)
                    rec.isListed = (string) rdr["IsListed"] == "T" ?  true : false;
 
                if (rdr["AllowsSubcomps"] != DBNull.Value)
                    rec.allowsSubcomps = (string) rdr["AllowsSubcomps"];
 
                if (rdr["FulfillMethod"] != DBNull.Value)
                    rec.fulfillMethod = (string) rdr["FulfillMethod"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            ProdType[] convert(DomainObj[] objs)
            {
                ProdType[] acls  = new ProdType[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
