/*
 *  $RCSfile: Agent.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_AGENT_H__
#define __ADVPCS_AGENT_H__


class wxString;
class CXmlDocument;
class CAtfException; 
class ICfg;
class ILogger;
class AgentException;
class EdiResponse;

#pragma warning( disable : 4290 )

class Agent {
public:
    Agent(ICfg& cfg, ILogger& logger);
    virtual ~Agent() {};

    virtual EdiResponse* Login(const wxString& userid, const wxString& pswd) 
        throw (AgentException) = 0;

    virtual EdiResponse* ChangePassword(const wxString& userid, const wxString& pswd, const wxString& newpswd) 
        throw (AgentException) = 0;

    virtual EdiResponse* PostFile(const wxString& fielName) 
        throw (AgentException) = 0;

    virtual bool UploadFile(const wxString& fileName) = 0;

    virtual EdiResponse* GetStatus() 
        throw (AgentException) = 0;

    virtual bool IsEnabled() const { return m_enabled; };
    virtual bool IsConnected() const = 0;
    virtual bool IsLoggedOn() const = 0;
	virtual wxString GetTransport() const = 0;

    virtual wxString GetUserId() const = 0;
protected:
    ILogger& GetLogger() const { return m_logger; };
    ICfg&    GetCfg() const { return m_cfg; };
    void SetEnabled(bool value) { m_enabled = value; };

private:
    ILogger& m_logger;
    ICfg&    m_cfg;
    bool     m_enabled;
};

#endif /* __ADVPCS_AGENT_H__ */
