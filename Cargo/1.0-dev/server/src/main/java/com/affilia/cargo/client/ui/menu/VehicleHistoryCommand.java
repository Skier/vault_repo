/**
 * $Id: VehicleHistoryCommand.java 232 2007-05-31 15:41:23Z moritur $
 *
 */
package com.affilia.cargo.client.ui.menu;

import com.google.gwt.user.client.Command;

public class VehicleHistoryCommand implements Command {
	
	public VehicleHistoryCommand() {
		
	}

	public void execute() {
		new VehicleHistoryDialog();
	}

}
