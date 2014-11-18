/*
 *  $RCSfile: SelectRange.cpp,v $
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
#include <advpcs/cmd/SelectRange.h>
/* -------------------------- implementation place -------------------------- */
bool CmdSelectRange::Do() {
    m_oldSelection = m_grid.GetSelection();
/*   
    if (m_oldSelection.GetTop() > m_newSelection.GetTop()) {
        m_grid.SetSelection(wxRect(m_oldSelection.GetLeft(), m_newSelection.GetTop(), m_oldSelection.GetWidth(), m_oldSelection.GetHeight()));

    } else if (m_oldSelection.GetLeft() > m_newSelection.GetLeft()) {
        m_grid.SetSelection(wxRect(m_newSelection.GetLeft(), m_oldSelection.GetTop(), m_oldSelection.GetWidth(), m_oldSelection.GetHeight()));

    } else if (m_oldSelection.GetBottom() < m_newSelection.GetBottom()) {
        m_grid.SetSelection(wxRect(wxPoint(m_oldSelection.GetTop(), m_oldSelection.GetLeft()), 
                            wxPoint(m_newSelection.GetBottom(), m_oldSelection.GetRight())));

    } else if (m_oldSelection.GetRight() < m_newSelection.GetRight()) {
        m_grid.SetSelection(wxRect(wxPoint(m_oldSelection.GetTop(), m_oldSelection.GetLeft()), 
                            wxPoint(m_oldSelection.GetBottom(), m_newSelection.GetRight())));
    };
*/
    m_grid.SetSelection(m_newSelection);
    return TRUE;
};

bool CmdSelectRange::Undo() {
    m_grid.SetSelection(m_oldSelection);
    return TRUE;
};

