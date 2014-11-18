/*
 *  $RCSfile: EventLogger.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// EventLogger.h: interface for the CEventLogger class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_EVENTLOGGER_H__8DF1830E_CB93_4D80_A8B7_EC02578D8420__INCLUDED_)
#define AFX_EVENTLOGGER_H__8DF1830E_CB93_4D80_A8B7_EC02578D8420__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Logger.h>



class CEventLogger : public ILogger {
public:
    CEventLogger(const char* name);
    virtual ~CEventLogger();

    void Fatal(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       unsigned char* dump = NULL);
    void Error(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       const void* dump = NULL);
    void Warn(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
    void Info(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
    void Debug(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length = 0, 
                       const void* dump = NULL);
    void Dump(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
    void DumpString(const char* file, long line, ATF_ERROR code, 
                      const char* msg, CString dump);
    void DumpXML(const char* file, long line, ATF_ERROR code, 
                      const char* msg, const CXmlNode& dump);

private:
    void Log(LOG_LEVEL level,const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
private:
    HANDLE m_eventSource;
};

#endif // !defined(AFX_EVENTLOGGER_H__8DF1830E_CB93_4D80_A8B7_EC02578D8420__INCLUDED_)
