/*
 *  $RCSfile: IPool.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IPool.h: interface for the IPool class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IPOOL_H__493E78AA_5547_49E5_A9A3_8E55A22A491E__INCLUDED_)
#define AFX_IPOOL_H__493E78AA_5547_49E5_A9A3_8E55A22A491E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class IPool {
public:
    virtual void* Get() = 0;
    virtual void Free(void* obj) = 0;
};

#endif // !defined(AFX_IPOOL_H__493E78AA_5547_49E5_A9A3_8E55A22A491E__INCLUDED_)
