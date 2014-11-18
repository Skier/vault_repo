using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
    /// <summary>
    /// Entity which reflects spACT_GetActivationWorkLog SP resulting recordset
    /// </summary>
    [Serializable]
    public class ActivationWorkLog : DomainObj
    {
        #region Helper classes

        [Serializable]
        private class ActivationWorkLogSQL : SqlGateway
        {
            private static int[] colIdx;

            public override void insert(DomainObj rec) {
                throw new NotSupportedException();
            }

            public override void delete(DomainObj rec) {
                throw new NotSupportedException();
            }

            public override void update(DomainObj rec) {
                throw new NotSupportedException();
            }

            protected override DomainObj reader(SqlDataReader rdr) {
                ActivationWorkLog rec = new ActivationWorkLog();

                if (colIdx == null) {
                    colIdx = new int[] {
                                           rdr.GetOrdinal("Activation_Work_Log_ID"),
                                           rdr.GetOrdinal("Activity_Type"),
                                           rdr.GetOrdinal("Description"),
                                           rdr.GetOrdinal("UserID"),
                                           rdr.GetOrdinal("Assigned_By"),
                                           rdr.GetOrdinal("ILEC"),
                                           rdr.GetOrdinal("Work_Start"),
                                           rdr.GetOrdinal("Work_Finish")
                                       };
                }

                if (!rdr.IsDBNull(colIdx[0])) {
                    rec.id = rdr.GetInt32(colIdx[0]);
                }

                if (!rdr.IsDBNull(colIdx[1])) {
                    rec.type = rdr.GetString(colIdx[1]);
                }

                if (!rdr.IsDBNull(colIdx[2])) {
                    rec.description = rdr.GetString(colIdx[2]);
                }

                if (!rdr.IsDBNull(colIdx[3])) {
                    rec.userId = rdr.GetString(colIdx[3]);
                }

                if (!rdr.IsDBNull(colIdx[4])) {
                    rec.assignedBy = rdr.GetString(colIdx[4]);
                }

                if (!rdr.IsDBNull(colIdx[5])) {
                    rec.ilec = rdr.GetString(colIdx[5]);
                }

                if (!rdr.IsDBNull(colIdx[6])) {
                    rec.workStart = rdr.GetDateTime(colIdx[6]);
                }

                if (!rdr.IsDBNull(colIdx[7])) {
                    rec.workFinish = rdr.GetDateTime(colIdx[7]);
                }

                return rec;
            }

            public ActivationWorkLog[] getByAccountNumber(UOW uow, int accountNumber) {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spACT_GetActivationWorkLog";
                cmd.Parameters.Add(new SqlParameter("@Accnumber", accountNumber));
                return convert(execReader(cmd));
            }

            private ActivationWorkLog[] convert(DomainObj[] objs) {
                ActivationWorkLog[] acls = new ActivationWorkLog[objs.Length];
                objs.CopyTo(acls, 0);
                return acls;
            }
        }

        #endregion Helper classes

        #region Constants

        public const string iName = "ActivationWorkLog";

        #endregion Constants

        #region Fields

        private int id;
        private string type;
        private string description;
        private string userId;
        private string assignedBy;
        private string ilec;
        private DateTime workStart;
        private DateTime workFinish;

        #endregion Fields

        #region Contrusctors

        public ActivationWorkLog() {
            sql = new ActivationWorkLogSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
            priority = 15000;
        }

        public ActivationWorkLog(UOW uow) : this() {
            if (uow == null) {
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            }

            if (uow.Imap == null) {
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            }

            this.uow = uow;
            this.uow.Imap.add(this);
        }

        #endregion Contrusctors

        #region Methods

        public static Key getKey(int id) {
            return new Key(iName, id.ToString());
        }

        public static ActivationWorkLog[] getByAccountNumber(UOW uow, int accountNumber) {
            ActivationWorkLog[] objs =
                (ActivationWorkLog[]) addToIMap(
                                          uow,
                                          (new ActivationWorkLogSQL()).getByAccountNumber(uow, accountNumber));
            for (int i = 0; i < objs.Length; i++) {
                objs[i].uow = uow;
            }
            return objs;
        }

        protected override SqlGateway loadSql() {
            return new ActivationWorkLogSQL();
        }

        public override void checkExists() {
            if ((Id < 1)) {
                throw new ArgumentException("Valid row is required for update and delete");
            }
        }

        #endregion Methods

        #region Properties

        public override IDomKey IKey {
            get { return new Key(iName, id.ToString()); }
        }

        public int Id {
            get { return id; }
        }

        public string Type {
            get { return type; }
            set {
                setState();
                type = value;
            }
        }

        public string Description {
            get { return description; }
            set {
                setState();
                description = value;
            }
        }

        public string UserId {
            get { return userId; }
            set {
                setState();
                userId = value;
            }
        }

        public string AssignedBy {
            get { return assignedBy; }
            set {
                setState();
                assignedBy = value;
            }
        }

        public string Ilec {
            get { return ilec; }
            set {
                setState();
                ilec = value;
            }
        }

        public DateTime WorkStart {
            get { return workStart; }
            set {
                setState();
                workStart = value;
            }
        }

        public DateTime WorkFinish {
            get { return workFinish; }
            set {
                setState();
                workFinish = value;
            }
        }

        #endregion Properties
    }
}