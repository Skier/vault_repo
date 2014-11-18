/*
 *  $RCSfile: IRunnable.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IRunnable.h: interface for the IRunnable class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IRUNNABLE_H__BE9DECF2_3C9C_412C_B0D3_27642366D55C__INCLUDED_)
#define AFX_IRUNNABLE_H__BE9DECF2_3C9C_412C_B0D3_27642366D55C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CThread;

class IRunnable  {
public:
    IRunnable(){};
    virtual ~IRunnable(){};
public:
	virtual int Run(CThread * thisThread) = 0;
};

#endif // !defined(AFX_IRUNNABLE_H__BE9DECF2_3C9C_412C_B0D3_27642366D55C__INCLUDED_)
