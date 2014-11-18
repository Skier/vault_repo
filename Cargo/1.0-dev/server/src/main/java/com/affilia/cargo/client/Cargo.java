/**
 * $Id: Cargo.java 287 2007-06-22 14:11:08Z moritur $
 *
 * Copyright (C) 2006 Logic Land Ltd.
 */

package com.affilia.cargo.client;

import com.affilia.cargo.client.ui.log.LogTable;
import com.affilia.cargo.client.ui.map.CargoMapFactory;
import com.affilia.cargo.client.ui.menu.MainMenu;
import com.affilia.cargo.client.ui.menu.MainToolBar;
import com.affilia.cargo.client.ui.tree.VehicleTree;
import com.google.gwt.core.client.EntryPoint;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.HorizontalSplitPanel;
import com.google.gwt.user.client.ui.MenuBar;
import com.google.gwt.user.client.ui.RootPanel;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;

public class Cargo
    implements EntryPoint, TimerListener {
    
    private static final String MAIN_CONTAINER_CSS = "ll-main-container";
    
    private static final String WORK_CONTAINER_CSS = "ll-work-container";
    private static final String TREE_CONTAINER_CSS = "ll-tree-container";
    private static final String MAP_LOG_CONTAINER_CSS = "ll-map-log-container";

    private static final int TIME_SHEDULE = 10000;

    
    private VerticalPanel mainPanel = new VerticalPanel();
    
    private Timer m_timer; 
    
    private VehicleTree m_vehicleTree;
    private LogTable m_logTable;
    
    private Grid m_mapLogGrid = new Grid(2,1);

    private CargoMapFactory m_cargoMapFactory;
    
    public void onModuleLoad() {
        
       m_timer = new Timer(this);
       
       m_timer.scheduleRepeating(Cargo.TIME_SHEDULE);
       
       m_cargoMapFactory = new CargoMapFactory(this);
        
       mainPanel.setStyleName(Cargo.MAIN_CONTAINER_CSS);
       
       RootPanel.get().add(mainPanel);
       mainPanel.add(makeMenu());
       mainPanel.add(makeToolbar());       
       
       SimplePanel unitTreePanel = new SimplePanel();
       unitTreePanel.setStyleName(Cargo.TREE_CONTAINER_CSS);
       unitTreePanel.add(makeUnitTree());
       
       
       m_mapLogGrid.setStyleName(Cargo.MAP_LOG_CONTAINER_CSS);
       updateCargoMap();
       m_mapLogGrid.setWidget(1, 0, makeLogBox());

       HorizontalSplitPanel m_workSplitPanel = new HorizontalSplitPanel();
       m_workSplitPanel.setLeftWidget(unitTreePanel);
       m_workSplitPanel.setRightWidget(m_mapLogGrid);
       m_workSplitPanel.setSplitPosition("230px");
              
       mainPanel.add(m_workSplitPanel);
       
    }
    
    public void onTime() {
        m_vehicleTree.reload();
        m_logTable.reload();
        m_cargoMapFactory.getCargoMap().reload();
    }
    
    public void updateCargoMap() {
        m_mapLogGrid.setWidget(0, 0, m_cargoMapFactory.getCargoMap().getWidget());
    }

    private MenuBar makeMenu() {
        return new MainMenu();
    }

    private Widget makeToolbar() {
        return new MainToolBar();
    }

    private Widget makeUnitTree() {
        m_vehicleTree = new VehicleTree(m_cargoMapFactory);
        return m_vehicleTree.getWidget();
    }

    private Widget makeLogBox() {
        
        m_logTable = new LogTable();
        return m_logTable.getWidget();

    }

}
