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
    public class Location : DomainObj
    {
		#region Data
        static string iName = "Location";
        int locId;
        string locType;
        int excLocOwner;
        string name;
        string locDesc;
        string lSR_Rule;
        string dPI_Resale_OCN;
        string dPI_UNEP_OCN;
		#endregion
        
		#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, locId.ToString()); }
        }
        public int LocId
        {
            get { return locId; }
        }
        public string LocType
        {
            get { return locType; }
            set
            {
                setState();
                locType = value;
            }
        }
        public int ExcLocOwner
        {
            get { return excLocOwner; }
            set
            {
                setState();
                excLocOwner = value;
            }
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
        public string LocDesc
        {
            get { return locDesc; }
            set
            {
                setState();
                locDesc = value;
            }
        }
        public string LSR_Rule
        {
            get { return lSR_Rule; }
            set
            {
                setState();
                lSR_Rule = value;
            }
        }
        public string DPI_Resale_OCN
        {
            get { return dPI_Resale_OCN; }
            set
            {
                setState();
                dPI_Resale_OCN = value;
            }
        }
        public string DPI_UNEP_OCN
        {
            get { return dPI_UNEP_OCN; }
            set
            {
                setState();
                dPI_UNEP_OCN = value;
            }
        }
		#endregion
        
		#region Constructors
        public Location()
        {
            sql = new LocationSQL();
            locId = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Location(UOW uow) : this()
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
            return new LocationSQL();
        }
        public override void checkExists()
        {
            if ((LocId < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		#endregion

		#region	Static methods
        public static Location find(UOW uow, int locId)
        {
            if (uow.Imap.keyExists(Location.getKey(locId)))
                return (Location)uow.Imap.find(Location.getKey(locId));
            
            Location cls = new Location();
            cls.uow = uow;
            cls.locId = locId;
            cls = (Location)DomainObj.addToIMap(uow, getOne(((LocationSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static Location find(UOW uow, string name)
		{
			Location cls = new Location();
			cls.uow = uow;
			cls = getOne(((LocationSQL)cls.Sql).getByName(cls, name));
			cls.uow = uow;
			return cls;
		}
		public static Location[] find(UOW uow, string zip, int ilec)
		{
			Location[] objs = (Location[])DomainObj.addToIMap(uow, (new LocationSQL()).GetProvLoc(uow, zip, ilec));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Location[] getAll(UOW uow)
        {
            Location[] objs = (Location[])DomainObj.addToIMap(uow, (new LocationSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int locId)
        {
            return new Key(iName, locId.ToString());
        }
		#endregion

#region Implementation
        static Location getOne(Location[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Location is not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Location src, Location tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.locId = src.locId;
            tar.locType = src.locType;
            tar.excLocOwner = src.excLocOwner;
            tar.name = src.name;
            tar.locDesc = src.locDesc;
            tar.lSR_Rule = src.lSR_Rule;
            tar.dPI_Resale_OCN = src.dPI_Resale_OCN;
            tar.dPI_UNEP_OCN = src.dPI_UNEP_OCN;
            tar.rowState = src.rowState;
        }
 
		#endregion

#region SQL
        [Serializable]
        class LocationSQL : SqlGateway
        {
            public Location[] getKey(Location rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Get_Id";
                cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = rec.locId;
                return convert(execReader(cmd));
            }
			public Location[] getByName(Location rec, string name)
			{
				SqlCommand cmd = getCommand(rec); 
				cmd.CommandText = "spLocation_Get_ByName";
				cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = name;
				return convert(execReader(cmd));
			}
			public Location[] GetProvLoc(UOW uow, string zip, int ilec)
			{
				SqlCommand cmd = makeCommand(uow); 
				cmd.CommandText = "dbo.spLocation_Get_ProvLoc";
				cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar, 5).Value = zip;
				cmd.Parameters.Add("@Ilec", SqlDbType.Int, 4).Value = ilec;
				return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                Location rec = (Location)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@LocId"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.locId = (int)cmd.Parameters["@LocId"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Location rec = (Location)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Del_Id";
                cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = rec.locId;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Location rec = (Location)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLocation_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Location[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spLocation_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Location rec)
            {
                cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = rec.locId;
 
                cmd.Parameters.Add("@LocType", SqlDbType.VarChar, 15).Value = rec.locType;
                
                // Numeric, nullable foreign key treatment:
                if (rec.ExcLocOwner == 0)
                    cmd.Parameters.Add("@ExcLocOwner", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ExcLocOwner", SqlDbType.Int, 0).Value = rec.excLocOwner;
 
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = rec.name;
 
                if (rec.locDesc == null)
                    cmd.Parameters.Add("@LocDesc", SqlDbType.VarChar, 200).Value = DBNull.Value;
                else
                {
                    if (rec.LocDesc.Length == 0)
                        cmd.Parameters.Add("@LocDesc", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@LocDesc", SqlDbType.VarChar, 200).Value = rec.locDesc;
                }
 
                if (rec.lSR_Rule == null)
                    cmd.Parameters.Add("@LSR_Rule", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.LSR_Rule.Length == 0)
                        cmd.Parameters.Add("@LSR_Rule", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@LSR_Rule", SqlDbType.VarChar, 15).Value = rec.lSR_Rule;
                }
 
                if (rec.dPI_Resale_OCN == null)
                    cmd.Parameters.Add("@DPI_Resale_OCN", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.DPI_Resale_OCN.Length == 0)
                        cmd.Parameters.Add("@DPI_Resale_OCN", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@DPI_Resale_OCN", SqlDbType.VarChar, 50).Value = rec.dPI_Resale_OCN;
                }
 
                if (rec.dPI_UNEP_OCN == null)
                    cmd.Parameters.Add("@DPI_UNEP_OCN", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.DPI_UNEP_OCN.Length == 0)
                        cmd.Parameters.Add("@DPI_UNEP_OCN", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@DPI_UNEP_OCN", SqlDbType.VarChar, 50).Value = rec.dPI_UNEP_OCN;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Location rec = new Location();
                
                if (rdr["LocId"] != DBNull.Value)
                    rec.locId = (int) rdr["LocId"];
 
                if (rdr["LocType"] != DBNull.Value)
                    rec.locType = (string) rdr["LocType"];
 
                if (rdr["ExcLocOwner"] != DBNull.Value)
                    rec.excLocOwner = (int) rdr["ExcLocOwner"];
 
                if (rdr["Name"] != DBNull.Value)
                    rec.name = (string) rdr["Name"];
 
                if (rdr["LocDesc"] != DBNull.Value)
                    rec.locDesc = (string) rdr["LocDesc"];
 
                if (rdr["LSR_Rule"] != DBNull.Value)
                    rec.lSR_Rule = (string) rdr["LSR_Rule"];
 
                if (rdr["DPI_Resale_OCN"] != DBNull.Value)
                    rec.dPI_Resale_OCN = (string) rdr["DPI_Resale_OCN"];
 
                if (rdr["DPI_UNEP_OCN"] != DBNull.Value)
                    rec.dPI_UNEP_OCN = (string) rdr["DPI_UNEP_OCN"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Location[] convert(DomainObj[] objs)
            {
                Location[] acls  = new Location[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
			
        }
		#endregion

    }
}
