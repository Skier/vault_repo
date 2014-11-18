/*
 *  $RCSfile: IAppFactory.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IAppFactory.h: interface for the IAppFactory class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_IAPPFACTORY_H__4FA230B3_2416_4313_96B3_248F8063BDB6__INCLUDED_)
#define AFX_IAPPFACTORY_H__4FA230B3_2416_4313_96B3_248F8063BDB6__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Cfg.h>
#include <atf/IApp.h>
// #include <atf/SystemException.h>
// #include <atf/ConfigurationException.h>

#pragma warning( disable : 4290 ) 

class CSystemException;      
class CConfigurationException;
class CAtfException;     

class IAppFactory {
public:
    virtual IApp * CreateApplication(ICfg &cfg) throw (CSystemException, 
                                                       CConfigurationException, 
                                                       CAtfException)= 0;
};

#endif // !defined(AFX_IAPPFACTORY_H__4FA230B3_2416_4313_96B3_248F8063BDB6__INCLUDED_)
