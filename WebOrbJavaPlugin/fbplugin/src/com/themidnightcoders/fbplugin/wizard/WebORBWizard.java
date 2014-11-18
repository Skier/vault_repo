package com.themidnightcoders.fbplugin.wizard;

import java.util.Iterator;
import java.util.Properties;
import java.util.HashMap;
import java.io.*;
import java.net.URL;
import org.eclipse.core.resources.IFile;

import org.eclipse.core.runtime.NullProgressMonitor;
import org.eclipse.core.runtime.Platform;
import org.eclipse.core.runtime.Path;
import org.eclipse.jface.dialogs.MessageDialog;
import org.eclipse.jface.viewers.ISelection;
import org.eclipse.jface.viewers.IStructuredSelection;
import org.eclipse.jface.wizard.Wizard;
import org.eclipse.jface.wizard.ProgressMonitorPart;
import org.eclipse.swt.widgets.Shell;
import org.eclipse.swt.SWT;
import org.eclipse.swt.custom.ScrolledComposite;
import org.eclipse.ui.IEditorPart;
import org.eclipse.ui.INewWizard;
import org.eclipse.ui.IWorkbench;
import org.eclipse.ui.PlatformUI;
import org.eclipse.ui.IFileEditorInput;


import org.osgi.framework.Bundle;
import org.systinet.wasp.webservice.Registry;

import com.adobe.flexbuilder.codemodel.common.XMLName;
import com.adobe.flexbuilder.editors.mxml.MXMLEditor;
import com.adobe.flexbuilder.mxmlmodel.IMXMLModel.IModelPosition;
import com.adobe.flexbuilder.mxmlmodel.*;


import com.themidnightcoders.ServiceSoap;
import com.themidnightcoders.fbplugin.service.QueryDescription;
import com.themidnightcoders.fbplugin.service.ServiceFacade;
import com.themidnightcoders.fbplugin.generator.WebORBCodeGenerator;
import com.themidnightcoders.fbplugin.generator.WebORBCodeGeneratorClient;

public class WebORBWizard extends Wizard implements INewWizard
{
    WebORBPage0 page0;
    WebORBPage1 page1;
    WebORBPage2 page2;
    WebORBPage3 page3;
    WebORBModel model;
    Properties properties;
    // workbench selection when the wizard was started
    protected IStructuredSelection selection;
    // the workbench instance
    protected IWorkbench workbench;

    public WebORBWizard()
    {
        super();
        model = new WebORBModel();
        IEditorPart editor = PlatformUI.getWorkbench().getActiveWorkbenchWindow().getActivePage().getActiveEditor();
        try
        {
            if( editor.getEditorInput() instanceof IFileEditorInput )
            {
                IFile file = ( (IFileEditorInput) editor.getEditorInput() ).getFile();
                model.project = file.getProject();
            }
        }
        catch( Throwable t )
        {
            MessageDialog.openInformation( PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell(), "Failure",
                    t.getClass().getName() );
        }
    }

    public boolean needsPreviousAndNextButtons()
    {
        return true;
    }

    public void addPages()
    {
        String showagain = "YES";
        String url = null;

        String path = model.project.getLocation().makeAbsolute().toString();

        Properties properties = new Properties();
        FileInputStream fis = null;

        try
        {
            fis = new FileInputStream( path + "\\.settings\\weborb.properties" );
            properties.load( fis );
            showagain = properties.getProperty( "showagain" );
            url = properties.getProperty( "url" );

            if( showagain == null || showagain.equals( "YES" ) )
            {
                page0 = new WebORBPage0( workbench, selection );
                addPage( page0 );
                page1 = new WebORBPage1( workbench, selection );
                addPage( page1 );
            }
            // if zeroth page is not to be shown
            else
            {
                if( url != null )
                {// if some url is stored then check whether a connection to
                    // it can be made

                    model.connectionURL = url;
                    try
                    {
                        Bundle bundle = Platform.getBundle( "com.themidnightcoders.fbplugin" );
                        Path pluginPathObj = new Path( "/" );
                        URL confURL = Platform.find( bundle, pluginPathObj );
                        String pluginPath = Platform.resolve( confURL ).toString();

                        if( pluginPath.toLowerCase().startsWith( "file:/" ) )
                            pluginPath = pluginPath.substring( 6 );

                        System.setProperty( "wasp.location", pluginPath );
                        System.setProperty( "wasp.config.location", "conf/clientconf.xml" );

                        model.svcSoap = (ServiceSoap) Registry.lookup( url, ServiceSoap.class );
                        // model.svcSoap.Ping();

                        // if above doesn't give an exception, don't show first
                        // page
                        // now we need to check whether we need to show the
                        // second page

                        if( ServiceFacade.getInstance( model ).hasHosts() )
                        { // if hosts were present then donot show 2nd page
                            page3 = new WebORBPage3( workbench, selection );
                            addPage( page3 );
                            // this is needed to have a next button in the
                            // dialog
                            // page1 = new WebORBPage1( workbench, selection );
                            // addPage( page1 );
                        }
                        else
                        {// show second page
                            page2 = new WebORBPage2( workbench, selection );
                            addPage( page2 );
                            // this is needed to have a next button in the
                            // dialog
                            // page1 = new WebORBPage1( workbench, selection );
                            // addPage( page1 );
                        }
                    }
                    catch( Throwable e )
                    {
                        e.printStackTrace();
                        // if exception is thrown, new url must be entered. Show
                        // first page
                        page1 = new WebORBPage1( workbench, selection );
                        addPage( page1 );
                        // this is needed to have a next button in the dialog
                        // page1 = new WebORBPage1( workbench, selection );
                        // addPage( page1 );
                    }
                }
                else
                {// if there is no url stored, show first page
                    page1 = new WebORBPage1( workbench, selection );
                    addPage( page1 );
                    // this is needed to have a next button in the dialog
                    // page1 = new WebORBPage1( workbench, selection );
                    // addPage( page1 );
                }
            }
        }
        catch( Throwable e )
        {
            e.printStackTrace();
            page0 = new WebORBPage0( workbench, selection );
            addPage( page0 );
            page1 = new WebORBPage1( workbench, selection );
            addPage( page1 );
        }
    }

