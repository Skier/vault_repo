/**
 * $Id: VehicleOutNetworkTimer.java 255 2007-06-06 13:00:22Z moritur $
 */
package com.affilia.cargo.server;

import java.sql.Connection;
import java.sql.SQLException;
import java.util.Timer;
import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;
import com.affilia.cargo.data.CargoDAO;
import com.affilia.cargo.data.CargoDAOImpl;
import com.affilia.cargo.data.ConnectionHelper;
import com.logicland.application.core.logger.LogHelper;

public class VehicleOutNetworkTimer 
    implements ServletContextListener {
    
    private static final long DATA_TESTING_PERIOD = 2*VehicleOutNetworkTask.VEHICLE_NOT_RESPOND_PERIOD;

    private Connection m_connection;
    private CargoDAO m_cargoDAO ;

    private Timer m_timer;

    
    public void contextInitialized(ServletContextEvent sce) {
        LogHelper.getLogger().debug("VehicleOutNetworkTimer.contextInitialized() call");
        m_connection =  ConnectionHelper.getInstance().getConnection();
        m_cargoDAO = new CargoDAOImpl(m_connection);
      
        m_timer  = new Timer();
        m_timer.schedule(
                new VehicleOutNetworkTask(m_cargoDAO), 
                VehicleOutNetworkTimer.DATA_TESTING_PERIOD,
                VehicleOutNetworkTimer.DATA_TESTING_PERIOD);
    }

    
    public void contextDestroyed(ServletContextEvent sce) {
        LogHelper.getLogger().debug("VehicleOutNetworkTimer.contextDestroyed() call");
        try {
            m_connection.close();
        } catch (SQLException ex) {
            LogHelper.getLogger().error("VehicleOutNetworkTimer.contextDestroyed() Error", ex);
            throw new RuntimeException(ex);
        }
    }

}
