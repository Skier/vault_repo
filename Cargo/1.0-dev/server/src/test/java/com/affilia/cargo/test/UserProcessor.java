/**
 * $Id: UserProcessor.java 225 2007-05-29 16:10:59Z hatu $
 *
 */
package com.affilia.cargo.test;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import util.testhelper.EntityProcessor;
import util.testhelper.Sequence;

public class UserProcessor extends EntityProcessor{
	
	private static final String TABLE_NAME = "CARGO_USER";
	private static final String SEQUENCE_NAME = "CARGO_USER_SQC";
	
    private static final String COLUMN_ID = "ID";
    private static final String COLUMN_USER_NAME = "USER_NAME";
    private static final String COLUMN_PASSWORD = "PASSWORD";
    
    private static final String SQL_GET_ALL =
        "select " + UserProcessor.COLUMN_ID + ", " +
        			UserProcessor.COLUMN_USER_NAME + ", " +
        			UserProcessor.COLUMN_PASSWORD + 
        " from " + UserProcessor.TABLE_NAME;

    private static final String SQL_GET_ONE = UserProcessor.SQL_GET_ALL  
		+ " where " + UserProcessor.COLUMN_ID + " = ?";

    private static final String SQL_CLEANUP =
    	"delete from " + UserProcessor.TABLE_NAME;

    private static final String SQL_FILLUP =
       "insert into " + UserProcessor.TABLE_NAME + " (" +
       			UserProcessor.COLUMN_ID + ", " +
       			UserProcessor.COLUMN_USER_NAME + ", " +
       			UserProcessor.COLUMN_PASSWORD + ") " +
       " values (?, ?, ?)";

    
    public static final String USER_NAME = "hatu";
    public static final String PASSWORD = "mikluho";
    
    private static Integer c_id;
    public String userName;
    public String password;
    
    
    private static UserProcessor c_processor;
    
    private UserProcessor() {
	   
    }

	public void setStartFields() {
		this.userName = UserProcessor.USER_NAME;
		this.password = UserProcessor.PASSWORD;
		
	}
    
    public static UserProcessor getProcessor() {
    	if (c_processor == null) {
    		c_processor = new UserProcessor();
    	}
    	return c_processor;
    }
    
    public String getAllQuery() {
    	return UserProcessor.SQL_GET_ALL;
    }

    public String getCleanupQuery() {
    	return UserProcessor.SQL_CLEANUP;
    }

    public String getFillupQuery() {
    	return UserProcessor.SQL_FILLUP;
    }

    public String getOneQuery() {
    	return UserProcessor.SQL_GET_ONE;
    }
    
    public Object[] getValues() {
    	c_id = Sequence.getSequence(UserProcessor.SEQUENCE_NAME);
    	return new Object[] {c_id,
    			userName,
    			password};
    }

    public Object[] process(ResultSet rs) throws SQLException {
    	ArrayList<UserMockObject> result = new ArrayList<UserMockObject>();
    	while ( rs.next() ) {
    		
    		UserMockObject userMockObject = new UserMockObject();

    		userMockObject.id = new Integer(rs.getInt(UserProcessor.COLUMN_ID));
    		userMockObject.userName = rs.getString(UserProcessor.COLUMN_USER_NAME);
    		userMockObject.password = rs.getString(UserProcessor.COLUMN_PASSWORD);

    		result.add(userMockObject);

    	}
    	return result.toArray(new UserMockObject[result.size()]);
    }

    public Integer getId() {
    	return c_id;
    }

    public Integer makeUser(String userName, String password)  {
        
    	this.userName = userName;
    	this.password = password;

    	fillup();

    	return c_id;
    }
}
