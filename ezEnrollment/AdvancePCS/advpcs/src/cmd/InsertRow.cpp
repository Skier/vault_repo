/*
 *  $RCSfile: InsertRow.cpp,v $
 *
 *  $Revision: 1.1 $
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
#include <advpcs/cmd/InsertRow.h>
/* -------------------------- implementation place -------------------------- */
bool CmdInsertRow::Do() {
    m_oldCursorRow = m_grid.GetGridCursorRow();
    m_oldCursorCol = m_grid.GetGridCursorCol();

    m_grid.ClearSelection();
    m_grid.InsertRows(GetRow());
    m_grid.SetGridCursor( GetRow(), 0);
    m_grid.MakeCellVisible( GetRow(), 0);
    return TRUE;
};

bool CmdInsertRow::Undo() {
    m_grid.DeleteRows(GetRow());
    m_grid.SetGridCursor(m_oldCursorRow, m_oldCursorCol);
    m_grid.MakeCellVisible(m_oldCursorRow, m_oldCursorCol);
    return TRUE;
};
