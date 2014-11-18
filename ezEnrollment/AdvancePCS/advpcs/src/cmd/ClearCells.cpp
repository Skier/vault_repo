/*
 *  $RCSfile: ClearCells.cpp,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 17:14:49 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/cmd/ClearCells.h>
/* -------------------------- implementation place -------------------------- */
bool CmdClearCells::Do() {
    if ( !m_wasUndo ) {
        if ( m_grid.IsSelection() ) {
    		for ( int i = 0; i < m_grid.GetNumberRows(); i++ ) {
    			for ( int j = 0; j < m_grid.GetNumberCols(); j++ ) {
    				if ( m_grid.IsInSelection(i,j) ) {
                        m_proc.Submit(new CmdChangeValue(m_table, i, j, "" ));
    				}
    			}
    		}
    	} else {
            m_proc.Submit(new CmdChangeValue(m_table, 
            								 m_grid.GetGridCursorRow(), 
            								 m_grid.GetGridCursorCol(), "" ));
    	}
    } else {
        while ( m_proc.CanRedo() ) {
            m_proc.Redo();
        }
    }
    m_grid.ForceRefresh();
    return TRUE;
};

bool CmdClearCells::Undo() {
    while ( m_proc.CanUndo() ) {
        m_proc.Undo();
    }
    m_wasUndo = true;
    m_grid.ForceRefresh();
    return TRUE;
};

