/**
 * $Id: ExceptionProxyFactory.java 199 2007-05-22 14:26:28Z moritur $
 */
package com.affilia.cargo.server;

import java.lang.reflect.Proxy;

import com.affilia.cargo.data.CargoDAO;

public class ExceptionProxyFactory {

    public static CargoDAO createExceptionProxy(CargoDAO cargoDAOImpl) {
        
        ExceptionProxy exceptionProxy = new ExceptionProxy(cargoDAOImpl);
        
        return (CargoDAO) Proxy.newProxyInstance(
                cargoDAOImpl.getClass().getClassLoader(),
                cargoDAOImpl.getClass().getInterfaces(),
                exceptionProxy);
        
    }
    
}
