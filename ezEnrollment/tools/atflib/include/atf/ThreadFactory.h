/*
 *  $RCSfile: ThreadFactory.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CdThreadFactory.h: interface for the CCdThreadFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDTHREADFACTORY_H__FA57A602_1C28_49BD_B858_3B73CBCAED15__INCLUDED_)
#define AFX_CDTHREADFACTORY_H__FA57A602_1C28_49BD_B858_3B73CBCAED15__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/AbstractPoolableThreadFactory.h>
#include <atf/Environment.h>
#include <atf/ThreadBodyRunnable.h>

class CThreadFactory : public CAbstractPoolableThreadFactory {
public:
    CThreadFactory():m_logger(c_defaultEnvironment.GetLogger()){};
    virtual ~CThreadFactory(){};
public:
    void EnableLogging(ILogger *logger) { m_logger = logger; };
protected:
    IRunnable * CreateBody(){ 
        LOG_DEBUG((*m_logger), ATF_DEBUG, "Active runnable created");
        return new CThreadBodyRunnable();
    };
private:
    ILogger           *m_logger;
};

#endif // !defined(AFX_CDTHREADFACTORY_H__FA57A602_1C28_49BD_B858_3B73CBCAED15__INCLUDED_)
