using System;
using System.Collections.Generic;
using System.Text;

namespace dalworth.domain
{
    public class EquipmentRequest
    {
        public EquipmentRequest(string name, int count)
        {
            m_name = name;
            m_count = count;
        }

        private string m_name;
        private int m_count;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public int Count
        {
            get { return m_count; }
            set { m_count = value; }
        }
    }
}
