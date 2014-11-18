/*
 *  $RCSfile: ServiceApp.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ServiceApp.cpp: implementation of the CServiceApp class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/ServiceApp.h>
#include <atf/Environment.h>
#include <atf/SystemException.h>

const char * ATF_CFG_SERVICE_NAME = "name";

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
void WINAPI ServiceMain(DWORD argc, LPTSTR *argv);
void Stop();
void Interrogate();
void Shutdown();


IService *c_svc=NULL;
SERVICE_STATUS          c_ssStatus;          
SERVICE_STATUS_HANDLE   c_sshStatusHandle;   
SERVICE_TABLE_ENTRY     c_dispatchTable[] = {
    { NULL, (LPSERVICE_MAIN_FUNCTION)ServiceMain },
    { NULL, NULL }
};


CServiceApp::CServiceApp(ICfg &cfg, IService * svc):m_cfg(cfg){
    c_svc=svc;
    c_ssStatus.dwServiceType             = SERVICE_WIN32_OWN_PROCESS;
    c_ssStatus.dwServiceSpecificExitCode = 0;
    c_ssStatus.dwCurrentState            = SERVICE_STOPPED;
    c_ssStatus.dwWin32ExitCode           = NO_ERROR;
    c_ssStatus.dwWaitHint                = 3000;
    c_ssStatus.dwCheckPoint              = 0;
    c_sshStatusHandle                    = NULL;
};

CServiceApp::~CServiceApp(){ 
    if ( NULL != c_svc ) {
        delete c_svc;  
    }
    if ( NULL != m_logger ) {
        delete m_logger;
    }

};


void CServiceApp::Initialize() {
    ICfg * loggerCfg = GetLoggerCfg();
    ILogger *logger = c_defaultEnvironment.GetLogger();
    if ( NULL == loggerCfg ) {
        LOG_WARN((*logger), ATF_WARNING, "No logger configuration, use default logger");
    } else {
        m_logger = ILogger::CreateLogger(*loggerCfg);
        LOG_INFO((*m_logger), ATF_INFO, "Service Logger defined");
        logger = m_logger;
        c_defaultEnvironment.SetLogger(m_logger);
    }

    if( !m_cfg.HasParam(ATF_CFG_SERVICE_NAME) ) {
        CString err = "Param [";
        err += ATF_CFG_SERVICE_NAME;
        err += "] not found";
        THROW_CFG_EXCEPTION(err);
    }

    m_name = m_cfg.GetParam(ATF_CFG_SERVICE_NAME);
    c_dispatchTable[0].lpServiceName = m_name.LockBuffer();
    c_svc->EnableLogging(logger);
    c_svc->Configure(m_cfg);
    
    LOG_INFO(GetLogger(), ATF_INFO, "Service configured");

    c_svc->Initialize();
    LOG_INFO(GetLogger(), ATF_INFO, "Service initialized");
};

ICfg* CServiceApp::GetLoggerCfg() {
    return m_cfg.GetChild(ATF_LOG);
};


static const char * err = "Can't start service";
int CServiceApp::Run() {
    if(!StartServiceCtrlDispatcher(c_dispatchTable)) {
        THROW_SYSTEM_EXCEPTION(err);
    }
    return 0;
};

void WINAPI ServiceCtrl(DWORD control_code) {
    switch(control_code) {
        case SERVICE_CONTROL_STOP:
            Stop();
            break;
        case SERVICE_CONTROL_INTERROGATE:
            Interrogate();
            break;
        case SERVICE_CONTROL_SHUTDOWN:
            Shutdown();
            break;
        default:
            break;
    }
};

static void WINAPI ServiceMain(DWORD argc, LPTSTR *argv) {
    c_sshStatusHandle = RegisterServiceCtrlHandler(c_svc->GetName(), ServiceCtrl);
    if (NULL == c_sshStatusHandle) {
        LOG_ERROR((*(c_defaultEnvironment.GetLogger())), ATF_SERVICE_ERR, "Can't register service control handler"); 
    };

    c_ssStatus.dwServiceType = SERVICE_WIN32_OWN_PROCESS;
    c_ssStatus.dwServiceSpecificExitCode = 0;
    c_ssStatus.dwCurrentState = SERVICE_START_PENDING;
    c_ssStatus.dwWin32ExitCode = NO_ERROR;
    c_ssStatus.dwWaitHint = 100000;
    c_ssStatus.dwCheckPoint = 0;
    Interrogate();

    try {
        c_svc->Start();
        c_ssStatus.dwCurrentState = SERVICE_RUNNING;
        c_ssStatus.dwControlsAccepted = SERVICE_ACCEPT_STOP|SERVICE_ACCEPT_SHUTDOWN;
    } catch(...) {
        LOG_ERROR((*(c_defaultEnvironment.GetLogger())), ATF_SERVICE_ERR, "Error during service start"); 
        c_ssStatus.dwCurrentState = SERVICE_STOPPED;
    }
    Interrogate();
};


void Interrogate() {
    SetServiceStatus(c_sshStatusHandle, &(c_ssStatus));
};

    /*  */
void Stop() {
    c_ssStatus.dwCurrentState = SERVICE_STOP_PENDING;
    Interrogate();

    c_svc->Stop();

    c_ssStatus.dwCurrentState = SERVICE_STOPPED;
    Interrogate();
};

void Shutdown() {
    Stop();
};

