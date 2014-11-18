/**
 * $Id: LogData.java 260 2007-06-07 06:32:53Z hatu $
 */
package com.affilia.cargo.client.data;

import com.google.gwt.user.client.rpc.IsSerializable;

public class LogData implements IsSerializable {
    public Long id;
    public String vehicleName;
    public Integer networkId;
    public String time;
    public Integer level;
    public String  text;
    public Boolean is_join;
    public Boolean is_leave;
    public Boolean is_authorized;
}
