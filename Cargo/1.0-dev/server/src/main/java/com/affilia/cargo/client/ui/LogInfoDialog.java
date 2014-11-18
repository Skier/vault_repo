/**
 * $Id: LogInfoDialog.java 273 2007-06-16 08:31:05Z moritur $
 *
 */
package com.affilia.cargo.client.ui;

import com.affilia.cargo.client.data.LogData;
import com.affilia.cargo.client.ui.util.CentralScreenDialogBox;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;

public class LogInfoDialog extends CentralScreenDialogBox {
	
	public static final int JOIN_INFO = 1;
	public static final int LEAVE_INFO = 2;
	public static final int AUTHORIZED_INFO = 3;
	
	private final static String DIALOG_BOX_CSS_CLASS = "ll-AttentionDialogBox";
	
	private final static String JOIN_INFO_CSS_CLASS = "ll-JoinInfoLabel";
	private final static String LEAVE_INFO_CSS_CLASS = "ll-LeaveInfoLabel";
	private final static String AUTHORIZED_INFO_CLASS = "ll-AuthorizedInfoLabel";
	
	
	private Label messageLabel = new Label();
	
	public LogInfoDialog(LogData log, int logInfoType) {
        super(false);
		
		setText("Vehicle Event");
		
        VerticalPanel rootPanel = getRootPanel();
        
        rootPanel.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
        
        switch (logInfoType) {
	        case 1:
	        	messageLabel = new Label("Vehicle " + log.vehicleName + 
	        			" join to network " + log.networkId);
	        	messageLabel.setStyleName(LogInfoDialog.JOIN_INFO_CSS_CLASS);
	        	break;
	        case 2:
	        	messageLabel = new Label("Vehicle " + log.vehicleName + 
	        			" leave network " + log.networkId);
	        	messageLabel.setStyleName(LogInfoDialog.LEAVE_INFO_CSS_CLASS);
	        	break;
	        case 3:
	        	messageLabel = new Label("Vehicle " + log.vehicleName + 
	        			" authorized in network " + log.networkId);
	        	messageLabel.setStyleName(LogInfoDialog.AUTHORIZED_INFO_CLASS);
	        	break;
	        default:
	        	break;
		}

        
        rootPanel.add(messageLabel);
        
        Button closeButton = new Button("Close");
        closeButton.addClickListener(new ClickListener() {
            public void onClick(Widget sender) {
            	hide();
            }
        });

        rootPanel.add(closeButton);
        
        show();
	}
	

	
	public String getStyleName() {
		return LogInfoDialog.DIALOG_BOX_CSS_CLASS;
	}
	
}