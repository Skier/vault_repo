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
    public class Location_Composition : DomainObj
    {
        /*        Data        */
        static string iName = "Location_Composition";
        int id;
        int loc;
        int subLoc;
        int owner;
        bool isDefault;
        bool isProvisioning;
        bool isDMA;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public int Loc
        {
            get { return loc; }
            set
            {
                setState();
                loc = value;
            }
        }
        public int SubLoc
        {
            get { return subLoc; }
            set
            {
                setState();
                subLoc = value;
            }
        }
        public int Owner
        {
            get { return owner; }
            set
            {
                setState();
                owner = value;
            }
        }
        public bool IsDefault
        {
            get { return isDefault; }
            set
            {
                setState();
                isDefault = value;
            }
        }
        public bool IsProvisioning
        {
            get { return isProvisioning; }
            set
            {
                setState();
                isProvisioning = value;
            }
        }
        public bool IsDMA
        {
            get { return isDMA; }
            set
            {
                setState();
                isDMA = value;
            }
        }
        
        /*        Constructors			*/
        public Location_Composition()
        {
            sql = new Location_CompositionSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Location_Composition(UOW uow) : this()
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
            return new Location_CompositionSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Location_Composition find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(Location_Composition.getKey(id)))
                return (Location_Composition)uow.Imap.find(Location_Composition.getKey(id));
            
            Location_Composition cls = new Location_Composition();
            cls.uow = uow;
            cls.id = id;
            cls = (Location_Composition)DomainObj.addToIMap(uow, getOne(((Location_CompositionSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Location_Composition[] getAll(UOW uow)
        {
            Location_Composition[] objs = (Location_Composition[])DomainObj.addToIMap(uow, (new Location_CompositionSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static Location_Composition getOne(Location_Composition[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Location_Composition src, Location_Composition tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.loc = src.loc;
            tar.subLoc = src.subLoc;
            tar.owner = src.owner;
            tar.isDefault = src.isDefault;
            tar.isProvisioning = src.isProvisioning;
            tar.isDMA = src.isDMA;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class Location_CompositionSQL : SqlGateway
        {
            public Location_Composition[] getKey(Location_Composition rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Composition_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Location_Composition rec = (Location_Composition)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Composition_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Location_Composition rec = (Location_Composition)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Composition_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Location_Composition rec = (Location_Composition)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Composition_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Location_Composition[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spLocation_Composition_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Location_Composition rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Loc == 0)
                    cmd.Parameters.Add("@Loc", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Loc", SqlDbType.Int, 0).Value = rec.loc;
                
                // Numeric, nullable foreign key treatment:
                if (rec.SubLoc == 0)
                    cmd.Parameters.Add("@SubLoc", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@SubLoc", SqlDbType.Int, 0).Value = rec.subLoc;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Owner == 0)
                    cmd.Parameters.Add("@Owner", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Owner", SqlDbType.Int, 0).Value = rec.owner;
 
                cmd.Parameters.Add("@IsDefault", SqlDbType.Char, 1).Value = (rec.isDefault == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsProvisioning", SqlDbType.Char, 1).Value = (rec.isProvisioning == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsDMA", SqlDbType.Char, 1).Value = (rec.isDMA == true) ? "T" : "F";
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Location_Composition rec = new Location_Composition();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Loc"] != DBNull.Value)
                    rec.loc = (int) rdr["Loc"];
 
                if (rdr["SubLoc"] != DBNull.Value)
                    rec.subLoc = (int) rdr["SubLoc"];
 
                if (rdr["Owner"] != DBNull.Value)
                    rec.owner = (int) rdr["Owner"];
 
                if (rdr["IsDefault"] != DBNull.Value)
                    rec.isDefault = (string) rdr["IsDefault"] == "T" ?  true : false;
 
                if (rdr["IsProvisioning"] != DBNull.Value)
                    rec.isProvisioning = (string) rdr["IsProvisioning"] == "T" ?  true : false;
 
                if (rdr["IsDMA"] != DBNull.Value)
                    rec.isDMA = (string) rdr["IsDMA"] == "T" ?  true : false;
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Location_Composition[] convert(DomainObj[] objs)
            {
                Location_Composition[] acls  = new Location_Composition[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
