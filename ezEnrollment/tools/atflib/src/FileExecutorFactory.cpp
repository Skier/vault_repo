/*
 *  $RCSfile: FileExecutorFactory.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// FileExecutorFactory.cpp: implementation of the CFileExecutorFactory class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/FileExecutorFactory.h>
#include <atf/FileExecutor.h>
#include <atf/Environment.h>
#include <atf/Const.h>


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CFileExecutorFactory::CFileExecutorFactory() {
    m_logger = c_defaultEnvironment.GetLogger();
};

void CFileExecutorFactory::Configure(ICfg &cfg) {
    m_workDir = cfg.GetParam(ATF_WORK_DIR_CFG);
    if ( cfg.HasParam(ATF_DONE_DIR_CFG) ) {
        m_doneDir = cfg.GetParam(ATF_DONE_DIR_CFG);
    }
    if ( cfg.HasParam(ATF_ERROR_DIR_CFG) ) {
        m_errorDir = cfg.GetParam(ATF_ERROR_DIR_CFG);
    }
};


IExecutor * CFileExecutorFactory::Create() {
    CFileExecutor *exec = new CFileExecutor();
    exec->EnableLogging(m_logger);
    exec->Initialize(m_doneDir, m_errorDir, m_workDir);

    LOG_INFO(GetLogger(), ATF_INFO, "CFileExecutorFactory: CFileExecutor instance created.");
    return exec;
};
