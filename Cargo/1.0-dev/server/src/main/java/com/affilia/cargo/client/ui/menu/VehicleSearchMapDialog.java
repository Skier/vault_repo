/**
 * $Id: VehicleSearchMapDialog.java 273 2007-06-16 08:31:05Z moritur $
 */
package com.affilia.cargo.client.ui.menu;

import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.ui.VehicleInfoDialog;
import com.affilia.cargo.client.ui.util.CentralScreenDialogBox;
import com.affilia.cargo.client.ui.util.MapWidget;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.KeyboardListener;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.mapitz.gwt.googleMaps.client.GIcon;
import com.mapitz.gwt.googleMaps.client.GLatLng;
import com.mapitz.gwt.googleMaps.client.GMarker;
import com.mapitz.gwt.googleMaps.client.GMarkerEventClickListener;
import com.mapitz.gwt.googleMaps.client.GMarkerEventManager;
import com.mapitz.gwt.googleMaps.client.GMarkerManager;
import com.mapitz.gwt.googleMaps.client.GMarkerOptions;
import com.mapitz.gwt.googleMaps.client.GPoint;
import com.mapitz.gwt.googleMaps.client.GSize;

public class VehicleSearchMapDialog extends CentralScreenDialogBox {
    private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";
    
    private static final String ICON_IMAGE_PATH = "img/unit.png";
    private static final String ICON_SHADOW_PATH = "img/unit_shadow.png";
    
    private static final String DIALOG_CAPTION_MSG = "Search Vehicle Map";
    private static final String CLOSE_BUTTON_MSG = "Close";
    
    private static final String MAP_WIDTH = "450px";
    private static final String MAP_HEIDHT = "450px";
    
    private MapWidget m_mapWidget = new MapWidget();
    

    public VehicleSearchMapDialog(VehicleData vehicleData) {
        super(true);
        
        setText(VehicleSearchMapDialog.DIALOG_CAPTION_MSG);
        
        VerticalPanel mainPanel = new VerticalPanel();
        mainPanel.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
        
        Button buttonClose = new Button(VehicleSearchMapDialog.CLOSE_BUTTON_MSG);
        buttonClose.addClickListener(new ClickListener(){
            public void onClick(Widget sender) {
                onCloseButton();
            }
        });

        HorizontalPanel buttonPanel = new HorizontalPanel();
        buttonPanel.add(buttonClose);
        
        m_mapWidget.setWidth(VehicleSearchMapDialog.MAP_WIDTH);
        m_mapWidget.setHeight(VehicleSearchMapDialog.MAP_HEIDHT);

        mainPanel.add(m_mapWidget);
        mainPanel.add(buttonPanel);
        
        getRootPanel().add(mainPanel);

        show();
                
        populateVehicle(vehicleData);
        
    }
    
    public String getStyleName() {
        return VehicleSearchMapDialog.DIALOG_BOX_CSS_CLASS;
    }

    
    public boolean onKeyDownPreview(char key, int modifiers) {
        switch(key) {
            case KeyboardListener.KEY_ESCAPE:
                onCloseButton();
                break;
        }
        return true;
    }
    
    private void populateVehicle(VehicleData vehicleData) {
        
        m_mapWidget.centerMap(getVehiclePoints(vehicleData));
        
        GMarkerManager markerManager = new GMarkerManager(m_mapWidget.getGmap());

        markerManager.addMarker(getMarker(vehicleData), getZoom());

        markerManager.refresh();

        
    }
    
    private int getZoom() {
        return MapWidget.getDefaultZoom();
    }
    
    private GLatLng getVehiclePoints(VehicleData vehicleData) {
        return new GLatLng(vehicleData.latitude, vehicleData.longitude);
    }
    
    private GMarker getMarker(final VehicleData vehicleData) {
                
        GIcon icon = new GIcon();
        icon.setImage(VehicleSearchMapDialog.ICON_IMAGE_PATH);
        icon.setShadow(VehicleSearchMapDialog.ICON_SHADOW_PATH);
        icon.setIconSize(new GSize(16, 24));
        icon.setShadowSize(new GSize(33, 30));
        icon.setIconAnchor(new GPoint(8, 24));
       
        GMarkerOptions markerOptions = new GMarkerOptions();
        markerOptions.setIcon(icon);
        
        GMarker marker = new GMarker(getVehiclePoints(vehicleData), markerOptions);

        GMarkerEventManager markerEventManager = GMarkerEventManager.getInstance(); 

        markerEventManager.addOnClickListener(
                marker, 
                new GMarkerEventClickListener() {
                    public void onClick(GMarker marker) {
                        new VehicleInfoDialog(vehicleData);
                    }

                    public void onDblClick(GMarker marker) {
                    }
            });

        return marker;
    }
    
    private void onCloseButton() {
        VehicleSearchMapDialog.this.hide();
    }

        
}
