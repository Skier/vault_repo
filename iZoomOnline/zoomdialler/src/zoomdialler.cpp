#include <windows.h>
#include <ras.h>
#include <raserror.h>

#include "wx/wx.h"
#include "wx/taskbar.h"
#include "wx/notebook.h"
#include "wx/combobox.h"
#include "wx/config.h"
#include "wx/fileconf.h"

#include "sample.xpm"

#include "zoomwsH.h"
#include "fscwsH.h"
#include "zofwsH.h"

#include "ZoomWebServiceSoap.nsmap"
#include "FreeSideConnectorBinding.nsmap"
#include "ZoomOnlineFrontendBinding.nsmap"

#include "IEHtmlWin.h"
#include "zoomdialler.h"

#define ZOOMONLINE_DUN "iZoom Online Network"
#define MAX_ATTEMPT 3

static HRASCONN hRasConn = NULL;
ZoomDiallerDialog   *dialog = NULL;

IMPLEMENT_APP(ZoomDiallerApp)

VOID WINAPI RasDialFunc(UINT unMsg, RASCONNSTATE rasconnstate, DWORD dwError)
{
    dialog->OnRasDial(rasconnstate, dwError);
}

bool ZoomDiallerApp::OnInit(void)
{
    dialog = new ZoomDiallerDialog(NULL, wxID_ANY, wxT("iZoom Online Dialer 1.1"), wxDefaultPosition, wxSize(320, 200));
    dialog->Show(true);
    return true;
}


enum {
    ID_DIAL = 20001,
    ID_HANGUP,
    ID_SETUP,
    ID_PHONE_LIST,
    ID_AREA_CODE,
    ID_TEST
};

BEGIN_EVENT_TABLE(ZoomDiallerDialog, wxDialog)
    EVT_BUTTON(ID_DIAL, ZoomDiallerDialog::OnDial)
    EVT_BUTTON(ID_HANGUP, ZoomDiallerDialog::OnHangup)
    EVT_BUTTON(ID_PHONE_LIST, ZoomDiallerDialog::OnPhoneList)
    EVT_BUTTON(ID_SETUP, ZoomDiallerDialog::OnSetup)
    EVT_COMBOBOX(ID_AREA_CODE, ZoomDiallerDialog::OnAreaChanged)
    EVT_BUTTON(ID_TEST, ZoomDiallerDialog::OnTest)
    EVT_TIMER(wxID_ANY, ZoomDiallerDialog::OnTimer)
    EVT_CLOSE(ZoomDiallerDialog::OnCloseWindow)
END_EVENT_TABLE()


ZoomDiallerDialog::ZoomDiallerDialog(wxWindow* parent, const wxWindowID id, const wxString& title,
    const wxPoint& pos, const wxSize& size, const long windowStyle):
  wxDialog(parent, id, title, pos, size, windowStyle), m_timer(this)
{
    m_attempt = 0;
    m_bootstrap_call = false;
    m_online = false;

    wxConfigBase *config = new wxFileConfig("izoomdialler", wxEmptyString, wxGetOSDirectory() + "\\zoomdialler.ini");
    wxConfigBase::Set(config);

    wxString initialPhoneNumber = wxConfigBase::Get()->Read("Default/InitialPhoneNumber", _T("No InitialPhoneNumber"));
    wxString lastAreaCode = wxConfigBase::Get()->Read("Default/AreaCode", _T(""));
    wxString lastPhoneNumber = wxConfigBase::Get()->Read("Default/PhoneNumber", _T(""));
    wxString lastUsername = wxConfigBase::Get()->Read("Default/Username", _T(""));
    wxString lastPassword = wxConfigBase::Get()->Read("Default/Password", _T(""));
    wxString lastModem = wxConfigBase::Get()->Read("Default/Modem", _T(""));

    bool useCountryCode;
    bool useAreaCode;
    wxConfigBase::Get()->Read("Default/UseCountryCode", &useCountryCode, false);
    wxConfigBase::Get()->Read("Default/UseAreaCode", &useAreaCode, true);
    wxString otherPrefix = wxConfigBase::Get()->Read("Default/OtherPrefix", _T(""));

    Init();

    m_lastAreaCode = lastAreaCode;
    m_phone->SetValue(lastPhoneNumber);
    m_username->SetValue(lastUsername);
    m_password->SetValue(lastPassword);

//#if FULL_VERSION
    m_areaCode->SetValue(lastAreaCode);
//#endif
    m_modem->SetStringSelection(lastModem);
    m_useCountryCode->SetValue(useCountryCode);
    m_useAreaCode->SetValue(useAreaCode);
    m_otherPrefix->SetValue(otherPrefix);

    if ( !m_lastAreaCode.IsEmpty() ) {
        // not first time
        LoadPhoneList(m_lastAreaCode);
    } else {
        // first time
        m_notebook->SetSelection(1);
    }
#if FULL_VERSION
    m_timer.Start(60000);
#endif
}

