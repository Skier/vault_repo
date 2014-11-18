#include <wx/wx.h>
#include <wx/datetime.h>
#include <rxclaim/DateEditor.h>

BEGIN_EVENT_TABLE(DateCtrl, wxTextCtrl)
    EVT_CHAR(DateCtrl::OnChar)
END_EVENT_TABLE()

// DateCtrl
void DateCtrl::OnChar(wxKeyEvent& event)
{
    size_t keycode = event.GetKeyCode();
    size_t point = GetInsertionPoint();
    wxString value = GetValue();
    long from, to;

    GetSelection(&from, &to);
    if ( from != to ) {
        value = DATE_PATTERN;
    }

    if ( WXK_BACK == keycode ) {
        size_t prevPoint = point - 1;
        if ( prevPoint >= 0 
             && prevPoint != 2 
             && prevPoint != 5 
             && value.Length() == 10 ) {
             value[prevPoint] = 32;
             SetValue(value);
             size_t prevPrev = prevPoint - 1;
             
             if ( prevPrev == 2 
                 || prevPrev == 5 ) {
                SetInsertionPoint(prevPrev);
            } else {
                SetInsertionPoint(prevPoint);
            }
        }   
    } else if ( WXK_DELETE == keycode ) {
        if ( point != 2 
             && point != 5 
             && value.Length() == 10 ) {
            value[point] = 32;
            SetValue(value);
            SetInsertionPoint(point);
        }    
    } else if ( !wxIsprint((int)keycode) ) {
        event.Skip();
    } else if ( point >= 10 ) {
        event.Skip();
    } else {
        if ( point  == 2 || point == 5 ) {
            point++;    
        }
#if 0
        wxDateTime date;
        bool correct = date.ParseFormat(value, "%m/%d/%Y");
        if ( correct ) {
            value = date.Format("%m/%d/%Y");
        } else {
            value = DATE_PATTERN;
        }
#else
        // compatibility with pattern must be here
        if ( value.Length() != 10 ) {
            value = DATE_PATTERN;
        }
#endif
        if ( wxIsdigit((int)keycode) ) {
            value[point] = event.GetKeyCode();
            SetValue(value);
            SetInsertionPoint(point+1);
        } 
   }
}

//
void GridDateCellEditor::Create(wxWindow* parent,
                                wxWindowID id,
                                wxEvtHandler* evtHandler)
{
    m_control = new DateCtrl(parent, id, wxDefaultPosition, wxDefaultSize);
#if 0
                               , wxTE_PROCESS_TAB | wxTE_MULTILINE |
                                 wxTE_NO_VSCROLL | wxTE_AUTO_SCROLL
#endif
    wxGridCellEditor::Create(parent, id, evtHandler);
}

void GridDateCellEditor::BeginEdit(int row, int col, wxGrid* grid)
{
    wxString value = grid->GetCellValue(row, col);
    wxDateTime date;

    if ( date.ParseFormat(value, "%m/%d/%Y") ) {
        value = date.Format("%m/%d/%Y");
    } else {
#if 0
        value = wxDateTime::Now().Format("%d/%m/%Y");
#else
        value = DATE_PATTERN; 
#endif 
    }
    grid->GetTable()->SetValue(row, col, value);
    wxGridCellTextEditor::BeginEdit(row, col, grid);
}


