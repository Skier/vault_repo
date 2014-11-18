/**
 * $Id: NetworkController.java 211 2007-05-25 06:49:10Z hatu $
 */

package com.affilia.cargo.client;

import com.google.gwt.user.client.Window;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.TreeItem;
import com.mapitz.gwt.googleMaps.client.GMap2;
import com.affilia.cargo.client.data.NetworkData;
import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.service.CargoServiceAsync;

public class NetworkController {
    
    private NetworkData m_network; 
    
    public NetworkController(NetworkData network, GMap2 map, TreeItem item) {
        m_network = network;
        refreshNetwork();       
    }
    
    public void refreshNetwork() {
        CargoServiceAsync service = CargoServiceLocator.getService();
        
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
                Window.alert(caught.getMessage());
            }

            public void onSuccess(Object result) {
                VehicleData[] vehicles = (VehicleData[]) result;
                for ( int i= 0; i<vehicles.length; i++ ) {
                    refreshVehicle(vehicles[i]);
                }
            }
        };
        service.getVehiclesByNetwork(m_network.id, callback);
    }
    
    public void refreshVehicle(VehicleData vehicle) {
      
    }
    
}