ZoomDiallerDialog::~ZoomDiallerDialog()
{
    if ( m_timer.IsRunning() ) {
        m_timer.Stop();
    }
    delete m_taskBarIcon;
}

void ZoomDiallerDialog::OnDial(wxCommandEvent& WXUNUSED(event))
{
    if ( 0 >= m_attempt ) {
        m_attempt = MAX_ATTEMPT;
        m_bootstrap_call = false;

        wxString phone = m_phone->GetValue();
        if ( m_useAreaCode->GetValue() ) {
            phone = m_areaCode->GetValue() + phone;
        }
        if ( m_useCountryCode->GetValue() ) {
            phone = "1" + phone;
        }
        phone = m_otherPrefix->GetValue() + phone;

        m_status->SetStatusText("Dialling " + phone + " ...");
        Dial(phone);
    }
}

void ZoomDiallerDialog::Dial(wxString& phone) 
{
    m_diallingPhoneNumber = phone;

    RASDIALPARAMS rdParams;
    char  szBuf[256] = "";
    ZeroMemory(&hRasConn, sizeof(HRASCONN));
    ZeroMemory(&rdParams, sizeof(RASDIALPARAMS));
    rdParams.dwSize = sizeof(RASDIALPARAMS);
    rdParams.szCallbackNumber[0] = '*';
    rdParams.szDomain[0] = '*';

    //lstrcpy(rdParams.szEntryName, m_dun->GetValue().c_str());
    lstrcpy(rdParams.szEntryName, ZOOMONLINE_DUN);
    lstrcpy(rdParams.szPhoneNumber, phone.c_str());
    lstrcpy(rdParams.szUserName, m_username->GetValue().c_str());
    lstrcpy(rdParams.szPassword, m_password->GetValue().c_str());

//wxMessageBox(rdParams.szUserName, "Username");

    DWORD dwRet = RasDial(NULL, NULL, &rdParams, 0L, (LPVOID)RasDialFunc, &hRasConn);
    if ( dwRet ){
        if ( 0 != RasGetErrorString((UINT)dwRet, (LPSTR)szBuf, 256) ) {
            m_status->SetStatusText("Dial: internal error, cannot get error status.");
        }
        m_status->SetStatusText(wxString(_T("Cannot start dialling: ")) + szBuf);
    }

/*
        wxMessageBox("Connection is esteblished.", "Success");
        RASCONNSTATUS rStatus;
        ZeroMemory(&rStatus, sizeof(RASCONNSTATUS));
        rStatus.dwSize = sizeof(RASCONNSTATUS);
        DWORD dwRet = RasGetConnectStatus(hRasConn, &rStatus);
        RASCONNSTATE rcs = rStatus.rasconnstate;
        //cout << "RASCONNSTATE: " << rcs << endl;
        wxMessageBox("Device Name", rStatus.szDeviceName);
        wxMessageBox("Device Type", rStatus.szDeviceType);
        wxMessageBox("Phone Number", rStatus.szPhoneNumber);
*/
}

