/**
 * $Id: CargoServiceLocator.java 183 2007-05-16 15:34:31Z moritur $
 */

package com.affilia.cargo.client.service;

import com.google.gwt.core.client.GWT;
import com.google.gwt.user.client.rpc.ServiceDefTarget;

public class CargoServiceLocator
{
    public static CargoServiceAsync getService() {
        CargoServiceAsync service = (CargoServiceAsync) GWT.create(CargoService.class);
        ServiceDefTarget endpoint = (ServiceDefTarget) service;
        String moduleRelativeURL = GWT.getModuleBaseURL() + "cargo.gwt";
        endpoint.setServiceEntryPoint(moduleRelativeURL);
        return service;
    }

}
