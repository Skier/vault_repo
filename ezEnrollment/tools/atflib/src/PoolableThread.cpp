/*
 *  $RCSfile: PoolableThread.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// PoolableThread.cpp: implementation of the CPoolableThread class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/PoolableThread.h>
#include <atf/Environment.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
DWORD WINAPI ThreadFunc( LPVOID lpParam );

void CPoolableThread::CreateNativeThread() {
    DWORD threadId;
    m_thread = CreateThread( NULL, 0, ThreadFunc, this, 0, &threadId);
};

static DWORD WINAPI ThreadFunc( LPVOID lpParam ) {
    CPoolableThread * thisThread = (CPoolableThread*) lpParam;
    while ( true ) {
        WaitForSingleObject(thisThread->m_runEvent, INFINITE);
        if ( thisThread->m_stop ) {
            LOG_DEBUG(thisThread->GetLogger(), ATF_DEBUG, "Thread stopped");
            return 0; // Stop thread
        }
        try {
            thisThread->GetBody().Run(thisThread); 
        } catch (...) {
            LOG_ERROR(thisThread->GetLogger(), ATF_DEBUG, "Unknown exception raised");
        }
        thisThread->m_pool.Free(thisThread);
        LOG_DEBUG(thisThread->GetLogger(), ATF_DEBUG, "Thread Freed");
    } 
};
