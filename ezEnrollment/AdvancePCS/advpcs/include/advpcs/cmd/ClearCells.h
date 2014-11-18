/*
 *  $RCSfile: ClearCells.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 17:14:49 $
 */

#ifndef __ADVPCS_CMD_CLEAR_CELLS_H__
#define __ADVPCS_CMD_CLEAR_CELLS_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/Grid.h>
#include <advpcs/DocTable.h>
#include <advpcs/cmd/ChangeValue.h>
/* -------------------------- implementation place -------------------------- */


class CmdClearCells: public wxCommand {
public:
    CmdClearCells(DocTable& table, Grid& grid)
        : wxCommand(TRUE, wxString::Format("Clear selected cells")), 
          m_grid(grid), m_wasUndo(false), 
          m_table(table)
    {
        m_proc.Initialize();
    };
    ~CmdClearCells(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

private:
    Grid&        m_grid;
    wxCommandProcessor m_proc;
    bool m_wasUndo;
    DocTable&    m_table;
};

#endif /* __ADVPCS_CMD_CLEAR_CELLS_H__ */
