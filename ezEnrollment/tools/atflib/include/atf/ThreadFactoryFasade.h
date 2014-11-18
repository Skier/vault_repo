/*
 *  $RCSfile: ThreadFactoryFasade.h,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// ThreadFactoryFasade.h: interface for the CThreadFactoryFasade class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_THREADFACTORYFASADE_H__C546EC13_5994_4EDD_A801_31D0242ADB67__INCLUDED_)
#define AFX_THREADFACTORYFASADE_H__C546EC13_5994_4EDD_A801_31D0242ADB67__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include <atf/IObjectFactory.h>
#include <atf/IThreadFactory.h>
#include <atf/Thread.h>

class CThreadFactoryFasade : public IObjectFactory {
public:
    CThreadFactoryFasade(IThreadFactory &factory): m_factory(factory){};
    virtual ~CThreadFactoryFasade(){};
    void* Create() { CThread *thread = m_factory.Create(); thread->Create(); return thread;};
    void  Destroy(void *obj){ m_factory.Destroy((CThread*)obj); };
public:
    IThreadFactory & m_factory;
};

#endif // !defined(AFX_THREADFACTORYFASADE_H__C546EC13_5994_4EDD_A801_31D0242ADB67__INCLUDED_)
