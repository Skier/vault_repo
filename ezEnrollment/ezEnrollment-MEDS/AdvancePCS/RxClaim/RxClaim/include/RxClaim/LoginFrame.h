#ifndef __RXCLAIM_LOGON_FRAME_H__
#define __RXCLAIM_LOGON_FRAME_H__

#include <wx/wxprec.h>
#include <wx/valtext.h>

class Logon
{
public:
    Logon() : m_login(""), m_pass(""){};

    Logon* GetLogon() { return this; };
    
    void SetLogin(const wxString& login) { m_login = login; };
    wxString GetLogin() const { return m_login; };

    void SetPassword(const wxString& password) { m_pass = password; };
    wxString GetPassword() const { return m_pass; };

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

    Logon* GetLogon() { return m_logon; }

    void OnOk(wxCommandEvent& event);
    void OnCancel(wxCommandEvent& event);
    void OnEdit(wxCommandEvent& event);

    DECLARE_EVENT_TABLE()
};

#endif ___CLIENT_LOGON_FRAME___
