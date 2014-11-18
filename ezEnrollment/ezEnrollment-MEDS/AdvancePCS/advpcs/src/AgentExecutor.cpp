/*
 *  $RCSfile: AgentExecutor.cpp,v $
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

/* -------------------------- header place ---------------------------------- */
#include <wx/filename.h>
#include <atf/CfgXml.h>
#include <atf/Logger.h>
#include <atf/XmlLoadException.h>
#include <advpcs/AgentExecutor.h>
#include <advpcs/Agent.h>
#include <advpcs/AgentException.h>
#include <advpcs/App.h>
#include <advpcs/Resources.h>
#include <advpcs/StatusList.h>
#include <advpcs/EdiResponse.h>
#include <advpcs/PassUpdateFrame.h>
#include <advpcs/EdiStatus.h>
/* -------------------------- implementation place -------------------------- */
AgentExecutor::AgentExecutor(ILogger& logger, StatusList& list, Agent& agent) 
    :m_logger(logger), m_list(list), m_agent(agent), m_connected(false)
{};

bool AgentExecutor::UploadFile(const wxString& fileName) {
	if ( !(0 == GetAgent().GetTransport().CmpNoCase("sftp")) ) {
		if ( !IsConnected() ) {
			if ( !Login() ) {
				return false;
			}
		}
		while (true) {
			try {
				std::auto_ptr<EdiResponse> res(GetAgent().PostFile(fileName) );

				wxString fName(fileName);
				wxFileName fn(fName);

				if ( wxGetApp().GetCfg()->GetChild(ADVPCS_AGENT_CFG)->GetParamAsBool(ADVPCS_HTTP_AGENT_COMPRESED_CFG) ) {
					fName = fn.GetPath(wxPATH_GET_SEPARATOR) + fn.GetName() + ADVPCS_ZIP_EXT;
				} else {
					fName = fn.GetPath(wxPATH_GET_SEPARATOR) + fn.GetName() + ADVPCS_EDI_EXT;
				}

				if ( res->GetCode() == ADVPCS_POST_FILE_RECEIVED_OK - ADVPCS_BASE ) {
					LOG_INFO( GetLogger(), 0, wxString::Format(ADVPCS_UPLOAD_OK, fName));
					 return true;
				}

				::wxMessageBox(wxString::Format("[%ld] %s", res->GetCode(), res->GetMessage()), 
								   ADVPCS_ERROR_TITLE, 
								   wxOK | wxCENTRE | wxICON_ERROR);

				if ( res->GetCode() == ADVPCS_LOGIN_AUTH_ERR - ADVPCS_BASE 
					|| res->GetCode() == ADVPCS_POST_AUTH_ERR - ADVPCS_BASE ) {
					m_connected = false;
					if ( !Login() ) {
						return false;
					} else {
						continue;
					}
				}
				m_connected = false;
				return false;

			} catch ( CXmlLoadException& ) {
				wxMessageBox(ADVPCS_UNEXPECTED_REPLY_MSG, ADVPCS_ERROR_TITLE,
							  wxOK | wxCENTRE | wxICON_ERROR );
				m_connected = false;
				return false;
			} catch ( CAtfException& ex) {
				wxMessageBox((const char*)ex.GetText(), ADVPCS_ERROR_TITLE,
							  wxOK | wxCENTRE | wxICON_ERROR );
				m_connected = false;
				return false;
			};

		}
	} else {
		return GetAgent().UploadFile(fileName);
	}

};

bool AgentExecutor::RequestStatus() {
    if (!IsConnected() ) {
		if ( !Login() ) {
			return false;
        }
    }      
        
	while ( true ) {
		try {
			std::auto_ptr<EdiResponse> res(GetAgent().GetStatus());

			if ( res->GetCode() == ADVPCS_STATUS_OK - ADVPCS_BASE ) {
				FillStatus(res.get());
				return true;
			}

			if ( res->GetCode() == ADVPCS_STATUS_NOT_AVAILABLE_ERR - ADVPCS_BASE ) {
				return false;
			}

			::wxMessageBox(wxString::Format("[%ld] %s", res->GetCode(), res->GetMessage()), 
									   ADVPCS_ERROR_TITLE, 
									   wxOK | wxCENTRE | wxICON_ERROR);

			if ( res->GetCode() == ADVPCS_LOGIN_AUTH_ERR - ADVPCS_BASE ) {
//				|| res->GetCode() == ADVPCS_STATUS_AUTH_ERR - ADVPCS_BASE ) {
				m_connected = false;
                if ( !Login() ) {
					return false;
				} else {
					continue;
				}
			}
			m_connected = false;
			return false;

		} catch ( CXmlLoadException&  ) {
			wxMessageBox(ADVPCS_UNEXPECTED_REPLY_MSG, ADVPCS_ERROR_TITLE,
						  wxOK | wxCENTRE | wxICON_ERROR );
			m_connected = false;
			return false;
		} catch ( CAtfException& ex) {
			wxMessageBox((const char*)ex.GetText(), ADVPCS_ERROR_TITLE,
						  wxOK | wxCENTRE | wxICON_ERROR );
			m_connected = false;
			return false;
		};
	};
};

