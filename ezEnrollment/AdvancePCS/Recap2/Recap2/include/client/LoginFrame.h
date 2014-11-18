#ifndef ___CLIENT_LOGON_FRAME___
#define ___CLIENT_LOGON_FRAME___

#include <wx/wxprec.h>
#include <wx/valtext.h>

class Logon
{
public:
	Logon() : m_login(""), m_pass(""){};

	Logon* GetLogon()
	{
		return this;
	};
    
    void SetLogin(wxString login)
    {
    	m_login = login;
    };

    wxString GetLogin()
    {
    	return m_login;
    };

    void SetPassword(wxString password)
    {
    	m_pass = password;
    };

    wxString GetPassword()
    {
    	return m_pass;
    };

private:

    wxString m_login;
    wxString m_pass;

};


class LoginFrame : public wxDialog
{
public:
    LoginFrame(wxWindow *parent, Logon* logon);

private:

	enum {
		Button_Ok,
		Button_Cancel,
		Username_Editor,
		Password_Editor
	};
	
	wxTextCtrl* m_loginCtrl;
	wxTextCtrl* m_passCtrl;
	
	wxButton* m_button_OK;
	wxButton* m_button_CANCEL;


	Logon* m_logon;

	Logon* GetLogon()
	{
		return m_logon;
	}

	void OnOk(wxCommandEvent& event);
	void OnCancel(wxCommandEvent& event);
	void OnEdit(wxCommandEvent& event);

    DECLARE_EVENT_TABLE()
};

#endif ___CLIENT_LOGON_FRAME___
