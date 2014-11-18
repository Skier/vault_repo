package com.themidnightcoders.fbplugin.listener;

import com.adobe.flexbuilder.editors.mxml.MXMLEditor;
import org.eclipse.ui.*;

public class WebORBPartListener implements IPartListener
{
    public void partClosed( IWorkbenchPart p )
    {
    }

    public void partDeactivated( IWorkbenchPart p )
    {
    }

    public void partBroughtToTop( IWorkbenchPart p )
    {
    }

    public void partOpened( IWorkbenchPart p )
    {
    }

    public void partActivated( IWorkbenchPart p )
    {
        if( p instanceof com.adobe.flexbuilder.editors.mxml.MXMLEditor )
        {
            MXMLEditor editor = (MXMLEditor) p;
            WebORBMXMLEditorListener listener = new WebORBMXMLEditorListener();
            editor.addListener( listener );
            WebORBMenuItemHandler.CheckMenu( editor );
        }
        else
        {
            WebORBMenuItemHandler.setMenu( false );
        }
    }
}