void ZoomDiallerDialog::OnRasDial(RASCONNSTATE rasconnstate, DWORD dwError) 
{
    if ( RASCS_Connected == rasconnstate ) {
        if ( !m_bootstrap_call ) {
            m_online =  true;
            wxConfigBase::Get()->Write("Default/PhoneNumber", m_phone->GetValue());
            wxConfigBase::Get()->Write("Default/Username", m_username->GetValue());
            wxConfigBase::Get()->Write("Default/Password", m_password->GetValue());

            m_status->SetStatusText("Logon to backend...");

#if FULL_VERSION
            FreeSideLogin();
#endif

            dialog->Show(false);
        } else {
            SetupLocalPhoneList();
        }
        m_status->SetStatusText("");
    } else if ( 0 != dwError ) {
        m_status->SetStatusText("");
        char  szBuf[256] = "";
        if ( 0 != RasGetErrorString((UINT)dwError, (LPSTR)szBuf, 256) ) {
            wxMessageBox("OnRasDial: internal error, cannot get error status.");
        }
        RasHangUp(hRasConn);
        m_online = false;
		wxString errorCode = wxString();
		errorCode.Printf("Connection Error %ld", dwError);
		wxMessageBox(szBuf, errorCode);
		// not authorized
		if ( 691 == dwError ) {
			m_attempt = 0;
		}

        m_attempt--;
        if ( 0 < m_attempt ) {
            m_status->SetStatusText("Redialling " + m_diallingPhoneNumber + "...");
            Dial(m_diallingPhoneNumber);
        }
    }
}

void ZoomDiallerDialog::OnHangup(wxCommandEvent& WXUNUSED(event))
{
    m_status->SetStatusText("Disconnection...");
    m_attempt = 0;
    Hangup();
}

void ZoomDiallerDialog::Hangup() 
{
    RasHangUp(hRasConn);
    m_online = false;
    m_status->SetStatusText("");
}

void ZoomDiallerDialog::GetModemList() 
{
    DWORD dwSize = 0;
    DWORD dwNumOfDevices = 0;
    DWORD dwError = RasEnumDevices(NULL, &dwSize, &dwNumOfDevices);

    RASDEVINFO *lpRdi = new RASDEVINFO[dwNumOfDevices];
    lpRdi->dwSize = sizeof(*lpRdi);
    dwError = RasEnumDevices(lpRdi, &dwSize, &dwNumOfDevices);
    if ( 0 != dwError ) {
        delete []lpRdi;
        char  szBuf[256] = "";
        if ( 0 != RasGetErrorString((UINT)dwError, (LPSTR)szBuf, 256) ) {
            wxMessageBox("GetModemList: internal error, cannot get error status.");
        } else {
            wxMessageBox(szBuf);
        }
        return;
    }

    m_modem->Clear();
    for(int i = 0; i < (int)dwNumOfDevices; i++) {
        wxString deviceType = lpRdi[i].szDeviceType;
        if ( 0 == deviceType.CmpNoCase("MODEM") ) {
            //wxMessageBox(lpRdi[i].szDeviceName, "Modem");
            m_modem->Append(lpRdi[i].szDeviceName);
        }
    }

    if ( 0 != (int)dwNumOfDevices ) {
        m_modem->SetSelection(0);
    }

    delete []lpRdi;
}

void ZoomDiallerDialog::OnSetup(wxCommandEvent& WXUNUSED(event))
{
    wxConfigBase::Get()->Write("Default/Modem", m_modem->GetStringSelection());
    wxConfigBase::Get()->Write("Default/UseCountryCode", m_useCountryCode->GetValue());
    wxConfigBase::Get()->Write("Default/AreaCode", m_areaCode->GetValue());
    wxConfigBase::Get()->Write("Default/UseAreaCode", m_useAreaCode->GetValue());
    wxConfigBase::Get()->Write("Default/OtherPrefix", m_otherPrefix->GetValue());

    if( ERROR_ALREADY_EXISTS == RasValidateEntryName(NULL, _T(ZOOMONLINE_DUN)) ) {
        RasDeleteEntry(NULL, _T(ZOOMONLINE_DUN));
    }
    CreateDUN(wxString(_T(ZOOMONLINE_DUN)), m_modem->GetStringSelection());

    m_notebook->SetSelection(0);
}

