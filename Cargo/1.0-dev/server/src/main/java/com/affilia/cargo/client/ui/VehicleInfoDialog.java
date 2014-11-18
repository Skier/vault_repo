/**
 * $Id: VehicleInfoDialog.java 273 2007-06-16 08:31:05Z moritur $
 */
package com.affilia.cargo.client.ui;

import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.ui.util.CentralScreenDialogBox;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;

public class VehicleInfoDialog extends CentralScreenDialogBox {
	
	private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";
	
    private static final String CAPTION_CELL_CSS = "ll-CaptionCell";
    private static final String INFO_CELL_CSS = "ll-InfoCell";

    private static final String LATITUDE_MSG = "Latitude: ";
    private static final String LONGITUDE_MSG = "Longitude: ";
    private static final String STATUS_MSG = "Status: ";
    private static final String TEMPERATURE_MSG = "Temperature: ";
    private static final String TEMPERATURE_SUF_MSG = "&deg;F";
    private static final String HUMIDITY_MSG = "Humidity: ";
    private static final String HUMIDITY_SUF_MSG = "%";
    	
	public VehicleInfoDialog(VehicleData vehicle) {
        super(false);
		
		setText(vehicle.name + " [ " + vehicle.itemId + " ] ");
        
        Grid infoGrid = new Grid(5, 2); 
		
		infoGrid.setText(0, 0, VehicleInfoDialog.LATITUDE_MSG);
		infoGrid.setText(0, 1, vehicle.latitude + "");
        
		infoGrid.setText(1, 0, VehicleInfoDialog.LONGITUDE_MSG);
		infoGrid.setText(1, 1, vehicle.longitude + "");
        
		infoGrid.setText(2, 0, VehicleInfoDialog.STATUS_MSG);
		infoGrid.setText(2, 1, vehicle.status);
        
		infoGrid.setText(3, 0, VehicleInfoDialog.TEMPERATURE_MSG);
		infoGrid.setHTML(3, 1, vehicle.temperature + VehicleInfoDialog.TEMPERATURE_SUF_MSG);
        
		infoGrid.setText(4, 0, VehicleInfoDialog.HUMIDITY_MSG);
		infoGrid.setText(4, 1, vehicle.humidity + VehicleInfoDialog.HUMIDITY_SUF_MSG);
		    
		for (int i = 0; i < infoGrid.getRowCount(); i++) {
			infoGrid.getCellFormatter().addStyleName(i, 0, VehicleInfoDialog.CAPTION_CELL_CSS);
			infoGrid.getCellFormatter().addStyleName(i, 1, VehicleInfoDialog.INFO_CELL_CSS);
		}

        VerticalPanel rootPanel = getRootPanel();
        
        rootPanel.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
        
        rootPanel.add(infoGrid);
        
        Button closeButton = new Button("Close");
        closeButton.addClickListener(new ClickListener() {
            public void onClick(Widget sender) {
            	hide();
            }
        });

        rootPanel.add(closeButton);
        
        show();
        
	}
	
	public String getStyleName() {
		return VehicleInfoDialog.DIALOG_BOX_CSS_CLASS;
	}

}
