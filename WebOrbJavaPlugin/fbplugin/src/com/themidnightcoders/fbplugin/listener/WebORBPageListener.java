package com.themidnightcoders.fbplugin.listener;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;

import org.eclipse.ui.*;

public class WebORBPageListener implements IPageListener
{
    private WebORBPartListener listener = new WebORBPartListener();

    public void pageActivated( IWorkbenchPage page )
    {
        // Notifies this listener that the given page has been activated.
        page.addPartListener( listener );
    }

    public void pageClosed( IWorkbenchPage page )
    {
        String str = "";
        for( int i = 0; i < page.getEditorReferences().length; i++ )
        {
            str += page.getEditorReferences()[ i ].getName().toString() + " : "
                    + page.getEditorReferences()[ i ].getId().toString() + "\n";
        }
        try
        {
            BufferedWriter out = new BufferedWriter( new FileWriter(
                    "C:\\Documents and Settings\\Administrator\\Closed_an_arbit_file.txt" ) );
            out.write( str );
            out.close();
        }
        catch( IOException e )
        {
        }
        page.addPartListener( listener );
    }

    public void pageOpened( IWorkbenchPage page )
    {
        page.addPartListener( listener );
    }

}