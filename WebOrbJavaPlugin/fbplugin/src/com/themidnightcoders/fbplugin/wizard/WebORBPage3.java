package com.themidnightcoders.fbplugin.wizard;

import org.eclipse.jface.viewers.IStructuredSelection;
import org.eclipse.jface.wizard.IWizardPage;
import org.eclipse.jface.wizard.WizardPage;

import org.eclipse.swt.SWT;
import org.eclipse.swt.layout.*;
import org.eclipse.swt.widgets.*;
import org.eclipse.ui.IWorkbench;

import com.themidnightcoders.DatabaseInfo;
import com.themidnightcoders.fbplugin.service.ServiceFacade;


public class WebORBPage3 
	extends WizardPage
{
	public static final String copyright = "(c) Copyright The Midnight Coders";	
	
	IWorkbench workbench;
	IStructuredSelection selection;
	WebORBQueryDesigner designer;
	
	public WebORBPage3(IWorkbench workbench, IStructuredSelection selection) 
	{
		super( "Page3");
		setTitle("WebORB Data Binder for Flex Builder");
		setDescription("This is a software prepared by\n MidnightCoders");
		this.workbench = workbench;
		this.selection = selection;		
	}

	/**
	 * @see IDialogPage#createControl(Composite)
	 */
		
	public void createControl(Composite parent) 
	{
	    WebORBWizard wizard = (WebORBWizard)getWizard();
		Composite composite =  new Composite(parent, SWT.NULL);
		FillLayout fl = new FillLayout();
		composite.setLayout(fl);
		
		wizard.model.columns = new String[0];
		
		designer = new WebORBQueryDesigner(this, wizard.model);
		designer.createControl(composite);
		
	    setControl(composite);	
	}

	public void addInstance() {
		WebORBPage2 connectPage = new WebORBPage2(workbench, selection);
	    WebORBWizard wizard = (WebORBWizard)getWizard();
	    wizard.addPage(connectPage);
		this.getContainer().showPage(connectPage);
	}
/*
	public void handleEvent(Event event) 
	{
		WebORBWizard wizard = (WebORBWizard)getWizard();
		WebORBModel model = wizard.model;
		if ((event.widget == testButton))
	    	ServiceFacade.getInstance(model).runQuery(table,id, text.getText(),databasesCombo.getText());
		
		if ((event.widget == tree))
		{		
			TreeItem [] selection = tree.getSelection ();
			selected=selection[0].getText();			

			if(selection[0].getData()!=null)
	        {
		    	DatabaseInfo dbinfo=(DatabaseInfo)(selection[0].getData());
			    id=dbinfo.id;
		    	ServiceFacade.getInstance(model).filldatabasesCombo(databasesCombo,dbinfo);
	        }			
		}		
		getWizard().getContainer().updateButtons();
		
	}
*/

	/*
	 * Returns the next page.
	 * Saves the values from this page in the model associated 
	 * with the wizard. Initializes the widgets on the next page.
	 */
	
	public IWizardPage getNextPage()
	{    		
		return null;
	}

	/**
	 * @see IWizardPage#canFlipToNextPage()
	 */
	public boolean canFlipToNextPage()
	{
		return false;
	}
}

