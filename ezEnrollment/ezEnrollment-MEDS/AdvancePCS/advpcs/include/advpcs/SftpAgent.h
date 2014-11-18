/*
 *  $RCSfile: SftpAgent.h,v $
 *
 *  $Revision: 1.0 $
 *
 *  last change: $Date: 2006/09/25 15:19:46 $
 */

#ifndef __ADVPCS_SFTP_AGENT_H__
#define __ADVPCS_SFTP_AGENT_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/Agent.h>
#include <advpcs/NetAutoHdr.h>
#include <advpcs/LoginFrame.h>
/* -------------------------- implementation place -------------------------- */

class CXmlDocument;
class ProcessIndicator;

class SftpAgent : public Agent {
public:
    SftpAgent(ICfg& cfg, ILogger& logger, ProcessIndicator& indicator);
    virtual ~SftpAgent();

    EdiResponse* Login(const wxString& userid, const wxString& pswd) 
        throw (AgentException);

    EdiResponse* ChangePassword(const wxString& userid, const wxString& pswd, const wxString& newpswd) 
        throw (AgentException);

    EdiResponse* PostFile(const wxString& fielName) 
        throw (AgentException);

    bool UploadFile(const wxString& fielName);

    EdiResponse* GetStatus() 
        throw (AgentException);

    bool IsConnected() const { return true; };
    bool IsLoggedOn() const { return m_loggedOn; };

	bool IsCompressed() const { return m_compressed; };

    wxString GetUserId() const { return m_userId; };
    wxString GetTransport() const { return "sftp"; };

protected:
    EdiResponse* GetResponse(NetAutoHdr& request);

private:
    bool UploadFileToSftp(wxString filename);
	wxString CreateCommandFile(wxString filename);
	
	wxString   m_userId;

    bool       m_loggedOn;
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
};


#endif /* __ADVPCS_SFTP_AGENT_H__ */ 
