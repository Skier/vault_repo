package com.themidnightcoders.fbplugin.wizard;


public class DatabaseDefaults 
{
    private String defaultHost;
    private String defaultPort;
    private String defaultUsername;
    private String defaultPassword;

    public DatabaseDefaults(String host, String port, String username, String password) {
        defaultHost = host;
        defaultPort = port;
        defaultUsername = username;
        defaultPassword = password;
    }

    public String getDefaultHost() {
        return defaultHost;
    }

    public String getDefaultPort() {
        return defaultPort;
    }

    public String getDefaultUsername() {
        return defaultUsername;
    }

    public String getDefaultPassword() {
        return defaultPassword;
    }

}