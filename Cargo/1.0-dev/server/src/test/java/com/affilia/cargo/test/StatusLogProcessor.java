/**
 * $Id: StatusLogProcessor.java 195 2007-05-21 11:59:21Z moritur $
 */

package com.affilia.cargo.test;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import util.testhelper.EntityProcessor;

public class StatusLogProcessor extends EntityProcessor {
	
    private static final String TABLE_NAME = "CARGO_STATUS_LOG";

    private static final String COLUMN_ID = "ID";
    private static final String COLUMN_LATITUDE = "LATITUDE";
    private static final String COLUMN_LONGITUDE = "LONGITUDE";
    private static final String COLUMN_STATUS = "STATUS";
    private static final String COLUMN_TEMPERATURE = "TEMPERATURE";
    private static final String COLUMN_HUMIDITY = "HUMIDITY";

    private static final String SQL_GET_ALL =
        "select " + StatusLogProcessor.COLUMN_ID + ", " +
        			StatusLogProcessor.COLUMN_LATITUDE + ", " +
        			StatusLogProcessor.COLUMN_LONGITUDE + ", " +
        			StatusLogProcessor.COLUMN_STATUS + ", " +
        			StatusLogProcessor.COLUMN_TEMPERATURE + ", " +
        			StatusLogProcessor.COLUMN_HUMIDITY + 
        " from " + StatusLogProcessor.TABLE_NAME;

    private static final String SQL_GET_ONE = StatusLogProcessor.SQL_GET_ALL  
        + " where " + StatusLogProcessor.COLUMN_ID + " = ?";

    private static final String SQL_CLEANUP =
        "delete from " + StatusLogProcessor.TABLE_NAME;

    private static final String SQL_FILLUP =
        "insert into " + StatusLogProcessor.TABLE_NAME + " (" +
        				 StatusLogProcessor.COLUMN_ID + ", " +
        				 StatusLogProcessor.COLUMN_LATITUDE + ", " +
        				 StatusLogProcessor.COLUMN_LONGITUDE + ", " +
        				 StatusLogProcessor.COLUMN_STATUS + ", " +
        				 StatusLogProcessor.COLUMN_TEMPERATURE + ", " +
        				 StatusLogProcessor.COLUMN_HUMIDITY + ") " +
        " values (?, ?, ?, ?, ?, ?)";
    
    public static final String LATITUDE = "-19.222222";
    public static final String LONGITUDE = "36.563221";
    public static final String STATUS = "active";
    public static final String TEMPERATURE = "50F";
    public static final String HUMIDITY = "80%";
    
    public Integer id;
    public String latitude;
    public String longitude;
    public String status;
    public String temperature;
    public String humidity;
    
    private static StatusLogProcessor c_processor;
    
    private StatusLogProcessor() {
    }
    
    public void setStartFields() {
        this.latitude = StatusLogProcessor.LATITUDE;
        this.longitude = StatusLogProcessor.LONGITUDE;
        this.status = StatusLogProcessor.STATUS;
        this.temperature = StatusLogProcessor.TEMPERATURE;
        this.humidity = StatusLogProcessor.HUMIDITY;
    }

    public static StatusLogProcessor getProcessor() {
        if (c_processor == null) {
            c_processor = new StatusLogProcessor();
        }
        return c_processor;
    }
    
    public String getAllQuery() {
        return StatusLogProcessor.SQL_GET_ALL;
    }

    public String getCleanupQuery() {
        return StatusLogProcessor.SQL_CLEANUP;
    }

    public String getFillupQuery() {
        return StatusLogProcessor.SQL_FILLUP;
    }

    public String getOneQuery() {
        return StatusLogProcessor.SQL_GET_ONE;
    }

    public Object[] getValues() {
        return new Object[] {id,
                latitude,
                longitude,
                status,
                temperature,
                humidity};
    }

    public Object[] process(ResultSet rs) throws SQLException {
        ArrayList<StatusLogMockObject> result = new ArrayList<StatusLogMockObject>();
        while ( rs.next() ) {
        	StatusLogMockObject statusLogMockObject = new StatusLogMockObject();
        	
        	statusLogMockObject.latitude = rs.getString(StatusLogProcessor.COLUMN_LATITUDE);
        	statusLogMockObject.longitude = rs.getString(StatusLogProcessor.COLUMN_LONGITUDE);
        	statusLogMockObject.status = rs.getString(StatusLogProcessor.COLUMN_STATUS);
        	statusLogMockObject.temperature = rs.getString(StatusLogProcessor.COLUMN_TEMPERATURE);
        	statusLogMockObject.humidity = rs.getString(StatusLogProcessor.COLUMN_HUMIDITY);
            
            result.add(statusLogMockObject);
            
        }
        return result.toArray(new StatusLogMockObject[result.size()]);
    }

    public Integer makeStatusLog(
            Integer id,
            String latitude, 
    		String longitude, 
    		String status,
    		String temperature,
    		String humidity) {
        
        this.id = id;
        this.latitude = latitude;
        this.longitude = longitude;
        this.status = status;
        this.temperature = temperature;
        this.humidity = humidity;
        
        fillup();
        return id;
    }
	
}
