/*
 *  $RCSfile: StreamLogger.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// StreamLogger.cpp: implementation of the CStreamLogger class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/StreamLogger.h>
#include <atf/Mutex.h>
#include <atf/AssertException.h>
#include <atf/XmlStringWriter.h>

using namespace std;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

void CStreamLogger::Fatal(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length, 
                       unsigned char* dump) {
    Log(LOG_FATAL, file, line, code, msg, length, dump);                           
};

void CStreamLogger::Error(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length, 
                       const void* dump) {
    Log(LOG_ERROR, file, line, code, msg, length, dump);                           
};

void CStreamLogger::Warn(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) {
    Log(LOG_WARN, file, line, code, msg, length, dump);                           
};

void CStreamLogger::Info(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) {
    Log(LOG_INFO, file, line, code, msg, length, dump);                           
};

void CStreamLogger::Debug(const char* file, long line, ATF_ERROR code, 
                       const char* msg, size_t length, 
                       const void* dump) {
    Log(LOG_DEBUG, file, line, code, msg, length, dump);                           
};

void CStreamLogger::Dump(const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) {
    Log(LOG_DUMP, file, line, code, msg, length, dump);                           
};

void CStreamLogger::DumpString(const char* file, long line, ATF_ERROR code, 
                const char* msg, CString dump) 
{
    CString message = CString(msg)+ "\n Body:["+dump+"]";
    Log(LOG_DUMP, file, line, code, message);
};

void CStreamLogger::DumpXML(const char* file, long line, ATF_ERROR code, 
             const char* msg, const CXmlNode& dump) 
{
    CXmlStringWriter writer(true);
    CString str;
    writer.Write(str, dump);
    DumpString(file, line, code, msg, str);
};

void CStreamLogger::LogDump(unsigned char *dump, size_t len) {
    size_t current = 0;
    size_t i = 0;
    m_stream<<endl<<"- - - - - - - - - - - - - -  Dump data ["<<len<<"] - - - - - - - - - - "<<endl;
    while(current < len){
        char str[10];
        sprintf(str,"%08x ",current);
        m_stream <<str;
        for(i=0; i<16;i++){
            if(current+i<len) {
                sprintf(str,"%02x ",dump[current+i]);
                m_stream << str;
            } else {
                m_stream<<"   ";
            }
            if(7 == i){
                m_stream<<" ";
            }
        }
        for(i=0; i<16 && current+i<len;i++){
            if(dump[current+i] >= 0x20){
                m_stream<<dump[current+i];
            } else {
                m_stream<<".";
            }
        }
        current += i;
        m_stream<<endl;
    };
    m_stream<<"- - - - - - - - - - - - - - - - End of data - - - - - - - - - - - - - - - "<<endl;
}

void CStreamLogger::Log(LOG_LEVEL level,const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump) {
    SYSTEMTIME t;
    GetSystemTime(&t);
    CMutexLocker locker(m_mutex);                       
    char tmp[1024];
    sprintf(tmp,"%04d.%02d.%02d %02d:%02d:%02d %06ld ",t.wYear,t.wMonth,t.wDay,t.wHour,t.wMinute,t.wSecond, t.wMilliseconds);
    m_stream<<endl<<LevelToPrefix(level)<<tmp;
    m_stream<<" \""<<file<<"\" "<<line<<" "<<code<<endl<<"\t ["<<msg<<"]";
    if ( NULL != dump && 0 != length) {
       LogDump((unsigned char *)dump, length); 
    }
    m_stream << endl;

    m_stream.flush();
}; 


