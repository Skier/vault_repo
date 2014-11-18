/*
 *  $RCSfile: ServiceInstallApp.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ServiceInstallApp.h: interface for the CServiceInstallApp class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_SERVICEINSTALLAPP_H__B6708337_8458_4353_992E_52AFE648B2EA__INCLUDED_)
#define AFX_SERVICEINSTALLAPP_H__B6708337_8458_4353_992E_52AFE648B2EA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IApp.h>
#include <atf/Environment.h>

#pragma warning( disable : 4290 ) 

extern const char* CFG_SERVICE_NAME;
extern const char* REG_EVENT_PATH;

class CServiceInstallApp : public IApp{
public:
    CServiceInstallApp(ICfg &cfg) : 
        m_cfg(cfg), m_name(""), m_manager(NULL), 
        m_moduleName(""), m_cfgLocation(""){};
    virtual ~CServiceInstallApp(){
        if(NULL != m_manager) {
            CloseServiceHandle(m_manager);
        }
    };
    int Run() throw (CSystemException);
    void Initialize() throw (CAtfException);
protected:
    void  RegisterEventSource() throw (CSystemException);
protected:
    ILogger& GetLogger() const { return *c_defaultEnvironment.GetLogger();};
    const CString& GetName() const { return m_name; };
private:
    ICfg      &m_cfg; 
    CString    m_name;
    CString    m_moduleName;
    CString    m_cfgLocation;
    SC_HANDLE  m_manager;
};

#endif // !defined(AFX_SERVICEINSTALLAPP_H__B6708337_8458_4353_992E_52AFE648B2EA__INCLUDED_)
