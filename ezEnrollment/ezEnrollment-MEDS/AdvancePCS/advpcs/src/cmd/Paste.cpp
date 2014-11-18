/*
 *  $RCSfile: Paste.cpp,v $
 *
 *  $Revision: 1.3 $
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
#include <advpcs/cmd/Paste.h>
#include <advpcs/cmd/AddRow.h>
/* -------------------------- implementation place -------------------------- */
bool CmdPaste::Do() {
    if ( !m_wasUndo ) {
        if ( m_grid.GetNumberRows() < 1 ) {
            m_proc.Submit(new CmdAddRow(m_grid));
            m_grid.SetGridCursor( 0, 0);
        }

        m_grid.SetGridCursor(m_rect.GetTop(), m_rect.GetLeft());

        if ( (m_matrix.size() == 1) && (m_matrix[0].size() == 1) ) {
            for (int i=m_rect.GetTop(); i<=m_rect.GetBottom(); i++){
                for (int j=m_rect.GetLeft(); j<=m_rect.GetRight(); j++) {
                    m_proc.Submit(new CmdChangeValue(m_table, i, j, m_matrix[0][0] ));
                }
            }
        } else {
            long offset = 0;
            int topRow = m_grid.GetGridCursorRow();
            int leftCol = m_grid.GetGridCursorCol();
            for ( StringTable::iterator row=m_matrix.begin(); row<m_matrix.end(); ++row, offset++ ) {
                        
                if ( (topRow+offset) == m_grid.GetNumberRows() ) {
                    m_proc.Submit( new CmdAddRow(m_grid) );
                }

                for ( size_t i=0; i<(*row).size(); i++ ) {
                    if ( (leftCol+i) < m_grid.GetNumberCols() ) {

                        if ( (*row)[i].Last() == '\r' ) {
                            m_proc.Submit( new CmdChangeValue(
                                                m_table,
                                                topRow+offset, 
                                                leftCol+i,
                                                (*row)[i].RemoveLast() )
                                         );
                        } else {
                            m_proc.Submit( new CmdChangeValue(
                                                m_table,
                                                topRow+offset, 
                                                leftCol+i,
                                                (*row)[i] )
                                         );
                        }
                    }
                }
            }
        }
    } else {
        while ( m_proc.CanRedo() ) {
            m_proc.Redo();
        }
    }
    m_grid.ForceRefresh();
    return TRUE;
};

bool CmdPaste::Undo() {
    while ( m_proc.CanUndo() ) {
        m_proc.Undo();
    }
    m_wasUndo = true;
    m_grid.ForceRefresh();
    return TRUE;
};

