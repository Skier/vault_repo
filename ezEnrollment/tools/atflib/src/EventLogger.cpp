/*
 *  $RCSfile: EventLogger.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// EventLogger.cpp: implementation of the CEventLogger class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/EventLogger.h>
#include <atf/XmlStringWriter.h>


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CEventLogger::CEventLogger(const char* name):m_eventSource(NULL) {
    m_eventSource = RegisterEventSource(NULL, name);
};

CEventLogger::~CEventLogger() {
    if ( NULL != m_eventSource ) {
        DeregisterEventSource(m_eventSource);
        m_eventSource = NULL;
    }
};


void CEventLogger::Fatal(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length, 
                       unsigned char* dump) 
{
    Log(LOG_FATAL, file, line, code, msg, length, dump);                           
};

void CEventLogger::Error(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length, 
                       const void* dump) 
{
    Log(LOG_ERROR, file, line, code, msg, length, dump);                           
};

void CEventLogger::Warn(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) 
{
    Log(LOG_WARN, file, line, code, msg, length, dump);                           
};

void CEventLogger::Info(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) 
{
    Log(LOG_INFO, file, line, code, msg, length, dump);                           
};

void CEventLogger::Debug(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length, 
                       const void* dump) 
{
    Log(LOG_DEBUG, file, line, code, msg, length, dump);                           
};

void CEventLogger::Dump(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) 
{
    Log(LOG_DUMP, file, line, code, msg, length, dump);                           
};

void CEventLogger::DumpString(const char* file, long line, ATF_ERROR code, 
                      const char* msg, CString dump)
{
    CString message = CString(msg) + "\n Body:["+dump+"]";
    Log(LOG_DUMP, file, line, code, message);
};

void CEventLogger::DumpXML(const char* file, long line, ATF_ERROR code, 
                      const char* msg, const CXmlNode& dump)
{
    CXmlStringWriter writer(true);
    CString str;
    writer.Write(str, dump);
    DumpString(file, line, code, msg, str);
};


void CEventLogger::Log(LOG_LEVEL level,const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) 
{
    if ( NULL != m_eventSource ) {
        char strLine[10];
        char* msgs[1];
        ltoa(line, strLine, 10);
        CString m = LevelToPrefix(level);
        m += " \"";
        m += file;
        m += "\" ";
        m += strLine;
        m += " ";
        ltoa(code, strLine, 10);
        m += strLine;
        m += " [";
        m += msg;
        m += "]";
        msgs[0] = (char*)((const char *)m);
        ReportEvent(m_eventSource, 
                    level == LOG_FATAL || level == LOG_ERROR?EVENTLOG_ERROR_TYPE:EVENTLOG_INFORMATION_TYPE, 
                    0, code, 0, 1, length, (const char**)msgs, (void *)dump);
    }
}; 
