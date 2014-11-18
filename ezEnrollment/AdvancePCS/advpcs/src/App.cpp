/*
 *  $RCSfile: App.cpp,v $
 *
 *  $Revision: 1.4 $
 *
 *  last change: $Date: 2003/10/16 15:11:44 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <memory>
#include <exception>
#include <fstream>
#include <wx/listctrl.h>
#include <wx/fl/controlbar.h>
#include <wx/datetime.h>
#include <wx/cmdproc.h>

#include <atf/Logger.h>
#include <atf/XmlDocument.h>
#include <atf/XmlLoader.h>
#include <atf/XmlIOHandler.h>
#include <atf/SystemException.h>
#include <atf/StreamIOHandler.h>
#include <atf/CfgException.h>
#include <atf/CfgXml.h>
#include <atf/FileLogger.h>
#include <advpcs/ListLogger.h>
#include <advpcs/MainFrame.h>
#include <advpcs/App.h>
#include <advpcs/Resources.h>
#include <advpcs/DocManager.h>
#include <advpcs/HttpAgent.h>
#include <advpcs/AgentExecutor.h>
#include <advpcs/StatusList.h>
#include <advpcs/GroupLogger.h>
/* -------------------------- implementation place -------------------------- */


bool AdvPCSApp::OnInit() {
    m_agentCfg = NULL;
    m_docManager = NULL;
    m_agent = NULL;
    m_executor = NULL;

    try {
        m_frame = new MainFrame(ADVPCS_FRAME_TITLE);
        SetTopWindow(m_frame);

        m_logger = new GroupLogger();
		((GroupLogger*)m_logger)->Add(new ListLogger(m_frame->GetLogList()));

        m_frame->Show();

        LoadCfg(ADVPCS_CFG_FILE_NAME);
  
        if ( NULL == m_cfg->GetRoot() ) {
            THROW_CFG_EXCEPTION(ADVPCS_CFG_ROOT_NOT_FOUND);
        }

        m_agentCfg = new CCfgXml(*m_cfg);

        if ( m_agentCfg->HasParam(ADVPCS_DEBUG_CFG) )  {
            if ( m_agentCfg->GetParamAsBool(ADVPCS_DEBUG_CFG) ) {
   		        std::ostream* os = new std::ofstream(m_agentCfg->GetParam(ADVPCS_DEBUG_LOG_CFG, "advpcs.log"));
		        ((GroupLogger*)m_logger)->Add(new CFileLogger(os));
            }
        }


		m_ediRowLength = m_agentCfg->GetParamAsLong(ADVPCS_ROW_SIZE_CFG);

        m_docManager = new DocManager(*(m_cfg->GetRoot()), m_frame->GetProcessIndicator());
        m_agent = new HttpAgent(*(m_agentCfg->GetChild(ADVPCS_AGENT_CFG)), GetLogger(), m_frame->GetProcessIndicator());
 
        m_executor = new AgentExecutor(GetLogger(), m_frame->GetStatusList(), GetAgent());

        m_frame->SetDocument(GetDocManager().Create());
        // Run wizard
        wxCommandEvent ev;
        m_frame->SetLastListColumn();
        m_frame->OnRunWizard(ev);
        m_frame->RefreshTitle();


        return true;
    } catch (const CAtfException& ex) {
        LOG_FATAL(GetLogger(), ATF_USER_ERR, ex.GetText());
        ::wxMessageBox(((const char*)ex.GetText()), ADVPCS_FATAL_TITLE);
    } catch (std::exception& ex) {
        ::wxMessageBox(ex.what(), ADVPCS_FATAL_TITLE);
    } catch (...) {
        ::wxMessageBox(ADVPCS_INTERNAL_ERROR, ADVPCS_FATAL_TITLE);
    }
    return false;
};


int AdvPCSApp::OnExit() {
    if ( NULL != m_docManager ) {
        wxDELETE(m_docManager);
    }
	if ( NULL != m_executor ) {
		wxDELETE(m_executor);
	}
    return 0;
};

void AdvPCSApp::LoadCfg(const wxString& cfgFilename) {
    m_cfg = new CXmlDocument();
    std::ifstream is(cfgFilename);
    if ( !is.is_open() ) {
        THROW_SYSTEM_EXCEPTION(cfgFilename);
    }
    CStreamIOHandler handler(is);
    CXmlLoader loader(handler);
    loader.Load(*m_cfg);

};


ILogger& AdvPCSApp::GetLogger() {
    wxASSERT(NULL != m_logger);

    return *m_logger;
};

wxString AdvPCSApp::EdiFileName() {
	ICfg* cfg = m_agentCfg->GetChild(ADVPCS_COMPOSER_CFG);
    wxString prefix = (const char*)cfg->GetParam(ADVPCS_COMPOSER_PREFIX_CFG);
    wxString suffix = (const char*)cfg->GetParam(ADVPCS_COMPOSER_SUFFIX_CFG, "");
#if 0
    if ( GetAgent().IsLoggedOn() ) {
        return prefix+GetAgent().GetUserId()+wxDateTime::Now().Format("%Y%m%d")+wxDateTime::Now().Format("%H%M%S"); 
    } else {
        return prefix+wxDateTime::Now().Format("%Y%m%d")+wxDateTime::Now().Format("%H%M%S"); 
    }
#else
    return prefix+wxDateTime::Now().Format("%m%d")+suffix; 
#endif
};


IMPLEMENT_APP(AdvPCSApp);
