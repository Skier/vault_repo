using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Security;
using System.Threading;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.SDK;
using Dalworth.Server.Servman.Domain;
using System.Text.RegularExpressions;

using Configuration=Dalworth.Server.SDK.Configuration;

namespace Dalworth.Server.Web
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class ServerSyncService : System.Web.Services.WebService
    {
        public ServerSyncService()
        {
            Configuration.ConnectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            Configuration.ServmanConnectionString = ConfigurationManager.ConnectionStrings["Servman"].ConnectionString;
            Configuration.Login = ConfigurationManager.AppSettings["Login"];
            Configuration.Password = ConfigurationManager.AppSettings["Password"];
        }

        private static void ValidateConnectionKey(string connectionKey, IDbConnection connection)
        {
            string hash = Hash.ComputeHash(connectionKey);
            try
            {
                ConnectionKey key = ConnectionKey.FindByPrimaryKey(hash, connection);
                if (!key.IsActive)
                    throw new SecurityException("Connection key is inactive");
            }
            catch (DataNotFoundException)
            {
                throw new SecurityException("Connection key is invalid");
            }            
        }

        [WebMethod]

        public string SubmitShortLead(int projectTypeId, string servmanTechId, int businessPartnerId, 
            string firstName, string lastName, 
             string phone1,  string email, string customerNotes, string advertisingSourceAcronym,
             string servmanAdvertisingSource, string servmanTrackCode
            )
        {
            Lead lead = new Lead();
            String result;

            Dictionary<string, string> errors = lead.Submit(projectTypeId, servmanTechId, businessPartnerId,
                firstName, lastName, phone1, email, customerNotes, advertisingSourceAcronym,
                servmanAdvertisingSource, servmanTrackCode);

            if (errors.Count == 0)
            {
                result = "ACK=OK&LEADID=" + lead.ID;
                return result;
            }

            result = "ACK=ERROR";

            foreach (KeyValuePair<string, string> kvp in errors)
            {
                result += "&" + kvp.Key + "=" + kvp.Value;
            }
            return result;
        }

        [WebMethod]

        public string SubmitFullLead(int projectTypeId, string servmanTechId, int businessPartnerId, string company,
            string firstName, string lastName, string address1, string address2,
            string city, string state, string zip, string phone1, string phone2, string email, string customerNotes,
            string preferredServiceDate, string preferredTime, string advertisingSourceAcronym,
            string servmanAdvetisingSource, string servmanTrackCode)
        {
            Lead lead = new Lead();
            String result;

            Dictionary <string, string> errors = 
                lead.Submit(projectTypeId, servmanTechId, businessPartnerId, company,
                                firstName, lastName, address1, address2,
                                city, state, zip, phone1, phone2, email, customerNotes,
                                preferredServiceDate, preferredTime, advertisingSourceAcronym,
                                servmanAdvetisingSource, servmanTrackCode);

            if (errors.Count == 0)
            {
                result = "ACK=OK&LEADID=" + lead.ID;
                return result;
            }

            result = "ACK=ERROR";

            foreach (KeyValuePair<string, string> kvp in errors)
            {
                result += "&" + kvp.Key + "=" + kvp.Value;
            }
            return result;
        }

        [WebMethod]
        public List<h_order> GetCustomerOrderHistory(string connectionKey, string servmanCustId)
        {
            int tryNumber = 1;

            while(true)
            {
                try
                {
                    using (new Impersonisation())
                    {                        
                        using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                        {
                            connection.Open();
                            ValidateConnectionKey(connectionKey, connection);
                        }

                        using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
                        {
                            connection.Open();
                            List<h_order> result = h_order.FindByCustomer(servmanCustId, connection);
                            foreach (h_order order in result)
                            {
                                order.ServiceTypeText = h_order.GetServiceTypeText(order);
                                order.CompletionTypeText = h_order.GetCompletionTypeText(order);
                            }

                            return result;
                        }
                    }
                }
                catch (Exception)
                {
                    if (tryNumber < 5)
                    {
                        tryNumber++;
                        continue;
                    }

                    throw;
                }                
            }
        }

        [WebMethod]
        public void SendMessage(string connectionKey, string servmanTruckId, string message)
        {
            int tryNumber = 1;

            while (true)
            {
                try
                {
                    using (new Impersonisation())
                    {
                        using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
                        {
                            connection.Open();
                            ValidateConnectionKey(connectionKey, connection);
                        }

                        using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
                        {
                            connection.Open();
                            PagerMessage.Send(servmanTruckId, message, connection);
                            return;
                        }                
                    }
                }
                catch (Exception)
                {
                    if (tryNumber < 5)
                    {
                        tryNumber++;
                        continue;
                    }

                    throw;
                }
            }
        }

//        [WebMethod]
//        public VisitCompleteResultPackage CompleteVisit(string connectionKey, int? dispatchId,
//            VisitCompletePackage completePackage)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//               
//                if (WorkTransaction.IsExistTodaysTransactionOrVisitCompleted(completePackage.VisitId,
//                    completePackage.Work.ID,
//                    WorkTransactionTypeEnum.VisitCompleted, connection))
//                {
//                    return null;
//                }
//
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    VisitCompleteResultPackage result = completePackage.CompleteVisit(dispatchId, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                    return result;
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }
//        }
//
//        [WebMethod]
//        public Message GetIncomingMessage(string connectionKey, int technicianId)
//        {            
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                return Message.FindBy(technicianId, connection);
//            }            
//        }
//
//        [WebMethod]
//        public void NotifyMessageReceived(string connectionKey, int messageId)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                Message.Delete(new Message(messageId), connection);
//            }            
//        }
//
//        [WebMethod]
//        public void NoGo(string connectionKey, int technicianId, int visitId)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                if (WorkTransaction.IsExistTodaysTransactionOrVisitCompleted(technicianId, visitId,
//                    WorkTransactionTypeEnum.NoGo, connection))
//                {
//                    return;
//                }
//
//
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {                    
//                    Visit.NoGo(technicianId, visitId, true, null, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }            
//        }
//
//        [WebMethod]
//        public void Etc(string connectionKey, int technicianId, int visitId, decimal saleAmount, int? hours, int? minutes, string notes)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    Visit.Etc(technicianId, visitId, saleAmount, hours, minutes, notes, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }    
//        }
//
//        [WebMethod]
//        public void Gps(string connectionKey, int workId, float latitude, float longitude, DateTime gpsTime)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    WorkTransaction.SaveGpsInfo(workId, latitude, longitude, gpsTime, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }    
//        }
//        [WebMethod]
//        public void ArrivedToVisit(string connectionKey, int technicianId, int visitId)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//
//                if (WorkTransaction.IsExistTodaysTransactionOrVisitCompleted(technicianId, visitId,
//                    WorkTransactionTypeEnum.VisitArrived, connection))
//                {
//                    return;
//                }
//
//
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    Visit.Arrive(technicianId, visitId, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }    
//        }
//
//        [WebMethod]
//        public void AcceptVisit(string connectionKey, int technicianId, int visitId)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//
//                if (WorkTransaction.IsExistTodaysTransactionOrVisitCompleted(technicianId, visitId, 
//                    WorkTransactionTypeEnum.VisitAccepted, connection))
//                {
//                    return;
//                }
//
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    Visit.Accept(technicianId, visitId, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }    
//        }
//
//        [WebMethod]
//        public void DeclineVisit(string connectionKey, int technicianId, int visitId, string reason)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//
//                if (WorkTransaction.IsExistTodaysTransactionOrVisitCompleted(technicianId, visitId,
//                    WorkTransactionTypeEnum.VisitDeclined, connection))
//                {
//                    return;
//                }
//
//
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    Visit.Decline(technicianId, visitId, reason, true, null, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }    
//        }
//
//
//        [WebMethod]
//        public List<Employee> GetEmployees(string connectionKey)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                return Employee.Find(connection);
//            }            
//        }
//
//        [WebMethod]
//        public bool IsTodayWorkExist(string connectionKey, int technicianEmployeeId)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                Work work = Work.FindPendingWork(technicianEmployeeId, DateTime.Now, connection);
//                if (work != null)
//                    return true;
//                return false;
//            }            
//        }
//
//        [WebMethod]
//        public StartDayPackage GetStartDayPackage(string connectionKey, int technicianEmployeeId)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                return StartDayPackage.GetStartDayPackage(technicianEmployeeId, DateTime.Now, connection);
//            }            
//        }
//
//        [WebMethod]
//        public void SaveStartDayDone(string connectionKey, StartDayDonePackage package)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    StartDayDonePackage.SaveStartDayDone(package, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }    
//        }
//
//        [WebMethod]
//        public VisitPackage GetVisit(string connectionKey, int visitId)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                return VisitPackage.GetVisit(visitId, connection);
//            }            
//        }
//
//        [WebMethod]
//        public void CompleteWork(string connectionKey, int workId, int unloadInventoryRoomId,
//            List<string> unloadEquipmentSerialNumbers)
//        {
//            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
//            {
//                connection.Open();
//                ValidateConnectionKey(connectionKey, connection);
//                IDbTransaction transaction = connection.BeginTransaction();
//
//                try
//                {
//                    Work.CompleteWork(workId, unloadEquipmentSerialNumbers,
//                        unloadInventoryRoomId, connection);
//                    DashboardState.MakeDashboardDirty(connection);
//                    transaction.Commit();
//                }
//                catch (Exception)
//                {
//                    transaction.Rollback();
//                    throw;
//                }
//            }    
//        }
//
    }
}
