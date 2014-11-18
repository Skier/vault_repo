class ZoomDiallerTaskBarIcon: public wxTaskBarIcon
{
public:
    ZoomDiallerTaskBarIcon(){}

    void OnLeftButtonDClick(wxTaskBarIconEvent&);
    void OnMenuRestore(wxCommandEvent&);
    void OnMenuHangup(wxCommandEvent&);
    void OnMenuExit(wxCommandEvent&);
    void OnMenuLiveChat(wxCommandEvent&);
    virtual wxMenu *CreatePopupMenu();

DECLARE_EVENT_TABLE()
};


// Define a new application
class ZoomDiallerApp: public wxApp
{
public:
    bool OnInit(void);
};

class ZoomDiallerDialog: public wxDialog
{
public:
    ZoomDiallerDialog(wxWindow* parent, const wxWindowID id, const wxString& title,
        const wxPoint& pos, const wxSize& size, const long windowStyle = wxDEFAULT_DIALOG_STYLE);
    ~ZoomDiallerDialog();

    void OnDial(wxCommandEvent& event);
    void OnHangup(wxCommandEvent& event);
    void OnPhoneList(wxCommandEvent& event);
    void OnSetup(wxCommandEvent& event);
    void OnAreaChanged(wxCommandEvent& event);
    void OnTest(wxCommandEvent& event);
    void OnTimer(wxTimerEvent& event);
    void OnCloseWindow(wxCloseEvent& event);

    void OnRasDial(RASCONNSTATE rasconnstate, DWORD dwError);
    void Init();
    void Dial(wxString& phone);
    void Hangup();
    void GetModemList();
	void CreateDUN(wxString& entryName, wxString& deviceName);
    void SetupLocalPhoneList();
    void FreeSideLogin();

	void LoadAreaList();
	void LoadPhoneList(wxString& areaCode);
	void SavePhoneList(wxString& areaCode);

protected:
    int             m_attempt;
	wxString		m_lastAreaCode;
	wxString		m_diallingPhoneNumber;
	bool			m_bootstrap_call;
	bool			m_online;

	wxTimer			m_timer;
    ZoomDiallerTaskBarIcon   *m_taskBarIcon;

    wxStatusBar		*m_status;
	wxNotebook		*m_notebook;
    //wxTextCtrl      *m_dun;
    wxComboBox      *m_phone;
    wxTextCtrl      *m_username;
    wxTextCtrl      *m_password;

#if FULL_VERSION
    wxTextCtrl      *m_areaCode;
#else
	wxComboBox      *m_areaCode;
#endif
    wxChoice        *m_modem;
	wxCheckBox		*m_useCountryCode;
	wxCheckBox		*m_useAreaCode;
    wxTextCtrl      *m_otherPrefix;

DECLARE_EVENT_TABLE()
};
