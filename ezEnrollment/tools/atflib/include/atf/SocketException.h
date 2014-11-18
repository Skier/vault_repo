/*
 *  $RCSfile: SocketException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// SocketException.h: interface for the CSocketException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_SOCKETEXCEPTION_H__85BAADDF_925A_4E0B_A587_CB5B31EB7587__INCLUDED_)
#define AFX_SOCKETEXCEPTION_H__85BAADDF_925A_4E0B_A587_CB5B31EB7587__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Exception.h>
#include <winsock2.h>

class CSocketException : public CAtfException {
public:
    CSocketException(const char *fileName, int lineNo, const char *message):
                      CAtfException(fileName, lineNo, ATF_SOCKET_ERR, message) {
        CString msg = SysErrorMsg(WSAGetLastError());
        DWORD e = SysErrorCode(); 
        char tmp[20];
        ltoa(e, tmp, 10);
        msg += "[";
        msg += tmp;
        msg += "]";
        m_message += msg;
    };
    virtual ~CSocketException(){};

};

#define THROW_SOCKET_EXCEPTION(message) \
{\
    throw CSocketException( __FILE__, __LINE__, message); \
}

#endif // !defined(AFX_SOCKETEXCEPTION_H__85BAADDF_925A_4E0B_A587_CB5B31EB7587__INCLUDED_)
