/*
 *  $RCSfile: ServiceUninstallApp.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ServiceUninstallApp.h: interface for the CServiceUninstallApp class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_SERVICEUNINSTALLAPP_H__80605AAF_9491_493E_BCA6_555672B11DEF__INCLUDED_)
#define AFX_SERVICEUNINSTALLAPP_H__80605AAF_9491_493E_BCA6_555672B11DEF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IApp.h>
#include <atf/Logger.h>
#include <atf/SystemException.h>
#include <atf/Exception.h>
#include <atf/Cfg.h>
#include <atf/Environment.h>

#pragma warning( disable : 4290 ) 

class CServiceUninstallApp : public IApp { 
public:
    CServiceUninstallApp(ICfg &cfg): 
        m_cfg(cfg), m_name(""), m_manager(NULL){};
    virtual ~CServiceUninstallApp(){
        if(NULL != m_manager) {
            CloseServiceHandle(m_manager);
        }
    };
    int Run() throw (CSystemException);
    void Initialize() throw (CAtfException);
protected:
    void  UnregisterEventSource() throw (CSystemException);
protected:
    ILogger& GetLogger() const { return *c_defaultEnvironment.GetLogger();};
    const CString& GetName() const { return m_name; };
private:
    ICfg      &m_cfg; 
    CString    m_name;
    SC_HANDLE  m_manager;
};

#endif // !defined(AFX_SERVICEUNINSTALLAPP_H__80605AAF_9491_493E_BCA6_555672B11DEF__INCLUDED_)
