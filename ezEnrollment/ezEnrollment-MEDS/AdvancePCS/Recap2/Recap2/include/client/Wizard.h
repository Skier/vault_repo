#ifndef ___CLIENT_HEADER_WIZARD___
#define ___CLIENT_HEADER_WIZARD___

#include <wx/wx.h>
#include <wx/wizard.h>
#include <client/MainFrame.h>

// ----------------------------------------------------------------------------
// constants
// ----------------------------------------------------------------------------

//#define VALIDATE_TEXT 101

// ----------------------------------------------------------------------------
// pages for our wizard
// ----------------------------------------------------------------------------

class RadioboxPage : public wxWizardPageSimple
{
public:

    RadioboxPage(wxWizard *parent, MyFrame* main_frame);

    void OnWizardCancel(wxWizardEvent& event);
    void OnRadiobox(wxWizardEvent& event);
    void OnBrowse(wxWizardEvent& event);
    void OnNext(wxWizardEvent& event);

    bool TransferDataFromWindow();

private:
    enum { New, Open };
    
    MyFrame* main_frame;
    wxRadioBox *m_radio;
    wxButton *m_button;
    wxTextCtrl *m_FileName;
    wxStaticText *m_label;

    MyFrame* GetMainFrame()
    {
        return main_frame;
    };

    DECLARE_EVENT_TABLE()
};

class HeaderRecordPage : public wxWizardPageSimple
{
public:
    HeaderRecordPage(wxWizard *parent, Header* header);
    
    virtual bool TransferDataFromWindow();

    void RefreshHeaderPanel()
    {
        GetHeaderPanel()->RefreshData();
    };

private:
    Header* m_header;
    HeaderPanel* m_headerPanel;

    Header* GetHeader()
    {
        return m_header;
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


#endif // ___CLIENT_HEADER_WIZARD___
