package com.themidnightcoders.fbplugin.listener;

import com.adobe.flexbuilder.editors.mxml.IMXMLEditorListener;
import com.adobe.flexbuilder.editors.mxml.MXMLEditor;


public class WebORBMXMLEditorListener implements IMXMLEditorListener
{
	public void pageChanged(MXMLEditor editor)
	{	
		WebORBMenuItemHandler.CheckMenu(editor);					
	}
}