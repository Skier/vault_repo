/*
 *  $RCSfile: MqException.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// MqException.cpp: implementation of the CMqException class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/MqException.h>
#include <atf/ErrorConst.h>


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CMqException::CMqException(const char *fileName, int lineNo, HRESULT result, const char *message):
              CAtfException(fileName, lineNo, ATF_MQ_ERR, message), m_result(result){
// TODO add message text for MQ result code
}
