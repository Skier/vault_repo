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
    public class CardApp : DomainObj, ICardApp
    {
        /*        Data        */
        static string iName = "CardApp";
        int id;
        string appType;
        DateTime appDate;
        string idNumber;
        string idState;
        string idType;
        DateTime idExpDate;
        int dmd;
		IDemand demand;
        string cardNumber;
        string expMonth;
        string expYear;
        string prevCard;
        bool approved;
        string status;
		bool verified;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string AppType
        {
            get { return appType; }
            set
            {
                setState();
                appType = value;
            }
        }
        public DateTime AppDate
        {
            get { return appDate; }
            set
            {
                setState();
                appDate = value;
            }
        }
        public string IdNumber
        {
            get { return idNumber; }
            set
            {
                setState();
                idNumber = value;
            }
        }
        public string IdState
        {
            get { return idState; }
            set
            {
                setState();
                idState = value;
            }
        }
        public string IdType
        {
            get { return idType; }
            set
            {
                setState();
                idType = value;
            }
        }
        public DateTime IdExpDate
        {
            get { return idExpDate; }
            set
            {
                setState();
                idExpDate = value;
            }
        }
		public IDemand ParDemand
		{
			get 
			{
				if (demand == null)
					if (dmd > 0)
						demand = Demand.find(uow, dmd);
 
				return demand; 
			}
			set 
			{ 
				demand = (Demand)value; 
				dmd = 0;
				
				if (demand != null)
					dmd = demand.Id;
				
				setState();
			}
		}
		public int Dmd
		{
		    get	
			{ 
				if (ParDemand == null)
					return 0;

				return ParDemand.Id;
			}
			set 
			{
				if (demand != null)
					if (demand.Id != value)
						throw new ApplicationException(
							"Demand id of Set propery conflicts with Demand already in this CardApp");

				this.dmd = value;
			}
		}
        public string CardNum
        {
            get { return cardNumber; }
            set
            {
                setState();
                cardNumber = value;
            }
        }
        public string ExpMonth
        {
            get { return expMonth; }
            set
            {
                setState();
                expMonth = value;
            }
        }
        public string ExpYear
        {
            get { return expYear; }
            set
            {
                setState();
                expYear = value;
            }
        }
        public string PrevCard
        {
            get { return prevCard; }
            set
            {
                setState();
                prevCard = value;
            }
        }
        public bool Approved
        {
            get { return approved; }
            set
            {
                setState();
                approved = value;
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
		public bool Verified
		{
			get { return verified; }
			set
			{
				setState();
				verified = value;
			}
		}
        
        /*        Constructors			*/
        public CardApp()
        {
            sql = new CardAppSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
			priority = 19000;
        }
		public CardApp(IMap imap) : this()
		{
			if(imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");

			imap.add(this);
 		}
        public CardApp(UOW uow) : this(uow.Imap)
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            this.uow = uow;
       }        
		public CardApp(IMap imap, IDemand demand) : this(imap)
		{
			if (demand == null)
				throw new ArgumentException("Demand is required", "Demand");
			
			this.demand = demand;
		}
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new CardAppSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static CardApp find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(CardApp.getKey(id)))
                return (CardApp)uow.Imap.find(CardApp.getKey(id));
            
            CardApp cls = new CardApp();
            cls.uow = uow;
            cls.id = id;
            cls = (CardApp)DomainObj.addToIMap(uow, getOne(((CardAppSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static CardApp[] getAll(UOW uow)
        {
            CardApp[] objs = (CardApp[])DomainObj.addToIMap(uow, (new CardAppSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static CardApp getOne(CardApp[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(CardApp src, CardApp tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.appType = src.appType;
            tar.appDate = src.appDate;
            tar.idNumber = src.idNumber;
            tar.idState = src.idState;
            tar.idType = src.idType;
            tar.idExpDate = src.idExpDate;
            tar.dmd = src.dmd;
            tar.cardNumber = src.cardNumber;
            tar.expMonth = src.expMonth;
            tar.expYear = src.expYear;
            tar.prevCard = src.prevCard;
            tar.approved = src.approved;
            tar.status = src.status;
            tar.rowState = src.rowState;
			tar.verified = src.verified;
        }
		public override	void RefreshForeignKeys()
		{
			if (dmd > 0)
				return;

			if (ParDemand != null)
				dmd = ParDemand.Id;
		}
        /*		SQL		*/
        [Serializable]
        class CardAppSQL : SqlGateway
        {
            public CardApp[] getKey(CardApp rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCardApp_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                CardApp rec = (CardApp)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCardApp_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CardApp rec = (CardApp)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCardApp_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CardApp rec = (CardApp)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCardApp_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CardApp[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCardApp_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, CardApp rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.appType == null)
                    cmd.Parameters.Add("@AppType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.AppType.Length == 0)
                        cmd.Parameters.Add("@AppType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AppType", SqlDbType.VarChar, 25).Value = rec.appType;
                }
 
                if (rec.appDate == DateTime.MinValue)
                    cmd.Parameters.Add("@AppDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AppDate", SqlDbType.DateTime, 0).Value = rec.appDate;
 
                if (rec.idNumber == null)
                    cmd.Parameters.Add("@IdNumber", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.IdNumber.Length == 0)
                        cmd.Parameters.Add("@IdNumber", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@IdNumber", SqlDbType.VarChar, 25).Value = rec.idNumber;
                }
 
                if (rec.idState == null)
                    cmd.Parameters.Add("@IdState", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.IdState.Length == 0)
                        cmd.Parameters.Add("@IdState", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@IdState", SqlDbType.VarChar, 25).Value = rec.idState;
                }
 
                if (rec.idType == null)
                    cmd.Parameters.Add("@IdType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.IdType.Length == 0)
                        cmd.Parameters.Add("@IdType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@IdType", SqlDbType.VarChar, 25).Value = rec.idType;
                }
 
                if (rec.idExpDate == DateTime.MinValue)
                    cmd.Parameters.Add("@IdExpDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@IdExpDate", SqlDbType.DateTime, 0).Value = rec.idExpDate;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Dmd == 0)
                    cmd.Parameters.Add("@Dmd", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Dmd", SqlDbType.Int, 0).Value = rec.dmd;
 
                if (rec.cardNumber == null)
                    cmd.Parameters.Add("@CardNumber", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.CardNum.Length == 0)
                        cmd.Parameters.Add("@CardNumber", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@CardNumber", SqlDbType.VarChar, 25).Value = rec.cardNumber;
                }
 
                if (rec.expMonth == null)
                    cmd.Parameters.Add("@ExpMonth", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.ExpMonth.Length == 0)
                        cmd.Parameters.Add("@ExpMonth", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ExpMonth", SqlDbType.VarChar, 15).Value = rec.expMonth;
                }
 
                if (rec.expYear == null)
                    cmd.Parameters.Add("@ExpYear", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.ExpYear.Length == 0)
                        cmd.Parameters.Add("@ExpYear", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ExpYear", SqlDbType.VarChar, 15).Value = rec.expYear;
                }
 
                if (rec.prevCard == null)
                    cmd.Parameters.Add("@PrevCard", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.PrevCard.Length == 0)
                        cmd.Parameters.Add("@PrevCard", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PrevCard", SqlDbType.VarChar, 25).Value = rec.prevCard;
                }
 
                cmd.Parameters.Add("@Approved", SqlDbType.Bit, 0).Value = rec.approved;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }
				cmd.Parameters.Add("@Verified", SqlDbType.Bit, 0).Value = rec.verified;

            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                CardApp rec = new CardApp();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["AppType"] != DBNull.Value)
                    rec.appType = (string) rdr["AppType"];
 
                if (rdr["AppDate"] != DBNull.Value)
                    rec.appDate = (DateTime) rdr["AppDate"];
 
                if (rdr["IdNumber"] != DBNull.Value)
                    rec.idNumber = (string) rdr["IdNumber"];
 
                if (rdr["IdState"] != DBNull.Value)
                    rec.idState = (string) rdr["IdState"];
 
                if (rdr["IdType"] != DBNull.Value)
                    rec.idType = (string) rdr["IdType"];
 
                if (rdr["IdExpDate"] != DBNull.Value)
                    rec.idExpDate = (DateTime) rdr["IdExpDate"];
 
                if (rdr["Dmd"] != DBNull.Value)
                    rec.dmd = (int) rdr["Dmd"];
 
                if (rdr["CardNumber"] != DBNull.Value)
                    rec.cardNumber = (string) rdr["CardNumber"];
 
                if (rdr["ExpMonth"] != DBNull.Value)
                    rec.expMonth = (string) rdr["ExpMonth"];
 
                if (rdr["ExpYear"] != DBNull.Value)
                    rec.expYear = (string) rdr["ExpYear"];
 
                if (rdr["PrevCard"] != DBNull.Value)
                    rec.prevCard = (string) rdr["PrevCard"];
 
                if (rdr["Approved"] != DBNull.Value)
                    rec.approved = (bool) rdr["Approved"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];

				if (rdr["Verified"] != DBNull.Value)
					rec.verified = (bool) rdr["Verified"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            CardApp[] convert(DomainObj[] objs)
            {
                CardApp[] acls  = new CardApp[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
