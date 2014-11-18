/*
 *  $RCSfile: PassUpdateFrame.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

// For compilers that support precompilation, includes "wx.h".
#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/PassUpdateFrame.h>
/* -------------------------- implementation place -------------------------- */
//
// PasswordUpdateFrame
//

BEGIN_EVENT_TABLE(PassUpdateFrame, wxDialog)
    EVT_BUTTON(Button_Ok, PassUpdateFrame::OnOk)
    EVT_BUTTON(Button_Cancel, PassUpdateFrame::OnCancel)
    EVT_TEXT(Username_Editor, PassUpdateFrame::OnEdit) 
    EVT_TEXT(Password_Editor, PassUpdateFrame::OnEdit) 
    EVT_TEXT(NewPassword_Editor, PassUpdateFrame::OnEdit) 
    EVT_TEXT(Confirm_Editor, PassUpdateFrame::OnEdit) 
END_EVENT_TABLE()

PassUpdateFrame::PassUpdateFrame(wxWindow* parent, Logon& logon) 
    : wxDialog(parent, -1, "New Password", wxDefaultPosition, wxSize(260, 260), wxDIALOG_MODAL | wxDEFAULT_DIALOG_STYLE ), 
	m_logon(logon), 
    m_loginCtrl(NULL), m_passCtrl(NULL), m_newPassCtrl(NULL),  m_confirmCtrl(NULL), 
	m_button_OK(NULL)
 	  
{

    wxString loginText = wxString("Login:");
    wxString passText = wxString("Password:");
    wxString newPassText = wxString("New password:");
    wxString confirmText = wxString("Confirm new password:");

    int textOffset = 10;
    int ctrlOffset = 120;
    int rowOffset = 30;
    int topOffset = 15;
    int ctrlWidth = 120;

    wxPanel* panel = new wxPanel(this, -1, wxDefaultPosition, wxSize(260, 200) );

    wxPanel* p = new wxPanel(this, -1, wxDefaultPosition, wxSize(260, 40) );

    m_button_OK = new wxButton(p, Button_Ok, "Update", wxPoint( 50, 5));
//    m_button_OK->Enable(false);
    m_button_OK->SetDefault();
    SetDefaultItem(m_button_OK);

    m_button_CANCEL = new wxButton(p, Button_Cancel, "Cancel", wxPoint( 135, 5));

    wxStaticText* text1 = new wxStaticText( panel, -1, loginText, 
                                            wxPoint(textOffset, topOffset) );
    wxStaticText* text2 = new wxStaticText( panel, -1, passText, 
                                            wxPoint(textOffset, topOffset+rowOffset) );
    wxStaticText* text3 = new wxStaticText( panel, -1, newPassText, 
                                            wxPoint(textOffset, topOffset+(rowOffset*2)) );
    wxStaticText* text4 = new wxStaticText( panel, -1, confirmText, 
                                            wxPoint(textOffset, topOffset+(rowOffset*3)) );
    m_loginCtrl = new wxTextCtrl( panel, Username_Editor, GetLogon().GetLogin(),
                                            wxPoint(ctrlOffset, topOffset), 
                                            wxSize(ctrlWidth, -1) );
    m_passCtrl = new wxTextCtrl( panel, Password_Editor, "",
                                            wxPoint(ctrlOffset, topOffset+rowOffset), 
                                            wxSize(ctrlWidth, -1), wxTE_PASSWORD );
    m_newPassCtrl = new wxTextCtrl( panel, NewPassword_Editor, "",
                                            wxPoint(ctrlOffset, topOffset+(rowOffset*2)), 
                                            wxSize(ctrlWidth, -1), wxTE_PASSWORD );
    m_confirmCtrl = new wxTextCtrl( panel, Confirm_Editor, "",
                                            wxPoint(ctrlOffset, topOffset+(rowOffset*3)), 
                                            wxSize(ctrlWidth, -1), wxTE_PASSWORD );


    wxBoxSizer *topSizer = new wxBoxSizer( wxVERTICAL );
    
    
    topSizer->Add( panel, 0, wxALIGN_TOP , 5 );
    topSizer->Add( p, 1, wxEXPAND, 5 );

    SetSizer( topSizer );

    topSizer->Fit( this );
    topSizer->SetSizeHints( this );
    topSizer->Layout();
    topSizer->RecalcSizes();

    m_loginCtrl->SetValue(GetLogon().GetLogin());
    m_passCtrl->SetValue(wxEmptyString);
}

