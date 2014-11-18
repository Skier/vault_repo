/*
 *  $RCSfile: StringIOHandler.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StringIOHandler.cpp: implementation of the CStringIOHandler class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/StringIOHandler.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

size_t CStringIOHandler::LoadBuffer(char * buffer, size_t bufferSize) {
    size_t len = m_body.GetLength();
    size_t count = 0;
    while ( m_pos < len && count < bufferSize) {
        buffer[count++] = m_body.GetAt(m_pos++);
    }
    return count;
};    
