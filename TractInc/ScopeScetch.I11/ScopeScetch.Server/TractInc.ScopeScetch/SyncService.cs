using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using TractInc.ScopeScetch.Data;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch
{
    public class SyncService
    {
        #region Fields

        private TractDataMapper m_tractDM;

        #endregion

        #region Methods

        public List<TractListInfo> GetTractReferenceNameList() {
            List<TractListInfo> result;
            
            using (SqlConnection conn = SqlHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                result = TractDM.GetReferenceNamesList(tran);
            }
            
            return result;
        }

        public List<Tract> GetLatestChanges(int userId, string deviceId) {
            SyncLogDataMapper syncDM = new SyncLogDataMapper(userId, deviceId);
            
            List<Tract> tractList;
            
            using (SqlConnection conn = SqlHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                if (syncDM.IsInitSyncExists(tran)) {
                    syncDM.DeleteDirtySyncRecords(tran);
                } else {
                    syncDM.CreateInitRecord(tran);
                }

                tractList = TractDM.GetModifiedTractsForUser(tran, userId, deviceId, true);

                if (tractList.Count > 0) {
                    syncDM.CreateDirtyRecord(tran);
                }
                
                tran.Commit();
            }
            
            return tractList;
        }
        
        public void DataReceived(int userId, string deviceId) {
            SyncLogDataMapper syncDM = new SyncLogDataMapper(userId, deviceId);
            
            using (SqlConnection conn = SqlHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                syncDM.ClearDirtyFlag(tran);
                tran.Commit();
            }
        }
        
        public Hashtable AcceptClientChanges(Tract[] tractList, int userId, string deviceId) {

            Hashtable newTractsMap = new Hashtable();
            
            using (SqlConnection conn = SqlHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                if (TractDM.GetModifiedTractsForUser(tran, userId, deviceId, false).Count > 0) {
                    throw new InvalidOperationException("User hasn't last database changes");
                }

                foreach (Tract tract in tractList) {
                    if (tract.TractId > 0) {
                        TractDM.Update(tran, tract, userId);
                    } else {
                        TractDM.Create(tran, tract, userId);
                        newTractsMap.Add(tract.Uid, tract.TractId);
                    }
                }

                if (tractList.Length > 0) {
                    SyncLogDataMapper syncDM = new SyncLogDataMapper(userId, deviceId);
                    syncDM.CreateDirtyRecord(tran);
                    syncDM.ClearDirtyFlag(tran);
                }
                
                tran.Commit();
            }
            
            return newTractsMap;
        }

        #endregion

        #region Properties

        private TractDataMapper TractDM {
            get {
                if (null == m_tractDM) {
                    m_tractDM = new TractDataMapper();
                }
                
                return m_tractDM;
            }
        }

        #endregion
    }
}
