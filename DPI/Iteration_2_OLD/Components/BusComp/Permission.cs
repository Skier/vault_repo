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
    public class Permission : DomainObj, IPermission
    {
        /*        Data        */
        static string iName = "Permission";
        string permsName;
        string description;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, permsName); }
        }
        public string PermsName
        {
            get { return permsName; }
            set
            {
                setState();
                permsName = value;
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
        
        /*        Constructors			*/
        public Permission()
        {
            sql = new PermissionSQL();
            rowState = RowState.New;
        }
        public Permission(UOW uow) : this()
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
            return new PermissionSQL();
        }
        public override void checkExists()
        {
            if ((PermsName ==  null))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Permission find(UOW uow, string permsName)
        {
            if (uow.Imap.keyExists(Permission.getKey(permsName)))
                return (Permission)uow.Imap.find(Permission.getKey(permsName));
            
            Permission cls = new Permission();
            cls.uow = uow;
            cls.permsName = permsName;
            cls = (Permission)DomainObj.addToIMap(uow, getOne(((PermissionSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Permission[] getAll(UOW uow)
        {
            Permission[] objs = (Permission[])DomainObj.addToIMap(uow, (new PermissionSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(string permsName)
        {
            return new Key(iName, permsName.ToString());
        }
        /*		Implementation		*/
        static Permission getOne(Permission[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Permission src, Permission tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.permsName = src.permsName;
            tar.description = src.description;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class PermissionSQL : SqlGateway
        {
            public Permission[] getKey(Permission rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermission_Get_Id";
                cmd.Parameters.Add("@PermsName", SqlDbType.VarChar, 50).Value = rec.permsName;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Permission rec = (Permission)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermission_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Permission rec = (Permission)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermission_Del_Id";
                cmd.Parameters.Add("@PermsName", SqlDbType.VarChar, 50).Value = rec.permsName;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Permission rec = (Permission)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPermission_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Permission[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spPermission_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Permission rec)
            {
 
                cmd.Parameters.Add("@PermsName", SqlDbType.VarChar, 50).Value = rec.permsName;
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 500).Value = rec.description;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Permission rec = new Permission();
                
                if (rdr["PermsName"] != DBNull.Value)
                    rec.permsName = (string) rdr["PermsName"];
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Permission[] convert(DomainObj[] objs)
            {
                Permission[] acls  = new Permission[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
