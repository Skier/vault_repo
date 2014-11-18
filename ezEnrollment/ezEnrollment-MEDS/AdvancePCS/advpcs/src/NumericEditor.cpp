/*
 *  $RCSfile: NumericEditor.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/NumericEditor.h>
/* -------------------------- implementation place -------------------------- */

const wxChar DOT = '.';

BEGIN_EVENT_TABLE(NumericCtrl, wxTextCtrl)
    EVT_CHAR(NumericCtrl::OnChar)
END_EVENT_TABLE()

void NumericCtrl::SetMaxLength(unsigned long len) {
    m_max_length = len;
    wxTextCtrl::SetMaxLength(m_max_length + 1);
}


void NumericCtrl::OnChar(wxKeyEvent& event)
{
    size_t keycode  = event.GetKeyCode();

    if ( WXK_BACK == keycode ) {
        RemoveDigit();
    } else if ( WXK_DELETE == keycode ) {
        SetValue(NUMERIC_PATTERN);
    } else if ( ::wxIsdigit(keycode) ) {
        AddDigit((char) keycode);
    }

    SetInsertionPointEnd();
}

void NumericCtrl::SetValue(const wxString& value) {
    wxTextCtrl::SetValue(value);
}


void NumericCtrl::AddDigit(char digit) {
    wxString value = GetValue();
    if ( m_max_length >= value.Length() ) { 
        size_t dotIndex = value.Length() - 3;

        value = value.erase(dotIndex, 1);
        value << digit;
        if ( value[0] == '0' ) {
            value = value.erase(0, 1);
            value = value.insert(dotIndex, 1, DOT);
        } else {
            value = value.insert(dotIndex+1, 1, DOT);
        }
        
        wxTextCtrl::SetValue(value);
    }
}

void NumericCtrl::RemoveDigit() {
    wxString value = GetValue();
    size_t dotIndex = value.Length() - 3;

    value = value.erase(dotIndex, 1);

    value = value.Mid(0, value.Length() - 1);
    if ( 2 > value.Length() ) {
        value = value.insert(0, 1, '0');
        value = value.insert(dotIndex, 1, DOT);
    } else {
        value = value.insert(dotIndex-1, 1, DOT);
    }

    wxTextCtrl::SetValue(value);
}

//
void GridNumericCellEditor::Create(wxWindow* parent,
                                wxWindowID id,
                                wxEvtHandler* evtHandler)
{
    m_control = new NumericCtrl(parent, id, wxDefaultPosition, wxDefaultSize);
    ((NumericCtrl*) m_control)->SetMaxLength(m_max_length);
    wxGridCellEditor::Create(parent, id, evtHandler);
}

void GridNumericCellEditor::BeginEdit(int row, int col, wxGrid* grid)
{
    wxString value = grid->GetCellValue(row, col);

    if ( value.IsEmpty() ) {
        value = NUMERIC_PATTERN; 
    }

    grid->GetTable()->SetValue(row, col, value);
    wxGridCellTextEditor::BeginEdit(row, col, grid);
}

void GridNumericCellEditor::SetMaxLength(unsigned long len) {
    m_max_length = len;
}

