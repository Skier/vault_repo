/*
 *  $RCSfile: DeleteRow.cpp,v $
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
#include <advpcs/cmd/DeleteRow.h>
#include <advpcs/cmd/ChangeValue.h>
/* -------------------------- implementation place -------------------------- */
bool CmdDeleteRow::Do() {
    if ( !m_wasUndo ) {
        m_oldCursorRow = m_grid.GetGridCursorRow();
        m_oldCursorCol = m_grid.GetGridCursorCol();

        for( int i = 0; i < m_grid.GetNumberCols(); i++) {
            m_proc.Submit( new CmdChangeValue(m_table, GetRow(), i, "") );
        }
    } else {
        while ( m_proc.CanRedo() )  {
            m_proc.Redo();
        }
    }
    m_grid.DeleteRows(GetRow());

    return TRUE;
};

bool CmdDeleteRow::Undo() {
    m_grid.InsertRows(GetRow());
    m_grid.SetGridCursor(m_oldCursorRow, m_oldCursorCol);
    m_grid.MakeCellVisible(m_oldCursorRow, m_oldCursorCol);
    while ( m_proc.CanUndo() ) {
       m_proc.Undo();
    }
    m_wasUndo = true;
    return TRUE;
};
