/**
 * $Id: VehicleJoinLogData.java 251 2007-06-05 13:40:29Z moritur $
 */
package com.affilia.cargo.client.data;

import java.util.Date;

public class VehicleJoinLogData {
    
    public Long id;
    
    // CARGO_LOG
    
    public Integer networkId;
    
    public Integer vehicleId;
    
    public Date logTime;
    
    public Integer logLevel;
    
    public String logText;
    
    // CARGO_NETWORK_LOG
    
    public Boolean isJoin;
    
    public Boolean isLeave;
    
    public Boolean isAuthorized;

}
