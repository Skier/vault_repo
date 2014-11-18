package com.themidnightcoders.fbplugin.service;

import java.util.StringTokenizer;

public class TableLocation {

    private final static String SEPARATOR = "/";

    public String instanceId;
    public String database;
    public String tablename;

    public TableLocation(String instanceId, String database, String tablename) {
        this.instanceId = instanceId;
        this.database = database;
        this.tablename = tablename;
    }

    public TableLocation(String stringifiedTableLocation) {
        StringTokenizer tk = new StringTokenizer(stringifiedTableLocation, TableLocation.SEPARATOR);
        try {
            instanceId = tk.nextToken();
            database = tk.nextToken();
            tablename = tk.nextToken();
        } catch (Exception ex) {
            throw new RuntimeException(ex);
        }
    }

    public String toString() {
        return instanceId 
            + TableLocation.SEPARATOR
            + database
            + TableLocation.SEPARATOR
            + tablename;
    }
}

