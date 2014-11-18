/*
 *  $RCSfile: Environment.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Environment.h: interface for the CEnvironment class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ENVIRONMENT_H__EA3FB4AF_FF42_47DD_9D14_4A638B660536__INCLUDED_)
#define AFX_ENVIRONMENT_H__EA3FB4AF_FF42_47DD_9D14_4A638B660536__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Logger.h>

class CEnvironment {
public:
    CEnvironment();
    ILogger* GetLogger();
    ILogger& GetRefLogger();
    void SetLogger(ILogger *logger) { m_logger = logger; };
private:
    ILogger * m_logger;    
};

extern CEnvironment c_defaultEnvironment; 

#endif // !defined(AFX_ENVIRONMENT_H__EA3FB4AF_FF42_47DD_9D14_4A638B660536__INCLUDED_)
