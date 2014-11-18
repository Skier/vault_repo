package com.llsvc.server.framework;

import java.util.Hashtable;
import javax.persistence.EntityManager;

import com.llsvc.server.framework.Logger;

public class EntityManagerHolder
{
    private final static EntityManagerHolder instance = new EntityManagerHolder();

    public static EntityManagerHolder getInstance() {
        return instance;
    }

/*
    private final ThreadLocal<Hashtable<String, EntityManager>> entityManagers =
        new ThreadLocal<Hashtable<String, EntityManager>>();
*/
    private final ThreadLocal<EntityManager> emHolder =
            new ThreadLocal<EntityManager>();

    private EntityManagerHolder() {
/*
        entityManagers.set(new Hashtable<String, EntityManager>());
*/
    }
    
    public EntityManager getEM() {
        return emHolder.get();
    }

    public void setEM(EntityManager em) {
        Logger.getInstance().getLog().debug("EntityManagerHolder.setEM: em=" + em);
        emHolder.set(em);
    }
}
