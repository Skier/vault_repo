/**
 * $Id: ConfirmWindow.java 281 2007-06-20 11:18:16Z moritur $
 */
package com.affilia.cargo.client.ui.util;

import com.google.gwt.user.client.ui.ClickListener;
import com.google.gwt.user.client.ui.Widget;
import com.logicland.gwt.message.client.ImageButton;
import com.logicland.gwt.message.client.PopupWindow;

public class ConfirmWindow 
    extends PopupWindow
    implements ClickListener {


    private static final String MESSAGE_WINDOW_CSS_CLASS = "ll-PopupWindowConfirm";
    private static final String WORK_CSS_CLASS = "ll-PopupWindowConfirm-work";
    
    private final static String CAPTION_MSG = "Choise";
    
    private ImageButton m_okButton = 
        new ImageButton("img/button-apply.png", this, "Ok");
    private ImageButton m_cancelButton = 
        new ImageButton("img/button-cancel.png", this, "Cencel");

    private ConfirmCallback m_callback;

    public ConfirmWindow(String message, ConfirmCallback callback) {
        super(message);
        m_callback = callback;
        
        getControlPanel().add(m_okButton);
        getControlPanel().add(m_cancelButton);
    
        show();
            
    }

    public void onClick(Widget widget) {
        if ( m_okButton == widget ) { 
            hide();
            m_callback.confirm(true);
        } else if ( m_cancelButton == widget ) {
            hide();
            m_callback.confirm(false);
        }
    }    
    
    public String getStyleName() {
        return ConfirmWindow.MESSAGE_WINDOW_CSS_CLASS;
    }

    protected String getRootWindowStyleName() {
        return ConfirmWindow.MESSAGE_WINDOW_CSS_CLASS;
    }
        
    protected String getWorkWindowStyleName() {
        return ConfirmWindow.WORK_CSS_CLASS;
    }
    
    protected String getButtonWindowStyleName() {
        return ConfirmWindow.WORK_CSS_CLASS;
    }

    protected String getCaption() {
        return ConfirmWindow.CAPTION_MSG;
    }

}
    
    