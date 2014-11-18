/*
 *  $RCSfile: ThreadBodyRunnbale.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ActiveRunnable.cpp: implementation of the CActiveRunnable class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/AssertException.h>
#include <atf/ExceptionLogger.h>
#include <atf/Thread.h>
#include <atf/ThreadBodyRunnable.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

int CThreadBodyRunnable::Run(CThread * thisThread){
    int rc = 1;
    ATF_ASSERT(NULL != m_task);
    try {
        rc = m_task->Run(thisThread);
    } catch (CAssertException &e) {
        CExceptionLogger::Log(thisThread->GetLogger(), e);
    } catch (CAtfException &te) {
        CExceptionLogger::Log(thisThread->GetLogger(), te);
    } catch (...) {
        LOG_ERROR(thisThread->GetLogger(), ATF_UNKNOWN_ERR, "Unknown exception was thrown.");
    }
    m_pool->Free(m_task);
    m_task = NULL;
    return rc;
};

