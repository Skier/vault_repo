/*
 *  $RCSfile: FileListener.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CdFileListener.h: interface for the CCdFileListener class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDFILELISTENER_H__1AE425D0_B6F0_4990_9959_A2A028B22E6E__INCLUDED_)
#define AFX_CDFILELISTENER_H__1AE425D0_B6F0_4990_9959_A2A028B22E6E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IListener.h>

class CFileListener : public IListener {
public:
    CFileListener(CExecutorPool &executors, CThreadPool &threads):
        IListener(executors, threads), 
        m_stopped(false), 
        m_retryUncompleted(false)
    {
        m_handles[0] = NULL;
        m_handles[1] = NULL;
    };
    virtual ~CFileListener() {
        if ( NULL != m_handles[0] ) {
            CloseHandle(m_handles[0]);
        }
        if ( NULL != m_handles[1] ) {
            CloseHandle(m_handles[1]);
        }
    };
public:
    void Configure(ICfg & cfg);
    void Initialize();
    void Stop();
    int Run(CThread * thisThread);
private:
    CString GetNextFile();
    void RetryUncompleted();
private:
    HANDLE   m_handles[2];
    CString  m_srcDir;
    CString  m_workDir;
    bool     m_stopped;
    bool     m_retryUncompleted;
};

#endif // !defined(AFX_CDFILELISTENER_H__1AE425D0_B6F0_4990_9959_A2A028B22E6E__INCLUDED_)
