/*
 *  $RCSfile: ChangeValue.cpp,v $
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
#include <advpcs/cmd/ChangeValue.h>
/* -------------------------- implementation place -------------------------- */
bool CmdChangeValue::Do() {
    m_oldValue = m_table.GetDocument().GetValue(m_row, m_table.Map(m_col));
    m_table.GetDocument().SetValue(m_row, m_table.Map(m_col), m_value);
    if ( m_wasUndo ) {
        m_table.GetView()->ForceRefresh();
        m_table.GetView()->SetGridCursor(m_row, m_col);
    }
    return TRUE;
};

bool CmdChangeValue::Undo() {
    m_table.GetDocument().SetValue(m_row, m_table.Map(m_col), m_oldValue);
    m_table.GetView()->ForceRefresh();
    m_table.GetView()->SetGridCursor(m_row, m_col);
    m_wasUndo = TRUE;
    return TRUE;
};
