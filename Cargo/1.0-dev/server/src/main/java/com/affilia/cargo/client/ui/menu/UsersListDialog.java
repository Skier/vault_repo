/**
 * $Id: UsersListDialog.java 281 2007-06-20 11:18:16Z moritur $
 */
package com.affilia.cargo.client.ui.menu;

import com.affilia.cargo.client.data.UserData;
import com.affilia.cargo.client.service.CargoServiceAsync;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.ui.util.ConfirmCallback;
import com.affilia.cargo.client.ui.util.ConfirmWindow;
import com.affilia.cargo.client.ui.util.ErrorWindow;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.Widget;
import com.logicland.gwt.message.client.ImageButton;
import com.logicland.gwt.user.ui.client.widget.DialogWindow;
import com.logicland.gwt.user.ui.client.widget.Table;
import com.logicland.gwt.util.client.util.Callback;

public class UsersListDialog 
    extends DialogWindow 
    implements ClickListener {

    private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";
    private final static String LIST_TABLE_CSS_CLASS = "ll-listTable";
    
    private final static String CAPTION_MSG = "Users list";
    private final static String USER_TABLE_NAME_MSG = "Name";
    
    private final Button m_addButton = new Button("Add", this);
    private final Button m_closeButton = new Button("Close", this);
    
    private Table m_usersTable = new Table();

    private CargoServiceAsync m_service = CargoServiceLocator.getService();

    
    public UsersListDialog() {
        setText(UsersListDialog.CAPTION_MSG);
        
        initRootPanel();
        
        show();

    }
    
    public String getStyleName() {
        return UsersListDialog.DIALOG_BOX_CSS_CLASS;
    }

    public void onClick(Widget source) {
        if ( source == m_closeButton ) {
            hide();
        } else if (source == m_addButton) {
        	new UserEditDialog(new Callback() {
                public void onCallback(Object result) {
                	addUser((UserData) result);
                }
            });
        }
    }
    
    private void initRootPanel() {
        
        getRootPanel().setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
        
		m_usersTable.setStyleName(LIST_TABLE_CSS_CLASS);

        m_usersTable.addTh(UsersListDialog.USER_TABLE_NAME_MSG);
        m_usersTable.addTh("");
        
        HorizontalPanel buttonPanel = new HorizontalPanel();
        buttonPanel.add(m_addButton);
        buttonPanel.add(m_closeButton);
        
        
        getRootPanel().add(m_usersTable);
        getRootPanel().add(buttonPanel);
        
        loadUsers();
    }
    
    private void loadUsers() {
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
                new ErrorWindow(caught.getMessage());
            }

            public void onSuccess(Object result) {
                populateUserTable((UserData[]) result);
            }
        };
        m_service.getUsers(callback);
    }
    
    private void populateUserTable(UserData[] userList) {
    	// Delete all rows from table
    	m_usersTable.removeAllRows();
    	// Populate users list 
        for ( int i=0; i<userList.length; i++ ) {
        	updateUserRow(m_usersTable.getRowCount(), userList[i]);
        }
    }
    
    private void updateUserRow(final int row, final UserData user) {
    	
        m_usersTable.setText(row, 0, user.userName);
                
        ImageButton editButton = new ImageButton(
                "img/edit.png", 
                new ClickListener() {
                    public void onClick(Widget widget) {
                    	new UserEditDialog(user,                        
                    		new Callback() {
                            	public void onCallback(Object result) {
                            		updateUserRow(row, (UserData)result);
                            	}
                        	});
                    }
                },
                "Edit");
        ImageButton delButton = new ImageButton("img/delete.png", 
                new ClickListener() {
                    public void onClick(Widget widget) {
                        new ConfirmWindow("Are you sure?", new ConfirmCallback() {
                            public void confirm(boolean result) {
                                if ( result ) {
                                	removeUser(user, row);
                                }
                            }
                        });
                    }
        		}, "Delete");
        HorizontalPanel buttonPanel = new HorizontalPanel();
        buttonPanel.add(editButton);
        buttonPanel.add(delButton);
        
        m_usersTable.setWidget(row, 1, buttonPanel);
        
    }
    
    private void addUser(final UserData user) {
    	// Get user table row count
    	final int row = m_usersTable.getRowCount();
    	// Fill user table with data
        m_usersTable.setText(row, 0, user.userName);
        // User edit button
        ImageButton editButton = new ImageButton(
                "img/edt.gif", 
                new ClickListener() {
                    public void onClick(Widget widget) {
                    	new UserEditDialog(user,                        
                    		new Callback() {
                            	public void onCallback(Object result) {
                            		updateUserRow(row, (UserData)result);
                            	}
                        	});
                    }
                },
                "Edit");
        // User delete button
        ImageButton delButton = new ImageButton("img/delete.png", 
                new ClickListener() {
                    public void onClick(Widget widget) {
                        new ConfirmWindow("Are you sure?", new ConfirmCallback() {
                            public void confirm(boolean result) {
                                if ( result ) {
                                	removeUser(user, row);
                                }
                            }
                        });
                    	removeUser(user, row);
                    }
        		}, "Delete");
        HorizontalPanel buttonPanel = new HorizontalPanel();
        buttonPanel.add(editButton);
        buttonPanel.add(delButton);
        
        m_usersTable.setWidget(row, 1, buttonPanel);
    	
    }

    private void removeUser(UserData user, int row) {
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
                new ErrorWindow(caught.getMessage());
            }

            public void onSuccess(Object result) {
            	// reload user
            	loadUsers();
            }
        };
        m_service.removeUser(user.id, callback);
    }

    
}
