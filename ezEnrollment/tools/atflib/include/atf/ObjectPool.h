/*
 *  $RCSfile: ObjectPool.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ObjectPool.h: interface for the CObjectPool class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_OBJECTPOOL_H__FB16424F_0CA2_460C_AE11_18BC1BA74564__INCLUDED_)
#define AFX_OBJECTPOOL_H__FB16424F_0CA2_460C_AE11_18BC1BA74564__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#pragma warning( disable : 4290 ) 

#include <atf/IPool.h>
#include <atf/SystemException.h>
#include <atf/IObjectFactory.h>

extern const size_t ATF_POOL_DEFAULT_MAX_SIZE;

class CPoolManager;

struct PoolEntry {
    void       *obj;
    PoolEntry  *next;
    DWORD       lastUsed;
    PoolEntry():obj(NULL), next(NULL), lastUsed(GetTickCount()){};
};

class CObjectPool : public IPool {
    friend CPoolManager;
public:
    CObjectPool(IObjectFactory & factory);
    virtual ~CObjectPool();

    void SetMaxSize(size_t maxSize){m_max=maxSize;};
    size_t GetMaxSize() const { return m_max; };
    void Initialize() throw (CSystemException) ;

public: 
    void* Get();
    void  Free(void* obj);
private:
    IObjectFactory& m_factory;
   
    HANDLE m_semaphore;
    HANDLE m_coreMutex;        // protect access to pool core 

    PoolEntry * m_free;       // free objects 
    PoolEntry * m_used;       // objects in use
    size_t m_allocated; 
    size_t m_max; 

};

#endif // !defined(AFX_OBJECTPOOL_H__FB16424F_0CA2_460C_AE11_18BC1BA74564__INCLUDED_)
