/*
 *  $RCSfile: IBaseService.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CdService.h: interface for the CCdService class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_BASESERVICE_H__3AB88491_32C1_44E3_AF4D_37DC9FF9A6D6__INCLUDED_)
#define AFX_BASESERVICE_H__3AB88491_32C1_44E3_AF4D_37DC9FF9A6D6__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IService.h>
#include <atf/Environment.h>
#include <atf/Logger.h>
#include <atf/ThreadPool.h>
#include <atf/PoolMap.h>
#include <atf/ThreadFactory.h>

class ILogger;
class ICfg;
class CAtfException;
class CConfigurationException;

extern const char* ATF_EXECUTORS_CFG;
extern const char* ATF_SERVICES_CFG;
extern const char* ATF_EXECUTOR_NAME_CFG;
extern const char* ATF_EXECUTOR_TYPE_CFG;
extern const char* ATF_POOL_SIZE_CFG;
extern const char* ATF_LISTENER_TYPE_CFG;
extern const char* ATF_SERVICE_EXECUTOR_CFG;
extern const char* ATF_THREADS_CFG;

extern const char* ATF_FILE_EXECUTOR_TYPE;
extern const char* ATF_HTTP_EXECUTOR_TYPE;
extern const char* ATF_MSMQ_EXECUTOR_TYPE;

extern const char* ATF_FILE_LISTENER_TYPE;
extern const char* ATF_MSMQ_LISTENER_TYPE;

class IBaseService : public IService {
public:
    IBaseService(const char * name):
        IService(name),
        m_logger(c_defaultEnvironment.GetLogger()) 
    {
       m_threadFactory = new CThreadFactory();
       m_threadPool    = new CThreadPool(*m_threadFactory);
    };
    virtual ~IBaseService();
public:
    void EnableLogging(ILogger * logger){
        m_logger = logger;
        m_threadFactory->EnableLogging(m_logger);
    };
    virtual void Configure(ICfg & cfg);
    virtual void Initialize();
    virtual void Start();
    virtual void Stop();

public:
    ILogger & GetLogger() {return *m_logger;};
protected:
    virtual void BuildExecutors(ICfg &cfg) 
        throw (CConfigurationException,CAtfException) = 0;
    virtual void BuildServices(ICfg &cfg) 
        throw (CConfigurationException,CAtfException) = 0;
protected:
    CPoolMap         m_map;
    ILogger*         m_logger;
    CPtrArray        m_listeners;
    CThreadFactory   *m_threadFactory;
    CThreadPool      *m_threadPool;
};

#endif // !defined(AFX_BASESERVICE_H__3AB88491_32C1_44E3_AF4D_37DC9FF9A6D6__INCLUDED_)
