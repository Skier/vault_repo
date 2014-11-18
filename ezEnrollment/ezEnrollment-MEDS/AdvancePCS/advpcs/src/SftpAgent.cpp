/*
 *  $RCSfile: SftpAgent.cpp,v $
 *
 *  $Revision: 1.0 $
 *
 *  last change: $Date: 2006/09/25 15:19:46 $
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
#include <advpcs/SftpAgent.h>
#include <advpcs/Resources.h>
#include <advpcs/AgentException.h>
#include <advpcs/EdiResponse.h>
#include <advpcs/ProcessIndicator.h>
#include <ZipArchive.h>
#include <ZipException.h>
#include <ZipMemFile.h> 
/* -------------------------- implementation place -------------------------- */

static wxString SFTP_NODE(wxT(           "sftp"                       ));
static wxString SFTP_HOST_ATTR(wxT(      "host"                       ));
static wxString SFTP_PORT_ATTR(wxT(      "port"                       ));
static wxString SFTP_LOGIN_ATTR(wxT(     "login"                      ));
static wxString SFTP_PASSWORD_ATTR(wxT(  "password"                   ));
static wxString SFTP_REMOTE_DIR_ATTR(wxT("remote_dir"                 ));
static wxString SFTP_CLIENT_ATTR(wxT(    "client"                     ));
static wxString SFTP_COMPRESSED_ATTR(wxT("compressed"                 ));
static wxString USE_LOGIN_ATTR(wxT(      "use_login_popup"            ));
static wxString WRONG_SFTP_CFG(wxT(      "Invalid sftp configuration "));

static wxString ADVPCS_DEFAULT_SFTP_CLIENT(wxT( "WinSCP3.com"));
static wxString ADVPCS_SFTP_COMMAND_FILE(wxT(   "command.ftp"));

SftpAgent::SftpAgent(ICfg& cfg, ILogger& logger, ProcessIndicator& indicator)
    : Agent(cfg, logger), m_loggedOn(false), m_compressed(false), m_userId(wxEmptyString)
{
    if ( ! IsEnabled() ) {
        return;
//		wxMessageBox("Sftp is disabled !");
    } 

	m_compressed = GetCfg().GetParamAsBool(SFTP_COMPRESSED_ATTR, true);
	m_sftpUseLogin = GetCfg().GetParamAsBool(USE_LOGIN_ATTR, false);
	
	if ( GetCfg().HasParam(SFTP_HOST_ATTR.c_str()) ) {
		m_sftpHost = GetCfg().GetParam(SFTP_HOST_ATTR.c_str());
	} else {
		THROW_SYSTEM_EXCEPTION(WRONG_SFTP_CFG + " [" + SFTP_HOST_ATTR + "]");
	}
	if ( GetCfg().HasParam(SFTP_PORT_ATTR.c_str()) ) {
		m_sftpPort = GetCfg().GetParam(SFTP_PORT_ATTR.c_str());
	} else {
		m_sftpPort = wxString("22");
	}
	if ( GetCfg().HasParam(SFTP_LOGIN_ATTR.c_str()) ) {
		m_sftpLogin = GetCfg().GetParam(SFTP_LOGIN_ATTR.c_str());
	} else  if ( m_sftpUseLogin ) {
		m_sftpLogin = wxEmptyString;
	} else {
		THROW_SYSTEM_EXCEPTION(WRONG_SFTP_CFG + "[" + SFTP_LOGIN_ATTR + "]");
	}
	if ( GetCfg().HasParam(SFTP_PASSWORD_ATTR.c_str()) ) {
		m_sftpPassword = GetCfg().GetParam(SFTP_PASSWORD_ATTR.c_str());
	} else {
		m_sftpPassword = wxEmptyString;
	}
	if ( GetCfg().HasParam(SFTP_REMOTE_DIR_ATTR.c_str()) ) {
		m_sftpRemoteDir = GetCfg().GetParam(SFTP_REMOTE_DIR_ATTR.c_str());
	} else {
		m_sftpRemoteDir = wxEmptyString;
	}
	if ( GetCfg().HasParam(SFTP_CLIENT_ATTR.c_str()) ) {
		m_sftpClient = GetCfg().GetParam(SFTP_CLIENT_ATTR.c_str());
	} else {
		m_sftpClient = ADVPCS_DEFAULT_SFTP_CLIENT;
	}
};


