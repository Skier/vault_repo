/*
 *  $RCSfile: ServiceUninstallApp.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ServiceUninstallApp.cpp: implementation of the CServiceUninstallApp class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/ServiceUninstallApp.h>
#include <atf/ServiceInstallApp.h>
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

int CServiceUninstallApp::Run(){
    SERVICE_STATUS    serviceStatus;       
    SC_HANDLE         service;

    service = OpenService(m_manager, m_name, SERVICE_ALL_ACCESS);
    if (!service) {
        THROW_SYSTEM_EXCEPTION(m_name);
    }

    if(!QueryServiceStatus(service, &serviceStatus)) {
        THROW_SYSTEM_EXCEPTION(m_name);
    }

    if(SERVICE_STOPPED != serviceStatus.dwCurrentState){
        THROW_ATF_EXCEPTION(ATF_SERVICE_ERR, "Service does not stopped, Stop it first.");
    }
    if(!DeleteService(service) ) {
        THROW_SYSTEM_EXCEPTION(m_name);
    }
    UnregisterEventSource();

    if(NULL != service) {
        if(!CloseServiceHandle(service)) {
            THROW_SYSTEM_EXCEPTION("Close service handle");
        }
    }
    return 0;
};

void CServiceUninstallApp::Initialize(){
    m_name = m_cfg.GetParam(CFG_SERVICE_NAME);
    m_manager = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);
    if(NULL == m_manager) {
        THROW_SYSTEM_EXCEPTION("Can't open SC Manager");
    }
};

void CServiceUninstallApp::UnregisterEventSource(){
    HKEY regKey = NULL;

    LONG result = ERROR_SUCCESS;
    result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, REG_EVENT_PATH, 0,
                     KEY_ALL_ACCESS, &regKey);
    if ( ERROR_SUCCESS != result ) {
        CString msg = "Can't open key \'";
        msg += REG_EVENT_PATH;
        msg += "\'";
        THROW_SYSTEM_EXCEPTION(msg);
    }

    result = RegDeleteKey(regKey, m_name);
    if ( ERROR_SUCCESS != result ) {
        CString msg = "Can't delete key \'";
        msg += REG_EVENT_PATH;
        msg += "\\";
        msg += m_name;
        msg += "\'";
        THROW_SYSTEM_EXCEPTION(msg);
    }

    RegCloseKey(regKey);
};
