/**
 * $Id: UserData.java 225 2007-05-29 16:10:59Z hatu $
 *
 */
package com.affilia.cargo.client.data;

import com.google.gwt.user.client.rpc.IsSerializable;

public class UserData implements IsSerializable {
	public Integer id;
	public String userName;
	public String password;
}
