/*
 *  $RCSfile: StreamLogger.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StreamLogger.h: interface for the CStreamLogger class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_STREAMLOGGER_H__111EBF56_BA86_4144_B638_C5BACA7773F5__INCLUDED_)
#define AFX_STREAMLOGGER_H__111EBF56_BA86_4144_B638_C5BACA7773F5__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/Logger.h>
#include <iostream>
#include <atf/Mutex.h>

using namespace std;

class CStreamLogger : public ILogger  
{
public:
    CStreamLogger(ostream &stream):m_stream(stream){
        m_mutex = CreateMutex(NULL, false, NULL);
    };
    virtual ~CStreamLogger() {
        CloseHandle(m_mutex);
    };

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
    void LogDump(unsigned char *dump, size_t len);
    void Log(LOG_LEVEL level,const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length = 0, 
                      const void* dump = NULL);
private:    
    ostream &m_stream;
    HANDLE  m_mutex;                      
};

#endif // !defined(AFX_STREAMLOGGER_H__111EBF56_BA86_4144_B638_C5BACA7773F5__INCLUDED_)
