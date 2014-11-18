using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
#if WINCE
using System.Data.SqlServerCe;
#endif

namespace QuickBooksAgent.Data
{

    public interface IResultSetAdapterModel<T>
    {
        T CreateInstance(IDataReader dataReader);
    }

    public class ResultSetAdapter<T> : IDisposable
    {
        #region Fields



#if WINCE

        int m_startIndex;

        int m_endIndex;

        List<T> m_itemBuffer = new List<T>();

        System.Data.SqlServerCe.SqlCeResultSet m_resultSet;

        const int BufferSize = 20;
#else
        List<T> m_resultSet;
#endif

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
#if WINCE
            SqlCeCommand ceCommand = (SqlCeCommand)dbCommand;

            m_resultSet = ceCommand.ExecuteResultSet(ResultSetOptions.Insensitive | ResultSetOptions.Scrollable);

            m_resultSet.ReadLast();

            if (m_resultSet.HasRows)
                m_rowCount = ((System.Collections.ICollection)m_resultSet.ResultSetView).Count;
            else
                m_rowCount = 0;
#else
            m_resultSet = new List<T>();

            using(IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while(dataReader.Read())
                    m_resultSet.Add(m_model.CreateInstance(dataReader));
            }

            m_rowCount = m_resultSet.Count;
#endif
        }
        #endregion

        #region CleanUp
        private void CleanUp()
        {
            if (m_resultSet != null)
            {
        #if WINCE
               m_resultSet.Close();
               m_itemBuffer.Clear();
        #else
                m_resultSet.Clear();
        #endif

               m_resultSet = null;
               m_rowCount = 0;

            }
        }
        #endregion

        #region GetObjectAt
        public T GetObjectAt(int rowIndex)
        {
#if WINCE
            if (rowIndex < m_startIndex || rowIndex > m_endIndex
                || m_itemBuffer.Count == 0)
            {
                UpdateBuffer(rowIndex);
            }

            return m_itemBuffer[rowIndex - m_startIndex];
#else
            return m_resultSet[rowIndex];
#endif

        }
        #endregion

        #region UpdateBuffer

#if WINCE
        private void UpdateBuffer(int targetIndex)
        {
            m_startIndex = targetIndex - BufferSize / 2 - 1;
            m_endIndex = targetIndex + BufferSize / 2 - 2;

            if (m_startIndex < 1)
            {
                m_endIndex += m_startIndex * -1;
                m_startIndex = 0;
            }

            if (m_endIndex > m_rowCount)
                m_endIndex = m_rowCount - 1;

            m_itemBuffer.Clear();

            int index = m_startIndex;

            while (m_resultSet.ReadAbsolute(index++))
                m_itemBuffer.Add(m_model.CreateInstance(m_resultSet));

        }
#endif
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
