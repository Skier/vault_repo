#ifndef __RXCLAIM_HEADER_PANEL_H__
#define __RXCLAIM_HEADER_PANEL_H__

#include <rxclaim/Header.h>

// ----------------------------------------------------------------------------
// constants
// ----------------------------------------------------------------------------

#define VALIDATE_TEXT 101

class HeaderPanel : public wxScrolledWindow
{
public:
    HeaderPanel(wxWindow* parent, Header* header);
    
    void ApplyData();
    void RefreshData();
    
private:
    void CreateFromHeader();    

    Header* GetHeader() { return m_header; };
    wxList* GetControls() { return m_controls; };
    wxList* GetFields() { return m_fields; };

private:
    Header* m_header;
    wxList* m_controls;
    wxList* m_fields;
};

#endif /* __RXCLAIM_HEADER_PANEL_H__ */
