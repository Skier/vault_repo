/**
 * $Id: CargoDAO.java 274 2007-06-16 13:08:32Z moritur $
 */
package com.affilia.cargo.data;

import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.Date;

import com.affilia.cargo.client.data.LogData;
import com.affilia.cargo.client.data.NetworkData;
import com.affilia.cargo.client.data.StatusLogData;
import com.affilia.cargo.client.data.UserData;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleJoinLogData;
import com.affilia.cargo.client.data.VehicleLogData;
import com.logicland.gwt.util.client.system.ClientException;

public interface CargoDAO {
    
    
    public NetworkData[] getAllNetworks();
    
    public StatusLogData getVehicleStatus(Integer vehicleId);
    
    public LogData[] getLogInformation(Long logId);
    
    public VehicleData[] getVehiclesByNetwork(Integer networkId);
    
    public Long getMaxLogId();
    
    public VehicleData getVehicle(String itemId)
        throws VehicleNotFoundByItemException, ClientException;
    
    public VehicleLogData[] getVehicleHistory(Integer vehicleId);
    
    public VehicleLogData getVehicalLogDataBeforeTime(Integer vehicleId, Date timePoint);

    public VehicleLogData getVehicalLogDataAfterTime(Integer vehicleId, Date timePoint);
    
    public VehicleLogData[] getVehicleHistory(Integer vehicleId,
            Date fromDate, Date toDate, long timeStep );
    
    public UserData storeUser(Integer id, String name, String password) 
    	throws DuplicateUserException, ClientException;
    	
    public void removeUser(Integer id);
       
    public UserData getUser(Integer id);
    
    public UserData getUser(String name);
    
    public UserData[] getUsers();
        
    public Integer storeVehicleInfoLog(VehicleLogData vehicleLogData);
    
    public Integer storeVehicleJoinToNetworkLog(VehicleJoinLogData vehicleLogData);

    public VehicleData[] getVehiclesWhichNotRespond(Timestamp fromTime);
    
    public Integer storeVehicleNotRespondLog(VehicleJoinLogData logData);
    
    public NetworkData getVehicleNetwork(Integer vehicleId) 
        throws SQLException;


}
