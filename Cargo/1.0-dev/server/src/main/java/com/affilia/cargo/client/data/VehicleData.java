/**
 * $Id: VehicleData.java 249 2007-06-05 12:01:42Z moritur $
 */
package com.affilia.cargo.client.data;

import com.google.gwt.user.client.rpc.IsSerializable;

public class VehicleData implements IsSerializable {
    public Integer id;
    public String itemId;
    public String name;
    public String itemOwner;
    public double latitude;
    public double longitude;
    
    public Integer networkId;
    
    public String status;
    public String temperature;
    public String humidity;
}
