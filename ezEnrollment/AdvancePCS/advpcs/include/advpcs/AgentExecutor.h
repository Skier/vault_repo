/*
 *  $RCSfile: AgentExecutor.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_AGENT_EXECUTOR_H__
#define __ADVPCS_AGENT_EXECUTOR_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/LoginFrame.h>
/* -------------------------- implementation place -------------------------- */

class ILogger;
class StatusList;
class wxString;
class Agent;
class EdiResponse;

class AgentExecutor {
public:
    AgentExecutor(ILogger& logger, StatusList& list, Agent& agent);

    bool ChangePassword();
    bool UploadFile(const wxString& fileName);
    bool RequestStatus();
    bool Login();
	bool IsConnected() const { return m_connected; };

protected:
    void FillStatus(EdiResponse* resp);

	ILogger& GetLogger() { return m_logger; };
	StatusList& GetList() { return m_list; };
	Agent& GetAgent() { return m_agent; };
private:

    ILogger&     m_logger;
	StatusList&  m_list;
    bool         m_connected;
	Agent&       m_agent;
	Logon        m_logon;
};

#endif /*__ADVPCS_AGENT_EXECUTOR_H__*/


