#ifndef ___CLIENT_HEADER_PANEL___
#define ___CLIENT_HEADER_PANEL___

//#include "wx/wxprec.h"
#include "client/header.h"

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
    Header* m_header;
    wxList* m_controls;
    wxList* m_fields;
    
    void CreateFromHeader();    

    Header* GetHeader()
    {
        return m_header;
    };

    wxList* GetControls()
    {
        return m_controls;
    };

    wxList* GetFields()
    {
        return m_fields;
    };

};

#endif // ___CLIENT_HEADER_PANEL___
