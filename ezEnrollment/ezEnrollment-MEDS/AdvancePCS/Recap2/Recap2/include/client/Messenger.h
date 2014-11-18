#ifndef ___MESSENGER___
#define ___MESSENGER___

#include <wx/wx.h>
#include <wx/sizer.h>
#include <wx/datetime.h>
#include <wx/listbox.h>
#include <wx/listctrl.h>

#include <atf/CfgXml.h>
#include <client/composer.h>
#include <client/LoginFrame.h>

class Messenger;

Messenger* GetMessenger();

enum { CLOSE };

class Messenger : public wxFrame {
public:
    Messenger(wxWindow* parent, CCfgXml* mailCfg);

    void LogMessage(int level, wxString& message);

    void SetEdiPath(wxString& path);

    void SetComposer(Composer* composer);

    bool ProcessMessage(bool verifyOnly, bool noMail);

    bool PostData(const wxString& fileName);
private:
    wxString   m_buffer;
    wxString   m_ediPath;
//    wxListBox* m_log;
    wxListCtrl* m_logList;
    wxButton*  m_close;
    CCfgXml*   m_mailConfig;
    Composer*  m_composer;

    Logon      m_logon;

    void OnClose();

    CCfgXml* GetMailConfig()
    {
        return m_mailConfig;
    };

    Composer* GetComposer()
    {
        return m_composer;
    };

    wxString& GetEdiPath() 
    {
        return m_ediPath;
    };

    wxString GetEdiFileName() 
    {
        return GetEdiPath() + "\\" + ComposeFileName();
    };

    wxString ComposeFileName();

    bool TerminateProcess(bool result)
    {
        m_close->Enable(true);
        return result;
    };

    bool canTerminate()
    {
        return m_close->IsEnabled();
    };

    DECLARE_EVENT_TABLE()
};

#endif // ___MESSENGER___