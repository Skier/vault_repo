/**
 * $Id: UsersListCommand.java 232 2007-05-31 15:41:23Z moritur $
 */
package com.affilia.cargo.client.ui.menu;

import com.google.gwt.user.client.Command;

public class UsersListCommand implements Command {

    public UsersListCommand() {
    }

    public void execute() {
        new UsersListDialog();
    }

}
