#ifndef __RECAP2_CHIOCE_H__
#define __RECAP2_CHIOCE_H__

#include "wx/wx.h"
#include "wx/grid.h"
#include "wx/generic/gridctrl.h"


class GridChoice : public wxGridCellChoiceEditor {
public: 
    GridChoice(size_t count,
                const wxString choices[] = NULL,
                bool allowOthers = FALSE)
        : wxGridCellChoiceEditor(count, choices, allowOthers)
    {};


    virtual bool EndEdit(int row, int col, wxGrid* grid) {
        bool result = wxGridCellChoiceEditor::EndEdit(row, col, grid);
        Combo()->SetSelection(-1);
        return result;
    };

	virtual void BeginEdit(int row, int col, wxGrid* grid) {
		wxString value = grid->GetCellValue(row, col);
		int idx = -1;
		if ( value != wxEmptyString ) {
    		for ( int i = 0; i < Combo()->GetCount(); i++ ) {
	    		if ( Combo()->GetString(i).StartsWith(value) ) {
		    	    idx = i;
			    	break;
				}
			}
		}
		wxGridCellChoiceEditor::BeginEdit(row, col, grid);
        Combo()->SetSelection(idx);
	};
};

#endif /* __RECAP2_CHIOCE_H__ */
