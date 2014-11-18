package com.themidnightcoders.fbplugin.wizard;

import java.io.*;
import java.net.URL;

import org.eclipse.core.runtime.Path;
import org.eclipse.core.runtime.Platform;
import org.eclipse.jface.viewers.IStructuredSelection;
import org.eclipse.jface.wizard.IWizardPage;
import org.eclipse.jface.wizard.WizardPage;
import org.eclipse.swt.SWT;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Event;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.widgets.Listener;
import org.eclipse.swt.widgets.Text;
import org.eclipse.ui.IWorkbench;
import org.eclipse.ui.PlatformUI;
import org.eclipse.jface.dialogs.MessageDialog;

import org.osgi.framework.Bundle;
import org.systinet.wasp.webservice.ServiceClient;
import org.systinet.wasp.webservice.Registry;

import com.themidnightcoders.ServiceSoap;

/**
 * Class representing the first page of the wizard
 */

public class WebORBPage1 extends WizardPage implements Listener
{
    public static final String copyright = "(c) Copyright The Midnight Coders";
    IWorkbench workbench;
    IStructuredSelection selection;
    Text host;
    Text port;
    Text user;
    Text password;
    Text wvd;
    Button testConnectionButton;
    ServiceClient svc;
    boolean connected = false;

    public WebORBPage1( IWorkbench workbench, IStructuredSelection selection )
    {
        super( "Page1" );
        setTitle( "Connect to WebORB" );
        setDescription( "" );
        this.workbench = workbench;
        this.selection = selection;
    }

    /**
     * @see IDialogPage#createControl(Composite)
     */
    public void createControl( Composite parent )
    {
        // create the composite to hold the widgets
        GridData gd;
        Composite composite = new Composite( parent, SWT.NULL );

        // create the desired layout for this wizard page
        GridLayout gl = new GridLayout();
        int ncol = 3;
        gl.numColumns = ncol;
        composite.setLayout( gl );

        // create the widgets. If the appearance of the widget is different from
        // the default,
        // create a GridData for it to set the alignment and define how much
        // space it will occupy

        new Label( composite, SWT.NONE ).setText( "Host Name:" );
        host = new Text( composite, SWT.BORDER );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        gd.horizontalSpan = ncol - 2;
        host.setLayoutData( gd );
        host.setText( "localhost" );

        new Label( composite, SWT.NONE ).setText( "" );
        new Label( composite, SWT.NONE ).setText( "Port Number:" );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        port = new Text( composite, SWT.BORDER );
        gd.horizontalSpan = ncol - 2;
        port.setLayoutData( gd );
        port.setText( "80" );

        createLine( composite, ncol );

        new Label( composite, SWT.NONE ).setText( "WebORB Virtual Directory:" );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        wvd = new Text( composite, SWT.BORDER );
        gd.horizontalSpan = ncol - 2;
        wvd.setLayoutData( gd );
        wvd.setText( "weborb" );

        createLine( composite, ncol );
        new Label( composite, SWT.NONE ).setText( "User Name:" );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        user = new Text( composite, SWT.BORDER );
        gd.horizontalSpan = ncol - 2;
        user.setLayoutData( gd );

        new Label( composite, SWT.NONE ).setText( "" );
        new Label( composite, SWT.NONE ).setText( "Password:" );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        password = new Text( composite, SWT.PASSWORD | SWT.BORDER);
        gd.horizontalSpan = ncol - 2;
        password.setLayoutData( gd );

        createLine( composite, ncol );

        testConnectionButton = new Button( composite, SWT.PUSH );
        testConnectionButton.setLayoutData( new GridData( GridData.BEGINNING ) );
        testConnectionButton.setText( "Test Connection" );

        // set the composite as the control for this page
        setControl( composite );

        testConnectionButton.addListener( SWT.Selection, this );
        host.addListener( SWT.KeyUp, this );
        port.addListener( SWT.KeyUp, this );
        wvd.addListener( SWT.KeyUp, this );
        user.addListener( SWT.KeyUp, this );
        password.addListener( SWT.KeyUp, this );
    }

    /**
     * @see Listener#handleEvent(Event)
     */
    public void handleEvent( Event event )
    {
        setErrorMessage( null );
        setMessage( null );

        connected = false;

        if( event.widget == testConnectionButton )
            connected = saveDataToModel();

        getWizard().getContainer().updateButtons();
    }

    /*
     * Returns the next page. Saves the values from this page in the model
     * associated with the wizard. Initializes the widgets on the next page.
     */

    public IWizardPage getNextPage()
    {
        WebORBWizard wizard = (WebORBWizard) getWizard();
        /*
         * if(connected) { WebORBPage2 page2 = new WebORBPage2(workbench,
         * selection); wizard.addPage(page2); return page2; }
         */
        if( saveDataToModel() )
        {
            WebORBPage2 page2 = new WebORBPage2( workbench, selection );
            wizard.addPage( page2 );
            return page2;
        }
        else
        {
            MessageDialog.openInformation( PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell(), "Failure!",
                    "Connection Failed! Try Again.." );
            return null;
        }
    }

    /**
     * @see IWizardPage#canFlipToNextPage()
     */
    public boolean canFlipToNextPage()
    {
        return true;
    }

    /*
     * Saves the uses choices from this page to the model. Called on exit of the
     * page
     */
    private boolean saveDataToModel()
    {
        connected = false;

        try
        {
            WebORBWizard wizard = (WebORBWizard) getWizard();
            WebORBModel model = wizard.model;

            String connURL = "http://" + host.getText() + ":" + port.getText() + "/" + wvd.getText()
                    + "/services/Service.asmx?wsdl";
            model.connectionURL = connURL;

            // HashMap props = new HashMap();
            // Wasp.init( props );
            Bundle bundle = Platform.getBundle( "com.themidnightcoders.fbplugin" );
            Path pluginPathObj = new Path( "/" );
            URL confURL = Platform.find( bundle, pluginPathObj );
            String pluginPath = Platform.resolve( confURL ).toString();

            if( pluginPath.toLowerCase().startsWith( "file:/" ) )
                pluginPath = pluginPath.substring( 6 );

            System.setProperty( "wasp.location", pluginPath );
            System.setProperty( "wasp.config.location", "conf/clientconf.xml" );

            model.svcSoap = (ServiceSoap) Registry.lookup( connURL, ServiceSoap.class );
            model.svcSoap.Ping();

            setMessage( "Connection Successful!", INFORMATION );
            connected = true;
        }
        catch( Throwable e )
        {
            setErrorMessage( e.getClass().getName() );// "Connection Failed!
            // Try Again");
            ByteArrayOutputStream byteStream = new ByteArrayOutputStream();
            PrintStream ps = new PrintStream( byteStream );
            e.printStackTrace( ps );
            ps.flush();
            ps.close();
            String exceptionStackTrace = new String( byteStream.toByteArray() );
            MessageDialog.openInformation( PlatformUI.getWorkbench().getActiveWorkbenchWindow().getShell(), "Failure",
                    exceptionStackTrace );

            connected = false;
        }
        return connected;
    }

    private void createLine( Composite parent, int ncol )
    {
        Label line = new Label( parent, SWT.SEPARATOR | SWT.HORIZONTAL | SWT.BOLD );
        GridData gridData = new GridData( GridData.FILL_HORIZONTAL );
        gridData.horizontalSpan = ncol;
        line.setLayoutData( gridData );
    }
}
