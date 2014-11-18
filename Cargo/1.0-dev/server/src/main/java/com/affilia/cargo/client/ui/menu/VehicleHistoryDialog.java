/**
 * $Id: VehicleHistoryDialog.java 286 2007-06-21 13:35:33Z moritur $
 *
 */
package com.affilia.cargo.client.ui.menu;

import com.affilia.cargo.client.data.VehicleData;
import com.affilia.cargo.client.data.VehicleLogData;
import com.affilia.cargo.client.service.CargoServiceAsync;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.ui.util.EditDialog;
import com.affilia.cargo.client.ui.util.ErrorWindow;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.KeyboardListener;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.ListBox;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.logicland.gwt.util.client.util.DateHelper;

public class VehicleHistoryDialog extends EditDialog implements ClickListener{
	
	private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";
	
	private final CargoServiceAsync m_service = 
		CargoServiceLocator.getService();

	
	private TextBox m_vehicleItemIdTextBox = new TextBox();
	private ListBox m_intervalType = new ListBox();
	private TextBox m_dateFromTextBox = new TextBox();
	private TextBox m_dateToTextBox = new TextBox();
	private TextBox m_intervalValue = new TextBox();
	private Button m_buttonOk = new Button("Ok", this);
	private Button m_buttonCancel = new Button("Cancel", this);
	
	public VehicleHistoryDialog() {
		
		//TODO: Add captions in resource
		
		setText("Vehicle History");
		
		VerticalPanel mainPanel = getRootPanel();
		mainPanel.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
		
		Label vehicleLabel = new Label("Vehicle Item ID: ");
		 
		HorizontalPanel unitPanel = new HorizontalPanel();
		unitPanel.add(vehicleLabel);
		unitPanel.add(m_vehicleItemIdTextBox);
		

		HorizontalPanel datePanel = new HorizontalPanel();
		Label dateFromLabel = new Label("From: ");
		Label dateToLabel = new Label("To: ");
		m_dateFromTextBox.setVisibleLength(10);
		m_dateToTextBox.setVisibleLength(10);
		datePanel.add(dateFromLabel);
		datePanel.add(m_dateFromTextBox);
		datePanel.add(dateToLabel);
		datePanel.add(m_dateToTextBox);
		
		
		// "intervalValue" field must be integer type
		Label intervalLabel = new Label("Interval: ");
		m_intervalValue.setVisibleLength(4);
		m_intervalType.addItem("minute(s)", "2");
		m_intervalType.addItem("hour(s)", "3");
		m_intervalType.addItem("day(s)", "4");
		m_intervalType.addItem("week(s)", "5");
		HorizontalPanel intervalPanel = new HorizontalPanel();
		intervalPanel.add(intervalLabel);
		intervalPanel.add(m_intervalValue);
		intervalPanel.add(m_intervalType);
		
		
		
		HorizontalPanel buttonPanel = new HorizontalPanel();
		buttonPanel.add(m_buttonOk);
		buttonPanel.add(m_buttonCancel);
		
		
		mainPanel.add(unitPanel);
		mainPanel.add(datePanel);
		mainPanel.add(intervalPanel);
		mainPanel.add(buttonPanel);
		
		show();
	}
	
	public void onClick(Widget sender) {
        if ( sender == m_buttonOk ) {
        	getVehicleHistory();
        } else if (sender == m_buttonCancel ) {
            hide();
        }
	}
	
	public boolean onKeyDownPreview(char key, int modifiers) {
		switch (key) {
		case KeyboardListener.KEY_ENTER:
			getVehicleHistory();
		case KeyboardListener.KEY_ESCAPE:
			hide();
			break;
		}

		return true;
	}

	public String getStyleName() {
		return VehicleHistoryDialog.DIALOG_BOX_CSS_CLASS;
	}

	
	private void getVehicleHistory() {
		
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
                new ErrorWindow(caught.getMessage());
            }

            public void onSuccess(Object result) {
                getVehicalHistoryPoints((VehicleData) result);
            }
        };
        
        m_service.getVehicle(m_vehicleItemIdTextBox.getText(), callback);

	}
    
    
    private void getVehicalHistoryPoints(final VehicleData vehicleData) {

        long timeStep = getTimeStep();
        
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
                new ErrorWindow(caught.getMessage());
            }

            public void onSuccess(Object result) {
                hide();
                new VehicleTravelDialog(vehicleData, (VehicleLogData[]) result);
            }
        };
        
        m_service.getVehicleHistory(
                m_vehicleItemIdTextBox.getText(),
                DateHelper.stringToDate(m_dateFromTextBox.getText()),
                DateHelper.stringToDate(m_dateToTextBox.getText()),
                timeStep, 
                callback);
    }
    

    private long getTimeStep() {
        long timeStep = 0;
        int interval = new Integer(m_intervalValue.getText()).intValue();
        char typeValue = m_intervalType.getValue(m_intervalType.getSelectedIndex()).charAt(0);
        switch (typeValue) {
        case '1':
            // in seconds
            timeStep = interval * 1000;
            break;
        case '2':
            // in minutes
            timeStep = interval * 60 * 1000;        
            break;
        case '3':
            // in hours
            timeStep = interval * 60 * 60 * 1000;       
            break;
        case '4':
            // in days
            timeStep = interval * 24 * 60 * 60 * 1000;
            break;
        case '5':
            timeStep = interval * 7 * 24 * 60 * 60 * 1000;
            break;

        default:
            break;
        }
        return timeStep;
    }
    
}
