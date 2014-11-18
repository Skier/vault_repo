#include <client/Messages.h>
#include <client/MainFrame.h>
#include <client/XmlDescriptor.h>
#include <client/ComposerDescriptor.h>
#include <client/composer.h>
#include <client/filematrix.h>
#include <client/Messenger.h>

#include <iostream>
#include <fstream>
#include <string.h>
#include <atf/XmlDocument.h>
#include <atf/StreamIOHandler.h>
#include <atf/CfgXml.h>
#include <atf/XmlLoader.h>
#include <atf/XmlLoadException.h>
#include <atf/CfgCmdLine.h>


#include <wx/msw/ole/automtn.h>

static CXmlDocument*       c_config;
static XmlDescriptor*      c_headerDescriptor;
static XmlDescriptor*      c_detailDescriptor;
static ComposerDescriptor* c_composerDescriptor;
static CCfgXml*            c_mailAgentConfig;
static wxString            c_log;
static Messenger*          c_messenger;

CXmlDocument* GetConfig()
{
    return c_config;
};

XmlDescriptor* GetHeaderDescriptor()
{
    return c_headerDescriptor;
};

XmlDescriptor* GetDetailDescriptor()
{
    return c_detailDescriptor;
};

ComposerDescriptor* GetComposerDescriptor()
{
    return c_composerDescriptor;
};

CCfgXml* GetMailAgentConfig()
{
    return c_mailAgentConfig;
};

Messenger* GetMessenger()
{
    return c_messenger;
};

void LogMessage(int level, wxString& message)
{
    GetMessenger()->LogMessage(level, message);
};

//remove first and last "" if found
wxString PreparePath(wxString& path)
{
    wxString new_path = wxString();
    if ( (path.First('\"') == 0) && (path.Last() == '\"') ) {
        new_path = wxString(path.AfterFirst('\"')).BeforeLast('\"');
        if ( new_path.Last == '\\' ) {
            return (wxString&)new_path.BeforeLast('\\');
        } else {
            return new_path;
        }
    } else {
        return path;
    }
};


bool LoadConfiguration(wxString& cfgFilename)
{
    c_config = new CXmlDocument();
    ifstream is(cfgFilename);
    if ( !is.is_open() ) {
        throw wxString(LOAD_XML_FILE_CANT_OPEN CONFIGURATION);
    }
    CStreamIOHandler handler(is);
    CXmlLoader loader(handler);
    try {
        loader.Load(*c_config);
    } catch (CXmlLoadException ex) {
        char buffer[1024];
        sprintf(buffer, "XmlException %s, %d", ex.GetErrorString(), ex.GetLineNumber());
        throw wxString(buffer);
    }
    return true;
};

void Init(wxString& cfgFilename) 
{ 
    try {
        LoadConfiguration(cfgFilename);
        CXmlNode* root = GetConfig()->GetRoot();
        if ( NULL == root ) {
            throw wxString(LOAD_XML_ROOT_NOT_FOUND);
        }
        CXmlNode* presentation = root->GetChild(PRESENTATION_CONF);
        if ( NULL == presentation ) {
            throw wxString(LOAD_XML_PRESENTATION_NOT_FOUND);
        }
        CXmlNode* headerNode = presentation->GetChild(HEADER_SUB_CONF);
        if ( NULL == headerNode ) {
            throw wxString(LOAD_XML_HEADER_NOT_FOUND);
        }
        c_headerDescriptor = new XmlDescriptor(*headerNode);

        CXmlNode* detailNode = presentation->GetChild(DETAIL_SUB_CONF);
        if ( NULL == detailNode ) {
            throw wxString(LOAD_XML_DETAIL_NOT_FOUND);
        }
        c_detailDescriptor = new XmlDescriptor(*detailNode);
        GetDetailDescriptor()->CheckValid();

        CXmlNode* composerNode = root->GetChild(COMPOSER_CONF);
        if ( NULL == composerNode ) {
            throw wxString(LOAD_XML_COMPOSER_NOT_FOUND);
        }
        c_composerDescriptor = new ComposerDescriptor(*composerNode);

        CCfgXml* conf = new CCfgXml(*GetConfig());
        c_mailAgentConfig = (CCfgXml*)conf->GetChild(MAIL_SUB_CONF);
    } catch (wxString ex) {
        wxMessageBox(ex, LOAD_XML_UNKNOWN_ERROR,
                        wxOK | wxCENTRE | wxICON_EXCLAMATION );
        exit(1);
    } catch (CAtfException &ex) {
        wxMessageBox(wxString(ex.GetText()), "Configuration error",
                        wxOK | wxCENTRE | wxICON_EXCLAMATION );
        exit(1);
    }

}

bool InitGui()
{
    MyFrame *frame = new MyFrame(FRAME_TITLE, 
        GetHeaderDescriptor(), GetDetailDescriptor(), GetComposerDescriptor());
    frame->Show(TRUE);
    wxTheApp->SetTopWindow(frame);

    {
        wxAutomationObject dummy;
    }
 
//    wxLogWindow *log = new wxLogWindow(frame, "Log");
//    wxLog::SetActiveTarget(log);

    // Create Messenger
    c_messenger = new Messenger(frame, GetMailAgentConfig());

    // Run wizard
    wxCommandEvent ev;
    frame->OnRunWizard(ev);

    return true;
}

bool InitConsole(int argc, char** argv)
{
#if 0
    CCfgCmdLine cfg(argc, argv);
    if ( !cfg.HasParam(DATA_FILE_KEY) ) {
        SHOW_MESSAGE(INFO_MESSAGE_PATTERN, USAGE);
        return false;
    }
    CString cfgFilename = cfg.GetParam(CONFIG_FILE_KEY, 
        CONFIGURATION);

    Init(wxString(cfgFilename));

    wxString ediPath(cfg.GetParam(EDI_PATH, "."));

    bool verify = cfg.HasParam(VERIFY_KEY);
    bool nomail = cfg.HasParam(NOMAIL_KEY);
    wxString dataFile(cfg.GetParam(DATA_FILE_KEY));

    c_messenger = new Messenger(NULL, GetMailAgentConfig());
    GetMessenger()->SetEdiPath(PreparePath(ediPath));
    try {
        FileMatrix* dataMatrix = new FileMatrix(PreparePath(wxString(dataFile)));
/*
        DefaultComposer composer(c_bufferSize,
                                 GetHeaderDescriptor(),
                                 GetDetailDescriptor(), 
                                 dataMatrix, 
                                 verify);
        GetMessenger()->SetComposer(&composer);
*/
        return GetMessenger()->ProcessMessage(verify, nomail);
    } catch (wxString ex) {
        LOG_MESSAGE(ERROR_MESSAGE_LEVEL, ex);
        return false;
    }
#else
    wxMessageBox(wxT("Not implemented yet."));
    return true;
#endif
}

bool MyApp::OnInit()
{

    Init(wxString(CONFIGURATION));

    if ( wxApp::argc == 1 ) {
        return InitGui();
    } else {
        InitConsole(wxApp::argc, wxApp::argv);
        return true;
    }
}


IMPLEMENT_APP(MyApp);

