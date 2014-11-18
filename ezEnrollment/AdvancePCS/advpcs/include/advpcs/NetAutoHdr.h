/*
 *  $RCSfile: NetAutoHdr.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_NET_AUTO_HDR_H__
#define __ADVPCS_NET_AUTO_HDR_H__

/* -------------------------- header place ---------------------------------- */
#include <windows.h>
#include <wininet.h>
/* -------------------------- implementation place -------------------------- */

class NetAutoHdr {
public:
    NetAutoHdr() : m_handle(NULL){};
    virtual ~NetAutoHdr() {
        Free();
    };

    HINTERNET operator=(HINTERNET handle) {
        Free();
        m_handle = handle;
        return m_handle;
    };

    void Free() {
        if ( NULL != m_handle ) {
            InternetCloseHandle(m_handle);
        }
        m_handle = NULL;
    }

    bool IsEmpty() const {
        return NULL == m_handle;
    };

    operator HINTERNET() {
        return m_handle;
    };
private:
    HINTERNET m_handle;
};

#endif /* __ADVPCS_NET_AUTO_HDR_H__ */
