#ifndef __RXCLAIM_MESSENGER_H__
#define __RXCLAIM_MESSENGER_H__

#include <wx/wx.h>
#include <wx/sizer.h>
#include <wx/datetime.h>
#include <wx/listbox.h>
#include <wx/listctrl.h>

#include <atf/CfgXml.h>
#include <rxclaim/Composer.h>
#include <rxclaim/LoginFrame.h>

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

    void OnClose();

    CCfgXml* GetMailConfig() { return m_mailConfig; };
    Composer* GetComposer() { return m_composer; };
    wxString& GetEdiPath() { return m_ediPath; };

    wxString GetEdiFileName() { 
        return GetEdiPath() + "\\" + ComposeFileName(); 
    };

    wxString ComposeFileName();

    bool TerminateProcess(bool result) { 
        m_close->Enable(true); 
        return result; 
    };

    bool canTerminate() { return m_close->IsEnabled(); };

private:
    wxString   m_buffer;
    wxString   m_ediPath;
    wxListCtrl* m_logList;
    wxButton*  m_close;
    CCfgXml*   m_mailConfig;
    Composer*  m_composer;

    Logon      m_logon;

    DECLARE_EVENT_TABLE()
};

Messenger* GetMessenger();

#endif /* __RXCLAIM_MESSENGER_H__ */
