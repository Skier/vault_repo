/*
 *  $RCSfile: Grid.h,v $
 *
 *  $Revision: 1.4 $
 *
 *  last change: $Date: 2003/10/13 17:12:08 $
 */

#ifndef __ADVPCS_GRID_H__ 
#define __ADVPCS_GRID_H__ 


/* -------------------------- header place ---------------------------------- */
#include <wx/grid.h>
#include <wx/cmdproc.h>
#include <wx/gdicmn.h>
/* -------------------------- implementation place -------------------------- */
class Grid : public wxGrid {
public:
    static wxRect NO_SELECTION;    
public:
    Grid( wxWindow* parent, 
          wxCommandProcessor& proc, 
          wxWindowID id, 
          const wxPoint& pos = wxDefaultPosition, 
          const wxSize& size = wxDefaultSize);
    
    void OnKeyDown( wxKeyEvent& event );
    void AddRow();
    void InsertRow();
    void DeleteRow();
    void DeleteRow(int row);
    void DeleteRow(int fromRow, int toRow);
	
	void ClearCells();

    void OnRangeSelected(wxGridRangeSelectEvent& event);
    
    const wxRect& GetSelection() const {
        return m_selection;
    };
    
    void SetSelection(const wxRect& rect) {
        m_restoration = true;
        m_selection = rect;
        SelectBlock(m_selection.GetTop(), m_selection.GetLeft(), 
                    m_selection.GetBottom(), m_selection.GetRight(), FALSE);
        m_restoration = false;
    };
protected: 
    wxCommandProcessor& GetProcessor() {
        return m_proc;
    };

private: 
    wxRect m_selection;
    wxCommandProcessor& m_proc;
    bool m_restoration;
};


#endif /* __ADVPCS_GRID_H__  */
