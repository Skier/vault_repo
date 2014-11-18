/**
 * $Id: VehicleTravelDialog.java 288 2007-06-25 08:29:11Z moritur $
 *
 */
package com.affilia.cargo.client.ui.menu;

import java.util.ArrayList;

import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleLogData;
import com.affilia.cargo.client.ui.VehicleInfoDialog;
import com.affilia.cargo.client.ui.util.CentralScreenDialogBox;
import com.affilia.cargo.client.ui.util.MapWidget;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.RootPanel;
import com.google.gwt.user.client.ui.Widget;
import com.mapitz.gwt.googleMaps.client.GIcon;
import com.mapitz.gwt.googleMaps.client.GLatLng;
import com.mapitz.gwt.googleMaps.client.GMap2;
import com.mapitz.gwt.googleMaps.client.GMarker;
import com.mapitz.gwt.googleMaps.client.GMarkerEventClickListener;
import com.mapitz.gwt.googleMaps.client.GMarkerEventManager;
import com.mapitz.gwt.googleMaps.client.GMarkerManager;
import com.mapitz.gwt.googleMaps.client.GMarkerOptions;
import com.mapitz.gwt.googleMaps.client.GPoint;
import com.mapitz.gwt.googleMaps.client.GPolyline;
import com.mapitz.gwt.googleMaps.client.GSize;

public class VehicleTravelDialog extends CentralScreenDialogBox {
	
	private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";
	
    private static final String ICON_IMAGE_PATH = "img/unit.png";
    private static final String ICON_SHADOW_PATH = "img/unit_shadow.png";
	private static final String POLYLINE_COLOR = "#9c210c";
	private static final int POLYLINE_WEIGHT = 3;
	private static final double POLYLINE_OPACITY = 1; 
	
    private static final String DIALOG_CAPTION_MSG = "Vehicle History Map";
    private static final String CLOSE_BUTTON_MSG = "Close";
    
    private static final String MAP_WIDTH = "450px";
    private static final String MAP_HEIDHT = "450px";
	
	private MapWidget m_mapWidget = new MapWidget();
    
    private VehicleData m_vehicleData;
	
	public VehicleTravelDialog(VehicleData vehicleData, VehicleLogData[] logData) {
        super(false);

        m_vehicleData = vehicleData;
        
		setText(VehicleTravelDialog.DIALOG_CAPTION_MSG);
		
        getRootPanel().setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
		
        m_mapWidget.setWidth(VehicleTravelDialog.MAP_WIDTH);
        m_mapWidget.setHeight(VehicleTravelDialog.MAP_HEIDHT);
        getRootPanel().add(m_mapWidget);
        
		Button buttonOk = new Button(VehicleTravelDialog.CLOSE_BUTTON_MSG);
		buttonOk.addClickListener(new ClickListener(){
			public void onClick(Widget sender) {
				hideDialog();
			}
		});
		
		HorizontalPanel buttonPanel = new HorizontalPanel();
		buttonPanel.add(buttonOk);

        
        getRootPanel().add(buttonPanel);

        show();
        
		buildingVehicleTravel(logData);
	}
	
	public Widget getWidget() {
		return m_mapWidget;
	}
	
	public String getStyleName() {
		return VehicleTravelDialog.DIALOG_BOX_CSS_CLASS;
	}

	private void buildingVehicleTravel(VehicleLogData[] data) {
        
        GLatLng[] vehiclePoints = getVehiclesPoints(data);
        
        int zoom = m_mapWidget.getZoom(vehiclePoints);
		
        getMap().setZoom(zoom);
        
        m_mapWidget.centerMap(vehiclePoints,zoom);
		
		GMarker[] markers = getMarkers(data);

        GMarkerManager markerManager = new GMarkerManager(getMap());

        markerManager.addMarkers(markers, zoom);

		markerManager.refresh();

		ArrayList polylines = getVehiclePolyLines(data);
        GPolyline travelPolyline = null;
        for ( int i=0; i<polylines.size(); i++ ) {
            travelPolyline = new GPolyline(
                    (GLatLng[]) polylines.get(i), 
                    VehicleTravelDialog.POLYLINE_COLOR, 
                    VehicleTravelDialog.POLYLINE_WEIGHT,
                    VehicleTravelDialog.POLYLINE_OPACITY);
            getMap().addOverlay(travelPolyline);
        }
		
	}
	
