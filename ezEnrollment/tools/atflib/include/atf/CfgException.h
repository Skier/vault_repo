/*
 *  $RCSfile: CfgException.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CfgException.h: interface for the CCfgException class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CFGEXCEPTION_H__B4870062_64D7_4678_B8C8_AA52AA4C994B__INCLUDED_)
#define AFX_CFGEXCEPTION_H__B4870062_64D7_4678_B8C8_AA52AA4C994B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Exception.h>

class CCfgException : public CAtfException {
public:
    CCfgException(const char *fileName, int lineNo, ATF_ERROR code, 
                  const char *message)
        :CAtfException(fileName, lineNo, code, message){};
};

#define THROW_CFG_EXCEPTION(message) \
     throw CCfgException(__FILE__, __LINE__, ATF_INVALID_CFG_ERR, message)

#endif // !defined(AFX_CFGEXCEPTION_H__B4870062_64D7_4678_B8C8_AA52AA4C994B__INCLUDED_)