void ZoomDiallerDialog::OnPhoneList(wxCommandEvent& WXUNUSED(event))
{
    if( ERROR_ALREADY_EXISTS == RasValidateEntryName(NULL, _T(ZOOMONLINE_DUN)) ) {
        RasDeleteEntry(NULL, _T(ZOOMONLINE_DUN));
    }
    CreateDUN(wxString(_T(ZOOMONLINE_DUN)), m_modem->GetStringSelection());

    // call to 1-800
    wxString initialPhoneNumber = wxConfigBase::Get()->Read("Default/InitialPhoneNumber", _T("No InitialPhoneNumber"));
    //m_phone->SetValue(initialPhoneNumber);
    m_bootstrap_call = true;
    m_status->SetStatusText("Obtaining local phone list via " + initialPhoneNumber + " ...");

    // to do: should be removed
    if ( 0 == initialPhoneNumber.CmpNoCase("alex") ) {
        SetupLocalPhoneList();
        return;
    }
    
    Dial(initialPhoneNumber);
}

void ZoomDiallerDialog::SetupLocalPhoneList() {
    soap soap;
    _zoomws__GetLocalNumbers request;
    _zoomws__GetLocalNumbersResponse response;

    soap_init(&soap);

    wxString areaCode = m_areaCode->GetValue();
    request.areaCode = (char*) areaCode.c_str();
    
    wxString initialPhoneNumberUrl = wxConfigBase::Get()->Read("Default/InitialPhoneNumberUrl", _T(""));
    if ( initialPhoneNumberUrl.IsEmpty() ) {
        wxMessageBox("Invalid configuration, Default/InitialPhoneNumberUrl is not found.", "Error");
    } else {
        if ( SOAP_OK == soap_call___zoomws__GetLocalNumbers(&soap, 
                initialPhoneNumberUrl.c_str(), NULL, &request, &response) ) {
            m_phone->Clear();
            for (int i=0; i<response.GetLocalNumbersResult->list->__sizestring; i++) {
                m_phone->Append(response.GetLocalNumbersResult->list->string[i]);
            }

            if ( 0 != m_phone->GetCount() ) {
                m_phone->SetSelection(0);
                wxConfigBase::Get()->Write("Default/AreaCode", areaCode);
                wxConfigBase::Get()->Write("Default/Modem", m_modem->GetStringSelection());
                SavePhoneList(areaCode);
                wxMessageBox(response.GetLocalNumbersResult->warning, "Warning");
                m_notebook->SetSelection(0);
            } else {
                wxMessageBox("There are no any local phone number for area code " + areaCode,
                    "Error");
            }
        }
    }
    Hangup();
}

void ZoomDiallerDialog::CreateDUN(wxString& entryName, wxString& deviceName)
{
    if( ERROR_ALREADY_EXISTS != RasValidateEntryName(NULL, entryName.c_str()) ) {
        LPRASENTRY rasEntry;
		DWORD dwBufferSize = 0;
		// This is important! Find the buffer size (different from sizeof(RASENTRY)).
		RasGetEntryProperties(NULL, "", NULL, &dwBufferSize, NULL, NULL);
		if ( dwBufferSize == 0 ) {
            wxMessageBox("CreateDUN: internal error, cannot get RASENTRY size.");
		}

		rasEntry = (LPRASENTRY)malloc(dwBufferSize);
        ::ZeroMemory(rasEntry, dwBufferSize);

        rasEntry->dwSize = dwBufferSize;
		//rasEntry.dwType = RASET_Phone;
        rasEntry->dwfNetProtocols = RASNP_Ip;         
        rasEntry->dwFramingProtocol = RASFP_Ppp; 
        rasEntry->dwfOptions = (RASEO_RemoteDefaultGateway | RASEO_ModemLights | RASEO_RequireEncryptedPw | RASEO_RequireMsEncryptedPw) & (!RASEO_Custom);
        // DON'T USE "RASDT_Modem", use "modem", otherwise dwRV = 87
        strcpy(rasEntry->szDeviceType, "modem"); 
        strcpy(rasEntry->szDeviceName, deviceName.c_str());

        RasSetEntryProperties(NULL, entryName.c_str(), rasEntry, dwBufferSize, NULL, 0);

		free(rasEntry);
    }
}

