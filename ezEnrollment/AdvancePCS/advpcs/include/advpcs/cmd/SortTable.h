/*
 *  $RCSfile: SortTable.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/16 15:13:20 $
 */

#ifndef __ADVPCS_CMD_SORT_TABLE_H__
#define __ADVPCS_CMD_SORT_TABLE_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/DocTable.h>
#include <advpcs/cmd/SwapRows.h>
/* -------------------------- implementation place -------------------------- */


class CmdSortTable: public wxCommand {
public:
    CmdSortTable(DocTable& table, int fromRow, int toRow, int byColumn, bool ascend)
        : wxCommand(TRUE, wxString::Format("Sort Data by column %d", byColumn)), 
          m_fromRow(fromRow), m_toRow(toRow), m_byColumn(byColumn),
          m_wasUndo(false), m_table(table), m_ascend(ascend)
    {
        m_proc.Initialize();
    };
    ~CmdSortTable(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

private:
    wxCommandProcessor m_proc;
    int m_fromRow;
    int m_toRow;
    int m_byColumn;
    bool m_wasUndo;
	bool m_ascend;
    DocTable&    m_table;
};

#endif /* __ADVPCS_CMD_SORT_TABLE_H__ */
