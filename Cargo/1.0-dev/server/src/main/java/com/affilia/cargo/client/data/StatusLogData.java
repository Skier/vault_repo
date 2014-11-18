/**
 * $Id: StatusLogData.java 165 2007-05-14 14:59:34Z hatu $
 * 
 */
package com.affilia.cargo.client.data;

import com.google.gwt.user.client.rpc.IsSerializable;

public class StatusLogData implements IsSerializable {
	
	public Long id;
	public String latitude;
	public String longtude;
	public String status;
	public String temperature;
	public String humidity;

}
