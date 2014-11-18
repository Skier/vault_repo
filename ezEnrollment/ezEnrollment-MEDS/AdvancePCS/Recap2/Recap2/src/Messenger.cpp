#include <client/Messenger.h>
#include <client/Messages.h>
#include <client/NetAutoHdr.h>
#include <wx/wfstream.h>
#include <wx/filename.h>
#include <wx/log.h>
#include <memory>


BEGIN_EVENT_TABLE(Messenger, wxFrame)
    EVT_CLOSE(Messenger::OnClose)
    EVT_BUTTON(CLOSE, Messenger::OnClose)
END_EVENT_TABLE()

wxString GetInetMessage(DWORD error);

Messenger::Messenger(wxWindow* parent, CCfgXml* mailCfg) 
    : wxFrame(parent, -1, wxString(FRAME_LOG_TITLE), 
        wxDefaultPosition, wxDefaultSize, wxDEFAULT_DIALOG_STYLE )
{
    m_mailConfig = mailCfg;

    m_logList = new wxListCtrl(this, -1, wxDefaultPosition, wxSize( 320, 240 ), wxLC_REPORT | wxLC_SINGLE_SEL );
    m_logList->Show();

 
    m_logList->InsertColumn(0, "Level");
    m_logList->InsertColumn(1, "Message", wxLIST_FORMAT_LEFT, 300);

    m_close = new wxButton( this, CLOSE, "Close");

    wxBoxSizer* main_sizer = new wxBoxSizer(wxVERTICAL);
    wxBoxSizer* button_sizer = new wxBoxSizer(wxHORIZONTAL);
    button_sizer->Add(m_close, 0, wxALIGN_CENTRE_HORIZONTAL);
    main_sizer->Add(m_logList, 1, wxALL | wxEXPAND, 3);
    main_sizer->Add(button_sizer, 0, wxALIGN_CENTRE_HORIZONTAL);
 
    SetSizer(main_sizer);
    main_sizer->Fit(this);
    main_sizer->SetSizeHints(this);
    CentreOnScreen();
};

void Messenger::LogMessage(int level, wxString& msg)
{

    long item;
    long idx = m_logList->GetItemCount();
    wxString message(msg);
    switch(level) {
       case ERROR_MESSAGE_LEVEL:
           item = m_logList->InsertItem(idx, wxString(ERROR_MESSAGE_LOG));
           m_logList->SetItemTextColour(item, *wxRED);
           break;
       case WARNING_MESSAGE_LEVEL:
           item = m_logList->InsertItem(idx, wxString(WARNING_MESSAGE_LOG));
           m_logList->SetItemTextColour(item, *wxBLUE);
           break;
       case INFO_MESSAGE_LEVEL:
           item = m_logList->InsertItem(idx, wxString(INFO_MESSAGE_LOG));
           break;
       default:
           item = m_logList->InsertItem(idx, wxString(ERROR_MESSAGE_LOG));
           m_logList->SetItemTextColour(item, *wxRED);
           message.Printf("Unknown message level '%ld'", level);
    }
    m_logList->SetItem(idx, 1, message);
};

void Messenger::SetEdiPath(wxString& path)
{
    m_ediPath = path;
};

void Messenger::SetComposer(Composer* composer)
{
    m_composer = composer;
};

bool Messenger::ProcessMessage(bool verifyOnly, bool noMail)
{
    if ( !noMail ) {
        if ( "" == m_logon.GetLogin() ) {
            std::auto_ptr<LoginFrame> login(new LoginFrame(GetParent(), &m_logon));
            if ( !(login->ShowModal() && m_logon.GetLogin() != "") ) {
                return false;
            }
        }
    }

    LOG_MESSAGE(INFO_MESSAGE_LEVEL, INET_TRANSACTION_START);

    if ( NULL != GetParent() ) {
        GetParent()->Enable(false);
    }
    m_logList->Clear();
    m_close->Enable(false);
    Show(true);
    if ( NULL == GetComposer() ) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, COMPOSE_ERROR_SETTING);
        return TerminateProcess(false);
    }
    try {
        wxString ediFilename(GetEdiFileName());
        if ( wxFile::Exists(ediFilename) ) {
            if ( wxMessageBox("File " + ediFilename + " already exists. Owerwrite?", COMPOSE_OVERWRITING_TITLE,
                     wxICON_QUESTION | wxYES_NO) != wxYES ) {
                LOG_MESSAGE(INFO_MESSAGE_LEVEL, COMPOSE_CANCEL);
                return TerminateProcess(false);
            }
        }
        if ( GetComposer()->compose(ediFilename) ) {
            if ( !verifyOnly ) {
                LOG_MESSAGE(INFO_MESSAGE_LEVEL, COMPOSE_OK);
            } else {
                LOG_MESSAGE(INFO_MESSAGE_LEVEL, VERIFY_OK);
                return TerminateProcess(true);
            }
        } else {
            LOG_MESSAGE(ERROR_MESSAGE_LEVEL, VERIFY_FAILED);
            wxRemoveFile(ediFilename);
            return TerminateProcess(false);
        }
        if ( !noMail ) {
            if ( PostData(ediFilename) ) {
                LOG_MESSAGE(INFO_MESSAGE_LEVEL, "OK");
            } else {
                LOG_MESSAGE(ERROR_MESSAGE_LEVEL, "FAIL");
            }
        }
        return TerminateProcess(true);
    } catch (wxString ex) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, ex);
        return TerminateProcess(false);
    }
};

