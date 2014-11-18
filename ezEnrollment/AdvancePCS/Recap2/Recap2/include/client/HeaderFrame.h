#ifndef ___CLIENT_HEADER_FRAME___
#define ___CLIENT_HEADER_FRAME___

//#include "wx/wxprec.h"
//#include "client/header.h"
#include "client/HeaderPanel.h"

enum {
	Button_Ok,
	Button_Cancel
};

class HeaderFrame : public wxDialog
{
public:
    HeaderFrame(wxWindow *parent, Header* header);

private:

    Header* m_header;
    HeaderPanel* m_headerPanel;

	void OnOk(wxCommandEvent& event);
	void OnCancel(wxCommandEvent& event);

    Header* GetHeader()
    {
        return m_header;
    };

    HeaderPanel* GetHeaderPanel()
    {
        return m_headerPanel;
    };    

    DECLARE_EVENT_TABLE()
};

#endif ___CLIENT_HEADER_FRAME___
