/*
 *  $RCSfile: StatusBar.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/StatusBar.h>
/* -------------------------- implementation place -------------------------- */
#define FIELDS 2 


static const int widths[FIELDS] = { 200, -1 };

StatusBar::StatusBar(wxWindow* parent, wxWindowID id, long style, const wxString& name)
    : wxStatusBar(parent, id, style, name), m_gauge(NULL), m_dialog(NULL), m_text(NULL)
{

    SetFieldsCount(FIELDS);
    SetStatusWidths(FIELDS, widths);

    Connect(-1, wxEVT_SIZE, (wxObjectEventFunction) (wxSizeEventFunction) OnSize);
    m_gauge = new wxGauge(this, -1, 100);
    m_gauge->Show(false);

    m_dialog = new wxDialog(parent, -1, "Please wait", wxDefaultPosition, wxSize(455,55), wxCAPTION);
    m_dialog->CentreOnParent();
    m_text = new wxStaticText(m_dialog, -1, wxEmptyString, wxPoint(15,5), wxSize(425, 20), 
        wxALIGN_CENTRE);
    m_text->CentreOnParent();
    m_text->Show(TRUE);
};

void StatusBar::OnSize(wxSizeEvent& event) {
    wxRect rect;
    GetFieldRect(0, rect);

    m_gauge->SetSize(rect.x + 2, rect.y + 2, rect.width - 2, rect.height - 2);

    event.Skip();
};

        // ProcessIndicator implementation
void StatusBar::StartProcess(long maxState, const wxString& processName) {
    wxASSERT(NULL != m_gauge);

    ::wxSetCursor(*wxHOURGLASS_CURSOR);
    m_gauge->SetRange(maxState);
    m_gauge->SetValue(0);
    m_gauge->Show(true);

    if ( wxEmptyString != processName ) {
		m_dialog->CentreOnParent();
        m_dialog->Show(true);
        m_text->SetLabel(processName);
    }
    ::wxYield();
};

void StatusBar::SetState(long state, const wxString& description) {
    wxASSERT(NULL != m_gauge);
    wxASSERT(m_gauge->IsShown());
    wxASSERT(state <= m_gauge->GetRange());

    m_gauge->SetValue(state);
    SetStatusText(description, 1);

//    m_text->SetLabel(description);
    ::wxYield();
};

void StatusBar::FinishProcess(const wxString& description) {
    wxASSERT(NULL != m_gauge);
    wxASSERT(m_gauge->IsShown());

    ::wxSetCursor(*wxSTANDARD_CURSOR);
    m_gauge->Hide();
    SetStatusText(description, 1);

    m_dialog->Show(false);
};
