using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
    internal class Tract
    {
        private TractDataMapper m_tractDM;
        
        public List<TractListInfo> GetDrawingList() {
            List<TractListInfo> result;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                result = TractDM.GetDrawingList(tran);

                tran.Commit();
            }
            
            return result;
        }

        public List<TractListInfo> GetTractList(int documentId) {
            List<TractListInfo> result;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                result = TractDM.GetTractList(tran, documentId);

                tran.Commit();
            }
            
            return result;
        }

        public List<TractListInfo> GetRecentUserTractList(int userId) {
            List<TractListInfo> result;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                result = TractDM.GetRecentTractList(tran, userId);

                tran.Commit();
            }
            
            return result;
        }

        public TractInfo OpenTract(int tractId) {
            TractInfo tract;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                tract = OpenTract(tran, tractId);
                
                tran.Commit();
            }
            
            return tract;
        }

        public TractInfo OpenTract(SqlTransaction tran, int tractId){
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
