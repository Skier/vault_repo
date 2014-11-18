/*
 *  $RCSfile: DeleteRows.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#ifndef __ADVPCS_CMD_DELETE_ROWS_H__
#define __ADVPCS_CMD_DELETE_ROWS_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/Grid.h>
#include <advpcs/DocTable.h>
#include <advpcs/cmd/DeleteRow.h>
/* -------------------------- implementation place -------------------------- */


class CmdDeleteRows: public wxCommand {
public:
    CmdDeleteRows(DocTable& table, Grid& grid, int fromRow, int toRow)
        : wxCommand(TRUE, wxString::Format("Delete Rows %d:%d", fromRow, toRow)), 
          m_grid(grid), m_fromRow(fromRow), m_toRow(toRow), m_wasUndo(false), 
          m_table(table)
    {
        m_proc.Initialize();
    };
    ~CmdDeleteRows(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

private:
    Grid&        m_grid;
    wxCommandProcessor m_proc;
    int m_fromRow;
    int m_toRow;
    bool m_wasUndo;
    DocTable&    m_table;
};

#endif /* __ADVPCS_CMD_DELETE_ROWS_H__ */
