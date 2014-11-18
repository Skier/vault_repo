/**
 * $Id: CargoServiceImpl.java 281 2007-06-20 11:18:16Z moritur $
 */

package com.affilia.cargo.server;

import java.sql.Connection;
import java.util.Date;
import com.affilia.cargo.client.data.LogData;
import com.affilia.cargo.client.data.NetworkData;
import com.affilia.cargo.client.data.StatusLogData;
import com.affilia.cargo.client.data.UserData;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleLogData;
import com.affilia.cargo.client.service.CargoService;
import com.affilia.cargo.data.CargoDAO;
import com.affilia.cargo.data.CargoDAOImpl;
import com.affilia.cargo.data.ConnectionHelper;
import com.google.gwt.user.server.rpc.RemoteServiceServlet;


public class CargoServiceImpl 
    extends RemoteServiceServlet 
    implements CargoService {

    
    private Connection m_connection =  ConnectionHelper.getInstance().getConnection();
    
    private final CargoDAO m_cargoDAO = 
        ExceptionProxyFactory.createExceptionProxy(
                new CargoDAOImpl(m_connection));
    
    public NetworkData[] getAllNetworks() {
        
        return m_cargoDAO.getAllNetworks();
        
    }
    
    public VehicleData[] getVehiclesByNetwork(Integer networkId) {
    	return m_cargoDAO.getVehiclesByNetwork(networkId);
    }
    
    public StatusLogData getVehicleStatus(Integer vehicleId) {
    	return m_cargoDAO.getVehicleStatus(vehicleId);
    }
    
    public LogData[] getLogInformation(Long logId) {
    	return m_cargoDAO.getLogInformation(logId);
    }
    
    public Long getMaxLogId() {
    	return m_cargoDAO.getMaxLogId();
    }
    
    public VehicleData getVehicle(String itemId) 
        throws Exception {
        return m_cargoDAO.getVehicle(itemId);
    }
    
    public VehicleLogData[] getVehicleHistory(String vehicleItemId,
            Date fromDate, Date toDate, long timeStep) throws Exception {
        VehicleData vehicle = getVehicle(vehicleItemId);
        return m_cargoDAO.getVehicleHistory(
                vehicle.id, fromDate, toDate, timeStep);
    }
    
    public UserData storeUser(Integer id, String name, String password) throws Exception {
    	return m_cargoDAO.storeUser(id, name, password);
    }
    
    public void removeUser(Integer id) {
    	m_cargoDAO.removeUser(id);
    }

    public UserData getUser(Integer id) {
        return m_cargoDAO.getUser(id);
    }
    
    public UserData getUser(String name) {
        return m_cargoDAO.getUser(name);
    }
    
    public UserData[] getUsers() {
        return m_cargoDAO.getUsers();
    }
}