void ZoomDiallerDialog::FreeSideLogin()
{
    soap soap;
    char *response;

    soap_init(&soap);
    soap_set_namespaces(&soap, fscws_namespaces);

    wxString fscwsUrl = wxConfigBase::Get()->Read("Default/FreeSideConnectorUrl", _T(""));

    int result = fscws::soap_call_fscws__SignOn(&soap, 
            fscwsUrl.c_str(),
            NULL, (char*) m_username->GetValue().c_str(), (char*) m_password->GetValue().c_str(), response);
    if ( SOAP_OK == result ) {
        //wxMessageBox(response);
        wxString url = response;
        if ( !wxLaunchDefaultBrowser(url) ) {
            wxMessageBox(_T("Failed to open URL \"%s\""), url.c_str());
        }
    } else {
        wxMessageBox(wxString::Format("FreeSide Connector invocation error = %ld", result), "Error");
    }
}

void ZoomDiallerDialog::OnAreaChanged(wxCommandEvent& WXUNUSED(event))
{
    m_phone->Clear();
    LoadPhoneList(m_areaCode->GetValue());
//    m_notebook->SetSelection(0);
}

void ZoomDiallerDialog::OnTest(wxCommandEvent& WXUNUSED(event))
{
    m_status->SetStatusText("Testing...");
    FreeSideLogin();
    m_status->SetStatusText("");
}

void ZoomDiallerDialog::OnTimer(wxTimerEvent& event)
{
//  wxMessageBox("OnTimer");
    if ( m_online ) {
        soap soap;
        char *response;

        soap_init(&soap);
        soap_set_namespaces(&soap, zofws_namespaces);

        wxString zofwsUrl = wxConfigBase::Get()->Read("Default/ZoomOnlineFrontendUrl", _T(""));
        
        int result = zofws::soap_call_zofws__GetNextUrl(&soap, 
                zofwsUrl.c_str(),
                NULL, (char*) wxString::Format("%ld", wxNewId()).c_str(), response);
        if ( SOAP_OK == result ) {
            wxString url = response;
/*
            if ( !wxLaunchDefaultBrowser(url) ) {
                wxMessageBox(_T("OnTimer: Failed to open URL \"%s\""), url.c_str());
            }
*/
//          url = "http://linux.kp.km.ua";
            wxDialog *popup = new wxDialog(this, wxID_ANY, _T("iZoom Online Dialer Message"), wxDefaultPosition, wxSize(500, 400));
            wxIEHtmlWin *browser = new wxIEHtmlWin(popup, wxID_ANY, wxDefaultPosition, popup->GetClientSize());
            browser->LoadUrl(url);
            popup->Centre(wxBOTH);
            popup->Show(true);

        } else {
            wxMessageBox(wxString::Format("iZoom Online Frontend invocation error = %ld", result), "Error");
        }
    }
}

void ZoomDiallerDialog::OnCloseWindow(wxCloseEvent& WXUNUSED(event))
{
    Show(false);
}

