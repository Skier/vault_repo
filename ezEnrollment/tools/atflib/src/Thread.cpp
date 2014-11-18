/*
 *  $RCSfile: Thread.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Thread.cpp: implementation of the CThread class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/Thread.h>
#include <atf/AssertException.h>
#include <atf/Environment.h>
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
DWORD WINAPI ThreadFunc( LPVOID lpParam );

CThread::CThread(IRunnable *body):
    m_body(body), m_thread(NULL), 
    m_logger(c_defaultEnvironment.GetLogger()), m_started(false), m_suspended(1) 
{
    CreateNativeThread();
};

CThread::~CThread() {
    if ( NULL != m_thread ) {
        if ( 0 == m_suspended ) {
            WaitForSingleObject(m_thread, INFINITE);
        } else {
            TerminateThread(m_thread, 1);
        }
        CloseHandle(m_thread);
    }
    if ( NULL != m_body ) {
        delete m_body;
    }
};

void CThread::Start() {

    ATF_ASSERT(NULL != m_thread);
    m_started = true;
    Resume();
};

void CThread::Suspend() {
    ATF_ASSERT(NULL != m_thread);
    LOG_DEBUG(GetLogger(), ATF_DEBUG, "Thread suspended");
    SuspendThread(m_thread);
    m_suspended++;
};

void CThread::Resume() {
    ATF_ASSERT(NULL != m_thread);
    while ( 1 != ResumeThread(m_thread) ) m_suspended--;
};


void CThread::CreateNativeThread() {
    ATF_ASSERT(NULL == m_thread);

    DWORD threadId;
    m_thread = CreateThread( NULL, 0, ThreadFunc, this, CREATE_SUSPENDED, &threadId);
};

void CThread::Create() {
    CreateNativeThread();
}

bool CThread::WaitForStop(size_t timeout) {
    DWORD result = WaitForSingleObject(m_thread, timeout);
    switch (result) {
        case WAIT_OBJECT_0:
        case WAIT_ABANDONED:
            return true;
        case WAIT_TIMEOUT:
            return false;
        default:
            THROW_SYSTEM_EXCEPTION("Can't wait thread");
    } 
    return false;
};

static DWORD WINAPI ThreadFunc( LPVOID lpParam ) { 
    CThread * thisThread = (CThread*) lpParam;
    return thisThread->GetBody().Run(thisThread);
}

