/**
 * $Id: MainToolBar.java 286 2007-06-21 13:35:33Z moritur $
 */
package com.affilia.cargo.client.ui.menu;

import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.HorizontalSplitPanel;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.Widget;
import com.logicland.gwt.message.client.ImageButton;

public class MainToolBar extends SimplePanel {
    
    private static final String TOOLBAR_CSS = "ll-toolbar";
    private static final String SUB_TOOLBAR_CSS = "ll-subToolbar";

    private static final String VEHICLE_SEARCH_PIC = "img/search_24.png";
    private static final String VEHICLE_HISTORY_PIC = "img/history_24.png";
    
    private static final String USER_LIST_PIC = "img/group_24.png";
    
    private ImageButton m_searchButtom;
    private ImageButton m_historyButtom;
    private ImageButton m_userMenegersButtom;
    
    public MainToolBar() {
        
        setStyleName(MainToolBar.TOOLBAR_CSS);
        
        
       HorizontalPanel unitsToolbur = new HorizontalPanel();
       unitsToolbur.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_LEFT);
        
        m_searchButtom = new ImageButton(
                MainToolBar.VEHICLE_SEARCH_PIC,
                new ClickListener() {
                    public void onClick(Widget widget) {
                        new SearchVehicleCommand().execute();
                    }
                },
                MainMenu.SEARCH_UNIT_MENU_MSG);
        unitsToolbur.add(m_searchButtom);
        
        m_historyButtom = new ImageButton(
                MainToolBar.VEHICLE_HISTORY_PIC,
                new ClickListener() {
                    public void onClick(Widget widget) {
                        new VehicleHistoryCommand().execute();
                    }
                },
                MainMenu.VEHICLE_HISTORY_MENU_MSG);
        unitsToolbur.add(m_historyButtom);
                
        HorizontalPanel adminToolbur = new HorizontalPanel();
        adminToolbur.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_LEFT);

        
        m_userMenegersButtom = new ImageButton(
                MainToolBar.USER_LIST_PIC,
                new ClickListener() {
                    public void onClick(Widget widget) {
                        new UsersListCommand().execute();
                    }
                },
                MainMenu.USERS_MANAGEMENT_MENU_MSG);
        adminToolbur.add(m_userMenegersButtom);
        
        
        HorizontalSplitPanel subToolbar = new HorizontalSplitPanel();
        subToolbar.setLeftWidget(unitsToolbur);
        subToolbar.setRightWidget(adminToolbur);
        
        add(subToolbar);
        

    }

}
