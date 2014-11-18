/*
 *  $RCSfile: PoolMap.cpp,v $
 *
 *  $Revision: 1.2 $
 *
 *  last change: $Date: 2003/09/05 07:18:17 $
 */
// PoolMap.cpp: implementation of the CCdPoolMap class.
//
//////////////////////////////////////////////////////////////////////

#include <atf/PoolMap.h>
#include <atf/IExecutorFactory.h>
//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CPoolMap::~CPoolMap() {

    CMap *current = m_content;

    while( NULL != current ) {
        m_content = current->m_next;
        delete current->m_pool;
        delete current->m_factory;
        delete current;
        current = m_content;
    }
};

void CPoolMap::Add(const char         * key, 
                     CExecutorPool    * pool, 
                     IExecutorFactory * factory) 
{
    CMap *map = new CMap(key, pool, factory);
    map->m_next = m_content;
    m_content = map;
};

CExecutorPool * CPoolMap::Get(const char * key) {
    CMap *current = m_content;
    while(NULL != current) {
        if ( 0 == current->m_key.CompareNoCase(key) ) {
            return current->m_pool;
        }
        current = current->m_next;
    }
    CString msg = "Pool [";
    msg += key;
    msg += "] not found";
    THROW_UNKNOWN_POOL_EXCEPTION(msg);
};
