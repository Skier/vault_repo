/**
 * $Id: DataReceiverServlet.java 258 2007-06-06 14:41:21Z moritur $
 */

package com.affilia.cargo.server;

import java.io.IOException;
import java.sql.Connection;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import com.affilia.cargo.data.CargoDAO;
import com.affilia.cargo.data.CargoDAOImpl;
import com.affilia.cargo.data.ConnectionHelper;
import com.logicland.application.core.logger.LogHelper;

public class DataReceiverServlet 
    extends HttpServlet {

    
    private Connection m_connection = ConnectionHelper.getInstance().getConnection();;
    
    private CargoDAO m_cargoDAO = new CargoDAOImpl(m_connection);;
    
    
    
    public void doPost(HttpServletRequest request, HttpServletResponse response) 
        throws IOException, ServletException
    {
        LogHelper.getLogger().debug("DataReceiverServlet.doPost: called.");
        String pack = request.getParameter("package");
        if ( null == pack ) {
            throw new RuntimeException("No package.");
        }
        System.out.println("DataReceiverServlet.doPost: package=" + pack);
        LogHelper.getLogger().debug("DataReceiverServlet.doPost: package=" + pack);

        String network = request.getParameter("network");
        if ( null == network ) {
            throw new RuntimeException("No network.");
        }
        System.out.println("DataReceiverServlet.doPost: network=" + network);
        LogHelper.getLogger().debug("DataReceiverServlet.doPost: network=" + network);
        Integer networkId = new Integer(network);

        DataPackage dp = DataPackageFactory.getInstance().createPackage(networkId, pack);
        dp.storeTo(m_cargoDAO);
    }

}
