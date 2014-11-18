/*
 *  $RCSfile: FieldEditor.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/10/13 17:12:08 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/FieldEditor.h>
#include <advpcs/Resources.h>
#include <advpcs/Document.h>
#include <advpcs/Descriptor.h>
#include <advpcs/DateEditor.h>
#include <advpcs/NumericEditor.h>
/* -------------------------- implementation place -------------------------- */
FieldEditor::FieldEditor(Field& field, wxWindow* parent, wxWindowID id, const wxPoint& pos, const wxSize& size)
    : m_field(field), m_control(NULL)
{
    if ( 0 == GetField().GetDescriptor().GetType().CmpNoCase(ADVPCS_EDIT_TYPE_CHOICE) ) {
        wxChoice* choice = new wxChoice(parent, id, pos, size);
        for (size_t i=0; i<GetField().GetDescriptor().GetChoices().GetCount(); i++) {
            choice->Append(GetField().GetDescriptor().GetChoices().Item(i));
        }
        m_control = choice;
    } else if ( GetField().GetDescriptor().GetType().CmpNoCase("date") == 0
        || GetField().GetDescriptor().GetType().CmpNoCase("date0") == 0 
        || GetField().GetDescriptor().GetType().CmpNoCase("date9") == 0 
        || GetField().GetDescriptor().GetType().CmpNoCase("date09") == 0 
        || GetField().GetDescriptor().GetType().CmpNoCase("longdate") == 0 
        || GetField().GetDescriptor().GetType().CmpNoCase("longdate0") == 0 
        || GetField().GetDescriptor().GetType().CmpNoCase("longdate9") == 0 
        || GetField().GetDescriptor().GetType().CmpNoCase("longdate09") == 0 ) 
    {
        DateCtrl* dc = new DateCtrl(parent, id, pos, size);
        m_control = dc;

    } else if ( 0 == GetField().GetDescriptor().GetType().CmpNoCase(ADVPCS_EDIT_TYPE_MONEY) ) {
        NumericCtrl* nc =  new NumericCtrl(parent, id, pos, size);
        m_control = nc;
        nc->SetMaxLength(GetField().GetDescriptor().GetMaxSize());

    } else {

        wxTextCtrl* tc = new wxTextCtrl(parent, id, GetField().GetValue(), pos, size);
        if ( GetField().GetDescriptor().GetType().CmpNoCase(ADVPCS_EDIT_TYPE_DATE) == 0 ) {
            tc->SetMaxLength(ADVPCS_DATE_MAX_SIZE);
        } else {
            tc->SetMaxLength(GetField().GetDescriptor().GetMaxSize());
        }
        m_control = tc;
    }

    wxASSERT(NULL != m_control);
};

FieldEditor::~FieldEditor() {
};

wxString FieldEditor::GetBufferValue() {
    if (0 == GetField().GetDescriptor().GetType().CmpNoCase(ADVPCS_EDIT_TYPE_CHOICE) ) {
        wxChoice& c = (wxChoice&)GetControl();
        return c.GetStringSelection();
    } else {
        wxTextCtrl& t = (wxTextCtrl&)GetControl();
        return t.GetValue();
    }
};

void FieldEditor::SetBufferValue(const wxString& value) {
    if (0 == GetField().GetDescriptor().GetType().CmpNoCase(ADVPCS_EDIT_TYPE_CHOICE) ) {
        wxChoice& c = (wxChoice&)GetControl();
        c.SetStringSelection(value);
    } else {
        wxTextCtrl& t = (wxTextCtrl&)GetControl();
        t.SetValue(value);
    }
};


void FieldEditor::PostValue() {
    if ( GetField().GetValue() != GetBufferValue() ) {
        GetField().SetValue(GetBufferValue());
    }
};

void FieldEditor::RefreshView() {
    SetBufferValue(GetField().GetValue());
};
