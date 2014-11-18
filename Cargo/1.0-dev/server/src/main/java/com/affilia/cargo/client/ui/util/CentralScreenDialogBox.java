/**
 * $Id: CentralScreenDialogBox.java 273 2007-06-16 08:31:05Z moritur $
 */
package com.affilia.cargo.client.ui.util;

import com.google.gwt.user.client.Window;
import com.google.gwt.user.client.ui.DialogBox;
import com.google.gwt.user.client.ui.VerticalPanel;

abstract public class CentralScreenDialogBox extends DialogBox {
    private final static String DIALOG_BOX_HIDE_CSS_CLASS = "gwt-DialogBox-hide";
    
    private VerticalPanel m_rootPanel = new VerticalPanel();
        
    public CentralScreenDialogBox(boolean isModal) {
        super(false, isModal);
        setWidget(m_rootPanel);
    }

    public abstract String getStyleName();
    
    public void show() {
        setStyleName(DIALOG_BOX_HIDE_CSS_CLASS);
        setPopupPosition(0, 0);
        super.show();
        center();
        setStyleName(getStyleName());
    }
        
    public VerticalPanel getRootPanel() {
        return m_rootPanel;
    }

    public void center() {
        int left, top;
        left = Window.getClientWidth() / 2 - getWidget().getOffsetWidth() / 2;
        top = Window.getClientHeight() / 2 - getWidget().getOffsetHeight() / 2;
        setPopupPosition(left, top);
    }
       
}
