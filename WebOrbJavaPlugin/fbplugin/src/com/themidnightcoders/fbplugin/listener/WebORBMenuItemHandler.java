package com.themidnightcoders.fbplugin.listener;

import org.eclipse.jface.viewers.ISelection;
import org.eclipse.swt.widgets.Menu;
import org.eclipse.swt.widgets.MenuItem;
import org.eclipse.ui.PlatformUI;

import com.adobe.flexbuilder.editors.mxml.MXMLEditor;
import com.adobe.flexbuilder.mxmlmodel.IMXMLModel;
import com.adobe.flexbuilder.mxmlmodel.MXMLSelection;

public class WebORBMenuItemHandler
{
    public static void CheckMenu( MXMLEditor editor )
    {
        WebORBSelectionChangedListener listener = new WebORBSelectionChangedListener();

        if( editor.isDesignEditorActive() )
        {
            Object input;
            try
            {
                if( editor.isCodeEditorActive() )
                    input = editor.getNonStatefulModel();
                else
                    input = editor.getModel();

                if( input instanceof IMXMLModel )
                {
                    ( (IMXMLModel) input ).getSelectionProvider().addSelectionChangedListener( listener );
                    ISelection selection = ( (IMXMLModel) input ).getSelectionProvider().getSelection();
                    MXMLSelection sel = (MXMLSelection) selection;
                    
                    if( sel.getLastSelectedItem().toShortString().startsWith( "StDataGrid" ) )
                        setMenu( true );
                }
            }
            catch( Exception e )
            {
                /*
                 * MessageDialog.openInformation(
                 * PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell(),
                 * "It shows status", e.toString());
                 */
            }
        }
    }

    public static void setMenu( boolean status )
    {
        Menu menu = PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell().getMenuBar();
        MenuItem[] menuItems = menu.getItems();
        
        for( int i = 0; i < menuItems.length; i++ )
            if( menuItems[ i ].getText().endsWith( "WebORB Data Binder" ) )
            {
                menuItems[ i ].setEnabled( status );
                break;
            }
    }
}