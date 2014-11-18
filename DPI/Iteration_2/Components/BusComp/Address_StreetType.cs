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
    public class Address_StreetType : DomainObj, IAddress_StreetType
    {
        /*        Data        */
        static string iName = "Address_StreetType";
        int address_StreetType_ID;
        string streetType;
        string streetTypeAbbr;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, address_StreetType_ID.ToString()); }
        }
        public int Address_StreetType_ID
        {
            get { return address_StreetType_ID; }
        }
        public string StreetType
        {
            get { return streetType; }
            set
            {
                setState();
                streetType = value;
            }
        }
        public string StreetTypeAbbr
        {
            get { return streetTypeAbbr; }
            set
            {
                setState();
                streetTypeAbbr = value;
            }
        }
        
        /*        Constructors			*/
        public Address_StreetType()
        {
            sql = new Address_StreetTypeSQL();
            address_StreetType_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Address_StreetType(UOW uow) : this()
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
            return new Address_StreetTypeSQL();
        }
        public override void checkExists()
        {
            if ((Address_StreetType_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Address_StreetType find(UOW uow, int address_StreetType_ID)
        {
            if (uow.Imap.keyExists(Address_StreetType.getKey(address_StreetType_ID)))
                return (Address_StreetType)uow.Imap.find(Address_StreetType.getKey(address_StreetType_ID));
            
            Address_StreetType cls = new Address_StreetType();
            cls.uow = uow;
            cls.address_StreetType_ID = address_StreetType_ID;
            cls = (Address_StreetType)DomainObj.addToIMap(uow, getOne(((Address_StreetTypeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Address_StreetType[] getAll(UOW uow)
        {
            Address_StreetType[] objs = (Address_StreetType[])DomainObj.addToIMap(uow, (new Address_StreetTypeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int address_StreetType_ID)
        {
            return new Key(iName, address_StreetType_ID.ToString());
        }
        /*		Implementation		*/
        static Address_StreetType getOne(Address_StreetType[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Address_StreetType src, Address_StreetType tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.address_StreetType_ID = src.address_StreetType_ID;
            tar.streetType = src.streetType;
            tar.streetTypeAbbr = src.streetTypeAbbr;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class Address_StreetTypeSQL : SqlGateway
        {
            public Address_StreetType[] getKey(Address_StreetType rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAddress_StreetType_Get_Id";
                cmd.Parameters.Add("@Address_StreetType_ID", SqlDbType.Int, 0).Value = rec.address_StreetType_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Address_StreetType rec = (Address_StreetType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAddress_StreetType_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Address_StreetType_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.address_StreetType_ID = (int)cmd.Parameters["@Address_StreetType_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Address_StreetType rec = (Address_StreetType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAddress_StreetType_Del_Id";
                cmd.Parameters.Add("@Address_StreetType_ID", SqlDbType.Int, 0).Value = rec.address_StreetType_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Address_StreetType rec = (Address_StreetType)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAddress_StreetType_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Address_StreetType[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAddress_StreetType_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Address_StreetType rec)
            {
                cmd.Parameters.Add("@Address_StreetType_ID", SqlDbType.Int, 0).Value = rec.address_StreetType_ID;
 
                cmd.Parameters.Add("@StreetType", SqlDbType.VarChar, 20).Value = rec.streetType;
 
                cmd.Parameters.Add("@StreetTypeAbbr", SqlDbType.VarChar, 10).Value = rec.streetTypeAbbr;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Address_StreetType rec = new Address_StreetType();
                
                if (rdr["Address_StreetType_ID"] != DBNull.Value)
                    rec.address_StreetType_ID = (int) rdr["Address_StreetType_ID"];
 
                if (rdr["StreetType"] != DBNull.Value)
                    rec.streetType = (string) rdr["StreetType"];
 
                if (rdr["StreetTypeAbbr"] != DBNull.Value)
                    rec.streetTypeAbbr = (string) rdr["StreetTypeAbbr"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Address_StreetType[] convert(DomainObj[] objs)
            {
                Address_StreetType[] acls  = new Address_StreetType[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
