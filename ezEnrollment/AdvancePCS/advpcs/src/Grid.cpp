/*
 *  $RCSfile: Grid.cpp,v $
 *
 *  $Revision: 1.4 $
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
#include <wx/grid.h>
#include <advpcs/Grid.h>
#include <advpcs/cmd/AddRow.h>
#include <advpcs/cmd/InsertRow.h>
#include <advpcs/cmd/DeleteRow.h>
#include <advpcs/cmd/DeleteRows.h>
#include <advpcs/cmd/ClearCells.h>
#include <advpcs/cmd/SelectRange.h>
/* -------------------------- implementation place -------------------------- */
void Grid::OnKeyDown(wxKeyEvent& event)
{
    size_t keycode = event.GetKeyCode();
    if ( WXK_TAB == keycode ) {
        if (event.ShiftDown()) {
            if ( (GetGridCursorCol() == 0) && (GetGridCursorRow() > 0) ) {
                SetGridCursor(GetGridCursorRow() - 1, GetNumberCols() - 1);
                MakeCellVisible(GetGridCursorRow() - 1, GetNumberCols() - 1);
            } else {
                wxGrid::OnKeyDown( event );
            }
        } else {
            if ( GetGridCursorCol() == GetNumberCols() - 1 ) {
                if (GetGridCursorRow() == GetNumberRows() - 1) {
                    AddRow();
                    SetGridCursor(GetNumberRows() - 1, 0);
                    MakeCellVisible(GetNumberRows() - 1, 0);
                } else {
                    SetGridCursor(GetGridCursorRow() + 1, 0);
                    MakeCellVisible(GetGridCursorRow() + 1, 0);
                }
            } else {
                wxGrid::OnKeyDown( event );
            }
        }
    } else if ( WXK_RETURN == keycode || WXK_NUMPAD_ENTER == keycode ) {
        if ( GetGridCursorCol() == GetNumberCols() - 1 ) {
            if (GetGridCursorRow() == GetNumberRows() - 1) {
                SetGridCursor(GetNumberRows() - 1, 0);
                MakeCellVisible(GetNumberRows() - 1, 0);
            } else {
                SetGridCursor(GetGridCursorRow() + 1, 0);
                MakeCellVisible(GetGridCursorRow() + 1, 0);
            }
        } else {
            wxGrid::OnKeyDown( event );
        }
    } else if ( WXK_DELETE == keycode && GetNumberRows() > 0 ) {
		ClearCells();
	} else {
        wxGrid::OnKeyDown( event );
    }
}

Grid::Grid( wxWindow* parent, wxCommandProcessor& proc, wxWindowID id, const wxPoint& pos, const wxSize& size)
    : wxGrid( parent, id, pos, size), m_proc(proc), m_restoration(false)
{
    Connect(-1, wxEVT_KEY_DOWN, (wxObjectEventFunction) (wxEventFunction) (wxCharEventFunction) Grid::OnKeyDown);
};

void Grid::AddRow() {
    GetProcessor().Submit(new CmdAddRow(*this));
};

void Grid::InsertRow() {
    GetProcessor().Submit(new CmdInsertRow(*this, GetGridCursorRow()));
};
void Grid::DeleteRow() {
    GetProcessor().Submit(new CmdDeleteRow(*((DocTable*)GetTable()), *this, GetGridCursorRow()));
};
void Grid::DeleteRow(int row) {
    wxASSERT(row < GetNumberRows());
    GetProcessor().Submit(new CmdDeleteRow(*((DocTable*)GetTable()), *this, row));
};
void Grid::DeleteRow(int fromRow, int toRow) {
    wxASSERT(fromRow >= 0);
    wxASSERT(toRow < GetNumberRows());
    GetProcessor().Submit(new CmdDeleteRows(*((DocTable*)GetTable()), *this, fromRow, toRow));
};
void Grid::ClearCells() {
    GetProcessor().Submit(new CmdClearCells(*((DocTable*)GetTable()), *this));
};


wxRect Grid::NO_SELECTION(0,0,0,0);    

void Grid::OnRangeSelected( wxGridRangeSelectEvent& ev )
{
    if ( ev.Selecting() ) {
        if ( !m_restoration ) {
            GetProcessor().Submit(new CmdSelectRange(*this, ev));
        }
    } else {
#if 0
        // todo unselect command
        m_TopSelected = -1;
        m_LeftSelected = -1;
        m_BottomSelected = -1;
        m_RightSelected = -1;
#endif 
    }
    ev.Skip();
}

