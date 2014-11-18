package com.llsvc.server.framework;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import javax.persistence.EntityManagerFactory;

public class EntryPoint
    implements ServletContextListener
{
//    private final static String CONFIGURATION_KEY = "configuration";

    private static EntityManagerFactory emf = null;

    public static EntityManagerFactory getEMF() {
        return emf;
    }

    public void contextInitialized(ServletContextEvent event) {
/*
        String configuration = event.getServletContext().getInitParameter(EntryPoint.CONFIGURATION_KEY);
        System.out.println("EntryPoint: starting application with configuration file \"" + configuration + "\".");
        BootLoader loader = new BootLoader();
        loader.main(configuration);
*/
        String databaseConnection = Configuration.getInstance().getDatabaseConnection();
        String databaseUsername = Configuration.getInstance().getDatabaseUsername();
        String databasePassword = Configuration.getInstance().getDatabasePassword();

        emf = EntityManagerFactoryHelper.createEntityManagerFactory("llsvc-persistence", 
                databaseUsername, databasePassword, databaseConnection);
        System.out.println("EntryPoint: starting application ...");
        System.out.println("EntryPoint: database connection: " + databaseConnection);
        System.out.println("EntryPoint: application is started.");
    }

    public void contextDestroyed(ServletContextEvent event) {
        System.out.println("EntryPoint: application is stopped.");
    }

}

