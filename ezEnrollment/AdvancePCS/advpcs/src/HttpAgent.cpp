/*
 *  $RCSfile: HttpAgent.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

#pragma warning(disable : 4786)
/* -------------------------- header place ---------------------------------- */
#include <memory.h>
#include <wx/wfstream.h>
#include <wx/filename.h>
#include <atf/Util.h>
#include <atf/SystemException.h>
#include <atf/Cfg.h>
#include <atf/Logger.h>
#include <atf/XmlDocument.h>
#include <atf/StringIOHandler.h>
#include <atf/XmlLoader.h>
#include <atf/Exception.h>
#include <advpcs/HttpAgent.h>
#include <advpcs/Resources.h>
#include <advpcs/AgentException.h>
#include <advpcs/EdiResponse.h>
#include <advpcs/ProcessIndicator.h>
#include <ZipArchive.h>
#include <ZipException.h>
#include <ZipMemFile.h> 
/* -------------------------- implementation place -------------------------- */

static const wxString boundary(wxT("---------------------------7d33b711bb9039a"));
static const wxString bodyHdr(wxT("--%s\r\nContent-Disposition: form-data; name=\"filename\"; filename=\"%s\"\r\nContent-type: application/octet-stream\r\n\r\n"));
static const wxString bodyZipHdr(wxT("--%s\r\nContent-Disposition: form-data; name=\"filename\"; filename=\"%s\"\r\nContent-type: application/x-zip-compressed\r\n\r\n"));

