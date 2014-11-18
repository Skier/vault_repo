/*
 *  $RCSfile: SortTable.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2004/04/19 14:47:13 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/cmd/SortTable.h>
/* -------------------------- implementation place -------------------------- */
bool CmdSortTable::Do() {

    if ( !m_wasUndo ) {
    	for ( int i=m_fromRow; i<=m_toRow; i++) {
    		for ( int j=m_toRow; j>i; j-- ) {
    			if ( m_ascend ) {
    				if ( m_table.GetDocument().GetValue(j-1, m_table.Map(m_byColumn)) > m_table.GetDocument().GetValue(j, m_table.Map(m_byColumn))) {
    					m_proc.Submit(new CmdSwapRows(m_table, j-1, j));
    				}
    			} else {
    				if ( m_table.GetDocument().GetValue(j-1, m_table.Map(m_byColumn)) < m_table.GetDocument().GetValue(j, m_table.Map(m_byColumn))) {
    					m_proc.Submit(new CmdSwapRows(m_table, j-1, j));
    				}
    			}
    		}
    	}
    } else {
        while ( m_proc.CanRedo() ) {
            m_proc.Redo();
        }
    }
    return TRUE;
};

bool CmdSortTable::Undo() {
    while ( m_proc.CanUndo() ) {
        m_proc.Undo();
    }
    m_wasUndo = true;
    return TRUE;
};
