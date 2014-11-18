/*
 *  $RCSfile: HttpAgent.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_HTTP_AGENT_H__
#define __ADVPCS_HTTP_AGENT_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/Agent.h>
#include <advpcs/NetAutoHdr.h>
/* -------------------------- implementation place -------------------------- */

class CXmlDocument;
class ProcessIndicator;

class HttpAgent : public Agent {
public:
    HttpAgent(ICfg& cfg, ILogger& logger, ProcessIndicator& indicator);
    virtual ~HttpAgent();

    EdiResponse* Login(const wxString& userid, const wxString& pswd) 
        throw (AgentException);

    EdiResponse* ChangePassword(const wxString& userid, const wxString& pswd, const wxString& newpswd) 
        throw (AgentException);

    EdiResponse* PostFile(const wxString& fielName) 
        throw (AgentException);

    EdiResponse* GetStatus() 
        throw (AgentException);

    bool IsConnected() const { return !m_connection.IsEmpty(); };
    bool IsLoggedOn() const { return m_loggedOn; };

	bool IsCompressed() const { return m_compressed; };

    wxString GetUserId() const {
        return m_userId;
    };

protected:
    wxString GetInetMessage(DWORD error);
    EdiResponse* GetResponse(NetAutoHdr& request);

private:
    NetAutoHdr m_session;
    NetAutoHdr m_connection;
    HMODULE    m_wininetdll;
    bool       m_loggedOn;

    wxString   m_host;
    long       m_port;
    wxString   m_login;
	wxString   m_changePwd;
    wxString   m_upload;
    wxString   m_status;
    bool       m_secure;
    DWORD      m_flags;
	bool       m_compressed;

    wxString   m_userId;
    ProcessIndicator& m_indicator;
};


#endif /* __ADVPCS_HTTP_AGENT_H__ */ 
