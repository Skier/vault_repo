/*
 *  $RCSfile: Choice.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/10/17 08:14:02 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/Choice.h>
/* -------------------------- implementation place -------------------------- */
bool GridChoice::EndEdit(int row, int col, wxGrid* grid) {
    bool result = wxGridCellChoiceEditor::EndEdit(row, col, grid);
    Combo()->SetSelection(-1);
    return result;
};

void GridChoice::BeginEdit(int row, int col, wxGrid* grid) {
    wxString value = grid->GetCellValue(row, col);
    m_filler->FillChoice(*this, row, col);
    int idx = -1;
    if ( value != wxEmptyString ) {
        for ( int i = 0; i < Combo()->GetCount(); i++ ) {
            if ( Combo()->GetString(i).StartsWith(value) ) {
                idx = i;
                break;
            }
        }
    }
    wxGridCellChoiceEditor::BeginEdit(row, col, grid);
    Combo()->SetSelection(idx);
};
