/*
 *  $RCSfile: IBaseService.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CdService.cpp: implementation of the CCdService class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/Logger.h>
#include <atf/Cfg.h>
#include <atf/AssertException.h>
#include <atf/IExecutorFactory.h>
#include <atf/IListener.h>
#include <atf/IBaseService.h>


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

const char* ATF_EXECUTORS_CFG        = "executors";
const char* ATF_SERVICES_CFG         = "services";
const char* ATF_EXECUTOR_NAME_CFG    = "name";
const char* ATF_EXECUTOR_TYPE_CFG    = "type";
const char* ATF_POOL_SIZE_CFG        = "pool-size";
const char* ATF_LISTENER_TYPE_CFG    = "listener-type";
const char* ATF_SERVICE_EXECUTOR_CFG = "executor";
const char* ATF_THREADS_CFG          = "threads";

const char* ATF_FILE_EXECUTOR_TYPE   = "file";
const char* ATF_HTTP_EXECUTOR_TYPE   = "http";
const char* ATF_MSMQ_EXECUTOR_TYPE   = "msmq";

const char* ATF_FILE_LISTENER_TYPE   = "file";
const char* ATF_MSMQ_LISTENER_TYPE   = "msmq";


void IBaseService::Configure(ICfg & cfg){

    ICfg* threadsCfg = cfg.GetChild(ATF_THREADS_CFG);
    long poolSize = threadsCfg->GetParamAsLong(ATF_POOL_SIZE_CFG);
    m_threadPool->SetMaxSize(poolSize);
    LOG_INFO(GetLogger(), ATF_INFO, "Thread pool size setted");

    ICfg* executorsCfg = cfg.GetChild(ATF_EXECUTORS_CFG);
    BuildExecutors(*executorsCfg);
    LOG_INFO(GetLogger(), ATF_INFO, "All executors created");

    ICfg* servicesCfg = cfg.GetChild(ATF_SERVICES_CFG);
    BuildServices(*servicesCfg);
    LOG_INFO(GetLogger(), ATF_INFO, "All services created");
};

void IBaseService::Initialize(){

    m_threadFactory->SetPool(m_threadPool);
    m_threadPool->Initialize();
    LOG_INFO(GetLogger(), ATF_INFO, "Thread pool initialized");

    int size = m_listeners.GetSize();
    for(int i=0; i<size; i++) {
        if ( NULL != m_listeners[i] ) {
            IListener * lsnr = (IListener*)(m_listeners[i]);
            lsnr->Initialize();
        }
    }
    LOG_INFO(GetLogger(), ATF_INFO, "Listeners initialized");
};

void IBaseService::Start(){
    int size = m_listeners.GetSize();
    for(int i=0; i<size; i++) {
        if ( NULL != m_listeners[i] ) {
            IListener * lsnr = (IListener*)(m_listeners[i]);
            lsnr->Start(new CThread(lsnr));
        }
    }
    LOG_INFO(GetLogger(), ATF_INFO, "Listeners started");
};

void IBaseService::Stop(){
    int size = m_listeners.GetSize();
    for(int i=0; i<size; i++) {
        if ( NULL != m_listeners[i] ) {
            IListener * lsnr = (IListener*)(m_listeners[i]);
            lsnr->Stop();
        }
    }
    LOG_INFO(GetLogger(), ATF_INFO, "Listeners stopped");
};

IBaseService::~IBaseService() {
    int size = m_listeners.GetSize();
    for(int i=0; i<size; i++) {
        if ( NULL != m_listeners[i] ) {
            IListener * lsnr = (IListener*)(m_listeners[i]);
            CThread * thread = lsnr->GetThread();
            if ( NULL != thread ) {
                delete thread;
            }
            m_listeners[i] = NULL;
        }
    }
    delete m_threadPool;
    delete m_threadFactory;
};
