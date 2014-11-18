/*
 *  $RCSfile: PoolableThread.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// PoolableThread.h: interface for the CPoolableThread class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_POOLABLETHREAD_H__58404395_6384_4D0B_B38F_02BF5CF9D861__INCLUDED_)
#define AFX_POOLABLETHREAD_H__58404395_6384_4D0B_B38F_02BF5CF9D861__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Thread.h>
#include <atf/ThreadPool.h>

class CPoolableThread : public CThread {
    friend DWORD WINAPI ThreadFunc( LPVOID lpParam );
public:
    CPoolableThread(IRunnable *body, CThreadPool & pool): CThread(body), m_stop(false), m_pool(pool) {
        m_runEvent = CreateEvent(NULL, false, true, NULL);
    };
    virtual ~CPoolableThread(){
        CloseHandle(m_runEvent);
    };

    void Run() {SetEvent(m_runEvent);}
    void StopSignal() { 
        m_stop = true; 
        Run();
    };
protected:
    virtual void CreateNativeThread() throw (CSystemException);
protected:
    bool   m_stop;
    HANDLE m_runEvent;
    CThreadPool& m_pool;
};

#endif // !defined(AFX_POOLABLETHREAD_H__58404395_6384_4D0B_B38F_02BF5CF9D861__INCLUDED_)
