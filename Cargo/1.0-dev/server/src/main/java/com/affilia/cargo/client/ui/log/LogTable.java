/**
 * $Id: LogTable.java 272 2007-06-15 15:09:25Z as $
 */
package com.affilia.cargo.client.ui.log;

import com.affilia.cargo.client.data.LogData;
import com.affilia.cargo.client.service.CargoServiceAsync;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.ui.LogInfoDialog;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.FlexTable;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.ScrollPanel;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;

public class LogTable {
    
    private final static int NUMBER_OF_ROWS = 30;
    
    private static final String LOG_CONTAINER_CSS = "ll-log-container";
    
    private static final String LOG_CSS = "ll-log";
    
    private static final String LEAVE_INFO_ROW_LOG_CSS = "ll-log-leaveInfoRow";
    
    private static final String TH_LEVEL_CSS = "ll-log-th-level";
    private static final String TH_VEHICLE_CSS = "ll-log-th-vehicle";
    private static final String TH_MESSAGE_CSS = "ll-log-th-message";
    private static final String TH_TIMESTEP_CSS = "ll-log-th-timestep";
    private static final String TH_HIDE_SCROLL_CSS = "ll-log-th-hide-scroll";
    private static final String LABEL_HIDE_SCROLL_CSS = "ll-log-label-hide-scroll";
    
    private static final String TD_LEVEL_CSS = "ll-log-td-level";
    private static final String TD_VEHICLE_CSS = "ll-log-td-vehicle";
    private static final String TD_MESSAGE_CSS = "ll-log-td-message";
    private static final String TD_TIMESTEP_CSS = "ll-log-td-timestep";
    
    private static final String LEVEL_MSG = "Level";
    private static final String VEHICLE_MSG = "Vehicle";
    private static final String MESSAGE_MSG = "Message";
    private static final String TIMESTEP_MSG = "Timestamp";
    
    
    private CargoServiceAsync m_service = CargoServiceLocator.getService();

    private VerticalPanel m_logPanel = new VerticalPanel();

    private FlexTable m_logTable = new FlexTable();
    
    private Long m_lastLogId = null;
    
    private ScrollPanel m_scrollPanell = null;
    
    public LogTable() {
        
        m_logPanel.setStyleName(LogTable.LOG_CONTAINER_CSS);

        Grid header = new Grid(1,5);
        
        header.setText(0, 0, LogTable.LEVEL_MSG);
        header.getCellFormatter().setStyleName(0, 0, LogTable.TH_LEVEL_CSS);

        header.setText(0, 1, LogTable.VEHICLE_MSG);
        header.getCellFormatter().setStyleName(0, 1, LogTable.TH_VEHICLE_CSS);
        
        header.setText(0, 2, LogTable.MESSAGE_MSG);
        header.getCellFormatter().setStyleName(0, 2, LogTable.TH_MESSAGE_CSS);

        header.setText(0, 3, LogTable.TIMESTEP_MSG);
        header.getCellFormatter().setStyleName(0, 3, LogTable.TH_TIMESTEP_CSS);

        
        Label hideScroll = new Label("");
        hideScroll.setStyleName(LogTable.LABEL_HIDE_SCROLL_CSS);
        header.setWidget(0, 4, hideScroll);
        header.getCellFormatter().setStyleName(0, 4, LogTable.TH_HIDE_SCROLL_CSS);
        
        m_logPanel.add(header);
        
        m_logTable.setCellPadding(1);

        m_scrollPanell = new ScrollPanel();
        m_scrollPanell.add(m_logTable);
        m_scrollPanell.setStyleName(LogTable.LOG_CSS);
        
        m_logPanel.add(m_scrollPanell);
        
        loadLastLogs();
        
//        loadLogs(new Long(-10));
        
    }
    
    public Widget getWidget() {
        return m_logPanel;
    }
    
    public void reload() {
        if ( null == m_lastLogId ) {
            loadLastLogs();
        } else {
            loadLogs(m_lastLogId);
        }
    }
    
    
    private void loadLastLogs() {
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
            }

            public void onSuccess(Object result) {
                m_lastLogId = (Long) result;
                loadLogs(m_lastLogId);            }
        };
        m_service.getMaxLogId(callback);
    }
    
    
    private void loadLogs(Long lastLogId) {
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
            }

            public void onSuccess(Object result) {
                LogData[] logs = (LogData[]) result;
                populateLogs(logs);
            }
        };
        m_service.getLogInformation(lastLogId, callback);
    }
    
    private void populateLogs(LogData[] logs) {
        for ( int i=0; i<logs.length; i++ ) {
            if (  LogTable.NUMBER_OF_ROWS == m_logTable.getRowCount() ) {
                m_logTable.removeRow(m_logTable.getRowCount()-1); 
            }            
//            populateRowLog(m_logTable.getRowCount(), logs[i]);
            
            m_logTable.insertRow(0);
            populateRowLog(0, logs[i]);
        }
        if ( 0 != logs.length ) {
            m_lastLogId = logs[logs.length-1].id;
        }
        m_scrollPanell.setScrollPosition(0);
    }
    
    private void populateRowLog(int rowNum, LogData log) {
    	
        m_logTable.setWidget(rowNum, 0, new Label(log.level.toString()));
        
        m_logTable.getCellFormatter().setStyleName(rowNum, 0, LogTable.TD_LEVEL_CSS);

        m_logTable.setWidget(rowNum, 1, new Label(log.vehicleName));
        m_logTable.getCellFormatter().setStyleName(rowNum, 1, LogTable.TD_VEHICLE_CSS);

        m_logTable.setWidget(rowNum, 2, new Label(log.text));
        m_logTable.getCellFormatter().setStyleName(rowNum, 2, LogTable.TD_MESSAGE_CSS);

        m_logTable.setWidget(rowNum, 3, new Label(log.time));
        m_logTable.getCellFormatter().setStyleName(rowNum, 3, LogTable.TD_TIMESTEP_CSS);
        
        if (Boolean.TRUE.equals(log.is_leave)) {
        	m_logTable.getRowFormatter().setStyleName(rowNum, LogTable.LEAVE_INFO_ROW_LOG_CSS);
    		new LogInfoDialog(log, LogInfoDialog.LEAVE_INFO);
    	}

        
    }
    
}
