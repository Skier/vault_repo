package com.llsvc.server.framework;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory; 

public class Logger
{
    private final static Logger instance = new Logger();

    public static Logger getInstance() {
        return instance;
    }

    private Log log = LogFactory.getLog(Logger.class);

    public Log getLog() {
        return log;
    }

}
