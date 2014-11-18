package com.themidnightcoders.fbplugin.listener;

import org.eclipse.ui.*;

public class WebORBWindowListener implements IWindowListener
{
    private WebORBPageListener listener = new WebORBPageListener();

    public void windowActivated( IWorkbenchWindow window )
    {// Notifies this listener that the given window has been activated.
        window.addPageListener( listener );
    }

    public void windowClosed( IWorkbenchWindow window )
    {// Notifies this listener that the given window has been closed.
        window.addPageListener( listener );
    }

    public void windowDeactivated( IWorkbenchWindow window )
    {// Notifies this listener that the given window has been deactivated.
        window.addPageListener( listener );
    }

    public void windowOpened( IWorkbenchWindow window )
    {// Notifies this listener that the given window has been opened.
        window.addPageListener( listener );
    }

}