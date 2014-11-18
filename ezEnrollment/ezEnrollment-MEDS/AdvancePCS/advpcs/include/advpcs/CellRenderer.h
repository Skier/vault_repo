/*
 *  $RCSfile: CellRenderer.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#ifndef __ADVPCS_CELL_RENDERER_H__
#define __ADVPCS_CELL_RENDERER_H__

/* -------------------------- header place ---------------------------------- */
#include "wx/wx.h"
#include "wx/grid.h"
#include "wx/generic/gridctrl.h"
/* -------------------------- implementation place -------------------------- */

class CellRenderer : public wxGridCellStringRenderer {
public:
    virtual void Draw(wxGrid& grid,
                      wxGridCellAttr& attr,
                      wxDC& dc,
                      const wxRect& rect,
                      int row, int col,
                      bool isSelected);

    virtual wxGridCellRenderer *Clone() const
        { return new CellRenderer; }

};

#endif /* __ADVPCS_CELL_RENDERER_H__ */