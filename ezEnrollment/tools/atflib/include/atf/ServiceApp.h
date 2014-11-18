/*
 *  $RCSfile: ServiceApp.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ServiceApp.h: interface for the CServiceApp class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_SERVICEAPP_H__AC489153_BEE9_4909_A036_EE410D323C72__INCLUDED_)
#define AFX_SERVICEAPP_H__AC489153_BEE9_4909_A036_EE410D323C72__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IApp.h>
#include <atf/IService.h>
#include <atf/Cfg.h>
#include <atf/Logger.h>

#pragma warning( disable : 4290 ) 

extern const char * ATF_CFG_SERVICE_NAME;

class CServiceApp : public IApp {
public:
    CServiceApp(ICfg &cfg, IService * svc);
    virtual ~CServiceApp();
    int Run() throw (CSystemException);
    void Initialize() throw (CAtfException);
protected:
    ILogger& GetLogger() const { return *m_logger; };
    const CString& GetName() const { return m_name; };
private:
    ICfg*  GetLoggerCfg();
private:
    ICfg & m_cfg;
    CString m_name;
    ILogger  *m_logger;
};

#endif // !defined(AFX_SERVICEAPP_H__AC489153_BEE9_4909_A036_EE410D323C72__INCLUDED_)
