/*
 *  $RCSfile: LoginFrame.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_LOGON_FRAME_H__
#define __ADVPCS_LOGON_FRAME_H__

/* -------------------------- header place ---------------------------------- */
class wxString;
class wxDialog;
/* -------------------------- implementation place -------------------------- */

class Logon
{
public:
    Logon() : m_login(wxEmptyString), m_pass(wxEmptyString), m_npass(wxEmptyString){};

    void SetLogin(const wxString& login) { m_login = login; };
    wxString GetLogin() const { return m_login; };

    void SetPassword(const wxString& password) { m_pass = password; };
    wxString GetPassword() const { return m_pass; };

    void SetNewPassword(const wxString& password) { m_npass = password; };
    wxString GetNewPassword() const { return m_npass; };
private:
    wxString m_login;
    wxString m_pass;
	wxString m_npass;
};


class LoginFrame : public wxDialog
{
public:
    LoginFrame(wxWindow *parent, Logon& logon);

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


    Logon& m_logon;

    Logon& GetLogon() { return m_logon; }

	bool IsDataCorrect();
    void OnOk(wxCommandEvent& event);
    void OnCancel(wxCommandEvent& event);
    void OnEdit(wxCommandEvent& event);

    DECLARE_EVENT_TABLE()
};

#endif /* __ADVPCS_LOGON_FRAME_H__ */
