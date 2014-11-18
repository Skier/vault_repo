/*
 *  $RCSfile: Wizard.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef ___ADVPCS_HEADER_WIZARD_H__
#define ___ADVPCS_HEADER_WIZARD_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/wizard.h>
#include <advpcs/MainFrame.h>
#include <advpcs/Document.h>
#include <advpcs/HeaderPanel.h>
/* -------------------------- implementation place -------------------------- */


class RadioboxPage : public wxWizardPageSimple
{
public:

    RadioboxPage(wxWizard *parent, MainFrame* main_frame);

    void OnWizardCancel(wxWizardEvent& event);
    void OnRadiobox(wxWizardEvent& event);
    void OnBrowse(wxWizardEvent& event);
    void OnNext(wxWizardEvent& event);

    bool TransferDataFromWindow();

private:
    enum { New, Open, Status};
    
    wxWizard* m_wizard;
	MainFrame* m_mainFrame;
    wxRadioBox *m_radio;
    wxButton *m_button;
    wxTextCtrl *m_fileName;
    wxStaticText *m_label;

    MainFrame* GetMainFrame()
    {
        return m_mainFrame;
    };

    DECLARE_EVENT_TABLE()
};

class HeaderRecordPage : public wxWizardPageSimple
{
public:
    HeaderRecordPage(wxWizard *parent, MainFrame* mainFrame);
    
    virtual bool TransferDataFromWindow();

    void RefreshHeaderPanel();

    void OnNext(wxWizardEvent& event);
private:
    MainFrame* m_mainFrame;
    HeaderPanel* m_headerPanel;

    Document& GetDocument()
    {
        return *(m_mainFrame->GetDocument());
    };    

    HeaderPanel* GetHeaderPanel()
    {
        return m_headerPanel;
    };    
};


class FinishPage : public wxWizardPageSimple
{
public:

    FinishPage(wxWizard *parent);

    void OnChangedPage(wxWizardEvent& event);
private:
    wxWizard* m_wizard;

    wxWizard* GetWizard()
    {
        return m_wizard;
    };

    DECLARE_EVENT_TABLE()
};


#endif /* __ADVPCS_HEADER_WIZARD_H__ */

