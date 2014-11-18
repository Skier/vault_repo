/*
 *  $RCSfile: HeaderPanel.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <wx/cmdproc.h>
#include <advpcs/HeaderPanel.h>
#include <advpcs/Resources.h>
#include <advpcs/Descriptor.h>
#include <advpcs/FieldEditor.h>
/* -------------------------- implementation place -------------------------- */


//
// HeaderPanel
//
HeaderPanel::HeaderPanel(wxWindow* parent, MainFrame* mainFrame)
    : wxScrolledWindow(parent, -1, wxDefaultPosition, wxDefaultSize, wxSUNKEN_BORDER | wxTAB_TRAVERSAL), 
      m_mainFrame(mainFrame), m_editors()
{
        wxASSERT(NULL != m_mainFrame);

    CreateFromHeader();
};
    
HeaderPanel::~HeaderPanel() {
    for (FieldEditorList::iterator i = GetEditors().begin(); i < GetEditors().end(); ++i) {
        delete (*i);
    }
};

void HeaderPanel::ApplyData()
{
    for (FieldEditorList::iterator i = GetEditors().begin(); i < GetEditors().end(); ++i) {
        (*i)->PostValue();
    }
};
    
void HeaderPanel::RefreshData()
{
    for (FieldEditorList::iterator i = GetEditors().begin(); i < GetEditors().end(); ++i) {
        (*i)->RefreshView();
    }
};
    
void HeaderPanel::CreateFromHeader()
{
    (void)new wxStaticText(this, -1, ADVPCS_HEADER_PANEL_PROPOSE);
    int textOffset = 20;
    int ctrlOffset = 105;
    int rowOffset = 20;
    int topOffset = 30;
    for (size_t i = 0; i < GetDocument().GetFieldCount(); i++) {
        Field& field = GetDocument().GetField(i);
        const FieldDescriptor& d = field.GetDescriptor();
        if ( d.IsEnabled() ) {
            wxStaticText* text = new wxStaticText(this, -1, d.GetName(), wxPoint(textOffset, topOffset));
            if ( d.IsRequired() ) {
                text->SetForegroundColour(REQUIRED_NAVY);
            }

            int ctrlWidth = (d.GetMaxSize()>10) ? 150 : ((d.GetType().Cmp(ADVPCS_EDIT_TYPE_CHOICE) == 0 ) ? 150 : (d.GetMaxSize() * 15));

            FieldEditor* fe = new FieldEditor(field, this, -1, wxPoint(ctrlOffset, topOffset), wxSize(ctrlWidth, -1) );

            GetEditors().push_back(fe);
            topOffset += rowOffset;
        }
    }
    SetScrollbars(-1,5,-1,topOffset/5+1);    
};

