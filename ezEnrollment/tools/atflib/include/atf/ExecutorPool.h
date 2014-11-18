/*
 *  $RCSfile: ExecutorPool.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// CdExecutorPool.h: interface for the CCdExecutorPool class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDEXECUTORPOOL_H__079D66E0_3B7D_4054_AEF7_B5AD4AD72BFD__INCLUDED_)
#define AFX_CDEXECUTORPOOL_H__079D66E0_3B7D_4054_AEF7_B5AD4AD72BFD__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/ObjectPool.h>
#include <atf/IExecutorFactory.h>
#include <atf/ExecutorFactoryFasade.h>

class CExecutorPool {
public:
    CExecutorPool(IExecutorFactory &factory):
        m_fasade(factory), m_pool(m_fasade){};
        virtual ~CExecutorPool(){};

    void SetMaxSize(size_t maxSize){m_pool.SetMaxSize(maxSize);};
    size_t GetMaxSize() const { return m_pool.GetMaxSize(); };
    void Initialize() throw (CSystemException) { m_pool.Initialize(); };

    IExecutor* Get() { return (IExecutor*)m_pool.Get();};
    void Free(IExecutor* obj) {
        obj->Recycle();
        m_pool.Free(obj);
    };
private: 
    CExecutorFactoryFasade m_fasade;
    CObjectPool m_pool;

};

#endif // !defined(AFX_CDEXECUTORPOOL_H__079D66E0_3B7D_4054_AEF7_B5AD4AD72BFD__INCLUDED_)
