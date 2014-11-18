/**
 * $Id: CargoMapImpl.java 288 2007-06-25 08:29:11Z moritur $
 */

package com.affilia.cargo.client.ui.map;

import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.service.CargoServiceAsync;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.ui.VehicleInfoDialog;
import com.affilia.cargo.client.ui.util.MapWidget;
import com.google.gwt.user.client.rpc.AsyncCallback;
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

public class CargoMapImpl extends MapWidget 
    implements CargoMap {

	private static final String ICON_IMAGE_PATH = "img/unit.png";
	private static final String ICON_SHADOW_PATH = "img/unit_shadow.png";
    
    public static final String WIDTH = "100%";
    public static final String HEIGHT = "450px";
//    public static final String HEIGHT = "100%";

    
    private Integer m_networkId;
    
	private final CargoServiceAsync m_service = 
		CargoServiceLocator.getService();

	
	public CargoMapImpl() {
		setWidth(CargoMapImpl.WIDTH);
		setHeight(CargoMapImpl.HEIGHT);
	}
	
    public void reload() {
        if ( null != m_networkId ) {
//            loadVehicles();
            loadVehiclesAndSetStartPosition();
        }
    }
    
    public Widget getWidget() {
        return this;
    }
    
    public void centerMap(VehicleData vehicle) {
        GLatLng point = new GLatLng(vehicle.latitude, vehicle.longitude);
        getMap().setCenter(point);
    }

    
    public void showNewNetwork(Integer networkId) {
        m_networkId = networkId;
        loadVehiclesAndSetStartPosition();
    }

    private void loadVehicles() {
        AsyncCallback callback = new AsyncCallback() {
            public void onFailure(Throwable caught) {

            }

            public void onSuccess(Object result) {
                addVehiclesOnMap((VehicleData[]) result);
            }
        };
        m_service.getVehiclesByNetwork(m_networkId, callback);
    }
    
    
	private void loadVehiclesAndSetStartPosition() {
		AsyncCallback callback = new AsyncCallback() {
			public void onFailure(Throwable caught) {

			}

			public void onSuccess(Object result) {
                VehicleData[] vehicles = (VehicleData[]) result;
                getMap().setZoom(getZoom(getVehiclesPoints(vehicles)));
                centerMap(getVehiclesPoints(vehicles), getZoom(getVehiclesPoints(vehicles)));
				addVehiclesOnMap(vehicles);
			}
		};
		m_service.getVehiclesByNetwork(m_networkId, callback);
	}      
    
	private void addVehiclesOnMap(VehicleData[] vehicles) {

		GMarker[] markers = getMarkers(vehicles);

        GMarkerManager markerManager = new GMarkerManager(getMap());

        getMap().clearOverlays();

        markerManager.addMarkers(markers, getZoom(getVehiclesPoints(vehicles)));

		markerManager.refresh();
	}

	private GMarker[] getMarkers(VehicleData[] vehicles) {
		GMarkerEventManager markerEventManager = GMarkerEventManager.getInstance();		
		GMarker[] markers = new GMarker[vehicles.length];
		GMarkerOptions markerOptions = new GMarkerOptions();
		
		// Setup icon & icon shadow image (It's advisable: set all properties
		// for icon, otherwise you don't see them)
		GIcon icon = new GIcon();
		icon.setImage(CargoMapImpl.ICON_IMAGE_PATH);
		icon.setShadow(CargoMapImpl.ICON_SHADOW_PATH);
        icon.setIconSize(new GSize(16, 24));
        icon.setShadowSize(new GSize(33, 30));
        icon.setIconAnchor(new GPoint(8, 24));

		icon.setInfoWindowAnchor(new GPoint(5, 1));
		
		markerOptions.setIcon(icon);

		for (int i = 0; i < vehicles.length; i++) {
			final VehicleData vehicle = vehicles[i];
			final GLatLng coord = new GLatLng(vehicles[i].latitude,
					vehicles[i].longitude);
			
			markers[i] = new GMarker(coord, markerOptions);
			
			markerEventManager.addOnClickListener(markers[i], 
					new GMarkerEventClickListener() {
						public void onClick(GMarker marker) {
							new VehicleInfoDialog(vehicle);
						}

						public void onDblClick(GMarker marker) {
						}
			});
		}

		return markers;
	}

	private GLatLng[] getVehiclesPoints(VehicleData[] vehicles) {
		GLatLng[] points = new GLatLng[vehicles.length];
		for (int i = 0; i < vehicles.length; i++) {
			points[i] = new GLatLng(vehicles[i].latitude, vehicles[i].longitude);
		}
		return points;
	}
    

}
