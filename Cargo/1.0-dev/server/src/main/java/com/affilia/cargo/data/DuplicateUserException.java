/**
 * $Id: $
 *
 */
package com.affilia.cargo.data;

import com.logicland.application.core.system.ApplicationException;

public class DuplicateUserException extends ApplicationException {
	
    private static final String USER_NAME_KEY = "userName";

    public DuplicateUserException(String userName) {
        getContext().put(DuplicateUserException.USER_NAME_KEY, userName);
    }


}
