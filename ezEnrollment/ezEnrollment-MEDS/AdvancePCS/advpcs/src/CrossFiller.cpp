/*
 *  $RCSfile: CrossFiller.cpp,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <advpcs/CrossFiller.h>
#include <advpcs/Descriptor.h>
/* -------------------------- implementation place -------------------------- */

void CrossFiller::FillChoice(GridChoice& choice, int row, int col) {
    int fieldIdx = m_table.Map(col);
    const FieldDescriptor& desc = m_table.GetDocument().GetColumnDescriptor(fieldIdx);
    if ( desc.GetCrossFieldName().IsEmpty() ) {
        return;
    }

    const FieldDescriptor& crossDesc = m_table.GetDocument().GetColumnDescriptor(desc.GetCrossFieldName());
    int crossIdx = m_table.MapToGrid(m_table.GetDocument().GetColumnIdx(desc.GetCrossFieldName()));

    wxString crossValue = m_table.GetValue(row, crossIdx);
    long crossId = 0;
    int i = 0;
    for ( i=0; i<crossDesc.GetChoicesDesk().size(); i++ ) {
        if (crossValue == crossDesc.PrepareValue(crossDesc.GetChoicesDesk()[i].choice)) {
            crossId = crossDesc.GetChoicesDesk()[i].id;
        }
    }

    choice.GetCombo()->Clear();
    for ( i=0; i<desc.GetChoicesDesk().size(); i++ ) {
        const IdList& ids = desc.GetChoicesDesk()[i].ids;
        for ( int j=0; j<ids.size(); j++ ) {
            if ( crossId == ids[j] ) {
                choice.GetCombo()->Append(desc.GetChoices().Item(i));
                break;
            }
        }

    }
};    


