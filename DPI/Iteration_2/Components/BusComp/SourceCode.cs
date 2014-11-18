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
    public class SourceCode : DomainObj, ISource
    {
        /*        Data        */
        static string iName = "SourceCode";
        int id;
        int sortOrder;
        string source;
        string description;
        string status;
        DateTime startDate;
        DateTime endDate;
        
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
			get { 
				if (source == null)
					return "";

				if (source.Trim().Length == 0)
					return "";
				
				if (description == null)
					return "";

				if (description.Trim().Length == 0)
					return "";

				return source + " -- " + description; }
		}
        public int SortOrder
        {
            get { return sortOrder; }
            set
            {
                setState();
                sortOrder = value;
            }
        }
        public string Source
        {
            get { return source; }
            set
            {
                setState();
                source = value;
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
        public string Status
        {
            get { return status; }
            set
            {
                setState();
                status = value;
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                setState();
                startDate = value;
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                setState();
                endDate = value;
            }
        }
        
        /*        Constructors			*/
        public SourceCode()
        {
            sql = new SourceCodeSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public SourceCode(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
        
        /*        Methods        */
		public override string ToString()
		{
			return Name;
		}
        protected override SqlGateway loadSql()
        {
            return new SourceCodeSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static SourceCode find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(SourceCode.getKey(id)))
                return (SourceCode)uow.Imap.find(SourceCode.getKey(id));
            
            SourceCode cls = new SourceCode();
            cls.uow = uow;
            cls.id = id;
            cls = (SourceCode)DomainObj.addToIMap(uow, getOne(((SourceCodeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static SourceCode[] getAll(UOW uow)
        {
            SourceCode[] objs = (SourceCode[])DomainObj.addToIMap(uow, (new SourceCodeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static ISource[] getActive(UOW uow)
		{
			SourceCode[] objs = (SourceCode[])DomainObj.addToIMap(uow, (new SourceCodeSQL()).getActive(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }	
		public static ISource[] AddEmpty(ISource[] sources)
		{
			if (sources.Length == 0)
				return sources;

			SourceCode[] res = new SourceCode[sources.Length + 1];
			Array.Copy(sources, 0, res, 1, sources.Length);			
			
			res[0]  = new SourceCode(); // add empty object
			return res;
		}
        /*		Implementation		*/

		static string[] ConvertToString(SourceCode[] objs, bool withDescr)
		{
			ArrayList ar = new ArrayList(objs.Length);

			for (int i = 0; i < objs.Length; i++)
			{
				if (objs[i].Source == null)
					continue;

				if (objs[i].Source.Trim().Length == 0)
					continue;

				if (withDescr)
				{
					ar.Add(objs[i].Source + " -- " + objs[i].Description);
					continue;
				}

				ar.Add(objs[i].Source);
			}

			if (ar.Count == 0)
				return new string[0];

			string[] names = new string[ar.Count];
			ar.CopyTo(names);
			return names;
		}
        static SourceCode getOne(SourceCode[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(SourceCode src, SourceCode tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.sortOrder = src.sortOrder;
            tar.source = src.source;
            tar.description = src.description;
            tar.status = src.status;
            tar.startDate = src.startDate;
            tar.endDate = src.endDate;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class SourceCodeSQL : SqlGateway
        {
            public SourceCode[] getKey(SourceCode rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spSourceCode_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                SourceCode rec = (SourceCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spSourceCode_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                SourceCode rec = (SourceCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spSourceCode_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                SourceCode rec = (SourceCode)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spSourceCode_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public SourceCode[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spSourceCode_Get_All";
                return convert(execReader(cmd));
            }
			public SourceCode[] getActive(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spSourceCode_Get_Active";
				return convert(execReader(cmd));
			}
            /*        Implementation        */
            void setParam(SqlCommand cmd, SourceCode rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                cmd.Parameters.Add("@SortOrder", SqlDbType.Int, 0).Value = rec.sortOrder;
 
                cmd.Parameters.Add("@Source", SqlDbType.VarChar, 25).Value = rec.source;
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = rec.description;
                }
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }
 
                if (rec.startDate == DateTime.MinValue)
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
                if (rec.endDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                SourceCode rec = new SourceCode();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["SortOrder"] != DBNull.Value)
                    rec.sortOrder = (int) rdr["SortOrder"];
 
                if (rdr["Source"] != DBNull.Value)
                    rec.source = (string) rdr["Source"];
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                if (rdr["StartDate"] != DBNull.Value)
                    rec.startDate = (DateTime) rdr["StartDate"];
 
                if (rdr["EndDate"] != DBNull.Value)
                    rec.endDate = (DateTime) rdr["EndDate"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            SourceCode[] convert(DomainObj[] objs)
            {
                SourceCode[] acls  = new SourceCode[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
