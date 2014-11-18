/*
 *  $RCSfile: SystemException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// SystemException.h: interface for the CSystemException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_SYSTEMEXCEPTION_H__C683C47D_8412_4497_A919_1A32331055FA__INCLUDED_)
#define AFX_SYSTEMEXCEPTION_H__C683C47D_8412_4497_A919_1A32331055FA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Exception.h>
#include <iostream>

class CSystemException : public CAtfException {
public:
    CSystemException(const char *fileName, int lineNo, const char *message):
                      CAtfException(fileName, lineNo, ATF_SYSTEM_ERR, message) {
        CString msg = SysErrorMsg(SysErrorCode());
        DWORD e = SysErrorCode(); 
        char tmp[20];
        ltoa(e, tmp, 10);
        msg += "[";
        msg += tmp;
        msg += "]";
        m_message += msg;
    };
    virtual ~CSystemException(){};

};

#define THROW_SYSTEM_EXCEPTION(message) \
    throw CSystemException( __FILE__, __LINE__, message)

#endif // !defined(AFX_SYSTEMEXCEPTION_H__C683C47D_8412_4497_A919_1A32331055FA__INCLUDED_)
