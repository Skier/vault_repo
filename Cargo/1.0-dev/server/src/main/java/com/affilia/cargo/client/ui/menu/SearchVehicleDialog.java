/**
 * $Id: SearchVehicleDialog.java 273 2007-06-16 08:31:05Z moritur $
 */
package com.affilia.cargo.client.ui.menu;

import com.affilia.cargo.client.service.CargoServiceAsync;
import com.affilia.cargo.client.service.CargoServiceLocator;
import com.affilia.cargo.client.ui.util.CentralScreenDialogBox;
import com.affilia.cargo.client.ui.util.ErrorWindow;
import com.google.gwt.user.client.rpc.AsyncCallback;
import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.Grid;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.HorizontalPanel;
import com.google.gwt.user.client.ui.KeyboardListener;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.TextBox;
import com.google.gwt.user.client.ui.Widget;
import com.logicland.gwt.util.client.util.Callback;

public class SearchVehicleDialog 
    extends CentralScreenDialogBox
    implements ClickListener {
    
    private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";

    private static final String DIALOG_CAPTION_MSG = "Search vehicle";

    private CargoServiceAsync m_service = CargoServiceLocator.getService();

    private final Button m_searchButton = new Button("search", this);
    private final Button m_cancelButton = new Button("cancel", this);
    
    private final TextBox m_itemIdText = new TextBox();

    private Callback m_callback;
    
    public SearchVehicleDialog(Callback callback) {
        super(true);
        m_callback = callback;
        
        setText(SearchVehicleDialog.DIALOG_CAPTION_MSG);
        
        initRootPanel();
        
        show();
    }

    
    public void onClick(Widget source) {
        if ( source == m_searchButton ) {
            searchVehicle();
        } else if (source == m_cancelButton ) {
            hide();
        }

    }
    
    public boolean onKeyDownPreview(char key, int modifiers) {
        switch(key) {
            case KeyboardListener.KEY_ENTER:
                searchVehicle();
                break;
            case KeyboardListener.KEY_ESCAPE:
                hide();
                break;
        }
        return true;
    }

    public String getStyleName() {
        return SearchVehicleDialog.DIALOG_BOX_CSS_CLASS;
    }
    
    private void initRootPanel() {
        
        getRootPanel().setHorizontalAlignment(HasHorizontalAlignment.ALIGN_CENTER);
        
        Grid formGrid = new Grid(1, 2);
        
        formGrid.setWidget(0, 0, new Label("itemId"));
        formGrid.setWidget(0, 1, m_itemIdText);
        
        getRootPanel().add(formGrid);
        
        HorizontalPanel buttonPanel = new HorizontalPanel();
        
        buttonPanel.add(m_searchButton);
        buttonPanel.add(m_cancelButton);

        getRootPanel().add(buttonPanel);

    }
    
    private void searchVehicle() {
        AsyncCallback callback =  new AsyncCallback() {
            public void onFailure(Throwable caught) {
                new ErrorWindow(caught.getMessage());
            }

            public void onSuccess(Object result) {
                hide();
                SearchVehicleDialog.this.m_callback.onCallback(result);
            }
        };
        m_service.getVehicle(m_itemIdText.getText(), callback);

    }
    
}
