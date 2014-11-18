/**
 * $Id: CargoMapProxy.java 277 2007-06-19 10:05:39Z moritur $
 */
package com.affilia.cargo.client.ui.map;

import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.Widget;

public class CargoMapProxy implements CargoMap {
    
    private Label m_simpleLabel = new Label();

    protected CargoMapProxy() {
        m_simpleLabel.setHeight(CargoMapImpl.HEIGHT);
        m_simpleLabel.setWidth(CargoMapImpl.WIDTH);
    }
    
    public Widget getWidget() {
        return m_simpleLabel;
    }
    
    public void reload() {
        //do nothing
    }

}