void Messenger::OnClose() 
{
    if ( canTerminate() )  {
        if ( NULL == GetParent() ) {
            Destroy();
        } else {
           GetParent()->Enable(true);
           Show(false);
        }
    }
}

wxString Messenger::ComposeFileName()
{
    wxDateTime today = wxDateTime::Today();
    return wxString(GetMailConfig()->GetParam("subject", "ABST" )) + today.Format("%m%d") + ".txt";
};


bool Messenger::PostData(const wxString& fileName) 
{
    NetAutoHdr session;
    NetAutoHdr connection;
    NetAutoHdr request;


    session = InternetOpen("AdvClient 160",
                        INTERNET_OPEN_TYPE_PRECONFIG, 
                        NULL, NULL, 
                        0);

    if ( session.IsEmpty() ) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, wxString::Format(INET_ERROR, GetInetMessage(GetLastError())));
        return false;
    }
    LOG_MESSAGE(INFO_MESSAGE_LEVEL, INET_OPENNED);

    ::wxYield();

    connection = InternetConnect(session, 
                                 GetMailConfig()->GetParam("host"), 
                                 GetMailConfig()->GetParamAsLong("port"), 
                                 NULL, 
                                 NULL,
                                 INTERNET_SERVICE_HTTP,
                                 0,
                                 0);

    if ( connection.IsEmpty() ) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, wxString::Format(INET_ERROR, GetInetMessage(GetLastError())));
        return false;
    }
    LOG_MESSAGE(INFO_MESSAGE_LEVEL, INET_CONNECTION_OK);

    ::wxYield();

    wxString url(GetMailConfig()->GetParam("url"));
    wxFileName fn(fileName);

    url += wxString::Format("?filename=%s&username=%s&password=%s", fn.GetName(), m_logon.GetLogin(), m_logon.GetPassword());

    DWORD flags=INTERNET_FLAG_DONT_CACHE |INTERNET_FLAG_PRAGMA_NOCACHE|INTERNET_FLAG_RELOAD;

    if ( GetMailConfig()->GetParamAsBool("secure", true ) ) {
        flags = flags |INTERNET_FLAG_SECURE
                      |INTERNET_FLAG_IGNORE_CERT_CN_INVALID
                      |INTERNET_FLAG_IGNORE_CERT_DATE_INVALID;
    }

    request = HttpOpenRequest(connection,
                "POST",
                url,
                NULL,
                "",
                NULL,
                flags,
                0);

    if( request.IsEmpty() ) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, wxString::Format(INET_ERROR, GetInetMessage(GetLastError())));
        return false;
    }
    LOG_MESSAGE(INFO_MESSAGE_LEVEL, INET_REQUEST_CREATED);

    ::wxYield();

    wxString fileBody;

    wxFile fi(fileName);

    if ( !fi.IsOpened() ) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, wxString::Format(INET_CANT_OPEN_FILE,fileName));
    }

    while ( !fi.Eof() ) {
        char buffer[1024];
        int readen = fi.Read((void*)buffer, 1024);
        if ( readen == -1 ) {
            break;
        }
        fileBody.Append(buffer, readen);
    }

    wxString headers = "Content-Type: multipart/form-data; boundary=WW\r\n" 
                   + wxString::Format("Content-Length: %ld", fileBody.Length());


    LOG_MESSAGE(INFO_MESSAGE_LEVEL, INET_MESSAGE_BODY_CREATED);
    ::wxYield();


    if(!HttpSendRequest(request, headers.c_str(),headers.Length(),
                        (void*)(fileBody.c_str()), fileBody.Length()))
    {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, wxString::Format(INET_ERROR, GetInetMessage(GetLastError())));
        return false;
    }
    LOG_MESSAGE(INFO_MESSAGE_LEVEL, INET_MESSAGE_SENT);
    ::wxYield();

    wxString response = "";
    char buffer[1024];
    DWORD readed;
    while( InternetReadFile(request,buffer, sizeof(buffer), &readed) ){
        response.Append(buffer, readed);
        if ( readed < sizeof(buffer) ) {
            break;
        }
    };

    bool result = false;
    if ( response == "OK" ) {
        LOG_MESSAGE(INFO_MESSAGE_LEVEL, INET_TRANSACTION_COMPLETE);
        result = true;
    } else if (response.StartsWith(INET_INVALID_PSWD_RESP)) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, INET_INVALID_PASSWORD);
        m_logon.SetLogin("");
        m_logon.SetPassword("");
    } else if (response.StartsWith(INET_INVALID_VERSION_RESP)) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, INET_INVALID_VERSION);
        m_logon.SetLogin("");
        m_logon.SetPassword("");
    } else {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, wxString::Format("%s, '%s'", INET_UNKNOWN_ERROR, response));
        m_logon.SetLogin("");
        m_logon.SetPassword("");
    }
    ::wxYield();

    return result;
};

static wxString GetInetMessage(DWORD error) {
   char buffer[1024];

   HMODULE h = GetModuleHandle("wininet.dll");
   FormatMessage(FORMAT_MESSAGE_FROM_HMODULE, h, error, 0, buffer, 1024, NULL);
   return wxString(buffer);
};

 