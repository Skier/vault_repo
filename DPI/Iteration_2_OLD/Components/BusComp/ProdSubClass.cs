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
    public class ProdSubClass : DomainObj
    {
        /*        Data        */
        static string iName = "ProdSubClass";
        string prodSubClass;
        string prodCode;
        string pymtAllocSeq;
        bool isInstallForEachInstance;
        string notes;
        string provHints;
        bool isRestrictedToOneInstance;
        string fulfillMethod;
        bool isFulfillRecurring;
		bool suppressZeroPrice;
		bool suppressOnWebReceipt;
		bool selectionUnselectsCurrent;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, prodSubClass); }
        }
        public string SubClass
        {
            get { return prodSubClass; }
            set
            {
                setState();
                prodSubClass = value;
            }
        }
        public string ProdCode
        {
            get { return prodCode; }
            set
            {
                setState();
                prodCode = value;
            }
        }
        public string PymtAllocSeq
        {
            get { return pymtAllocSeq; }
            set
            {
                setState();
                pymtAllocSeq = value;
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
        public string Notes
        {
            get { return notes; }
            set
            {
                setState();
                notes = value;
            }
        }
        public string ProvHints
        {
            get { return provHints; }
            set
            {
                setState();
                provHints = value;
            }
        }
        public bool IsRestrictedToOneInstance
        {
            get { return isRestrictedToOneInstance; }
            set
            {
                setState();
                isRestrictedToOneInstance = value;
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
		public bool IsFulfillRecurring
		{
			get { return isFulfillRecurring; }
			set
			{
				setState();
				isFulfillRecurring = value;
			}
		}
		public bool SuppressZeroPrice
		{
			get { return suppressZeroPrice; }
			set
			{
				setState();
				suppressZeroPrice = value;
			}
		}
		public bool SuppressOnWebReceipt
		{
			get { return suppressOnWebReceipt; }
			set
			{
				setState();
				suppressOnWebReceipt = value;
			}
		}
		public bool SelectionUnselectsCurrent
		{
			get { return selectionUnselectsCurrent; }
			set
			{
				setState();
				selectionUnselectsCurrent = value;
			}
		}
        
        /*        Constructors			*/
        public ProdSubClass()
        {
            sql = new ProdSubClassSQL();
            rowState = RowState.New;
        }
        public ProdSubClass(UOW uow) : this()
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
            return new ProdSubClassSQL();
        }
        public override void checkExists()
        {
            if ((SubClass ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ProdSubClass find(UOW uow, string prodSubClass)
        {
            if (uow.Imap.keyExists(ProdSubClass.getKey(prodSubClass)))
                return (ProdSubClass)uow.Imap.find(ProdSubClass.getKey(prodSubClass));
            
            ProdSubClass cls = new ProdSubClass();
            cls.uow = uow;
            cls.prodSubClass = prodSubClass;
            cls = (ProdSubClass)DomainObj.addToIMap(uow, getOne(((ProdSubClassSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ProdSubClass[] getAll(UOW uow)
        {
            ProdSubClass[] objs = (ProdSubClass[])DomainObj.addToIMap(uow, (new ProdSubClassSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string prodSubClass)
        {
            return new Key(iName, prodSubClass.ToString());
        }
        /*		Implementation		*/
        static ProdSubClass getOne(ProdSubClass[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ProdSubClass src, ProdSubClass tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.prodSubClass              = src.prodSubClass;
            tar.prodCode                  = src.prodCode;
            tar.pymtAllocSeq              = src.pymtAllocSeq;
            tar.isInstallForEachInstance  = src.isInstallForEachInstance;
            tar.notes                     = src.notes;
            tar.provHints                 = src.provHints;
            tar.isRestrictedToOneInstance = src.isRestrictedToOneInstance;
            tar.fulfillMethod             = src.fulfillMethod;
            tar.isFulfillRecurring        = src.isFulfillRecurring;
            tar.rowState                  = src.rowState;
			tar.suppressZeroPrice         = src.suppressZeroPrice;
			tar.suppressOnWebReceipt      = src.suppressOnWebReceipt;
			tar.selectionUnselectsCurrent = src.selectionUnselectsCurrent;
        }
 
        /*		SQL		*/
        [Serializable]
        class ProdSubClassSQL : SqlGateway
        {
            public ProdSubClass[] getKey(ProdSubClass rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdSubClass_Get_Id";
                cmd.Parameters.Add("@ProdSubClass", SqlDbType.VarChar, 15).Value = rec.prodSubClass;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ProdSubClass rec = (ProdSubClass)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdSubClass_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ProdSubClass rec = (ProdSubClass)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdSubClass_Del_Id";
                cmd.Parameters.Add("@ProdSubClass", SqlDbType.VarChar, 15).Value = rec.prodSubClass;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ProdSubClass rec = (ProdSubClass)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdSubClass_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ProdSubClass[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spProdSubClass_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, ProdSubClass rec)
            {
 
                cmd.Parameters.Add("@ProdSubClass", SqlDbType.VarChar, 15).Value = rec.prodSubClass;
 
                if (rec.prodCode == null)
                    cmd.Parameters.Add("@ProdCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.ProdCode.Length == 0)
                        cmd.Parameters.Add("@ProdCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProdCode", SqlDbType.VarChar, 10).Value = rec.prodCode;
                }
 
                if (rec.pymtAllocSeq == null)
                    cmd.Parameters.Add("@PymtAllocSeq", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.PymtAllocSeq.Length == 0)
                        cmd.Parameters.Add("@PymtAllocSeq", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PymtAllocSeq", SqlDbType.VarChar, 15).Value = rec.pymtAllocSeq;
                }
 
                cmd.Parameters.Add("@IsInstallForEachInstance", SqlDbType.Char, 1).Value = (rec.isInstallForEachInstance == true) ? "T" : "F";
 
                if (rec.notes == null)
                    cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 250).Value = DBNull.Value;
                else
                {
                    if (rec.Notes.Length == 0)
                        cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 250).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Notes", SqlDbType.VarChar, 250).Value = rec.notes;
                }
 
                if (rec.provHints == null)
                    cmd.Parameters.Add("@ProvHints", SqlDbType.VarChar, 500).Value = DBNull.Value;
                else
                {
                    if (rec.ProvHints.Length == 0)
                        cmd.Parameters.Add("@ProvHints", SqlDbType.VarChar, 500).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProvHints", SqlDbType.VarChar, 500).Value = rec.provHints;
                }
 
                cmd.Parameters.Add("@IsRestrictedToOneInstance", SqlDbType.Char, 1).Value = (rec.isRestrictedToOneInstance == true) ? "T" : "F";
 
                if (rec.fulfillMethod == null)
                    cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.FulfillMethod.Length == 0)
                        cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = rec.fulfillMethod;
                }
 
				cmd.Parameters.Add("@IsFulfillRecurring", SqlDbType.Char, 1).Value = (rec.isFulfillRecurring == true) ? "T" : "F";
				cmd.Parameters.Add("@SuppressZeroPrice", SqlDbType.Char, 1).Value = (rec.suppressZeroPrice == true) ? "T" : "F";
				cmd.Parameters.Add("@SuppressOnWebReceipt", SqlDbType.Char, 1).Value = (rec.suppressOnWebReceipt == true) ? "T" : "F";
				cmd.Parameters.Add("@SelectionUnselectsCurrent", SqlDbType.Bit).Value = rec.selectionUnselectsCurrent;

            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ProdSubClass rec = new ProdSubClass();
                
                if (rdr["ProdSubClass"] != DBNull.Value)
                    rec.prodSubClass = (string) rdr["ProdSubClass"];
 
                if (rdr["ProdCode"] != DBNull.Value)
                    rec.prodCode = (string) rdr["ProdCode"];
 
                if (rdr["PymtAllocSeq"] != DBNull.Value)
                    rec.pymtAllocSeq = (string) rdr["PymtAllocSeq"];
 
                if (rdr["IsInstallForEachInstance"] != DBNull.Value)
                    rec.isInstallForEachInstance = (string) rdr["IsInstallForEachInstance"] == "T" ?  true : false;
 
                if (rdr["Notes"] != DBNull.Value)
                    rec.notes = (string) rdr["Notes"];
 
                if (rdr["ProvHints"] != DBNull.Value)
                    rec.provHints = (string) rdr["ProvHints"];
 
                if (rdr["IsRestrictedToOneInstance"] != DBNull.Value)
                    rec.isRestrictedToOneInstance = (string) rdr["IsRestrictedToOneInstance"] == "T" ?  true : false;
 
                if (rdr["FulfillMethod"] != DBNull.Value)
                    rec.fulfillMethod = (string) rdr["FulfillMethod"];
 
				if (rdr["IsFulfillRecurring"] != DBNull.Value)
					rec.isFulfillRecurring = (string) rdr["IsFulfillRecurring"] == "T" ?  true : false;
 
				if (rdr["SuppressZeroPrice"] != DBNull.Value)
					rec.suppressZeroPrice = (string) rdr["SuppressZeroPrice"] == "T" ?  true : false;

				if (rdr["SuppressOnWebReceipt"] != DBNull.Value)
					rec.suppressOnWebReceipt = (string) rdr["SuppressOnWebReceipt"] == "T" ?  true : false;

				if (rdr["SelectionUnselectsCurrent"] != DBNull.Value)
					rec.selectionUnselectsCurrent = (bool)rdr["SelectionUnselectsCurrent"];

				rec.rowState = RowState.Clean;
                return rec;
            }
            ProdSubClass[] convert(DomainObj[] objs)
            {
                ProdSubClass[] acls  = new ProdSubClass[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}