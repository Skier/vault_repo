/**
 * $Id: CargoMapFactory.java 277 2007-06-19 10:05:39Z moritur $
 */
package com.affilia.cargo.client.ui.map;

import com.affilia.cargo.client.Cargo;

public class CargoMapFactory {
    
    private CargoMapProxy m_cargoMapProxy = new CargoMapProxy();
    private CargoMapImpl m_cargoMapImpl = null;

    private Cargo m_cargo = null;
    
    
    public CargoMapFactory(Cargo cargo) {
        m_cargo = cargo;
    }
    
    public CargoMap getCargoMap() {
        if ( null == m_cargoMapImpl ) {
            return m_cargoMapProxy;
        } else {
            return m_cargoMapImpl;
        }
    }
    
    public void cleanCargoMap() {
        m_cargoMapImpl = null; 
        m_cargo.updateCargoMap();
    }
    
    public void createCargoMap(Integer networkId) {
        m_cargoMapImpl = new CargoMapImpl();
        m_cargoMapImpl.showNewNetwork(networkId);
        m_cargo.updateCargoMap();
    }
        
}
