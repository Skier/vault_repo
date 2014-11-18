/*
 *  $RCSfile: IService.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// IService.h: interface for the IService class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_ISERVICE_H__8402DFF9_EB32_421B_B9C7_D2D852F379ED__INCLUDED_)
#define AFX_ISERVICE_H__8402DFF9_EB32_421B_B9C7_D2D852F379ED__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class ILogger;
class ICfg;
#include <AFXWIN.H>

class IService {
public:
    IService(const char * name):m_name(name){};
    virtual ~IService(){};

    CString GetName() const {return m_name;};
    void SetName(const char *name) {m_name = name;};

    virtual void EnableLogging(ILogger * logger) = 0;
    virtual void Configure(ICfg & cfg) = 0;
    virtual void Initialize() = 0;
    virtual void Start() = 0;
    virtual void Stop() = 0;
private:
    CString m_name;
};

#endif // !defined(AFX_ISERVICE_H__8402DFF9_EB32_421B_B9C7_D2D852F379ED__INCLUDED_)
