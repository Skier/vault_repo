/*
 *  $RCSfile: LoginFrame.cpp,v $
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
#include <advpcs/LoginFrame.h>
/* -------------------------- implementation place -------------------------- */
BEGIN_EVENT_TABLE(LoginFrame, wxDialog)
    EVT_BUTTON(Button_Ok, LoginFrame::OnOk)
    EVT_BUTTON(Button_Cancel, LoginFrame::OnCancel)
    EVT_TEXT(Username_Editor, LoginFrame::OnEdit) 
    EVT_TEXT(Password_Editor, LoginFrame::OnEdit) 
END_EVENT_TABLE()

LoginFrame::LoginFrame(wxWindow* parent, Logon& logon) 
    : wxDialog(parent, -1, "Login", wxDefaultPosition, wxSize(250, 160), wxDIALOG_MODAL | wxDEFAULT_DIALOG_STYLE ), 
	  m_logon(logon), m_loginCtrl(NULL), m_passCtrl(NULL), m_button_OK(NULL)
{

    wxString loginText = wxString("Login:");
    wxString passText = wxString("Password:");

    int textOffset = 10;
    int ctrlOffset = 80;
    int rowOffset = 30;
    int topOffset = 15;
    int ctrlWidth = 150;

    wxPanel* panel = new wxPanel(this, -1, wxDefaultPosition, wxSize(250, 80) );


    wxPanel* p = new wxPanel(this, -1, wxDefaultPosition, wxSize(250, 40) );

    m_button_OK = new wxButton(p, Button_Ok, "Ok", wxPoint( 50, 5));
    m_button_OK->Enable(false);
    m_button_OK->SetDefault();
    SetDefaultItem(m_button_OK);

    m_button_CANCEL = new wxButton(p, Button_Cancel, "Cancel", wxPoint( 130, 5));

    wxStaticText* text1 = new wxStaticText( panel, -1, loginText, 
                                            wxPoint(textOffset, topOffset) );
    wxStaticText* text2 = new wxStaticText( panel, -1, passText, 
                                            wxPoint(textOffset, topOffset+rowOffset) );
    m_loginCtrl = new wxTextCtrl( panel, Username_Editor, wxEmptyString,
                                            wxPoint(ctrlOffset, topOffset), 
                                            wxSize(ctrlWidth, -1) );
    m_passCtrl = new wxTextCtrl( panel, Password_Editor, wxEmptyString ,
                                            wxPoint(ctrlOffset, topOffset+rowOffset), 
                                            wxSize(ctrlWidth, -1), wxTE_PASSWORD );
    m_loginCtrl->SetValue(GetLogon().GetLogin());
    m_passCtrl->SetValue(wxEmptyString);
    
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

bool LoginFrame::IsDataCorrect() {
	if ( m_loginCtrl->GetValue().IsEmpty() ) {
		wxMessageBox("Please provide your user ID.");
		m_loginCtrl->SetFocus();
		return false;
	}
	if ( m_passCtrl->GetValue().IsEmpty() ) {
		wxMessageBox("Please provide your password.");
		m_passCtrl->SetFocus();
		return false;
	} else if ( m_passCtrl->GetValue().Length()<6 
				|| m_passCtrl->GetValue().Length()>8) {
		wxMessageBox("The password must be between 6 and 8 characters long");
		m_passCtrl->SetFocus();
		return false;
	} else {
		wxString s = wxString(m_passCtrl->GetValue());
		for ( size_t i=0; i<s.Length(); i++ ) {
			if ( (s.GetChar(i)>='a' && s.GetChar(i)<='z')
				|| (s.GetChar(i)>='A' && s.GetChar(i)<='Z') 
				|| (s.GetChar(i)>='0' && s.GetChar(i)<='9') ) {
			} else {
				wxMessageBox("Password can not contain special character.");
				m_passCtrl->SetFocus();
				return false;
			}
		}
	}
	return true;
}

void LoginFrame::OnOk(wxCommandEvent& event)
{
    if ( IsDataCorrect() ) {
		GetLogon().SetLogin(m_loginCtrl->GetValue());
		GetLogon().SetPassword(m_passCtrl->GetValue());
		m_passCtrl->SetValue(wxEmptyString);
		EndModal(wxID_OK);
	}
	m_passCtrl->SetValue(wxEmptyString);
}

void LoginFrame::OnCancel(wxCommandEvent& event)
{
    m_passCtrl->SetValue(wxEmptyString);
    EndModal(wxID_CANCEL);
}

void LoginFrame::OnEdit(wxCommandEvent& event) 
{
	if ( m_loginCtrl && m_passCtrl ) {
		m_button_OK->Enable( m_loginCtrl->GetValue() != "" && m_passCtrl->GetValue() != "" );
		m_button_OK->Refresh();
	}
};
