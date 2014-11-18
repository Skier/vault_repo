package com.themidnightcoders;

/**

 */
public interface ServiceSoap {


    /**

     */
    void Ping();

 
    /**
     * Asynchronous 'begin' version of operation Ping
     */
    org.systinet.wasp.async.AsyncConversation beginPing();

    /**
     * Asynchronous 'end' version of operation Ping
     */
    void endPing(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    com.themidnightcoders.DatabaseInfoType[] GetSupportedDatabaseTypes();

 
    /**
     * Asynchronous 'begin' version of operation GetSupportedDatabaseTypes
     */
    org.systinet.wasp.async.AsyncConversation beginGetSupportedDatabaseTypes();

    /**
     * Asynchronous 'end' version of operation GetSupportedDatabaseTypes
     */
    com.themidnightcoders.DatabaseInfoType[] endGetSupportedDatabaseTypes(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    java.lang.String CheckDatabaseHost(com.themidnightcoders.DatabaseInfoType type, java.lang.String hostname, java.lang.String port, java.lang.String userid, java.lang.String password);

 
    /**
     * Asynchronous 'begin' version of operation CheckDatabaseHost
     */
    org.systinet.wasp.async.AsyncConversation beginCheckDatabaseHost(com.themidnightcoders.DatabaseInfoType type, java.lang.String hostname, java.lang.String port, java.lang.String userid, java.lang.String password);

    /**
     * Asynchronous 'end' version of operation CheckDatabaseHost
     */
    java.lang.String endCheckDatabaseHost(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    com.themidnightcoders.DatabaseInfo AddDatabaseHost(com.themidnightcoders.DatabaseInfoType type, java.lang.String hostname, java.lang.String port, java.lang.String userid, java.lang.String password);

 
    /**
     * Asynchronous 'begin' version of operation AddDatabaseHost
     */
    org.systinet.wasp.async.AsyncConversation beginAddDatabaseHost(com.themidnightcoders.DatabaseInfoType type, java.lang.String hostname, java.lang.String port, java.lang.String userid, java.lang.String password);

    /**
     * Asynchronous 'end' version of operation AddDatabaseHost
     */
    com.themidnightcoders.DatabaseInfo endAddDatabaseHost(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    com.themidnightcoders.DatabaseInfo[] GetDatabaseHosts();

 
    /**
     * Asynchronous 'begin' version of operation GetDatabaseHosts
     */
    org.systinet.wasp.async.AsyncConversation beginGetDatabaseHosts();

    /**
     * Asynchronous 'end' version of operation GetDatabaseHosts
     */
    com.themidnightcoders.DatabaseInfo[] endGetDatabaseHosts(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    java.lang.String[] GetDatabases(java.lang.String hostId);

 
    /**
     * Asynchronous 'begin' version of operation GetDatabases
     */
    org.systinet.wasp.async.AsyncConversation beginGetDatabases(java.lang.String hostId);

    /**
     * Asynchronous 'end' version of operation GetDatabases
     */
    java.lang.String[] endGetDatabases(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    com.themidnightcoders.QueryResult TestQuery(java.lang.String hostId, java.lang.String database, java.lang.String query);

 
    /**
     * Asynchronous 'begin' version of operation TestQuery
     */
    org.systinet.wasp.async.AsyncConversation beginTestQuery(java.lang.String hostId, java.lang.String database, java.lang.String query);

    /**
     * Asynchronous 'end' version of operation TestQuery
     */
    com.themidnightcoders.QueryResult endTestQuery(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    java.lang.String[] GetTables(java.lang.String hostId, java.lang.String database);

 
    /**
     * Asynchronous 'begin' version of operation GetTables
     */
    org.systinet.wasp.async.AsyncConversation beginGetTables(java.lang.String hostId, java.lang.String database);

    /**
     * Asynchronous 'end' version of operation GetTables
     */
    java.lang.String[] endGetTables(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    com.themidnightcoders.ColumnInfo[] GetColumns(java.lang.String hostId, java.lang.String database, java.lang.String table);

 
    /**
     * Asynchronous 'begin' version of operation GetColumns
     */
    org.systinet.wasp.async.AsyncConversation beginGetColumns(java.lang.String hostId, java.lang.String database, java.lang.String table);

    /**
     * Asynchronous 'end' version of operation GetColumns
     */
    com.themidnightcoders.ColumnInfo[] endGetColumns(org.systinet.wasp.async.AsyncConversation conversation);


    /**

     */
    void GenerateSourceCode(java.lang.String hostId, java.lang.String database, java.lang.String table, java.lang.String query);

 
    /**
     * Asynchronous 'begin' version of operation GenerateSourceCode
     */
    org.systinet.wasp.async.AsyncConversation beginGenerateSourceCode(java.lang.String hostId, java.lang.String database, java.lang.String table, java.lang.String query);

    /**
     * Asynchronous 'end' version of operation GenerateSourceCode
     */
    void endGenerateSourceCode(org.systinet.wasp.async.AsyncConversation conversation);



}

/*
 * Generated by Systinet WSDL2Java
 * http://www.systinet.com
 */

