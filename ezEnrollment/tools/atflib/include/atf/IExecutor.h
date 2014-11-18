/*
 *  $RCSfile: IExecutor.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IExecutor.h: interface for the IExecutor class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ICDEXECUTOR_H__FA1E49CC_1C83_4670_886B_862B7C91F78E__INCLUDED_)
#define AFX_ICDEXECUTOR_H__FA1E49CC_1C83_4670_886B_862B7C91F78E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Environment.h>
#include <atf/IRunnable.h>
#include <atf/AssertException.h>
#include <atf/IMessage.h>

class ILogger;
class ICfg;

class IExecutor : public IRunnable{
public:
    IExecutor():m_logger(c_defaultEnvironment.GetLogger()), m_task(NULL){};
    virtual ~IExecutor(){ 
        if ( NULL != m_task ) {
            delete m_task;
            m_task = NULL;
        }
    };
public:
    void SetTask(IMessage * task){m_task = task;};
    IMessage& GetTask(){
        ATF_ASSERT(NULL!=m_task);
        return *m_task;
    };
    virtual void Recycle() {
        if ( NULL != m_task ) {
            delete m_task;
            m_task = NULL;
        }
    };
public:
    void EnableLogging(ILogger * logger){m_logger = logger;};
    ILogger& GetLogger() {return *m_logger;};
private:
    ILogger     *m_logger;
    IMessage    *m_task;
};

#endif // !defined(AFX_ICDEXECUTOR_H__FA1E49CC_1C83_4670_886B_862B7C91F78E__INCLUDED_)