HttpAgent::HttpAgent(ICfg& cfg, ILogger& logger, ProcessIndicator& indicator)
    : Agent(cfg, logger), m_session(), m_connection(), m_wininetdll(NULL), m_loggedOn(false),
	  m_compressed(false), m_indicator(indicator), m_userId(wxEmptyString)
{
    if ( ! IsEnabled() ) {
        return;
    } 

    SetEnabled(false);

    m_wininetdll = GetModuleHandle(ADVPCS_INET_MODULE);
    if ( NULL == m_wininetdll ) {
        THROW_SYSTEM_EXCEPTION(ADVPCS_INET_MODULE);
    }

    m_session = InternetOpen(ADVPCS_AGENT_NAME,
                             INTERNET_OPEN_TYPE_PRECONFIG, NULL, NULL, 0);

    if ( m_session.IsEmpty() ) {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    m_secure = GetCfg().GetParamAsBool(ADVPCS_HTTP_AGENT_SECURE_CFG, true);
    m_host = GetCfg().GetParam(ADVPCS_HTTP_AGENT_HOST_CFG);
    
    m_port = m_secure ? INTERNET_DEFAULT_HTTPS_PORT : INTERNET_DEFAULT_HTTP_PORT;

    m_port = GetCfg().GetParamAsLong(ADVPCS_HTTP_AGENT_PORT_CFG, m_port);

    m_login = GetCfg().GetParam(ADVPCS_HTTP_AGENT_LOGIN_CFG);
	m_changePwd = GetCfg().GetParam(ADVPCS_HTTP_AGENT_CHANGEPWD_CFG);
    m_upload = GetCfg().GetParam(ADVPCS_HTTP_AGENT_UPLOAD_CFG);
    m_status = GetCfg().GetParam(ADVPCS_HTTP_AGENT_STATUS_CFG);
	m_compressed = GetCfg().GetParamAsBool(ADVPCS_HTTP_AGENT_COMPRESED_CFG);

    m_flags=INTERNET_FLAG_DONT_CACHE|INTERNET_FLAG_PRAGMA_NOCACHE|INTERNET_FLAG_RELOAD|INTERNET_FLAG_NO_CACHE_WRITE;

    if ( m_secure ) {
        m_flags = m_flags |INTERNET_FLAG_SECURE
                          |INTERNET_FLAG_IGNORE_CERT_CN_INVALID
                          |INTERNET_FLAG_IGNORE_CERT_DATE_INVALID
//https: added flags
                          |INTERNET_FLAG_HYPERLINK
                          |INTERNET_FLAG_KEEP_CONNECTION;
    }

    SetEnabled(true);
};


HttpAgent::~HttpAgent() {
};


EdiResponse*  HttpAgent::Login(const wxString& userid, const wxString& pswd) 
    throw (AgentException) 
{
    wxASSERT(IsEnabled());

#if 0
    m_indicator.StartProcess(1, "Logging...");
    AutoFinnisher a(m_indicator);
#else
	LOG_INFO(GetLogger(), 0, ADVPCS_LOGIN_WAITING);
    ::wxYield();
#endif    
	
	NetAutoHdr request;

    if ( m_connection.IsEmpty() ) {
        m_connection = InternetConnect(m_session, m_host, m_port, 
                                      NULL, NULL, 
                                      INTERNET_SERVICE_HTTP,
                                      0, 0);
        if ( m_connection.IsEmpty() ) {
            THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
        }
    }

//https: added "HTTP/1.1"
    request = HttpOpenRequest(m_connection, "POST",  m_login,  "HTTP/1.1", "", 
                              NULL,       m_flags, 0);

    if ( request.IsEmpty() ) {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

	// BF
    wxString body = wxString::Format("txtLoginId=%s&txtPassword=%s", userid, pswd);
    wxString headers = 
                wxString("Content-Type: application/x-www-form-urlencoded\r\n");

    if(!HttpSendRequest(request, headers.c_str(),headers.Length(),
                        (void*)(body.c_str()), body.Length()))
    {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    EdiResponse* response = GetResponse(request);
    if ( response->GetCode() != ADVPCS_LOGIN_OK - ADVPCS_BASE ) {
        LOG_ERROR(GetLogger(), response->GetCode(), response->GetMessage());
        m_userId = wxEmptyString;
    } else { 
        m_loggedOn = true;
        m_userId = userid;
    } 
    return response;
};

EdiResponse* HttpAgent::ChangePassword(const wxString& userid, const wxString& pswd, const wxString& newpswd) 
        throw (AgentException)
{
    wxASSERT(IsEnabled());

#if 0
    m_indicator.StartProcess(1, "Changing Password...");
    AutoFinnisher a(m_indicator);
#else
	LOG_INFO(GetLogger(), 0, ADVPCS_CHANGE_PSWD_WAITING);
    ::wxYield();
#endif

    NetAutoHdr request;

    if ( m_connection.IsEmpty() ) {
        m_connection = InternetConnect(m_session, m_host, m_port, 
                                      NULL, NULL, 
                                      INTERNET_SERVICE_HTTP,
                                      0, 0);
        if ( m_connection.IsEmpty() ) {
            THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
        }
    }

	printf("Connecting to %s", m_changePwd);
    request = HttpOpenRequest(m_connection, "POST",  m_changePwd,  NULL, "", 
                              NULL,       m_flags, 0);

    if ( request.IsEmpty() ) {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    wxString body = wxString::Format("txtLoginId=%s&txtPassword=%s&txtNewPassword=%s", userid, pswd, newpswd);
    wxString headers = 
                wxString("Content-Type: application/x-www-form-urlencoded\r\n");

    if(!HttpSendRequest(request, headers.c_str(),headers.Length(),
                        (void*)(body.c_str()), body.Length()))
    {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    EdiResponse* response = GetResponse(request);
    if ( response->GetCode() != ADVPCS_PSWD_OK - ADVPCS_BASE ) {
        LOG_ERROR(GetLogger(), response->GetCode(), response->GetMessage());
        m_userId = wxEmptyString;
    } else { 
        m_loggedOn = true;
        m_userId = userid;
    } 
    return response;
};

EdiResponse* HttpAgent::PostFile(const wxString& fileName) 
    throw (AgentException) 
{
    wxASSERT(IsEnabled());
    wxASSERT(IsConnected());
    wxASSERT(IsLoggedOn());

#if 0
    m_indicator.StartProcess(1, "File uploading...");
    AutoFinnisher a(m_indicator);
#else
	LOG_INFO(GetLogger(), 0, ADVPCS_UPLOAD_WAITING);
    ::wxYield();
#endif

    NetAutoHdr request;

	wxString fName(fileName);
    request = HttpOpenRequest(m_connection, "POST",  m_upload,  NULL, "", 
                              NULL,       m_flags, 0);

    if ( request.IsEmpty() ) {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    if ( IsCompressed() ) {
        try {
            CZipMemFile mf;

            wxFile f(fName);
            if ( !f.IsOpened() ) {
                THROW_SYSTEM_EXCEPTION(fName);
            }

            char b[1024];
            for(off_t readed = f.Read(b, 1024); readed != 0; readed = f.Read(b, 1024) ) {
                    mf.Write(b, readed);
            }
            f.Close();

            mf.SeekToBegin();
            wxFileName fn(fName);

			fName = fn.GetPath(wxPATH_GET_SEPARATOR)
					+ fn.GetName()
					+ ADVPCS_ZIP_EXT;

            CZipArchive arc;
            arc.Open(fName, CZipArchive::zipCreate);
            arc.AddNewFile(mf, fn.GetFullName());
            arc.Close();
        } catch (CZipException& ex) {
            THROW_ATF_EXCEPTION(0, ex.GetErrorDescription());
        }
        
	} 

    wxFile fi(fName);

    if ( !fi.IsOpened() ) {
        THROW_SYSTEM_EXCEPTION(fileName);
    }

	wxFileName fn(fName);
	
	wxString hdr = wxString();

    if ( IsCompressed() ) {
		hdr = wxString::Format(bodyZipHdr, boundary, fn.GetFullName() );
	} else {
		hdr = wxString::Format(bodyHdr, boundary, fn.GetFullName() );
	}
    wxString trlr = wxString::Format("\r\n--%s--\r\n", boundary);

    size_t len = fi.Length() + hdr.Length() + trlr.Length();
    char * content = new char[len];
    if ( NULL == content ) {
        THROW_SYSTEM_EXCEPTION("No memory");
    }

    CCharHolder holder(content);
    memcpy(content, hdr.c_str(), hdr.Length());
    char * f = content + hdr.Length();
    while ( !fi.Eof() ) {
        int readen = fi.Read((void*)f, fi.Length());
        if ( readen != fi.Length() ) {
            THROW_SYSTEM_EXCEPTION(fName);
        }
    }

    f += fi.Length();
    memcpy(f, trlr.c_str(), trlr.Length()); 

	

    wxString headers = 
        wxString::Format("Content-Type: multipart/form-data; boundary=%s\r\n", boundary) 
              + wxString::Format("Content-Length: %ld", len);

    if(!HttpSendRequest(request, headers.c_str(),headers.Length(),
                        (void*)content, len))
    {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    EdiResponse* response = GetResponse(request);
    
    if ( response->GetCode() != ADVPCS_POST_FILE_RECEIVED_OK - ADVPCS_BASE ) {
        LOG_ERROR(GetLogger(), response->GetCode(), response->GetMessage());
    } 

    return response;
};

EdiResponse* HttpAgent::GetStatus() 
    throw (AgentException) 
{
    wxASSERT(IsEnabled());
    wxASSERT(IsConnected());
    wxASSERT(IsLoggedOn());

#if 0
    m_indicator.StartProcess(1, "Getting status...");
    AutoFinnisher a(m_indicator);
#else
	LOG_INFO(GetLogger(), 0, ADVPCS_GET_STATUS_WAITING);
    ::wxYield();
#endif

    NetAutoHdr request;

    request = HttpOpenRequest(m_connection, "POST",  m_status,  NULL, "", 
                              NULL,       m_flags, 0);

    if ( request.IsEmpty() ) {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    wxString headers = 
                wxString("Content-Type: application/x-www-form-urlencoded\r\nContent-Length: 0");

    if( !HttpSendRequest(request, headers.c_str(),headers.Length(),NULL,0) ) {
        THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
    }

    EdiResponse* response = GetResponse(request);
    
   if ( response->GetCode() != ADVPCS_STATUS_OK - ADVPCS_BASE ) {
        LOG_ERROR(GetLogger(), response->GetCode(), response->GetMessage());
    } 

    return response;
};

wxString HttpAgent::GetInetMessage(DWORD error) 
{
    wxASSERT(NULL != m_wininetdll );

    char buffer[1024];

    ::FormatMessage(FORMAT_MESSAGE_FROM_HMODULE, m_wininetdll, error, 0, buffer, 1024, NULL);
    return wxString(buffer);
};


EdiResponse* HttpAgent::GetResponse(NetAutoHdr& request) {
    wxString response = wxEmptyString;
    char buffer[1024];
    DWORD readed;
    while( true ) {
		if ( !InternetReadFile(request, buffer, sizeof(buffer), &readed) ) {
			THROW_SYSTEM_EXCEPTION(GetInetMessage(::GetLastError()));
		}
        response.Append(buffer, readed);
//        if ( readed < sizeof(buffer) ) {
		if ( 0 == readed ) {
			break;
        }
	};

#ifdef __WXDEBUG__
    LOG_DEBUG(GetLogger(), ATF_DEBUG, response);
#endif
	
    CXmlDocument * doc = new CXmlDocument();

    try {
        CStringIOHandler handler(response);
        CXmlLoader loader(handler);

        loader.Load(*doc);

        EdiResponse* resp = new EdiResponse(doc);
        if ( 0 == resp->GetCode()%10 ){
			LOG_INFO(GetLogger(), 0, wxString::Format("[%ld] %s",resp->GetCode(), resp->GetMessage()));
		} else {
			LOG_WARN(GetLogger(), 0, wxString::Format("[%ld] %s",resp->GetCode(), resp->GetMessage()));
		}

        return resp;
    } catch (CXmlLoadException & ex) {
		wxDELETE(doc);
		throw ex;
    } catch (...) {
        wxDELETE(doc);
        throw;
    }

    return NULL;

};




