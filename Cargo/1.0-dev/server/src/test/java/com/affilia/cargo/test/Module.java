/**
 * $Id: Module.java 173 2007-05-15 12:34:44Z moritur $
 */
package com.affilia.cargo.test;

import com.logicland.application.core.data.AbstractModule;

public class Module 
    extends AbstractModule{

    private final static String NAME = "cargo-data-test";

    private final static Module module = new Module();

    public static Module getInstance() {
        return module;
    }

    private Module() {
    }

    public String getName() {
        return Module.NAME;
    }

}
