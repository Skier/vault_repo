/**
 * $Id: NetworkVehicleProcessor.java 243 2007-06-04 12:55:32Z moritur $
 */
package com.affilia.cargo.test;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import util.testhelper.EntityProcessor;
import util.testhelper.Sequence;

public class NetworkVehicleProcessor extends EntityProcessor {
    
    private static final String TABLE_NAME = "CARGO_NETWORK_VEHICLE";
    private static final String SEQUENCE_NAME = "CARGO_NETWORK_VEHICLE_SQC";
    
    private static final String COLUMN_ID = "ID";
    private static final String COLUMN_NETWORK_ID = "NETWORK_ID";
    private static final String COLUMN_VEHICLE_ID = "VEHICLE_ID";

    private static final String SQL_GET_ALL =
        "select " + NetworkVehicleProcessor.COLUMN_ID + ", " +
                NetworkVehicleProcessor.COLUMN_NETWORK_ID + ", " +
                NetworkVehicleProcessor.COLUMN_VEHICLE_ID + 
        " from " + NetworkVehicleProcessor.TABLE_NAME;

    private static final String SQL_GET_ONE = NetworkVehicleProcessor.SQL_GET_ALL  
        + " where " + NetworkVehicleProcessor.COLUMN_ID + " = ?";
    
    private static final String SQL_CLEANUP =
        "delete from " + NetworkVehicleProcessor.TABLE_NAME;
    
    private static final String SQL_FILLUP =
        "insert into " + NetworkVehicleProcessor.TABLE_NAME + " (" +
            NetworkVehicleProcessor.COLUMN_ID + ", " +
            NetworkVehicleProcessor.COLUMN_NETWORK_ID + ", " +
            NetworkVehicleProcessor.COLUMN_VEHICLE_ID +  ") " +
        " values (?, ?, ?)";

    private static Integer c_id;
    
    public Integer networkId;
    public Integer vehicleId;

    private static NetworkVehicleProcessor c_processor;
    
    private NetworkVehicleProcessor() {
    }
    
    public static NetworkVehicleProcessor getProcessor() {
        if (c_processor == null) {
            c_processor = new NetworkVehicleProcessor();
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
        c_id = Sequence.getSequence(NetworkVehicleProcessor.SEQUENCE_NAME);
         return new Object[] {
                 c_id,
                 networkId,
                 vehicleId} ; 
    }

    public Object[] process(ResultSet rs) throws SQLException {
        ArrayList<NetworkVehicleMockObject> result = new ArrayList<NetworkVehicleMockObject>();
        while ( rs.next() ) {
            NetworkVehicleMockObject networkVech = new NetworkVehicleMockObject();
            networkVech.id = rs.getInt(NetworkVehicleProcessor.COLUMN_ID);
            networkVech.networkId = rs.getInt(NetworkVehicleProcessor.COLUMN_NETWORK_ID);
            networkVech.vehicleId = rs.getInt(NetworkVehicleProcessor.COLUMN_VEHICLE_ID);
            result.add(networkVech);
        }
        return result.toArray(new NetworkVehicleMockObject[result.size()]);
    }

    public void setStartFields() {
        c_id = null;
    }

    public Integer getId() {
        return c_id;
    }

    public Integer makeNetworkVehicle() {
        fillup();
        return c_id;
    }

}
