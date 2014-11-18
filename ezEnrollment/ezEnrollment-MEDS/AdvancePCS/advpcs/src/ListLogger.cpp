/*
 *  $RCSfile: ListLogger.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/03 15:19:46 $
 */

#include "wx/wxprec.h"

#ifdef __BORLANDC__
    #pragma hdrstop
#endif

#ifndef WX_PRECOMP
#include <wx/wx.h>
#endif

/* -------------------------- header place ---------------------------------- */
#include <wx/datetime.h>
#include <wx/listctrl.h>
#include <advpcs/ListLogger.h>
#include <atf/XmlStringWriter.h>
/* -------------------------- implementation place -------------------------- */
void ListLogger::Fatal(const char* file, long line, ATF_ERROR code, 
           const char* msg, size_t length, 
           unsigned char* dump) 
{
    Log(LOG_FATAL, file, line, code, msg, length, dump);                           
};

void ListLogger::Error(const char* file, long line, ATF_ERROR code, 
           const char* msg, size_t length, 
           const void* dump)
{
    Log(LOG_ERROR, file, line, code, msg, length, dump);                           
};

void ListLogger::Warn(const char* file, long line, ATF_ERROR code, 
          const char* msg, size_t length, 
          const void* dump)
{
    Log(LOG_WARN, file, line, code, msg, length, dump);                           
};

void ListLogger::Info(const char* file, long line, ATF_ERROR code, 
          const char* msg, size_t length, 
          const void* dump)
{
    Log(LOG_INFO, file, line, code, msg, length, dump);                           
};

void ListLogger::Debug(const char* file, long line, ATF_ERROR code, 
           const char* msg, size_t length, 
           const void* dump)
{
    Log(LOG_DEBUG, file, line, code, msg, length, dump);                           
};

void ListLogger::Dump(const char* file, long line, ATF_ERROR code, 
          const char* msg, size_t length, 
          const void* dump)
{
    Log(LOG_DUMP, file, line, code, msg, length, dump);                           
};

void ListLogger::DumpString(const char* file, long line, ATF_ERROR code, 
          const char* msg, CString dump)
{
    wxString message = wxString(msg)+ "\n Body:["+wxString(dump)+"]";
    Log(LOG_DUMP, file, line, code, message);
};

void ListLogger::DumpXML(const char* file, long line, ATF_ERROR code, 
          const char* msg, const CXmlNode& dump)
{
    CXmlStringWriter writer(true);
    CString str;
    writer.Write(str, dump);
    DumpString(file, line, code, msg, str);
};

void ListLogger::LogDump(unsigned char *dump, size_t len)
{
};

void ListLogger::Log(LOG_LEVEL level,const char* file, long line, ATF_ERROR code, 
                      const char* msg, size_t length, 
                      const void* dump)
{

    long item;
    long idx = GetList()->GetItemCount();
    
    wxString message(msg);
    item = GetList()->InsertItem(idx, wxString(LevelToPrefix(level)), 0 );
    GetList()->SetItemData(item, code);
    GetList()->SetItem(idx, 1, wxDateTime::Now().Format("%H:%M:%S"));
    switch(level) {
       case LOG_FATAL:
       case LOG_ERROR:
           m_logList->SetItemTextColour(item, *wxRED);
           break;
       case LOG_WARN:
           m_logList->SetItemTextColour(item, *wxBLUE);
           break;
       case LOG_NONE:
       case LOG_INFO: 
       case LOG_DEBUG:
       case LOG_DUMP:
           break;
       default:
           m_logList->SetItemTextColour(item, *wxRED);
           message.Printf("Unknown message level '%ld'", level);
    }
    GetList()->SetItem(idx, 2, message);
    GetList()->SetItemState(item, wxLIST_STATE_FOCUSED, wxLIST_STATE_FOCUSED);
	GetList()->EnsureVisible(item);

//    GetList()->SetItem(idx, 3, wxString::Format("%s:%ld", file, line);

    if ( NULL != dump && 0 != length) {
       LogDump((unsigned char *)dump, length); 
    }
};