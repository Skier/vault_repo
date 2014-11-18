/**
 * $Id: $
 */
package com.affilia.cargo.server;

import java.sql.Timestamp;
import java.util.TimerTask;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleJoinLogData;
import com.affilia.cargo.data.CargoDAO;
import com.logicland.application.core.logger.LogHelper;

public class VehicleOutNetworkTask extends TimerTask {
    
    public final static long VEHICLE_NOT_RESPOND_PERIOD = 30000;
    
    private final static Integer VEHICLE_NOT_RESPOND_LOG_LEVEL = new Integer(2);
    private final static String VEHICLE_NOT_RESPOND_LOG_TEXT = "vehicle leave network";
    private final static Boolean VEHICLE_NOT_RESPOND_IS_JOIN = Boolean.FALSE;
    private final static Boolean VEHICLE_NOT_RESPOND_IS_LEAVE = Boolean.TRUE;
    private final static Boolean VEHICLE_NOT_RESPOND_IS_AUTHORIZED = Boolean.FALSE;
    
    private CargoDAO m_cargoDAO;
    
    public VehicleOutNetworkTask(CargoDAO cargoDAO) {
        LogHelper.getLogger().debug("DataTestinTask() call");
        m_cargoDAO = cargoDAO;
    }
    
    
    public void run() {
        LogHelper.getLogger().debug("DataTestinTask.run() call");
        findVehiclesWhichNotRespond();
    }

    
    private void findVehiclesWhichNotRespond() {
        
        long carrentTime = System.currentTimeMillis();
        Timestamp fromTime = 
            new Timestamp(carrentTime - VehicleOutNetworkTask.VEHICLE_NOT_RESPOND_PERIOD);
        
        VehicleData[] vehicles = m_cargoDAO.getVehiclesWhichNotRespond(fromTime);
        LogHelper.getLogger().debug("  vehicles.lenght=" + vehicles.length);
        
        for ( int i=0; i<vehicles.length; i++ ) {
            
            VehicleJoinLogData logData = new VehicleJoinLogData();
            logData.networkId = vehicles[i].networkId;
            logData.vehicleId = vehicles[i].id;
            logData.logTime = new Timestamp(carrentTime);
            logData.logLevel = VehicleOutNetworkTask.VEHICLE_NOT_RESPOND_LOG_LEVEL;
            logData.logText = VehicleOutNetworkTask.VEHICLE_NOT_RESPOND_LOG_TEXT;
            logData.isJoin = VehicleOutNetworkTask.VEHICLE_NOT_RESPOND_IS_JOIN;
            logData.isLeave = VehicleOutNetworkTask.VEHICLE_NOT_RESPOND_IS_LEAVE;
            logData.isAuthorized = VehicleOutNetworkTask.VEHICLE_NOT_RESPOND_IS_AUTHORIZED;
            
            m_cargoDAO.storeVehicleNotRespondLog(logData);
        }
        
    }
    

}
