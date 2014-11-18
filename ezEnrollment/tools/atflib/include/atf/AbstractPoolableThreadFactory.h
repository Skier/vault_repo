/*
 *  $RCSfile: AbstractPoolableThreadFactory.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// AbstractPoolableThreadFactory.h: interface for the CAbstractPoolableThreadFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ABSTRACTPOOLABLETHREADFACTORY_H__1369EB45_29C2_4514_9F99_8D0C1558C358__INCLUDED_)
#define AFX_ABSTRACTPOOLABLETHREADFACTORY_H__1369EB45_29C2_4514_9F99_8D0C1558C358__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IThreadFactory.h>
#include <atf/ThreadPool.h>
#include <atf/PoolableThread.h>
#include <atf/IRunnable.h>
#include <atf/ExceptionLogger.h>
#include <atf/Environment.h>

class CAbstractPoolableThreadFactory : public IThreadFactory {
public:
    CAbstractPoolableThreadFactory():m_pool(NULL) {};

    CThread* Create() { 
        IRunnable *runnable = CreateBody();
        CPoolableThread *thread =  new CPoolableThread(runnable, *m_pool);
        thread->EnableLogging(c_defaultEnvironment.GetLogger());
        return thread;
    };

    void  Destroy(CThread * obj) {
        CPoolableThread * pt = (CPoolableThread*)obj;
        try {
            pt->StopSignal();
            if ( obj->WaitForStop(INFINITE) ) {
                delete obj;
            } else {
                LOG_WARN(pt->GetLogger(), ATF_WARNING, "Can't free thread object");
            }
        } catch (CSystemException &se) {
            CExceptionLogger::Log(pt->GetLogger(),se);
        }
    };

    void SetPool(CThreadPool *pool) {m_pool = pool;};
    CThreadPool& GetPool() {return *m_pool;};

protected:
    virtual IRunnable * CreateBody() = 0;
private:
    CThreadPool  *m_pool;
};

#endif // !defined(AFX_ABSTRACTPOOLABLETHREADFACTORY_H__1369EB45_29C2_4514_9F99_8D0C1558C358__INCLUDED_)
