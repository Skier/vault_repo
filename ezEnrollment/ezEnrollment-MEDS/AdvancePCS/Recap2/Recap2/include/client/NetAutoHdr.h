// NetAutoHdr.h: interface for the NetAutoHdr class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_NETAUTOHDR_H__9E00042A_4FE3_4568_A825_9F1CC66ED3FA__INCLUDED_)
#define AFX_NETAUTOHDR_H__9E00042A_4FE3_4568_A825_9F1CC66ED3FA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include <windows.h>
#include <wininet.h>

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

	bool IsEmpty() {
		return NULL == m_handle;
	};

	operator HINTERNET() {
		return m_handle;
	};
private:
    HINTERNET m_handle;
};

#endif // !defined(AFX_NETAUTOHDR_H__9E00042A_4FE3_4568_A825_9F1CC66ED3FA__INCLUDED_)