void ZoomDiallerDialog::Init(void)
{
    m_status = new wxStatusBar(this, wxID_ANY);
    //statbarBottom->SetStatusText(_T("This is a bottom status bar"), 0);

    m_notebook = new wxNotebook(this, wxID_ANY, wxPoint(0, 0), this->GetSize());

    wxPanel *dialPage = new wxPanel(m_notebook, wxID_ANY);
    m_notebook->AddPage(dialPage, _T("Dial"));
/*
    (void)new wxStaticText(dialPage, wxID_ANY, _T("DUN"), wxPoint(20, 10));
    m_dun = new wxTextCtrl(dialPage, -1, _T(ZOOMONLINE_DUN), wxPoint(80, 10), wxSize(140, 20));
    m_dun->SetEditable(false);
*/

    (void)new wxStaticText(dialPage, wxID_ANY, _T("Phone"), wxPoint(20, 40));
    m_phone = new wxComboBox(dialPage, -1, "", wxPoint(80, 40), wxSize(140, 20));

    (void)new wxStaticText(dialPage, wxID_ANY, _T("Username"), wxPoint(20, 70));
    m_username = new wxTextCtrl(dialPage, -1, "", wxPoint(80, 70), wxSize(140, 20));

    (void)new wxStaticText(dialPage, wxID_ANY, _T("Password"), wxPoint(20, 100));
    m_password = new wxTextCtrl(dialPage, -1, "", wxPoint(80, 100), wxSize(140, 20), wxTE_PASSWORD);

    (new wxButton(dialPage, ID_DIAL, _T("Dial"), wxPoint(60, 130), wxSize(80, 25)))->SetDefault();
    new wxButton(dialPage, ID_HANGUP, _T("Disconnect"), wxPoint(160, 130), wxSize(80, 25));

    //new wxButton(dialPage, ID_TEST, _T("Test"), wxPoint(250, 130));

    // second page
    wxPanel *setupPage = new wxPanel(m_notebook, wxID_ANY);
    m_notebook->AddPage(setupPage, _T("Setup"));

    (void)new wxStaticText(setupPage, wxID_ANY, _T("Area Code"), wxPoint(10, 10));
#if FULL_VERSION
    m_areaCode = new wxTextCtrl(setupPage, -1, m_lastAreaCode, wxPoint(70, 10), wxSize(70, 20));
    new wxButton(setupPage, ID_PHONE_LIST, _T("Get Local Phones"), wxPoint(150, 10), wxSize(100, 20));
#else
    m_areaCode = new wxComboBox(setupPage, ID_AREA_CODE, "", wxPoint(70, 10), wxSize(70, 20));
    LoadAreaList();
#endif

    (void)new wxStaticText(setupPage, wxID_ANY, _T("Modem"), wxPoint(20, 50));
    m_modem = new wxChoice(setupPage, -1, wxPoint(80, 50), wxSize(200, 20));
    GetModemList();

    m_useCountryCode = new wxCheckBox(setupPage, -1, _T("Use Country Code - 1"), wxPoint(20, 80));
    m_useAreaCode = new wxCheckBox(setupPage, -1, _T("Use Area Code"), wxPoint(150, 80));
    (void)new wxStaticText(setupPage, wxID_ANY, _T("Other Dialling Prefix(es)"), wxPoint(20, 110));
    m_otherPrefix = new wxTextCtrl(setupPage, -1, _T(""), wxPoint(150, 110), wxSize(100, 20));
    new wxButton(setupPage, ID_SETUP, _T("Save Settings"), wxPoint(150, 140), wxSize(100, 20));

    m_taskBarIcon = new ZoomDiallerTaskBarIcon();
    if ( !m_taskBarIcon->SetIcon(wxIcon(sample_xpm), wxT("ZoomDialler Dialer Icon")) ) {
        wxMessageBox(wxT("Could not set icon."));
    }

    wxBoxSizer *sizerTop = new wxBoxSizer(wxVERTICAL);
    sizerTop->Add(m_notebook, 0, wxGROW);
    //sizerTop->Add(-1, 10, 1, wxGROW);
    sizerTop->Add(m_status, 0, wxGROW);

    SetSizer(sizerTop);
    sizerTop->Fit(this);
    sizerTop->SetSizeHints(this);

    Centre(wxBOTH);
}

