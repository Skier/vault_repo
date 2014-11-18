using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Servman.Domain
{
    public partial class h_order
    {
        public h_order(){ }

        #region TechnicianName

        private string m_technicianName;
        public string TechnicianName
        {
            get { return m_technicianName; }
            set { m_technicianName = value; }
        }

        #endregion

        #region ServiceTypeText

        private string m_serviceTypeText;
        public string ServiceTypeText
        {
            get { return m_serviceTypeText; }
            set { m_serviceTypeText = value; }
        }

        #endregion

        #region CompletionTypeText

        private string m_completionTypeText;
        public string CompletionTypeText
        {
            get { return m_completionTypeText; }
            set { m_completionTypeText = value; }
        }

        #endregion

        #region GetServiceTypeText

        public static string GetServiceTypeText(h_order order)
        {
            if (order.serv_type == 1)
                return "Residential Carpet Cleaning";
            if (order.serv_type == 2)
                return "Duct Cleaning";
            if (order.serv_type == 3)
                return "Restoration";
            if (order.serv_type == 4)
                return "Deflood";
            if (order.serv_type == 5)
                return "Commercial Carpet Cleaning";
            return string.Empty;
        }

        #endregion

        #region GetCompletionTypeText

        public static string GetCompletionTypeText(h_order order)
        {
            if (order.comp_type == 1)
                return "Not Completed";
            if (order.comp_type == 2)
                return "Normal";
            if (order.comp_type == 3)
                return "Canceled";
            if (order.comp_type == 4)
                return "Converted";
            if (order.comp_type == 5)
                return "Expired";
            return string.Empty;
        }

        #endregion


        #region FindByCustomer

        private const string SqlFindByCustomer =
            @"select * from h_order 
              LEFT JOIN techmast on h_order.Tech_id = techmast.Tech_id 
                where Cust_id = ? 
             order by Date desc";

        public static List<h_order> FindByCustomer(string custId, IDbConnection connection)
        {
            List<h_order> orders = new List<h_order>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCustomer, connection))
            {
                Database.PutParameter(dbCommand, "@Cust_id", custId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        h_order order = Load(dataReader);
                        if (!dataReader.IsDBNull(38))
                            order.TechnicianName = dataReader.GetString(40).Trim();

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        #endregion

        #region FindNewOrders

        //TODO: setup upper limit in DB instead of hardcoding
        private const string SqlFindNewOrders =
            @"select *  from [h_order] 
                where serv_type = 4 
                and tran_type = 1
                and comp_type = 1
                and ticket_num > ?
                and ticket_num < '300000'
                and Date > ?
             order by Ticket_num asc";

        public static List<h_order> FindNewOrders(string latestImportedTicketNumber)
        {
            List<h_order> orders = new List<h_order>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNewOrders, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@ticket_num", latestImportedTicketNumber);
                Database.PutParameter(dbCommand, "@Date", DateTime.Now.AddDays(-20));

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        orders.Add(Load(dataReader));
                    }
                }
            }

            return orders;
        }

        #endregion


        #region GetLatestOrderNumber

        private const string SqlGetLatestOrderNumber =
            @"select top 1 * from [h_order] 
                order by ticket_num desc";

        private static string GetLatestOrderNumber()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetLatestOrderNumber, ConnectionKeyEnum.Servman))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader).ticket_num;
                }
            }

            throw new DataNotFoundException("");

        }

        #endregion

        #region GetFirstOrderNumber

        private const string SqlGetFirstOrderNumber =
            @"select top 1 * from [h_order] 
                order by ticket_num";

        private static string GetFirstOrderNumber()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetFirstOrderNumber, ConnectionKeyEnum.Servman))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader).ticket_num;
                }
            }

            throw new DataNotFoundException("");

        }

        #endregion

        #region FindByPrimaryKey

        public static h_order FindByPrimaryKey(String ticket_num, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
                Database.PutParameter(dbCommand, "@ticket_num", ticket_num);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("h_order not found, search by primary key");
        }

        #endregion
    }
}
      