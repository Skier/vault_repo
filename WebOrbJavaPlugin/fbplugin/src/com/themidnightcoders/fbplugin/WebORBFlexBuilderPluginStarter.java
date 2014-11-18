package com.themidnightcoders.fbplugin;

import org.eclipse.ui.IStartup;
import org.eclipse.ui.IWorkbenchWindow;
import org.eclipse.ui.IWorkbench;

import org.eclipse.ui.PlatformUI;
import org.eclipse.ui.IWorkbenchPage;

import com.adobe.flexbuilder.editors.mxml.MXMLEditor;
import com.themidnightcoders.fbplugin.listener.WebORBMXMLEditorListener;
import com.themidnightcoders.fbplugin.listener.WebORBMenuItemHandler;
import com.themidnightcoders.fbplugin.listener.WebORBPageListener;
import com.themidnightcoders.fbplugin.listener.WebORBPartListener;

public class WebORBFlexBuilderPluginStarter implements IStartup
{
    public void earlyStartup()
    {
        // Very Important to have the early startup run in the same thread
        // as the workbench initialisation to that it may be able
        // to access the SWT ui elements from the workench

        final IWorkbench workbench = PlatformUI.getWorkbench();
        workbench.getDisplay().asyncExec( new Runnable()
        {
            public void run()
            {
                IWorkbenchWindow window = workbench.getActiveWorkbenchWindow();
                if( window != null )
                {
                    initialize( window );
                }
            }
        } );

    }

    public void initialize( IWorkbenchWindow window )
    {
        WebORBPageListener pagelistener = new WebORBPageListener();
        WebORBPartListener partlistener = new WebORBPartListener();
        IWorkbenchPage page = window.getActivePage();
        
        if( page != null )
        {
            page.addPartListener( partlistener );
            
            if( page.getActiveEditor() != null )
            {
                if( page.getActiveEditor() instanceof MXMLEditor )
                {
                    MXMLEditor editor = (MXMLEditor) page.getActiveEditor();
                    WebORBMenuItemHandler.CheckMenu( editor );
                    WebORBMXMLEditorListener listener = new WebORBMXMLEditorListener();
                    editor.addListener( listener );
                    WebORBMenuItemHandler.CheckMenu( editor );
                }
                else
                    WebORBMenuItemHandler.setMenu( false );
            }
            WebORBMenuItemHandler.setMenu( false );
        }
        else
        {
            window.getWorkbench().getActiveWorkbenchWindow().addPageListener( pagelistener );
            WebORBMenuItemHandler.setMenu( false );
        }
    }
}