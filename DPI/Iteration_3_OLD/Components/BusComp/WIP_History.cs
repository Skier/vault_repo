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
    public class WIP_History : DomainObj
    {
        /*        Data        */
        static string iName = "WIP_History";
        int id;
        string workflow;
        int wipId;
        string step;
        DateTime stepStart;
        DateTime stepFinish;
        string busObjId;
        string busObjType;
        string nextStep;
        bool isCompleted;
		string user;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string Workflow
        {
            get { return workflow; }
            set
            {
                setState();
                workflow = value;
            }
        }
        public int WipId
        {
            get { return wipId; }
            set
            {
                setState();
                wipId = value;
            }
        }
        public string Step
        {
            get { return step; }
            set
            {
                setState();
                step = value;
            }
        }
        public DateTime StepStart
        {
            get { return stepStart; }
            set
            {
                setState();
                stepStart = value;
            }
        }
        public DateTime StepFinish
        {
            get { return stepFinish; }
            set
            {
                setState();
                stepFinish = value;
            }
        }
        public string BusObjId
        {
            get { return busObjId; }
            set
            {
                setState();
                busObjId = value;
            }
        }
        public string BusObjType
        {
            get { return busObjType; }
            set
            {
                setState();
                busObjType = value;
            }
        }
        public string NextStep
        {
            get { return nextStep; }
            set
            {
                setState();
                nextStep = value;
            }
        }
        public bool IsCompleted
        {
            get { return isCompleted; }
            set
            {
                setState();
                isCompleted = value;
            }
        }
		public string User
		{
			get { return user; }
			set
			{
				setState();
				user = value;
			}
		}
        
        /*        Constructors			*/
        public WIP_History()
        {
            sql = new WIP_HistorySQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public WIP_History(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
		public WIP_History(UOW uow, IStepInfo step) : this(uow)
		{
			this.BusObjId    = step.BusObj;
			this.BusObjType  = step.BusObjType;
			this.IsCompleted = step.IsCompleted;
			this.NextStep    = step.NextStepName;
			this.Step        = step.StepName;
			this.StepFinish  = step.StepEnd;
			this.StepStart   = step.StepStart;
			this.WipId       = step.WipId;
			this.Workflow    = step.Workflow;
			this.User		 = step.User; 
		}
        
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new WIP_HistorySQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static WIP_History find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(WIP_History.getKey(id)))
                return (WIP_History)uow.Imap.find(WIP_History.getKey(id));
            
            WIP_History cls = new WIP_History();
            cls.uow = uow;
            cls.id = id;
            cls = (WIP_History)DomainObj.addToIMap(uow, getOne(((WIP_HistorySQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static WIP_History[] getAll(UOW uow)
        {
            WIP_History[] objs = (WIP_History[])DomainObj.addToIMap(uow, (new WIP_HistorySQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static WIP_History getOne(WIP_History[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(WIP_History src, WIP_History tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.workflow = src.workflow;
            tar.wipId = src.wipId;
            tar.step = src.step;
            tar.stepStart = src.stepStart;
            tar.stepFinish = src.stepFinish;
            tar.busObjId = src.busObjId;
            tar.busObjType = src.busObjType;
            tar.nextStep = src.nextStep;
            tar.isCompleted = src.isCompleted;
            tar.rowState = src.rowState;
			tar.user = src.user;
        }
 
        /*		SQL		*/
        [Serializable]
        class WIP_HistorySQL : SqlGateway
        {
            public WIP_History[] getKey(WIP_History rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWIP_History_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                WIP_History rec = (WIP_History)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWIP_History_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                WIP_History rec = (WIP_History)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWIP_History_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                WIP_History rec = (WIP_History)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWIP_History_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public WIP_History[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spWIP_History_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, WIP_History rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@Workflow", SqlDbType.VarChar, 30).Value = rec.workflow;
                cmd.Parameters.Add("@WipId", SqlDbType.Int, 0).Value = rec.wipId;
 
                if (rec.step == null)
                    cmd.Parameters.Add("@Step", SqlDbType.VarChar, 30).Value = DBNull.Value;
                else
                {
                    if (rec.Step.Length == 0)
                        cmd.Parameters.Add("@Step", SqlDbType.VarChar, 30).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Step", SqlDbType.VarChar, 30).Value = rec.step;
                }
 
                if (rec.stepStart == DateTime.MinValue)
                    cmd.Parameters.Add("@StepStart", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StepStart", SqlDbType.DateTime, 0).Value = rec.stepStart;
 
                if (rec.stepFinish == DateTime.MinValue)
                    cmd.Parameters.Add("@StepFinish", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StepFinish", SqlDbType.DateTime, 0).Value = rec.stepFinish;
 
                if (rec.busObjId == null)
                    cmd.Parameters.Add("@BusObjId", SqlDbType.VarChar, 30).Value = DBNull.Value;
                else
                {
                    if (rec.BusObjId.Length == 0)
                        cmd.Parameters.Add("@BusObjId", SqlDbType.VarChar, 30).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@BusObjId", SqlDbType.VarChar, 30).Value = rec.busObjId;
                }
 
                if (rec.busObjType == null)
                    cmd.Parameters.Add("@BusObjType", SqlDbType.VarChar, 30).Value = DBNull.Value;
                else
                {
                    if (rec.BusObjType.Length == 0)
                        cmd.Parameters.Add("@BusObjType", SqlDbType.VarChar, 30).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@BusObjType", SqlDbType.VarChar, 30).Value = rec.busObjType;
                }
 
                if (rec.nextStep == null)
                    cmd.Parameters.Add("@NextStep", SqlDbType.VarChar, 30).Value = DBNull.Value;
                else
                {
                    if (rec.NextStep.Length == 0)
                        cmd.Parameters.Add("@NextStep", SqlDbType.VarChar, 30).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NextStep", SqlDbType.VarChar, 30).Value = rec.nextStep;
                }
 
                cmd.Parameters.Add("@IsCompleted", SqlDbType.Bit, 0).Value = rec.isCompleted;

				if (rec.user == null)
					cmd.Parameters.Add("@WFUser", SqlDbType.VarChar, 30).Value = DBNull.Value;
				else
				{
					if (rec.user.Length == 0)
						cmd.Parameters.Add("@WFUser", SqlDbType.VarChar, 30).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@WFUser", SqlDbType.VarChar, 30).Value = rec.user;
				}

            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                WIP_History rec = new WIP_History();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Workflow"] != DBNull.Value)
                    rec.workflow = (string) rdr["Workflow"];
 
                if (rdr["WipId"] != DBNull.Value)
                    rec.wipId = (int) rdr["WipId"];
 
                if (rdr["Step"] != DBNull.Value)
                    rec.step = (string) rdr["Step"];
 
                if (rdr["StepStart"] != DBNull.Value)
                    rec.stepStart = (DateTime) rdr["StepStart"];
 
                if (rdr["StepFinish"] != DBNull.Value)
                    rec.stepFinish = (DateTime) rdr["StepFinish"];
 
                if (rdr["BusObjId"] != DBNull.Value)
                    rec.busObjId = (string) rdr["BusObjId"];
 
                if (rdr["BusObjType"] != DBNull.Value)
                    rec.busObjType = (string) rdr["BusObjType"];
 
                if (rdr["NextStep"] != DBNull.Value)
                    rec.nextStep = (string) rdr["NextStep"];
 
                if (rdr["IsCompleted"] != DBNull.Value)
                    rec.isCompleted = (bool) rdr["IsCompleted"];

				if (rdr["WFUser"] != DBNull.Value)
					rec.user = (string) rdr["WFUser"];

                rec.rowState = RowState.Clean;
                return rec;
            }
            WIP_History[] convert(DomainObj[] objs)
            {
                WIP_History[] acls  = new WIP_History[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
