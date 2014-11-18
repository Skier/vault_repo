/*
 *  $RCSfile: IManageable.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IManageable.h: interface for the IManageable class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IMANAGEABLE_H__C5AB9877_7BA6_4EA9_9DA9_9990E8125425__INCLUDED_)
#define AFX_IMANAGEABLE_H__C5AB9877_7BA6_4EA9_9DA9_9990E8125425__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CPoolManager;

class IManageable {
public:
    virtual void SetManager(long interval, CPoolManager &manager) = 0;    
};

#endif // !defined(AFX_IMANAGEABLE_H__C5AB9877_7BA6_4EA9_9DA9_9990E8125425__INCLUDED_)
