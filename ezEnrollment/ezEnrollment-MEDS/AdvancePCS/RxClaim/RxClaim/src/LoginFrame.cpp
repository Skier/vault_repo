#include <rxclaim/LoginFrame.h>
//
// HeaderRecordFrame
//

BEGIN_EVENT_TABLE(LoginFrame, wxDialog)
    EVT_BUTTON(Button_Ok, LoginFrame::OnOk)
    EVT_BUTTON(Button_Cancel, LoginFrame::OnCancel)
    EVT_TEXT(Username_Editor, LoginFrame::OnEdit) 
    EVT_TEXT(Password_Editor, LoginFrame::OnEdit) 
END_EVENT_TABLE()

LoginFrame::LoginFrame(wxWindow* parent, Logon* logon) 
    : wxDialog(parent, -1, "Login", wxDefaultPosition, wxSize(250, 160), wxDIALOG_MODAL | wxDEFAULT_DIALOG_STYLE ), m_logon(logon)
{

    wxString loginText = wxString("Login:");
    wxString passText = wxString("Password:");

    int textOffset = 10;
    int ctrlOffset = 80;
    int rowOffset = 30;
    int topOffset = 15;
    int ctrlWidth = 150;

    wxPanel* panel = new wxPanel(this, -1, wxDefaultPosition, wxSize(250, 80) );

    wxStaticText* text1 = new wxStaticText( panel, -1, loginText, 
                                            wxPoint(textOffset, topOffset) );
    wxStaticText* text2 = new wxStaticText( panel, -1, passText, 
                                            wxPoint(textOffset, topOffset+rowOffset) );
    m_loginCtrl = new wxTextCtrl( panel, Username_Editor, GetLogon()->GetLogin(),
                                            wxPoint(ctrlOffset, topOffset), 
                                            wxSize(ctrlWidth, -1) );
    m_passCtrl = new wxTextCtrl( panel, Password_Editor, GetLogon()->GetPassword(),
                                            wxPoint(ctrlOffset, topOffset+rowOffset), 
                                            wxSize(ctrlWidth, -1), wxTE_PASSWORD );

    wxPanel* p = new wxPanel(this, -1, wxDefaultPosition, wxSize(250, 40) );

    m_button_OK = new wxButton(p, Button_Ok, "Ok", wxPoint( 50, 5));
    m_button_OK->Enable(false);
    m_button_OK->SetDefault();
    SetDefaultItem(m_button_OK);

    m_button_CANCEL = new wxButton(p, Button_Cancel, "Cancel", wxPoint( 130, 5));
    
    wxBoxSizer *topSizer = new wxBoxSizer( wxVERTICAL );
    
    
    topSizer->Add( panel, 0, wxALIGN_TOP , 5 );
    topSizer->Add( p, 1, wxEXPAND, 5 );

    SetSizer( topSizer );

    topSizer->Fit( this );
    topSizer->SetSizeHints( this );
    topSizer->Layout();
    topSizer->RecalcSizes();

    m_loginCtrl->SetValue(GetLogon()->GetLogin());
    m_passCtrl->SetValue(GetLogon()->GetPassword());
}

void LoginFrame::OnOk(wxCommandEvent& event)
{
    GetLogon()->SetLogin(m_loginCtrl->GetValue());
    GetLogon()->SetPassword(m_passCtrl->GetValue());
    EndModal(1);
}

void LoginFrame::OnCancel(wxCommandEvent& event)
{
    EndModal(0);
}

void LoginFrame::OnEdit(wxCommandEvent& event) 
{
    m_button_OK->Enable( m_loginCtrl->GetValue() != "" && m_passCtrl->GetValue() != "" );
    m_button_OK->Refresh();
};
