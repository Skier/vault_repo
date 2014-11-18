/**
 * $Id: MapWidget.java 273 2007-06-16 08:31:05Z moritur $
 *
 */
package com.affilia.cargo.client.ui.util;

import com.mapitz.gwt.googleMaps.client.GLatLng;
import com.mapitz.gwt.googleMaps.client.GLatLngBounds;
import com.mapitz.gwt.googleMaps.client.GMap2;
import com.mapitz.gwt.googleMaps.client.GMap2Widget;

public class MapWidget extends GMap2Widget{
	
	public MapWidget() {
		super();
	}
	
	public MapWidget(String height, String width) {
		super(height, width);
	}
	
	public GLatLng getMaxNorthEast(GLatLng[] points) {
		double maxLat = -90;
		double maxLng = -180;
		for (int i = 0; i < points.length; i++) {
			if (maxLat <= points[i].lat()) {
				maxLat = points[i].lat();
			}
			if (maxLng <= points[i].lng()) {
				maxLng = points[i].lng();
			}
		}

		return new GLatLng(maxLat, maxLng);
	}

	public GLatLng getMaxSouthWest(GLatLng[] points) {
		double maxLat = 90;
		double maxLng = 180;
		for (int i = 0; i < points.length; i++) {
			if (maxLat >= points[i].lat()) {
				maxLat = points[i].lat();
			}
			if (maxLng >= points[i].lng()) {
				maxLng = points[i].lng();
			}
		}

		return new GLatLng(maxLat, maxLng);
	}
	
    public void centerMap(GLatLng point) {
        getMap().setCenter(point);
    }

    
    public void centerMap(GLatLng[] points, int zoomLevel) {
        GLatLngBounds bounds = new GLatLngBounds(getMaxSouthWest(points),
				getMaxNorthEast(points));
		getMap().setCenter(bounds.getCenter(), zoomLevel);
	}
    
    public int getZoom(GLatLng[] points) {
        GLatLngBounds bounds = new GLatLngBounds(getMaxSouthWest(points),
                getMaxNorthEast(points));
        return getMap().getBoundsZoomLevel(bounds);
    }

	
	public GMap2 getMap() {
		return getGmap();
	}
	
	public void setHeight(String height) {
		super.setHeight(height);
	}
	
	public void setWidth(String width) {
		super.setWidth(width);
	}


}
