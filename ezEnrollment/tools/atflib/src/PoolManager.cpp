/*
 *  $RCSfile: PoolManager.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// PoolManager.cpp: implementation of the CPoolManager class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/PoolManager.h>
#include <atf/Mutex.h>
#include <atf/ObjectPool.h>
#include <atf/Environment.h>

static const DWORD   shrinkInterval = 30*1000;
static const CString className("CPoolManager:");
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

class DummyRunnable : public IRunnable {
public:
    DummyRunnable(CPoolManager &manager):m_manager(manager){};
    virtual ~DummyRunnable() {};
    int Run(CThread * thisThread) {return m_manager.Run(thisThread);} ;
private:
    CPoolManager &m_manager;
};

void CPoolManager::Start() {
    m_thread->Start();
    m_started = true;
    LOG_INFO(m_thread->GetLogger(), ATF_INFO, className + " Started");

};

void CPoolManager::Stop() {
    if ( m_started ) {
        SetEvent(m_stopEvent);
        m_thread->WaitForStop(INFINITE);
        m_started = false;
    }
};

CPoolManager::CPoolManager(): 
    m_shedule(NULL), m_minInterval(0), m_started(false)
{
    m_thread = new CThread(new DummyRunnable(*this));
    m_thread->EnableLogging(c_defaultEnvironment.GetLogger());
    m_sheduleMutex = CreateMutex(NULL, false, NULL);
    m_addedEvent   = CreateEvent(NULL, false, false, NULL);
    m_stopEvent    = CreateEvent(NULL, false, false, NULL);
}

CPoolManager::~CPoolManager(){
    if ( m_started ) {
        Stop();
    } 
    delete m_thread;

    CShedule * current = NULL;
    
    while(m_shedule != NULL) {
        current = m_shedule->m_next;
        delete m_shedule;
        m_shedule = current;
    }

    if ( NULL != m_sheduleMutex ) {
        CloseHandle(m_sheduleMutex);
        m_sheduleMutex = NULL;
    }
    if ( NULL != m_addedEvent ) {
        CloseHandle(m_addedEvent);
    }
    if ( NULL != m_stopEvent ) {
        CloseHandle(m_stopEvent);
    }
}

void CPoolManager::manage(long interval, CObjectPool &pool) {
    CShedule * shedule = new CShedule(interval, pool);
    CMutexLocker locker(m_sheduleMutex);
    shedule->m_next = m_shedule;
    m_shedule = shedule;

    SetEvent(m_addedEvent);
}; 

int CPoolManager::Run(CThread * thisThread) {
    HANDLE wait[2];
    wait[0] = m_addedEvent;
    wait[1] = m_stopEvent;

    LOG_INFO(thisThread->GetLogger(), ATF_INFO, className + "Thread Started");

    while ( true ) {

        DWORD result = WaitForMultipleObjects(2, wait, false, shrinkInterval);
        if ( WAIT_OBJECT_0+1 == result ) {
            LOG_INFO(thisThread->GetLogger(), ATF_INFO, className + " Stop signalled");
            break;
        } 

        LOG_DEBUG(thisThread->GetLogger(), ATF_DEBUG, className + " Start shrinking");

        {
            CMutexLocker locker(m_sheduleMutex);
            CShedule * shedule = m_shedule; 
            while( NULL != shedule ) {
               if ( GetTickCount() - shedule->m_lastShrink > shedule->m_interval ) {
                    Shrink(shedule);
                    shedule->m_lastShrink = GetTickCount();
                }
                shedule = shedule->m_next;
            }
        }
    }
    return 0;
};

void CPoolManager::Shrink(CShedule *shedule) {
    CObjectPool & pool = shedule->m_pool;
    CMutexLocker locker(pool.m_coreMutex);
    PoolEntry * current = pool.m_free;
    PoolEntry * last = NULL; 
    while ( NULL != current &&
            current->lastUsed > (GetTickCount() - shedule->m_interval)) 
    {
        last = current;
        current = current->next;
    }
    if ( NULL == current ) {
        return;
    }

    if ( NULL == last ) {
        pool.m_free = NULL;
    } else {
        last->next = NULL;
    }
    while ( NULL != current ) {
        pool.m_factory.Destroy(current->obj);
        last = current;
        current = current->next;
        delete last;
        pool.m_allocated--;
        LOG_INFO(m_thread->GetLogger(), ATF_INFO, className + " Pool shrinked by one");
    }
};
