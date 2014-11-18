package com.themidnightcoders.fbplugin.generator;

import java.io.FileWriter;
import java.io.BufferedWriter;
import java.util.Iterator;
import java.util.Map;
import java.util.List;
import org.eclipse.jface.viewers.ISelection;

import com.adobe.flexbuilder.codemodel.common.XMLName;
import com.adobe.flexbuilder.editors.mxml.MXMLEditor;
import com.adobe.flexbuilder.mxmlmodel.IMXMLModel.IModelPosition;
import com.adobe.flexbuilder.mxmlmodel.*;

public class WebORBCodeGeneratorClient
    extends WebORBVelocityCodeGenerator
{

	public WebORBCodeGeneratorClient() {
		super("client.vm", null);
	}
	
	public void generate(String resultFilename, Map context)
		throws Exception
	{
		IMXMLModel model = (IMXMLModel) context.get("editor");
		String[] columns = (String[]) context.get("columns");
		Boolean multitable = (Boolean) context.get("multitable");
		if ( null != model ) {
			createCodeInEditor(model, columns, multitable.booleanValue());
		}
		super.generate(resultFilename, context);
	}
	
	protected void createCodeInEditor(IMXMLModel input, String[] columns, boolean isMultitable)		throws Exception
	{
        ISelection selection = ( (IMXMLModel) input ).getSelectionProvider().getSelection();
        MXMLSelection sel = (MXMLSelection) selection;

        if( sel.getLastSelectedItem().toShortString().startsWith( "StDataGrid" ) )
        {
            IContainerInstance root = (IContainerInstance) ( (IMXMLModel) input ).getRoot();
            String onLoadCallBack = root.getPropertyStringValue( "creationComplete" );
            
            if( onLoadCallBack == null )
                onLoadCallBack = "init()";
            else if( onLoadCallBack.indexOf( "init()" ) == -1 )
                onLoadCallBack += ";init()";
            
            root.setProperty( "creationComplete", onLoadCallBack );

            try
            {
            	IComponentInstance scriptTag = root.newComponentInstance( new XMLName( "http://www.adobe.com/2006/mxml", "Script" ), sel.getLastSelectedItem(), 0 );
            	scriptTag.setProperty( "source", "weborb_client.as" );
            }
            catch(Exception e)
            {
                java.util.List children = root.getChildren();
                Iterator it = children.iterator();
                while( it.hasNext() )
                {
                    IMXMLItem item = (IMXMLItem) it.next();
                    
                    if( item.getName().getName().equals( "Script" ) )
                    {
                        String source = item.getTag().getAttributeStringValue( "source" );
                        
                        if( source != null && source.equals( "weborb_client.as" ) )
                            break;
                        
                        if( item.getLength() == 12 )
                        {
                            item.getTag().setAttribute( "source", "weborb_client.as" );
                            break;
                        }
                    }
                }                        
            }
            //IComponentInstance remoteObj = containerInstance.newComponentInstance( new XMLName(
            //        "http://www.adobe.com/2006/mxml", "RemoteObject" ), null, 0 );

            IComponentInstance inst = (IComponentInstance) sel.getLastSelectedItem();
            IProperty dataGridId = inst.getProperty( "id" );
            String gridId;
            
            if( dataGridId == null )
            {
                gridId = "myDataGrid";
                inst.setProperty( "id", gridId );
            }
            else
            {
                gridId = (String) dataGridId.getValue();
            }
            
            IArray array = inst.getArrayAccessor( "columns" );
            
            while( array.size() != 0 )
                array.get( 0 ).remove();
            
            XMLName name = new XMLName( "http://www.adobe.com/2006/mxml", "DataGridColumn" );
            
            for( int i = 0; i < columns.length; i++ )
            {                       
                IComponentInstance newGridColumn = array.newComponentInstance( name, null, 0 );
                newGridColumn.setProperty( "headerText", columns[ i ] );
                newGridColumn.setProperty( "dataField", columns[ i ] );
            }

            if ( !isMultitable ) {
	            IContainerInstance canvasInstance = (IContainerInstance) inst.getContainer().newComponentInstance(new XMLName( "http://www.adobe.com/2006/mxml", "Canvas" ));
	            canvasInstance.setProperty("x", "10");
	            canvasInstance.setProperty("y", "250");
	            canvasInstance.setProperty("width", "800");
	            canvasInstance.setProperty("height", "" + (columns.length + 3) * 25);
	            canvasInstance.setProperty("borderStyle", "solid");
	            canvasInstance.setProperty("cornerRadius", "10");
	            
	            for( int i = 0; i < columns.length; i++ )
	            {                       
	            	IComponentInstance textInstance = canvasInstance.newComponentInstance(new XMLName( "http://www.adobe.com/2006/mxml", "Text" ));
	            	textInstance.setProperty("x", "60");
	            	textInstance.setProperty("y", "" + ((i+1) * 25));
	            	textInstance.setProperty("text", columns[i]);
	            	
	            	IComponentInstance inputInstance = canvasInstance.newComponentInstance(new XMLName( "http://www.adobe.com/2006/mxml", "TextInput" ));
	            	inputInstance.setProperty("x", "170");
	            	inputInstance.setProperty("y", "" + ((i+1) * 25));
	            	inputInstance.setProperty("width", "130");
	            	inputInstance.setProperty("styleName", "myTextInput");
	            	inputInstance.setProperty("id", columns[i] + "_");
	            	inputInstance.setProperty("editable", "true");
	            }
	            
	        	IComponentInstance updateInstance = canvasInstance.newComponentInstance(new XMLName( "http://www.adobe.com/2006/mxml", "Button" ));
	        	updateInstance.setProperty("x", "170");
	        	updateInstance.setProperty("y", "" + ((columns.length+1) * 25));
	        	updateInstance.setProperty("label", "Update");
	        	updateInstance.setProperty("id", "updateButton");
	        	updateInstance.setProperty("enabled", "true");
	        	updateInstance.setProperty("click", "doUpdateWithForm(event)");
	        	
	        	IComponentInstance insertInstance = canvasInstance.newComponentInstance(new XMLName( "http://www.adobe.com/2006/mxml", "Button" ));
	        	insertInstance.setProperty("x", "250");
	        	insertInstance.setProperty("y", "" + ((columns.length+1) * 25));
	        	insertInstance.setProperty("label", "Insert");
	        	insertInstance.setProperty("id", "insertButton");
	        	insertInstance.setProperty("enabled", "true");
	        	insertInstance.setProperty("click", "doInsertWithForm(event)");
            }
        }
	}
	
}
