package com.llsvc.server.framework;

import java.io.File;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.FileInputStream;
import java.io.DataInputStream;

import javax.servlet.ServletException;
import javax.servlet.ServletContext;
import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpSession;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.fileupload.FileItemIterator;
import org.apache.commons.fileupload.FileItemStream;
import org.apache.commons.fileupload.util.Streams;
import org.apache.commons.fileupload.servlet.ServletFileUpload;

import javax.persistence.Query;
import javax.persistence.NoResultException;

import com.llsvc.server.entity.DocumentAttachmentEntity;
import com.llsvc.server.framework.EntityManagerHolder;

public class Configuration
{

    private static Configuration instance = null;

    public static Configuration getInstance() {
        if ( null == instance ) {
            instance = new Configuration();
        }
        return instance;
    }

    private javax.naming.Context environment = null;

    private Configuration() {
        try {
            javax.naming.Context ctx = new javax.naming.InitialContext();
            environment = (javax.naming.Context) ctx.lookup("java:comp/env");
        } catch (javax.naming.NamingException ex) {
            throw new RuntimeException(ex);
        }
    }

    public String getDatabaseConnection() {
        return "jdbc:postgresql://" + getDatabaseHost() + ":" + getDatabasePort() + "/" + getDatabaseName();
    }

    public String getDatabaseHost() {
        return getEnvValue("databaseHost");
    }

    public String getDatabasePort() {
        return getEnvValue("databasePort");
    }

    public String getDatabaseSchema() {
        return getEnvValue("databaseSchema");
    }

    public String getDatabaseName() {
        return getEnvValue("databaseName");
    }

    public String getDatabaseUsername() {
        return getEnvValue("databaseUsername");
    }

    public String getDatabasePassword() {
        return getEnvValue("databasePassword");
    }

    public String getGeoserverUrl() {
        return getEnvValue("geoServerUrl");
    }

    public String getGeoserverUsername() {
        return getEnvValue("geoServerUsername");
    }

    public String getGeoserverPassword() {
        return getEnvValue("geoServerPassword");
    }

    public String getAttachmentsStoragePrefix() {
        return getEnvValue("attachmentsStorage");
    }

    public String getIndexesDirectory() {
        return getEnvValue("indexesDirectory");
    }

    public String getTemporaryDirectory() {
        return getEnvValue("temporaryDirectory");
    }

    public String getEnvValue(String key) {
        try {
            return (String) environment.lookup(key);
        } catch (javax.naming.NamingException ex) {
            throw new RuntimeException(ex);
        }
    }

}
