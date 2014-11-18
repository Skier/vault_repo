#include <client/wizard.h>
#include <client/Messages.h>
#include <wx/textfile.h>

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

RadioboxPage::RadioboxPage(wxWizard* parent, MyFrame* m_frame) 
    : wxWizardPageSimple(parent), main_frame(m_frame)
{
    wxString choices[2];
    choices[0] = "Create New file              ";
    choices[1] = "Open Existing file";

    m_radio = new wxRadioBox(this, -1, "Allow to proceed:",
                             wxPoint(5, 5), wxDefaultSize,
                             WXSIZEOF(choices), choices,
                             1, wxRA_SPECIFY_COLS);
    m_radio->SetSelection(New);

        
    m_label = new wxStaticText(this, -1, "Header File Name", wxPoint(30,150));
    m_FileName = new wxTextCtrl(this, VALIDATE_TEXT, "", wxPoint(30, 170), wxSize(220, -1));
    m_FileName->Enable(false);
    m_button = new wxButton(this, -1, "Browse", wxPoint(170, 200), wxSize(80,25));
    m_button->Enable(false);
}

void RadioboxPage::OnWizardCancel(wxWizardEvent& event)
{
    if ( wxMessageBox(WIZARD_CANCEL_MESSAGE, WIZARD_CANCEL_TITLE,
                      wxICON_QUESTION | wxYES_NO, this) != wxYES )
    {
        event.Veto();
    }
}

void RadioboxPage::OnRadiobox(wxWizardEvent& event)
{
    int sel = m_radio->GetSelection();

    if ( sel == New ) {
        m_FileName->Enable(false);
        m_button->Enable(false);
        return;
    } else {
        m_FileName->Enable(true);
        m_button->Enable(true);
        return;
    }
}

void RadioboxPage::OnBrowse(wxWizardEvent& event)
{
    wxFileDialog dialog(
                this,
                _T(WIZARD_OPEN_HEADER_TITLE),
                _T(""),
                _T(""),
                _T("Records files (*.csv)|*.csv")
             );

//    dialog.SetDirectory(wxGetHomeDir());
    if (dialog.ShowModal() == wxID_OK) {
        m_FileName->Clear();
        *m_FileName << dialog.GetPath().c_str();
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
            wxString& value = m_FileName->GetValue();
            if ( value.IsEmpty() ) {
                throw wxString(WIZARD_OPEN_ERROR_BLANK_NAME);
            }
    		wxTextFile file(value);
    		if ( !file.Exists() ) {
        		throw wxString(GRID_OPEN_ERROR_NOT_EXIST + value);
    		} else {
            	GetMainFrame()->Open(value);
            }
        } catch (wxString ex) {
            wxMessageBox(ex, WIZARD_OPEN_ERROR_TITLE,
            				wxOK | wxCENTRE | wxICON_ERROR);
            return false;
        }
    } 
    GetMainFrame()->isSaved=true;
    return true;
}

//
// HeaderRecordPage
//
HeaderRecordPage::HeaderRecordPage(wxWizard* parent, Header* header) 
    : wxWizardPageSimple(parent), m_header(header)
{
    m_bitmap = wxBITMAP(wiztest2);
    m_headerPanel = new HeaderPanel(this, header);
    m_headerPanel->SetSize(wxSize(300, 260));
//    m_headerPanel->SetScrollbars(-1, 5, -1, 50);
}

bool HeaderRecordPage::TransferDataFromWindow()
{
    GetHeaderPanel()->ApplyData();
    wxString reason;
    if ( !GetHeader()->IsValid(reason) ) {
        wxMessageBox(VALIDATING_HEADER_PANEL_ERROR + reason, VALIDATING_HEADER_PANEL_ERROR_TITLE,
                     wxICON_ERROR | wxOK, this); 
        return false;
    } else {
        return true;
    }
}

FinishPage::FinishPage(wxWizard* parent) 
    : wxWizardPageSimple(parent), m_wizard(parent)
{
};

void FinishPage::OnChangedPage(wxWizardEvent& event)
{
    GetWizard()->EndModal(0);
};
