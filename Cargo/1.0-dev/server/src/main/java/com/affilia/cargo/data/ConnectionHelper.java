/**
 * $Id: ConnectionHelper.java 252 2007-06-05 15:43:26Z hatu $
 */
package com.affilia.cargo.data;

import java.sql.Connection;
import java.sql.SQLException;

import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.sql.DataSource;

public class ConnectionHelper {
    
    public static ConnectionHelper instance = new ConnectionHelper();
    
    public static ConnectionHelper getInstance() {
        return instance;
    }
    
    private ConnectionHelper() {
    }
    
    public Connection getConnection() {
        try {
            
            //   Obtain our environment naming context
                Context initCtx = new InitialContext();
                Context envCtx = (Context) initCtx.lookup("java:comp/env");
            
            //   Look up our data source
                DataSource ds = (DataSource)envCtx.lookup("jdbc/cargo");
            
            //   Allocate and use a connection from the pool
                return ds.getConnection();
        } catch ( SQLException ex ) {
            ex.printStackTrace(System.err);
            return null;
        } catch ( NamingException ex ) {
            ex.printStackTrace(System.err);
            return null;
        }
                
    }

}
