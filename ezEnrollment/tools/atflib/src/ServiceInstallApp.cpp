/*
 *  $RCSfile: ServiceInstallApp.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ServiceInstallApp.cpp: implementation of the CServiceInstallApp class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/ServiceInstallApp.h>
#include <atf/SystemException.h>
#include <atf/AbstractAppFactory.h>

const char* REG_EVENT_PATH = "SYSTEM\\CurrentControlSet\\Services\\EventLog\\Application";

const char* CFG_SERVICE_NAME="name";
#define MAX_FILE_NAME_LEN 1024
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
int CServiceInstallApp::Run() {

    RegisterEventSource();

    CString cmd = m_moduleName;
    cmd += " ";
    cmd += ATF_MODE;
    cmd += "=";
    cmd += ATF_MODE_SERVICE;

    cmd += " ";
    cmd += ATF_CFG;
    cmd += "=";
    cmd += m_cfgLocation;

    SC_HANDLE service = NULL;
    service = CreateService(
            m_manager,               // SCManager database
            m_name,              // name of service
            m_name,              // name to display
            SERVICE_ALL_ACCESS,         // desired access
            SERVICE_WIN32_OWN_PROCESS,  // service type
            SERVICE_DEMAND_START,       // start type
            SERVICE_ERROR_NORMAL,       // error control type
            cmd,           // service's binary
            NULL,                       // no load ordering group
            NULL,                       // no tag identifier
            "",                         // dependencies
            NULL,                       // LocalSystem account
            NULL);                      // no password

    if(NULL == service) {
        THROW_SYSTEM_EXCEPTION("CreateService");
    }

    if(NULL != service) {
        if(!CloseServiceHandle(service)) {
            THROW_SYSTEM_EXCEPTION("Close service handle");
        }
    }
    return 0;
};

void CServiceInstallApp::Initialize() {
    m_name = m_cfg.GetParam(CFG_SERVICE_NAME);
    m_manager = OpenSCManager(NULL, NULL, SC_MANAGER_ALL_ACCESS);
    if(NULL == m_manager) {
        THROW_SYSTEM_EXCEPTION("Can't open SC Manager");
    }
    char tmp[MAX_FILE_NAME_LEN];
    if(0 == GetModuleFileName(NULL, tmp, MAX_FILE_NAME_LEN)) {
        THROW_SYSTEM_EXCEPTION("Can't get module name");
    }
    m_moduleName = tmp;
    m_cfgLocation = m_cfg.GetParam(ATF_CFG);
};

void  CServiceInstallApp::RegisterEventSource() {
    HKEY     regKey = NULL;
    HKEY     newKey = NULL; 
    DWORD    eventType = 0x07;
    LONG     result = ERROR_SUCCESS;

    result = RegOpenKeyEx(HKEY_LOCAL_MACHINE, 
                          REG_EVENT_PATH, 
                          0, 
                          KEY_ALL_ACCESS, 
                          &regKey);
    if ( ERROR_SUCCESS != result ) {
        CString msg = "Can't open \'";
        msg += REG_EVENT_PATH;
        msg += "\'";
        THROW_SYSTEM_EXCEPTION(msg);        
    };

    if( ERROR_SUCCESS != RegCreateKey(regKey, m_name, &newKey) ) {
        CString msg = "Can't create \'";
        msg += REG_EVENT_PATH;
        msg += "\\";
        msg += m_name;
        msg += "\'";
        THROW_SYSTEM_EXCEPTION(msg);        
    }

    result = RegSetValueEx(newKey, 
                           "EventMessageFile", 
                           0, 
                           REG_SZ, 
                           (const unsigned char*)((const char*)m_moduleName), 
                           m_moduleName.GetLength());
    if ( ERROR_SUCCESS != result) {
        THROW_SYSTEM_EXCEPTION("Can't register \'EventMessageFile\'");        
    }

    result = RegSetValueEx(newKey, "TypesSupported", 0, REG_DWORD, 
             (const unsigned char *)&eventType, sizeof(DWORD));

    if ( ERROR_SUCCESS != result) {
        THROW_SYSTEM_EXCEPTION("Can't register \'TypesSupported\'");        
    }

    if ( NULL != newKey ) {
        CloseHandle(newKey);
    }
    if ( NULL != regKey ) {
        CloseHandle(regKey);
    }
};
