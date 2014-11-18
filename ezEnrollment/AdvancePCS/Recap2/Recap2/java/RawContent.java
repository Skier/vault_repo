/* $Id: RawContent.java,v 1.1 2003/06/09 16:12:36 developer Exp $
 *
 */

import java.io.*;
import java.text.*;
import java.util.*;
import javax.servlet.*;
import javax.servlet.http.*;

/**
 * The simplest possible servlet.
 *
 * @author James Duncan Davidson
 */

public class RawContent extends HttpServlet {

    public void doPost(HttpServletRequest request,
                      HttpServletResponse response)
        throws IOException, ServletException
    {
        BufferedReader r = request.getReader();
        PrintWriter out = response.getWriter();
        response.setContentType("plain/text");
	Map params = request.getParameterMap();
	if ( !params.containsKey("filename") ) {
	    out.print("ERROR-1: Invalid request. Parameter 'filename' not found");
	    return;
	}
	if ( !params.containsKey("username") ) {
	    out.print("ERROR-1: Invalid request. Parameter 'username' not found");
	    return;
	}
	if ( !params.containsKey("password") ) {
	    out.print("ERROR-1: Invalid request. Parameter 'password' not found");
	    return;
	}
	if ( !("USERID".equals(request.getParameter("username"))) ) {
	    out.print("ERROR-2: Invalid username or password");
	    return;
	}
	if ( !("PASSWORD".equals(request.getParameter("password"))) ) {
	    out.print("ERROR-2: Invalid username or password");
	    return;
	}

	java.lang.String fileName = request.getParameter("filename");
	FileOutputStream os = new FileOutputStream(fileName);
        int c = 0;
        while ( true ) {
            try {
                c = r.read();
		if ( -1 == c ) {
		    break;
		}
            } catch ( IOException ex ) {
                // expected
                break;
            };
           os.write(c);
        }
	out.print("OK");
    }
}



