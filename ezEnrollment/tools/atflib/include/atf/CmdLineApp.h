/*
 *  $RCSfile: CmdLineApp.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CmdLineApp.h: interface for the CCmdLineApp class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CMDLINEAPP_H__AD91C105_AD8C_4E38_BBB7_05470C629F23__INCLUDED_)
#define AFX_CMDLINEAPP_H__AD91C105_AD8C_4E38_BBB7_05470C629F23__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IApp.h>
#include <atf/Cfg.h>
#include <atf/IService.h>

class CConfigurationException;

class CCmdLineApp : public IApp {
public:
    CCmdLineApp(ICfg &cfg, IService * svc);
    virtual ~CCmdLineApp();
    void Initialize() throw (CConfigurationException);
    virtual int Run();
private:
    ICfg*  GetLoggerCfg();
private:
    IService *m_svc;
    ILogger  *m_logger;
    ICfg     &m_cfg;
};

#endif // !defined(AFX_CMDLINEAPP_H__AD91C105_AD8C_4E38_BBB7_05470C629F23__INCLUDED_)
