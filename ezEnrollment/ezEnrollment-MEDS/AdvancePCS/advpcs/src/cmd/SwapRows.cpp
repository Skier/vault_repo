/*
 *  $RCSfile: SwapRows.cpp,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/16 15:13:20 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/cmd/SwapRows.h>
/* -------------------------- implementation place -------------------------- */
bool CmdSwapRows::Do() {
    if ( !m_table.GetDocument().SwapRows(m_row1, m_row2) ) {
		return FALSE;
	}
    if ( m_wasUndo ) {
        m_table.GetView()->ForceRefresh();
    }
    return TRUE;
};

bool CmdSwapRows::Undo() {
    m_table.GetDocument().SwapRows(m_row1, m_row2);
    m_table.GetView()->ForceRefresh();
    m_wasUndo = TRUE;
    return TRUE;
};
