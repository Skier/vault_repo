/*
 *  $RCSfile: Util.h,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2004/03/31 12:20:44 $
 */
#ifndef __TAG_UTIL_H__
#define __TAG_UTIL_H__

#include <stdlib.h>

wchar_t * CharToWchar(const char *str, wchar_t *wstr);
bool FileCheckDirExists(const char* dir_name);

bool TransIdCheck(const char* tranId, size_t len);
bool TransIdCheck(const wchar_t* tranId, size_t len);

class CWCharHolder {
public:
    CWCharHolder(wchar_t *body): m_body(body){};
    ~CWCharHolder() { 
        if ( NULL != m_body ) {
            delete []m_body;
        };
    };
private:
    wchar_t * m_body;
};

class CCharHolder {
public:
    CCharHolder(char* body): m_body(body){};
    ~CCharHolder() {
        if ( NULL != m_body ) {
            delete []m_body;
        }
    };
private:
    char* m_body;
};

#endif /* __TAG_UTIL_H__ */