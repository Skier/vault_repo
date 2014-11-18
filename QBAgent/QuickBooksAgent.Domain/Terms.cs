using System;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class Terms : ICounterField
    {
        #region Constructors

        public Terms()
        {

        }

        public Terms(int TermsId, String Name)
        {
            m_termsId = TermsId;
            m_name = Name;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return m_name;
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_termsId;
            }
            set
            {
                m_termsId = value;
            }
        }

        const String counterName = "Terms";

        public string CounterName
        {
            get { return counterName; }
        }

        #endregion

        #region FindByQuickBooksId

        private const String SqlSelectByQuickBooksId = @"Select 
	        TermsId, 
	        QuickBooksListId, 
	        Name, 
	        TimeCreated, 
	        TimeModified,
	        EditSequence,
	        StdDueDays, 
	        StdDiscountDays, 
	        DiscountPct
        From Terms where QuickBooksListId = @QuickBooksListId";

        public static Terms FindByQuickBooksId(int quickBooksId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByQuickBooksId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksListId", quickBooksId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Terms not found, search by primary key");
        }

        #endregion

        #region Equals & GetHashCode

        public override int GetHashCode()
        {
            return m_termsId;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Terms terms = obj as Terms;
            if (terms == null) return false;
            if (m_termsId != terms.m_termsId) return false;
            return true;
        }

        #endregion
    }
}
