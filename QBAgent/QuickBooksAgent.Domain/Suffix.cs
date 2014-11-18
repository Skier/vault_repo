using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public class Suffix
    {
        #region Costructors

        public Suffix()
        {

        }

        public Suffix(String name)
        {
            m_name = name;
        }

        #endregion

        #region Name
        protected String m_name;
        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        #endregion

        #region Find

        public static List<Suffix> Find()
        {
            List<Suffix> list = new List<Suffix>();
            list.Add(new Suffix(String.Empty));
            list.Add(new Suffix("Esq."));
            list.Add(new Suffix("I"));
            list.Add(new Suffix("II"));
            list.Add(new Suffix("III"));
            list.Add(new Suffix("Jr."));
            list.Add(new Suffix("Sr."));

            return list;
        }
        #endregion

        #region ToString

        public override string ToString()
        {
            return m_name;
        }

        #endregion
    }
}
