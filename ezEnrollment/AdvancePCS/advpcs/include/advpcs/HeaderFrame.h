/*
 *  $RCSfile: HeaderFrame.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_HEADER_FRAME_H__
#define __ADVPCS_HEADER_FRAME_H__

/* -------------------------- header place ---------------------------------- */
#include <advpcs/HeaderPanel.h>
/* -------------------------- implementation place -------------------------- */

class MainFrame;

enum { Button_Ok, Button_Cancel};

class HeaderFrame : public wxDialog
{
public:
    HeaderFrame(MainFrame* mainFrame);

private:
    void OnOk(wxCommandEvent& event);
    void OnCancel(wxCommandEvent& event);

    HeaderPanel* GetHeaderPanel()
    {
        return m_headerPanel;
    };
      
private:
    HeaderPanel* m_headerPanel;

    DECLARE_EVENT_TABLE()
};

#endif /* __ADVPCS_HEADER_FRAME_H__ */

