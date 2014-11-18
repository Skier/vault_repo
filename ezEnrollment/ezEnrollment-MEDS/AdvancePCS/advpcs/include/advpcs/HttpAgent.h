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
#include <advpcs/LoginFrame.h>
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

    bool UploadFile(const wxString& fielName);

    EdiResponse* GetStatus() 
        throw (AgentException);

    bool IsConnected() const { return !m_connection.IsEmpty(); };
    bool IsLoggedOn() const { return m_loggedOn; };

	bool IsCompressed() const { return m_compressed; };

	bool UseSftp() const { return m_useSftp; };

    wxString GetUserId() const {
        return m_userId;
    };

    wxString GetTransport() const { return "http"; };

protected:
    wxString GetInetMessage(DWORD error);
    EdiResponse* GetResponse(NetAutoHdr& request);

private:
    bool UploadFileToSftp(wxString filename);
	wxString CreateCommandFile(wxString filename);
	
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

    bool       m_useSftp;
	wxString   m_sftpHost;
	wxString   m_sftpPort;
	wxString   m_sftpLogin;
	wxString   m_sftpPassword;
	wxString   m_sftpRemoteDir;
	wxString   m_sftpClient;
	bool       m_sftpUseLogin;
	Logon      m_logon;

    wxString   m_userId;
    ProcessIndicator& m_indicator;
};


#endif /* __ADVPCS_HTTP_AGENT_H__ */ 
