/*
 *  $RCSfile: StreamIOHandler.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StreamIOHandler.cpp: implementation of the CStreamIOHandler class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/StreamIOHandler.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

size_t CStreamIOHandler::LoadBuffer(char * buffer, size_t bufferSize) {
    size_t len = m_stream.read(buffer, bufferSize).gcount();
    return len;
};    
