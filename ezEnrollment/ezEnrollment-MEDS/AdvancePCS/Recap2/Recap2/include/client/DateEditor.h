#ifndef __GRID_DATE_EDITOR__
#define __GRID_DATE_EDITOR__

#include <wx/grid.h>
#include <wx/textctrl.h>

#define DATE_PATTERN "    /  /  "

class DateCtrl : public wxTextCtrl
{
public:
    DateCtrl(wxWindow *parent, wxWindowID id,
               const wxPoint &pos, const wxSize &size, int style = 0)
        : wxTextCtrl(parent, id, DATE_PATTERN, pos, size, style)
    {
        SetMaxLength(10);
    };

    void OnChar(wxKeyEvent& event);

private:
    DECLARE_EVENT_TABLE()
};

class WXDLLEXPORT GridDateCellEditor 
    : public wxGridCellTextEditor
{
public:
    virtual void Create(wxWindow* parent,
                        wxWindowID id,
                        wxEvtHandler* evtHandler);

    void BeginEdit(int row, int col, wxGrid* grid);


};

#endif
