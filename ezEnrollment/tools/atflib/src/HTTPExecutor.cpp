/*
 *  $RCSfile: HTTPExecutor.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/09/09 07:44:32 $
 */
// CdHTTPExecutor.cpp: implementation of the CCdHTTPExecutor class.
//
//////////////////////////////////////////////////////////////////////
#include <atf/CfgException.h>
#include <atf/SystemException.h>
#include <atf/Exception.h>
#include <atf/AssertException.h>
#include <atf/Cfg.h>
#include <atf/Thread.h>
#include <atf/HTTPExecutor.h>
#include <wininet.h>
#include <fstream>

using namespace std;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

class CInetHandleCloser {
public:
    CInetHandleCloser(HINTERNET handle):m_handle(handle){};
    ~CInetHandleCloser(){
        if( NULL != m_handle ) {
            InternetCloseHandle(m_handle);
        }
    };
private:
    HINTERNET m_handle;
};

static const char * ATF_AGENT="Affilia telecommunication framework AGENT";

void CHTTPExecutor::Initialize(const CString& serverName, unsigned short port,
                    const CString& userName, const CString& password,
                    const CString& objectName, const CString& url, 
                    const CString& varName, const CString& workDir, 
                    const CString& doneDir, const CString& errorDir)
{
    IDoneExecutor::Initialize(doneDir, errorDir);
    m_serverName   = serverName;
    m_port         = port;
    m_userName     = userName; 
    m_password     = password;
    m_objectName   = objectName;
    m_url          = url;
    m_varName      = varName;
    m_workDir      = workDir;
}


CHTTPExecutor::~CHTTPExecutor() {
}

void CHTTPExecutor::Execute(IMessage& msg, ILogger& logger) {
    DWORD errorCode = 0;

    CString fileName = GetFileName(msg);


    HINTERNET hINET = InternetOpen(ATF_AGENT,
                                   INTERNET_OPEN_TYPE_PRECONFIG,
                                   NULL,
                                   NULL,
                                   0);
    if ( NULL == hINET ) {
        THROW_SYSTEM_EXCEPTION("Can't open internet");
    };

    CInetHandleCloser inet(hINET);

    HINTERNET hICON = InternetConnect(
                          hINET, m_serverName, m_port,
                          m_userName, m_password,INTERNET_SERVICE_HTTP, 0, 0);
    if ( NULL == hICON ) {
        THROW_SYSTEM_EXCEPTION("Can't get internet connection");
    };
    CInetHandleCloser icon(hICON);


    HINTERNET hREQ = HttpOpenRequest( 
                          hICON, "POST", m_url,
                          NULL, NULL, NULL,
                          INTERNET_FLAG_DONT_CACHE |INTERNET_FLAG_PRAGMA_NOCACHE,  0);
    if ( NULL == hREQ ) {
        THROW_SYSTEM_EXCEPTION("Can't open request");
    };

    CInetHandleCloser ireq(hREQ);
  
    CString header = "Content-Type: application/x-www-form-urlencoded";

    CString message(m_varName);
    message += "=";
    message += fileName;

    if (!HttpSendRequest(hREQ, header, header.GetLength(), 
                        (void *)((const char*)message), message.GetLength()))
    {
        THROW_SYSTEM_EXCEPTION("Can't send request");
    };

    LOG_DEBUG(logger, ATF_INFO, "HTTP executor: message properly sent");

    return;
};


CString CHTTPExecutor::GetFileName(IMessage & msg) {
    CString result = msg.GetFileName();
    if ( msg.GetFileName().IsEmpty() ) {
        result = m_workDir;
        result += "\\";
        result += msg.GetTransactionID();
        result += ".";
        result += msg.GetType();
        WriteFile(result, msg);
    }
    return result;
};

