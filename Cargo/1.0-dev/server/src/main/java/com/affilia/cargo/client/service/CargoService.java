/**
 * $Id: CargoService.java 281 2007-06-20 11:18:16Z moritur $
 */

package com.affilia.cargo.client.service;

import java.util.Date;

import com.google.gwt.user.client.rpc.RemoteService;
import com.affilia.cargo.client.data.NetworkData;
import com.affilia.cargo.client.data.StatusLogData;
import com.affilia.cargo.client.data.UserData;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.LogData;
import com.affilia.cargo.client.data.VehicleLogData;

public interface CargoService 
    extends RemoteService 
{
    public NetworkData[] getAllNetworks();

    public VehicleData[] getVehiclesByNetwork(Integer networkId);
    
    public StatusLogData getVehicleStatus(Integer vehicleId);
    
    public LogData[] getLogInformation(Long logId);
    
    public Long getMaxLogId();
    
    public VehicleData getVehicle(String itemId) throws Exception;

    public VehicleLogData[] getVehicleHistory(String vehicleItemId,
            Date fromDate, Date toDate, long timeStep) throws Exception;
    
    public UserData storeUser(Integer id, String name, String password) 
    	throws Exception;
    
    public void removeUser(Integer id);

    public UserData getUser(Integer id);
    
    public UserData getUser(String name);
    
    public UserData[] getUsers();
    
}
