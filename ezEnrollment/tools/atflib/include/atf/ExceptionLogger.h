/*
 *  $RCSfile: ExceptionLogger.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// TagExceptionLogger.h: interface for the CTagExceptionLogger class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_TAGEXCEPTIONLOGGER_H__EE2DCA14_1802_49E2_980E_5CE93ABACD5C__INCLUDED_)
#define AFX_TAGEXCEPTIONLOGGER_H__EE2DCA14_1802_49E2_980E_5CE93ABACD5C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class ILogger;
class CAtfException;    
class CSystemException; 
class CAssertException; 
class CCfgException;
class CXmlLoadException;


class CExceptionLogger{
public:
    static void Log(ILogger &logger, const CAtfException &ex);
    static void Log(ILogger &logger, const CSystemException &ex);
    static void Log(ILogger &logger, const CAssertException &ex);
    static void Log(ILogger &logger, const CCfgException &ex);
    static void Log(ILogger &logger, const CXmlLoadException &ex);
};

#endif // !defined(AFX_TAGEXCEPTIONLOGGER_H__EE2DCA14_1802_49E2_980E_5CE93ABACD5C__INCLUDED_)
