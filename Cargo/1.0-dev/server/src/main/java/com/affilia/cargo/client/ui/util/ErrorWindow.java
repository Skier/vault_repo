/**
 * $Id: ErrorWindow.java 247 2007-06-04 16:09:09Z hatu $
 * 
 */
package com.affilia.cargo.client.ui.util;

import com.google.gwt.user.client.ui.Button;
import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.SimplePanel;
import com.google.gwt.user.client.ui.Widget;
import com.logicland.gwt.message.client.PopupWindow;

public class ErrorWindow   
    extends PopupWindow
    implements ClickListener {

    private static final String MESSAGE_WINDOW_CSS_CLASS = "ll-PopupWindowError";
    private static final String WORK_CSS_CLASS = "ll-PopupWindowError-work";
    private static final String ADDITIONAL_CSS_CLASS = "ll-PopupWindowError-additional";
    
    private static final String CAPTION_MSG = "Exception";
    private static final String CLOUSE_BUTTON_POPUP_MSG = "Close";
    private static final String ADITIONAL_INFO_OPEN_BUTTON_POPUP_MSG = "More";
    private static final String ADITIONAL_INFO_CLOSE_BUTTON_POPUP_MSG = "Hide";
    

    private SimplePanel m_buttonPanel;
    private SimplePanel m_additionalPanel;

    private Button m_additionalInfoButton;
    
    private Button m_closeAdditionInfoButton;
    private Button m_closeButton =
        new Button(ErrorWindow.CLOUSE_BUTTON_POPUP_MSG,this);

    public ErrorWindow(String message) {
        super(message);

        getControlPanel().add(m_closeButton);
        
        show();
    }

    public ErrorWindow(String message, Throwable ex) {
        super(message);

        getControlPanel().add(m_closeButton);

        
        m_additionalInfoButton =
            new Button(ErrorWindow.ADITIONAL_INFO_OPEN_BUTTON_POPUP_MSG, this);
        m_closeAdditionInfoButton =
            new Button(ErrorWindow.ADITIONAL_INFO_CLOSE_BUTTON_POPUP_MSG, this);

        m_buttonPanel = new SimplePanel();
        getButtonPanel().add(m_additionalInfoButton);
        getRootPanel().add(getButtonPanel());

        m_additionalPanel = new SimplePanel();
        Label additionalInfoLabel =
            new Label(ex.getMessage());
        additionalInfoLabel.setStyleName(ErrorWindow.ADDITIONAL_CSS_CLASS);

        getAdditionalPanel().add(additionalInfoLabel);
        
        show();
    }

    public void onClick(Widget widget) {
        if ( m_additionalInfoButton == widget ) {
            showAdditionalInfo();
        } else if ( m_closeAdditionInfoButton == widget ) {
            closeAdditionalInfo();
        } else if ( m_closeButton == widget ) {
            hide();
        }
    }
    
    public String getStyleName() {
        return ErrorWindow.MESSAGE_WINDOW_CSS_CLASS;
    }

    protected String getRootWindowStyleName() {
        return ErrorWindow.MESSAGE_WINDOW_CSS_CLASS;
    }

    protected String getWorkWindowStyleName() {
        return ErrorWindow.WORK_CSS_CLASS;
    }

    protected String getAdditionalWindowStyleName() {
        return ErrorWindow.ADDITIONAL_CSS_CLASS;
    }

    protected String getCaption() {
        return ErrorWindow.CAPTION_MSG;
    }

    private void showAdditionalInfo() {
        getButtonPanel().remove(m_additionalInfoButton);
        getButtonPanel().add(m_closeAdditionInfoButton);
        getRootPanel().add(getAdditionalPanel());
    }

    private void closeAdditionalInfo() {
        getButtonPanel().remove(m_closeAdditionInfoButton);
        getButtonPanel().add(m_additionalInfoButton);
        getRootPanel().remove(getAdditionalPanel());
    }

    private SimplePanel getButtonPanel() {
        return m_buttonPanel;
    }

    private SimplePanel getAdditionalPanel() {
        return m_additionalPanel;
    }


}
