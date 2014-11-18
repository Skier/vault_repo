using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech
{
    public class BitExtract
    {
        public  byte[] m_bytes;

        public BitExtract(byte[] byteArray)
        {
            m_bytes = byteArray;
        }

        public byte[] Bytes
        {
            get
            {
                return m_bytes;
            }
        }

        public byte SetBit( int bit , bool val)
        {
            if (m_bytes==null)
                return 0;

            int i = bit / 8;

            int j = bit % 8;

            byte mask = (byte)(1 << j);

            if(val)
                return m_bytes[i] = (byte)(m_bytes[i] | mask);
            else
                return m_bytes[i] = (byte)(m_bytes[i] & ~mask);

        }


        public bool GetBit(int bit)
        {
            if (m_bytes==null)
                return false;

            int i = bit / 8;

            int j = bit % 8;

            return ( ( ( m_bytes[i] & (byte)( 1 << j ) ) != 0 ) ? 1 : 0 ) == 1;
        }
    }

}
