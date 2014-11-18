using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public class Salutation
    {
        #region Costructors

        public Salutation()
        {

        }

        public Salutation(String name)
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

        public static List<Salutation> Find()
        {
            List<Salutation> list = new List<Salutation>();
            list.Add(new Salutation(String.Empty));
            list.Add(new Salutation("Dr."));
            list.Add(new Salutation("Miss"));
            list.Add(new Salutation("Mr."));
            list.Add(new Salutation("Mrs."));
            list.Add(new Salutation("Ms."));
            list.Add(new Salutation("Prof."));

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
