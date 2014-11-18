/*
 *  $RCSfile: Util.cpp,v $
 *
 *  $Revision: 1.3 $
 *
 *  last change: $Date: 2004/03/31 12:20:44 $
 */
#include <atf/Util.h>
#include <atf/SystemException.h>
#include <string.h>

wchar_t* CharToWchar(const char *str, wchar_t * wstr) {
    char * result_t = (char*)wstr;
    size_t i = 0;
    for ( i=0; str[i]!=0; i++ ) {
        result_t[i*sizeof(wchar_t)] = str[i];  
        result_t[i*sizeof(wchar_t)+1] = 0;  
    }
    wstr[i] = 0;
    return wstr;
};


bool FileCheckDirExists(const char* dir_name) {
    DWORD attr = GetFileAttributes(dir_name);
    if ( 0xFFFFFFFF == attr ) {
        return false;
    }
    if(0 == (attr & FILE_ATTRIBUTE_DIRECTORY)) {
        return false;
    };
    return true;
};

#define MAX_DWORD_STRING_LEN 10

bool TransIdCheck(const char* tranId, size_t len) {
    for (int i=0; 0!=tranId[i] && i<len; i++) {
        if ( i >= MAX_DWORD_STRING_LEN ) {
            return false;
        }
        if( tranId[i] < '0' || tranId[i] > '9' ) {
            return false;
        }
    }
    return true;
};

bool TransIdCheck(const wchar_t* tranId, size_t len) {
    wchar_t minDigit = '0';
    wchar_t maxDigit = '9';

    for (int i=0; 0!=tranId[i] && i<len; i++) {
        if ( i >= MAX_DWORD_STRING_LEN ) {
            return false;
        }
        if( tranId[i] < minDigit || tranId[i] > maxDigit ) {
            return false;
        }
    }
    return true;
};