void ZoomDiallerDialog::LoadPhoneList(wxString& areaCode)
{
    wxString groupKey = "Area_" + areaCode;
    long count = wxConfigBase::Get()->Read(groupKey + "/PhoneCount", (long) 0);
    for (int i=0; i<(int)count; i++) {
        wxString phone = wxConfigBase::Get()->Read(
            groupKey + wxString::Format("/PhoneNumber%d", i), _T(""));
        m_phone->Append(phone);
    }
}

void ZoomDiallerDialog::LoadAreaList()
{
    wxString groupKey = "AreaList";
    long count = wxConfigBase::Get()->Read(groupKey + "/AreaCount", (long) 0);
    for (int i=0; i<(int)count; i++) {
        wxString areaCode = wxConfigBase::Get()->Read(
            groupKey + wxString::Format("/AreaCode%d", i), _T(""));
        m_areaCode->Append(areaCode);
    }
}

void ZoomDiallerDialog::SavePhoneList(wxString& areaCode)
{
    wxString groupKey = "Area_" + areaCode;
    wxConfigBase::Get()->DeleteGroup(groupKey);
    wxConfigBase::Get()->Write(groupKey + "/PhoneCount", (long) m_phone->GetCount());
    for (int i=0; i<m_phone->GetCount(); i++) {
        wxConfigBase::Get()->Write(groupKey + wxString::Format("/PhoneNumber%d", i), 
            m_phone->GetString(i));
    }
}

enum 
{
    PU_RESTORE = 10001,
    PU_HANGUP,
    PU_EXIT,
    PU_LIVECHAT
};


BEGIN_EVENT_TABLE(ZoomDiallerTaskBarIcon, wxTaskBarIcon)
    EVT_MENU(PU_RESTORE, ZoomDiallerTaskBarIcon::OnMenuRestore)
    EVT_MENU(PU_HANGUP, ZoomDiallerTaskBarIcon::OnMenuHangup)
    EVT_MENU(PU_EXIT,    ZoomDiallerTaskBarIcon::OnMenuExit)
#if FULL_VERSION
    EVT_MENU(PU_LIVECHAT,ZoomDiallerTaskBarIcon::OnMenuLiveChat)
#endif
    EVT_TASKBAR_LEFT_DCLICK  (ZoomDiallerTaskBarIcon::OnLeftButtonDClick)
END_EVENT_TABLE()

void ZoomDiallerTaskBarIcon::OnMenuRestore(wxCommandEvent& )
{
    dialog->Show(true);
}

void ZoomDiallerTaskBarIcon::OnMenuHangup(wxCommandEvent& )
{
    dialog->Hangup();
}

void ZoomDiallerTaskBarIcon::OnMenuExit(wxCommandEvent& )
{
    dialog->Hangup();
    dialog->Destroy();
}

void ZoomDiallerTaskBarIcon::OnMenuLiveChat(wxCommandEvent&)
{
    wxString url = wxConfigBase::Get()->Read("Default/ZoomOnlineFrontendLiveChatUrl", _T(""));
//    wxString url = _T("http://linux.kp.km.ua");

    if ( !wxLaunchDefaultBrowser(url) ) {
        wxMessageBox(_T("Failed to open URL \"%s\""), url.c_str());
    }
}

wxMenu *ZoomDiallerTaskBarIcon::CreatePopupMenu()
{
    wxMenu *menu = new wxMenu;
    menu->Append(PU_RESTORE, _T("&Restore iZoom Online Dialler"));
    menu->AppendSeparator();
#if FULL_VERSION
    menu->Append(PU_LIVECHAT, _T("&Live Chat"));    
    menu->AppendSeparator();
#endif
    menu->Append(PU_HANGUP, _T("&Disconnect"));    
    menu->Append(PU_EXIT,    _T("E&xit"));
    return menu;
}

void ZoomDiallerTaskBarIcon::OnLeftButtonDClick(wxTaskBarIconEvent&)
{
    dialog->Show(true);
}
