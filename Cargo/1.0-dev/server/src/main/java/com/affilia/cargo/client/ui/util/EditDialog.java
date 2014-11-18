/**
 * $Id: EditDialog.java 247 2007-06-04 16:09:09Z hatu $
 *
 */
package com.affilia.cargo.client.ui.util;

import com.google.gwt.user.client.ui.ComplexPanel;
import com.google.gwt.user.client.ui.HasHorizontalAlignment;
import com.google.gwt.user.client.ui.Label;
import com.google.gwt.user.client.ui.VerticalPanel;
import com.logicland.gwt.user.ui.client.widget.DialogWindow;

public abstract class EditDialog extends DialogWindow {
	
	private final static String DIALOG_BOX_CSS_CLASS = "gwt-DialogBox";

	private final static String TILE_MESSAGES_CSS_CLASS = "ll-EditDialog-TileMessages";

	private VerticalPanel m_messagePanel = new VerticalPanel();

	private Label m_messageLable = new Label();

	public EditDialog() {
		m_messagePanel.setStyleName(EditDialog.TILE_MESSAGES_CSS_CLASS);
		m_messagePanel.setHorizontalAlignment(
				HasHorizontalAlignment.ALIGN_CENTER);
		m_messagePanel.setVisible(false);
		m_messagePanel.add(m_messageLable);
		getRootPanel().add(m_messagePanel);
	}

	protected void hide() {
		super.hide();
	}

	public String getStyleName() {
		return EditDialog.DIALOG_BOX_CSS_CLASS;
	}

	private ComplexPanel getMessagePanel() {
		return m_messagePanel;
	}

	public void showWarning(String message) {
		m_messageLable.setText(message);
		getMessagePanel().setVisible(true);
	}

	public void hideWarning() {
		m_messageLable.setText("");
		getMessagePanel().setVisible(false);
	}

	public void setStatus(String message) {
		getMessagePanel().clear();
		Label label = new Label(message);
		getMessagePanel().add(label);
	}

}
