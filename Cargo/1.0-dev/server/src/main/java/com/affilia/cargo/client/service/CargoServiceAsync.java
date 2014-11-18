/**
 * $Id: CargoServiceAsync.java 281 2007-06-20 11:18:16Z moritur $
 */

package com.affilia.cargo.client.service;


import java.util.Date;

import com.google.gwt.user.client.rpc.AsyncCallback;

public interface CargoServiceAsync 
{
    public void getAllNetworks(AsyncCallback callback);
    
    public void getVehiclesByNetwork(Integer networkId, AsyncCallback callback);
    
    public void getVehicleStatus(Integer vehicleId, AsyncCallback callback);
    
    public void getLogInformation(Long logId, AsyncCallback callback);
    
    public void getMaxLogId(AsyncCallback callback);
    
    public void getVehicle(String itemId, AsyncCallback callback);

    public void getVehicleHistory(String vehicleItemId,
            Date fromDate, Date toDate, long timeStep, AsyncCallback callback);
    
    public void storeUser(Integer id, String name, String password, 
    		AsyncCallback callback);
    
    public void removeUser(Integer id, AsyncCallback callback);
    
    public void getUser(Integer id, AsyncCallback callback);
    
    public void getUser(String name, AsyncCallback callback);
    
    public void getUsers(AsyncCallback callback);
    
}
