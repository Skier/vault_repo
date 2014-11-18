using System;
using System.Collections.Generic;
using System.Data;

namespace TractInc.Server.Data
{

    public interface IResultSetAdapterModel<T>
    {
        T CreateInstance(IDataReader dataReader);
    }

    public class ResultSetAdapter<T> : IDisposable
    {
        #region Fields

        List<T> m_resultSet;

        IResultSetAdapterModel<T> m_model;

        int m_rowCount = -1;

        public int RowCount
        {
            get { return m_rowCount; }
        }



        #endregion

        #region Constructor
        public ResultSetAdapter(IDbCommand dbCommand,IResultSetAdapterModel<T> model)
        {
            m_model = model;

            OnCommandChanged(dbCommand);
        }
        #endregion

        #region OnCommandChanged
        void OnCommandChanged(IDbCommand dbCommand)
        {
            CleanUp();
            m_resultSet = new List<T>();

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                    m_resultSet.Add(m_model.CreateInstance(dataReader));
            }

            m_rowCount = m_resultSet.Count;
        }
        #endregion

        #region CleanUp
        private void CleanUp()
        {
            if (m_resultSet != null)
            {
               m_resultSet.Clear();
               m_resultSet = null;
               m_rowCount = 0;

            }
        }
        #endregion

        #region GetObjectAt
        public T GetObjectAt(int rowIndex)
        {
            return m_resultSet[rowIndex];
        }
        #endregion        

        #region IDisposable Members

        public void Dispose()
        {
            CleanUp();
        }

        #endregion

        public T this[int index]
        {
            get
            {
                return GetObjectAt(index);
            }
        }
    }
}
