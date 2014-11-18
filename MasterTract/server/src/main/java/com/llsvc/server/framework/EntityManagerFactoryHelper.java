package com.llsvc.server.framework;

import java.io.IOException;
import java.io.InputStream;

import java.util.Map;
import java.util.HashMap;
import java.util.Properties;
import java.util.Enumeration;

import javax.persistence.Persistence;
import javax.persistence.EntityManagerFactory;

public class EntityManagerFactoryHelper
{

    public static final String CONN_USERNAME="hibernate.connection.username";
    public static final String CONN_PASSWORD="hibernate.connection.password";
    public static final String CONN_URL="hibernate.connection.url";

    public static final String TRANSACTION_TYPE="javax.persistence.transactionType";
    public static final String JTA_DATASOURCE="javax.persistence.jtaDataSource";

    public static final String JTA_TRANSACTION_TYPE="JTA";

    public static EntityManagerFactory createEntityManagerFactory(String moduleName, Properties properties) {
        Map<String, String> config = new HashMap<String, String>();

        String transactionType = properties.getProperty(EntityManagerFactoryHelper.TRANSACTION_TYPE);
        if ( null != transactionType 
                && EntityManagerFactoryHelper.JTA_TRANSACTION_TYPE.equals(transactionType) ) {
            String jtaDataSource = properties.getProperty(EntityManagerFactoryHelper.JTA_DATASOURCE);
            if ( null == jtaDataSource ) {
                throw new IllegalArgumentException(EntityManagerFactoryHelper.JTA_DATASOURCE + " is not supplied.");
            }
            Enumeration names = properties.propertyNames();
            while ( names.hasMoreElements() ) {
                String key = (String) names.nextElement();
                config.put(key, properties.getProperty(key));
            }
/*
            config.put(EntityManagerFactoryHelper.TRANSACTION_TYPE, transactionType);
            config.put(EntityManagerFactoryHelper.JTA_DATASOURCE, jtaDataSource);
            config.put("hibernate.transaction.factory_class", "org.hibernate.ejb.transaction.JoinableCMTTransactionFactory");            
            config.put("hibernate.transaction.manager_lookup_class", "org.hibernate.transaction.JOTMTransactionManagerLookup");
*/
        } else {
            String username = properties.getProperty(EntityManagerFactoryHelper.CONN_USERNAME);
            if ( null == username ) {
                throw new IllegalArgumentException(EntityManagerFactoryHelper.CONN_USERNAME + " is not supplied.");
            }
            String password = properties.getProperty(EntityManagerFactoryHelper.CONN_PASSWORD);
            if ( null == password ) {
                throw new IllegalArgumentException(EntityManagerFactoryHelper.CONN_PASSWORD + " is not supplied.");
            }
            String url = properties.getProperty(EntityManagerFactoryHelper.CONN_URL);
            if ( null == url ) {
                throw new IllegalArgumentException(EntityManagerFactoryHelper.CONN_URL + " is not supplied.");
            }

            config.put(EntityManagerFactoryHelper.CONN_USERNAME, username);
            config.put(EntityManagerFactoryHelper.CONN_PASSWORD, password);
            config.put(EntityManagerFactoryHelper.CONN_URL, url);
        }
/*
        config.put("hibernate.search.default.directory_provider", "org.hibernate.search.store.FSDirectoryProvider");
        config.put("hibernate.search.default.indexBase", "c:/indexes");
*/
        return Persistence.createEntityManagerFactory(moduleName, config);
    }


    public static EntityManagerFactory createEntityManagerFactory(String moduleName, String username, String password, String url) {
        Properties properties = new Properties();
        properties.setProperty(EntityManagerFactoryHelper.CONN_USERNAME, username);
        properties.setProperty(EntityManagerFactoryHelper.CONN_PASSWORD, password);
        properties.setProperty(EntityManagerFactoryHelper.CONN_URL, url);

        return EntityManagerFactoryHelper.createEntityManagerFactory(moduleName, properties);
    }

    public static EntityManagerFactory createEntityManagerFactory(String moduleName, InputStream propertiesStream) {
        Properties properties = new Properties();
        try {
            properties.load(propertiesStream);
        } catch (IOException ex) {
            throw new RuntimeException("Cannot load properties from stream.");
        }

        return EntityManagerFactoryHelper.createEntityManagerFactory(moduleName, properties);
    }

}
