/*
 *  $RCSfile: ExecutorFactoryFasade.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ExecutorFactoryFasade.h: interface for the CExecutorFactoryFasade class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CDEXECUTORFACTORYFASADE_H__2C62DD1D_1E1F_415E_A612_4DFEB5DE3A1A__INCLUDED_)
#define AFX_CDEXECUTORFACTORYFASADE_H__2C62DD1D_1E1F_415E_A612_4DFEB5DE3A1A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IObjectFactory.h>
#include <atf/IExecutor.h>
#include <atf/IExecutorFactory.h>

class CExecutorFactoryFasade : public IObjectFactory {
public:
    CExecutorFactoryFasade(IExecutorFactory & factory):
         m_factory(factory){};
public:
    void* Create(){ return m_factory.Create();};
    void  Destroy(void * obj) {
        IExecutor *exec = (IExecutor*)obj; 
        m_factory.Destroy(exec);
    };
private:
    IExecutorFactory &m_factory;
};

#endif // !defined(AFX_CDEXECUTORFACTORYFASADE_H__2C62DD1D_1E1F_415E_A612_4DFEB5DE3A1A__INCLUDED_)
