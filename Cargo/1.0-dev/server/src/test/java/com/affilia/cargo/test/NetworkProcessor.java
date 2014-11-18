/**
 * $Id: NetworkProcessor.java 182 2007-05-16 14:53:08Z moritur $
 */
package com.affilia.cargo.test;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import util.testhelper.EntityProcessor;
import util.testhelper.Sequence;

public class NetworkProcessor extends EntityProcessor {
    
    private static final String TABLE_NAME = "CARGO_NETWORK";
    private static final String SEQUENCE_NAME = "CARGO_NETWORK_SQC";
    
    private static final String COLUMN_ID = "ID";
    private static final String COLUMN_NAME = "NAME";

    private static final String SQL_GET_ALL =
        "select " + NetworkProcessor.COLUMN_ID + ", " +
            NetworkProcessor.COLUMN_NAME + 
        " from " + NetworkProcessor.TABLE_NAME;

    private static final String SQL_GET_ONE = NetworkProcessor.SQL_GET_ALL  
        + " where " + NetworkProcessor.COLUMN_ID + " = ?";
    
    private static final String SQL_CLEANUP =
        "delete from " + NetworkProcessor.TABLE_NAME;
    
    private static final String SQL_FILLUP =
        "insert into " + NetworkProcessor.TABLE_NAME + " (" +
            NetworkProcessor.COLUMN_ID + ", " +
            NetworkProcessor.COLUMN_NAME + ") " +
        " values (?, ?)";

    private static final String NAME = "network";
    
    private static Integer c_id;
    public String name;
    
    private static NetworkProcessor c_processor;
    
    private NetworkProcessor() {
    }
    
    public static NetworkProcessor getProcessor() {
        if (c_processor == null) {
            c_processor = new NetworkProcessor();
        }
        return c_processor;
    }

    public String getAllQuery() {
        return SQL_GET_ALL;
    }

    public String getCleanupQuery() {
        return SQL_CLEANUP;
    }

    public String getFillupQuery() {
        return SQL_FILLUP;
    }

    public String getOneQuery() {
        return SQL_GET_ONE;
    }

    public Object[] getValues() {
           c_id = Sequence.getSequence(NetworkProcessor.SEQUENCE_NAME);
            return new Object[] {
                    c_id,
                    name } ; 
    }

    public Object[] process(ResultSet rs) throws SQLException {
        ArrayList<NetworkMokcObject> result = new ArrayList<NetworkMokcObject>();
        while ( rs.next() ) {
            NetworkMokcObject network = new NetworkMokcObject();
            network.id = rs.getInt(NetworkProcessor.COLUMN_ID);
            network.name = rs.getString(NetworkProcessor.COLUMN_NAME);
            result.add(network);
        }
        return result.toArray(new NetworkMokcObject[result.size()]);
    }

    public void setStartFields() {
        c_id = null;
        name = NetworkProcessor.NAME;
    }
    
    public Integer getId() {
        return c_id;
    }

    public Integer makeNetwork(String name) {
        this.name = name;
        fillup();
        return c_id;
    }
    
    
}
