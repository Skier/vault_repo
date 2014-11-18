/*
 *  $RCSfile: DeleteRow.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#ifndef __ADVPCS_CMD_DELETE_ROW_H__
#define __ADVPCS_CMD_DELETE_ROW_H__

/* -------------------------- header place ---------------------------------- */
#include <vector>
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/Grid.h>
#include <advpcs/DocTable.h>
/* -------------------------- implementation place -------------------------- */


class CmdDeleteRow: public wxCommand {
public:
    CmdDeleteRow(DocTable& table, Grid& grid, size_t rowNum)
        : wxCommand(TRUE, "Delete Row"), m_grid(grid), 
          m_rowNum(rowNum), m_oldCursorRow(-1), m_oldCursorCol(-1), 
          m_table(table), m_wasUndo(false)

    {};
    ~CmdDeleteRow(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

    size_t GetRow() {
        return m_rowNum;
    };

private:
    Grid&        m_grid;
    DocTable&    m_table;
    size_t       m_rowNum;
    size_t       m_oldCursorRow;
    size_t       m_oldCursorCol;
    wxCommandProcessor m_proc;
    bool         m_wasUndo;
};

#endif /* __ADVPCS_CMD_DELETE_ROW_H__ */
