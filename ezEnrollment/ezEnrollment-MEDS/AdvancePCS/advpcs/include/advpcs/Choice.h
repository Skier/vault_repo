/*
 *  $RCSfile: Choice.h,v $
 *
 *  $Revision: 1.4 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#ifndef __ADVPCS_CHIOCE_H__
#define __ADVPCS_CHIOCE_H__

/* -------------------------- header place ---------------------------------- */
#include "wx/wx.h"
#include "wx/grid.h"
#include "wx/generic/gridctrl.h"
/* -------------------------- implementation place -------------------------- */

class GridChoice;

class ChoiceFiller {
public:
    virtual void FillChoice(GridChoice& choice, int row, int col) = 0;    
};

class DummyFiller : public ChoiceFiller {
public:
    virtual void FillChoice(GridChoice& choice, int row, int col)  {
    };    
};

class GridChoice : public wxGridCellChoiceEditor {
public: 
    GridChoice( ChoiceFiller* filler,
                size_t count = 0,
                const wxString choices[] = NULL,
                bool allowOthers = FALSE)
        : wxGridCellChoiceEditor(count, choices, allowOthers), m_filler(filler)
    {
        wxASSERT(NULL != filler);
    };

    ~GridChoice() {
        wxDELETE(m_filler);
    };

    virtual bool EndEdit(int row, int col, wxGrid* grid);
    virtual void BeginEdit(int row, int col, wxGrid* grid);
    
    wxComboBox *GetCombo() const { return Combo(); }
private:
    ChoiceFiller* m_filler;
};

#endif /* __ADVPCS_CHIOCE_H__ */
