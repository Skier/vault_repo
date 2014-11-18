#ifndef __RXCLAIM_HEADER_FRAME_H__
#define __RXCLAIM_HEADER_FRAME_H__

#include <rxclaim/HeaderPanel.h>

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

#endif /* __RXCLAIM_HEADER_FRAME_H__ */