SftpAgent::~SftpAgent() {
};


EdiResponse*  SftpAgent::Login(const wxString& userid, const wxString& pswd) 
    throw (AgentException) 
{
    THROW_SYSTEM_EXCEPTION("Login functionality does not supported in Sftp Agent");
};

EdiResponse* SftpAgent::ChangePassword(const wxString& userid, const wxString& pswd, const wxString& newpswd) 
        throw (AgentException)
{
    THROW_SYSTEM_EXCEPTION("ChangePassword functionality does not supported in Sftp Agent");
};

EdiResponse* SftpAgent::PostFile(const wxString& fileName) 
    throw (AgentException) 
{
    THROW_SYSTEM_EXCEPTION("PostFile functionality does not supported in Sftp Agent");
};

bool SftpAgent::UploadFile(const wxString& fileName)     
{

	LOG_INFO(GetLogger(), 0, "Using SFTP transport for uploading file");
	LOG_INFO(GetLogger(), 0, ADVPCS_UPLOAD_WAITING);
    ::wxYield();

	wxString fName(fileName);

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

	wxFileName fn(fName);

	if ( !UploadFileToSftp( fn.GetFullPath() ) ) {
		return false;
	} else {
	    return true;
	}
};

EdiResponse* SftpAgent::GetStatus() 
    throw (AgentException) 
{
    THROW_SYSTEM_EXCEPTION("GetStatus functionality does not supported in Sftp Agent");
};


EdiResponse* SftpAgent::GetResponse(NetAutoHdr& request) 
    throw (AgentException) 
{
    THROW_SYSTEM_EXCEPTION("GetResponse functionality does not supported in Sftp Agent");
};


wxString SftpAgent::CreateCommandFile(wxString filename)
{
	wxFile out(ADVPCS_SFTP_COMMAND_FILE, wxFile::write);

    if ( !out.IsOpened() ) {
        THROW_SYSTEM_EXCEPTION(ADVPCS_SFTP_COMMAND_FILE);
    }
	
	wxString cmds;
	cmds << "option batch on" << "\r\n";
	cmds << "option confirm off" << "\r\n";
	cmds << "open " << m_sftpLogin << ":" << m_sftpPassword << "@" << m_sftpHost << ":" << m_sftpPort << "\r\n";
	if ( !m_sftpRemoteDir.IsEmpty() ) {
		cmds << "cd " << m_sftpRemoteDir << "\r\n";
	}
	cmds << "put " << filename << "\r\n";
	cmds << "close" << "\r\n";
	cmds << "exit" << "\r\n";
    out.Write(cmds);
	out.Close();
	return ADVPCS_SFTP_COMMAND_FILE;
};	


bool SftpAgent::UploadFileToSftp(wxString filename)
{
	if ( m_sftpUseLogin ) {
		LoginFrame* login = new LoginFrame(::wxTheApp->GetTopWindow(), m_logon);
		while (true) {
			if ( !(wxID_OK == login->ShowModal()) ) {
				 return false;
			} else {
				m_sftpLogin    = m_logon.GetLogin();
				m_sftpPassword = m_logon.GetPassword();
				break;
			}
		};
		wxDELETE(login);
	}
	
	wxString cmd;
	cmd << m_sftpClient;
	cmd << " /console";
	cmd << " /script=" << CreateCommandFile(filename);
	wxArrayString output, errors;

	wxBusyCursor wait;
	::wxBeginBusyCursor(wxHOURGLASS_CURSOR);
	int code = wxExecute(cmd, output, errors);
	::wxEndBusyCursor();

	if ( code != -1 ) {
		for ( size_t n = 0; n < output.GetCount(); n++ ) {
			LOG_INFO(GetLogger(), 0, output[n]);
		}
		for ( size_t m = 0; m < errors.GetCount(); m++ ) {
			LOG_WARN(GetLogger(), 0, errors[m]);
		}
		if ( 0 == code ) {
			return true;
		} else {
			return false;
		}
	} else {
		return false;
	}
};
