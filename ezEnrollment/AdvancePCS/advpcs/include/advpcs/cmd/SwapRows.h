/*
 *  $RCSfile: SwapRows.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/16 15:13:20 $
 */

#ifndef __ADVPCS_CMD_SWAP_ROWS_H__
#define __ADVPCS_CMD_SWAP_ROWS_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/DocTable.h>
/* -------------------------- implementation place -------------------------- */


class CmdSwapRows: public wxCommand {
public:
    CmdSwapRows(DocTable& table, int row1, int row2)
        : wxCommand(TRUE, wxString::Format("Swap row %d and row %d", row1, row2)), 
          m_table(table), m_row1(row1), m_row2(row2), 
          m_wasUndo(FALSE)

    {
    };
    ~CmdSwapRows(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

private:
    DocTable&  m_table;
    int        m_row1;
    int        m_row2;
    bool       m_wasUndo;
};

#endif /* __ADVPCS_CMD_SWAP_ROWS_H__ */
