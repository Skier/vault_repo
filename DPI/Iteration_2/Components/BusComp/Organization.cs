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
    public class Organization : DomainObj
    {
        /*        Data        */
        static string iName = "Organization";
        int id;
        string name;
        bool isSupplier;
        bool isVendor;
        int parent;
        string addrLine1;
        string addrLine2;
        string city;
        int state;
        int zip;
        string phone;
        string fax;
        string uRL;
        string contact;
        string connType;
        bool isBillableAllowed;
        bool isILEC;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
            set
            {
                setState();
                name = value;
            }
        }
        public bool IsSupplier
        {
            get { return isSupplier; }
            set
            {
                setState();
                isSupplier = value;
            }
        }
        public bool IsVendor
        {
            get { return isVendor; }
            set
            {
                setState();
                isVendor = value;
            }
        }
        public int Parent
        {
            get { return parent; }
            set
            {
                setState();
                parent = value;
            }
        }
        public string AddrLine1
        {
            get { return addrLine1; }
            set
            {
                setState();
                addrLine1 = value;
            }
        }
        public string AddrLine2
        {
            get { return addrLine2; }
            set
            {
                setState();
                addrLine2 = value;
            }
        }
        public string City
        {
            get { return city; }
            set
            {
                setState();
                city = value;
            }
        }
        public int State
        {
            get { return state; }
            set
            {
                setState();
                state = value;
            }
        }
        public int Zip
        {
            get { return zip; }
            set
            {
                setState();
                zip = value;
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                setState();
                phone = value;
            }
        }
        public string Fax
        {
            get { return fax; }
            set
            {
                setState();
                fax = value;
            }
        }
        public string URL
        {
            get { return uRL; }
            set
            {
                setState();
                uRL = value;
            }
        }
        public string Contact
        {
            get { return contact; }
            set
            {
                setState();
                contact = value;
            }
        }
        public string ConnType
        {
            get { return connType; }
            set
            {
                setState();
                connType = value;
            }
        }
        public bool IsBillableAllowed
        {
            get { return isBillableAllowed; }
            set
            {
                setState();
                isBillableAllowed = value;
            }
        }
        public bool IsILEC
        {
            get { return isILEC; }
            set
            {
                setState();
                isILEC = value;
            }
        }
        
        /*        Constructors			*/
        public Organization()
        {
            sql = new OrganizationSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Organization(UOW uow) : this()
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
            return new OrganizationSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Organization find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(Organization.getKey(id)))
                return (Organization)uow.Imap.find(Organization.getKey(id));
            
            Organization cls = new Organization();
            cls.uow = uow;
            cls.id = id;
            cls = (Organization)DomainObj.addToIMap(uow, getOne(((OrganizationSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Organization[] getAll(UOW uow)
        {
            Organization[] objs = (Organization[])DomainObj.addToIMap(uow, (new OrganizationSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static Organization[] GetILEC_ByCode(UOW uow, string code)
		{
			Organization[] objs = (Organization[])DomainObj.addToIMap(uow, (new OrganizationSQL()).getILEC_ByCode(uow, code));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}

		// (UOW uow, string code)
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static Organization getOne(Organization[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Organization src, Organization tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.name = src.name;
            tar.isSupplier = src.isSupplier;
            tar.isVendor = src.isVendor;
            tar.parent = src.parent;
            tar.addrLine1 = src.addrLine1;
            tar.addrLine2 = src.addrLine2;
            tar.city = src.city;
            tar.state = src.state;
            tar.zip = src.zip;
            tar.phone = src.phone;
            tar.fax = src.fax;
            tar.uRL = src.uRL;
            tar.contact = src.contact;
            tar.connType = src.connType;
            tar.isBillableAllowed = src.isBillableAllowed;
            tar.isILEC = src.isILEC;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class OrganizationSQL : SqlGateway
        {
            public Organization[] getKey(Organization rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spOrganization_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
 
            public override void insert(DomainObj obj)
            {
                Organization rec = (Organization)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spOrganization_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Organization rec = (Organization)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spOrganization_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Organization rec = (Organization)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spOrganization_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Organization[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spOrganization_Get_All";
                return convert(execReader(cmd));
            }
			public Organization[] getILEC_ByCode(UOW uow, string code)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spOrganization_Get_ILEC_BY_Code";
				cmd.Parameters.Add("@Code", SqlDbType.VarChar, 3).Value = code;
				return convert(execReader(cmd));
			}

			//   @Code
            /*        Implementation        */
            void setParam(SqlCommand cmd, Organization rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = rec.name;
 
                cmd.Parameters.Add("@IsSupplier", SqlDbType.Char, 1).Value = (rec.isSupplier == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsVendor", SqlDbType.Char, 1).Value = (rec.isVendor == true) ? "T" : "F";
                cmd.Parameters.Add("@Parent", SqlDbType.Int, 0).Value = rec.parent;
 
                if (rec.addrLine1 == null)
                    cmd.Parameters.Add("@AddrLine1", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.AddrLine1.Length == 0)
                        cmd.Parameters.Add("@AddrLine1", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AddrLine1", SqlDbType.VarChar, 50).Value = rec.addrLine1;
                }
 
                if (rec.addrLine2 == null)
                    cmd.Parameters.Add("@AddrLine2", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.AddrLine2.Length == 0)
                        cmd.Parameters.Add("@AddrLine2", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AddrLine2", SqlDbType.VarChar, 50).Value = rec.addrLine2;
                }
 
                if (rec.city == null)
                    cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.City.Length == 0)
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = rec.city;
                }
                cmd.Parameters.Add("@State", SqlDbType.Int, 0).Value = rec.state;
                cmd.Parameters.Add("@Zip", SqlDbType.Int, 0).Value = rec.zip;
 
                if (rec.phone == null)
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.Phone.Length == 0)
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 20).Value = rec.phone;
                }
 
                if (rec.fax == null)
                    cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.Fax.Length == 0)
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Fax", SqlDbType.VarChar, 15).Value = rec.fax;
                }
 
                if (rec.uRL == null)
                    cmd.Parameters.Add("@URL", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.URL.Length == 0)
                        cmd.Parameters.Add("@URL", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@URL", SqlDbType.VarChar, 50).Value = rec.uRL;
                }
 
                if (rec.contact == null)
                    cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.Contact.Length == 0)
                        cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 20).Value = rec.contact;
                }
 
                if (rec.connType == null)
                    cmd.Parameters.Add("@ConnType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.ConnType.Length == 0)
                        cmd.Parameters.Add("@ConnType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ConnType", SqlDbType.VarChar, 15).Value = rec.connType;
                }
 
                cmd.Parameters.Add("@IsBillableAllowed", SqlDbType.Char, 1).Value = (rec.isBillableAllowed == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsILEC", SqlDbType.Char, 1).Value = (rec.isILEC == true) ? "T" : "F";
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Organization rec = new Organization();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Name"] != DBNull.Value)
                    rec.name = (string) rdr["Name"];
 
                if (rdr["IsSupplier"] != DBNull.Value)
                    rec.isSupplier = (string) rdr["IsSupplier"] == "T" ?  true : false;
 
                if (rdr["IsVendor"] != DBNull.Value)
                    rec.isVendor = (string) rdr["IsVendor"] == "T" ?  true : false;
 
                if (rdr["Parent"] != DBNull.Value)
                    rec.parent = (int) rdr["Parent"];
 
                if (rdr["AddrLine1"] != DBNull.Value)
                    rec.addrLine1 = (string) rdr["AddrLine1"];
 
                if (rdr["AddrLine2"] != DBNull.Value)
                    rec.addrLine2 = (string) rdr["AddrLine2"];
 
                if (rdr["City"] != DBNull.Value)
                    rec.city = (string) rdr["City"];
 
                if (rdr["State"] != DBNull.Value)
                    rec.state = (int) rdr["State"];
 
                if (rdr["Zip"] != DBNull.Value)
                    rec.zip = (int) rdr["Zip"];
 
                if (rdr["Phone"] != DBNull.Value)
                    rec.phone = (string) rdr["Phone"];
 
                if (rdr["Fax"] != DBNull.Value)
                    rec.fax = (string) rdr["Fax"];
 
                if (rdr["URL"] != DBNull.Value)
                    rec.uRL = (string) rdr["URL"];
 
                if (rdr["Contact"] != DBNull.Value)
                    rec.contact = (string) rdr["Contact"];
 
                if (rdr["ConnType"] != DBNull.Value)
                    rec.connType = (string) rdr["ConnType"];
 
                if (rdr["IsBillableAllowed"] != DBNull.Value)
                    rec.isBillableAllowed = (string) rdr["IsBillableAllowed"] == "T" ?  true : false;
 
                if (rdr["IsILEC"] != DBNull.Value)
                    rec.isILEC = (string) rdr["IsILEC"] == "T" ?  true : false;
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Organization[] convert(DomainObj[] objs)
            {
                Organization[] acls  = new Organization[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
