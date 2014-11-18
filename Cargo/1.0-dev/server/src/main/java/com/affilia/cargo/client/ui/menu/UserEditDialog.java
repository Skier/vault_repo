/**
 * $Id: UserEditDialog.java 247 2007-06-04 16:09:09Z hatu $
 *
 */
package com.affilia.cargo.client.ui.menu;

import com.affilia.cargo.client.data.UserData;
import com.affilia.cargo.client.service.CargoServiceAsync;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.ui.util.EditDialog;
import com.affilia.cargo.client.ui.util.ErrorWindow;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.PasswordTextBox;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.logicland.gwt.util.client.util.Callback;

public class UserEditDialog extends EditDialog implements ClickListener{
	
	private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";
	
	private final CargoServiceAsync m_service = 
		CargoServiceLocator.getService();

	
	private Button m_buttonOk = new Button("Ok", this);
	private Button m_buttonCancel = new Button("Cancel", this);
	private TextBox m_userNameTextBox = new TextBox();
	private PasswordTextBox m_passwordTextBox = new PasswordTextBox();
	private PasswordTextBox m_confirmPasswordTextBox = new PasswordTextBox();	
	private Label m_userNameLabel = new Label("User name: ");
	private Label m_passwordLabel = new Label("Password: ");
	private Label m_confirmPasswordLabel = new Label("Confirm Password: ");
	
	private Integer m_id = null; 
	private Callback m_callback = null;
	
	// Constructor for new user
	public UserEditDialog(Callback callback) {
		m_callback = callback;
		setText("Edit User");
		initDialog();
		show();
	}
	
	// Constructor for edit existing user
	public UserEditDialog(UserData data, Callback callback) {
		this(callback);
		setUser(data);
	}
	
	public void onClick(Widget sender) {
        if ( sender == m_buttonOk ) {
    		if (!m_passwordTextBox.getText().equals(
    				m_confirmPasswordTextBox.getText())) {
    			new ErrorWindow("Password and confirm not equals");
    		} else {
    			storeUser(m_id);
    		}
        } else if (sender == m_buttonCancel ) {
            hide();
        }
	}

	
	public String getStyleName() {
		return UserEditDialog.DIALOG_BOX_CSS_CLASS;
	}
	
	private void initDialog() {
		
		VerticalPanel mainPanel = getRootPanel();
		mainPanel.setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
		
		Grid userInfoGrid = new Grid(3, 3);
		userInfoGrid.setWidget(0, 0, m_userNameLabel);
		userInfoGrid.setWidget(0, 1, m_userNameTextBox);
		userInfoGrid.setWidget(1, 0, m_passwordLabel);
		userInfoGrid.setWidget(1, 1, m_passwordTextBox);
		userInfoGrid.setWidget(2, 0, m_confirmPasswordLabel);
		userInfoGrid.setWidget(2, 1, m_confirmPasswordTextBox);

		
		HorizontalPanel buttonPanel = new HorizontalPanel();
		buttonPanel.add(m_buttonOk);
		buttonPanel.add(m_buttonCancel);
		
		mainPanel.add(userInfoGrid);
		mainPanel.add(buttonPanel);
		
	}
	
	private void storeUser(Integer id) {
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
                new ErrorWindow(caught.getMessage());
            }

            public void onSuccess(Object result) {
                hide();
                UserEditDialog.this.m_callback.onCallback(result);
                
            }
        };
        
        m_service.storeUser(id, m_userNameTextBox.getText(), 
        		m_passwordTextBox.getText(), callback);
	}
	
	private void setUser(UserData data) {
		m_id = data.id;
		m_userNameTextBox.setText(data.userName);
		m_passwordTextBox.setText(data.password);
		m_confirmPasswordTextBox.setText(data.password);
	}

}
