/**
 * $Id: ExceptionProxy.java 246 2007-06-04 15:18:07Z moritur $
 */
package com.affilia.cargo.server;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.MissingResourceException;
import java.util.ResourceBundle;
import com.affilia.cargo.data.CargoDAO;
import com.logicland.application.core.logger.LogHelper;
import com.logicland.application.core.system.ExceptionProcessor;
import com.logicland.application.core.system.SystemException;
import com.logicland.gwt.util.client.system.ClientException;

public class ExceptionProxy 
    implements InvocationHandler {
    
    private static final String CARGO_RESOURCES = "cargo-resources";
    
    private CargoDAO m_cargoDAOImpl;
    
    private static ResourceBundle resources = null;
    
    protected ExceptionProxy(CargoDAO cargoDAOImpl) {
        m_cargoDAOImpl = cargoDAOImpl;
    }
    
    public Object invoke(Object proxy, Method method, Object[] args)
        throws Throwable {
        
        LogHelper.getLogger().debug("ExceptionProxy.invoke() call");

        Object result;
        try {
            result = method.invoke(m_cargoDAOImpl, args);
        } catch (InvocationTargetException e) {
            LogHelper.getLogger().debug("TransactionalProxy.invoke: invocation exception", e.getTargetException());
            throw new ClientException(ExceptionProcessor.process(e.getTargetException(), 
                    getResources()));
        } catch (Throwable e) {
            throw new ClientException(ExceptionProcessor.process(e,getResources()));
        }
        
        return result;
    }
    
    private ResourceBundle getResources() {
        if ( null == resources ) {
            try {
                resources = ResourceBundle.getBundle(ExceptionProxy.CARGO_RESOURCES);
            } catch (MissingResourceException ex) {
                LogHelper.getLogger().error("ExceptionProxy.getResources: resources.properties is not found", ex);
                throw new SystemException("ExceptionProxy.getResources: resources.properties is not found", ex);
            }
        }
        return resources;
    }

}
