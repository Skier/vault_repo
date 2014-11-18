/*
 *  $RCSfile: PoolManager.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// PoolManager.h: interface for the CPoolManager class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_POOLMANAGER_H__477E8D75_DD88_43FB_B6AE_E03E0FF4C91F__INCLUDED_)
#define AFX_POOLMANAGER_H__477E8D75_DD88_43FB_B6AE_E03E0FF4C91F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CObjectPool;

#include <atf/IRunnable.h>
#include <atf/Thread.h>
#include <AFXWIN.H>

struct CShedule {
    DWORD   m_interval;
    DWORD   m_lastShrink;
    CObjectPool & m_pool;

    CShedule * m_next;

    CShedule(DWORD interval, CObjectPool &pool):
        m_pool(pool), 
        m_interval(interval*1000), 
        m_lastShrink(GetTickCount()), m_next(NULL){};
};

class CPoolManager :public IRunnable {
public:
    CPoolManager();
    virtual ~CPoolManager();
public:
    virtual void Shrink(CShedule *shedule);
    void manage(long interval, CObjectPool &pool); 
    int Run(CThread * thisThread);

public:
    void EnableLogging(ILogger *logger){m_thread->EnableLogging(logger);};
    void Start();
    void Stop();
private:
    HANDLE    m_sheduleMutex;
    HANDLE    m_addedEvent;
    HANDLE    m_stopEvent;
    CThread  *m_thread;
    CShedule *m_shedule;
    DWORD     m_minInterval;
    bool      m_started;
};

#endif // !defined(AFX_POOLMANAGER_H__477E8D75_DD88_43FB_B6AE_E03E0FF4C91F__INCLUDED_)
