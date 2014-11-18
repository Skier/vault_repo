/*
 *  $RCSfile: CellRenderer.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <memory>
#include <advpcs/CellRenderer.h>
#include <advpcs/Resources.h>
#include <advpcs/DocTable.h>
/* -------------------------- implementation place -------------------------- */
void CellRenderer::Draw(wxGrid& grid, wxGridCellAttr& attr, wxDC& dc, const wxRect& rect, int row, int col, bool isSelected) {

#if 0 

    DocTable* t = (DocTable*)grid.GetTable();
    if ( t->GetDocument().GetColumnDescriptor(t->Map(col)).IsRequired() && t->GetDocument().IsValidValue(row, t->Map(col))) {
		attr.SetBackgroundColour(REQUIRED_COL);
	} else if ( !t->GetDocument().IsValidValue(row, t->Map(col))) { 
		attr.SetBackgroundColour(NOT_VALID_COL);
	} else {
		attr.SetBackgroundColour(VALID_COL);
	}

#endif 

	wxGridCellStringRenderer::Draw(grid, attr, dc, rect, row, col, isSelected);
};
