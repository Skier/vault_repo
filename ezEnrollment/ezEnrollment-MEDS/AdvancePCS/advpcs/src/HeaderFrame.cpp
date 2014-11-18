/*
 *  $RCSfile: HeaderFrame.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <wx/cmdproc.h>
#include <advpcs/HeaderFrame.h>
#include <advpcs/Resources.h>
#include <advpcs/MainFrame.h>
/* -------------------------- implementation place -------------------------- */

BEGIN_EVENT_TABLE(HeaderFrame, wxDialog)
    EVT_BUTTON(Button_Ok, HeaderFrame::OnOk)
    EVT_BUTTON(Button_Cancel, HeaderFrame::OnCancel)
END_EVENT_TABLE()

HeaderFrame::HeaderFrame(MainFrame* mainFrame) 
    : wxDialog(mainFrame, -1, "Edit Header Record", wxDefaultPosition, wxSize(320, 450), wxDIALOG_MODAL | wxDEFAULT_DIALOG_STYLE )
{
    m_headerPanel = new HeaderPanel(this, mainFrame);
        m_headerPanel->RefreshData();
    m_headerPanel->SetSize(wxSize(300, 260));
    
    wxButton* m_button_OK = new wxButton(this, Button_Ok, "Ok", wxPoint( -1, -1), wxSize( -1, -1 ));
    wxButton* m_button_CANCEL = new wxButton(this, Button_Cancel, "Cancel", wxPoint( -1, -1), wxSize( -1, -1 ));
    
    wxBoxSizer *topSizer = new wxBoxSizer( wxVERTICAL );
    wxBoxSizer *buttonSizer = new wxBoxSizer( wxHORIZONTAL );
    
    buttonSizer->Add( m_button_OK, 0, wxALIGN_CENTRE_HORIZONTAL, 5 );
    buttonSizer->Add( 15, 15);
    buttonSizer->Add( m_button_CANCEL, 0, wxALIGN_CENTRE_HORIZONTAL, 5 );
    topSizer->Add( m_headerPanel, 0, wxEXPAND | wxALL, 5 );
    topSizer->Add( buttonSizer, 0, wxALIGN_CENTRE_HORIZONTAL, 5 );

//    SetAutoLayout( TRUE );
    SetSizer( topSizer );

    topSizer->Fit( this );
    topSizer->SetSizeHints( this );

}

void HeaderFrame::OnOk(wxCommandEvent& event)
{
    GetHeaderPanel()->ApplyData();
    wxString reason;

    if ( !GetHeaderPanel()->GetDocument().IsHeaderValid(true) ) {
        wxMessageBox(ADVPCS_VALIDATING_HEADER_PANEL_ERROR, ADVPCS_VALIDATING_HEADER_PANEL_ERROR_TITLE,
                     wxICON_EXCLAMATION | wxCENTRE | wxOK, this); 
        return;
    } else {
        Close();
    }
}

void HeaderFrame::OnCancel(wxCommandEvent& event)
{
    Close();
}