bool PassUpdateFrame::IsDataCorrect() {
	if ( m_passCtrl->GetValue().IsEmpty() ) {
		wxMessageBox("Please provide your current password.");
		m_passCtrl->SetFocus();
		return false;
	} else if ( m_passCtrl->GetValue().Length()<6 
				|| m_passCtrl->GetValue().Length()>8) {
		wxMessageBox("The current password must be between 6 and 8 characters long");
		m_passCtrl->SetFocus();
		return false;
	} else {
		wxString s = wxString(m_passCtrl->GetValue());
		for ( size_t i=0; i<s.Length(); i++ ) {
			if ( (s.GetChar(i)>='a' && s.GetChar(i)<='z')
				|| (s.GetChar(i)>='A' && s.GetChar(i)<='Z') 
				|| (s.GetChar(i)>='0' && s.GetChar(i)<='9') ) {
			} else {
				wxMessageBox("Current password can not contain special character.");
				m_passCtrl->SetFocus();
				return false;
			}
		}
	}
	if ( m_newPassCtrl->GetValue().IsEmpty() ) {
		wxMessageBox("Please provide a new password.");
		m_passCtrl->SetFocus();
		return false;
	} else if ( m_newPassCtrl->GetValue().Length()<6 
				|| m_newPassCtrl->GetValue().Length()>8) {
		wxMessageBox("The new password must be between 6 and 8 characters long");
		m_passCtrl->SetFocus();
		return false;
	} else {
		wxString s = wxString(m_newPassCtrl->GetValue());
		for ( size_t i=0; i<s.Length(); i++ ) {
			if ( (s.GetChar(i)>='a' && s.GetChar(i)<='z')
				|| (s.GetChar(i)>='A' && s.GetChar(i)<='Z') 
				|| (s.GetChar(i)>='0' && s.GetChar(i)<='9') ) {
			} else {
				wxMessageBox("New password can not contain special character.");
				m_passCtrl->SetFocus();
				return false;
			}
		}
	}
	if ( m_confirmCtrl->GetValue().Cmp(m_newPassCtrl->GetValue()) != 0 ) {
		wxMessageBox("New password does not match the confirm password");
		m_passCtrl->SetFocus();
		return false;
	}
	if ( m_passCtrl->GetValue().Cmp(m_newPassCtrl->GetValue()) == 0 ) {
		wxMessageBox("Your new password selection must be different than your current password");
		m_passCtrl->SetFocus();
		return false;
	}
	return true;

}

void PassUpdateFrame::OnOk(wxCommandEvent& event)
{
    if ( IsDataCorrect() ) {
		GetLogon().SetLogin(m_loginCtrl->GetValue());
		GetLogon().SetNewPassword(m_newPassCtrl->GetValue());
		GetLogon().SetPassword(m_passCtrl->GetValue());
		m_passCtrl->SetValue(wxEmptyString);
		m_newPassCtrl->SetValue(wxEmptyString);
		m_confirmCtrl->SetValue(wxEmptyString);
		EndModal(wxID_OK);
	}
   	m_passCtrl->SetValue(wxEmptyString);
   	m_newPassCtrl->SetValue(wxEmptyString);
   	m_confirmCtrl->SetValue(wxEmptyString);
}

void PassUpdateFrame::OnCancel(wxCommandEvent& event)
{
    m_passCtrl->SetValue(wxEmptyString);
    m_newPassCtrl->SetValue(wxEmptyString);
    m_confirmCtrl->SetValue(wxEmptyString);
    EndModal(wxID_CANCEL);
}

void PassUpdateFrame::OnEdit(wxCommandEvent& event) 
{
#if 0
	if ( m_passCtrl && m_newPassCtrl && m_confirmCtrl ) {
		m_button_OK->Enable( m_loginCtrl->GetValue() != "" 
											 && m_passCtrl->GetValue() != "" 
											 && m_newPassCtrl->GetValue() != "" 
											 && m_confirmCtrl->GetValue() != "" 
											 && m_newPassCtrl->GetValue() == m_confirmCtrl->GetValue() );

		m_button_OK->Refresh();
	}
#endif
};
