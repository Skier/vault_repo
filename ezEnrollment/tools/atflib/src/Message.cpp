/*
 *  $RCSfile: Message.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// Message.cpp: implementation of the CMessage class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/Message.h>
#include <atf/SystemException.h>
#include <atf/Mutex.h>
#include <fstream>

using namespace std;

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

static const size_t DEFAULT_INCREMENT=1024;

HANDLE CMessage::m_mutex = CreateMutex(NULL, false, NULL);
size_t CMessage::m_currentId = 0;

CMessage::CMessage(CString fileName): 
    m_fileName(fileName),   m_length(0), 
    m_id(0),                m_body(NULL),
    m_isLoaded(false),      m_size(0)
{
    m_id = CMessage::GetNextId();
    m_transactionId.Format("%010lx", m_id);
};

CMessage::CMessage(const BYTE* body, size_t length): 
    m_length(length),      m_id(0),  
    m_body(NULL),          m_isLoaded(true), 
    m_size(length)
{
    m_id = CMessage::GetNextId();
    m_transactionId.Format("%010lx", m_id);

    m_body = (BYTE*)malloc(length);
    for(size_t i=0; i<m_length; i++) m_body[i] = body[i];
};

CMessage::~CMessage() {
   if ( m_body != NULL ) {
      free(m_body);
   }
};

void CMessage::Load() {
    if (!m_isLoaded) {
       ifstream is(m_fileName, ios::binary|ios::in);
       if (!is.is_open()) {
           THROW_SYSTEM_EXCEPTION(m_fileName);
       }

       char c;
       while(true) {
          is.get(c);
          if (is.eof()) {
              break;
          }
          if ( m_length == m_size ) {
              void * body = realloc((void*)m_body, m_size+DEFAULT_INCREMENT);
              if ( NULL == body ) {
                  THROW_SYSTEM_EXCEPTION("No memory");
              }
              m_body = (BYTE*)body;
              m_size += DEFAULT_INCREMENT;
          }
          m_body[m_length++] = c;
       }
       m_isLoaded = true;
    }
};

size_t CMessage::GetNextId() {
    CMutexLocker locker(CMessage::m_mutex);
    CMessage::m_currentId++;
    return CMessage::m_currentId;
};
