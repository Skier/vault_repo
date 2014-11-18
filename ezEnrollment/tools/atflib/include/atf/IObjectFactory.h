/*
 *  $RCSfile: IObjectFactory.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IObjectFactory.h: interface for the IObjectFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IOBJECTFACTORY_H__73C9980F_CF58_4897_9EEA_BAC119C13957__INCLUDED_)
#define AFX_IOBJECTFACTORY_H__73C9980F_CF58_4897_9EEA_BAC119C13957__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class IObjectFactory {
public:
    virtual void* Create() = 0;
    virtual void  Destroy(void * obj) = 0;
    virtual void  Recycle(void * obj) {};
};

#endif // !defined(AFX_IOBJECTFACTORY_H__73C9980F_CF58_4897_9EEA_BAC119C13957__INCLUDED_)
