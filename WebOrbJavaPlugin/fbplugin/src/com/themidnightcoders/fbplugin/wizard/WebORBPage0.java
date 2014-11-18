package com.themidnightcoders.fbplugin.wizard;

import org.eclipse.jface.viewers.IStructuredSelection;
import org.eclipse.jface.wizard.IWizardPage;
import org.eclipse.jface.wizard.WizardPage;
import org.eclipse.swt.SWT;
import org.eclipse.swt.layout.GridData;
import org.eclipse.swt.layout.GridLayout;
import org.eclipse.swt.widgets.Composite;
import org.eclipse.swt.widgets.Event;
import org.eclipse.swt.widgets.Label;
import org.eclipse.swt.widgets.List;
import org.eclipse.swt.widgets.Listener;
import org.eclipse.ui.IWorkbench;
import org.eclipse.swt.widgets.Button;

public class WebORBPage0 extends WizardPage implements Listener
{
	IWorkbench workbench;
	IStructuredSelection selection;
	
	Button showAgainButton;
	List message;
	
	public WebORBPage0(IWorkbench workbench, IStructuredSelection selection) 
	{
		super("Page0");
		setTitle("WebORB Data Binder for Flex Builder");
		setDescription("This is a software prepared by MidnightCoders\n");	       
				     
		this.workbench = workbench;
		this.selection = selection;
	}
	
	public void createControl(Composite parent) 
	{
	    // create the composite to hold the widgets
	    Composite composite = new Composite(parent, SWT.NONE);    
	    
	    GridLayout gl = new GridLayout();
	    int ncol = 2;
	    gl.numColumns = ncol;
	    composite.setLayout(gl);	    
	    
	    new Label (composite, SWT.NONE).setText("The message");
	    
	    message = new List(composite, SWT.BORDER | SWT.READ_ONLY  );
	    GridData gd = new GridData(GridData.FILL_HORIZONTAL);
		gd.horizontalSpan =ncol;
		message.setLayoutData(gd);
	    
		new Label (composite, SWT.NONE).setText("The Button");
		
	    showAgainButton = new Button(composite, SWT.CHECK);
	    showAgainButton.setText("Do not display this message again");
	    gd = new GridData(GridData.FILL_HORIZONTAL);
	    gd.horizontalSpan = ncol;
	    showAgainButton.setLayoutData(gd);
	    showAgainButton.setSelection(true);
	    showAgainButton.addListener(SWT.Selection, this);	    
	    
		
		message.add("......................................................");
		message.add("It provides extensive functionality for data binding");
		message.add("of DataGrid components of the GUI to database queries ");
		message.add("Any changes or suggestions/comments should be mailed :");
		message.add("       Mark Piller : mark@themidnightcoders.com       ");
		message.add("The is a test message, please donot take it seriously");
		message.add("......................................................");        	    
	    
	    
	    setControl(composite);	    
	}
	
	public void handleEvent(Event e)
	{					
		getWizard().getContainer().updateButtons();		
	}

	
	public boolean canFlipToNextPage()
	{		
		return true;
	}
	
	public IWizardPage getNextPage()
	{
		saveDataToModel();
		WebORBPage1 page = ((WebORBWizard)getWizard()).page1;
		return page;
	}
	
	private void saveDataToModel()
	{
		WebORBWizard wizard = (WebORBWizard)getWizard();	
		wizard.model.toDisplay = !showAgainButton.getSelection();	
	}
	
}