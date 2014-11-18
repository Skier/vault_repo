package com.themidnightcoders.fbplugin.wizard;

import org.eclipse.swt.events.SelectionListener;
import org.eclipse.swt.events.SelectionEvent;
import org.eclipse.jface.viewers.IStructuredSelection;
import org.eclipse.jface.wizard.IWizardPage;
import org.eclipse.jface.wizard.WizardPage;
import org.eclipse.swt.SWT;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Button;
import org.eclipse.swt.widgets.Combo;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Event;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.widgets.Listener;
import org.eclipse.swt.widgets.Text;
import org.eclipse.ui.IWorkbench;
import org.eclipse.ui.PlatformUI;
import org.eclipse.jface.dialogs.MessageDialog;

import com.themidnightcoders.DatabaseInfoType;
import com.themidnightcoders.fbplugin.service.ServiceFacade;

/**
 * Class representing the first page of the wizard
 */

public class WebORBPage2 extends WizardPage implements Listener
{
    public static final String copyright = "(c) Copyright The Midnight Coders";

    IWorkbench workbench;
    IStructuredSelection selection;

    Combo database;
    Text host;
    Text port;
    Text user;
    Text password;
    Button testConnectionButton;

    WebORBModel model;

    boolean connected = false;

    public WebORBPage2( IWorkbench workbench, IStructuredSelection selection )
    {
        super( "Page2" );
        setTitle( "Connect to a DataBase" );
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

        new Label( composite, SWT.NONE ).setText( "Database:" );
        database = new Combo( composite, SWT.BORDER | SWT.READ_ONLY );
        gd = new GridData( GridData.FILL_HORIZONTAL );

        database.setLayoutData( gd );

        WebORBWizard wizard = (WebORBWizard) getWizard();
        ServiceFacade.getInstance( wizard.model ).fillCombo( database );
        database.addSelectionListener(new SelectionListener() {
        	public void widgetSelected(SelectionEvent event) {
        		setDatabaseDefaults();
        	}
        	public void widgetDefaultSelected(SelectionEvent event) {}
        });

        gd.horizontalSpan = ncol - 2;
        database.setLayoutData( gd );
        database.select( 1 );

        new Label( composite, SWT.NONE ).setText( "" );
        new Label( composite, SWT.NONE ).setText( "Host Name:" );
        host = new Text( composite, SWT.BORDER );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        gd.horizontalSpan = ncol - 2;
        host.setLayoutData( gd );

        // Date of return
        new Label( composite, SWT.NONE ).setText( "" );
        new Label( composite, SWT.NONE ).setText( "Port Number:" );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        port = new Text( composite, SWT.BORDER );
        gd.horizontalSpan = ncol - 2;
        port.setLayoutData( gd );

        createLine( composite, ncol );
        // Departure
        new Label( composite, SWT.NONE ).setText( "User Name:" );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        user = new Text( composite, SWT.BORDER );
        gd.horizontalSpan = ncol - 2;
        user.setLayoutData( gd );

        new Label( composite, SWT.NONE ).setText( "" );
        new Label( composite, SWT.NONE ).setText( "Password:" );
        gd = new GridData( GridData.FILL_HORIZONTAL );
        password = new Text( composite, SWT.PASSWORD | SWT.BORDER );
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
        user.addListener( SWT.KeyUp, this );
        password.addListener( SWT.KeyUp, this );

        setDatabaseDefaults();
    }

    /**
     * @see Listener#handleEvent(Event)
     */
    public void handleEvent( Event event )
    {
        setErrorMessage( null );
        setMessage( null );
        String result = "";

        connected = false;
        if( event.widget == testConnectionButton )
        {
            try
            {
                WebORBWizard wizard = (WebORBWizard) getWizard();
                WebORBModel model = wizard.model;
                saveDataToModel();
                result = ServiceFacade.getInstance( model ).checkHost();

                if( result.toUpperCase().equals( "SUCCESS" ) )
                {
                    connected = true;
                    setMessage( "Connection Successful!", INFORMATION );
                } else {
                    setErrorMessage( result );
                }
            }
            catch( Throwable e )
            {
                setErrorMessage( "Connection Failed! Try Again");
            }
            
        }
        getWizard().getContainer().updateButtons();
    }

    /*
     * Returns the next page. Saves the values from this page in the model
     * associated with the wizard. Initializes the widgets on the next page.
     */

    public IWizardPage getNextPage()
    {
        String result = "";

        WebORBWizard wizard = (WebORBWizard) getWizard();
/*
        if( connected )
        {
            WebORBPage3 page3 = new WebORBPage3( workbench, selection );
            wizard.addPage( page3 );
            return page3;
        }
*/
        saveDataToModel();

        try
        {
            result = ServiceFacade.getInstance( model ).checkHost();

        }
        catch( Throwable e )
        {
            setErrorMessage( "Connection Failed! Try Again" + result );
            return null;
        }

        if( result.toUpperCase().equals( "SUCCESS" ) )
        {
        	ServiceFacade.getInstance(model).addHost();
            WebORBPage3 page3 = new WebORBPage3( workbench, selection );
            wizard.addPage( page3 );
            return page3;
        } else {
            setErrorMessage(result);
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

    private void createLine( Composite parent, int ncol )
    {
        Label line = new Label( parent, SWT.SEPARATOR | SWT.HORIZONTAL | SWT.BOLD );
        GridData gridData = new GridData( GridData.FILL_HORIZONTAL );
        gridData.horizontalSpan = ncol;
        line.setLayoutData( gridData );
    }

    private void saveDataToModel()
    {
        WebORBWizard wizard = (WebORBWizard) getWizard();
        WebORBModel model = wizard.model;
        model.host = host.getText();
        model.password = password.getText();
        model.username = user.getText();
        model.port = port.getText();
        model.type = (DatabaseInfoType) ( database.getData( database.getText() ) );
    }
    
    private void setDatabaseDefaults() {
		DatabaseInfoType type = (DatabaseInfoType) database.getData(database.getText());
		DatabaseDefaults defaults = DatabaseDefaultsFactory.getDefaults(type);
        host.setText(defaults.getDefaultHost());
        port.setText(defaults.getDefaultPort());
        user.setText(defaults.getDefaultUsername());
        password.setText(defaults.getDefaultPassword());
    }
}
