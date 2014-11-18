/*
 *  $RCSfile: StatusBar.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_STATUS_BAR_H__
#define __ADVPCS_STATUS_BAR_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/statusbr.h>
#include <wx/gauge.h>
#include <advpcs/ProcessIndicator.h>
/* -------------------------- implementation place -------------------------- */
class StatusBar : public wxStatusBar, public ProcessIndicator {
public: 
    StatusBar(wxWindow* parent, wxWindowID id, 
              long style = wxST_SIZEGRIP, const wxString& name = "statusBar");

    void OnSize(wxSizeEvent& event);

        // ProcessIndicator implementation
    virtual void StartProcess(long maxState = 100, const wxString& processName = wxEmptyString);
    virtual void SetState(long state, const wxString& description = wxEmptyString);
    virtual void FinishProcess(const wxString& description = wxEmptyString);

private:
    wxGauge*      m_gauge;
    wxDialog*     m_dialog;
    wxStaticText* m_text;
};

#endif /* __ADVPCS_STATUS_BAR_H__ */

