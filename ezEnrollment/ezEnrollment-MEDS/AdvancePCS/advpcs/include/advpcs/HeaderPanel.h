/*
 *  $RCSfile: HeaderPanel.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_HEADER_PANEL_H__
#define __ADVPCS_HEADER_PANEL_H__

/* -------------------------- header place ---------------------------------- */
#include <vector>
#include <advpcs/Document.h>
#include <advpcs/FieldEditor.h>
#include <advpcs/MainFrame.h>
/* -------------------------- implementation place -------------------------- */

#define VALIDATE_TEXT 101

typedef std::vector<FieldEditor*> FieldEditorList;

class HeaderPanel : public wxScrolledWindow
{
public:
    HeaderPanel(wxWindow* parent, MainFrame* mainFrame);
    ~HeaderPanel();
    void ApplyData();
    void RefreshData();
    
    Document& GetDocument() { return *(m_mainFrame->GetDocument()); };
private:
    void CreateFromHeader();    

    FieldEditorList& GetEditors() { return m_editors; };

private:
    MainFrame* m_mainFrame;
    FieldEditorList m_editors;
};

#endif /* __ADVPCS_HEADER_PANEL_H__ */
