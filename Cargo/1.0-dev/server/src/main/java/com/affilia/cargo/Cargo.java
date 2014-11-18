/**
 * $Id: Cargo.java 183 2007-05-16 15:34:31Z moritur $
 */

package com.affilia.cargo;

import java.io.IOException;

import javax.sql.DataSource;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class Cargo extends HttpServlet {

    public static final String CARGO_PARAM = "cargo_id";
    
	private static final long serialVersionUID = 8689697095013990417L;
  
    
//	@Override
	protected void doGet(HttpServletRequest arg0, HttpServletResponse arg1) throws ServletException, IOException {
		doPut(arg0, arg1);
	}

//	@Override
	protected void doPut(HttpServletRequest request, HttpServletResponse response) 
        throws ServletException, IOException 
    {
		try {
			Connection conn = getConnection();
            try {       
                PreparedStatement stmt = conn.prepareStatement(
                    "select LATITUDE, LONGITUDE, TIME_STAMP " +
                    "  from CARGO_TRACE " +
                    " where CARGO_ID = ? "+ 
                    " order by TIME_STAMP ");
                stmt.setString(1, request.getParameter(CARGO_PARAM));
                ResultSet rs = stmt.executeQuery();
                StringBuffer result = new StringBuffer();
                result.append("<cargo>");
                while ( rs.next() ) {
                    result.append("<point ltd=\"");
                    result.append(rs.getString("LATITUDE"));
                    result.append("\"  lng=\"");
                    result.append(rs.getString("LONGITUDE"));
                    result.append("\"  time=\"");
                    result.append(rs.getString("TIME_STAMP"));
                    result.append("\"> </point>");                    
                }
                result.append("</cargo>");
                response.getWriter().write(result.toString());
	        } finally {
	            conn.close();	
	        }
        } catch ( SQLException ex ) {
            response.getWriter().write(
			"<error  text=\"internal error\"></error>");
        } catch ( NamingException ex ) {
             response.getWriter().write(
                 "<error  text=\"internal error\"></error>");
        }
    }

    private Connection getConnection() 
        throws NamingException, SQLException 
    {	
	//	 Obtain our environment naming context
        Context initCtx = new InitialContext();
        Context envCtx = (Context) initCtx.lookup("java:comp/env");
	
	//	 Look up our data source
        DataSource ds = (DataSource)envCtx.lookup("jdbc/cargo");
	
	//	 Allocate and use a connection from the pool
        return ds.getConnection();
    }
}
