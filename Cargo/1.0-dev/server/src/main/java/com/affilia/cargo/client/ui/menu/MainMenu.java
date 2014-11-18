/**
 * $Id: MainMenu.java 282 2007-06-20 12:43:15Z moritur $
 */
package com.affilia.cargo.client.ui.menu;

import com.google.gwt.user.client.Command;
import com.google.gwt.user.client.Window;
import com.google.gwt.user.client.ui.MenuBar;

public class MainMenu extends MenuBar {

    private static final String MENU_CSS = "ll-menu";
    
    private static final String SUB_MENU_CSS = "ll-sub-menu";
    
    private static final String MAIN_MENU_USERS_MSG = "Units";
    
    public static final String SEARCH_UNIT_MENU_MSG = "Search Unit...";
    private static final String CREATE_NETWORK_MENU_MSG = "Create Network...";
    private static final String ADD_UNIT_MENU_MSG = "Add Unit...";
    public static final String VEHICLE_HISTORY_MENU_MSG = "Unit History...";
    
    
    private static final String MAIN_MENU_ADMINISTRATION_MSG = "Administration";
    public static final String USERS_MANAGEMENT_MENU_MSG = "Users management";
    
    public MainMenu() {
        
        addStyleName(MainMenu.MENU_CSS);
        
        MenuBar unitsMenu = new MenuBar(true);

        unitsMenu.addStyleName(MainMenu.SUB_MENU_CSS);

//        unitsMenu.addItem(MainMenu.CREATE_NETWORK_MENU_MSG, new DummyCommand("Create Network"));
        
//        unitsMenu.addItem(MainMenu.ADD_UNIT_MENU_MSG, new DummyCommand("Add Unit"));
        
        unitsMenu.addItem(
        		MainMenu.VEHICLE_HISTORY_MENU_MSG, 
        		new VehicleHistoryCommand());
        
        unitsMenu.addItem(
                MainMenu.SEARCH_UNIT_MENU_MSG,                
                new SearchVehicleCommand());

        addItem(MainMenu.MAIN_MENU_USERS_MSG, unitsMenu);

        
        MenuBar adminsMenu = new MenuBar(true);
        adminsMenu.addStyleName(MainMenu.SUB_MENU_CSS);

        adminsMenu.addItem(MainMenu.USERS_MANAGEMENT_MENU_MSG, new UsersListCommand());
        
        addItem(MainMenu.MAIN_MENU_ADMINISTRATION_MSG, adminsMenu);

    }
    
    public class DummyCommand implements Command {
        private String m_message = null;

        public DummyCommand(final String message) {
            m_message = message;
        }

        public void execute() {
            Window.alert(m_message);
        }  
    };
    
    
    


}
