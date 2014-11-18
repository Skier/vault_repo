using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class ProjectConstructionBillPay
    {
        public ProjectConstructionBillPay()
        {
        }

        #region AmountValue

        public decimal AmountValue
        {
            get
            {
                if (BillPayType == ProjectConstructionBillPayTypeEnum.Credit
                    || BillPayType == ProjectConstructionBillPayTypeEnum.Payment)
                    return -(Amount);
                else
                    return Amount;
            }
        }

        #endregion

        #region BillPayType

        public ProjectConstructionBillPayTypeEnum BillPayType
        {
            get { return (ProjectConstructionBillPayTypeEnum)m_projectConstructionBillPayTypeId; }
            set { m_projectConstructionBillPayTypeId = (int)value; }
        }

        #endregion

        #region BillPayTypeText

        public string BillPayTypeText
        {
            get
            {
                return ProjectConstructionBillPayType.GetText(BillPayType);
            }
        }

        #endregion

        #region Balance

        private decimal m_balance;
        public decimal Balance
        {
            get { return m_balance; }
            set { m_balance = value; }
        }

        #endregion

        #region StatusImageIndex

        public int StatusImageIndex
        {
            get { return IsVoided ? 1 : 0; }
        }

        #endregion

        #region FindByProject

        private const String SqlFindByProject =
                @"SELECT * 
                    FROM projectconstructionbillpay 
                   WHERE ProjectId = ?ProjectId";

        public static BindingList<ProjectConstructionBillPay> FindByProject(int projectId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProject))
            {
                Database.PutParameter(dbCommand, "?ProjectId", projectId);

                BindingList<ProjectConstructionBillPay> result = new BindingList<ProjectConstructionBillPay>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }

                }
                return result;
            }

        }

        #endregion

    }
}
      