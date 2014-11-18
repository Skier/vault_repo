/*
 *  $RCSfile: IApp.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IApp.h: interface for the IApp class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IAPP_H__DDA41AAF_81E6_4CCC_84BD_01D2A53C5904__INCLUDED_)
#define AFX_IAPP_H__DDA41AAF_81E6_4CCC_84BD_01D2A53C5904__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CAtfException;
class CSystemException; 

#pragma warning( disable : 4290 ) 

class IApp {
public:
    IApp(){};
    virtual ~IApp(){};
public:
    virtual void Initialize() throw (CAtfException) = 0;
    virtual int Run() throw (CSystemException) = 0;
};

#endif // !defined(AFX_IAPP_H__DDA41AAF_81E6_4CCC_84BD_01D2A53C5904__INCLUDED_)
