/**
 * $Id: NetworkLogProcessor.java 250 2007-06-05 13:08:15Z moritur $
 *
 */
package com.affilia.cargo.test;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import util.testhelper.EntityProcessor;
import util.testhelper.Sequence;

public class NetworkLogProcessor extends EntityProcessor{
	
	private static final String TABLE_NAME = "CARGO_NETWORK_LOG";
    private static final String SEQUENCE_NAME = "CARGO_NETWORK_LOG_SQC";
    
    private static final String COLUMN_ID = "ID";
    private static final String COLUMN_IS_JOIN = "IS_JOIN";
    private static final String COLUMN_IS_LEAVE = "IS_LEAVE";
    private static final String COLUMN_IS_AUTHORIZED = "IS_AUTHORIZED";
    
    private static final String SQL_GET_ALL =
        "select " + NetworkLogProcessor.COLUMN_ID + ", " +
        			NetworkLogProcessor.COLUMN_IS_JOIN + ", " +
        			NetworkLogProcessor.COLUMN_IS_LEAVE + ", " +
        			NetworkLogProcessor.COLUMN_IS_AUTHORIZED +
        " from " + NetworkLogProcessor.TABLE_NAME;
    
    private static final String SQL_GET_ONE = NetworkLogProcessor.SQL_GET_ALL  
		+ " where " + NetworkLogProcessor.COLUMN_ID + " = ?";
    
    private static final String SQL_CLEANUP =
    	"delete from " + NetworkLogProcessor.TABLE_NAME;

    private static final String SQL_FILLUP =
        "insert into " + NetworkLogProcessor.TABLE_NAME + " (" +
        	NetworkLogProcessor.COLUMN_ID + ", " +
        	NetworkLogProcessor.COLUMN_IS_JOIN + ", " +
        	NetworkLogProcessor.COLUMN_IS_LEAVE + ", " +
        	NetworkLogProcessor.COLUMN_IS_AUTHORIZED + ") " +
        " values (?, ?, ?, ?)";
    
    public static final Boolean IS_JOIN = Boolean.TRUE;
    public static final Boolean IS_LEAVE = Boolean.FALSE;
    public static final Boolean IS_AUTHORIZED = Boolean.TRUE;
    
    private static Integer c_id;
    public Boolean isJoin;
    public Boolean isLeave;
    public Boolean isAuthorized;
    
    private static NetworkLogProcessor c_processor;
    
    private NetworkLogProcessor() {
    	
    }
    
    public void setStartFields() {
    	this.isJoin = NetworkLogProcessor.IS_JOIN;
    	this.isLeave = NetworkLogProcessor.IS_LEAVE;
    	this.isAuthorized = NetworkLogProcessor.IS_AUTHORIZED;
    }
    
    public static NetworkLogProcessor getProcessor() {
    	if (c_processor == null) {
    		c_processor = new NetworkLogProcessor();
    	}
    	return c_processor;
    }

    public String getAllQuery() {
    	return NetworkLogProcessor.SQL_GET_ALL;
    }

    public String getCleanupQuery() {
    	return NetworkLogProcessor.SQL_CLEANUP;
    }

    public String getFillupQuery() {
    	return NetworkLogProcessor.SQL_FILLUP;
    }

    public String getOneQuery() {
    	return NetworkLogProcessor.SQL_GET_ONE;
    }


    public Object[] getValues() {
    	c_id = Sequence.getSequence(NetworkLogProcessor.SEQUENCE_NAME);
    	return new Object[] {c_id,
    			isJoin,
    			isLeave,
    			isAuthorized };
    }

    public Object[] process(ResultSet rs) throws SQLException {
    	ArrayList<NetworkLogMockObject> result = new ArrayList<NetworkLogMockObject>();
    	while ( rs.next() ) {
    		NetworkLogMockObject networkLogMockObject = new NetworkLogMockObject();

    		networkLogMockObject.isJoin = 
    			rs.getBoolean(NetworkLogProcessor.COLUMN_IS_JOIN);
    		networkLogMockObject.isLeave = 
    			rs.getBoolean(NetworkLogProcessor.COLUMN_IS_LEAVE);
    		networkLogMockObject.isAuthorized = 
    			rs.getBoolean(NetworkLogProcessor.COLUMN_IS_AUTHORIZED);

    		result.add(networkLogMockObject);

    	}
    	return result.toArray(new NetworkLogMockObject[result.size()]);
    }

    public Integer getId() {
    	return c_id;
    }

    public Integer makeNetworkLog(Boolean isJoin, 
    		Boolean isLeave,
    		Boolean isAuthorized) 
    {
    	this.isJoin = isJoin;
    	this.isLeave = isLeave;
    	this.isAuthorized = isAuthorized;

    	fillup();

    	return c_id;
    }
	
}
