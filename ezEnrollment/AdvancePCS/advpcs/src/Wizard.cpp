/*
 *  $RCSfile: Wizard.cpp,v $
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
#include <wx/listctrl.h>
#include <wx/textfile.h>
#include <advpcs/Wizard.h>
#include <advpcs/Resources.h>
/* -------------------------- implementation place -------------------------- */

//
// RadioboxPage
//
BEGIN_EVENT_TABLE(RadioboxPage, wxWizardPageSimple)
    EVT_RADIOBOX(-1, RadioboxPage::OnRadiobox)
    EVT_BUTTON(-1, RadioboxPage::OnBrowse)
    EVT_WIZARD_CANCEL(-1, RadioboxPage::OnWizardCancel)
    EVT_WIZARD_PAGE_CHANGING(-1, RadioboxPage::OnNext)
END_EVENT_TABLE()

BEGIN_EVENT_TABLE(FinishPage, wxWizardPageSimple)
    EVT_WIZARD_PAGE_CHANGED(-1, FinishPage::OnChangedPage)
END_EVENT_TABLE()

RadioboxPage::RadioboxPage(wxWizard* parent, MainFrame* m_frame) 
    : wxWizardPageSimple(parent), m_mainFrame(m_frame), m_wizard(parent)
{
    wxString choices[3];
    choices[0] = "Create New file              ";
    choices[1] = "Open Existing file";
    choices[2] = "View status";

    m_radio = new wxRadioBox(this, -1, "Allow to proceed:",
                             wxPoint(5, 5), wxDefaultSize,
                             WXSIZEOF(choices), choices,
                             1, wxRA_SPECIFY_COLS);
    m_radio->SetSelection(New);

        
    m_label = new wxStaticText(this, -1, "Header File Name", wxPoint(30,150));
    m_fileName = new wxTextCtrl(this, VALIDATE_TEXT, "", wxPoint(30, 170), wxSize(220, -1));
    m_fileName->Enable(false);
    m_button = new wxButton(this, -1, "Browse", wxPoint(170, 200), wxSize(80,25));
    m_button->Enable(false);
}

void RadioboxPage::OnWizardCancel(wxWizardEvent& event)
{
    if ( wxMessageBox(ADVPCS_WIZARD_CANCEL_MESSAGE, ADVPCS_WIZARD_CANCEL_TITLE,
                      wxICON_QUESTION | wxYES_NO, this) != wxYES )
    {
        event.Veto();
    }
}

void RadioboxPage::OnRadiobox(wxWizardEvent& event)
{
    int sel = m_radio->GetSelection();

    if ( sel == New || sel == Status ) {
        m_fileName->Enable(false);
        m_button->Enable(false);
        return;
	} else {
        m_fileName->Enable(true);
        m_button->Enable(true);
        return;
    }
}

void RadioboxPage::OnBrowse(wxWizardEvent& event)
{
    wxFileDialog dialog(
                this,
                _T(ADVPCS_WIZARD_OPEN_HEADER_TITLE),
                _T(""),
                _T(""),
                _T("Records files (*.csv)|*.csv")
             );

//    dialog.SetDirectory(wxGetHomeDir());
    if (dialog.ShowModal() == wxID_OK) {
        m_fileName->Clear();
        *m_fileName << dialog.GetPath().c_str();
    }
}

void RadioboxPage::OnNext(wxWizardEvent& event)
{
    HeaderRecordPage *hrp = (HeaderRecordPage*)GetNext();
    hrp->RefreshHeaderPanel(); 
};

bool RadioboxPage::TransferDataFromWindow()
{
    int sel = m_radio->GetSelection();

    if ( sel == Open ) {
        try {
            wxString& value = m_fileName->GetValue();
            if ( value.IsEmpty() ) {
                // FIXME: 
                throw wxString(ADVPCS_WIZARD_OPEN_ERROR_BLANK_NAME);
            }
            wxTextFile file(value);
            if ( !file.Exists() ) {
                // FIXME: 
                throw wxString(ADVPCS_GRID_OPEN_ERROR_NOT_EXIST + value);
            } else {
                GetMainFrame()->Open(value);
            }
        } catch (wxString ex) {
            wxMessageBox(ex, ADVPCS_WIZARD_OPEN_ERROR_TITLE,
                            wxOK | wxCENTRE | wxICON_ERROR);
            return false;
        }
	} else if ( sel == Status ) {
//		wxCommandEvent& event = wxCommandEvent();
//		GetMainFrame()->OnUpdateStatus(event);
		int result = 1;
		GetMainFrame()->SetWizardResult(result);
		m_wizard->EndModal(0);
	}
    return true;
}

//
// HeaderRecordPage
//
HeaderRecordPage::HeaderRecordPage(wxWizard* parent, MainFrame* mainFrame) 
    : wxWizardPageSimple(parent), m_mainFrame(mainFrame)
{
    wxASSERT(NULL != m_mainFrame);

    m_bitmap = wxBITMAP(wiztest2);
    m_headerPanel = new HeaderPanel(this, m_mainFrame);
    m_headerPanel->SetSize(wxSize(300, 260));
//    m_headerPanel->SetScrollbars(-1, 5, -1, 50);
}

bool HeaderRecordPage::TransferDataFromWindow()
{
    GetHeaderPanel()->ApplyData();

    wxString reason;

    if ( !GetDocument().IsHeaderValid(true) ) {
        wxMessageBox(ADVPCS_VALIDATING_HEADER_PANEL_ERROR, ADVPCS_VALIDATING_HEADER_PANEL_ERROR_TITLE,
                     wxICON_ERROR | wxOK, this); 
        return false;
    } else {
        return true;
    }

    return true;
}

void HeaderRecordPage::RefreshHeaderPanel() {
        wxASSERT( NULL != m_headerPanel );

        wxDELETE(m_headerPanel);

    m_headerPanel = new HeaderPanel(this, m_mainFrame);
    m_headerPanel->SetSize(wxSize(300, 260));

        m_headerPanel->Show();

    GetHeaderPanel()->RefreshData();
};


FinishPage::FinishPage(wxWizard* parent) 
    : wxWizardPageSimple(parent), m_wizard(parent)
{
};

void FinishPage::OnChangedPage(wxWizardEvent& event)
{
    GetWizard()->EndModal(0);
};
