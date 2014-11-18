/**
 * $Id: VehicleProcessor.java 195 2007-05-21 11:59:21Z moritur $
 *
 */
package com.affilia.cargo.test;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import util.testhelper.EntityProcessor;
import util.testhelper.Sequence;

public class VehicleProcessor extends EntityProcessor{
	
	private static final String TABLE_NAME = "CARGO_VEHICLE";
	private static final String SEQUENCE_NAME = "CARGO_VEHICLE_SQC";
	
    private static final String COLUMN_ID = "ID";
    private static final String COLUMN_ITEM_ID = "ITEM_ID";
    private static final String COLUMN_ITEM_OWNER = "ITEM_OWNER";
    private static final String COLUMN_NAME = "NAME";
    
    private static final String SQL_GET_ALL =
        "select " + VehicleProcessor.COLUMN_ID + ", " +
        			VehicleProcessor.COLUMN_ITEM_ID + ", " +
        			VehicleProcessor.COLUMN_ITEM_OWNER + ", " +
        			VehicleProcessor.COLUMN_NAME +
        " from " + VehicleProcessor.TABLE_NAME;

    private static final String SQL_GET_ONE = VehicleProcessor.SQL_GET_ALL  
		+ " where " + VehicleProcessor.COLUMN_ID + " = ?";

    private static final String SQL_CLEANUP =
    	"delete from " + VehicleProcessor.TABLE_NAME;

    private static final String SQL_FILLUP =
       "insert into " + VehicleProcessor.TABLE_NAME + " (" +
        	VehicleProcessor.COLUMN_ID + ", " +
        	VehicleProcessor.COLUMN_ITEM_ID + ", " +
        	VehicleProcessor.COLUMN_ITEM_OWNER + ", " +
        	VehicleProcessor.COLUMN_NAME + ") " +
       " values (?, ?, ?, ?)";
    
    public static final String ITEM_ID = "1";
    public static final String ITEM_OWNER = "owner";
    public static final String NAME = "name";
    
    private static Integer c_id;
    public String itemId;
    public String itemOwner;
    public String name;
    
    private static VehicleProcessor c_processor;
    
    private VehicleProcessor() {
	   
    }
    
    public void setStartFields() {
    	this.itemId = VehicleProcessor.ITEM_ID;
    	this.itemOwner = VehicleProcessor.ITEM_OWNER;
    	this.name = VehicleProcessor.NAME;
    }
    
    public static VehicleProcessor getProcessor() {
    	if (c_processor == null) {
    		c_processor = new VehicleProcessor();
    	}
    	return c_processor;
    }
    
    public String getAllQuery() {
    	return VehicleProcessor.SQL_GET_ALL;
    }

    public String getCleanupQuery() {
    	return VehicleProcessor.SQL_CLEANUP;
    }

    public String getFillupQuery() {
    	return VehicleProcessor.SQL_FILLUP;
    }

    public String getOneQuery() {
    	return VehicleProcessor.SQL_GET_ONE;
    }
    
    public Object[] getValues() {
    	c_id = Sequence.getSequence(VehicleProcessor.SEQUENCE_NAME);
    	return new Object[] {c_id,
    			itemId,
    			itemOwner,
    			name};
    }

    public Object[] process(ResultSet rs) throws SQLException {
    	ArrayList<VehicleMockObject> result = new ArrayList<VehicleMockObject>();
    	while ( rs.next() ) {
    		VehicleMockObject vehicleMockObject = new VehicleMockObject();

            vehicleMockObject.id = new Integer(rs.getInt(VehicleProcessor.COLUMN_ID));
    		vehicleMockObject.itemId = rs.getString(VehicleProcessor.COLUMN_ITEM_ID);
    		vehicleMockObject.itemOwner = rs.getString(VehicleProcessor.COLUMN_ITEM_OWNER);
    		vehicleMockObject.name = rs.getString(VehicleProcessor.COLUMN_NAME);

    		result.add(vehicleMockObject);

    	}
    	return result.toArray(new VehicleMockObject[result.size()]);
    }

    public Integer getId() {
    	return c_id;
    }

    public Integer makeVehicle(String itemId, 
    		String itemOwner, 
    		String name)  {
        
    	this.itemId = itemId;
    	this.itemOwner = itemOwner;
    	this.name = name;

    	fillup();

    	return c_id;
    }
}
