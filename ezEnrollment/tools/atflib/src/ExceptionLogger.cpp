/*
 *  $RCSfile: ExceptionLogger.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// TagExceptionLogger.cpp: implementation of the CTagExceptionLogger class.
//
//////////////////////////////////////////////////////////////////////
// #include <AFXWIN.H>
#include <atf/Exception.h>
#include <atf/SystemException.h>
#include <atf/XmlLoadException.h>
#include <atf/CfgException.h>
#include <atf/AssertException.h>
#include <atf/Logger.h>
#include <atf/ExceptionLogger.h>
#include <stdio.h>

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

void CExceptionLogger::Log(ILogger &logger, const CAtfException &ex) {
    CString err = "CAtfException:\'";
    err += ex.GetText();
    err += "\'";
    if( logger.IsErrorEnabled() ) { 
        logger.Error(ex.GetFileName(), ex.GetLine(), ex.GetCode(), err); 
    }
};

void CExceptionLogger::Log(ILogger &logger, const CSystemException &ex) {
    CString err = "CSystemException:\'";
    err += ex.GetText();
    err += "\'";
    if( logger.IsErrorEnabled() ) { 
        logger.Error(ex.GetFileName(), ex.GetLine(), ex.GetCode(), err); 
    }
};

void CExceptionLogger::Log(ILogger &logger, const CAssertException &ex) {
    CString err = "CAssertException:\'";
    err += ex.GetText();
    err += "\'";
    if( logger.IsErrorEnabled() ) { 
        logger.Error(ex.GetFileName(), ex.GetLine(), ex.GetCode(), err); 
    }
};

void CExceptionLogger::Log(ILogger &logger, const CCfgException &ex) {
    CString err = "CCfgException:\'";
    err += ex.GetText();
    err += "\'";
    if( logger.IsErrorEnabled() ) { 
        logger.Error(ex.GetFileName(), ex.GetLine(), ex.GetCode(), err); 
    }
};

void CExceptionLogger::Log(ILogger &logger, const CXmlLoadException &ex) {
    CString err = "CXmlLoadException:\'";
    err += ex.GetErrorString();
    err += "\'";
    if( logger.IsErrorEnabled() ) { 
        logger.Error("Unknown", ex.GetLineNumber(), ex.GetErrorCode(), err); 
    }
};