	private GLatLng[] getVehiclesPoints(VehicleLogData[] data) {
        ArrayList points = new ArrayList();
        int i;
		for (i = 0; i < data.length; i++) {
            if ( null != data[i] ) {
                points.add(new GLatLng(data[i].latitude.doubleValue(), 
                        data[i].longitude.doubleValue()));
            }
		}
        if ( 0 == points.size() ) {
            points.add(new GLatLng(0,0));
        }
        
		return getListAsGLatLngArray(points);
	}
    
    
    private ArrayList getVehiclePolyLines(VehicleLogData[] data) {
        
        ArrayList polyLines = new ArrayList();
        ArrayList points = null;
        for (int i = 0; i < data.length; i++) {
            if ( null != data[i] ) {
                if ( null == points ) {
                    points = new ArrayList();
                }
                points.add(new GLatLng(data[i].latitude.doubleValue(), 
                        data[i].longitude.doubleValue()));
            } else {
                if ( null != points ) {
                    polyLines.add(getListAsGLatLngArray(points));
                }
                points = null;
            }
        }        

        if ( null != points ) {
            polyLines.add(getListAsGLatLngArray(points));
        }
        
        return polyLines;
        
    }
	
	private GMarker[] getMarkers(VehicleLogData[] vehicles) {
		GMarkerEventManager markerEventManager = GMarkerEventManager.getInstance();		
		ArrayList markers = new ArrayList();
		GMarkerOptions markerOptions = new GMarkerOptions();
		
		// Setup icon & icon shadow image (It's advisable: set all properties
		// for icon, otherwise you don't see them)
		GIcon icon = new GIcon();
        icon.setImage(VehicleTravelDialog.ICON_IMAGE_PATH);
        icon.setShadow(VehicleTravelDialog.ICON_SHADOW_PATH);
        icon.setIconSize(new GSize(16, 24));
        icon.setShadowSize(new GSize(33, 30));
        icon.setIconAnchor(new GPoint(8, 24));
		icon.setInfoWindowAnchor(new GPoint(5, 1));
		
		markerOptions.setIcon(icon);
        GMarker marker;
        int i;
		for ( i = 0; i < vehicles.length; i++) {
            if ( null != vehicles[i] ) {
    			final GLatLng coord = new GLatLng(
    					vehicles[i].latitude.doubleValue(),
    					vehicles[i].longitude.doubleValue());
    			
                marker = new GMarker(coord, markerOptions);
    			
    			final VehicleData vehicle = getVehicleData(vehicles[i]);
    			
    			markerEventManager.addOnClickListener(marker, 
    					new GMarkerEventClickListener() {
    						public void onClick(GMarker marker) {
    							new VehicleInfoDialog(vehicle);
    						}
    
    						public void onDblClick(GMarker marker) {
    						}
    			});
                markers.add(marker);
            }
		}

		return getListAsGMarkerArray(markers);
	}

	
    private VehicleData getVehicleData(VehicleLogData logData) {
        VehicleData result = new VehicleData();
        result.id = m_vehicleData.id;
        result.itemId = m_vehicleData.itemId;
        result.name = m_vehicleData.name;
        result.itemOwner = m_vehicleData.itemOwner;
        
        result.latitude = logData.latitude.doubleValue();
        result.longitude = logData.longitude.doubleValue();
        result.networkId = logData.networkId;
        result.status = logData.status;
        result.temperature = logData.temperature;
        result.humidity = logData.humidity;
        
        return result;
    }
    
    private GLatLng[] getListAsGLatLngArray(ArrayList list) {
        GLatLng[] result = new GLatLng[list.size()];
        for ( int i = 0; i < list.size(); i++ ) {
            result[i] = (GLatLng) list.get(i);
        }
        return result;
    }

    private GMarker[] getListAsGMarkerArray(ArrayList list) {
        GMarker[] result = new GMarker[list.size()];
        for ( int i = 0; i < list.size(); i++ ) {
            result[i] = (GMarker) list.get(i);
        }
        return result;
    }

    
	private GMap2 getMap() {
		return m_mapWidget.getGmap();
	}
	
	private void hideDialog() {
		RootPanel.get().remove(this);
	}
}
