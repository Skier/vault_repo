/*
 *  $RCSfile: Paste.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#ifndef __ADVPCS_CMD_PASTE_H__
#define __ADVPCS_CMD_PASTE_H__

/* -------------------------- header place ---------------------------------- */
#include <vector>
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/Grid.h>
#include <advpcs/DocTable.h>
#include <advpcs/cmd/ChangeValue.h>
/* -------------------------- implementation place -------------------------- */

typedef std::vector<wxString> StrList;
typedef std::vector<StrList> StringTable;

class CmdPaste : public wxCommand {
public:
    CmdPaste(Grid& grid, DocTable& table, StringTable& matrix, wxRect rect)
        : wxCommand(TRUE, "Paste"), m_matrix(matrix), m_grid(grid), 
          m_table(table), m_rect(rect), m_wasUndo(false)
    {};
    ~CmdPaste(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

private:
    Grid&       m_grid;
    DocTable&   m_table;
    StringTable m_matrix;
    wxRect      m_rect;
    wxCommandProcessor m_proc;
    bool        m_wasUndo;
};

#endif /* __ADVPCS_CMD_PASTE_H__ */
