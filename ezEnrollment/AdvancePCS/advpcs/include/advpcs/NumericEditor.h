/*
 *  $RCSfile: DateEditor.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __GRID_NUMERIC_EDITOR__
#define __GRID_NUMERIC_EDITOR__

/* -------------------------- header place ---------------------------------- */
#include <wx/grid.h>
#include <wx/textctrl.h>
/* -------------------------- implementation place -------------------------- */

#define NUMERIC_PATTERN ".00"

class NumericCtrl : public wxTextCtrl
{
public:
    NumericCtrl(wxWindow *parent, wxWindowID id, const wxPoint &pos, const wxSize &size)
        : wxTextCtrl(parent, id, wxEmptyString, pos, size, wxTE_RIGHT)
    {
    };

    void OnChar(wxKeyEvent& event);

    virtual void SetValue(const wxString& value);
    virtual void SetMaxLength(unsigned long len);
private:
    void AddDigit(char digit);
    void RemoveDigit();

private:
    size_t        m_max_length;
    DECLARE_EVENT_TABLE()
};

class WXDLLEXPORT GridNumericCellEditor 
    : public wxGridCellTextEditor
{
public:
    virtual void Create(wxWindow* parent,
                        wxWindowID id,
                        wxEvtHandler* evtHandler);

    void BeginEdit(int row, int col, wxGrid* grid);

public:
    void SetMaxLength(unsigned long len);

private:
    size_t        m_max_length;
};

#endif
