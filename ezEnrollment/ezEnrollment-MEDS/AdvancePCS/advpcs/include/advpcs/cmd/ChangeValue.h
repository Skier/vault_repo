/*
 *  $RCSfile: ChangeValue.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/13 08:04:50 $
 */

#ifndef __ADVPCS_CMD_CHANGE_VALUE_H__
#define __ADVPCS_CMD_CHANGE_VALUE_H__

/* -------------------------- header place ---------------------------------- */
#include <wx/wx.h>
#include <wx/cmdproc.h>
#include <advpcs/DocTable.h>
/* -------------------------- implementation place -------------------------- */


class CmdChangeValue: public wxCommand {
public:
    CmdChangeValue(DocTable& table, int row, int col, const wxString& value)
        : wxCommand(TRUE, wxString::Format("Change value at row %d col %d", row, col)), 
          m_table(table), m_col(col), m_row(row), 
          m_value(value), m_wasUndo(FALSE)

    {
    };
    ~CmdChangeValue(){};

    // Override this to perform a command
    virtual bool Do();

    // Override this to undo a command
    virtual bool Undo();

private:
    DocTable&  m_table;
    int        m_row;
    int        m_col;
    wxString   m_value;
    wxString   m_oldValue;
    bool       m_wasUndo;
};

#endif /* __ADVPCS_CMD_CHANGE_VALUE_H__ */
