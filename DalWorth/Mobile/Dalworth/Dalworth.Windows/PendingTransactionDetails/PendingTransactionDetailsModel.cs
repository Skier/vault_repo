using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;

namespace Dalworth.Windows.PendingTransactionDetails
{
    public class PendingTransactionDetailsModel : IModel
    {
        #region Transaction

        private WorkTransaction m_transaction;
        public WorkTransaction Transaction
        {
            set { m_transaction = value; }
            get { return m_transaction; }
        }

        #endregion

        #region Init

        public void Init()
        {
            
        }

        #endregion

        #region GetWorkTransactionText

        public string GetWorkTransactionText()
        {
            string result = string.Empty;

            result += string.Format("{0}, Visit ID - {1}, Work ID - {2}, Transaction Date - {3}",
                WorkTransactionType.GetText(m_transaction.WorkTransactionType),
                m_transaction.VisitId.Value, m_transaction.WorkId, m_transaction.TransactionDate);
            result += "\r\n\r\n";


            if (m_transaction.WorkTransactionType == WorkTransactionTypeEnum.VisitCompleted)
            {
                List<WorkTransactionTask> tasks = WorkTransactionTask.FindBy(m_transaction.ID);
                foreach (WorkTransactionTask task in tasks)
                {
                    result += string.Format("Task ID - {0}, TaskStatus - {1}",
                        task.TaskId, TaskStatus.GetText(task.TaskStatus));
                    result += "\r\n_______________Items______________\r\n";

                    List<WorkTransactionTaskItem> items = WorkTransactionTaskItem.FindBy(task.ID);
                    foreach (WorkTransactionTaskItem workTransactionTaskItem in items)
                    {
                        Item item = Item.FindByPrimaryKey(workTransactionTaskItem.ItemId);

                        result += string.Format("[{0}], ", item.SerialNumber);

                        if (workTransactionTaskItem.IsCaptured)
                            result += "Captured, ";
                        else if (workTransactionTaskItem.IsLeft)
                            result += "Delivered, ";
                        
                        result += string.Format("Item - ({0})", item.Name);

                        if (item.IsProtectorApplied)
                            result += ", Protector " + item.ProtectorCost.ToString("C");

                        if (item.IsPaddingApplied)
                            result += ", Padding " + item.PaddingCost.ToString("C");

                        if (item.IsMothRepelApplied)
                            result += ", Moth Repel " + item.MothRepelCost.ToString("C");

                        if (item.IsRapApplied)
                            result += ", Rap " + item.RapCost.ToString("C");

                        result += ", Clean " + item.CleanCost.ToString("C");

                        if (item.OtherCost != decimal.Zero)
                            result += ", Other " + item.OtherCost.ToString("C");

                        result += ", SubTotal " + item.SubTotalCost.ToString("C");
                        result += ", Tax " + item.TaxCost.ToString("C");
                        result += ", Total " + item.TotalCost.ToString("C");

                        result += "\r\n";
                    }
                }
            }
            else if (m_transaction.WorkTransactionType == WorkTransactionTypeEnum.VisitDeclined)
            {
                if (m_transaction.Notes != null && m_transaction.Notes != string.Empty)
                    result += "Reason - " + m_transaction.Notes;
            }
            else if (m_transaction.WorkTransactionType == WorkTransactionTypeEnum.SubmitETC)
            {
                WorkTransactionEtc etc = WorkTransactionEtc.FindByPrimaryKey(m_transaction.ID);

                bool isAtLeastOneOptionListed = false;
                if (etc.SaleAmount != decimal.Zero)
                {
                    result += "Sale Amount - " + etc.SaleAmount.ToString("C");
                    isAtLeastOneOptionListed = true;
                }
                    

                if (etc.Hours.HasValue || etc.Minutes.HasValue)
                {
                    string optionText = "Estimate time - " + (etc.Hours ?? 0) + "h " + (etc.Minutes ?? 0) + "min";
                    if (isAtLeastOneOptionListed)
                        result += ", " + optionText;
                    else
                        result += optionText;
                    isAtLeastOneOptionListed = true;
                }

                if (etc.Notes != null && etc.Notes != string.Empty)
                {
                    string optionText = "Notes - " + etc.Notes;
                    if (isAtLeastOneOptionListed)
                        result += ", " + optionText;
                    else
                        result += optionText;
                }
            }
            else if (m_transaction.WorkTransactionType == WorkTransactionTypeEnum.GPS)
            {
                WorkTransactionGps gps = WorkTransactionGps.FindByPrimaryKey(m_transaction.ID);
                result += string.Format("Latitude - {0}, Longitude - {1}, Gps Time - {2}",
                                        gps.Latitude, gps.Longitude, gps.GpsTime);
            }

            return result;
        }

        #endregion
    }
}
