/**
 * $Id: InfoDataPackage.java 275 2007-06-16 13:44:06Z moritur $
 */

package com.affilia.cargo.server;

import java.sql.Timestamp;

import com.logicland.application.core.logger.LogHelper;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleJoinLogData;
import com.affilia.cargo.client.data.VehicleLogData;
import com.affilia.cargo.data.CargoDAO;

public class InfoDataPackage 
    extends DataPackage
{
    
    private final static Integer LOG_STATUS = new Integer(1);
    private final static String LOG_TEXT = "vehicle info";
    private final static String STATUS_VOLTAGE = "voltage:";
    private final static String STATUS_ILLUMINATION = "illumination:";
    
    private String humidity = null;
    private String temperature = null;
    private String voltage = null;
    private String illumination = null;
    
    public InfoDataPackage(Integer networkId, String data) {
        super(networkId, data);
    }

    public void storeTo(CargoDAO dao) {
        LogHelper.getLogger().debug("InfoDataPackage.storeTo() call");

        System.out.println("InfoDataPackage.storeTo: networkId=" + networkId);
        System.out.println("InfoDataPackage.storeTo: itemId=" + itemId);
        System.out.println("InfoDataPackage.storeTo: timestamp=" + timestamp);
        System.out.println("InfoDataPackage.storeTo: latitude=" + latitude);
        System.out.println("InfoDataPackage.storeTo: longitude=" + longitude);
        System.out.println("InfoDataPackage.storeTo: humidity=" + humidity);
        System.out.println("InfoDataPackage.storeTo: temperature=" + temperature);
        System.out.println("InfoDataPackage.storeTo: voltage=" + voltage);
        System.out.println("InfoDataPackage.storeTo: illumination=" + illumination);
        
        VehicleData vehicleData = null;
        try {
            vehicleData = dao.getVehicle(itemId);
        } catch (Exception ex) {
            LogHelper.getLogger().error(
                    "InfoDataPackage.storeTo() Error:vehicle not found by itemId=" + itemId, ex);
            throw new RuntimeException(
                    "InfoDataPackage.storeTo() Error:vehicle not found by itemId=" + itemId);
        }
        
        if ( null == vehicleData.networkId ) {
            VehicleJoinLogData logData = new VehicleJoinLogData();
            logData.networkId = networkId;
            logData.vehicleId = vehicleData.id;
            logData.logTime = new Timestamp(System.currentTimeMillis());
            logData.logLevel = JoinDataPackage.LOG_STATUS;
            logData.logText = JoinDataPackage.LOG_TEXT;
            logData.isJoin = JoinDataPackage.IS_JOIN;
            logData.isLeave = JoinDataPackage.IS_LEAVE;
            logData.isAuthorized = JoinDataPackage.IS_AUTHORIZED;
            
            dao.storeVehicleJoinToNetworkLog(logData);

        }
        
        VehicleLogData vehicleLogData = new VehicleLogData();
        vehicleLogData.networkId = networkId;
        vehicleLogData.vehicleId = vehicleData.id;
        vehicleLogData.logTime = new Timestamp(System.currentTimeMillis());
        vehicleLogData.logLevel = InfoDataPackage.LOG_STATUS;
        vehicleLogData.logText = InfoDataPackage.LOG_TEXT;
        vehicleLogData.latitude = latitude;
        vehicleLogData.longitude = longitude;
        vehicleLogData.status = InfoDataPackage.STATUS_VOLTAGE + voltage + ","
            + InfoDataPackage.STATUS_ILLUMINATION + illumination;
        vehicleLogData.humidity = humidity;
        vehicleLogData.temperature = temperature;
        
        dao.storeVehicleInfoLog(vehicleLogData);
    }

    public void parse() {
        super.parse();
        humidity = parser.nextToken();
        temperature = parser.nextToken();
        voltage = parser.nextToken();
        illumination = parser.nextToken();
    }

}
