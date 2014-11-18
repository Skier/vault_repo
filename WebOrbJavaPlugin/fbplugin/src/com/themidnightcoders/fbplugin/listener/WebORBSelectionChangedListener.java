package com.themidnightcoders.fbplugin.listener;

import org.eclipse.jface.viewers.ISelectionChangedListener;
import org.eclipse.jface.viewers.SelectionChangedEvent;

import com.adobe.flexbuilder.mxmlmodel.MXMLSelection;

public class WebORBSelectionChangedListener implements ISelectionChangedListener
{
    public void selectionChanged( SelectionChangedEvent e )
    {
        MXMLSelection selected = (MXMLSelection) ( e.getSelection() );
        
        if( selected == null || selected.getLastSelectedItem() == null )
            return;
        
        String compName = selected.getLastSelectedItem().toShortString();
        
        if( compName.startsWith( "StDataGrid" ) )
            WebORBMenuItemHandler.setMenu( true );
        else
            WebORBMenuItemHandler.setMenu( false );
    }
}

// import com.adobe.flexbuilder.mxmlmodel.IMXMLModelListener;
// import com.adobe.flexbuilder.mxmlmodel.MXMLModelEvent;

/*
 * public class WebORBSelectionChangedListener implements IMXMLModelListener {
 * public void modelChanged(MXMLModelEvent e) {
 *  } public void modelAboutToBeChanged(MXMLModelEvent e) {
 *  } }
 */