using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.QBSDK;
using QuickBooksAgent.Windows.UI.Synchronize.Basic;

namespace QuickBooksAgent.Windows.UI.Synchronize.Details
{
    public class SynchronizeDetailsModel:ITableModel
    {

        public class Detail
        {
            public Detail(Synchronizer sycnronizer, QBResponseStatus status)
            {
                m_synchronizer = sycnronizer;
                m_status = status;
            }

            Synchronizer m_synchronizer;

            public Synchronizer Synchronizer
            {
                get { return m_synchronizer; }
            }

            QBResponseStatus m_status;

            public QBResponseStatus Status
            {
                get { return m_status; }
            }

            public String Operation
            {
                get
                {
                    if (m_status.CommandType == QBCommandTypeEnum.Add)
                        return "Add";
                    else if (m_status.CommandType == QBCommandTypeEnum.Delete)
                        return "Delete";
                    else if (m_status.CommandType == QBCommandTypeEnum.Update)
                        return "Modify";
                    else if (m_status.CommandType == QBCommandTypeEnum.Query)
                        return "Query";
                    else
                        return "Unknown";
                }
            }

            public String Result
            {
                get
                {
                    if (m_status.IsError)
                        return "Error";
                    else if (m_status.CommandType == QBCommandTypeEnum.Query)
                    {
                        if (m_status.DomainObjects.Count > 1)
                            return String.Format("{0} Items", m_status.DomainObjects.Count);
                        else if (m_status.DomainObjects.Count == 1)
                            return "1 Item";
                        else
                            return "No Items";
                     }
                    else
                        return "OK";
                }
            }
        }

        List<Synchronizer> m_sychronizerList;

        List<Detail> m_detailList = new List<Detail>();

        internal void Init(List<Synchronizer> sychronizerList)
        {
            m_sychronizerList = sychronizerList;

            foreach (Synchronizer synchronizer in m_sychronizerList)
            {
                foreach (QBResponseStatus status in synchronizer.QBReader.ResponseStatus)
                { 
                    m_detailList.Add(new Detail(synchronizer,status));
                }
            }
        }

        #region ITableModel Members

        public int GetRowCount()
        {
            return m_detailList.Count;
        }

        public int GetColumnCount()
        {
            return 3;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Synchronizer";
            else if (columnIndex == 1)
                return "Operation";
            else
                return "Result";
        }

        public Type GetColumnClass(int columnIndex)
        {
            return String.Empty.GetType();
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_detailList[rowIndex].Synchronizer.Name;
            else if (columnIndex == 1)
                return m_detailList[rowIndex].Operation;
            else
                return m_detailList[rowIndex].Result;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_detailList[rowIndex];
        }

        public Detail GetObject(int rowIndex, int columnIndex)
        {
            return m_detailList[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion
    }
}
