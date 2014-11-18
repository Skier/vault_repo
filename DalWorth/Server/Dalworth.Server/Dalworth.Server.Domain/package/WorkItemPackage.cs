using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.package
{
    public class WorkItemPackage
    {
        private Customer m_customer;
        private Task m_task;
        private int m_itemsCount;

        #region WorkItemPackage

        public WorkItemPackage(Customer customer, Task task, int itemsCount)
        {
            m_customer = customer;
            m_task = task;
            m_itemsCount = itemsCount;
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region TaskNumber

        public string TaskNumber
        {
            get { return m_task.Number; }
        }

        #endregion

        #region ItemsCount

        public int ItemsCount
        {
            get { return m_itemsCount; }
        }

        #endregion

        #region FindBy Work

        private const string SqlFindByWork =
            @"SELECT t.*, c.*, count(i.ID) as ItemQty FROM WorkDetail wd
                inner join Visit v on v.ID = wd.VisitId
                inner join Customer c on v.CustomerId = c.ID
                inner join VisitTask vt on vt.VisitId = v.ID
                inner join Task t on vt.TaskId = t.ID and t.TaskTypeId = 2
                inner join Item i on i.TaskId = t.ID
                where wd.WorkId = ?WorkId
                group by t.ID";

        public static List<WorkItemPackage> FindBy(Work work)
        {
            List<WorkItemPackage> result = new List<WorkItemPackage>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWork))
            {
                Database.PutParameter(dbCommand, "?WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Task task = Task.Load(dataReader);
                        Customer customer = Customer.Load(dataReader, Task.FieldsCount);
                        int quantity = dataReader.GetInt32(Task.FieldsCount + Customer.FieldsCount);
                        result.Add(new WorkItemPackage(customer, task, quantity));
                    }
                }
            }
            return result;
        }

        #endregion        
    }
}
