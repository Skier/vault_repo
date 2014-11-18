/**
 * $Id: JoinDataPackage.java 275 2007-06-16 13:44:06Z moritur $
 */
package com.affilia.cargo.server;

import java.sql.Timestamp;

import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleJoinLogData;
import com.affilia.cargo.data.CargoDAO;
import com.logicland.application.core.logger.LogHelper;

public class JoinDataPackage
    extends DataPackage {

    public final static Integer LOG_STATUS = new Integer(2);
    public final static String LOG_TEXT = "vehicle join";
    public final static Boolean IS_JOIN = Boolean.TRUE;
    public final static Boolean IS_LEAVE = Boolean.FALSE;
    public final static Boolean IS_AUTHORIZED = Boolean.FALSE;
    
    public JoinDataPackage(Integer networkId, String data) {
        super(networkId, data);
    }
    
    public void storeTo(CargoDAO dao) {
        LogHelper.getLogger().debug("JoinDataPackage.storeTo() call");

        VehicleData vehicleData = null;
        try {
            vehicleData = dao.getVehicle(itemId);
        } catch (Exception ex) {
            LogHelper.getLogger().error(
                    "InfoDataPackage.storeTo() Error:vehicle not found by itemId=" + itemId, ex);
            throw new RuntimeException(
                    "InfoDataPackage.storeTo() Error:vehicle not found by itemId=" + itemId);
        }

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


}
