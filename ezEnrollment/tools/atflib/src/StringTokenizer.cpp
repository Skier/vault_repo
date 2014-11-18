/*
 *  $RCSfile: StringTokenizer.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StringTokenizer.cpp: implementation of the CStringTokenizer class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/StringTokenizer.h>
#include <atf/NoMoreDataException.h>

static const size_t NOT_FOUND = -1;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CString CStringTokenizer::nextToken(){
    CString result;
    int idx = m_arg.Find(m_delim, m_pos);
    if ( NOT_FOUND == idx ) {
        if ( m_pos >= m_arg.GetLength() ) {
            THROW_NO_MORE_DATA_EXCEPTION("No more tokens in string ["+m_arg+"]", m_arg.GetLength(), m_pos);
        }
        result = m_arg.Right(m_arg.GetLength() - m_pos);
        m_pos = m_arg.GetLength();
    } else {
        result = m_arg.Mid(m_pos, idx - m_pos);
        m_pos = idx+1;
    }
    return result;
};



