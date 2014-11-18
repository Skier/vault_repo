/*
 *  $RCSfile: ThreadBodyRunnable.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ActiveRunnable.h: interface for the CActiveRunnable class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ACTIVERUNNABLE_H__08047547_56AF_4176_AE81_E3A3DDE848EE__INCLUDED_)
#define AFX_ACTIVERUNNABLE_H__08047547_56AF_4176_AE81_E3A3DDE848EE__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IRunnable.h> 
#include <atf/ExecutorPool.h>
#include <objbase.h>

class CThreadBodyRunnable : public IRunnable {
public:
    CThreadBodyRunnable():m_task(NULL),m_pool(NULL){};
    virtual ~CThreadBodyRunnable(){
    };
public:
    int Run(CThread * thisThread);
    void SetTask(IExecutor *task, CExecutorPool *pool) {
        m_task = task; 
        m_pool = pool;
    };
private: 
    IExecutor     * m_task;
    CExecutorPool * m_pool;
};

#endif // !defined(AFX_ACTIVERUNNABLE_H__08047547_56AF_4176_AE81_E3A3DDE848EE__INCLUDED_)
