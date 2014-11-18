/*
 *  $RCSfile: Cfg.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Cfg.h: interface for the CCfg class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CFG_H__0B43B21D_57C8_4E21_A08F_A1DC4347EE1C__INCLUDED_)
#define AFX_CFG_H__0B43B21D_57C8_4E21_A08F_A1DC4347EE1C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <AFXWIN.H>
#include <stdio.h>
#include <atf/CfgException.h>

#pragma warning( disable : 4290 ) 

class ICfg {
public:
    virtual CString GetName()const  = 0;
    virtual void SetName(const char * name) = 0;
    
    virtual ICfg * CreateChild(const char *name ) = 0;
    virtual ICfg * GetChild(const char *name) throw (CCfgException) = 0;
    virtual ICfg * GetChildren() throw (CCfgException) = 0;
    virtual ICfg * GetNext() = 0; 

    virtual bool HasParam(const char *name) const = 0;
    
    virtual CString GetParam(const char *name, const char * defaultvalue) const = 0;
    virtual CString GetParam(const char *name) const throw (CCfgException) = 0;
    virtual void SetParam(const char *name, const char *value) = 0;
    
    virtual long GetParamAsLong(const char *name, long defaultValue) const = 0;
    virtual long GetParamAsLong(const char *name) const throw (CCfgException) = 0;
    virtual void SetParam(const char *name, long value) = 0;
    
    virtual bool GetParamAsBool(const char *name, bool defaultValue) const = 0;
    virtual bool GetParamAsBool(const char *name) const throw (CCfgException) = 0;
    virtual void SetParam(const char *name, bool value = false) = 0;
};

#endif // !defined(AFX_CFG_H__0B43B21D_57C8_4E21_A08F_A1DC4347EE1C__INCLUDED_)
