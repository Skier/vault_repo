/*
 *  $RCSfile: SelectRange.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#ifndef __ADVPCS_CMD_SELECT_RANGE_H__
#define __ADVPCS_CMD_SELECT_RANGE_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/Grid.h>
/* -------------------------- implementation place -------------------------- */


class CmdSelectRange: public wxCommand {
public:
    CmdSelectRange(Grid& grid, wxGridRangeSelectEvent& evt)
        : wxCommand(TRUE, "Select Range"), m_grid(grid), 
          m_newSelection(evt.GetLeftCol(), evt.GetTopRow(), evt.GetRightCol(), evt.GetBottomRow())
    {
        
    };
    ~CmdSelectRange() {
    };

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

private:
    Grid&        m_grid;
    wxRect       m_oldSelection;
    wxRect       m_newSelection;

};

#endif /* __ADVPCS_CMD_SELECT_RANGE_H__ */
