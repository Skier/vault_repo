/*
 *  $RCSfile: App.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_APP_H__
#define __ADVPCS_APP_H__

class DocManager;
class MainFrame;
class ILogger;
class CXmlDocument;
class ICfg;
class Agent;
class AgentExecutor;

#pragma warning( disable : 4786)

class AdvPCSApp : public wxApp {
public:
    virtual bool OnInit();
    virtual int OnExit();
    ILogger& GetLogger();

    const DocManager& GetDocManager() const {
        wxASSERT(NULL != m_docManager);
        return *m_docManager;
    };
    Agent& GetAgent() {
        wxASSERT(NULL != m_agent);
        return *m_agent;
    };

	wxString EdiFileName();

    size_t GetEdiRowLength() {
		return m_ediRowLength;
	};

	AgentExecutor& GetExecutor() {
		wxASSERT(NULL != m_executor);
		return *m_executor;
	};

	ICfg* GetCfg() {
		wxASSERT(NULL != m_agentCfg);
		return m_agentCfg;
	}
private:
    void LoadCfg(const wxString& cfgFilename);

    ILogger*       m_logger;
    CXmlDocument*  m_cfg;
    MainFrame*     m_frame;

    ICfg*          m_agentCfg;
    DocManager*    m_docManager;
    Agent*         m_agent;
	AgentExecutor*  m_executor;

	size_t m_ediRowLength;
};

DECLARE_APP(AdvPCSApp);

#endif /* __ADVPCS_APP_H__ */
