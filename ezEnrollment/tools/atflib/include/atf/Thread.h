/*
 *  $RCSfile: Thread.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Thread.h: interface for the CThread class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_THREAD_H__EE158F75_B68C_4843_AC64_C12F352F268A__INCLUDED_)
#define AFX_THREAD_H__EE158F75_B68C_4843_AC64_C12F352F268A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IRunnable.h>
#include <atf/SystemException.h>
#include <atf/Logger.h>

#pragma warning( disable : 4290 ) 

class CThread {
public:
    CThread(IRunnable *body);
    virtual ~CThread();
    void Create();
    void Start();
    void Suspend();
    void Resume();
    IRunnable & GetBody() {return *m_body;};
    bool WaitForStop(size_t timeout) throw (CSystemException);
public:
    void EnableLogging(ILogger *logger){m_logger = logger;};
    ILogger& GetLogger(){ return *m_logger;};
protected:
    virtual void CreateNativeThread() throw (CSystemException);
protected:
    HANDLE     m_thread;
    IRunnable *m_body;
    ILogger   *m_logger;
    bool       m_started;
    size_t     m_suspended;
};

#endif // !defined(AFX_THREAD_H__EE158F75_B68C_4843_AC64_C12F352F268A__INCLUDED_)
