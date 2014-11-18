/*
 *  $RCSfile: Mutex.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Mutex.h: interface for the CMutex class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_MUTEX_H__5620AB64_A5B7_4C4F_A7CD_B7A72CA8F326__INCLUDED_)
#define AFX_MUTEX_H__5620AB64_A5B7_4C4F_A7CD_B7A72CA8F326__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <AFXWIN.H>

class CMutexLocker {
public:
    CMutexLocker( HANDLE mutex ) {
        m_mutex = mutex;
        WaitForSingleObject(m_mutex, INFINITE);
    };
    virtual ~CMutexLocker() {
        ReleaseMutex(m_mutex);
    };
private:
    HANDLE m_mutex;
};

#endif // !defined(AFX_MUTEX_H__5620AB64_A5B7_4C4F_A7CD_B7A72CA8F326__INCLUDED_)
