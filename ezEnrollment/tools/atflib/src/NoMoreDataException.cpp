/*
 *  $RCSfile: NoMoreDataException.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// NoMoreDataException.cpp: implementation of the CNoMoreDataException class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/NoMoreDataException.h>
#include <atf/ErrorConst.h>


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNoMoreDataException::CNoMoreDataException(
                          const char *fileName, 
                          int lineNo, 
                          size_t exists, 
                          size_t requested,
                          const char * message):
    CAtfException(
        fileName, 
        lineNo, 
        ATF_NO_MORE_DATA_ERR,message), 
    m_exists(exists), 
    m_requested(requested)
{
    m_message.Format("No more data.%s. Requested:%ld exists:%ld", message, m_requested, m_exists);
};

