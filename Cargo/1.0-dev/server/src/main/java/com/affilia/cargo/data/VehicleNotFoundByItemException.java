/**
 * $Id: VehicleNotFoundByItemException.java 201 2007-05-23 07:59:18Z moritur $
 */
package com.affilia.cargo.data;

import com.logicland.application.core.system.ApplicationException;

public class VehicleNotFoundByItemException 
    extends ApplicationException {

    private static final String ITEM_ID_KEY = "itemId";

    public VehicleNotFoundByItemException(String itemId) {
        getContext().put(VehicleNotFoundByItemException.ITEM_ID_KEY, itemId);
    }
}
