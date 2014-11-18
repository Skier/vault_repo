/*
 *  $RCSfile: IExecutorFactory.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IExecutorFactory.h: interface for the IExecutorFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ICDEXECUTORFACTORY_H__4839338C_67FB_4D03_B19C_080ED397F862__INCLUDED_)
#define AFX_ICDEXECUTORFACTORY_H__4839338C_67FB_4D03_B19C_080ED397F862__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IExecutor.h>
#include <atf/Environment.h>
#include <atf/Logger.h>

class CConfigurationException;

static CString destroyMessage(" Object destroyed.");

class IExecutorFactory {
public:
    IExecutorFactory() : m_logger(c_defaultEnvironment.GetLogger()) {};
    virtual ~IExecutorFactory(){};
public:
    void EnableLogging(ILogger *logger){m_logger=logger;};
    ILogger& GetLogger() {return *m_logger;};

    virtual void Configure(ICfg &cfg) throw (CConfigurationException) = 0;
    virtual void Initialize() = 0;

    virtual const char* GetName() const = 0;
public:
    virtual IExecutor * Create() = 0;
    virtual Destroy(IExecutor *obj) { 
        delete obj; 
        LOG_DEBUG(GetLogger(), ATF_DEBUG, GetName()+destroyMessage );
    };

protected:
    ILogger* m_logger;
};

#endif // !defined(AFX_ICDEXECUTORFACTORY_H__4839338C_67FB_4D03_B19C_080ED397F862__INCLUDED_)
