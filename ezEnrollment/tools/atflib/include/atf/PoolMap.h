/*
 *  $RCSfile: PoolMap.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CdPoolMap.h: interface for the CCdPoolMap class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDPOOLMAP_H__4D403D82_25E5_4A1A_B6CD_CA7D529A3D7F__INCLUDED_)
#define AFX_CDPOOLMAP_H__4D403D82_25E5_4A1A_B6CD_CA7D529A3D7F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <AFXWIN.H>
#include <atf/ExecutorPool.h>
#include <atf/ErrorConst.h>

class IExecutorFactory;

struct CMap {
    CString             m_key;
    CExecutorPool   * m_pool;
    CMap            * m_next;
    IExecutorFactory* m_factory;

    CMap(const char* key, CExecutorPool *pool, 
           IExecutorFactory* factory):
        m_key(key), m_pool(pool), m_factory(factory), m_next(NULL){};
};

class CUnknownPoolException : public CAtfException {
public:
    CUnknownPoolException(
        const char *fileName, 
        int lineNo, 
        const char *message):
         CAtfException(fileName, lineNo, ATF_NOTFOUND_ERR, message){};
};

class CPoolMap {
public:
    CPoolMap():m_content(NULL){};
    virtual ~CPoolMap();
public:
    void Add(const char* key, CExecutorPool * pool, IExecutorFactory * factory);
    CExecutorPool * Get(const char * key) 
        throw (CUnknownPoolException);
private:
    CMap * m_content;
};

#define THROW_UNKNOWN_POOL_EXCEPTION(message) \
     throw CUnknownPoolException(__FILE__, __LINE__, message)


#endif // !defined(AFX_CDPOOLMAP_H__4D403D82_25E5_4A1A_B6CD_CA7D529A3D7F__INCLUDED_)
