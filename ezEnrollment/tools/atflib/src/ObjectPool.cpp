/*
 *  $RCSfile: ObjectPool.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ObjectPool.cpp: implementation of the CObjectPool class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/ObjectPool.h>
#include <atf/Mutex.h>
#include <atf/AssertException.h>
#include <atf/Environment.h>

const size_t ATF_POOL_DEFAULT_MAX_SIZE=10;


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
CObjectPool::CObjectPool(IObjectFactory & factory) : m_factory(factory),m_coreMutex(NULL),
                                           m_semaphore(NULL),
                                           m_allocated(0),
                                           m_max(ATF_POOL_DEFAULT_MAX_SIZE), m_free(NULL), m_used(NULL){
};

void* CObjectPool::Get(){
    ATF_ASSERT( NULL != m_coreMutex );

    LOG_DUMP_STRING((*(c_defaultEnvironment.GetLogger())), "Wait for semaphore", "");
    WaitForSingleObject(m_semaphore, INFINITE);
    
    CMutexLocker coreLock(m_coreMutex);

    PoolEntry * current = NULL;
    if ( NULL != m_free ) {
        current = m_free;
        m_free = m_free->next;
    } else {
        current =  new PoolEntry();
        current->obj = m_factory.Create();
        m_allocated++;
    }

    current->next = m_used;
    m_used = current;

    return current->obj;
};

void CObjectPool::Free(void* obj) {
    ATF_ASSERT( NULL != m_coreMutex );

    CMutexLocker coreLock(m_coreMutex);

    m_factory.Recycle(obj);

    ATF_ASSERT( NULL != m_used );
    
    PoolEntry * prev = NULL;
    PoolEntry * current = m_used; 
    while ( NULL != current ) {
        if ( current->obj == obj ) {
            if ( NULL == prev ) {
                m_used = current->next;
                current->next = m_free;
                m_free = current;
            } else {
                prev->next = current->next;
                current->next = m_free;
                m_free = current;
            }
            long old;
            m_free->lastUsed = GetTickCount();
            ReleaseSemaphore(m_semaphore, 1, &old);
            return;
        }
        prev = current;
        current = current->next;
    }
    ATF_ASSERT_MSG(false, "Unknown object");
};


CObjectPool::~CObjectPool(){
    {
        CMutexLocker coreLock(m_coreMutex);

        ATF_ASSERT_MSG( NULL == m_used, "Objects in use" );

        PoolEntry *current = m_free;
        while( NULL != current ) {
            PoolEntry * next = current->next;
            m_factory.Destroy(current->obj);
            delete current;
            current = next;
            LOG_DUMP_STRING((*(c_defaultEnvironment.GetLogger())), "Object destroied from Pool destructor.", "");
        }

    }
    if ( NULL != m_coreMutex ) {
        CloseHandle(m_coreMutex);
    }
    if ( NULL != m_semaphore ) {
        CloseHandle(m_semaphore);
    }
};


void CObjectPool::Initialize() {
    m_coreMutex     = CreateMutex(NULL, false, NULL);
    m_semaphore     = CreateSemaphore(NULL, m_max, m_max, NULL);
};
