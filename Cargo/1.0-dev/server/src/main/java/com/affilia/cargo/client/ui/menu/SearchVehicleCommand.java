/**
 * $Id: SearchVehicleCommand.java 273 2007-06-16 08:31:05Z moritur $
 */
package com.affilia.cargo.client.ui.menu;

import com.affilia.cargo.client.data.VehicleData;
import com.google.gwt.user.client.Command;
import com.logicland.gwt.util.client.util.Callback;

public class SearchVehicleCommand 
    implements Command {
    
    public SearchVehicleCommand() {
    }
    
    public void execute() {
        new SearchVehicleDialog(
                new Callback() {
                    public void onCallback(Object result) {                                                
                        new VehicleSearchMapDialog((VehicleData) result);
                    }
                });
    }
 
}
