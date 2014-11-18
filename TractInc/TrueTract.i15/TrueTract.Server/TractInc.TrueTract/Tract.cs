using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
    internal class Tract
    {
        private TractDataMapper m_tractDM;
        
        public List<TractInfo> GetUserDrawings(int userId) {
            List<TractInfo> result;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                result = TractDM.GetDrawingsByUser(tran, userId, false);

                tran.Commit();
            }
            
            return result;
        }

        public List<TractInfo> GetUserRecentDrawings(int userId) {
            List<TractInfo> result;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                result = TractDM.GetRecentDrawings(tran, userId, false);

                tran.Commit();
            }
            
            return result;
        }
        
        public List<TractInfo> GetDrawingsByRefName(string refName) {
            List<TractInfo> result;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                result = TractDM.GetTractsByRefName(tran, refName, false);

                tran.Commit();
            }
            
            return result;
        }
        
        public List<TractInfo> GetTractList(int documentId) {
            List<TractInfo> result;

            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                result = TractDM.GetTractsByDocId(tran, documentId, false);

                tran.Commit();
            }

            return result;
        }

//        public List<TractInfo> GetRecentUserTractList(int userId) {
//            List<TractInfo> result;
//            
//            using (SqlConnection conn = SQLHelper.CreateConnection()) 
//            {
//                conn.Open();
//
//                SqlTransaction tran = conn.BeginTransaction();
//                
//                result = TractDM.GetRecentTractList(tran, userId);
//
//                tran.Commit();
//            }
//            
//            return result;
//        }

        public TractInfo LoadTract(int tractId) {
            TractInfo tract;

            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                tract = LoadTract(tran, tractId);

                tran.Commit();
            }

            return tract;
        }

        public TractInfo LoadTract(SqlTransaction tran, int tractId){
            return TractDM.GetByTractId(tran, tractId, true);
        }
        
        public TractInfo SaveTract(TractInfo tract, int userId) {

            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                SaveTract(tran, tract, userId);
                
                tran.Commit();
            }
            
            return tract;
        }

        public TractInfo SaveTract(SqlTransaction tran, TractInfo tract, int userId) {
            if (tract.TractId > 0) {
                TractDM.Update(tran, tract, userId);
            } else {
                TractDM.Create(tran, tract, userId);
            }
            
            return tract;
        }

        public TractDataMapper TractDM {
            get {
                if (null == m_tractDM) {
                    m_tractDM = new TractDataMapper();
                }
                
                return m_tractDM;
            }
        }

    }
}
