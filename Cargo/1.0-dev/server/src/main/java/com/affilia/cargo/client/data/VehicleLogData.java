/**
 * $Id: VehicleLogData.java 241 2007-06-04 08:57:38Z moritur $
 */
package com.affilia.cargo.client.data;

import java.util.Date;

import com.google.gwt.user.client.rpc.IsSerializable;

public class VehicleLogData implements IsSerializable{
    
    public Long id;
    
    // CARGO_LOG
    
    public Integer networkId;
    
    public Integer vehicleId;
    
    public Date logTime;
    
    public Integer logLevel;
    
    public String logText;
    
    // CARGO_STATUS_LOG
    
    public Double latitude;
    
    public Double longitude;
    
    public String status;
    
    public String temperature;
    
    public String humidity;
    

}
