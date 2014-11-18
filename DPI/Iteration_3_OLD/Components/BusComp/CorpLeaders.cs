//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Components;
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//    [Serializable]
//    public class CorpLeaders : DomainObj
//    {
//        /*        Data        */
//        static string iName = "CorpLeaders";
//        int id;
//        string storeCode;
//        string storeNumber;
//        int corpid;
//        DateTime statDate;
//        string statType;
//        int rank;
//        int rankValue;
//        
//        /*        Properties        */
//        public override IDomKey IKey 
//        {
//             get { return new Key(iName, id.ToString()); }
//        }
//        public int Id
//        {
//            get { return id; }
//        }
//        public string StoreCode
//        {
//            get { return storeCode; }
//            set
//            {
//                setState();
//                storeCode = value;
//            }
//        }
//        public string StoreNumber
//        {
//            get { return storeNumber; }
//            set
//            {
//                setState();
//                storeNumber = value;
//            }
//        }
//        public int Corpid
//        {
//            get { return corpid; }
//            set
//            {
//                setState();
//                corpid = value;
//            }
//        }
//        public DateTime StatDate
//        {
//            get { return statDate; }
//            set
//            {
//                setState();
//                statDate = value;
//            }
//        }
//        public string StatType
//        {
//            get { return statType; }
//            set
//            {
//                setState();
//                statType = value;
//            }
//        }
//        public int Rank
//        {
//            get { return rank; }
//            set
//            {
//                setState();
//                rank = value;
//            }
//        }
//        public int RankValue
//        {
//            get { return rankValue; }
//            set
//            {
//                setState();
//                rankValue = value;
//            }
//        }
//        
//        /*        Constructors			*/
//        public CorpLeaders()
//        {
//            sql = new CorpLeadersSQL();
//            id = random.Next(Int32.MinValue, -1);
//            rowState = RowState.New;
//        }
//        public CorpLeaders(UOW uow) : this()
//        {
//            if(uow == null)
//                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
//            
//            if(uow.Imap == null)
//                throw new ArgumentException("IdentityMap is required", "Identity Map");
//            
//            this.uow = uow;
//            this.uow.Imap.add(this);
//        }
//        
//        /*        Methods        */
//        protected override SqlGateway loadSql()
//        {
//            return new CorpLeadersSQL();
//        }
//        public override void checkExists()
//        {
//            if ((Id < 1))
//                throw new ArgumentException("Valid row is required for update and delete");
//        }
//        /*		Static methods		*/
//        public static CorpLeaders find(UOW uow, int id)
//        {
//            if (uow.Imap.keyExists(CorpLeaders.getKey(id)))
//                return (CorpLeaders)uow.Imap.find(CorpLeaders.getKey(id));
//            
//            CorpLeaders cls = new CorpLeaders();
//            cls.uow = uow;
//            cls.id = id;
//            cls = (CorpLeaders)DomainObj.addToIMap(uow, getOne(((CorpLeadersSQL)cls.Sql).getKey(cls)));
//            cls.uow = uow;
//            
//            return cls;
//        }		
//		public static CorpLeaders[] GetAll_Date(UOW uow, DateTime statDate, string statType)
//		{
//			CorpLeaders[] objs = 
//				(CorpLeaders[])DomainObj.addToIMap(uow, (new CorpLeadersSQL()).getAll_Date(uow, statDate, statType));
//			
//			for (int i = 0; i < objs.Length; i++)
//				objs[i].uow = uow;
//			
//			return objs;
//		}		
//        public static CorpLeaders[] getAll(UOW uow)
//        {
//            CorpLeaders[] objs = (CorpLeaders[])DomainObj.addToIMap(uow, (new CorpLeadersSQL()).getAll(uow));
//            for (int i = 0; i < objs.Length; i++)
//                objs[i].uow = uow;
//            return objs;
//        }
//		public static CorpLeaders[] getAllCorps(UOW uow, DateTime statDate)
//		{
//			CorpLeaders[] objs = (CorpLeaders[])new CorpLeadersSQL().getAllCorps(uow,statDate);
//			return objs;
//		}
//        public static Key getKey(int id)
//        {
//            return new Key(iName, id.ToString());
//        }
//        /*		Implementation		*/
//        static CorpLeaders getOne(CorpLeaders[] acls)
//        {
//            if (acls.Length == 1)
//                return acls[0];
//            
//            if (acls.Length == 0)
//                throw new ArgumentException("Row not found");
//            
//            throw new ArgumentException("More than one row found");
//        }
//        static void copyAttrs(CorpLeaders src, CorpLeaders tar)
//        {
//            if (tar == null)
//                throw new ArgumentNullException("Target object must not be null");
//            
//            if (src == null)
//                throw new ArgumentNullException("Source object must not be null");
//            
//            tar.id = src.id;
//            tar.storeCode = src.storeCode;
//            tar.storeNumber = src.storeNumber;
//            tar.corpid = src.corpid;
//            tar.statDate = src.statDate;
//            tar.statType = src.statType;
//            tar.rank = src.rank;
//            tar.rankValue = src.rankValue;
//            tar.rowState = src.rowState;
//        }
// 
//        /*		SQL		*/
//        [Serializable]
//        class CorpLeadersSQL : SqlGateway
//        {
//			////////////////////////////////////////////////////////////		
//			
//			public CorpLeaders[] getAllCorps(UOW uow, DateTime statDate)
//			{
//				SqlCommand cmd = uow.Cn.CreateCommand();
//				cmd.Transaction = uow.Tran; 
//				cmd.CommandType = CommandType.StoredProcedure;		
//				cmd.CommandText = "spCorpStatistics_Get_AllCorp";
//				
//				if (statDate == DateTime.MinValue)
//					cmd.Parameters.Add("@StatsDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
//				else
//					cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = statDate;
//				return convert(execReader(cmd));
//				
//			}
//			/// //////////////////////////////////////////////////////////////////////////////
//            public CorpLeaders[] getKey(CorpLeaders rec)
//            {
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spCorpLeaders_Get_Id";
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
//                return convert(execReader(cmd));
//            }
//            public override void insert(DomainObj obj)
//            {
//                CorpLeaders rec = (CorpLeaders)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spCorpLeaders_Ins";
//                setParam(cmd, rec);
//                
//                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
//                execScalar(cmd);
//                rec.id = (int)cmd.Parameters["@Id"].Value;
//                rec.rowState = RowState.Clean;
//            }
//            public override void delete(DomainObj obj)
//            {
//                CorpLeaders rec = (CorpLeaders)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spCorpLeaders_Del_Id";
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
//                
//                execScalar(cmd);
//                rec.rowState = RowState.Deleted;
//            }
//            public override void update(DomainObj obj)
//            {
//                CorpLeaders rec = (CorpLeaders)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spCorpLeaders_Upd";
//                setParam(cmd, rec);
//                
//                execScalar(cmd);
//                rec.rowState = RowState.Clean;
//            }
//            public CorpLeaders[] getAll(UOW uow)
//            {
//                SqlCommand cmd = makeCommand(uow);
//                cmd.CommandText = "spCorpLeaders_Get_All";
//                return convert(execReader(cmd));
//            }
//			public CorpLeaders[] getAll_Date(UOW uow, DateTime statDate, string statType)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				cmd.CommandText = "spCorpLeaders_Get_All_Date";
//
//				if (statDate == DateTime.MinValue)
//					cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
//				else
//					cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = statDate;
//	
//				if (statType == null)
//					cmd.Parameters.Add("@StatType", SqlDbType.VarChar, 25).Value = DBNull.Value;
//				else
//				{
//					if (statType.Length == 0)
//						cmd.Parameters.Add("@StatType", SqlDbType.VarChar, 25).Value = DBNull.Value;
//					else
//						cmd.Parameters.Add("@statType", SqlDbType.VarChar, 25).Value = statType;
//				}
//
//				return convert(execReader(cmd));
//			}
//            /*        Implementation        */
//            void setParam(SqlCommand cmd, CorpLeaders rec)
//            {
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
// 
//                if (rec.storeCode == null)
//                    cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                else
//                {
//                    if (rec.StoreCode.Length == 0)
//                        cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.storeCode;
//                }
// 
//                if (rec.storeNumber == null)
//                    cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                else
//                {
//                    if (rec.StoreNumber.Length == 0)
//                        cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = rec.storeNumber;
//                }
//                
//                // Numeric, nullable foreign key treatment:
//                if (rec.Corpid == 0)
//                    cmd.Parameters.Add("@Corpid", SqlDbType.Int, 0).Value = DBNull.Value;
//                else
//                    cmd.Parameters.Add("@Corpid", SqlDbType.Int, 0).Value = rec.corpid;
// 
//                if (rec.statDate == DateTime.MinValue)
//                    cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
//                else
//                    cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = rec.statDate;
// 
//                if (rec.statType == null)
//                    cmd.Parameters.Add("@StatType", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                else
//                {
//                    if (rec.StatType.Length == 0)
//                        cmd.Parameters.Add("@StatType", SqlDbType.VarChar, 10).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@StatType", SqlDbType.VarChar, 10).Value = rec.statType;
//                }
//                cmd.Parameters.Add("@Rank", SqlDbType.Int, 0).Value = rec.rank;
//                cmd.Parameters.Add("@RankValue", SqlDbType.Int, 0).Value = rec.rankValue;
//            }
//            protected override DomainObj reader(SqlDataReader rdr)
//            {
//                CorpLeaders rec = new CorpLeaders();
//                
//                if (rdr["Id"] != DBNull.Value)
//                    rec.id = (int) rdr["Id"];
// 
//                if (rdr["StoreCode"] != DBNull.Value)
//                    rec.storeCode = (string) rdr["StoreCode"];
// 
//                if (rdr["StoreNumber"] != DBNull.Value)
//                    rec.storeNumber = (string) rdr["StoreNumber"];
// 
//                if (rdr["Corpid"] != DBNull.Value)
//                    rec.corpid = (int) rdr["Corpid"];
// 
//                if (rdr["StatDate"] != DBNull.Value)
//                    rec.statDate = (DateTime) rdr["StatDate"];
// 
//                if (rdr["StatType"] != DBNull.Value)
//                    rec.statType = (string) rdr["StatType"];
// 
//                if (rdr["Rank"] != DBNull.Value)
//                    rec.rank = (int) rdr["Rank"];
// 
//                if (rdr["RankValue"] != DBNull.Value)
//                    rec.rankValue = (int) rdr["RankValue"];
// 
//                rec.rowState = RowState.Clean;
//                return rec;
//            }
//            CorpLeaders[] convert(DomainObj[] objs)
//            {
//                CorpLeaders[] acls  = new CorpLeaders[objs.Length];
//                objs.CopyTo(acls, 0);
//                return  acls;
//            }
//        }
//    }
//}
