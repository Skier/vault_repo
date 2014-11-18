/*
 *  $RCSfile: CmdLineApp.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CmdLineApp.cpp: implementation of the CCmdLineApp class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/CmdLineApp.h>
#include <atf/AssertException.h>
#include <atf/Environment.h>
#include <iostream>

using namespace std;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CCmdLineApp::CCmdLineApp(ICfg &cfg, IService * svc) : m_cfg(cfg), m_logger(NULL){
    ATF_ASSERT(NULL != svc);
    m_svc = svc;
}


CCmdLineApp::~CCmdLineApp() {
    delete m_svc;
    m_svc = NULL;
    if ( NULL != m_logger ) {
        delete m_logger;
        m_logger = NULL;
    }
}


void CCmdLineApp::Initialize() {
    ICfg * loggerCfg = GetLoggerCfg();
    ILogger *logger = c_defaultEnvironment.GetLogger();
    if ( NULL == loggerCfg ) {
        LOG_WARN((*logger), ATF_WARNING, "No logger configuration, use default logger");
    } else {
        m_logger = ILogger::CreateLogger(*loggerCfg);
        logger = m_logger;
        c_defaultEnvironment.SetLogger(m_logger);
    }
    m_svc->EnableLogging(m_logger);
    m_svc->Configure(m_cfg);
    
    LOG_INFO((*m_logger), ATF_INFO, "Service configured");

    m_svc->Initialize();
    LOG_INFO((*m_logger), ATF_INFO, "Service initialized");
};

int CCmdLineApp::Run() {

    m_svc->Start();

    cout << endl << "Application started."<< endl << "Press ENTER for stop" << endl;
    fgetc(stdin);
    m_svc->Stop();

    return 0;
};

ICfg* CCmdLineApp::GetLoggerCfg() {
    return m_cfg.GetChild(ATF_LOG);
};

