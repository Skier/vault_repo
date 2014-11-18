/*
 *  $RCSfile: AbstractAppFactory.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// AbstractAppFactory.cpp: implementation of the CAbstractAppFactory class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/AbstractAppFactory.h>
#include <atf/CfgException.h>
#include <string.h>
#include <atf/StreamIOHandler.h>
#include <fstream>
#include <atf/XmlLoader.h>
#include <atf/Environment.h>
#include <atf/CfgXml.h>

using namespace std;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

const char * ATF_MODE_SERVICE   = "service";
const char * ATF_MODE_CMDLINE   = "cmd";
const char * ATF_MODE_INSTALL   = "install";
const char * ATF_MODE_UNINSTALL = "uninstall";
const char * ATF_MODE_TEST      = "test";
const char * ATF_MODE_USAGE     = "usage";
const char * ATF_MODE           = "mode";
const char * ATF_CFG            = "cfg";
const char * ATF_CFG_FILE       = "file:";
const char * ATF_CFG_REGISTRY   = "registry:";

IApp * CAbstractAppFactory::CreateApplication(ICfg &cfg) {
    IApp * app = NULL;
    try {
        CString str = cfg.GetParam(ATF_MODE);
        if ( 0 == str.CompareNoCase(ATF_MODE_SERVICE) ) {

            BuildAppConfiguration(cfg);
            app = CreateServiceApplication(*GetAppConfiguration());

        } else if ( 0 == str.CompareNoCase(ATF_MODE_CMDLINE) ) {

            BuildAppConfiguration(cfg);
            app = CreateCmdLineApplication(*GetAppConfiguration());

        } else if ( 0 == str.CompareNoCase(ATF_MODE_INSTALL) ) {

//            BuildAppConfiguration(cfg);
            app = CreateInstallApplication(cfg);

        } else if ( 0 == str.CompareNoCase(ATF_MODE_UNINSTALL) ) {

            app = CreateUninstallApplication(cfg);

        } else if ( 0 == str.CompareNoCase(ATF_MODE_TEST) ) {

//            BuildAppConfiguration(cfg);
            app = CreateTestServiceApplication(cfg);

        } else if ( 0 == str.CompareNoCase(ATF_MODE_USAGE) ) {

            BuildAppConfiguration(cfg);
            app = CreateUsageApplication(cfg);

        } else {
            CString err("Unknown mode \"");
            err += str;
            err += "\"";
            LOG_ERROR((*(c_defaultEnvironment.GetLogger())), ATF_INVALID_CFG_ERR, err);
            app = CreateUsageApplication(cfg);
        }
    } catch ( CCfgException  ) {
        app = CreateUsageApplication(cfg);
    }
    return app;
};

void CAbstractAppFactory::BuildAppConfiguration(ICfg& cfg) {
    if ( !cfg.HasParam(ATF_CFG) ) {
        THROW_CFG_EXCEPTION("Configuration location param, not found");
    }

    CString location = cfg.GetParam(ATF_CFG);
    if ( location.IsEmpty() ) {
        THROW_CFG_EXCEPTION("Configuration location param, not found");
    }

        /* File based configuration */
    if ( 0 == strncmp(location, ATF_CFG_FILE, strlen(ATF_CFG_FILE)) ) {
        ifstream is(((const char*)location)+strlen(ATF_CFG_FILE));
        if ( !is.is_open() ) {
            CString msg("Can't open file \"");
            msg += (((const char*)location)+strlen(ATF_CFG_FILE));
            msg += "\"";
            THROW_CFG_EXCEPTION(msg);
        }
        CStreamIOHandler handler(is);
        CXmlLoader loader(handler);
        m_xmlAppConfiguration = new CXmlDocument();
        try {
            loader.Load(*m_xmlAppConfiguration);
            if( !m_xmlAppConfiguration->IsOk() ) {
                CString msg("Configuration not properly loaded from location \"");
                msg += location;
                msg += "\", may be empty";
                THROW_CFG_EXCEPTION(msg);
            }
            m_appConfiguration = new CCfgXml(*m_xmlAppConfiguration);
        } catch (CXmlLoadException ex) {
            if ( (*(c_defaultEnvironment.GetLogger())).IsFatalEnabled() ) {
                CString msg("Configuration's xml parsing error:");
                char codes[1024];
                sprintf(codes, "Error code: %ld, Error Line: %ld, Error:",ex.GetErrorCode(), ex.GetLineNumber());
                msg += codes;
                msg += ex.GetErrorString();
                c_defaultEnvironment.GetLogger()->Fatal(__FILE__, __LINE__, ATF_INVALID_CFG_ERR, msg);   
            }
            THROW_CFG_EXCEPTION("Invalid XML configuration file");
        }

        /* Registry based configuration */
    } else if ( 0 == strncmp(location, ATF_CFG_REGISTRY, strlen(ATF_CFG_REGISTRY)) ) {
        THROW_ATF_EXCEPTION(ATF_NO_IMPLEMENT_ERR, "NO_IMPLEMENT");
    } else {
        CString msg = "Unknown configuration location:\"";
        msg += location;
        msg += "\"";
        THROW_CFG_EXCEPTION(msg);
    }
};

