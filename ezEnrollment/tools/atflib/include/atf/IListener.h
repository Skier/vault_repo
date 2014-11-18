/*
 *  $RCSfile: IListener.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IListener.h: interface for the IListener class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDLISTENER_H__75F45C85_C6D9_4308_81BA_E7C13CAAE439__INCLUDED_)
#define AFX_CDLISTENER_H__75F45C85_C6D9_4308_81BA_E7C13CAAE439__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IRunnable.h>
#include <atf/Environment.h>
#include <atf/Logger.h>
#include <atf/Thread.h>

class CExecutorPool;
class CThreadPool;

class IListener : public IRunnable {
public:
    IListener(CExecutorPool &executors, CThreadPool &threads):
        m_logger(c_defaultEnvironment.GetLogger()), 
        m_executors(executors),m_threads(threads), m_thread(NULL){};
    virtual ~IListener(){};
public:
    virtual void Configure(ICfg & cfg) = 0;
    virtual void Initialize() = 0;
    
    virtual void Start(CThread* thread){
        m_thread=thread;
        m_thread->Start();
        LOG_DEBUG(GetLogger(), ATF_DEBUG, "Listener started");
    };
    virtual void Stop() = 0;
public:
    void EnableLogging(ILogger * logger){m_logger = logger;};
    ILogger& GetLogger() {return *m_logger;};
    CThread* GetThread() {return m_thread;};
protected:
    ILogger         *m_logger;
    CExecutorPool   &m_executors;
    CThreadPool     &m_threads;
    CThread         *m_thread;
};

#endif // !defined(AFX_CDLISTENER_H__75F45C85_C6D9_4308_81BA_E7C13CAAE439__INCLUDED_)
