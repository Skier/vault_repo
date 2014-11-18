/*
 *  $RCSfile: IThreadFactory.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ThreadFactory.h: interface for the ThreadFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_THREADFACTORY_H__E82415E6_F003_49C6_AB15_1388F0D42AF2__INCLUDED_)
#define AFX_THREADFACTORY_H__E82415E6_F003_49C6_AB15_1388F0D42AF2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CThread;

class IThreadFactory {
public:
    virtual CThread* Create() = 0;
    virtual void  Destroy(CThread * obj) = 0;
};

#endif // !defined(AFX_THREADFACTORY_H__E82415E6_F003_49C6_AB15_1388F0D42AF2__INCLUDED_)
