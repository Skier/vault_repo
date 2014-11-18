/*
 *  $RCSfile: Exception.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Exception.cpp: implementation of the CTagException class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/Exception.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

    /* Return last system error code */
unsigned long SysErrorCode() {
    return GetLastError();
};

    /* Return system error message */
CString SysErrorMsg(unsigned long err_code) {
    void * msg_buf;
     if ( 0 ==  err_code ) {
        err_code = SysErrorCode();
    }
    FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM,
            NULL, err_code,
            MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
            (LPTSTR)&msg_buf,
            0, NULL);

    CString result = (const char *)msg_buf;
    LocalFree(msg_buf);
    return result;
};


