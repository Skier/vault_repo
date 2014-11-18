/*
 *  $RCSfile: HTTPExecutorFactory.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// CdHTTPExecutorFactory.cpp: implementation of the CCdHTTPExecutorFactory class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/Cfg.h>
#include <atf/Environment.h>
#include <atf/HTTPExecutorFactory.h>
#include <atf/HTTPExecutor.h>
#include <atf/Const.h>
#include <atlbase.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

static const char* ATF_SERVER_CFG     = "server";
static const char* ATF_PORT_CFG       = "port";
static const char* ATF_USER_CFG       = "username";
static const char* ATF_PASSWORD_CFG   = "password";
static const char* ATF_OBJECT_CFG     = "object";
static const char* ATF_URL_CFG        = "url";
static const char* ATF_VAR_NAME_CFG   = "var-name";

IExecutor * CHTTPExecutorFactory::Create() {
   CHTTPExecutor *exec = new CHTTPExecutor();
   exec->EnableLogging(m_logger);
   exec->Initialize(m_serverName, m_port, m_userName, m_password,
                    m_objectName, m_url, m_varName, m_workDir, m_doneDir, m_errorDir);

   LOG_INFO(GetLogger(), ATF_INFO, "CHTTPExecutorFactory: CHTTPExecutor instance created.");
   return exec;
};

CHTTPExecutorFactory::CHTTPExecutorFactory(){
    m_logger = c_defaultEnvironment.GetLogger();
};

void CHTTPExecutorFactory::Configure(ICfg &cfg) {
    m_serverName   = cfg.GetParam(ATF_SERVER_CFG);
    m_port         = (short)cfg.GetParamAsLong(ATF_PORT_CFG);
    m_userName     = cfg.GetParam(ATF_USER_CFG); 
    m_password     = cfg.GetParam(ATF_PASSWORD_CFG);
    m_objectName   = cfg.GetParam(ATF_OBJECT_CFG);
    m_url          = cfg.GetParam(ATF_URL_CFG);
    m_varName      = cfg.GetParam(ATF_VAR_NAME_CFG);
    m_workDir      = cfg.GetParam(ATF_WORK_DIR_CFG);
    if ( cfg.HasParam(ATF_DONE_DIR_CFG) ) {
        m_doneDir = cfg.GetParam(ATF_DONE_DIR_CFG);
    }
    if ( cfg.HasParam(ATF_ERROR_DIR_CFG) ) {
        m_errorDir = cfg.GetParam(ATF_ERROR_DIR_CFG);
    }
};
