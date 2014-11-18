/*
 *  $RCSfile: CrossFiller.h,v $
 *
 *  $Revision: 1.1 $
 *
 *  last change: $Date: 2003/10/17 16:12:48 $
 */

#ifndef __ADVPCS_CROSS_FILLER_H__
#define __ADVPCS_CROSS_FILLER_H__

/* -------------------------- header place ---------------------------------- */
#include "wx/wx.h"
#include "wx/grid.h"
#include "wx/generic/gridctrl.h"
#include "advpcs/Choice.h"
#include "advpcs/DocTable.h"
/* -------------------------- implementation place -------------------------- */

class CrossFiller : public ChoiceFiller {

public:
    CrossFiller(DocTable& table) : m_table(table) {};
    void FillChoice(GridChoice& choice, int row, int col);    

private:
    DocTable& m_table;
};


#endif /* __ADVPCS_CROSS_FILLER_H__ */
