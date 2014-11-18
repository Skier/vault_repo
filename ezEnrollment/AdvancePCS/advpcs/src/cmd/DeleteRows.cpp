/*
 *  $RCSfile: DeleteRows.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2003/10/16 15:11:44 $
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
#include <advpcs/cmd/DeleteRows.h>
/* -------------------------- implementation place -------------------------- */
bool CmdDeleteRows::Do() {
    if ( !m_wasUndo ) {
        for (int row = m_toRow; row >= m_fromRow; row--) {
            m_proc.Submit(new CmdDeleteRow(m_table, m_grid, row));
        }
    } else {
        while ( m_proc.CanRedo() ) {
            m_proc.Redo();
        }
    }
    return TRUE;
};

bool CmdDeleteRows::Undo() {
    while ( m_proc.CanUndo() ) {
        m_proc.Undo();
    }
    m_wasUndo = true;
    return TRUE;
};
