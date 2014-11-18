/**
 * $Id: LogProcessor.java 240 2007-06-01 15:04:17Z moritur $
 *
 */
package com.affilia.cargo.test;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.ArrayList;

import util.testhelper.EntityProcessor;
import util.testhelper.Sequence;

import com.logicland.application.core.util.DateHelper;;

public class LogProcessor extends EntityProcessor {
	
	private static final String TABLE_NAME = "CARGO_LOG";
    private static final String SEQUENCE_NAME = "CARGO_LOG_SQC";
    
    private static final String COLUMN_ID = "ID";
    private static final String COLUMN_NETWORK_ID = "NETWORK_ID";
    private static final String COLUMN_VEHICLE_ID = "VEHICLE_ID";
    private static final String COLUMN_LOG_TIME = "LOG_TIME";
    private static final String COLUMN_LOG_LEVEL = "LOG_LEVEL";
    private static final String COLUMN_LOG_TEXT = "LOG_TEXT";
    
    private static final String SQL_GET_ALL =
        "select " + LogProcessor.COLUMN_ID + ", " +
        			LogProcessor.COLUMN_NETWORK_ID + ", " +
        			LogProcessor.COLUMN_VEHICLE_ID + ", " +
        			LogProcessor.COLUMN_LOG_TIME + ", " +
        			LogProcessor.COLUMN_LOG_LEVEL + ", " +
        			LogProcessor.COLUMN_LOG_TEXT + 
        " from " + LogProcessor.TABLE_NAME;

    private static final String SQL_GET_ONE = LogProcessor.SQL_GET_ALL  
    	+ " where " + LogProcessor.COLUMN_ID + " = ?";

    private static final String SQL_CLEANUP =
    	"delete from " + LogProcessor.TABLE_NAME;

    private static final String SQL_FILLUP =
        "insert into " + LogProcessor.TABLE_NAME + " (" +
        				 LogProcessor.COLUMN_ID + ", " +
        				 LogProcessor.COLUMN_NETWORK_ID + ", " +
        				 LogProcessor.COLUMN_VEHICLE_ID + ", " +
        				 LogProcessor.COLUMN_LOG_TIME + ", " +
        				 LogProcessor.COLUMN_LOG_LEVEL + ", " +
        				 LogProcessor.COLUMN_LOG_TEXT + ") " +
        " values (?, ?, ?, ?, ?, ?)";
    
    public static final Timestamp LOG_TIME = new Timestamp(DateHelper.getDate(2007, 6, 2).getTime());
    public static final Integer LOG_LEVEL = new Integer(4);
    public static final String LOG_TEXT = "CARGO MOVE TO POINT B";
    
    private static Integer c_id;
    public Integer networkId;
    public Integer vehicleId;
    public Timestamp logTime;
    public Integer logLevel;
    public String logText;
    
    private static LogProcessor c_processor;
    
    private LogProcessor() {
	   
    }
   
    public void setStartFields() {
    	this.logTime = LogProcessor.LOG_TIME;
    	this.logLevel = LogProcessor.LOG_LEVEL;
    	this.logText = LogProcessor.LOG_TEXT;
    }
   
    public static LogProcessor getProcessor() {
    	if (c_processor == null) {
    		c_processor = new LogProcessor();
    	}
    	return c_processor;
    }

    public String getAllQuery() {
    	return LogProcessor.SQL_GET_ALL;
    }

    public String getCleanupQuery() {
    	return LogProcessor.SQL_CLEANUP;
    }

    public String getFillupQuery() {
    	return LogProcessor.SQL_FILLUP;
    }

    public String getOneQuery() {
    	return LogProcessor.SQL_GET_ONE;
    }


    public Object[] getValues() {
    	c_id = Sequence.getSequence(LogProcessor.SEQUENCE_NAME);
    	return new Object[] {c_id,
    			networkId,
    			vehicleId,
    			logTime,
    			logLevel,
    			logText};
    }

    public Object[] process(ResultSet rs) throws SQLException {
    	ArrayList<LogMockObject> result = new ArrayList<LogMockObject>();
    	while ( rs.next() ) {
    		LogMockObject logMockObject = new LogMockObject();

    		logMockObject.networkId = rs.getInt(LogProcessor.COLUMN_NETWORK_ID);
    		logMockObject.vehicleId = rs.getInt(LogProcessor.COLUMN_VEHICLE_ID);
    		logMockObject.logTime = rs.getTimestamp(LogProcessor.COLUMN_LOG_TIME);
    		logMockObject.logLevel = rs.getInt(LogProcessor.COLUMN_LOG_LEVEL);
    		logMockObject.logText = rs.getString(LogProcessor.COLUMN_LOG_TEXT);

    		result.add(logMockObject);

    	}
    	return result.toArray(new LogMockObject[result.size()]);
    }

    public Integer getId() {
    	return c_id;
    }

    public Integer makeLog(Integer networkId, 
    		Integer vehicleId, 
    		Timestamp logTime,
    		Integer logLevel,
    		String logText) 
    {
    	this.networkId = networkId;
    	this.vehicleId = vehicleId;
    	this.logTime = logTime;
    	this.logLevel = logLevel;
    	this.logText = logText;

    	fillup();

    	return c_id;
    }
}
