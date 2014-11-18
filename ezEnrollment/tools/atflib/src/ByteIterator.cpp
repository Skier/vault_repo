/*
 *  $RCSfile: ByteIterator.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ByteIterator.cpp: implementation of the CByteIterator class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/ByteIterator.h>
#include <atf/NoMoreDataException.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
BYTE CByteIterator::GetNext() {
    if ( m_pos >= m_stream.GetLength() ) {
       THROW_NO_MORE_DATA_EXCEPTION("", m_pos+1, m_stream.GetLength());
    }
    const BYTE* buf = m_stream.GetBuffer();
    return buf[m_pos++];
};

size_t CByteIterator::Rest() {
    return m_stream.GetLength() - m_pos;
};

void CByteIterator::GetNextN(BYTE* buffer, size_t length) {
    if ( m_pos + length > m_stream.GetLength() ) {
       THROW_NO_MORE_DATA_EXCEPTION("", m_pos+length, m_stream.GetLength());
    } 
    const BYTE* buf = m_stream.GetBuffer();
    memcpy(buffer, buf+m_pos, length);
    m_pos += length;
};

void   CByteIterator::Reset() {
    m_pos = 0;
};