    public void init( IWorkbench workbench, IStructuredSelection selection )
    {
        this.workbench = workbench;
        this.selection = selection;
    }

    public boolean canFinish()
    {
        return this.getContainer().getCurrentPage().getName().equals( "Page3" );
    }

    public boolean performFinish()
    {
        // here the method for code generation should be called
        IEditorPart editor = PlatformUI.getWorkbench().getActiveWorkbenchWindow().getActivePage().getActiveEditor();
        Shell shell = PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell();
        ScrolledComposite scrolledComposite = new ScrolledComposite(shell, SWT.H_SCROLL | SWT.V_SCROLL | SWT.BORDER);

        HashMap context = new HashMap();
        try
        {
            Object input;
            if( editor instanceof MXMLEditor )
            {
                MXMLEditor mxmlEditor = (MXMLEditor) editor;
                if( mxmlEditor.isCodeEditorActive() ) {
                    input = mxmlEditor.getNonStatefulModel();
                } else {
                    input = mxmlEditor.getModel();
                }

                if( input instanceof IMXMLModel ) {
                    context.put("editor", input);
                }
            }

            String path = model.project.getLocation().makeAbsolute().toString();
            QueryDescription qdesc = model.query;
            String query = qdesc.toQuery();
            
            context.put("columns", model.columns);
            context.put("namespace", "WebORB.Generated");
            context.put("gridId", "myDataGrid");
          	context.put("multitable", new Boolean(1 < qdesc.getTables().length));
            
            // client side generation is only for 1 table for now
            if ( 1 <= qdesc.getTables().length ) {
                String tablename = qdesc.getTables()[0];
                
                if ( null != qdesc.getPrimaryKeyDescription() ) {
                    context.put("primaryKey", qdesc.getPrimaryKeyDescription().columnInfo.name);
                }
                context.put("entity", tablename);
                
                WebORBCodeGenerator clientCodeGenerator = new WebORBCodeGeneratorClient();
                clientCodeGenerator.generate(path + "\\weborb_client.as", context);
            }
            
            // server side code generation
            for (int i=0; i<qdesc.getTables().length; i++) {
            	String tablename = qdesc.getTables()[i];
                ServiceFacade.getInstance(model).generateServerSideCode(model.instanceId, model.database, tablename, query);  
            }
            
            ProgressMonitorPart pm = new ProgressMonitorPart( scrolledComposite, null );
            pm.setTaskName( "Refreshing project" );
            pm.setSize( 400, 200 );
            model.project.refreshLocal( 2, pm );

            String show = "NO";
            if( model.toDisplay )
                show = "YES";

            Properties properties = new Properties();
            FileOutputStream fos = null;

            fos = new FileOutputStream( path + "\\.settings\\weborb.properties" );
            properties.setProperty( "showagain", show );
            properties.setProperty( "url", model.connectionURL );
            properties.store( fos, "WebORB Plugin Configuration File" );
            fos.flush();
            fos.close();
        }
        catch( Throwable t )
        {
            t.printStackTrace();
            ByteArrayOutputStream byteStream = new ByteArrayOutputStream();
            PrintStream ps = new PrintStream( byteStream );
            t.printStackTrace( ps );
            ps.flush();
            ps.close();
            String exceptionStackTrace = new String( byteStream.toByteArray() );
            MessageDialog.openInformation( PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell(), "Failure",
                    exceptionStackTrace );
        }

        this.dispose();
        editor.doSave( new ProgressMonitorPart( scrolledComposite, null ) );

        // dispose service facade
        ServiceFacade.getInstance(model).dispose();

        return true;
    }
}