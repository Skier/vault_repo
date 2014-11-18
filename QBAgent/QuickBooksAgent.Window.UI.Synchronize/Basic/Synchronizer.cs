using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QuickBooksAgent.QBSDK;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public abstract class Synchronizer
    {
        public void ProcessRequest(XmlWriter xmlWriter)
        {
            OnProcessRequest(xmlWriter);
        }

        protected virtual void OnProcessRequest(XmlWriter xmlWriter) { }

        public virtual void Reset()
        { }

        public virtual void ErrorAppears() { }

        public abstract QBResponseReader QBReader { get; }

        protected String m_name;

        public String Name
        {
            get { return m_name; }
        }
    }

    public abstract class Synchronizer<TQBReader, TDomainClass> : Synchronizer
        where TQBReader : QBResponseReader<TDomainClass>, new()
        where TDomainClass:class
    {
        TQBReader m_reader = new TQBReader();

        public TQBReader Reader
        {
            get { return m_reader; }
        }

        public override QBResponseReader QBReader
        {
            get { return m_reader; }
        }
    }

    public abstract class Synchronizer<TQBReader,TQBWriter,TDomainClass>:
        Synchronizer<TQBReader,TDomainClass>
        where TQBReader : QBResponseReader<TDomainClass>,new()
        where TQBWriter : QBRequestWriter<TDomainClass>,new()
        where TDomainClass:class
    {

        TQBWriter m_writer = new TQBWriter();

        public TQBWriter Writer
        {
            get { return m_writer; }
        }

        QBExpectedResponse<TDomainClass> m_expectedResponse;

        public QBExpectedResponse<TDomainClass> ExpectedResponse
        {
            get { return m_expectedResponse; }
        }

        protected override void OnProcessRequest(XmlWriter xmlWriter)
        {
            m_expectedResponse = m_writer.Process(xmlWriter);
        }

        public override void Reset()
        {
            base.Reset();

            if (m_expectedResponse != null)
            {
                m_expectedResponse.AddedObjects.Clear();
                m_expectedResponse.DeletedObjects.Clear();
                m_expectedResponse.QueriedObjects.Clear();
                m_expectedResponse.UpdatedObjects.Clear();
            }
        }
    }
}
