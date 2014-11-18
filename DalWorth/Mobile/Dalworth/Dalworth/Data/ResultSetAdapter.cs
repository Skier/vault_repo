using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace Dalworth.Data
{

    public interface IResultSetAdapterModel<T>
    {
        T CreateInstance(IDataReader dataReader);
    }

    public class ResultSetAdapter<T> : IDisposable
    {
        #region Fields

        int m_startIndex;

        int m_endIndex;

        List<T> m_itemBuffer = new List<T>();

        SqlCeResultSet m_resultSet;

        const int BufferSize = 20;

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
            SqlCeCommand ceCommand = (SqlCeCommand)dbCommand;

            m_resultSet = ceCommand.ExecuteResultSet(ResultSetOptions.Insensitive | ResultSetOptions.Scrollable);

            m_resultSet.ReadLast();

            if (m_resultSet.HasRows)
                m_rowCount = ((System.Collections.ICollection)m_resultSet.ResultSetView).Count;
            else
                m_rowCount = 0;
        }
        #endregion

        #region CleanUp
        private void CleanUp()
        {
            if (m_resultSet != null)
            {
               m_resultSet.Close();
               m_itemBuffer.Clear();

               m_resultSet = null;
               m_rowCount = 0;

            }
        }
        #endregion

        #region GetObjectAt
        public T GetObjectAt(int rowIndex)
        {
            if (rowIndex < m_startIndex || rowIndex > m_endIndex
                || m_itemBuffer.Count == 0)
            {
                UpdateBuffer(rowIndex);
            }

            return m_itemBuffer[rowIndex - m_startIndex];

        }
        #endregion

        #region UpdateBuffer

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
