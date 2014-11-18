/*
 *  $RCSfile: AbstractAppFactory.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// AbstractAppFactory.h: interface for the CAbstractAppFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ABSTRACTAPPFACTORY_H__87A8B87A_D63C_4495_92D4_A1B08B7D91B7__INCLUDED_)
#define AFX_ABSTRACTAPPFACTORY_H__87A8B87A_D63C_4495_92D4_A1B08B7D91B7__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#pragma warning( disable : 4290 ) 

#include <atf/IAppFactory.h>
#include <atf/XmlDocument.h>
#include <atf/CfgException.h>

extern const char * ATF_MODE;
extern const char * ATF_MODE_SERVICE;
extern const char * ATF_MODE_CMDLINE;
extern const char * ATF_MODE_INSTALL;
extern const char * ATF_MODE_UNINSTALL;
extern const char * ATF_MODE_TEST;
extern const char * ATF_MODE_USAGE;
extern const char * ATF_CFG;

class CAbstractAppFactory : public IAppFactory {
public:
    CAbstractAppFactory():m_appConfiguration(NULL),m_xmlAppConfiguration(NULL){};
    virtual ~CAbstractAppFactory() { 
        if ( NULL != m_appConfiguration ) {
            delete m_appConfiguration; 
            m_appConfiguration = NULL;
        }
        if ( NULL != m_xmlAppConfiguration ) {
            delete m_xmlAppConfiguration; 
            m_xmlAppConfiguration = NULL;
        }
        
    };

    IApp * CreateApplication(ICfg &cfg) throw (CSystemException, 
                                               CConfigurationException, 
                                               CAtfException);
    virtual IApp* CreateServiceApplication(ICfg &cfg) throw (CSystemException, 
                                                             CConfigurationException, 
                                                             CAtfException)= 0;
    virtual IApp* CreateCmdLineApplication(ICfg &cfg) throw (CSystemException, 
                                                             CConfigurationException, 
                                                             CAtfException)= 0;
    virtual IApp* CreateInstallApplication(ICfg &cfg) throw (CSystemException, 
                                                             CConfigurationException, 
                                                             CAtfException)= 0;
    virtual IApp* CreateUninstallApplication(ICfg &cfg) throw (CSystemException, 
                                                               CConfigurationException, 
                                                               CAtfException)= 0;
    virtual IApp* CreateTestServiceApplication(ICfg &cfg) throw (CSystemException, 
                                                                 CConfigurationException, 
                                                                 CAtfException)= 0;
    virtual IApp* CreateUsageApplication(ICfg &cfg) throw (CSystemException, 
                                                           CConfigurationException, 
                                                           CAtfException)= 0;

private:
    void BuildAppConfiguration(ICfg& cfg) throw (CConfigurationException);

    ICfg * GetAppConfiguration() const {return m_appConfiguration;};
private:
    ICfg * m_appConfiguration;
    CXmlDocument * m_xmlAppConfiguration; 
};

#endif // !defined(AFX_ABSTRACTAPPFACTORY_H__87A8B87A_D63C_4495_92D4_A1B08B7D91B7__INCLUDED_)
