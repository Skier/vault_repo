/*
 *  $RCSfile: ThreadPool.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ThreadPool.h: interface for the CThreadPool class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_THREADPOOL_H__11044740_B976_42EC_BA3C_0497E594D944__INCLUDED_)
#define AFX_THREADPOOL_H__11044740_B976_42EC_BA3C_0497E594D944__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/ObjectPool.h>
#include <atf/Thread.h>
#include <atf/IThreadFactory.h>
#include <atf/ThreadFactoryFasade.h>
#include <atf/IManageable.h>
#include <atf/PoolManager.h>

#pragma warning( disable : 4290 ) 

class CThreadPool : public IManageable {
public:
    CThreadPool(IThreadFactory &factory):m_fasade(factory), m_pool(m_fasade){};
    virtual ~CThreadPool(){};

    void SetMaxSize(size_t maxSize){m_pool.SetMaxSize(maxSize);};
    void Initialize() throw (CSystemException) { m_pool.Initialize(); };
    void SetManager(long interval, CPoolManager &manager) {
        manager.manage(interval, m_pool);
    };    

    CThread* Get(){ return (CThread*)m_pool.Get(); };
    void  Free(CThread* obj) { 
        m_pool.Free(obj); 
    };
private:
    CThreadFactoryFasade m_fasade;
    CObjectPool m_pool;
};

#endif // !defined(AFX_THREADPOOL_H__11044740_B976_42EC_BA3C_0497E594D944__INCLUDED_)
