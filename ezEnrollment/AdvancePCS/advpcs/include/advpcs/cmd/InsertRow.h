/*
 *  $RCSfile: InsertRow.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#ifndef __ADVPCS_CMD_INSERT_ROW_H__
#define __ADVPCS_CMD_INSERT_ROW_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/Grid.h>
/* -------------------------- implementation place -------------------------- */


class CmdInsertRow: public wxCommand {
public:
    CmdInsertRow(Grid& grid, size_t rowNum)
        : wxCommand(TRUE, "Insert Row"), m_grid(grid), 
          m_rowNum(rowNum), m_oldCursorRow(-1), m_oldCursorCol(-1)

    {};
    ~CmdInsertRow(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

    size_t GetRow() {
        return m_rowNum;
    };

private:
    Grid&        m_grid;
    size_t       m_rowNum;
    size_t       m_oldCursorRow;
    size_t       m_oldCursorCol;
};

#endif /* __ADVPCS_CMD_INSERT_ROW_H__ */
