/*
 *  $RCSfile: FileLogger.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// FileLogger.h: interface for the CFileLogger class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_FILELOGGER_H__2B1DE35A_6CE0_461F_B845_363EDA6B6E6B__INCLUDED_)
#define AFX_FILELOGGER_H__2B1DE35A_6CE0_461F_B845_363EDA6B6E6B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/StreamLogger.h>
#include <iostream>

class CFileLogger : public CStreamLogger {
public:
    CFileLogger(ostream * os):CStreamLogger(*os),m_os(os){};
    virtual ~CFileLogger() {
        if ( NULL != m_os ) {
            delete m_os;
        }
    };
private:
    ostream *m_os;
};

#endif // !defined(AFX_FILELOGGER_H__2B1DE35A_6CE0_461F_B845_363EDA6B6E6B__INCLUDED_)