bool AgentExecutor::Login(){
    return true;
};

bool AgentExecutor::ChangePassword() {
    PassUpdateFrame* passwd = new PassUpdateFrame(wxTheApp->GetTopWindow(), m_logon);
    try {
		while(true) {
			if ( !(wxID_OK == passwd->ShowModal()) ) {
				return false;
			}
#if 0
			if ( !Login(m_logon.GetLogin(), m_logon.GetPassword()) ) {
				continue;
			}
#else
			if ( !IsConnected() ) {
				std::auto_ptr<EdiResponse> res(GetAgent().Login(m_logon.GetLogin(), m_logon.GetPassword()));
				if ( res->GetCode() == ADVPCS_LOGIN_OK - ADVPCS_BASE ) {
					m_connected = true;
				}
				if ( res->GetCode() == ADVPCS_LOGIN_AUTH_ERR - ADVPCS_BASE ) {
					::wxMessageBox(wxString::Format("[%ld] %s", res->GetCode(), res->GetMessage()), 
								   ADVPCS_ERROR_TITLE, 
								   wxOK | wxCENTRE | wxICON_ERROR);
					continue;
				}
				if ( !( res->GetCode() == ADVPCS_LOGIN_PSWD_EXPIRED_ERR - ADVPCS_BASE 
				    || res->GetCode() == ADVPCS_LOGIN_OK - ADVPCS_BASE ) ) 
				{
					::wxMessageBox(wxString::Format("[%ld] %s", res->GetCode(), res->GetMessage()), 
								   ADVPCS_ERROR_TITLE, 
								   wxOK | wxCENTRE | wxICON_ERROR);
					return false;
				}
			}
#endif
			std::auto_ptr<EdiResponse> res(GetAgent().ChangePassword(m_logon.GetLogin(), m_logon.GetPassword(), m_logon.GetNewPassword()));

    		if ( res->GetCode() == ADVPCS_PSWD_OK - ADVPCS_BASE ) {
				FillStatus(res.get());
				m_connected = true;
				::wxMessageBox(ADVPCS_CHANGE_PSWD_OK);
				return true;
			}
 		    ::wxMessageBox(wxString::Format("[%ld] %s", res->GetCode(), res->GetMessage()), 
						   ADVPCS_ERROR_TITLE, 
						   wxOK | wxCENTRE | wxICON_ERROR);
			if ( res->GetCode() == ADVPCS_PSWD_INVALID_ERR - ADVPCS_BASE 
				|| res->GetCode() == ADVPCS_PSWD_DUPLICATE_ERR - ADVPCS_BASE ) 
			{
				continue;
			}
			if ( res->GetCode() == ADVPCS_LOGIN_AUTH_ERR - ADVPCS_BASE  ) {
				m_connected = false;
				continue;
			}
			return false;
		}
	} catch ( CXmlLoadException&  ) {
		wxMessageBox(ADVPCS_UNEXPECTED_REPLY_MSG, ADVPCS_ERROR_TITLE,
					 wxOK | wxCENTRE | wxICON_ERROR );
			return false;
	} catch (CAtfException& ex) {
		wxMessageBox((const char*)ex.GetText(), ADVPCS_ERROR_TITLE,
					 wxOK | wxCENTRE | wxICON_ERROR );
			return false;
	}
	wxDELETE(passwd);
	return false;
};

void AgentExecutor::FillStatus(EdiResponse* resp) {
    GetList().DeleteAllItems();
	for ( EdiStatus* status = resp->GetNext(); NULL != status; status = resp->GetNext() ) {
        GetList().AddStatus(*status);
	    delete status;
    }
};