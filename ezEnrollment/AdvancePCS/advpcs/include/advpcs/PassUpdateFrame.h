/*
 *  $RCSfile: PassUpdateFrame.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __RXCLAIM_PASS_UPDATE_FRAME_H__
#define __RXCLAIM_PASS_UPDATE_FRAME_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/valtext.h>
#include <advpcs/LoginFrame.h>
/* -------------------------- implementation place -------------------------- */

class PassUpdateFrame : public wxDialog
{
public:
    PassUpdateFrame(wxWindow *parent, Logon& logon);

private:

    enum {
        Button_Ok,
        Button_Cancel,
        Username_Editor,
        Password_Editor,
        NewPassword_Editor,
        Confirm_Editor
    };
    
    wxTextCtrl* m_loginCtrl;
    wxTextCtrl* m_passCtrl;
    wxTextCtrl* m_newPassCtrl;
    wxTextCtrl* m_confirmCtrl;
    
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

#endif ___CLIENT_PASS_UPDATE_FRAME___
