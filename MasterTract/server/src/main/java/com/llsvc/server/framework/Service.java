package com.llsvc.server.framework;

import java.util.List;

import flex.messaging.FlexContext;
import javax.servlet.http.HttpSession;
import javax.servlet.http.HttpServletRequest;

import javax.persistence.Query;
import javax.persistence.NoResultException;
import javax.persistence.EntityTransaction;

import org.hibernate.search.jpa.Search;
import org.hibernate.search.jpa.FullTextEntityManager;

import com.llsvc.server.entity.ModuleTypeEntity;
import com.llsvc.server.entity.ModuleEntity;
import com.llsvc.server.entity.RoleEntity;
import com.llsvc.server.entity.UserEntity;
import com.llsvc.server.entity.UserOptionEntity;
import com.llsvc.server.entity.PersonEntity;
import com.llsvc.server.entity.ClientEntity;

import com.llsvc.server.framework.data.DesktopPackage;


public class Service
{

    public static UserEntity getAuthorizedUser() {
        HttpServletRequest request = FlexContext.getHttpRequest();
        HttpSession session = request.getSession(true);
        UserEntity result = (UserEntity) session.getAttribute("authorizedUser");
        if ( null == result ) {
            throw new RuntimeException("not authorized.");
        }
        return result;
    }

    public String getGeoServerUrl() {
        try {
            javax.naming.Context ctx = new javax.naming.InitialContext();
            javax.naming.Context env = (javax.naming.Context) ctx.lookup("java:comp/env");
            return (String) env.lookup("geoServerUrl");
        } catch (javax.naming.NamingException ex) {
            throw new RuntimeException(ex);
        }
/*
        HttpServletRequest request = FlexContext.getHttpRequest();
        String schema = request.getScheme();
        String server = request.getServerName();
        int port = request.getServerPort();
        String context = request.getContextPath();
        return schema + "://" + server + ":" + port + "/geoserver/";
*/
    }

    public UserEntity login(String login, String password) 
        throws Exception
    {
        UserEntity user = findUserByLogin(login.toLowerCase());
        if ( null != user ) {
            if ( password.equals(user.password) && user.isActive ) {
                if ( null != user.client && user.client.isActive ) {
                    HttpServletRequest request = FlexContext.getHttpRequest();
                    HttpSession session = request.getSession(true);
                    session.setAttribute("authorized", "yes");
                    session.setAttribute("authorizedUser", user);
                    return user;
                } else {
                    throw new Exception("No user/client account found please register.");
                }
            } else {
                throw new Exception("No user account found please register.");
            }
        } else {
            throw new Exception("No user account found please register.");
        }
    }

    public UserEntity register(PersonEntity person, String login, String password)
        throws Exception
    {
        UserEntity result = null;

        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            result = createUser(person, login, password, 0, false, false, false, null);
        } catch (Exception ex) {
            tr.rollback();
            throw ex;
        }

        tr.commit();

        return result;
    }

    public DesktopPackage getPackage(UserEntity user) {
        user.roleList = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                RoleEntity.FIND_ALL).getResultList();

        DesktopPackage pkg = new DesktopPackage();
        pkg.user = user;
        pkg.moduleList = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                ModuleEntity.FIND_ALL).getResultList();

        return pkg;
    }

    public List<UserOptionEntity> getUserOptions(Integer userId) {
        Query query =  EntityManagerHolder.getInstance().getEM().createNamedQuery(
                UserOptionEntity.FIND_BY_USER);
        query.setParameter("userId", userId);
        return query.getResultList();
    }

    public void storeUserOption(UserOptionEntity userOption) {
        if ( 0 == userOption.id ) {
            userOption.id = null;
            EntityManagerHolder.getInstance().getEM().persist(userOption);
        } else {
            EntityManagerHolder.getInstance().getEM().merge(userOption);
        }
    }

    public List<UserEntity> getUsers() {
        Query query =  EntityManagerHolder.getInstance().getEM().createNamedQuery(
                UserEntity.FIND_ALL);
        return query.getResultList();
    }

    public UserEntity storeUser(UserEntity user) {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            if ( 0 == user.id ) {
                user = createUser(user.personal, user.login, user.password, 0, user.isActive.booleanValue(),
                        user.isAdmin.booleanValue(), user.isProjectManager.booleanValue(), user.client);
            } else {
                EntityManagerHolder.getInstance().getEM().merge(user.personal);
                EntityManagerHolder.getInstance().getEM().merge(user);
            }
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("Service.storeUser: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("Service.storeUser: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return (UserEntity) EntityManagerHolder.getInstance().getEM().find(UserEntity.class, user.id);        
    }

    public List<ClientEntity> getClients() {
        Query query =  EntityManagerHolder.getInstance().getEM().createNamedQuery(
                ClientEntity.FIND_ALL);
        return query.getResultList();
    }

    public ClientEntity storeClient(ClientEntity client) {
        EntityTransaction tr = EntityManagerHolder.getInstance().getEM().getTransaction();
        tr.begin();

        try {
            if ( 0 == client.id ) {
                client.id = null;
                EntityManagerHolder.getInstance().getEM().persist(client);
            } else {
                EntityManagerHolder.getInstance().getEM().merge(client);
            }
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("Service.storeClient: ", ex);
            tr.rollback();
            throw new RuntimeException(ex);
        }
        
        try {
            tr.commit();
        } catch (Throwable ex) {
            Logger.getInstance().getLog().error("Service.storeClient: commit failed ", ex);
            throw new RuntimeException(ex);
        }

        return (ClientEntity) EntityManagerHolder.getInstance().getEM().find(ClientEntity.class, client.id);        
    }

    private UserEntity createUser(PersonEntity person, String login, String password, int hackAttempts, boolean isActive, boolean isAdmin, boolean isPM, ClientEntity client)
        throws Exception
    {
        UserEntity checkLogin = findUserByLogin(login);
        if ( null != checkLogin ) {
            throw new Exception("User with login " + login + " already exists");
        }

        UserEntity user = new UserEntity();
        user.login = login;
        user.password = password;
        user.hackAttempts = hackAttempts;
        user.isActive = new Boolean(isActive);

        person.id = null;
        EntityManagerHolder.getInstance().getEM().persist(person);
        user.personal = person;   
        user.client = client;
        user.isAdmin = new Boolean(isAdmin);
        user.isProjectManager = new Boolean(isPM);
        EntityManagerHolder.getInstance().getEM().persist(user);

/*
            UserRole userRole = new UserRole();
            userRole.UserId = user.UserId;
            userRole.RoleId = UserRole.INITIAL_USER_ROLE;
            UserRole.Insert(userRole);

            UserPreference up = new UserPreference();
            up.UserId = user.UserId;
            up.DefaultSite = User.DEFAULT_SITE;
            up.NewTracts = User.NEW_TRACTS;
            UserPreference.Insert(up);
*/
/*        
        FullTextEntityManager fullTextEntityManager = Search.createFullTextEntityManager(
                EntityManagerHolder.getInstance().getEM());
        List<UserEntity> us = EntityManagerHolder.getInstance().getEM().createQuery(
                "select u from UserEntity as u").getResultList();
        for (UserEntity u : us) {
            fullTextEntityManager.index(u);
        } 
*/
        return user;
    }

    private UserEntity findUserByLogin(String login) {
        Query query = EntityManagerHolder.getInstance().getEM().createNamedQuery(
                UserEntity.FIND_BY_LOGIN);
        query.setParameter("login", login);
        try {
            return (UserEntity) query.getSingleResult();
        } catch (NoResultException ex) {
            return null;
        }
    }


}
