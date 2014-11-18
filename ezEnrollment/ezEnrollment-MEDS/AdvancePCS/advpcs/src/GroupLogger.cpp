/*
 *  $RCSfile: GroupLogger.cpp,v $
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
#include <advpcs/GroupLogger.h>
/* -------------------------- implementation place -------------------------- */
GroupLogger::~GroupLogger() {
    for (size_t i=0; i<m_group.size(); i++) {
        delete m_group[i];                           
    }
};


void GroupLogger::Fatal(const char* file, long line, ATF_ERROR code, 
           const char* msg, size_t length, 
           unsigned char* dump) 
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->Fatal(file, line, code, msg, length, dump);                           
    }
};

void GroupLogger::Error(const char* file, long line, ATF_ERROR code, 
           const char* msg, size_t length, 
           const void* dump)
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->Error(file, line, code, msg, length, dump);                           
    }
};

void GroupLogger::Warn(const char* file, long line, ATF_ERROR code, 
          const char* msg, size_t length, 
          const void* dump)
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->Warn(file, line, code, msg, length, dump);                           
    }
};

void GroupLogger::Info(const char* file, long line, ATF_ERROR code, 
          const char* msg, size_t length, 
          const void* dump)
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->Info(file, line, code, msg, length, dump);                           
    }
};

void GroupLogger::Debug(const char* file, long line, ATF_ERROR code, 
           const char* msg, size_t length, 
           const void* dump)
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->Debug(file, line, code, msg, length, dump);                           
    }
};

void GroupLogger::Dump(const char* file, long line, ATF_ERROR code, 
          const char* msg, size_t length, 
          const void* dump)
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->Dump(file, line, code, msg, length, dump);                           
    }
};

void GroupLogger::DumpString(const char* file, long line, ATF_ERROR code, 
          const char* msg, CString dump)
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->DumpString(file, line, code, msg, dump);                           
    }
};

void GroupLogger::DumpXML(const char* file, long line, ATF_ERROR code, 
          const char* msg, const CXmlNode& dump)
{
    for (size_t i=0; i<m_group.size(); i++) {
        m_group[i]->DumpXML(file, line, code, msg, dump);                           
    }
};

